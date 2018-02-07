Imports System.Text.RegularExpressions

Public Class SVGPath
    Private Shared _idCount As Integer = 1

    Private figures As New List(Of Figure)
    Public selectedPoints As New ListWithEvents(Of PathPoint)
    Public selectedFigures As New ListWithEvents(Of Figure)
    Private _attributes As New Dictionary(Of String, String)
    Private _strokePen As Pen
    Private _fillBrush As SolidBrush

    Public Property Attributes() As Dictionary(Of String, String)
        Get
            Return _attributes
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _attributes = value
        End Set
    End Property

    Public Property StrokeColor() As Color
        Get
            Return HTMLParser.HTMLStringToColor(_attributes("stroke"))
        End Get
        Set(ByVal value As Color)
            _attributes("stroke") = HTMLParser.ColorToHexString(value)
            _strokePen.Color = value
        End Set
    End Property

    Public Property StrokeWidth() As Integer
        Get
            Return CInt(_attributes("stroke-width"))
        End Get
        Set(ByVal value As Integer)
            _attributes("stroke-width") = value
            _strokePen.Width = value
        End Set
    End Property

    Public Property FillColor() As Color
        Get
            Return HTMLParser.HTMLStringToColor(_attributes("fill"))
        End Get
        Set(ByVal value As Color)
            _attributes("fill") = HTMLParser.ColorToHexString(value)
            _fillBrush.Color = value
        End Set
    End Property

    Public Property Id() As String
        Get
            Return _attributes("id")
        End Get
        Set(ByVal value As String)
            value = Regex.Replace(value, "\s+$", "").Replace(" ", "-")
            If value.Length <= 0 Then
                value = "Path_" & _idCount
                _idCount += 1
            End If
            _attributes("id") = value
            RaiseEvent OnIdChanged(Me, value)
        End Set
    End Property

    Public Property SelectedFigure() As Figure
        Get
            Return selectedFigures(0)
        End Get
        Set(ByVal value As Figure)
            selectedFigures.Clear()
            selectedFigures.Add(value)
            RaiseEvent OnSelectFigure(Me, value)
        End Set
    End Property

    'Public Sub SelectionAddFigure(ByRef fig As Figure)
    '    selectedFigures.Add(fig)
    'End Sub
    'Public Sub SelectionRemoveFigure(ByRef fig As Figure)
    '    selectedFigures.Remove(fig)
    'End Sub
    'Public Sub SelectionRemoveFigureAt(ByVal index As Integer)
    '    selectedFigures.RemoveAt(index)
    'End Sub
    'Public Sub SelectionClearFigures()
    '    selectedFigures.Clear()
    'End Sub


    'Public Iterator Function GetSelectedFigures() As IEnumerator(Of Figure)
    '    For Each fig As Figure In selectedFigures
    '        Yield fig
    '    Next
    'End Function

    Public Shared Event OnFigureAdded(ByRef sender As SVGPath, ByRef fig As Figure)
    Public Shared Event OnFigureRemoving(ByRef sender As SVGPath, ByRef fig As Figure)
    Public Shared Event OnFiguresClear(ByRef sender As SVGPath)
    Public Shared Event OnStrokeWidthChanged(ByRef sender As SVGPath)
    Public Shared Event OnIdChanged(ByRef sender As SVGPath, id As String)
    Public Shared Event OnSelectFigure(ByRef sender As SVGPath, ByRef fig As Figure)
    Public Shared Event OnChangeFigureIndex(ByRef sender As SVGPath, oldIndx As Integer, newIndx As Integer)


    Public Sub New()
        _attributes.Add("id", "Path_" & _idCount)
        _idCount += 1
        _attributes.Add("stroke", "lightgray")
        _attributes.Add("fill", "white")
        _attributes.Add("stroke-width", "1")
        _attributes.Add("d", "")

        _fillBrush = New SolidBrush(FillColor)
        _strokePen = New Pen(StrokeColor, StrokeWidth)
        _strokePen.LineJoin = Drawing2D.LineJoin.Miter

        SVG.SelectedPath = Me
        AddNewFigure()
    End Sub

    Public Function InsertNewFigure(index As Integer) As Figure
        Dim newFig As Figure = New Figure(Me)
        figures.Insert(index, newFig)
        RaiseEvent OnFigureAdded(Me, newFig)
        SelectedFigure = newFig

        Return newFig
    End Function

    Public Function AddNewFigure() As Figure
        Return InsertNewFigure(figures.Count)
    End Function

    Public Sub Insert(index As Integer, ByRef fig As Figure)
        figures.Insert(index, fig)
        RaiseEvent OnFigureAdded(Me, fig)
        SelectedFigure = fig
    End Sub

    Public Sub Add(ByRef fig As Figure)
        figures.Add(fig)
        RaiseEvent OnFigureAdded(Me, fig)
        SelectedFigure = fig
    End Sub

    Public Function DuplicateFigure(ByRef fig As Figure) As Figure
        Dim newFig As Figure = fig.Clone
        Insert(fig.GetIndex + 1, newFig)
        Return newFig
    End Function

    Public Sub Remove(ByRef fig As Figure)
        fig.Clear()

        'Always have at least one figure in a path
        If figures.Count > 1 Then
            'RaiseEvent OnFigureRemoving(Me, figures.Last)
            RaiseEvent OnFigureRemoving(Me, fig)
            Dim fi As Integer = fig.GetIndex

            figures.Remove(fig)

            If SelectedFigure Is fig AndAlso figures.Count > 0 Then SelectedFigure = figures(Math.Min(figures.Count - 1, fi))
        End If
    End Sub

    Public Sub Clear()
        RaiseEvent OnFiguresClear(Me)
        For Each fig As Figure In figures
            fig.Clear()
        Next
        figures.Clear()
        If SVG.SelectedPath Is Me Then SVG.selectedPoints.Clear()
    End Sub

    Public Function GetIndex() As Integer
        Return SVG.IndexOf(Me)
    End Function

    Public Iterator Function GetAllPPoints() As IEnumerable(Of PathPoint)
        For Each fig As Figure In figures
            For Each pp As PathPoint In fig
                Yield pp
            Next
        Next
    End Function

    Public Iterator Function GetFigures() As IEnumerable(Of Figure)
        For Each fig As Figure In figures
            Yield fig
        Next
    End Function

    Public Function IndexOf(ByRef fig As Figure) As Integer
        Return figures.IndexOf(fig)
    End Function

    Public Function Figure(ByVal index As Integer) As Figure
        Return figures(index)
    End Function

    Public Sub ChangeFigureIndex(oldIndx As Integer, newIndx As Integer)
        Dim fig As Figure = figures(oldIndx)
        'If destIndex > index Then destIndex -= 1

        'RaiseEvent OnFigureRemoving(Me, fig)
        RaiseEvent OnChangeFigureIndex(Me, oldIndx, newIndx)

        figures.RemoveAt(oldIndx)
        figures.Insert(newIndx, fig)
        SelectedFigure = fig
        'Me.Insert(destIndex, fig)
    End Sub

    Public Sub Offset(ammount As PointF)
        For Each fig As Figure In figures
            For Each pp As PathPoint In fig
                pp.Offset(ammount)
            Next
        Next
    End Sub

    Public Sub Offset(xoff As Single, yoff As Single)
        Offset(New PointF(xoff, yoff))
    End Sub

    'Returns the point's index in the path
    Public Function FigureIndexToPath(ByRef pp As PathPoint) As Integer
        Dim ret As Integer = 0
        For Each fig As Figure In figures
            If fig.Contains(pp) Then
                Return ret + fig.IndexOf(pp)
            End If
            ret += fig.Count
        Next

        Return -1
    End Function

    'Returns the point's index in the path ('index' is point's index inside the figure)
    Public Function FigureIndexToPath(ByRef fig As Figure, ppindex As Integer) As Integer
        Dim ret As Integer = 0
        For Each ifig As Figure In figures
            If ifig.Equals(fig) Then
                Return ret + ppindex
            End If
            ret += ifig.Count
        Next

        Return -1
    End Function

    'Returns the point's index inside it's parent figure, and the parent figure
    Public Function PathIndexToFigure(index As Integer) As KeyValuePair(Of Integer, Figure)
        Dim ret As KeyValuePair(Of Integer, Figure) = Nothing
        Dim i As Integer = 0

        For Each fig As Figure In figures
            If i + fig.Count > index Then
                i = (index - i)
                ret = New KeyValuePair(Of Integer, Figure)(i, fig)
                Return ret
            Else
                i += fig.Count
            End If
        Next

        Return Nothing
    End Function

    Public Sub Draw(ByRef graphs As Graphics)
        'Set drawing to be smooth
        graphs.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        graphs.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        graphs.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        _strokePen.Width = StrokeWidth * SVG.CanvasZoom

        'Draw the Path of every Figure
        For Each fig As Figure In figures
            fig.DrawPath(graphs, _strokePen, _fillBrush)
        Next
    End Sub

    Public Function IsEmpty() As Boolean
        Return figures.Count <= 0 OrElse figures(0).IsEmpty()
    End Function

    Public Function GetLastMoveto(ByRef ppoint As PathPoint) As PathPoint

        For Each pp As PathPoint In Me.GetAllPPoints().Reverse
            If pp.pointType = PointType.moveto Then
                Return pp
            End If
        Next

        Return Nothing
    End Function

    Public Function GetRect() As RectangleF
        Dim pathRc As New RectangleF(99999, 99999, 0, 0)

        For Each pp As PathPoint In Me.GetAllPPoints()
            If pp.pos Is Nothing Then Continue For

            If pp.pos.X < pathRc.X Then
                pathRc.X = pp.pos.X
            ElseIf pp.pos.X > pathRc.Width Then
                pathRc.Width = pp.pos.X
            End If

            If pp.pos.Y < pathRc.Y Then
                pathRc.Y = pp.pos.Y
            ElseIf pp.pos.Y > pathRc.Height Then
                pathRc.Height = pp.pos.Y
            End If
        Next

        pathRc.Width = pathRc.Width - pathRc.X
        pathRc.Height = pathRc.Height - pathRc.Y

        Return pathRc
    End Function

    Public Function GetString(optimize As Boolean) As String
        Dim str As String = ""

        For Each fig As Figure In figures
            For Each pp As PathPoint In fig
                str &= pp.GetString(optimize) & " "
            Next
            str &= "Z"
        Next

        'If Me.GetAllPPoints().Count > 0 AndAlso Me.GetAllPPoints().Last.pointType <> PointType.closepath Then str &= "Z"
        If optimize Then Return OptimizePathD(str)
        Return str
    End Function

    Public Function GetHtml(optimize As Boolean) As String
        Dim str As String = "<path "

        _attributes("d") = GetString(optimize)

        For Each item In _attributes
            str &= item.Key & "=""" & item.Value & """ "
        Next

        Return str & "/>"
    End Function

    Public Sub Scale(scale As Double, Optional centered As Boolean = True)
        For Each fig As Figure In figures
            fig.Scale(scale, centered)
        Next
    End Sub

    Public Function GetClosestPoint(pos As PointF, incSecPoints As Boolean) As PathPoint
        Dim closestDist As Single = Single.PositiveInfinity
        Dim closestPP As PathPoint = Nothing
        Dim dist As Single

        For Each fig As Figure In selectedFigures
            If incSecPoints = False Then
                'No secondary points
                For Each pp As PathPoint In fig
                    If pp.pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
                    dist = LineLength(pp.pos, pos)
                    If dist < closestDist Then
                        closestDist = dist
                        closestPP = pp
                    End If
                Next
            Else
                'With secondary points (ref points)
                For Each pp As PathPoint In fig
                    If pp.pos Is Nothing OrElse pp.nonInteractve = True Then Continue For
                    dist = LineLength(pp.GetClosestPoint(pos), pos)
                    If dist < closestDist Then
                        closestDist = dist
                        closestPP = pp
                    End If
                Next
            End If
        Next
        Return closestPP
    End Function

    'Returns the 2 points closest to 'pos', taking as reference a midpoint between those points because works better
    Public Function GetClosestPointsMidpoint(pos As PointF, incSecPoints As Boolean) As PathPoint()
        Dim closestDist As Single = Single.PositiveInfinity
        Dim first As PathPoint = Nothing
        Dim second As PathPoint = Nothing
        Dim p1, p2 As PathPoint
        Dim dist As Single

        For Each fig As Figure In selectedFigures
            If fig.Count > 1 Then
                first = fig.Last
                'If first.pointType = PointType.closepath AndAlso fig.Count > 2 Then first = fig(fig.Count - 2)
                second = fig.First

                If Not fig.First.pointType = PointType.moveto Then
                    Dim buf As PathPoint = first
                    first = second
                    second = buf
                End If

                If incSecPoints = False Then
                    closestDist = LineLength(Midpoint(first.pos, second.pos), pos)
                Else
                    closestDist = LineLength(Midpoint(first.GetClosestPoint(pos), second.GetClosestPoint(pos)), pos)
                End If
            End If

            For i As Integer = 1 To fig.Count - 1
                p1 = fig(i - 1)
                p2 = fig(i)

                dist = LineLength(Midpoint(p1.pos, p2.pos), pos)

                If dist < closestDist Then
                    first = p1
                    second = p2
                    closestDist = dist
                End If
            Next
        Next

        Return {first, second}

        'Dim ppl As New List(Of PathPoint())
        'For Each fig As Figure In selectedFigures
        '    ppl.Add(fig.GetClosestPointsMidpoint(pos, incSecPoints))
        'Next
        'Dim p1, p2 As PathPoint
        'Dim dist As Single
        'For Each ppa As PathPoint() In ppl
        '    p1 = ppa(0)
        '    p2 = ppa(1)
        '    If p1 Is Nothing OrElse p2 Is Nothing Then Continue For

        '    dist = LineLength(Midpoint(p1.pos, p2.pos), pos)

        '    If dist < closestDist Then
        '        closestDist = dist
        '        first = p1
        '        second = p2
        '    End If
        'Next

        'If first Is Nothing AndAlso second Is Nothing Then
        '    first = ppl(0)(0)
        '    second = ppl(0)(1)
        'End If

        'Return {first, second}
    End Function

    Public Function GetClosestSelectedFigure(pos As PointF) As Figure
        Dim closestDist As Single = Single.PositiveInfinity, dist
        Dim ppa As PathPoint()
        Dim closest As Figure = Nothing
        Dim str As String = "  "

        For Each fig As Figure In selectedFigures
            ppa = fig.GetClosestPointsLineDist(pos, False)

            If ppa(0) IsNot Nothing AndAlso ppa(1) IsNot Nothing Then
                dist = DistanceToLine(pos, ppa(0).pos, ppa(1).pos)
                str &= dist & ", "
                If dist < closestDist Then
                    closest = fig
                    closestDist = dist
                End If
            End If
        Next

        If closest Is Nothing Then Return SelectedFigure
        Return closest
    End Function

    Public Shared Function GetPreviousPPoint(ByRef pp As PathPoint)
        Dim prevpp As PathPoint = pp.prevPPoint
        If prevpp Is Nothing Then
            'Get last point in path, before pp
            For i As Integer = pp.parent.GetIndex - 1 To 0 Step -1
                prevpp = pp.parent.parent.Figure(pp.parent.GetIndex - 1).Last
                If prevpp IsNot Nothing Then Exit For
            Next
        End If

        Return prevpp
    End Function

    Public Shared Sub ResetIdCount()
        _idCount = 1
    End Sub

    Public Sub AppedAttributes(ByRef attrs As Dictionary(Of String, String), fireEvents As Boolean)
        For Each item In attrs
            If fireEvents Then
                Select Case item.Key
                    Case "id"
                        Id = item.Value
                        Continue For
                    Case "stroke-width"
                        StrokeWidth = item.Value
                        Continue For
                    Case "stroke"
                        StrokeColor = HTMLParser.HTMLStringToColor(item.Value)
                        Continue For
                    Case "fill"
                        FillColor = HTMLParser.HTMLStringToColor(item.Value)
                        Continue For
                End Select
            End If

            _attributes(item.Key) = item.Value
        Next
    End Sub

End Class