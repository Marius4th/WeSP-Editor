Imports System.Text.RegularExpressions

Public NotInheritable Class SVG
    Public Shared paths As New ListWithEvents(Of SVGPath)
    Public Shared selectedPaths As New List(Of SVGPath)

    Public Shared CanvasImg As New Bitmap(640, 640)
    Private Shared _canvasSize As New SizeF(64, 64)
    Private Shared _canvasSizeZoomed As New Size(640, 640)
    Private Shared _canvasZoom As Single = 10.0
    Private Shared _stickyGrid As New SizeF(1.0, 1.0)

    Public Shared Event OnCanvasSizeChanged()
    Public Shared Event OnCanvasZoomChanged()
    Public Shared Event OnPathAdded(ByRef path As SVGPath)
    Public Shared Event OnPathRemoving(ByRef path As SVGPath)
    Public Shared Event OnPathClear()
    Public Shared Event OnSelectPath(ByRef path As SVGPath)
    Public Shared Event OnSelectFigure(ByRef fig As Figure)
    Public Shared Event OnSelectPoint(ByRef pp As PathPoint)
    Public Shared Event OnStickyGridChanged()

    Public Shared selectedPoints As New ListWithEvents(Of PathPoint)

    Public Shared placementRefPPoints(1) As PathPoint

    Public Shared Property CanvasSize() As SizeF
        Get
            Return _canvasSize
        End Get
        Set(ByVal value As SizeF)
            _canvasSize = value
            _canvasSizeZoomed = New Size(_canvasSize.Width * _canvasZoom, _canvasSize.Height * _canvasZoom)
            CanvasZoom = _canvasZoom
            RaiseEvent OnCanvasSizeChanged()
        End Set
    End Property

    Public Shared Property CanvasSizeZoomed() As Size
        Get
            Return _canvasSizeZoomed
        End Get
        Set(ByVal value As Size)
            _canvasSizeZoomed = value
            _canvasSize = New Size(_canvasSizeZoomed.Width / _canvasZoom, _canvasSizeZoomed.Height / _canvasZoom)
            RaiseEvent OnCanvasSizeChanged()
        End Set
    End Property

    Public Shared Property CanvasZoom() As Single
        Get
            Return _canvasZoom
        End Get
        Set(ByVal value As Single)
            Dim maxZoom As Single = Math.Min(20, 4000.0F / Math.Max(_canvasSize.Width, _canvasSize.Height))
            _canvasZoom = Math.Min(maxZoom, Math.Max(0.1F, value))
            _canvasSizeZoomed = New Size(_canvasSize.Width * _canvasZoom, _canvasSize.Height * _canvasZoom)
            RaiseEvent OnCanvasZoomChanged()
        End Set
    End Property

    Public Shared Property StikyGrid() As SizeF
        Get
            Return _stickyGrid
        End Get
        Set(ByVal value As SizeF)
            _stickyGrid = value
            RaiseEvent OnStickyGridChanged()
        End Set
    End Property

    Public Shared Property SelectedPath() As SVGPath
        Get
            If selectedPaths.Count <= 0 Then Return Nothing
            Return selectedPaths(0)
        End Get
        Set(ByVal value As SVGPath)
            selectedPaths.Clear()
            selectedPaths.Add(value)
            RaiseEvent OnSelectPath(value)
        End Set
    End Property

    Public Shared Property SelectedFigure() As Figure
        Get
            If SelectedPath Is Nothing Then Return Nothing
            Return SelectedPath.SelectedFigure
        End Get
        Set(ByVal value As Figure)
            If SelectedPath Is Nothing Then Return
            SelectedPath.SelectedFigure = value
            RaiseEvent OnSelectFigure(value)
        End Set
    End Property

    Public Shared Iterator Function GetSelectedFigures() As IEnumerable(Of Figure)
        If SelectedPath IsNot Nothing Then
            For Each fig As Figure In SelectedPath.selectedFigures
                Yield fig
            Next
        Else
            Yield Nothing
        End If
    End Function

    Public Shared Property SelectedPoint() As PathPoint
        Get
            Return selectedPoints(0)
        End Get
        Set(ByVal value As PathPoint)
            If value Is Nothing OrElse (selectedPoints.Count = 1 AndAlso selectedPoints(0) Is value) Then Return
            selectedPoints.Clear()
            selectedPoints.Add(value)
            RaiseEvent OnSelectPoint(value)
        End Set
    End Property

    Public Shared Sub Init()
        AddPath()
        RaiseEvent OnCanvasSizeChanged()
        RaiseEvent OnCanvasZoomChanged()
    End Sub

    Public Shared Event OnCanvasSelectedPointsChanged()

    Public Shared Sub SelectPointsInRect(ByRef rect As RectangleF, Optional clear As Boolean = True)
        If clear Then selectedPoints.Clear()

        For Each path As SVGPath In selectedPaths
            For Each fig As Figure In path.selectedFigures
                For Each pp As PathPoint In fig
                    If pp.pos Is Nothing Then Continue For

                    If rect.Contains(pp.pos.X, pp.pos.Y) Then
                        If clear = False AndAlso selectedPoints.Contains(pp) Then Continue For
                        selectedPoints.Add(pp)
                    End If
                Next
            Next
        Next

        RaiseEvent OnCanvasSelectedPointsChanged()
    End Sub

    Public Shared Sub DeselectPointsInRect(ByRef rect As RectangleF)
        For Each pp As PathPoint In selectedPoints.Reverse
            If pp.pos Is Nothing Then Continue For
            If rect.Contains(pp.pos.X, pp.pos.Y) Then
                selectedPoints.Remove(pp)
            End If
        Next

        RaiseEvent OnCanvasSelectedPointsChanged()
    End Sub

    Public Shared Iterator Function GetAllPPoints() As IEnumerable(Of PathPoint)
        For Each path As SVGPath In paths
            For Each fig As Figure In path.GetFigures
                For Each pp As PathPoint In fig
                    Yield pp
                Next
            Next
        Next
    End Function

    Public Shared Function IsEmpty() As Boolean
        Return paths.Count <= 0 OrElse paths(0).IsEmpty()
    End Function

    Public Shared Function IndexOf(ByRef path As SVGPath) As Integer
        Return paths.IndexOf(path)
    End Function

    Public Shared Function GetHtml() As String
        Dim str As String = "<svg width=""" & _canvasSize.Width & """ height=""" & _canvasSize.Height & """ viewbox=""0 0 " & _canvasSize.Width & " " & _canvasSize.Height & """>" & vbCrLf

        For Each path As SVGPath In paths
            str &= vbTab & path.GetHtml() & vbCrLf
        Next

        str &= "</svg>" & vbCrLf & vbCrLf

        str &= "<!--<wesp "
        str &= "zoom=""" & CanvasZoom & """"
        str &= ">" & vbCrLf

        For Each path As SVGPath In paths
            For Each fig As Figure In path.GetFigures
                For Each pp As PathPoint In fig
                    If pp.mirroredPos IsNot Nothing AndAlso pp.mirroredPP IsNot Nothing Then
                        str &= "<mirror id=""" & path.GetIndex & "," & fig.GetIndex & "," & pp.GetIndex & """"
                        str &= " posi=""" & pp.mirroredPos.GetIndex & """"
                        str &= " ppi=""" & pp.mirroredPP.GetIndex & """"
                        str &= " orig=""" & Convert.ToInt32(pp.isMirrorOrigin) & """"
                        str &= " orient=""" & pp.mirrorOrient & """"
                        str &= ">" & vbCrLf
                    End If
                Next
            Next
        Next

        str &= "</wesp>-->"


        Return str
    End Function

    Public Shared Sub AddPath()
        Dim newPath As SVGPath = New SVGPath
        paths.Add(newPath)
        SelectPath(paths.Count - 1)

        RaiseEvent OnPathAdded(paths.Last)

        newPath.FillColor = SelectedPath.FillColor
        newPath.StrokeColor = SelectedPath.StrokeColor
    End Sub

    Public Shared Sub RemovePath(ByRef path As SVGPath)
        RaiseEvent OnPathRemoving(paths.Last)

        If selectedPaths.Contains(path) Then
            selectedPaths.Remove(path)
        End If

        paths.Remove(path)

        If paths.Count <= 0 Then AddPath()
        If SelectedPath Is Nothing Then SelectedPath = paths(paths.Count - 1)
    End Sub

    Public Shared Sub SelectPath(index As Integer)
        SelectedPath = paths(index)

        RaiseEvent OnSelectPath(paths(index))
    End Sub

    Public Shared Function GetBounds() As RectangleF
        Dim minx As Single = Single.PositiveInfinity
        Dim miny As Single = Single.PositiveInfinity
        Dim maxx As Single = Single.NegativeInfinity
        Dim maxy As Single = Single.NegativeInfinity
        Dim rc As New RectangleF

        For Each path As SVGPath In paths
            For Each fig As Figure In path.GetFigures
                For Each pp As PathPoint In fig
                    If pp.pos Is Nothing Then Continue For
                    rc = pp.GetBounds()
                    If rc.Left < minx Then minx = rc.Left
                    If rc.Right > maxx Then maxx = rc.Right
                    If rc.Top < miny Then miny = rc.Top
                    If rc.Bottom > maxy Then maxy = rc.Bottom
                Next
            Next
        Next

        If minx = Single.PositiveInfinity Then Return New RectangleF(New PointF(0, 0), _canvasSizeZoomed)
        Return New RectangleF(minx, miny, maxx - minx, maxy - miny)
    End Function

    Public Shared Sub Offset(ammount As PointF)
        For Each path As SVGPath In paths
            For Each fig As Figure In path.GetFigures
                For Each pp As PathPoint In fig
                    pp.Offset(ammount)
                Next
            Next
        Next
    End Sub

    Public Shared Sub Offset(xoff As Single, yoff As Single)
        Offset(New PointF(xoff, yoff))
    End Sub

    Public Shared Sub Clear()
        For Each path As SVGPath In paths
            For Each fig As Figure In path.GetFigures
                fig.Clear()
            Next
            path.Clear()
        Next
        selectedPoints.Clear()

        paths.Clear()
        RaiseEvent OnPathClear()

        AddPath()
    End Sub

    Public Shared Function UnZoom(pt As PointF) As PointF
        Return New PointF(pt.X / _canvasZoom, pt.Y / _canvasZoom)
    End Function
    Public Shared Function UnZoom(rc As RectangleF) As RectangleF
        Return New RectangleF(rc.X / _canvasZoom, rc.Y / _canvasZoom, rc.Width / _canvasZoom, rc.Height / _canvasZoom)
    End Function

    Public Shared Function ApplyZoom(pt As PointF) As PointF
        Return New PointF(pt.X * _canvasZoom, pt.Y * _canvasZoom)
    End Function

    Public Shared Function ApplyZoom(rc As RectangleF) As RectangleF
        Return New RectangleF(rc.X * _canvasZoom, rc.Y * _canvasZoom, rc.Width * _canvasZoom, rc.Height * _canvasZoom)
    End Function

    Public Shared Function StickPointToCanvasGrid(pt As CPointF) As CPointF
        Return New CPointF(Math.Round(pt.X / SVG.StikyGrid.Width) * SVG.StikyGrid.Width, Math.Round(pt.Y / SVG.StikyGrid.Height) * SVG.StikyGrid.Height)
    End Function

    Public Shared Sub RefreshPlacementRefs(mpos As PointF)
        placementRefPPoints = {Nothing, Nothing}
        If SVG.SelectedPath IsNot Nothing Then
            Dim closestFig As Figure = SVG.SelectedPath.GetClosestSelectedFigure(mpos)
            If closestFig IsNot Nothing Then
                If placeBetweenClosest = True Then
                    placementRefPPoints = closestFig.GetClosestPointsMidpoint(mpos, False)
                Else
                    placementRefPPoints(0) = closestFig.FirstPPoint
                    placementRefPPoints(1) = closestFig.LastPPoint
                End If
            End If
        End If
    End Sub

    Public Shared Sub ParseString(str As String)
        Dim d As String = "", substr As String = ""
        Dim figData As String()
        Dim ppType As PointType = PointType.moveto
        Dim lastPP As PathPoint = Nothing
        Dim relative As Boolean = False

        SVG.Clear()

        'Parse SVG's Size
        If str.ToLower.Contains("width=""") Then
            SVG.CanvasSize = New SizeF(Convert.ToSingle(GetHTMLAttributeValue(str.ToLower, "width")), SVG.CanvasSize.Height)
        End If
        If str.ToLower.Contains("height=""") Then
            SVG.CanvasSize = New SizeF(SVG.CanvasSize.Width, Convert.ToSingle(GetHTMLAttributeValue(str.ToLower, "height")))
        End If

        For Each path As String In Split(str, "<path")
            If Not path.Contains("d=""") Then Continue For

            If Not SVG.SelectedPath.IsEmpty Then SVG.AddPath()
            path = path.Replace("'", """")

            'Parse path's colors
            If path.ToLower.Contains("stroke=""") Then
                substr = GetHTMLAttributeValue(path.ToLower, "stroke")
                If IsStringHexColor(substr) Then SVG.SelectedPath.StrokeColor = HexStringToColor(substr)
            End If
            If path.ToLower.Contains("fill=""") Then
                substr = GetHTMLAttributeValue(path.ToLower, "fill")
                If IsStringHexColor(substr) Then SVG.SelectedPath.FillColor = HexStringToColor(substr)
            End If

            If path.ToLower.Contains("stroke-width=""") Then
                substr = GetHTMLAttributeValue(path.ToLower, "stroke-width")
                If substr.Length > 0 Then SVG.SelectedPath.StrokeWidth = CInt(substr)
            End If

            'Parse path's commands
            d = GetHTMLAttributeValue(path, "d").Replace(" ", ",").Replace("-", ",-").Replace("z", "Z")

            For Each fig As String In Split(d, "Z")
                If fig.Length <= 1 Then Continue For
                'dData = Regex.Split(d, "([A-Z]+)", RegexOptions.IgnoreCase)
                figData = Regex.Split(fig, "([A-Z]+)", RegexOptions.IgnoreCase)

                If Not SVG.SelectedFigure.IsEmpty Then SVG.SelectedPath.AddNewFigure()

                For Each dat As String In figData
                    If dat.Length <= 0 Then Continue For
                    Select Case dat.ToUpper
                        Case Chr(PointType.moveto), Chr(PointType.lineto), Chr(PointType.horizontalLineto), Chr(PointType.verticalLineto), Chr(PointType.curveto), Chr(PointType.smoothCurveto), Chr(PointType.quadraticBezierCurve), Chr(PointType.smoothQuadraticBezierCurveto), Chr(PointType.ellipticalArc)
                            ppType = Asc(dat.ToUpper)
                            If dat <> dat.ToUpper Then
                                relative = True
                            Else
                                relative = False
                            End If
                        Case Else
                            Dim coords As New List(Of Single)

                            'Parse the string to get all the numbers
                            For Each item As String In Split(dat, ",")
                                If item.Length <= 0 Then Continue For
                                Dim dots As String() = Split(item, ".")

                                If dots.Length > 2 Then
                                    coords.Add(Convert.ToSingle(dots(0) & "." & dots(1)))
                                    For di As Integer = 2 To dots.Count - 1 Step 1
                                        coords.Add(Convert.ToSingle("0." & dots(di)))
                                    Next
                                Else
                                    coords.Add(Convert.ToSingle(item))
                                End If
                            Next

                            Select Case ppType
                                Case PointType.moveto, PointType.lineto
                                    For i As Integer = 1 To coords.Count - 1 Step 2
                                        lastPP = SelectedFigure.AddPPoint(ppType, New CPointF(coords(i - 1), coords(i)))
                                        If relative Then lastPP.RelativeToAbsolute()
                                    Next
                                Case PointType.horizontalLineto
                                    For i As Integer = 0 To coords.Count - 1 Step 1
                                        lastPP = SelectedFigure.AddPPoint(PointType.lineto, New CPointF(coords(i), 0))
                                        If relative Then lastPP.RelativeToAbsolute()
                                        If lastPP.prevPPoint IsNot Nothing Then
                                            lastPP.pos.Y = lastPP.prevPPoint.pos.Y
                                        End If
                                    Next
                                Case PointType.verticalLineto
                                    For i As Integer = 0 To coords.Count - 1 Step 1
                                        lastPP = SelectedFigure.AddPPoint(PointType.lineto, New CPointF(0, coords(i)))
                                        If relative Then lastPP.RelativeToAbsolute()
                                        If lastPP.prevPPoint IsNot Nothing Then
                                            lastPP.pos.X = lastPP.prevPPoint.pos.X
                                        End If
                                    Next
                                Case PointType.curveto
                                    For i As Integer = 5 To coords.Count - 1 Step 6
                                        lastPP = SelectedFigure.AddPPoint(ppType, New CPointF(coords(i - 1), coords(i)))
                                        CType(lastPP, PPCurveto).refPoint2 = New CPointF(coords(i - 3), coords(i - 2))
                                        CType(lastPP, PPCurveto).refPoint1 = New CPointF(coords(i - 5), coords(i - 4))
                                        If relative Then lastPP.RelativeToAbsolute()
                                    Next
                                Case PointType.smoothCurveto

                                Case PointType.quadraticBezierCurve
                                    For i As Integer = 3 To coords.Count - 1 Step 4
                                        lastPP = SelectedFigure.AddPPoint(ppType, New CPointF(coords(i - 1), coords(i)))
                                        CType(lastPP, PPBezier).refPoint = New CPointF(coords(i - 3), coords(i - 2))
                                        If relative Then lastPP.RelativeToAbsolute()
                                    Next
                                Case PointType.smoothQuadraticBezierCurveto

                                Case PointType.ellipticalArc
                                    For i As Integer = 6 To coords.Count - 1 Step 7
                                        lastPP = SelectedFigure.AddPPoint(ppType, New CPointF(coords(i - 1), coords(i)))
                                        CType(lastPP, PPEllipticalArc).sweep = coords(i - 2)
                                        CType(lastPP, PPEllipticalArc).size = New SizeF(coords(i - 6), coords(i - 5))
                                        If relative Then lastPP.RelativeToAbsolute()
                                    Next
                                Case Else
                            End Select
                    End Select
                Next
            Next

            If SelectedFigure.IsEmpty Then SelectedPath.Remove(SelectedFigure)
        Next

        'Parse WeSP's extra data ---------------------------------------------------
        If Not str.Contains("<wesp") Then Return
        Dim wesp As String = Split(Split(str, "<wesp")(1), "</wesp>")(0)

        'Zoom
        If wesp.ToLower.Contains("zoom=""") Then
            CanvasZoom = Convert.ToSingle(GetHTMLAttributeValue(wesp.ToLower, "zoom"))
        End If

        'Mirrored PPoints
        Dim mirrors As String() = Split(wesp, "<mirror", -1, StringSplitOptions.RemoveEmptyEntries)
        Dim pathi, figi, ppi As Integer
        Dim ppData As String()
        For Each item As String In mirrors
            If Not item.Contains("posi") Then Continue For
            substr = GetHTMLAttributeValue(item, "id")
            ppData = Split(substr, ",")
            If ppData.Length = 3 Then
                pathi = Convert.ToInt32(ppData(0))
                figi = Convert.ToInt32(ppData(1))
                ppi = Convert.ToInt32(ppData(2))
            End If

            SVG.paths(pathi).Figure(figi)(ppi).SetMirrorPPoint(SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLAttributeValue(item, "ppi"))), Convert.ToInt32(GetHTMLAttributeValue(item, "orient")))
            SVG.paths(pathi).Figure(figi)(ppi).SetMirrorPos(SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLAttributeValue(item, "posi"))), Convert.ToInt32(GetHTMLAttributeValue(item, "orient")))

            With SVG.paths(pathi).Figure(figi)(ppi)
                '.mirroredPos = SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLPropertyValue(item, "posi")))
                '.mirroredPP = SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLPropertyValue(item, "ppi")))
                .isMirrorOrigin = Convert.ToInt32(GetHTMLAttributeValue(item, "orig"))
                '.mirrorOrient = Convert.ToInt32(GetHTMLPropertyValue(item, "orient"))
            End With
        Next

    End Sub

End Class

