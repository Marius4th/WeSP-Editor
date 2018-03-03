Imports System.Text.RegularExpressions

Public Class BkgTemplate
    Public image As Bitmap
    Public path As String
    Public position As PointF
    Public size As SizeF
    Public keepAspect As Boolean
    Public visible As Boolean

    Public Sub New(img As Bitmap)
        Me.image = img
        Me.path = ""
        Me.position = New PointF(0, 0)
        Me.size = img.Size
        Me.keepAspect = True
        Me.visible = True
    End Sub

    Public Sub New(imgPath As String)
        Me.New(New Bitmap(imgPath))
        Me.path = imgPath
    End Sub
End Class

Public NotInheritable Class SVG
    Public Shared paths As New ListWithEvents(Of SVGPath)
    Public Shared selectedPaths As New List(Of SVGPath)

    Public Shared CanvasImg As New Bitmap(640, 640)
    Private Shared _canvasSize As New SizeF(64.0F, 64.0F)
    Private Shared _canvasSizeZoomed As New Size(640, 640)
    Private Shared _canvasZoom As Single = 10.0F
    Private Shared _canvasOffset As New Point(0, 0)
    Private Shared _stickyGrid As New SizeF(1.0F, 1.0F)
    Private Shared _attributes As New Dictionary(Of String, String)

    Private Shared _bkgTemplates As New List(Of BkgTemplate)
    Public Shared selectedBkgTemp As BkgTemplate = Nothing

    Public Shared selectedPoints As New ListWithEvents(Of PathPoint)

    Public Shared placementRefPPoints(1) As PathPoint

    Public Shared Event OnCanvasSizeChanged()
    Public Shared Event OnCanvasZoomChanged()
    Public Shared Event OnPathAdded(ByRef path As SVGPath)
    Public Shared Event OnPathRemoving(ByRef path As SVGPath)
    Public Shared Event OnPathClear()
    Public Shared Event OnSelectPath(ByRef path As SVGPath)
    Public Shared Event OnSelectPoint(ByRef pp As PathPoint)
    Public Shared Event OnStickyGridChanged()
    Public Shared Event OnChangePathIndex(oldIndx As Integer, newIndx As Integer)
    Public Shared Event OnBkgTemplateAdd(ByRef bkgTemp As BkgTemplate)
    Public Shared Event OnBkgTemplateRemoving(ByRef bkgTemp As BkgTemplate, index As Integer)
    Public Shared Event OnBkgTemplatesClear()
    Public Shared Event OnCanvasOffsetChanged(ByVal newVal As Point)

    Public Shared ReadOnly Property BkgTemplates() As List(Of BkgTemplate)
        Get
            Return _bkgTemplates
        End Get
    End Property

    Public Shared Property CanvasSize() As SizeF
        Get
            Return _canvasSize
        End Get
        Set(ByVal value As SizeF)
            _canvasSize = value
            _canvasSizeZoomed = New Size(_canvasSize.Width * _canvasZoom, _canvasSize.Height * _canvasZoom)
            CanvasZoom = _canvasZoom
            _attributes("viewbox") = "0 0 " & value.Width & " " & value.Height
            _attributes("width") = value.Width & "px"
            _attributes("height") = value.Height & "px"
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

    Public Shared Property CanvasOffset() As Point
        Get
            Return _canvasOffset
        End Get
        Set(ByVal value As Point)
            _canvasOffset = value
            RaiseEvent OnCanvasOffsetChanged(value)
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
        AttributesSetDefaults()

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
                    If pp.pos Is Nothing OrElse pp.nonInteractve Then Continue For

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

    Public Shared Function GetHtml(optimize As Boolean) As String
        Dim str As String = "<svg "

        For Each item In _attributes
            str &= item.Key & "=""" & item.Value & """ "
        Next

        str &= ">" & vbCrLf

        For Each path As SVGPath In paths
            str &= vbTab & path.GetHtml(optimize) & vbCrLf
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
                        str &= " orig=""" & pp.isMirrorOrigin & """"
                        str &= " orient=""" & pp.mirrorOrient & """"
                        str &= " noninteract=""" & pp.nonInteractve & """"
                        str &= "/>" & vbCrLf
                    End If
                Next
            Next
        Next

        For Each bkgtemp As BkgTemplate In _bkgTemplates
            str &= "<bkgtemp path=""" & bkgtemp.path & """"
            str &= " pos=""" & bkgtemp.position.ToStringForm(3) & """"
            str &= " size=""" & bkgtemp.size.ToStringForm(3) & """"
            str &= " kaspect=""" & bkgtemp.keepAspect & """"
            str &= " visible=""" & bkgtemp.visible & """"
            str &= "/>" & vbCrLf
        Next

        str &= "</wesp>-->"

        Return str
    End Function

    Public Shared Function AddPath() As SVGPath
        Dim newPath As SVGPath = New SVGPath
        paths.Add(newPath)
        SelectPath(paths.Count - 1)

        RaiseEvent OnPathAdded(paths.Last)

        newPath.FillColor = SelectedPath.FillColor
        newPath.StrokeColor = SelectedPath.StrokeColor

        Return newPath
    End Function

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

    Public Shared Sub ChangePathIndex(oldIndx As Integer, newIndx As Integer)
        Dim path As SVGPath = paths(oldIndx)
        'If destIndex > index Then destIndex -= 1

        'RaiseEvent OnFigureRemoving(Me, fig)
        RaiseEvent OnChangePathIndex(oldIndx, newIndx)

        paths.RemoveAt(oldIndx)
        paths.Insert(newIndx, path)
        SelectedPath = path
        'Me.Insert(destIndex, fig)
    End Sub

    Public Shared Function GetBounds() As RectangleF
        Dim minx As Single = Single.PositiveInfinity
        Dim miny As Single = Single.PositiveInfinity
        Dim maxx As Single = Single.NegativeInfinity
        Dim maxy As Single = Single.NegativeInfinity
        Dim rc As New RectangleF

        For Each path As SVGPath In paths
            rc = path.GetBounds()
            If rc.Left < minx Then minx = rc.Left
            If rc.Right > maxx Then maxx = rc.Right
            If rc.Top < miny Then miny = rc.Top
            If rc.Bottom > maxy Then maxy = rc.Bottom
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
            path.Clear()
        Next
        selectedPoints.Clear()

        paths.Clear()
        RaiseEvent OnPathClear()

        SVGPath.ResetIdCount()
        AddPath()
    End Sub

    Public Shared Function UnZoom(val As Double) As Double
        Return val / _canvasZoom
    End Function
    Public Shared Function UnZoom(pt As PointF) As PointF
        Return New PointF(pt.X / _canvasZoom, pt.Y / _canvasZoom)
    End Function
    Public Shared Function UnZoom(rc As RectangleF) As RectangleF
        Return New RectangleF(rc.X / _canvasZoom, rc.Y / _canvasZoom, rc.Width / _canvasZoom, rc.Height / _canvasZoom)
    End Function

    Public Shared Function ApplyZoom(val As Double) As Double
        Return val * _canvasZoom
    End Function
    Public Shared Function ApplyZoom(pt As PointF) As PointF
        Return New PointF(pt.X * _canvasZoom, pt.Y * _canvasZoom)
    End Function
    Public Shared Function ApplyZoom(pt As SizeF) As SizeF
        Return New SizeF(pt.Width * _canvasZoom, pt.Height * _canvasZoom)
    End Function
    Public Shared Function ApplyZoom(rc As RectangleF) As RectangleF
        Return New RectangleF(rc.X * _canvasZoom, rc.Y * _canvasZoom, rc.Width * _canvasZoom, rc.Height * _canvasZoom)
    End Function

    Public Shared Function StickPointToCanvasGrid(pt As CPointF) As CPointF
        Return New CPointF(Math.Round(pt.X / _stickyGrid.Width) * _stickyGrid.Width, Math.Round(pt.Y / _stickyGrid.Height) * _stickyGrid.Height)
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

    Public Shared Sub AttributesSetDefaults()
        _attributes.Clear()
        _attributes("viewbox") = "0 0 64 64"
        _attributes("width") = "64px"
        _attributes("height") = "64px"
    End Sub

    Public Shared Sub ParseString(str As String)
        Dim d As String = "", substr As String = ""
        Dim figData As String()
        Dim ppType As PointType = PointType.moveto
        Dim lastPP As PathPoint = Nothing
        Dim relative As Boolean = False

        historyLock = True
        SVG.Clear()
        ClearBkgTemplates()

        AttributesSetDefaults()
        AppedAttributes(HTMLParser.GetAttributes(str), True)
        '_attributes = HTMLParser.GetAttributes(str)

        'Parse SVG's Size
        'SVG.CanvasSize = New SizeF(CSng(_attributes("width").GetNumbers), CSng(_attributes("height").GetNumbers))

        For Each path As String In Split(str, "<path")
            'path = path.Replace("'", """")
            If Not path.Contains("d=""") Then Continue For

            If Not SVG.SelectedPath.IsEmpty Then SVG.AddPath()

            Dim pathAttribs = HTMLParser.GetAttributes(path)
            'SVG.SelectedPath.Attributes = pathAttribs

            SVG.SelectedPath.AppedAttributes(pathAttribs, True)

            'If pathAttribs.ContainsKey("id") Then
            '    SVG.SelectedPath.Id = pathAttribs("id")
            'End If
            ''Parse path's colors
            'If pathAttribs.ContainsKey("stroke") Then
            '    SVG.SelectedPath.StrokeColor = HTMLParser.HTMLStringToColor(pathAttribs("stroke"))
            'End If
            'If pathAttribs.ContainsKey("fill") Then
            '    SVG.SelectedPath.FillColor = HTMLParser.HTMLStringToColor(pathAttribs("fill"))
            'End If
            'If pathAttribs.ContainsKey("stroke-width") Then
            '    substr = pathAttribs("stroke-width").GetNumbers
            '    If substr.Length > 0 Then SVG.SelectedPath.StrokeWidth = CInt(substr)
            'End If

            'Parse path's commands
            d = pathAttribs("d").Replace(" ", ",").Replace("-", ",-").Replace("z", "Z")

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
                                        If SelectedFigure.HasMoveto() Then
                                            lastPP = New PPLineto(New CPointF(coords(i - 1), coords(i)), SelectedFigure)
                                        Else
                                            lastPP = New PPMoveto(New CPointF(coords(i - 1), coords(i)), SelectedFigure)
                                        End If

                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)
                                    Next
                                Case PointType.horizontalLineto
                                    For i As Integer = 0 To coords.Count - 1 Step 1
                                        lastPP = New PPLineto(New CPointF(coords(i), 0), SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)

                                        If lastPP.prevPPoint IsNot Nothing Then
                                            lastPP.Pos.Y = lastPP.PrevPPoint.Pos.Y
                                            lastPP.SetPosition(New PointF(lastPP.Pos.X, lastPP.PrevPPoint.Pos.Y))
                                        End If
                                    Next
                                Case PointType.verticalLineto
                                    For i As Integer = 0 To coords.Count - 1 Step 1
                                        lastPP = New PPLineto(New CPointF(0, coords(i)), SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)

                                        If lastPP.PrevPPoint IsNot Nothing Then
                                            lastPP.SetPosition(New PointF(lastPP.PrevPPoint.Pos.X, lastPP.Pos.Y))
                                        End If
                                    Next
                                Case PointType.curveto
                                    For i As Integer = 5 To coords.Count - 1 Step 6
                                        lastPP = New PPCurveto(New CPointF(coords(i - 1), coords(i)),
                                                               New CPointF(coords(i - 5), coords(i - 4)),
                                                               New PointF(coords(i - 3), coords(i - 2)),
                                                               SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)
                                    Next
                                Case PointType.smoothCurveto
                                    For i As Integer = 3 To coords.Count - 1 Step 4
                                        lastPP = New PPCurveto(New CPointF(coords(i - 1), coords(i)),
                                                               New PointF(coords(i - 3), coords(i - 2)),
                                                               New PointF(coords(i - 3), coords(i - 2)),
                                                               SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        CType(lastPP, PPCurveto).ReflectPrevPP()
                                        If relative Then lastPP.RelativeToAbsolute(True)
                                    Next
                                Case PointType.quadraticBezierCurve
                                    For i As Integer = 3 To coords.Count - 1 Step 4
                                        lastPP = New PPQuadraticBezier(New CPointF(coords(i - 1), coords(i)),
                                                                       New PointF(coords(i - 3), coords(i - 2)),
                                                                       SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)
                                    Next
                                Case PointType.smoothQuadraticBezierCurveto
                                    For i As Integer = 1 To coords.Count - 1 Step 2
                                        lastPP = New PPQuadraticBezier(New CPointF(coords(i - 1), coords(i)),
                                                                       New CPointF(coords(i - 1), coords(i)),
                                                                       SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        CType(lastPP, PPQuadraticBezier).ReflectPrevPP()
                                        If relative Then lastPP.RelativeToAbsolute(True)
                                    Next
                                Case PointType.ellipticalArc
                                    For i As Integer = 6 To coords.Count - 1 Step 7
                                        lastPP = New PPEllipticalArc(New CPointF(coords(i - 1), coords(i)),
                                                                         New PointF(coords(i - 6), coords(i - 5)),
                                                                         coords(i - 4),
                                                                         coords(i - 3),
                                                                         coords(i - 2),
                                                                         SelectedFigure)
                                        SelectedFigure.Add(lastPP, False)
                                        If relative Then lastPP.RelativeToAbsolute(False)
                                    Next
                                Case Else
                            End Select
                    End Select
                Next
            Next

            If SelectedFigure.IsEmpty Then SelectedPath.Remove(SelectedFigure)
        Next

        'Make sure UI gets updated with the new data (by firing the path selection event)
        SVG.SelectedPath = SVG.SelectedPath

        'Parse WeSP's extra data ---------------------------------------------------
        If Not str.Contains("<wesp") Then Return
        Dim wesp As String = Split(Split(str, "<wesp")(1), "</wesp>")(0)

        Dim wesphAttribs = HTMLParser.GetAttributes(wesp)
        'Zoom
        CanvasZoom = wesphAttribs.GetValue("zoom", "10").GetNumbers

        'Mirrored PPoints
        Dim mirrors As String() = Split(wesp, "<mirror", -1, StringSplitOptions.RemoveEmptyEntries)
        Dim pathi, figi, ppi As Integer
        Dim ppData As String()
        Dim pp, mirPP, mirPos As PathPoint
        Dim orient As Orientation
        For Each item As String In mirrors
            Dim itemAttribs = HTMLParser.GetAttributes(item)
            If Not itemAttribs.ContainsKey("posi") Then Continue For

            substr = itemAttribs("id")
            ppData = Split(substr, ",")
            If ppData.Length = 3 Then
                pathi = Convert.ToInt32(ppData(0))
                figi = Convert.ToInt32(ppData(1))
                ppi = Convert.ToInt32(ppData(2))
            End If

            pp = SVG.paths(pathi).Figure(figi)(ppi)
            mirPP = SVG.paths(pathi).Figure(figi)(itemAttribs.GetValue("ppi", "0").GetNumbers)
            mirPos = SVG.paths(pathi).Figure(figi)(itemAttribs.GetValue("posi", "0").GetNumbers)
            orient = itemAttribs.GetValue("orient", "0").GetNumbers

            pp.SetMirrorPPoint(mirPP, orient)
            Form_main.Pic_canvas.Refresh()
            pp.SetMirrorPos(mirPos, orient)
            Form_main.Pic_canvas.Refresh()

            With pp
                '.mirroredPos = SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLPropertyValue(item, "posi")))
                '.mirroredPP = SVG.paths(pathi).Figure(figi)(Convert.ToInt32(GetHTMLPropertyValue(item, "ppi")))
                .isMirrorOrigin = CBool(itemAttribs("orig"))
                .nonInteractve = CBool(itemAttribs("noninteract"))
                '.mirrorOrient = Convert.ToInt32(GetHTMLPropertyValue(item, "orient"))
            End With
        Next

        Dim bkgtemps As String() = Split(mirrors.Last, "<bkgtemp", -1, StringSplitOptions.RemoveEmptyEntries)
        For Each item As String In bkgtemps
            Dim itemAttribs = HTMLParser.GetAttributes(item)
            If Not itemAttribs.ContainsKey("path") Then Continue For
            Dim newBkgt As New BkgTemplate(itemAttribs("path"))
            newBkgt.position.Parse(itemAttribs("pos"))
            newBkgt.size.Parse(itemAttribs("size"))
            newBkgt.keepAspect = CBool(itemAttribs("kaspect"))
            newBkgt.visible = CBool(itemAttribs("visible"))
            AddBkgTemplate(newBkgt)
        Next
        historyLock = False
        AddToHistory()
    End Sub

    Public Shared Function GetSelectedPPLastInPath() As PathPoint
        Dim higherIndex As Integer = -1
        Dim higherPP As PathPoint = Nothing
        For Each pp As PathPoint In selectedPoints
            If pp.GetIndex() > higherIndex Then
                higherIndex = pp.GetIndex()
                higherPP = pp
            End If
        Next

        Return higherPP
    End Function

    Public Shared Sub SortSelectedPoints()
        Dim oredered As ListWithEvents(Of PathPoint) = selectedPoints.OrderBy(Function(pp) pp.GetIndex()).ToList
        'For Each pp As PathPoint In oredered
        '    Console.WriteLine(pp.GetIndex())
        'Next
        'selectedPoints = oredered
        selectedPoints.Clear()
        oredered.CopyTo(selectedPoints)
    End Sub


    Public Shared Sub AppedAttributes(ByRef attrs As Dictionary(Of String, String), fireEvents As Boolean)
        For Each item In attrs
            If fireEvents Then
                Select Case item.Key
                    Case "width"
                        CanvasSize = New SizeF(item.Value.GetNumbers("1"), CanvasSize.Height)
                        Continue For
                    Case "height"
                        CanvasSize = New SizeF(CanvasSize.Width, item.Value.GetNumbers("1"))
                        Continue For
                End Select
            End If

            _attributes(item.Key) = item.Value
        Next
    End Sub

    Public Shared Sub AddBkgTemplate(ByRef bkgTemp As BkgTemplate)
        _bkgTemplates.Add(bkgTemp)
        RaiseEvent OnBkgTemplateAdd(bkgTemp)
    End Sub

    Public Shared Sub RemoveBkgTemplate(ByRef bkgTemp As BkgTemplate)
        RaiseEvent OnBkgTemplateRemoving(bkgTemp, _bkgTemplates.IndexOf(bkgTemp))
        If selectedBkgTemp Is bkgTemp Then selectedBkgTemp = Nothing
        _bkgTemplates.Remove(bkgTemp)
    End Sub

    Public Shared Sub RemoveBkgTemplateAt(index As Integer)
        RaiseEvent OnBkgTemplateRemoving(_bkgTemplates(index), index)
        If selectedBkgTemp Is _bkgTemplates(index) Then selectedBkgTemp = Nothing
        _bkgTemplates.RemoveAt(index)
    End Sub

    Public Shared Sub ClearBkgTemplates()
        RaiseEvent OnBkgTemplatesClear()
        _bkgTemplates.Clear()
        selectedBkgTemp = Nothing
    End Sub

    Public Shared Function GetBitmap() As Bitmap
        Dim bmp As New Bitmap(SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height)
        Dim grxCanvas As Graphics = Graphics.FromImage(bmp)

        grxCanvas.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        grxCanvas.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        grxCanvas.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For Each path As SVGPath In SVG.paths
            path.Draw(grxCanvas)
        Next

        Return bmp
    End Function

    Public Shared Sub OffsetCanvas(offx As Integer, offy As Integer)
        _canvasOffset.X += offx
        _canvasOffset.Y += offy
        RaiseEvent OnCanvasOffsetChanged(_canvasOffset)
    End Sub

End Class

