Public Class Figure
    Implements IEnumerable(Of PathPoint)

    '----------------------------------------------------------------------------------------------------------------------
    Private points As New List(Of PathPoint)
    Private refs As New List(Of Boolean) 'Keeps track of which ppoints are references to ppoints in other figures
    Public parent As SVGPath
    Public mirrorOrient As Orientation = Orientation.None
    Private _numMirrored As Integer = 0

    Public Shared Event OnPPointAdded(ByRef sender As Figure, ByRef pp As PathPoint)
    Public Shared Event OnPPointRemoving(ByRef sender As Figure, ByRef pp As PathPoint)
    Public Shared Event OnPPointsClear(ByRef sender As Figure)

    Public Property NumMirrored() As Integer
        Get
            Return _numMirrored
        End Get
        Set(ByVal value As Integer)
            _numMirrored = value
            If _numMirrored = 1 AndAlso Me.Last IsNot Nothing AndAlso Me.Last.nonInteractve Then
                Me.Remove(Me.Last)
            End If
            If _numMirrored <= 0 Then
                mirrorOrient = Orientation.None
            End If
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

        For i As Integer = 0 To points.Count - 1
            pp = points(i)
            dup.Add(pp.Clone(dup), refs(i))
        Next

        Return dup
    End Function

    Public Sub FlipHorizontally()
        Dim moveto As PathPoint = GetMoveto()

        For Each pp As PathPoint In points
            If pp.pointType = PointType.moveto Then Continue For
            pp.Offset(New PointF((moveto.pos.X - pp.pos.X) * 2.0, 0))
        Next
    End Sub

    Public Sub FlipVertically()
        Dim moveto As PathPoint = GetMoveto()

        For Each pp As PathPoint In points
            If pp.pointType = PointType.moveto Then Continue For
            pp.Offset(New PointF(0, (moveto.pos.Y - pp.pos.Y) * 2.0))
        Next
    End Sub

    '----------------------------------------------------------------------------------------------------------------------
    Public Iterator Function GetEnumerator() As IEnumerator(Of PathPoint) Implements IEnumerable(Of PathPoint).GetEnumerator
        For Each pp As PathPoint In points
            Yield pp
        Next
    End Function

    Private Iterator Function GetEnumerator1() As IEnumerator _
    Implements IEnumerable.GetEnumerator
        Yield GetEnumerator()
    End Function

    Public Function Point(index As Integer)
        Return points(index)
    End Function

    Public Function FirstPPoint() As PathPoint
        If points.Count <= 0 Then Return Nothing
        Return points(0)
    End Function

    Public Function LastPPoint() As PathPoint
        If points.Count <= 0 Then Return Nothing
        Return points(points.Count - 1)
    End Function

    Public Function IndexOf(ByRef pp As PathPoint) As Integer
        Return points.IndexOf(pp)
    End Function

    Public Function GetIndex() As Integer
        Return parent.IndexOf(Me)
    End Function

    Public Sub Add(ByRef item As PathPoint, isref As Boolean)
        points.Add(item)
        refs.Add(isref)

        item.RefreshPrevPPoint()

        RaiseEvent OnPPointAdded(Me, item)
    End Sub

    Public Sub Insert(index As Integer, ByRef item As PathPoint, isref As Boolean)
        points.Insert(index, item)
        refs.Insert(index, isref)

        item.RefreshPrevPPoint()
        If index + 1 < points.Count Then
            points(index + 1).prevPPoint = item
        End If

        RaiseEvent OnPPointAdded(Me, item)
    End Sub

    Public Sub Move(destIndex As Integer, ByRef srcItem As PathPoint)
        Dim indx = srcItem.GetIndex
        Dim isref As Boolean = refs(indx)

        Me.Remove(srcItem)
        Me.Insert(destIndex, srcItem, isref)

        If srcItem.mirroredPP IsNot Nothing Then numMirrored -= 1
        srcItem.mirroredPP.mirroredPP = Nothing
    End Sub

    Public Sub RemoveAt(index As Integer)
        RaiseEvent OnPPointRemoving(Me, points(index))

        points(index).Dispose()

        points.RemoveAt(index)
        refs.RemoveAt(index)
        If index < points.Count Then
            points(index).RefreshPrevPPoint()
        End If
    End Sub

    Public Sub Remove(ByRef pp As PathPoint)
        RaiseEvent OnPPointRemoving(Me, pp)
        Me.RemoveAt(pp.GetIndex())
    End Sub

    Public Sub RemoveRange(start As Integer, count As Integer)
        For i As Integer = start To Math.Min(start + count, points.Count - 1)
            RaiseEvent OnPPointRemoving(Me, points(i))
            points(i).Dispose()
        Next
        points.RemoveRange(start, count)
        refs.RemoveRange(start, count)
        If start < points.Count Then
            points(start).RefreshPrevPPoint()
        End If
    End Sub

    Public Function Count() As Integer
        Return points.Count
    End Function

    Public Sub Clear()
        'For Each pp As PathPoint In points.AsEnumerable.Reverse
        '    RaiseEvent OnPPointRemoving(Me, pp)
        'Next
        RaiseEvent OnPPointsClear(Me)
        points.Clear()
        refs.Clear()
    End Sub


    '----------------------------------------------------------------------------------------------------------------------


    Public Function GetPath() As Drawing2D.GraphicsPath
        Dim borrowedMoveto As Boolean = False
        Dim path As New Drawing2D.GraphicsPath

        If points.Count <= 0 Then Return path

        'If Not Me(0).pointType = PointType.moveto Then
        '    Me.Insert(0, GetLastMoveto(GetPointGlobalIndex(Me, Me.Count - 1)), False)
        '    borrowedMoveto = True
        'End If

        For i As Integer = 1 To points.Count - 1
            Dim p1 As PathPoint = points(i - 1)
            Dim p2 As PathPoint = points(i)

            p2.AddToPath(path)
        Next

        path.CloseFigure()

        'If borrowedMoveto = True Then
        '    Me.RemoveAt(0)
        'End If

        Return path
    End Function

    Public Sub DrawPath(graphs As Graphics, pen As Pen, brush As SolidBrush)
        'Static penMirror As New Pen(Color.Purple, 1)

        'penMirror.DashPattern = {3, 10}
        'penMirror.Color = ColorRotate(brush.Color)

        Dim path As Drawing2D.GraphicsPath = Me.GetPath
        graphs.FillPath(brush, path)
        graphs.DrawPath(pen, path)

        'For Each pp As PathPoint In points
        '    If pp.pos Is Nothing Then Continue For
        '    If pp.mirroredPos IsNot Nothing AndAlso pp.isMirrorOrigin Then
        '        graphs.DrawLine(penMirror, SVG.ApplyZoom(pp.pos), SVG.ApplyZoom(pp.mirroredPos.pos))
        '    End If
        '    'BBox test
        '    'If pp.prevPPoint IsNot Nothing Then
        '    '    'graphs.DrawRectangle(penMirror, New Rectangle(pp.GetLeft, pp.prevPPoint.pos.Y, pp.GetRight - pp.GetLeft, Math.Abs(pp.prevPPoint.pos.Y - pp.pos.Y)))
        '    '    'graphs.DrawLine(penMirror, pp.GetLeft, pp.prevPPoint.pos.Y, pp.GetLeft, pp.pos.Y)
        '    '    'graphs.DrawLine(penMirror, pp.GetRight, pp.prevPPoint.pos.Y, pp.GetRight, pp.pos.Y)
        '    '    Dim rc As RectangleF = pp.GetBounds()
        '    '    rc = SVG.ApplyZoom(rc)
        '    '    graphs.DrawRectangle(penMirror, rc.ToRectangle)
        '    'End If
        'Next
    End Sub

    Public Function IsPointRef(index As Integer) As Boolean
        Return refs(index)
    End Function

    Public Function IsPointRef(ByRef pp As PathPoint) As Boolean
        Return refs(points.IndexOf(pp))
    End Function


    Public Function GetClosestPoint(pos As PointF, incSecPoints As Boolean) As PathPoint
        Dim closestDist As Single = Single.PositiveInfinity
        Dim closestPP As PathPoint = Nothing
        Dim dist As Single

        If incSecPoints = False Then
            'No secondary points
            For Each pp As PathPoint In points
                If pp.pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
                dist = LineLength(pp.pos, pos)
                If dist < closestDist Then
                    closestDist = dist
                    closestPP = pp
                End If
            Next
        Else
            'With secondary points (ref points)
            For Each pp As PathPoint In points
                If pp.pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
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

        If points.Count <= 0 Then Return {Nothing, Nothing}

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
        For i As Integer = 0 To points.Count - 1
            If i > 0 Then
                p1 = points(i - 1)
            Else
                p1 = points.Last
            End If
            p2 = points(i)

            If p1.nonInteractve Then Continue For

            dist = LineLength(Midpoint(p1.pos, p2.pos), pos)

            If dist < closestDist Then
                closestDist = dist
                firstIndex = i - 1
                If firstIndex < 0 Then firstIndex = points.Count - 1
                secondIndex = i
            End If
        Next

        Return {points(firstIndex), points(secondIndex)}
    End Function

    'Returns the 2 points closest to 'pos', taking as reference a midpoint between those points because works better
    Public Function GetClosestPointsLineDist(pos As PointF, incSecPoints As Boolean) As PathPoint()
        Dim closestDist As Single = Single.PositiveInfinity
        Dim firstIndex As Integer = 0
        Dim secondIndex As Integer = 0

        If points.Count <= 0 Then Return {Nothing, Nothing}

        If points.Count > 1 Then
            firstIndex = points.Count - 1
            'If points(firstIndex).pointType = PointType.closepath AndAlso Me.Count > 2 Then firstIndex = Me.Count - 2
            secondIndex = 0

            If Not points(0).pointType = PointType.moveto Then
                Dim buf As Integer = firstIndex
                firstIndex = secondIndex
                secondIndex = buf
            End If

            If incSecPoints = False Then
                closestDist = DistanceToLine(pos, points(firstIndex).pos, points(secondIndex).pos)
            Else
                closestDist = DistanceToLine(pos, points(firstIndex).GetClosestPoint(pos), points(secondIndex).GetClosestPoint(pos))
            End If
        End If

        Dim p1, p2 As PathPoint
        Dim dist As Single
        For i As Integer = 1 To points.Count - 1
            p1 = points(i - 1)
            p2 = points(i)

            dist = DistanceToLine(pos, p1.pos, p2.pos)

            If dist < closestDist Then
                closestDist = dist
                firstIndex = i - 1
                secondIndex = i
            End If
        Next

        Return {points(firstIndex), points(secondIndex)}
    End Function

    Public Function GetClosestPointsList(pos As PointF) As IEnumerable(Of KeyValuePair(Of PathPoint, Integer))
        Dim distances As New SortedList(Of PathPoint, Integer)

        For i As Integer = 0 To points.Count - 1
            Dim pp As PathPoint = points(i)
            Dim dist As Integer = LineLength(pos, pp.pos)

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
        For i As Integer = Math.Max(0, start) To Math.Min(points.Count - 1, start + count)
            If points(i).nonInteractve Then Continue For
            SVG.selectedPoints.Add(points(i))
        Next
    End Sub

    Public Sub SelectAllPPoints(Optional clear As Boolean = True)
        SelectPPoints(0, points.Count - 1)
    End Sub

    Public Function HaveMoveto() As Boolean
        Return (points.Count > 0 AndAlso points(0).pointType = PointType.moveto)
    End Function

    Public Function GetMoveto() As PathPoint
        If points.Count > 0 AndAlso points(0).pointType = PointType.moveto Then
            Return points(0)
        End If
        Return Nothing
    End Function

    Public Function AddPPoint(ptype As PointType, ppos As CPointF) As PathPoint
        Return Me.InsertPPoint(ptype, ppos, Me.Count)
    End Function

    Public Function InsertPPointAtPos(ptype As PointType, ppos As CPointF, refPos As Point) As PathPoint
        Static index As Integer

        Dim closest() As PathPoint = Me.GetClosestPointsLineDist(refPos, False)
        index = Me.IndexOf(closest(1))
        If index = 0 Then index = Me.Count

        Return Me.InsertPPoint(ptype, ppos, index)
    End Function

    Public Function InsertPPoint(ptype As PointType, ppos As PointF, index As Integer) As PathPoint
        Static gIndex As Integer

        If ptype = PointType.moveto AndAlso (Me.HaveMoveto OrElse index > 0) Then
            Form_main.SetSelectedCommand(PointType.lineto)
            ptype = PointType.lineto
        End If

        If SVG.SelectedPath.IsEmpty() Then ptype = PointType.moveto
        If index < 0 Then index = points.Count
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
                pp = New PPBezier(ppos, New PointF(ppos.X - 1, ppos.Y - 1), Me)
            Case PointType.smoothQuadraticBezierCurveto
            Case PointType.ellipticalArc
                pp = New PPEllipticalArc(ppos, 1, Me)
            Case Else
        End Select

        If pp Is Nothing Then
            pp = New PathPoint(ptype, ppos, Me)
        End If

        'Add reference to last moveto in the path, if necessary
        If points.Count <= 0 AndAlso Not ptype = PointType.moveto Then
            Dim mp As PathPoint = Me.parent.GetLastMoveto(pp)
            Me.Insert(0, mp, True)
            pp.prevPPoint = mp
            Me.Insert(index + 1, pp, False)
        Else
            Me.Insert(index, pp, False)
        End If

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
        Return points.Count <= 0
    End Function

    Public Sub Scale(scale As Double, Optional centered As Boolean = True)

        If centered Then
            'Uses the middle of the figure as pivotal point
            Dim centralPoint As New PointF(0, 0)
            Dim posDiff As New PointF(0, 0)
            For Each pp As PathPoint In points
                If pp.pos Is Nothing Then Continue For
                centralPoint.X += pp.pos.X
                centralPoint.Y += pp.pos.Y
                pp.pos.X *= scale
                pp.pos.Y *= scale
            Next

            posDiff.X = ((centralPoint.X * scale) - centralPoint.X) / points.Count
            posDiff.Y = ((centralPoint.Y * scale) - centralPoint.Y) / points.Count

            For Each pp As PathPoint In points
                If pp.pos Is Nothing Then Continue For
                pp.pos.X -= posDiff.X
                pp.pos.Y -= posDiff.Y
            Next

        Else
            'Uses the moveto as pivotal point
            Dim moveto As PathPoint = Me.GetMoveto()
            Dim refPointOld As New PointF(0, 0)
            Dim refPointNew As New PointF(0, 0)
            If moveto IsNot Nothing Then refPointOld = moveto.pos

            For Each pp As PathPoint In points
                If pp.pos Is Nothing Then Continue For
                pp.pos.X *= scale
                pp.pos.Y *= scale
            Next

            If moveto IsNot Nothing Then refPointNew = moveto.pos

            For Each pp As PathPoint In points
                If pp.pos Is Nothing Then Continue For
                pp.pos.X -= refPointNew.X - refPointOld.X
                pp.pos.Y -= refPointNew.Y - refPointOld.Y
            Next

        End If
    End Sub

End Class