'WeSP, a program for creating/editing simple SVG graphics for webs.
'Copyright(C) 2018  Marius I.V., @marius4th

'This program Is free software: you can redistribute it And/Or modify
'it under the terms Of the GNU General Public License As published by
'the Free Software Foundation, either version 3 Of the License, Or
'(at your option) any later version.

'This program Is distributed In the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty Of
'MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License For more details.

'You should have received a copy Of the GNU General Public License
'along with this program.  If Not, see < https: //www.gnu.org/licenses/>.

Imports System.Windows.Input

Public Class Form_main

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Custom Events

    Public Sub InitHandlers()
        AddHandler SVG.OnCanvasSizeChanged, AddressOf SVG_OnCanvasSizeChanged
        AddHandler SVG.OnCanvasZoomChanged, AddressOf SVG_OnCanvasZoomChanged
        AddHandler SVG.OnPathAdded, AddressOf SVG_OnPathAdded
        AddHandler SVG.OnPathRemoving, AddressOf SVG_OnPathRemoving
        AddHandler SVG.OnPathClear, AddressOf SVG_OnPathClear
        AddHandler SVG.OnSelectPath, AddressOf SVG_OnSelectPath
        AddHandler SVG.OnSelectPoint, AddressOf SVG_OnSelectPoint
        AddHandler SVG.OnStickyGridChanged, AddressOf SVG_OnStickyGridChanged
        AddHandler SVG.OnChangePathIndex, AddressOf SVG_OnChangePathIndex

        AddHandler SVG.selectedPoints.OnAdd, AddressOf SVGSelectedPoints_OnAdd
        AddHandler SVG.selectedPoints.OnRemoving, AddressOf SVGSelectedPoints_OnRemoving
        AddHandler SVG.selectedPoints.OnRemovingRange, AddressOf SVGSelectedPoints_OnRemovingRange
        AddHandler SVG.selectedPoints.OnClear, AddressOf SVGSelectedPoints_OnClear

        AddHandler Figure.OnPPointAdded, AddressOf Figure_OnPPointAdded
        AddHandler Figure.OnPPointRemoving, AddressOf Figure_OnPPointRemoving
        AddHandler Figure.OnPPointsClear, AddressOf Figure_OnPPointsClear

        AddHandler SVGPath.OnFigureAdded, AddressOf SVGPath_OnFigureAdded
        AddHandler SVGPath.OnFigureRemoving, AddressOf SVGPath_OnFigureRemoving
        AddHandler SVGPath.OnFiguresClear, AddressOf SVGPath_OnFiguresClear
        AddHandler SVGPath.OnStrokeWidthChanged, AddressOf SVGPath_OnStrokeWidthChanged
        AddHandler SVGPath.OnIdChanged, AddressOf SVGPath_OnIdChanged
        AddHandler SVGPath.OnSelectFigure, AddressOf SVGPath_OnSelectFigure
        AddHandler SVGPath.OnChangeFigureIndex, AddressOf SVGPath_OnChangeFigureIndex

        AddHandler PathPoint.OnModified, AddressOf PPoint_OnModified
    End Sub

    Public Sub SVG_OnCanvasSizeChanged()
        Pic_canvas.Size = New Size(Math.Ceiling(SVG.CanvasSizeZoomed.Width), Math.Ceiling(SVG.CanvasSizeZoomed.Height))
        Pic_canvas.Refresh()
        If Num_canvasWidth.Value <> SVG.CanvasSize.Width Then
            Num_canvasWidth.Value = SVG.CanvasSize.Width
        End If
        If Num_canvasHeight.Value <> SVG.CanvasSize.Height Then
            Num_canvasHeight.Value = SVG.CanvasSize.Height
        End If

        Pic_preview.Size = SVG.CanvasSize.ToSize
        If Form_result.Visible Then
            Form_result.Pic_realSize.Size = SVG.CanvasSize.ToSize
        End If

        AddToHistory()
    End Sub

    Public Sub SVG_OnCanvasZoomChanged()
        Pic_canvas.Size = SVG.CanvasSizeZoomed
        Pic_canvas.Refresh()
        If Num_zoom.Value <> SVG.CanvasZoom Then
            Num_zoom.Value = SVG.CanvasZoom
        End If

        gridZoomed = New SizeF(grid.Width * SVG.CanvasZoom, grid.Height * SVG.CanvasZoom)

        Lab_zoom.Text = "Zoom: " & Math.Round(SVG.CanvasZoom * 100, 1) & "%"

        RefreshBackGrid()
    End Sub

    Public Sub SVG_OnPathAdded(ByRef path As SVGPath)
        Lb_paths.Items.Add(path.Id)
        Lb_paths.SelectedIndex = Lb_paths.Items.Count - 1
        Col_stroke.BackColor = path.StrokeColor
        Col_fill.BackColor = path.FillColor

        AddToHistory()
    End Sub

    Public Sub SVG_OnPathRemoving(ByRef path As SVGPath)
        Lb_paths.Items.RemoveAt(path.GetIndex())

        AddToHistory()
    End Sub

    Public Sub SVG_OnPathClear()
        Lb_paths.Items.Clear()

        AddToHistory()
    End Sub

    Public Sub SVG_OnSelectPath(ByRef path As SVGPath)
        Col_stroke.BackColor = path.StrokeColor
        Col_fill.BackColor = path.FillColor
        Num_strokeWidth.Value = path.StrokeWidth

        Lb_figures.Items.Clear()
        For Each fig In path.GetFigures
            Lb_figures.Items.Add("Figure_" & fig.GetIndex() + 1)
        Next

        'Change selected figure
        Lb_figures.SelectionMode = SelectionMode.None
        Lb_figures.SelectionMode = SelectionMode.MultiExtended
        If SVG.SelectedPath IsNot Nothing Then
            For Each fig As Figure In SVG.SelectedPath.selectedFigures.Reverse
                Lb_figures.SelectedIndices.Add(fig.GetIndex())
            Next
        End If

        Pic_canvas.Invalidate()
    End Sub

    Public Sub SVG_OnSelectPoint(ByRef pp As PathPoint)
    End Sub

    Public Sub SVG_OnStickyGridChanged()
        If SVG.StikyGrid.Width <> Num_stikyGWidth.Value Then
            Num_stikyGWidth.Value = SVG.StikyGrid.Width
        End If
        If SVG.StikyGrid.Height <> Num_stickyGHeight.Value Then
            Num_stickyGHeight.Value = SVG.StikyGrid.Height
        End If
        RefreshBackGrid()
        'Pic_canvas.Invalidate()
    End Sub

    Public Sub SVG_OnChangePathIndex(oldIndx As Integer, newIndx As Integer)
        Dim oldItem = Lb_paths.Items(oldIndx)
        Lb_paths.Items.RemoveAt(oldIndx)
        Lb_paths.Items.Insert(newIndx, oldItem)
    End Sub

    '----------------------------------------------------------------------------------------------------------------------------

    Public Sub SVGPath_OnFigureAdded(ByRef sender As SVGPath, ByRef fig As Figure)
        If SVG.SelectedPath IsNot sender Then Return
        Lb_figures.Items.Add("Figure_" & fig.GetIndex() + 1)
        'Lb_figures.SelectedIndices.Clear()
        'Lb_figures.SelectedIndex = fig.GetIndex

        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub SVGPath_OnFigureRemoving(ByRef sender As SVGPath, ByRef fig As Figure)
        Lb_figures.Items.RemoveAt(fig.GetIndex())
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub SVGPath_OnFiguresClear(ByRef sender As SVGPath)
        Lb_figures.Items.Clear()
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub SVGPath_OnStrokeWidthChanged(ByRef sender As SVGPath)
        If sender Is SVG.SelectedPath Then
            Num_strokeWidth.Value = sender.StrokeWidth
        End If
    End Sub

    Public Sub SVGPath_OnIdChanged(ByRef sender As SVGPath, id As String)
        Lb_paths.Items.Item(sender.GetIndex()) = id
    End Sub

    Public Sub SVGPath_OnSelectFigure(ByRef sender As SVGPath, ByRef fig As Figure)
        If SVG.SelectedPath IsNot sender Then Return
        If Lb_figures.Items.Count <= 0 Then Return

        Lb_figures.SelectionMode = SelectionMode.One
        Lb_figures.SelectedIndex = fig.GetIndex()
        Lb_figures.SelectionMode = SelectionMode.MultiExtended
        Pic_canvas.Invalidate()
    End Sub

    Public Sub SVGPath_OnChangeFigureIndex(ByRef sender As SVGPath, oldIndx As Integer, newIndx As Integer)
        If SVG.SelectedPath IsNot sender Then Return
        Dim oldItem = Lb_figures.Items(oldIndx)
        Lb_figures.Items.RemoveAt(oldIndx)
        Lb_figures.Items.Insert(newIndx, oldItem)
    End Sub

    '----------------------------------------------------------------------------------------------------------------------------

    Public Sub Figure_OnPPointAdded(ByRef sender As Figure, ByRef pp As PathPoint)
        If sender.HaveMoveto Then EnableButton(But_moveto, False)
        If pp.pointType = PointType.moveto Then
            SetSelectedCommand(PointType.lineto)
        End If

        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub Figure_OnPPointRemoving(ByRef sender As Figure, ByRef pp As PathPoint)
        If SVG.selectedPoints.Contains(pp) Then SVG.selectedPoints.Remove(pp)
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub Figure_OnPPointsClear(ByRef sender As Figure)
        If SVG.SelectedFigure Is sender Then
            SVG.selectedPoints.Clear()
            EnableButton(But_moveto, True)
        End If
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub PPoint_OnModified(ByRef pp As PathPoint)
        'Static tim As Timer = Nothing
        'If tim Is Nothing Then
        '    tim = New Timer
        '    tim.Interval = 33
        '    AddHandler tim.Tick, Sub(ByVal sender As Object, ByVal e As EventArgs)
        '                             Dim ppt As PathPoint = sender.tag
        '                             Form1.Lb_figurePoints.Items.Item(ppt.GetIndex) = ppt.GetString(False)
        '                             Form1.Lb_points.Items.Item(ppt.GetIndexInPath) = ppt.GetString(False)
        '                             tim.Enabled = False
        '                         End Sub
        'End If
        'tim.Enabled = True
        'tim.Tag = pp

        ''Form1.Pic_canvas.Invalidate()
        Dim index As Integer = SVG.selectedPoints.IndexOf(pp)
        If index >= 0 Then
            Lb_selPoints.Items.Item(index) = pp.GetString(False)
        End If
        Pic_canvas.Invalidate()

    End Sub

    Public Sub SVGSelectedPoints_OnAdd(sender As ListWithEvents(Of PathPoint), d As PathPoint)
        Lb_selPoints.Items.Add(d.GetString(False))
        Pic_canvas.Invalidate()

        If SVG.SelectedPath IsNot Nothing AndAlso SVG.SelectedFigure.numMirrored > 0 Then
            Dim anyMirrored As Boolean = False
            Dim prevMirrored As Boolean = False

            For Each pp As PathPoint In SVG.selectedPoints
                If pp.mirroredPos IsNot Nothing Then
                    anyMirrored = True
                    If prevMirrored Then Exit For
                End If

                If pp.prevPPoint Is Nothing Then Continue For
                If pp.prevPPoint.mirroredPos IsNot Nothing Then
                    prevMirrored = True
                    If anyMirrored Then Exit For
                End If
            Next

            If prevMirrored Then
                EnableButton(But_mirrorHor, True)
                EnableButton(But_mirrorVert, True)
            Else
                EnableButton(But_mirrorHor, False)
                EnableButton(But_mirrorVert, False)
            End If

            If anyMirrored Then
                If SVG.SelectedFigure.mirrorOrient = Orientation.Horizontal Then HighlightButton(But_mirrorHor, True)
                If SVG.SelectedFigure.mirrorOrient = Orientation.Vertical Then HighlightButton(But_mirrorVert, True)
            Else
                HighlightButton(But_mirrorHor, False)
                HighlightButton(But_mirrorVert, False)
            End If
        Else
            HighlightButton(But_mirrorHor, False)
            HighlightButton(But_mirrorVert, False)
            EnableButton(But_mirrorHor, True)
            EnableButton(But_mirrorVert, True)
        End If
    End Sub
    Public Sub SVGSelectedPoints_OnRemovingRange(sender As ListWithEvents(Of PathPoint), start As Integer, count As Integer)
        For i As Integer = start To start + count - 1
            Lb_selPoints.Items.RemoveAt(i)
        Next
        Pic_canvas.Invalidate()
    End Sub
    Public Sub SVGSelectedPoints_OnRemoving(sender As ListWithEvents(Of PathPoint), d As PathPoint)
        Lb_selPoints.Items.RemoveAt(sender.IndexOf(d))
        Pic_canvas.Invalidate()
    End Sub
    Public Sub SVGSelectedPoints_OnClear(sender As ListWithEvents(Of PathPoint))
        Lb_selPoints.Items.Clear()
        Pic_canvas.Invalidate()
    End Sub

    'Custom Events
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Subroutines and Functions

    Public Sub HighlightButton(ByRef but As Button, highlight As Boolean)
        If highlight Then
            If but.Enabled = True Then but.BackColor = Color.Gold
            but.FlatAppearance.BorderSize = 1
            but.FlatAppearance.BorderColor = Color.DarkOrange
        Else
            If but.Enabled = True Then but.BackColor = Color.Snow
            but.FlatAppearance.BorderSize = 1
            but.FlatAppearance.BorderColor = Color.DimGray
        End If
        but.Parent.Focus()
    End Sub

    Public Sub UnhighlightAll()
        'Unhighlight other tools
        For Each but As Button In Pan_tools.Controls.OfType(Of Button)
            HighlightButton(but, False)
        Next
    End Sub

    Public Sub EnableButton(btn As Button, enable As Boolean)
        btn.Enabled = enable
        If enable = True Then
            btn.BackColor = Color.Snow
        Else
            btn.BackColor = btn.Parent.BackColor
        End If
    End Sub

    Public Sub SetSelectedTool(tool As Tool)
        UnhighlightAll()
        selectedTool = tool

        Select Case selectedTool
            Case Tool.Selection
                HighlightButton(But_selection, True)
            Case Tool.Drawing
                Select Case selectedType
                    Case PointType.moveto
                        HighlightButton(But_moveto, True)
                    Case PointType.lineto
                        HighlightButton(But_lineto, True)
                    Case PointType.horizontalLineto
                        HighlightButton(But_horLineto, True)
                    Case PointType.verticalLineto
                        HighlightButton(But_vertLineto, True)
                    Case PointType.curveto
                        HighlightButton(But_curveto, True)
                    Case PointType.smoothCurveto
                        HighlightButton(But_smoothCurveto, True)
                    Case PointType.quadraticBezierCurve
                        HighlightButton(But_bezier, True)
                    Case PointType.smoothQuadraticBezierCurveto
                        HighlightButton(But_smoothBezier, True)
                    Case PointType.ellipticalArc
                        HighlightButton(But_elliArc, True)
                        'Case PointType.closepath
                    Case Else
                End Select
        End Select
    End Sub

    Public Sub SetSelectedCommand(typ As PointType)
        selectedType = typ
        SetSelectedTool(Tool.Drawing)
    End Sub

    'Refresh info panel
    Private Sub UpdateStats()
        Static mpos As PointF = GetMousePlacePos(Pic_canvas)
        Dim digits As Integer = Math.Max(Math.Ceiling(SVG.CanvasSize.Width).ToString.Length, Math.Ceiling(SVG.CanvasSize.Height).ToString.Length) + 3
        mpos = GetMousePlacePos(Pic_canvas)
        Lab_mposX.Text = "Mouse X: " & FormatNumber(mpos.X, 2, TriState.True, TriState.False, TriState.False).ToString.PadLeft(digits, "0") & " (" & SVG.ApplyZoom(mpos).X & ")"
        Lab_mposY.Text = "Mouse Y: " & FormatNumber(mpos.Y, 2, TriState.True, TriState.False, TriState.False).ToString.PadLeft(digits, "0") & " (" & SVG.ApplyZoom(mpos).Y & ")"

        Static rc As RectangleF
        rc = SVG.UnZoom(SVG.GetBounds())
        Lab_sizeW.Text = "Width: " & Math.Round(rc.Width, 2)
        Lab_sizeH.Text = "Height: " & Math.Round(rc.Height, 2)

        Group_info.Invalidate()
    End Sub

    'Redraw the canva's back grid
    Public Sub RefreshBackGrid()
        Dim bm As New Bitmap(CInt(SVG.CanvasZoom * SVG.StikyGrid.Width), CInt(SVG.CanvasZoom * SVG.StikyGrid.Height))
        Dim grx As Graphics = Graphics.FromImage(bm)
        Dim br As New SolidBrush(Color.Black)
        Dim pen As New Pen(Color.FromArgb(255, 30, 30, 30), 1)

        grx.FillRectangle(br, New Rectangle(0, 0, bm.Width, bm.Height))
        grx.DrawRectangle(pen, New Rectangle(0, 0, bm.Width, bm.Height))

        Pic_canvas.BackgroundImage = bm
        'The background is in tile mode
    End Sub

    Public Sub RefreshClosestPointToMouse()
        Dim mpos As PointF = GetMousePlacePos(Pic_canvas)
        'Select closest point
        If SVG.selectedPoints.Count <= 1 Then
            SVG.SelectedPoint = SVG.SelectedPath.GetClosestPoint(mpos, False)
            Pic_canvas.Invalidate()
        End If
    End Sub

    'Subroutines and Functions
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Initializations (or something like that)

    Private Sub Form1_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.CurrentCulture = New Globalization.CultureInfo("en-GB")
        Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("en-GB")
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")

        InitHandlers()

        'Make grid background
        RefreshBackGrid()

        'Toggles
        HighlightButton(But_placeBetClosest, placeBetweenClosest)
        HighlightButton(But_showPoints, showPoints)
        HighlightButton(But_lineto, showPoints)

        'Add first figure
        SVG.Init()
        'Combo_figure.Items.Add("Figure 1")
        'Combo_figure.SelectedIndex = 0

        'Dim asy As New CAsync
        'asy.AddTask(AddressOf Test, {"test", "yep"})

        Dim m1 As New Matrix({{0, 4, -2}, {-4, -3, 0}})
        Dim m2 As New Matrix({{0, 1}, {1, -1}, {2, 3}})

        Dim m3 As Matrix = m1.Multiply(m2)

        Dim m4 As Matrix = m3.Multiply(3)

        historyLock = False
        AddToHistory()
    End Sub

    'Initializations
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Canvas Stuff

    Private Sub Pan_canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles Pan_canvas.MouseDown
        If e.Button = MouseButtons.Middle Then
            'Move the canvas
            mouseLastPos = Cursor.Position
            movingCanvas = True
            Pan_canvas.Cursor = Cursors.SizeAll
        End If
    End Sub

    Private Sub Pan_canvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles Pan_canvas.MouseWheel
        If My.Computer.Keyboard.CtrlKeyDown Then
            'Zoom in and out with mouse wheel
            If SVG.CanvasZoom >= 2 Then
                SVG.CanvasZoom += e.Delta / 120
            Else
                SVG.CanvasZoom += e.Delta / 1200
            End If
        End If
    End Sub

    Private Sub Pan_canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles Pan_canvas.MouseUp
        'Reset cursor
        Pan_canvas.Cursor = Cursors.Default
    End Sub

    Private Sub Pan_canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Pan_canvas.MouseMove
        If e.Button = MouseButtons.Middle AndAlso movingCanvas = True Then
            'Move the canvas
            Pic_canvas.Left -= mouseLastPos.X - Cursor.Position.X
            Pic_canvas.Top -= mouseLastPos.Y - Cursor.Position.Y
            mouseLastPos = Cursor.Position
        End If

        Pan_canvas.Refresh()
        'PictureBox1.Location = Cursor.Position
        'PictureBox1_Move(PictureBox1, New EventArgs)
    End Sub

    Private Sub Pic_canvas_Click(sender As Object, e As EventArgs) Handles Pic_canvas.Click
        'Dim mpos As Point = GetMousePlacePos(Pic_canvas)
        Pan_canvas.Focus()
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Pic_canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles Pic_canvas.MouseDown
        Dim mpos As CPointF = GetMousePlacePos(Pic_canvas)
        pressedMButton = e.Button

        If e.Button = MouseButtons.Left Then

            'Move closest point to mouse
            If My.Computer.Keyboard.CtrlKeyDown OrElse selectedTool = Tool.Movement Then
                If SVG.selectedPoints.Count = 1 Then SVG.selectedPoints(0).OnMoveStart(mpos)

                movingPoints = True
                mouseLastPos = mpos
            End If

            Select Case selectedTool

                Case Tool.Drawing '----------------------------------------------------------------------------------------------

                    'Move closest point to mouse
                    If My.Computer.Keyboard.CtrlKeyDown Then

                        ''If selectedPoints.Count <= 0 Then
                        ''    Dim closestIG As Integer = GetPointGlobalIndex(selectedFigure, GetClosestPoint(selectedFigure, mpos))
                        ''    selectedPoints.Clear()
                        ''    selectedPoints.Add(closestIG, pathPoints(closestIG))
                        ''End If

                        'If SVG.selectedPoints.Count = 1 Then SVG.selectedPoints(0).OnMoveStart(mpos)

                        'movingPoints = True
                        'mouseLastPos = mpos

                    ElseIf SVG.SelectedPath IsNot Nothing AndAlso SVG.SelectedFigure IsNot Nothing Then 'Add a Point

                        Static index As Integer
                        Static closestFig As Figure
                        closestFig = SVG.SelectedPath.GetClosestSelectedFigure(mpos)

                        If placeBetweenClosest = True AndAlso SVG.placementRefPPoints(1) IsNot Nothing Then
                            index = SVG.placementRefPPoints(1).GetIndex()
                            If index = 0 Then index = closestFig.Count
                        Else
                            index = closestFig.Count
                        End If

                        'If closestFig(Math.Max(0, index - 1)) IsNot Nothing AndAlso closestFig(Math.Max(0, index - 1)).pointType = PointType.closepath Then index = Math.Max(0, index - 1)

                        Dim pp As PathPoint = closestFig.InsertNewPPoint(selectedType, mpos, index)

                        SVG.SelectedPoint = pp

                        movingPoints = True
                        mouseLastPos = mpos

                    End If

                Case Tool.Selection '--------------------------------------------------------------------------------------------

                    If My.Computer.Keyboard.CtrlKeyDown Then

                        mouseLastPos = mpos
                        movingPoints = True

                    Else 'If pressedMod = Keys.None Then

                        selStart = mpos
                        selectionRect = New Rectangle(mpos.X, mpos.Y, 0, 0)

                    End If

                Case Tool.Movement '----------------------------------------------------------------------------------------------

                    If pressedKey = Keys.Space Then
                        'Move the entire SVG (all paths)

                    End If

            End Select
        ElseIf e.Button = MouseButtons.Middle Then
            ''Move the canvas
            'mouseLastPos = Cursor.Position
            'movingCanvas = True
            'Box_canvas.Cursor = Cursors.SizeAll
        End If

        Pan_canvas_MouseDown(sender, e)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Pic_canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles Pic_canvas.MouseUp
        Dim mpos As CPointF = GetMousePlacePos(Pic_canvas)

        Select Case selectedTool
            Case Tool.Drawing
            Case Tool.Selection
                If e.Button = MouseButtons.Left AndAlso movingPoints = False Then
                    'Change selection rect
                    selEnd = mpos
                    selectionRect = LineToRectangle(selStart, selEnd)
                    If My.Computer.Keyboard.ShiftKeyDown Then
                        'Add points to selection
                        SVG.SelectPointsInRect(selectionRect, False)
                    ElseIf My.Computer.Keyboard.AltKeyDown Then
                        'Deselect points
                        SVG.DeselectPointsInRect(selectionRect)
                    Else
                        'Select point, and clear previous selection
                        SVG.SelectPointsInRect(selectionRect, True)
                    End If

                End If
        End Select

        selectionRect.Size = New Size(0, 0)

        movingPoints = False
        movingObject = False
        snapToGrid = False

        Pan_canvas.Cursor = Cursors.Default

        Pan_canvas_MouseUp(sender, e)
        Pic_canvas.Invalidate()
        'Timer_canvasMMove.Enabled = False

        If pressedMButton <> MouseButtons.None Then
            AddToHistory()
        End If

        pressedMButton = MouseButtons.None
    End Sub

    Private Sub Pic_canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Pic_canvas.MouseMove
        'Small optimization
        Static lastTime As DateTime = Now
        If Now.Ticks - lastTime.Ticks < 290000 Then 'Precess every 29ms
            Exit Sub
        Else
            lastTime = Now
        End If

        Dim mpos As CPointF = GetMousePlacePos(Pic_canvas)

        If pressedMButton = MouseButtons.None AndAlso pressedKey = Keys.None AndAlso pressedMod = Keys.None Then
            If SVG.selectedPoints.Count <= 1 Then
                'Select closest point to mouse
                RefreshClosestPointToMouse()
            End If
            SVG.RefreshPlacementRefs(mpos)
        End If

        'Left Mouse Button Pressed
        If pressedMButton = MouseButtons.Left Then

            Select Case selectedTool
                Case Tool.Movement '----------------------------------------------------------------------------------------------
                    If pressedKey = Keys.Space Then
                        'Move the entire SVG (all paths)
                        SVG.Offset(mpos - mouseLastPos)
                        mouseLastPos = mpos

                        movingPoints = False
                    ElseIf My.Computer.Keyboard.ShiftKeyDown Then
                        'Move the entire Path (all figures)
                        SVG.SelectedPath.Offset(mpos - mouseLastPos)
                        mouseLastPos = mpos

                        movingPoints = False
                    End If
            End Select

            'Move the selected points
            If movingPoints = True AndAlso SVG.selectedPoints.Count > 0 Then

                For Each pp As PathPoint In SVG.selectedPoints
                    If SVG.selectedPoints.Count > 1 Then
                        pp.Offset(mpos - mouseLastPos)
                    Else
                        If My.Computer.Keyboard.ShiftKeyDown Then
                            pp.OffsetSelPoint(mpos, mpos - mouseLastPos, MoveMods.StickTo45Degs)
                            'pp.StickSelToAngles(45)
                        ElseIf My.Computer.Keyboard.AltKeyDown Then
                            pp.OffsetSelPoint(mpos, mpos - mouseLastPos, MoveMods.StickToGrid)
                            'pp.StickSelToGrid()
                        Else
                            pp.OffsetSelPoint(mpos, mpos - mouseLastPos, 0)
                        End If
                    End If

                    'Change ellipticalArc's sweep value
                    'If pp.pointType = PointType.ellipticalArc Then
                    '    If My.Computer.Keyboard.ShiftKeyDown Then
                    '        CType(pp, PPEllipticalArc).sweep = False
                    '    Else
                    '        CType(pp, PPEllipticalArc).sweep = True
                    '    End If
                    'End If

                    'Update listboxes' values
                    'pp.RefreshLBsValue(Lb_figurePoints, Lb_points)
                Next

                mouseLastPos = mpos

                'ElseIf movingObject
                '    'Move the entire path
                '    OffsetPath(mouseLastPos - mpos)
                '    mouseLastPos = mpos
                '   

            ElseIf selectedTool = Tool.Selection Then
                ''Change selection rect
                selEnd = mpos
                selectionRect = LineToRectangle(selStart, selEnd) 'New Rectangle(Math.Min(selStart.X, selEnd.X), Math.Min(selStart.Y, selEnd.Y), Math.Abs(selStart.X - selEnd.X), Math.Abs(selStart.Y - selEnd.Y))
                'SVG.SelectPointsInRect(selectionRect)
            End If

        ElseIf pressedMButton = MouseButtons.Middle Then
            'If movingCanvas = True Then
            '    'Move the canvas
            '    Pic_canvas.Left -= mouseLastPos.X - Cursor.Position.X
            '    Pic_canvas.Top -= mouseLastPos.Y - Cursor.Position.Y
            '    mouseLastPos = Cursor.Position
            'End If
        End If

        Pic_canvas.Refresh()
        Pan_canvas_MouseMove(sender, e)
    End Sub

    Private Sub Pic_canvas_Paint(sender As Object, e As PaintEventArgs) Handles Pic_canvas.Paint
        Static penAxis As New Pen(Color.FromArgb(150, Color.Chocolate), 1)
        Static penCentralAxis As New Pen(Color.FromArgb(100, Color.White), 1)

        'Set drawing to be smooth
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        'Draw 32x32 cells reference Grid --------------------------------------------------------------------------------------
        penAxis.DashPattern = {5, 5}
        For ix As Integer = gridZoomed.Width To SVG.CanvasSizeZoomed.Width - 1 Step gridZoomed.Width
            e.Graphics.DrawLine(penAxis, ix, 0, ix, SVG.CanvasSizeZoomed.Height)
        Next
        For iy As Integer = gridZoomed.Height To SVG.CanvasSizeZoomed.Height - 1 Step gridZoomed.Height
            e.Graphics.DrawLine(penAxis, 0, iy, SVG.CanvasSizeZoomed.Width, iy)
        Next

        penCentralAxis.DashPattern = {5, 5}
        e.Graphics.DrawLine(penCentralAxis, 0, SVG.CanvasSizeZoomed.Height / 2.0F, SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height / 2.0F)
        e.Graphics.DrawLine(penCentralAxis, SVG.CanvasSizeZoomed.Width / 2.0F, 0, SVG.CanvasSizeZoomed.Width / 2.0F, SVG.CanvasSizeZoomed.Height)

        'Draw SVG -------------------------------------------------------------------------------------------------------------
        SVG.CanvasImg = New Bitmap(SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height)
        Dim grxCanvas As Graphics = Graphics.FromImage(SVG.CanvasImg)

        For Each path As SVGPath In SVG.paths
            path.Draw(grxCanvas)
        Next

        e.Graphics.DrawImage(SVG.CanvasImg, New Point(0, 0))

        If Pic_preview.Image IsNot Nothing Then Pic_preview.Image.Dispose()
        Pic_preview.Image = SVG.CanvasImg

        If Form_result.Visible Then
            If Form_result.Pic_realSize.Image IsNot Nothing Then Form_result.Pic_realSize.Image.Dispose()
            Form_result.Pic_realSize.Image = SVG.CanvasImg
        End If

        'DRAW POINTS IN HERE >>>>>>>>>

        Static penPointsRefIn As New Pen(Color.Orange, 1)
        Static penPointsSelIn As New Pen(Color.Lime, 1)
        Static penPointsClosestIn As New Pen(Color.LightSalmon, 1)
        Static penPointsMovetoIn As New Pen(Color.Violet, 1)
        Static penPointsIn As New Pen(Color.DeepSkyBlue, 1)
        Static penPointsOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)
        Static brushRefIn As New SolidBrush(Color.Orange)
        Static brushRefOut As New SolidBrush(Color.FromArgb(255, 40, 40, 40))

        'Reference points for placing next point-------------------------------------------------------------------------------
        'Dim mpos As CPointF = GetMousePlacePos(Pic_canvas)

        'Dim closestToMouseI As Integer = GetPointGlobalIndex(selectedFigures(0), GetClosestPoint(selectedFigures(0), mpos))

        'Draw the Points ------------------------------------------------------------------------------------------------------

        If showPoints Then
            'Draw secondary points
            If SVG.selectedPoints.Count > 0 Then
                For Each pp As PathPoint In SVG.selectedPoints
                    If pp Is Nothing Then Continue For
                    pp.DrawSecPoints(e.Graphics)
                Next
            End If

            'Draw pos points
            For Each fig As Figure In SVG.GetSelectedFigures()

                Static penMirror As New Pen(Color.Purple, 1)
                penMirror.DashPattern = {3, 10}

                For Each pp As PathPoint In fig
                    If pp.pos Is Nothing OrElse pp.nonInteractve = True Then Continue For

                    penMirror.Color = ColorRotate(fig.parent.FillColor)
                    If pp.mirroredPos IsNot Nothing AndAlso pp.isMirrorOrigin Then
                        e.Graphics.DrawLine(penMirror, SVG.ApplyZoom(pp.pos), SVG.ApplyZoom(pp.mirroredPos.pos))
                        If pp.mirroredPP IsNot Nothing AndAlso pp.mirroredPos IsNot pp.mirroredPP Then
                            e.Graphics.DrawLine(penMirror, SVG.ApplyZoom(pp.pos), SVG.ApplyZoom(pp.mirroredPP.pos))
                        End If
                    End If

                    Dim rc As New RectangleF(New PointF(pp.pos.X * SVG.CanvasZoom - 6, pp.pos.Y * SVG.CanvasZoom - 6), New SizeF(12, 12))

                    'Refs for placing next ppoint
                    If pp.Equals(SVG.placementRefPPoints(0)) OrElse pp.Equals(SVG.placementRefPPoints(1)) Then
                        e.Graphics.FillEllipse(brushRefOut, rc)
                        e.Graphics.FillEllipse(brushRefIn, rc)
                    End If

                    If SVG.selectedPoints.Contains(pp) Then
                        'Selected ppoints
                        rc.Inflate(New Size(1, 1))
                        e.Graphics.DrawEllipse(penPointsOut, rc)
                        e.Graphics.DrawEllipse(penPointsSelIn, rc)
                    ElseIf pp.pointType = PointType.moveto Then
                        'Movetos
                        e.Graphics.DrawEllipse(penPointsOut, rc)
                        e.Graphics.DrawEllipse(penPointsMovetoIn, rc)
                    Else
                        'Everything else
                        e.Graphics.DrawEllipse(penPointsOut, rc)
                        e.Graphics.DrawEllipse(penPointsIn, rc)
                    End If
                Next
            Next

        End If


        '<<<<<<<<<<<<<<<<<<<<<<<<<


        'Draw moving point's axis ---------------------------------------------------------------------------------------------
        penAxis.DashPattern = {16, 16}

        If movingPoints = True AndAlso SVG.selectedPoints.Count = 1 AndAlso SVG.SelectedPoint.selPoint IsNot Nothing Then
            e.Graphics.DrawLine(penAxis, New PointF(SVG.ApplyZoom(SVG.SelectedPoint.selPoint).X, 0), New PointF(SVG.ApplyZoom(SVG.SelectedPoint.selPoint).X, SVG.CanvasSizeZoomed.Height))
            e.Graphics.DrawLine(penAxis, New PointF(0, SVG.ApplyZoom(SVG.SelectedPoint.selPoint).Y), New PointF(SVG.CanvasSizeZoomed.Width, SVG.ApplyZoom(SVG.SelectedPoint.selPoint).Y))
        End If

        'Draw selection -------------------------------------------------------------------------------------------------------
        penAxis.DashPattern = {3, 3}
        If selectedTool = Tool.Selection Then
            e.Graphics.DrawRectangle(penAxis, SVG.ApplyZoom(selectionRect).ToRectangle)
        End If

        ' ---------------------------------------------------------------------------------------------------------------------
        'TESTS


        '' Create a GraphicsPath object.
        'Dim myPath As New Drawing2D.GraphicsPath

        '' Set up and call AddArc, and close the figure.
        'Dim rect As New Rectangle(Num1_1.Value, Num1_2.Value, Num3_1.Value, Num3_2.Value)
        'myPath.StartFigure()
        'myPath.AddLine(New PointF(300, 200), New PointF(150, 200))
        'myPath.AddArc(rect, Num4_1.Value, Num4_2.Value)
        'myPath.CloseFigure()

        '' Draw the path to screen.
        'e.Graphics.DrawPath(New Pen(Color.Red, 3), myPath)

        ' ---------------------------------------------------------------------------------------------------------------------

        UpdateStats()
        If Not Tb_html.Focused Then Tb_html.Text = SVG.GetHtml(optimizePath)

        'Clean
        grxCanvas.Dispose()
    End Sub

    'Canvas Stuff
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Commands Stuff

    Private Sub But_placeBetClosest_Click(sender As Object, e As EventArgs) Handles But_placeBetClosest.Click
        placeBetweenClosest = Not placeBetweenClosest
        HighlightButton(But_placeBetClosest, placeBetweenClosest)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub But_showPoints_Click(sender As Object, e As EventArgs) Handles But_showPoints.Click
        showPoints = Not showPoints
        HighlightButton(But_showPoints, showPoints)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub But_mirrorHor_Click(sender As Object, e As EventArgs) Handles But_mirrorHor.Click
        If SVG.SelectedPath Is Nothing OrElse SVG.SelectedPath.selectedFigures.Count > 1 Then Return
        If SVG.SelectedFigure.mirrorOrient = Orientation.Vertical Then Return

        historyLock = True

        Dim lastPP As PathPoint = SVG.GetSelectedPPLastInPath()
        SVG.SortSelectedPoints()

        If SVG.selectedPoints.Count > 0 Then
            Dim ph As PathPoint = Nothing
            If SVG.SelectedFigure.numMirrored <= 0 Then
                ph = SVG.SelectedFigure.AddNewPPoint(PointType.lineto, lastPP.Pos) '.SetMirrorPos(lastPP, Orientation.Horizontal)

                For Each pp As PathPoint In SVG.selectedPoints.AsEnumerable.Reverse
                    pp.Mirror(Orientation.Horizontal)
                Next
            Else
                For Each pp As PathPoint In SVG.selectedPoints
                    pp.Mirror(Orientation.Horizontal)
                Next
            End If

            If ph IsNot Nothing Then lastPP.SetMirrorPos(ph, Orientation.Horizontal)
            Pic_canvas.Refresh()
        End If

        historyLock = False
        AddToHistory()
    End Sub

    Private Sub But_mirrorVert_Click(sender As Object, e As EventArgs) Handles But_mirrorVert.Click
        If SVG.SelectedPath Is Nothing OrElse SVG.SelectedPath.selectedFigures.Count > 1 Then Return
        If SVG.SelectedFigure.mirrorOrient = Orientation.Horizontal Then Return

        historyLock = True

        Dim lastPP As PathPoint = SVG.GetSelectedPPLastInPath()
        SVG.SortSelectedPoints()

        If SVG.selectedPoints.Count > 0 Then
            Dim ph As PathPoint = Nothing
            If SVG.SelectedFigure.numMirrored <= 0 Then
                ph = SVG.SelectedFigure.AddNewPPoint(PointType.lineto, lastPP.Pos)

                For Each pp As PathPoint In SVG.selectedPoints.AsEnumerable.Reverse
                    pp.Mirror(Orientation.Vertical)
                Next
            Else
                For Each pp As PathPoint In SVG.selectedPoints
                    pp.Mirror(Orientation.Vertical)
                Next
            End If

            If ph IsNot Nothing Then lastPP.SetMirrorPos(ph, Orientation.Vertical)
            Pic_canvas.Refresh()
        End If

        historyLock = False
        AddToHistory()
    End Sub

    Private Sub But_selection_Click(sender As Object, e As EventArgs) Handles But_selection.Click
        UnhighlightAll()
        selectedTool = Tool.Selection
        HighlightButton(But_selection, True)
    End Sub

    Private Sub But_movement_Click(sender As Object, e As EventArgs) Handles But_movement.Click
        UnhighlightAll()
        selectedTool = Tool.Movement
        HighlightButton(But_movement, True)
    End Sub

    Private Sub But_moveto_Click(sender As Object, e As EventArgs) Handles But_moveto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.moveto
        HighlightButton(But_moveto, True)
    End Sub

    Private Sub But_lineto_Click(sender As Object, e As EventArgs) Handles But_lineto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.lineto
        HighlightButton(But_lineto, True)
    End Sub

    Private Sub But_horLineto_Click(sender As Object, e As EventArgs) Handles But_horLineto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.horizontalLineto
        HighlightButton(But_horLineto, True)
    End Sub

    Private Sub But_vertLineto_Click(sender As Object, e As EventArgs) Handles But_vertLineto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.verticalLineto
        HighlightButton(But_vertLineto, True)
    End Sub

    Private Sub But_curveto_Click(sender As Object, e As EventArgs) Handles But_curveto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.curveto
        HighlightButton(But_curveto, True)
    End Sub

    Private Sub But_smoothCurveto_Click(sender As Object, e As EventArgs) Handles But_smoothCurveto.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.smoothCurveto
        HighlightButton(But_smoothCurveto, True)
    End Sub

    Private Sub But_bezier_Click(sender As Object, e As EventArgs) Handles But_bezier.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.quadraticBezierCurve
        HighlightButton(But_bezier, True)
    End Sub

    Private Sub But_smoothBezier_Click(sender As Object, e As EventArgs) Handles But_smoothBezier.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.smoothQuadraticBezierCurveto
        HighlightButton(But_smoothBezier, True)
    End Sub

    Private Sub But_elliArc_Click(sender As Object, e As EventArgs) Handles But_elliArc.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing
        selectedType = PointType.ellipticalArc
        HighlightButton(But_elliArc, True)
    End Sub

    Private Sub But_closePath_Click(sender As Object, e As EventArgs) Handles But_closePath.Click, CreateFigureToolStripMenuItem.Click, But_addFigure.Click
        UnhighlightAll()
        selectedTool = Tool.Drawing

        selectedType = PointType.moveto
        HighlightButton(But_moveto, True)

        SVG.SelectedPath.AddNewFigure()
        EnableButton(But_moveto, True)

        Pic_canvas.Invalidate()
    End Sub

    'Commands Stuff
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim mpos As Point = Pic_canvas.PointToClient(Cursor.Position)
        'If mouse over canvas
        If Pic_canvas.ClientRectangle.Contains(mpos) Then

            If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
                'Select all points in the selected path
                For Each fig As Figure In SVG.GetSelectedFigures()
                    fig.SelectAllPPoints(True)
                Next
                Pic_canvas.Invalidate()

            ElseIf e.KeyCode = Keys.D AndAlso e.Control Then
                'Clear selection
                SVG.selectedPoints.Clear()
                Pic_canvas.Invalidate()

            ElseIf e.KeyCode = Keys.Delete AndAlso e.Modifiers = Keys.None Then
                'Delete the selected points
                For Each pp As PathPoint In SVG.selectedPoints.Reverse
                    If pp.pointType = PointType.moveto Then Continue For
                    pp.Delete()
                Next
                Pic_canvas.Invalidate()

            ElseIf e.KeyCode = Keys.M Then
                'Mirror PPoint
                If SVG.selectedPoints.Count > 0 Then
                    For Each pp As PathPoint In SVG.selectedPoints.AsEnumerable.Reverse
                        pp.Mirror(Orientation.Vertical)
                    Next
                End If
            ElseIf e.KeyCode = Keys.Z AndAlso (e.Modifiers = (Keys.Shift Or Keys.Control)) Then
                'Redo
                Redo()
            ElseIf e.KeyCode = Keys.Z AndAlso e.Modifiers = Keys.Control Then
                'Undo
                Undo()
            End If

        End If
        pressedKey = e.KeyCode
        pressedMod = e.Modifiers
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        pressedKey = Keys.None
        pressedMod = Keys.None
        Dim mpos As Point = Pic_canvas.PointToClient(Cursor.Position)
        'If mouse over canvas
        If Pic_canvas.ClientRectangle.Contains(mpos) Then
            RefreshClosestPointToMouse()
        End If
    End Sub

    Private Sub MoveTo00ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveTo00ToolStripMenuItem.Click
        Dim minPos As New PointF(99999, 99999)

        'Get smallest x and y
        For Each pp As PathPoint In SVG.GetAllPPoints()
            If pp.GetBounds.Left < minPos.X Then minPos.X = pp.GetBounds.Left
            If pp.GetBounds.Top < minPos.Y Then minPos.Y = pp.GetBounds.Top
        Next

        minPos.X *= -1
        minPos.Y *= -1

        'Offset all points
        For Each pp As PathPoint In SVG.GetAllPPoints()
            pp.Offset(minPos)
        Next

        Pic_canvas.Invalidate()
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        SVG.selectedPoints.Clear()

        For Each path As SVGPath In SVG.selectedPaths
            path.Clear()
        Next

        Pic_canvas.Invalidate()
    End Sub

    Private Sub RoundPositionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoundPositionsToolStripMenuItem.Click
        For Each pp As PathPoint In SVG.GetAllPPoints
            pp.RoundPosition()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num1_ValueChanged(sender As Object, e As EventArgs)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click, But_removePath.Click
        For Each path As SVGPath In SVG.selectedPaths.AsEnumerable.Reverse
            SVG.RemovePath(path)
        Next
    End Sub

    Private Sub ClearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem1.Click
        SVG.SelectedFigure.Clear()
        Pic_canvas.Invalidate()
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click, But_removeFigure.Click
        'Lb_figurePoints.Items.Clear()
        'Combo_figure.Items.RemoveAt(Combo_figure.SelectedIndex)
        If SVG.SelectedPath Is Nothing Then Return
        For Each fig As Figure In SVG.SelectedPath.selectedFigures.AsEnumerable.Reverse
            SVG.SelectedPath.Remove(fig)
        Next

        Pic_canvas.Invalidate()
    End Sub

    Private Sub Tb_html_KeyDown(sender As Object, e As KeyEventArgs) Handles Tb_html.KeyDown
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.A Then
            Tb_html.SelectAll()
        End If
    End Sub

    Private Sub AddLotsOPointsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddLotsOPointsToolStripMenuItem.Click
        Dim num As Integer = 0
        For ix As Integer = 0 To SVG.CanvasSizeZoomed.Width - 1 Step 50
            For iy As Integer = 0 To SVG.CanvasSizeZoomed.Height - 1 Step 50
                SVG.SelectedFigure.InsertNewPPoint(PointType.lineto, SVG.UnZoom(New PointF(ix, iy)), -1)
                num += 1
                If num > 100 Then Return
            Next
        Next
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click, But_addPath.Click
        SVG.AddPath()
    End Sub

    Private Sub Col_stroke_Click(sender As Object, e As EventArgs) Handles Col_stroke.Click
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            Col_stroke.BackColor = ColorDialog1.Color
            SVG.SelectedPath.StrokeColor = ColorDialog1.Color
            Pic_canvas.Refresh()
        End If
    End Sub

    Private Sub Col_fill_Click(sender As Object, e As EventArgs) Handles Col_fill.Click
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            Col_fill.BackColor = ColorDialog1.Color
            SVG.SelectedPath.FillColor = ColorDialog1.Color
            Pic_canvas.Refresh()
        End If
    End Sub

    Private Sub Pic_canvas_MouseLeave(sender As Object, e As EventArgs) Handles Pic_canvas.MouseLeave
        'Timer_canvasMMove.Enabled = False
    End Sub

    Private Sub Pic_canvas_MouseEnter(sender As Object, e As EventArgs) Handles Pic_canvas.MouseEnter
        'Timer_canvasMMove.Enabled = True
    End Sub

    Private Sub Num_canvasWidth_ValueChanged(sender As Object, e As EventArgs) Handles Num_canvasWidth.ValueChanged
        SVG.CanvasSize = New SizeF(Num_canvasWidth.Value, SVG.CanvasSize.Height)
    End Sub

    Private Sub Num_canvasHeight_ValueChanged(sender As Object, e As EventArgs) Handles Num_canvasHeight.ValueChanged
        SVG.CanvasSize = New SizeF(SVG.CanvasSize.Width, Num_canvasHeight.Value)
    End Sub

    Private Sub Num_zoom_ValueChanged(sender As Object, e As EventArgs) Handles Num_zoom.ValueChanged
        SVG.CanvasZoom = Num_zoom.Value
    End Sub

    Private Sub ScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScaleToolStripMenuItem.Click
        Form_scale.Show()
    End Sub

    Private Sub MoveTo00ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MoveTo00ToolStripMenuItem1.Click
        Dim bounds As RectangleF = SVG.GetBounds()
        SVG.Offset(bounds.X * -1, bounds.Y * -1)
        Pic_canvas.Refresh()
    End Sub

    Private Sub ClearToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem2.Click
        SVG.Clear()
        Pic_canvas.Refresh()
    End Sub

    Private Sub CropToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CropToolStripMenuItem.Click
        Dim margin As Single = 0
        Dim response As String = InputBox("How much margin to leave around?" & vbCrLf & "Use decimal separator """ & Application.CurrentCulture.NumberFormat.CurrencyDecimalSeparator & """", "Cropping margin", "0")
        If response.Length <= 0 Then Exit Sub 'If canceled or no input, then exit the sub
        If IsNumeric(response) Then margin = response

        Dim bounds As RectangleF = SVG.GetBounds()
        SVG.Offset((bounds.X - margin) * -1, (bounds.Y - margin) * -1)
        SVG.CanvasSize = New SizeF(bounds.Width + margin * 2, bounds.Height + margin * 2)
        Pic_canvas.Refresh()
    End Sub

    Private Sub Lb_figures_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_figures.SelectedIndexChanged
        'Change selected figure
        If SVG.SelectedPath IsNot Nothing Then
            'Select the last figure if nothing is selected
            If Lb_figures.SelectedIndices.Count <= 0 Then
                Lb_figures.SelectedIndex = Lb_figures.Items.Count - 1
            End If

            For Each i As Integer In Lb_figures.SelectedIndices
                Dim fig As Figure = SVG.SelectedPath.Figure(i)
                If Not SVG.SelectedPath.selectedFigures.Contains(fig) Then
                    SVG.SelectedPath.selectedFigures.Add(fig)
                End If
            Next

            For Each fig As Figure In SVG.SelectedPath.selectedFigures.Reverse
                If Not Lb_figures.SelectedIndices.Contains(fig.GetIndex()) Then
                    SVG.SelectedPath.selectedFigures.Remove(fig)
                End If
            Next

            Pic_canvas.Refresh()
        End If
    End Sub

    Private Sub ViewHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHelpToolStripMenuItem.Click
        Form_help.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Form_about.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub ExportAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAsToolStripMenuItem.Click
        'Save the bitmap in the specified format
        SaveFileDialog1.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp|TIFF|*.tiff|ICON|*.ico"
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim oldZoom As Single = SVG.CanvasZoom
            SVG.CanvasZoom = 1
            Select Case IO.Path.GetExtension(SaveFileDialog1.FileName).ToLower
                Case ".jpg"
                    SVG.CanvasImg.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
                Case ".bmp"
                    SVG.CanvasImg.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Bmp)
                Case ".tiff"
                    SVG.CanvasImg.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Tiff)
                Case ".ico"
                    Dim ico As Drawing.Icon = Drawing.Icon.FromHandle(SVG.CanvasImg.GetHicon)
                    Dim oFileStream As New IO.FileStream(SaveFileDialog1.FileName, IO.FileMode.CreateNew)
                    ico.Save(oFileStream)
                    oFileStream.Close()
                Case Else
                    SVG.CanvasImg.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
            End Select
            SVG.CanvasZoom = oldZoom
        End If
    End Sub

    Private Sub ShowRealSizePreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowRealSizePreviewToolStripMenuItem.Click
        Form_result.Show()
    End Sub

    Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Form_result.TopMost = False
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Form_result.onTop Then
            SetWindowPos(Form_result.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOACTIVATE Or SWP_NOMOVE Or SWP_NOSIZE)
        Else
            SetWindowPos(Form_result.Handle, HWND_TOP, 0, 0, 0, 0, SWP_NOACTIVATE Or SWP_NOMOVE Or SWP_NOSIZE)
            Me.BringToFront()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SVG.ParseString(Tb_html.Text.ToString)
        Focus()
        Tb_html.Focus()
    End Sub

    Private Sub Pan_canvas_Paint(sender As Object, e As PaintEventArgs) Handles Pan_canvas.Paint

        'Draw sub cursor
        'Dim mpos As PointF = Cursor.Position
        'mpos = Pan_canvas.PointToClient(mpos.ToPoint)
        'e.Graphics.DrawImage(subCursor, mpos.X + 12, mpos.Y + 12, 12, 12)

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFileDialog1.Filter = "WeSP SVG|*.wsvg"

        If filePath = defFilePath Then
            SaveFileDialog1.FileName = IO.Path.GetFileName(filePath)
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                filePath = SaveFileDialog1.FileName
                Me.Text = IO.Path.GetFileNameWithoutExtension(filePath) & " - WeSP Editor"
            Else
                Return
            End If
        End If

        IO.File.WriteAllText(filePath, SVG.GetHtml(optimizePath))
        modsSinceLastSave = 0
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.Filter = "WSVG|*.wsvg"
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            filePath = SaveFileDialog1.FileName
            Me.Text = IO.Path.GetFileNameWithoutExtension(filePath) & " - WeSP Editor"
            IO.File.WriteAllText(filePath, SVG.GetHtml(optimizePath))
            modsSinceLastSave = 0
        End If
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        OpenFileDialog1.Filter = "WSVG|*.wsvg"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            filePath = OpenFileDialog1.FileName
            Me.Text = IO.Path.GetFileNameWithoutExtension(filePath) & " - WeSP Editor"
            SVG.ParseString(IO.File.ReadAllText(filePath))
        End If
    End Sub

    Private Sub Pic_preview_Resize(sender As Object, e As EventArgs) Handles Pic_preview.Resize
        If Pic_preview.Width > 84 Or Pic_preview.Height > 84 Then
            Pic_preview.SizeMode = PictureBoxSizeMode.Zoom
            Pic_preview.Size = New Size(84, 84)
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        'Select all points in the selected figure's
        For Each fig As Figure In SVG.GetSelectedFigures()
            fig.SelectAllPPoints(True)
        Next

        Pic_canvas.Invalidate()
    End Sub

    Private Sub DeleteToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        'Delete the selected points
        For Each pp As PathPoint In SVG.selectedPoints.Reverse
            If pp.pointType = PointType.moveto Then Continue For
            pp.Delete()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num_gridWidth_ValueChanged(sender As Object, e As EventArgs) Handles Num_gridWidth.ValueChanged
        grid = New SizeF(Num_gridWidth.Value, grid.Height)
        gridZoomed = New SizeF(grid.Width * SVG.CanvasZoom, grid.Height * SVG.CanvasZoom)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num_gridHeight_ValueChanged(sender As Object, e As EventArgs) Handles Num_gridHeight.ValueChanged
        grid = New SizeF(grid.Width, Num_gridHeight.Value)
        gridZoomed = New SizeF(grid.Width * SVG.CanvasZoom, grid.Height * SVG.CanvasZoom)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        Redo()
    End Sub

    Private Sub Num_strokeWidth_ValueChanged(sender As Object, e As EventArgs) Handles Num_strokeWidth.ValueChanged
        If SVG.SelectedPath Is Nothing Then Return
        SVG.SelectedPath.StrokeWidth = Num_strokeWidth.Value
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num_stikyGWidth_ValueChanged(sender As Object, e As EventArgs) Handles Num_stikyGWidth.ValueChanged
        SVG.StikyGrid = New SizeF(Num_stikyGWidth.Value, SVG.StikyGrid.Height)
    End Sub

    Private Sub Num_stickyGHeight_ValueChanged(sender As Object, e As EventArgs) Handles Num_stickyGHeight.ValueChanged
        SVG.StikyGrid = New SizeF(SVG.StikyGrid.Width, Num_stickyGHeight.Value)
    End Sub

    Private Sub DuplicateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DuplicateToolStripMenuItem.Click
        If SVG.SelectedPath Is Nothing Then Return
        For Each fig As Figure In SVG.GetSelectedFigures.Reverse
            SVG.SelectedPath.DuplicateFigure(fig)
        Next
    End Sub

    Private Sub FlipHorizontallyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FlipHorizontallyToolStripMenuItem.Click
        If SVG.SelectedPath Is Nothing Then Return
        For Each fig As Figure In SVG.GetSelectedFigures.Reverse
            fig.FlipHorizontally()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub FlipVerticallyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FlipVerticallyToolStripMenuItem.Click
        If SVG.SelectedPath Is Nothing Then Return
        For Each fig As Figure In SVG.GetSelectedFigures.Reverse
            fig.FlipVertically()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub DeselectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeselectToolStripMenuItem.Click
        Dim tmpSel As New List(Of Integer)
        For Each item In Lb_selPoints.SelectedIndices
            tmpSel.Add(item)
        Next

        tmpSel.Reverse()
        For Each item In tmpSel
            SVG.selectedPoints.RemoveAt(item)
        Next
    End Sub

    Private Sub DelteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelteToolStripMenuItem.Click
        Dim tmpSel As New List(Of Integer)
        For Each item In Lb_selPoints.SelectedIndices
            tmpSel.Add(item)
        Next

        tmpSel.Reverse()
        For Each item In tmpSel
            SVG.SelectedFigure.Remove(SVG.selectedPoints(item))
            'SVG.selectedPoints.RemoveAt(item)
        Next
    End Sub

    Private Sub But_mirror_Click(sender As Object, e As EventArgs) Handles But_mirror.Click
        SVG.SelectedPoint.Clone(1, Nothing)
    End Sub

    Private Sub Cb_optimize_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_optimize.CheckedChanged
        optimizePath = Cb_optimize.Checked
        Pic_canvas.Refresh()
    End Sub

    Private Sub Num_decimals_ValueChanged(sender As Object, e As EventArgs) Handles Num_decimals.ValueChanged
        decimalPlaces = Num_decimals.Value
        Pic_canvas.Invalidate()
    End Sub

    Private Sub But_figMoveUp_Click(sender As Object, e As EventArgs) Handles But_figMoveUp.Click
        Dim selIndxes As List(Of Integer) = Lb_figures.SelectedIndices.ToList
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            SVG.SelectedPath.ChangeFigureIndex(indx, indx - 1)
        Next
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            Lb_figures.SelectedIndex = indx - 1
        Next
    End Sub

    Private Sub But_figMoveDown_Click(sender As Object, e As EventArgs) Handles But_figMoveDown.Click
        Dim selIndxes As List(Of Integer) = Lb_figures.SelectedIndices.ToList
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_figures.Items.Count - 1 Then Continue For
            SVG.SelectedPath.ChangeFigureIndex(indx, indx + 1)
        Next
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_figures.Items.Count - 1 Then Continue For
            Lb_figures.SelectedIndex = indx + 1
        Next
    End Sub

    Private Sub But_figMoveTop_Click(sender As Object, e As EventArgs) Handles But_figMoveTop.Click
        Dim selIndxes As List(Of Integer) = Lb_figures.SelectedIndices.ToList
        Dim indxShift As Integer = selIndxes(0)
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            SVG.SelectedPath.ChangeFigureIndex(indx, indx - indxShift)
        Next
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            Lb_figures.SelectedIndex = indx - indxShift
        Next
    End Sub

    Private Sub But_figMoveBottom_Click(sender As Object, e As EventArgs) Handles But_figMoveBottom.Click
        Dim selIndxes As List(Of Integer) = Lb_figures.SelectedIndices.ToList
        Dim indxShift As Integer = (Lb_figures.Items.Count - 1) - selIndxes.Last
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + indxShift < 0 Then Continue For
            SVG.SelectedPath.ChangeFigureIndex(indx, indx + indxShift)
        Next
        For Each indx As Integer In selIndxes
            If indx + indxShift > Lb_figures.Items.Count - 1 Then Continue For
            Lb_figures.SelectedIndex = indx + indxShift
        Next
    End Sub

    Private Sub Lb_paths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_paths.SelectedIndexChanged
        If Lb_paths.SelectedIndex >= 0 AndAlso Lb_paths.SelectedIndex <> SVG.SelectedPath.GetIndex Then
            SVG.SelectedPath = SVG.paths(Lb_paths.SelectedIndex)
            Return
        End If

        'Select the last path if nothing is selected
        If Lb_paths.SelectedIndex < 0 AndAlso Lb_paths.Items.Count > 0 Then
            SVG.SelectedPath = SVG.paths(Lb_paths.Items.Count - 1)
        End If
    End Sub

    Private Sub But_pathMoveUp_Click(sender As Object, e As EventArgs) Handles But_pathMoveUp.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx - 1)
        Next
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            Lb_paths.SelectedIndex = indx - 1
        Next
    End Sub

    Private Sub But_pathMoveDown_Click(sender As Object, e As EventArgs) Handles But_pathMoveDown.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_paths.Items.Count - 1 Then Continue For
            SVG.ChangePathIndex(indx, indx + 1)
        Next
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_paths.Items.Count - 1 Then Continue For
            Lb_paths.SelectedIndex = indx + 1
        Next
    End Sub

    Private Sub But_pathMoveTop_Click(sender As Object, e As EventArgs) Handles But_pathMoveTop.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        Dim indxShift As Integer = selIndxes(0)
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx - indxShift)
        Next
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            Lb_paths.SelectedIndex = indx - indxShift
        Next
    End Sub

    Private Sub But_pathMoveBottom_Click(sender As Object, e As EventArgs) Handles But_pathMoveBottom.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        Dim indxShift As Integer = (Lb_paths.Items.Count - 1) - selIndxes.Last
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + indxShift < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx + indxShift)
        Next
        For Each indx As Integer In selIndxes
            If indx + indxShift > Lb_paths.Items.Count - 1 Then Continue For
            Lb_paths.SelectedIndex = indx + indxShift
        Next
    End Sub

    Private Sub But_pathRename_Click(sender As Object, e As EventArgs) Handles But_pathRename.Click
        If SVG.SelectedPath Is Nothing Then Return
        Dim newId = InputBox("Insert new path Id", "New Id", SVG.SelectedPath.Id)
        If newId.Length > 0 Then SVG.SelectedPath.Id = newId
    End Sub

    Private Sub Timer_autoBackup_Tick(sender As Object, e As EventArgs) Handles Timer_autoBackup.Tick
        If modsSinceLastBkp > 0 Then
            IO.File.WriteAllText("backup.wsvg", SVG.GetHtml(optimizePath))
            Lab_lastBkp.Text = "Last Bkp: " & Now.ToShortTimeString
            modsSinceLastBkp = 0
        End If
    End Sub

    Private Sub LoadBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadBackupToolStripMenuItem.Click
        If IO.File.Exists("backup.wsvg") Then
            SVG.ParseString(IO.File.ReadAllText("backup.wsvg"))
        End If
    End Sub

    Private Sub Form_main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If e.CloseReason = CloseReason.WindowsShutDown Then Return

        If e.CloseReason = CloseReason.UserClosing AndAlso modsSinceLastSave > 0 Then
            Dim answer As MsgBoxResult = MsgBox("Save changes before exiting?", MsgBoxStyle.YesNoCancel)
            If answer = MsgBoxResult.Yes Then
                SaveFileDialog1.Filter = "WeSP SVG|*.wsvg"
                If filePath = defFilePath Then
                    SaveFileDialog1.FileName = IO.Path.GetFileName(filePath)
                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                        filePath = SaveFileDialog1.FileName
                        Me.Text = IO.Path.GetFileNameWithoutExtension(filePath) & " - WeSP Editor"
                    Else
                        Return
                    End If
                End If

                IO.File.WriteAllText(filePath, SVG.GetHtml(optimizePath))
            ElseIf answer = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub
End Class
