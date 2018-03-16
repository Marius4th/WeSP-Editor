Public Class Figure
    Implements IEnumerable(Of PathPoint)

    '----------------------------------------------------------------------------------------------------------------------
    Private _points As New List(Of PathPoint)
    Private _refs As New List(Of Boolean) 'Keeps track of which ppoints are references to ppoints in other figures
    Public parent As SVGPath
    Public mirrorOrient As Orientation = Orientation.None
    Private _numMirrored As Integer = 0
    Private _transform As New Drawing2D.Matrix

    Public Shared Event OnPPointAdded(ByRef sender As Figure, ByRef pp As PathPoint)
    Public Shared Event OnPPointRemoving(ByRef sender As Figure, ByRef pp As PathPoint)
    Public Shared Event OnPPointsClear(ByRef sender As Figure)

    Public Property NumMirrored() As Integer
        Get
            Return _numMirrored
        End Get
        Set(ByVal value As Integer)
            If value < _numMirrored AndAlso value = 1 AndAlso Me.Last IsNot Nothing AndAlso Me.Last.nonInteractve Then
                Me.Remove(Me.Last)
            End If
            If value <= 0 Then
                mirrorOrient = Orientation.None
            End If
            _numMirrored = value
        End Set
    End Property

    Public Sub New(ByRef paPath As SVGPath)
        parent = paPath
        'SVG.SelectedFigure = Me
    End Sub

    Public Function Clone(Optional pa As SVGPath = Nothing) As Figure
        If pa Is Nothing Then pa = Me.parent
        Dim dup As New Figure(pa)
        Dim pp As PathPoint

        For i As Integer = 0 To _points.Count - 1
            pp = _points(i)
            dup.Add(pp.Clone(dup), _refs(i))
        Next

        Return dup
    End Function

    Public Sub FlipHorizontally()
        Dim moveto As PathPoint = GetMoveto()

        For Each pp As PathPoint In _points
            If pp.pointType = PointType.moveto Then Continue For
            pp.Offset(New PointF((moveto.Pos.X - pp.Pos.X) * 2.0, 0), False)
            pp.FlipSecondary(Orientation.Horizontal)
        Next
    End Sub

    Public Sub FlipVertically()
        Dim moveto As PathPoint = GetMoveto()

        For Each pp As PathPoint In _points
            If pp.pointType = PointType.moveto Then Continue For
            pp.Offset(New PointF(0, (moveto.Pos.Y - pp.Pos.Y) * 2.0), False)
            pp.FlipSecondary(Orientation.Vertical)
        Next
    End Sub

    '----------------------------------------------------------------------------------------------------------------------
    Public Iterator Function GetEnumerator() As IEnumerator(Of PathPoint) Implements IEnumerable(Of PathPoint).GetEnumerator
        For Each pp As PathPoint In _points
            Yield pp
        Next
    End Function

    Private Iterator Function GetEnumerator1() As IEnumerator _
    Implements IEnumerable.GetEnumerator
        Yield GetEnumerator()
    End Function

    Public Function Point(index As Integer)
        Return _points(index)
    End Function

    Public Function FirstPPoint() As PathPoint
        If _points.Count <= 0 Then Return Nothing
        Return _points(0)
    End Function

    Public Function LastPPoint() As PathPoint
        If _points.Count <= 0 Then Return Nothing
        Return _points(_points.Count - 1)
    End Function

    Public Function IndexOf(ByRef pp As PathPoint) As Integer
        Return _points.IndexOf(pp)
    End Function

    Public Function GetIndex() As Integer
        Return parent.IndexOf(Me)
    End Function

    Public Sub Add(ByRef item As PathPoint, isref As Boolean)
        'Add reference to last moveto in the path, if necessary
        If _points.Count <= 0 AndAlso Not item.pointType = PointType.moveto Then
            Dim mp As PathPoint = Me.parent.GetLastMoveto(item)
            If mp IsNot Nothing Then
                Me.Insert(0, mp, True)
                'item.SetPrevPPoint(mp)
                Me.Insert(1, item, False)
            End If
        Else
            _points.Add(item)
            _refs.Add(isref)
        End If

        item.RefreshSeccondaryData()

        RaiseEvent OnPPointAdded(Me, item)
    End Sub

    Public Sub Insert(index As Integer, ByRef item As PathPoint, isref As Boolean)

        'Add reference to last moveto in the path, if necessary
        If index = 0 AndAlso Not item.pointType = PointType.moveto Then
            Dim mp As PathPoint = Me.parent.GetLastMoveto(item)
            If mp IsNot Nothing Then
                Me.Insert(0, mp, True)
                'item.SetPrevPPoint(mp)
                Me.Insert(1, item, False)
            End If
        Else
            _points.Insert(index, item)
            _refs.Insert(index, isref)
        End If

        item.RefreshSeccondaryData()
        If index + 1 < _points.Count Then
            _points(index + 1).RefreshSeccondaryData()
        End If

        RaiseEvent OnPPointAdded(Me, item)
    End Sub

    Public Sub ChangePPointIndex(ByRef srcItem As PathPoint, destIndex As Integer)
        Dim indx = srcItem.GetIndex
        Dim isref As Boolean = _refs(indx)

        Me.Remove(srcItem)
        Me.Insert(destIndex, srcItem, isref)

        'If srcItem.mirroredPP IsNot Nothing Then numMirrored -= 1
        srcItem.mirroredPP.mirroredPP = Nothing
    End Sub

    Public Sub RemoveAt(index As Integer)
        RaiseEvent OnPPointRemoving(Me, _points(index))

        _points(index).Dispose()

        _points.RemoveAt(index)
        _refs.RemoveAt(index)
        If index < _points.Count Then
            _points(index).RefreshSeccondaryData()
        End If
    End Sub

    Public Sub Remove(ByRef pp As PathPoint)
        RaiseEvent OnPPointRemoving(Me, pp)
        Me.RemoveAt(pp.GetIndex())
    End Sub

    Public Sub RemoveRange(start As Integer, count As Integer)
        For i As Integer = start To Math.Min(start + count, _points.Count - 1)
            RaiseEvent OnPPointRemoving(Me, _points(i))
            _points(i).Dispose()
        Next
        _points.RemoveRange(start, count)
        _refs.RemoveRange(start, count)
        If start < _points.Count Then
            _points(start).RefreshSeccondaryData()
        End If
    End Sub

    Public Function Count() As Integer
        Return _points.Count
    End Function

    Public Sub Clear()
        'For Each pp As PathPoint In points.AsEnumerable.Reverse
        '    RaiseEvent OnPPointRemoving(Me, pp)
        'Next
        RaiseEvent OnPPointsClear(Me)
        _points.Clear()
        _refs.Clear()
    End Sub


    '----------------------------------------------------------------------------------------------------------------------


    Public Function GetPath() As Drawing2D.GraphicsPath
        Dim path As New Drawing2D.GraphicsPath

        If _points.Count <= 0 Then Return path

        For i As Integer = 1 To _points.Count - 1
            Dim p1 As PathPoint = _points(i - 1)
            Dim p2 As PathPoint = _points(i)

            p2.AddToPath(path)
        Next

        path.CloseFigure()

        Return path
    End Function

    Public Sub DrawPath(graphs As Graphics, ByRef pen As Pen, ByRef brush As SolidBrush)
        Dim path As Drawing2D.GraphicsPath = Me.GetPath()
        path.Transform(_transform)

        'Dim translateMatrix As New Drawing2D.Matrix
        'translateMatrix.Translate(offset.X, offset.Y)
        'path.Transform(translateMatrix)

        graphs.FillPath(brush, path)
        If pen.Width > 0 Then graphs.DrawPath(pen, path)
    End Sub

    Public Function IsPointRef(index As Integer) As Boolean
        Return _refs(index)
    End Function

    Public Function IsPointRef(ByRef pp As PathPoint) As Boolean
        Return _refs(_points.IndexOf(pp))
    End Function

    Public Function GetClosestPoint(pos As PointF, incSecPoints As Boolean) As PathPoint
        Dim closestDist As Single = Single.PositiveInfinity
        Dim closestPP As PathPoint = Nothing
        Dim dist As Single

        If incSecPoints = False Then
            'No secondary points
            For Each pp As PathPoint In _points
                If pp.Pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
                dist = LineLength(pp.Pos, pos)
                If dist < closestDist Then
                    closestDist = dist
                    closestPP = pp
                End If
            Next
        Else
            'With secondary points (ref points)
            For Each pp As PathPoint In _points
                If pp.Pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
                dist = LineLength(pp.GetClosestPoint(pos), pos)
                If dist < closestDist Then
                    closestDist = dist
                    closestPP = pp
                End If
            Next
        End If

        Return closestPP
    End Function

    'Returns the 2 points closest to 'pos', taking as reference a midpoint between those points because works better
    Public Function GetClosestPointsMidpoint(pos As PointF, incSecPoints As Boolean) As PathPoint()
        Dim closestDist As Single = Single.PositiveInfinity
        Dim firstIndex As Integer = 0
        Dim secondIndex As Integer = 0

        If _points.Count <= 0 Then Return {Nothing, Nothing}

        'If points.Count > 1 Then
        '    firstIndex = points.Count - 1
        '    'If points(firstIndex).pointType = PointType.closepath AndAlso Me.Count > 2 Then firstIndex = Me.Count - 2
        '    secondIndex = 0

        '    If Not points(0).pointType = PointType.moveto Then
        '        Dim buf As Integer = firstIndex
        '        firstIndex = secondIndex
        '        secondIndex = buf
        '    End If

        '    If incSecPoints = False Then
        '        closestDist = LineLength(Midpoint(points(firstIndex).pos, points(secondIndex).pos), pos)
        '    Else
        '        closestDist = LineLength(Midpoint(points(firstIndex).GetClosestPoint(pos), points(secondIndex).GetClosestPoint(pos)), pos)
        '    End If
        'End If

        Dim p1, p2 As PathPoint
        Dim dist As Single
        For i As Integer = 0 To _points.Count - 1
            If i > 0 Then
                p1 = _points(i - 1)
            Else
                p1 = _points.Last
            End If
            p2 = _points(i)

            If p1.nonInteractve Then Continue For

            dist = LineLength(Midpoint(p1.Pos, p2.Pos), pos)

            If dist < closestDist Then
                closestDist = dist
                firstIndex = i - 1
                If firstIndex < 0 Then firstIndex = _points.Count - 1
                secondIndex = i
            End If
        Next

        Return {_points(firstIndex), _points(secondIndex)}
    End Function

    'Returns the 2 points closest to 'pos', taking as reference a midpoint between those points because works better
    Public Function GetClosestPointsLineDist(pos As PointF, incSecPoints As Boolean) As PathPoint()
        Dim closestDist As Single = Single.PositiveInfinity
        Dim firstIndex As Integer = 0
        Dim secondIndex As Integer = 0

        If _points.Count <= 0 Then Return {Nothing, Nothing}

        If _points.Count > 1 Then
            firstIndex = _points.Count - 1
            'If points(firstIndex).pointType = PointType.closepath AndAlso Me.Count > 2 Then firstIndex = Me.Count - 2
            secondIndex = 0

            If Not _points(0).pointType = PointType.moveto Then
                Dim buf As Integer = firstIndex
                firstIndex = secondIndex
                secondIndex = buf
            End If

            If incSecPoints = False Then
                closestDist = DistanceToLine(pos, _points(firstIndex).Pos, _points(secondIndex).Pos)
            Else
                closestDist = DistanceToLine(pos, _points(firstIndex).GetClosestPoint(pos), _points(secondIndex).GetClosestPoint(pos))
            End If
        End If

        Dim p1, p2 As PathPoint
        Dim dist As Single
        For i As Integer = 1 To _points.Count - 1
            p1 = _points(i - 1)
            p2 = _points(i)

            dist = DistanceToLine(pos, p1.Pos, p2.Pos)

            If dist < closestDist Then
                closestDist = dist
                firstIndex = i - 1
                secondIndex = i
            End If
        Next

        Return {_points(firstIndex), _points(secondIndex)}
    End Function

    Public Function GetClosestPointsList(pos As PointF) As IEnumerable(Of KeyValuePair(Of PathPoint, Integer))
        Dim distances As New SortedList(Of PathPoint, Integer)

        For i As Integer = 0 To _points.Count - 1
            Dim pp As PathPoint = _points(i)
            Dim dist As Integer = LineLength(pos, pp.Pos)

            distances.Add(pp, dist)
        Next

        Dim spv = From c In distances
                  Order By c.Value
                  Select c

        Return spv
    End Function

    Public Sub SelectPPoints(start As Integer, count As Integer, Optional clear As Boolean = True)
        If clear Then SVG.selectedPoints.Clear()
        start = Math.Max(0, start)
        For i As Integer = Math.Max(0, start) To Math.Min(_points.Count - 1, start + count)
            If _points(i).nonInteractve Then Continue For
            SVG.selectedPoints.Add(_points(i))
        Next
    End Sub

    Public Sub SelectAllPPoints(Optional clear As Boolean = True)
        SelectPPoints(0, _points.Count - 1)
    End Sub

    Public Function HasMoveto() As Boolean
        Return (_points.Count > 0 AndAlso _points(0).pointType = PointType.moveto)
    End Function

    Public Function GetMoveto() As PathPoint
        If _points.Count > 0 AndAlso _points(0).pointType = PointType.moveto Then
            Return _points(0)
        End If
        Return Nothing
    End Function

    Public Function AddNewPPoint(ptype As PointType, ppos As CPointF) As PathPoint
        Return Me.InsertNewPPoint(ptype, ppos, Me.Count)
    End Function

    Public Function InsertNewPPointAtPos(ptype As PointType, ppos As CPointF, refPos As Point) As PathPoint
        Static index As Integer

        Dim closest() As PathPoint = Me.GetClosestPointsLineDist(refPos, False)
        index = Me.IndexOf(closest(1))
        If index = 0 Then index = Me.Count

        Return Me.InsertNewPPoint(ptype, ppos, index)
    End Function

    Public Function InsertNewPPoint(ptype As PointType, ppos As PointF, index As Integer) As PathPoint
        Static gIndex As Integer

        If ptype = PointType.moveto AndAlso (Me.HasMoveto OrElse index > 0) Then
            ptype = PointType.lineto
        End If

        If SVG.SelectedPath.IsEmpty() Then ptype = PointType.moveto
        If index < 0 Then index = _points.Count
        gIndex = SVG.SelectedPath.FigureIndexToPath(Me, index)

        Dim pp As PathPoint = Nothing

        Select Case ptype
            Case PointType.moveto
                pp = New PPMoveto(ppos, Me)
            Case PointType.lineto
                pp = New PPLineto(ppos, Me)
            Case PointType.horizontalLineto
            Case PointType.verticalLineto
            Case PointType.curveto
                pp = New PPCurveto(ppos, New PointF(ppos.X, ppos.Y), New PointF(ppos.X + 2, ppos.Y + 2), Me)
            Case PointType.smoothCurveto
            Case PointType.quadraticBezierCurve
                pp = New PPQuadraticBezier(ppos, New PointF(ppos.X - 1, ppos.Y - 1), Me)
            Case PointType.smoothQuadraticBezierCurveto
            Case PointType.ellipticalArc
                pp = New PPEllipticalArc(ppos, New PointF(1, 1), 0, False, False, Me)
            Case Else
        End Select

        If pp Is Nothing Then
            pp = New PathPoint(ptype, ppos, Me)
        End If

        Me.Insert(index, pp, False)

        Return pp
    End Function

    'Public Function MovePPointBetweenNeighbours(pp As PathPoint) As Integer
    '    Dim index As Integer = pp.GetIndex
    '    Dim nextPP As PathPoint = Nothing
    '    If index + 1 < points.Count - 1 Then
    '        nextPP = points(index + 1)
    '    End If

    '    points.RemoveAt(index)
    '    index = Me.GetClosestPointsMidpoint(pp.pos.ToPoint, False)(0).GetIndex()
    '    points.Insert(index, pp)

    '    pp.RefreshPrevPPoint()
    '    If nextPP IsNot Nothing Then nextPP.RefreshPrevPPoint()
    '    If index + 1 < points.Count - 1 Then
    '        points(index + 1).RefreshPrevPPoint()
    '    End If
    'End Function

    Public Function IsEmpty() As Boolean
        Return _points.Count <= 0
    End Function

    Public Function GetCenterPoint() As PointF
        Dim centralPoint As New PointF(0, 0)
        For Each pp As PathPoint In _points
            If pp.Pos Is Nothing Then Continue For
            centralPoint.X += pp.Pos.X
            centralPoint.Y += pp.Pos.Y
        Next

        centralPoint.X /= _points.Count
        centralPoint.Y /= _points.Count

        Return centralPoint
    End Function

    'Scales the figure modifying it's points
    Public Sub Scale(sx As Single, sy As Single, Optional centered As Boolean = True, Optional pivotPt As CPointF = Nothing)
        Dim posDiff As New PointF(0, 0)

        If pivotPt Is Nothing Then
            If centered Then
                'Uses the middle of the figure as pivotal point
                pivotPt = GetCenterPoint()
            Else
                'Use the moveto as pivotal point
                Dim moveto As PathPoint = Me.GetMoveto()
                If moveto IsNot Nothing Then pivotPt = moveto.Pos
            End If
        End If

        posDiff.X = (1 - sx) * pivotPt.X
        posDiff.Y = (1 - sy) * pivotPt.Y

        For Each pp As PathPoint In _points
            If pp.Pos Is Nothing Then Continue For
            pp.Multiply(sx, sy)
            pp.Offset(posDiff.X, posDiff.Y)
        Next
    End Sub

    'Applyies scaling to the path of the figure but doesn't change the values of it's points
    Public Sub TransformScale(sx As Single, sy As Single, Optional centered As Boolean = True, Optional pivotPt As CPointF = Nothing)
        Dim posDiff As New PointF(0, 0)

        If pivotPt Is Nothing Then
            If centered Then
                'Uses the middle of the figure as pivotal point
                pivotPt = SVG.ApplyZoom(GetCenterPoint())
            Else
                'Use the moveto as pivotal point
                Dim moveto As PathPoint = Me.GetMoveto()
                If moveto IsNot Nothing Then pivotPt = SVG.ApplyZoom(moveto.Pos)
            End If
        Else
            pivotPt = SVG.ApplyZoom(pivotPt)
        End If

        posDiff.X = (1 - sx) * pivotPt.X
        posDiff.Y = (1 - sy) * pivotPt.Y

        _transform.Reset()
        _transform.Translate(posDiff.X, posDiff.Y)
        _transform.Scale(sx, sy)
    End Sub

End Class