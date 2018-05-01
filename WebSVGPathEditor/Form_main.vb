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

Imports System.Text.RegularExpressions

Public Class Form_main

    Private Enum ClipOp
        None = -1
        Cut = 0
        Copy = 1
    End Enum

    Private Enum LBLockMode
        None = 0
        User = 1
        SVG = 2
    End Enum


    Private canvasImg As Bitmap
    Private canvasBack As Bitmap
    Private refreshHtml As Boolean = True
    Private showGrid As Boolean = True
    Private showBigGrid As Boolean = True

    Private myClipboard As New List(Of Object)
    Private myClipboarOp As ClipOp = ClipOp.None

    Private initializing As Boolean = True

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Custom Events

    Public Sub InitHandlers()
        AddHandler SVG.OnCanvasSizeChanged, AddressOf SVG_OnCanvasSizeChanged
        AddHandler SVG.OnCanvasZoomChanged, AddressOf SVG_OnCanvasZoomChanged
        AddHandler SVG.OnPathAdded, AddressOf SVG_OnPathAdded
        AddHandler SVG.OnPathRemoving, AddressOf SVG_OnPathRemoving
        AddHandler SVG.OnPathClear, AddressOf SVG_OnPathClear
        AddHandler SVG.OnSelectionAddPath, AddressOf SVG_OnSelectionAddPath
        AddHandler SVG.OnSelectionRemovingPath, AddressOf SVG_OnSelectionRemovingPath
        AddHandler SVG.OnSelectionClearPaths, AddressOf SVG_OnSelectionClearPaths
        AddHandler SVG.OnSelectPoint, AddressOf SVG_OnSelectPoint
        AddHandler SVG.OnStickyGridChanged, AddressOf SVG_OnStickyGridChanged
        AddHandler SVG.OnChangePathIndex, AddressOf SVG_OnChangePathIndex
        AddHandler SVG.OnBkgTemplateAdd, AddressOf SVG_OnBkgTemplateAdd
        AddHandler SVG.OnBkgTemplateRemoving, AddressOf SVG_OnBkgTemplateRemoving
        AddHandler SVG.OnBkgTemplatesClear, AddressOf SVG_OnBkgTemplatesClear
        AddHandler SVG.OnCanvasOffsetChanged, AddressOf SVG_OnCanvasOffsetChanged
        AddHandler SVG.OnParsingStart, AddressOf SVG_OnParsingStart
        AddHandler SVG.OnParsingEnd, AddressOf SVG_OnParsingEnd

        'AddHandler SVG.selectedPaths.OnAdd, AddressOf SVGSelectedPaths_OnAdd
        'AddHandler SVG.selectedPaths.OnRemoving, AddressOf SVGSelectedPaths_OnRemoving
        'AddHandler SVG.selectedPaths.OnRemovingRange, AddressOf SVGSelectedPaths_OnRemovingRange
        'AddHandler SVG.selectedPaths.OnClear, AddressOf SVGSelectedPaths_OnClear

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
        AddHandler SVGPath.OnSelectionAddFigure, AddressOf SVGPath_OnSelectionAddFigure
        AddHandler SVGPath.OnSelectionRemovingFigure, AddressOf SVGPath_OnSelectionRemovingFigure
        AddHandler SVGPath.OnSelectionClearFigures, AddressOf SVGPath_OnSelectionClearFigures
        AddHandler SVGPath.OnChangeFigureIndex, AddressOf SVGPath_OnChangeFigureIndex
        AddHandler SVGPath.OnSetAttribute, AddressOf SVGPath_OnSetAttribute

        AddHandler PathPoint.OnModified, AddressOf PPoint_OnModified
    End Sub

    Public Sub SVG_OnCanvasSizeChanged()
        'Pic_canvas.Size = New Size(Math.Ceiling(SVG.CanvasSizeZoomed.Width), Math.Ceiling(SVG.CanvasSizeZoomed.Height))
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

        Lab_zoomedW.Text = "Zoomed W: " & SVG.CanvasSizeZoomed.Width
        Lab_zoomedH.Text = "Zoomed H: " & SVG.CanvasSizeZoomed.Height

        Pic_canvas.Invalidate()
        AddToHistory()
    End Sub

    Public Sub SVG_OnCanvasZoomChanged()
        If Num_zoom.Value <> SVG.CanvasZoom Then
            Num_zoom.Value = SVG.CanvasZoom
        End If

        gridZoomed = New SizeF(grid.Width * SVG.CanvasZoom, grid.Height * SVG.CanvasZoom)

        Lab_zoom.Text = "Zoom: " & Math.Round(SVG.CanvasZoom * 100, 1) & "%"
        Lab_zoomedW.Text = Lab_zoomedW.Tag & SVG.CanvasSizeZoomed.Width
        Lab_zoomedH.Text = Lab_zoomedH.Tag & SVG.CanvasSizeZoomed.Height

        RefreshBackGrid()
    End Sub

    Public Sub SVG_OnPathAdded(ByRef path As SVGPath)
        Lb_paths.Items.Add(path.Id)

        Lb_paths.SelectionMode = SelectionMode.One
        Lb_paths.SelectedIndex = Lb_paths.Items.Count - 1
        Lb_paths.SelectionMode = SelectionMode.MultiExtended

        'Load atributes into the listbox
        'Lb_attributes.Items.Clear()
        'For Each attr In path.Attributes
        '    Lb_attributes.Items.Add(attr.Key & ":" & attr.Value)
        'Next

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


    Public Sub SVG_OnSelectionAddPath(ByRef path As SVGPath)
        If path Is Nothing Then Return
        If Lb_paths.Tag <> LBLockMode.User Then Lb_paths.Tag = LBLockMode.SVG

        'Load atributes into the listbox
        If SVG.SelectedPath Is path Then
            Lb_attributes.Items.Clear()
            For Each attr In path.Attributes
                Lb_attributes.Items.Add(attr.Key & ":" & attr.Value)
            Next

            'Load figures
            Lb_figures.Items.Clear()
            For Each fig In SVG.SelectedPath.GetFigures
                Lb_figures.Items.Add(MakeFigureName(fig))
            Next

            'Change selected figure
            Lb_figures.SelectionMode = SelectionMode.None
            Lb_figures.SelectionMode = SelectionMode.MultiExtended
            If SVG.SelectedPath IsNot Nothing Then
                If SVG.SelectedPath.selectedFigures.Count <= 0 Then SVG.SelectedPath.SelectFigure(0)
                For Each fig As Figure In SVG.SelectedPath.selectedFigures.Reverse
                    Lb_figures.SelectedIndices.Add(fig.GetIndex())
                Next
            End If
            Pic_canvas.Invalidate()
        End If

        If SVG.SelectedPath.SelectedFigure.HasMoveto Then
            EnableButton(But_moveto, False)
        Else
            EnableButton(But_moveto, True)
        End If

        If Lb_figures.Tag = LBLockMode.User Then Return '  . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

        'Change selected path
        If Not Lb_paths.SelectedIndices.Contains(path.GetIndex) Then
            Lb_paths.SelectedIndices.Add(path.GetIndex)
        End If

        Pic_canvas.Invalidate()

        If Lb_paths.Tag = LBLockMode.SVG Then Lb_paths.Tag = LBLockMode.None
    End Sub

    Public Sub SVG_OnSelectionRemovingPath(ByRef path As SVGPath)
        If Lb_paths.Tag = LBLockMode.User Then Return
        If Lb_paths.Tag <> LBLockMode.User Then Lb_paths.Tag = LBLockMode.SVG

        Lb_paths.SelectedIndices.Remove(path.GetIndex)

        If SVG.SelectedPath IsNot path Then

        Else

        End If

        Pic_canvas.Invalidate()
        If Lb_paths.Tag = LBLockMode.SVG Then Lb_paths.Tag = LBLockMode.None
    End Sub

    Public Sub SVG_OnSelectionClearPaths()
        If Lb_paths.Tag = LBLockMode.User Then Return
        If Lb_paths.Tag <> LBLockMode.User Then Lb_paths.Tag = LBLockMode.SVG

        SVG.selectedPoints.Clear()
        Lb_figures.Items.Clear()
        Lb_paths.SelectionMode = SelectionMode.None
        Lb_paths.SelectedIndices.Clear()
        Lb_paths.SelectionMode = SelectionMode.MultiExtended

        Pic_canvas.Invalidate()
        If Lb_paths.Tag = LBLockMode.SVG Then Lb_paths.Tag = LBLockMode.None
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

    Public Sub SVG_OnBkgTemplateAdd(ByRef bkgTemp As BkgTemplate)
        Combo_templates.Items.Add(IO.Path.GetFileNameWithoutExtension(bkgTemp.path))
        Combo_templates.SelectedIndex = Combo_templates.Items.Count - 1
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub SVG_OnBkgTemplateRemoving(ByRef bkgTemp As BkgTemplate, index As Integer)
        Combo_templates.Items.RemoveAt(index)
        Combo_templates.SelectedIndex = Combo_templates.Items.Count - 1
        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub SVG_OnBkgTemplatesClear()
        Combo_templates.Items.Clear()

        AddToHistory()
    End Sub

    Public Sub SVG_OnCanvasOffsetChanged(ByVal newVal As Point)
        'GetVisibleCanvasRect
        If HScroll_canvasX.Tag <> True Then
            HScroll_canvasX.Minimum = Math.Min(0, -newVal.X)
            HScroll_canvasX.Maximum = Math.Max(HScroll_canvasX.LargeChange, -newVal.X) 'Math.Max(HScroll_canvasX.Minimum, (HScroll_canvasX.Minimum + SVG.CanvasSizeZoomed.Width) - Pic_canvas.Width)
            HScroll_canvasX.Value = -newVal.X
        End If
        If VScroll_canvasY.Tag <> True Then
            VScroll_canvasY.Minimum = Math.Min(0, -newVal.Y)
            VScroll_canvasY.Maximum = Math.Max(VScroll_canvasY.LargeChange, -newVal.Y) 'Math.Max(VScroll_canvasY.Minimum, (VScroll_canvasY.Minimum + SVG.CanvasSizeZoomed.Height) - Pic_canvas.Height)
            VScroll_canvasY.Value = -newVal.Y
        End If

        Pic_canvas.Invalidate()
    End Sub

    Public Sub SVG_OnParsingStart()
        'Stop controls from drawing, to speed thing up a bit
        Pan_all.SuspendDrawing()
        Cursor = Cursors.WaitCursor
    End Sub

    Public Sub SVG_OnParsingEnd()
        'Reenable drawing
        Pan_all.ResumeDrawing()
        Cursor = Cursors.Default
    End Sub

    'Public Sub SVGSelectedPaths_OnAdd(ByRef sender As ListWithEvents(Of SVGPath), ByRef d As SVGPath)
    '    If Lb_paths.Items.Count <= 0 Then Return
    '    If sender.Count = 1 Then
    '        If Not Lb_paths.SelectedIndex = d.GetIndex OrElse Lb_paths.SelectedIndices.Count > 1 Then
    '            Lb_paths.SelectionMode = SelectionMode.One
    '            Lb_paths.SelectedIndex = d.GetIndex
    '            Lb_paths.SelectionMode = SelectionMode.MultiExtended
    '        End If
    '    ElseIf Not Lb_paths.SelectedIndices.Contains(sender.IndexOf(d)) Then
    '        Lb_paths.SelectedIndices.Add(d.GetIndex)
    '    End If

    '    'If d IsNot Nothing Then
    '    '    Lb_figures.Items.Clear()
    '    '    For Each fig In d.GetFigures
    '    '        Lb_figures.Items.Add("Figure_" & fig.GetIndex() + 1)
    '    '    Next

    '    '    'Change selected figure
    '    '    Lb_figures.SelectionMode = SelectionMode.None
    '    '    Lb_figures.SelectionMode = SelectionMode.MultiExtended

    '    '    For Each fig As Figure In d.selectedFigures.Reverse
    '    '        Lb_figures.SelectedIndices.Add(fig.GetIndex())
    '    '    Next
    '    'End If
    'End Sub

    'Public Sub SVGSelectedPaths_OnRemoving(ByRef sender As ListWithEvents(Of SVGPath), ByRef d As SVGPath)
    '    Lb_paths.SelectedIndices.Remove(d.GetIndex)
    'End Sub

    'Public Sub SVGSelectedPaths_OnRemovingRange(ByRef sender As ListWithEvents(Of SVGPath), start As Integer, count As Integer)
    '    For i As Integer = start To start + count - 1
    '        Lb_paths.SelectedIndices.Remove(sender(i).GetIndex)
    '    Next
    'End Sub

    'Public Sub SVGSelectedPaths_OnClear(ByRef sender As ListWithEvents(Of SVGPath))
    '    Lb_paths.SelectedIndices.Clear()
    'End Sub

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
        If SVG.SelectedPath Is sender Then
            Lb_figures.Items.RemoveAt(fig.GetIndex())
        End If

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
            'Num_strokeWidth.Value = sender.StrokeWidth
        End If
    End Sub

    Public Sub SVGPath_OnIdChanged(ByRef sender As SVGPath, id As String)
        If sender.GetIndex() < 0 Then Return
        Lb_paths.Items.Item(sender.GetIndex()) = MakePathName(sender)
    End Sub

    Public Sub SVGPath_OnSelectionAddFigure(ByRef sender As SVGPath, ByRef fig As Figure)
        If fig.HasMoveto Then
            EnableButton(But_moveto, False)
        Else
            EnableButton(But_moveto, True)
        End If

        If SVG.SelectedPath IsNot sender Then Return
        If Lb_figures.Items.Count <= 0 Then Return
        If Lb_figures.Tag <> LBLockMode.User Then Lb_figures.Tag = LBLockMode.SVG
        If Lb_figures.Tag = LBLockMode.User Then Return

        SVG.selectedPoints.Clear()

        If Not Lb_figures.SelectedIndices.Contains(fig.GetIndex) Then
            Lb_figures.SelectedIndices.Add(fig.GetIndex)
        End If

        'Lb_figures.SelectionMode = SelectionMode.One
        'Lb_figures.SelectedIndex = fig.GetIndex()
        'Lb_figures.SelectionMode = SelectionMode.MultiExtended
        Pic_canvas.Invalidate()

        If Lb_figures.Tag = LBLockMode.SVG Then Lb_figures.Tag = LBLockMode.None
    End Sub

    Public Sub SVGPath_OnSelectionRemovingFigure(ByRef sender As SVGPath, ByRef fig As Figure)
        If Lb_figures.Tag = LBLockMode.User Then Return
        If Lb_figures.Tag <> LBLockMode.User Then Lb_figures.Tag = LBLockMode.SVG
        If SVG.SelectedPath IsNot sender Then Return
        If Lb_figures.Items.Count <= 0 Then Return

        SVG.selectedPoints.Clear()

        Lb_figures.SelectedIndices.Remove(fig.GetIndex)

        Pic_canvas.Invalidate()

        If Lb_figures.Tag = LBLockMode.SVG Then Lb_figures.Tag = LBLockMode.None
    End Sub

    Public Sub SVGPath_OnSelectionClearFigures(ByRef sender As SVGPath)
        If Lb_figures.Tag = LBLockMode.User Then Return
        If Lb_figures.Tag <> LBLockMode.User Then Lb_figures.Tag = LBLockMode.SVG
        If SVG.SelectedPath IsNot sender Then Return
        If Lb_figures.Items.Count <= 0 Then Return

        SVG.selectedPoints.Clear()

        Lb_figures.SelectedIndices.Clear()

        Pic_canvas.Invalidate()

        If Lb_figures.Tag = LBLockMode.SVG Then Lb_figures.Tag = LBLockMode.None
    End Sub

    Public Sub SVGPath_OnChangeFigureIndex(ByRef sender As SVGPath, oldIndx As Integer, newIndx As Integer)
        If SVG.SelectedPath IsNot sender Then Return
        Dim oldItem = Lb_figures.Items(oldIndx)
        Lb_figures.Items.RemoveAt(oldIndx)
        Lb_figures.Items.Insert(newIndx, oldItem)
    End Sub

    Public Sub SVGPath_OnSetAttribute(ByRef sender As SVGPath, attr As String, newVal As String)
        For i As Integer = 0 To Lb_attributes.Items.Count - 1
            If Lb_attributes.Items(i).StartsWith(attr & ":") Then
                Lb_attributes.Items(i) = attr & ":" & newVal
                Return
            End If
        Next
        'Add if not found
        Lb_attributes.Items.Add(attr & ":" & newVal)
    End Sub

    '----------------------------------------------------------------------------------------------------------------------------

    Public Sub Figure_OnPPointAdded(ByRef sender As Figure, ByRef pp As PathPoint)
        If sender.HasMoveto Then EnableButton(But_moveto, False)
        If pp.pointType = PointType.moveto AndAlso selectedTool = Tool.Drawing Then
            SetSelectedCommand(lastSelectedType)
        End If

        Pic_canvas.Invalidate()

        AddToHistory()
    End Sub

    Public Sub Figure_OnPPointRemoving(ByRef sender As Figure, ByRef pp As PathPoint)
        If SVG.selectedPoints.Contains(pp) Then SVG.selectedPoints.Remove(pp)

        If sender.HasMoveto = False Then EnableButton(But_moveto, True)

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
        Static tim As Timer = Nothing
        If tim Is Nothing Then
            tim = New Timer
            tim.Interval = 1000
            AddHandler tim.Tick, Sub(ByVal sender As Object, ByVal e As EventArgs)
                                     RefreshAttributesList()
                                     CType(sender, Timer).Enabled = False
                                 End Sub
        End If
        'Reset timer
        tim.Enabled = False
        tim.Enabled = True

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
        If index >= 0 AndAlso index < Lb_selPoints.Items.Count Then
            Lb_selPoints.Items.Item(index) = pp.GetString(False, noHV)
        End If

        Pic_canvas.Invalidate()
    End Sub

    Private queueSelectedPts As New List(Of PathPoint)

    Public Sub SVGSelectedPoints_OnAdd(ByRef sender As ListWithEvents(Of PathPoint), ByRef d As PathPoint)
        queueSelectedPts.Add(d)

        'Performance optimization
        Static tim As Timer = Nothing
        If tim Is Nothing Then
            tim = New Timer
            tim.Interval = 300
            AddHandler tim.Tick, Sub(ByVal sender2 As Object, ByVal e As EventArgs)
                                     For Each pp As PathPoint In queueSelectedPts
                                         Lb_selPoints.Items.Add(pp.GetString(False, noHV))
                                     Next
                                     Pic_canvas.Invalidate()
                                     CType(sender2, Timer).Enabled = False
                                 End Sub
        End If
        'Reset timer
        tim.Enabled = False
        tim.Enabled = True

        If SVG.SelectedPath IsNot Nothing AndAlso SVG.SelectedFigure.NumMirrored > 0 Then
            Dim anyMirrored As Boolean = False
            Dim prevMirrored As Boolean = False

            For Each pp As PathPoint In SVG.selectedPoints
                If pp.mirroredPos IsNot Nothing Then
                    anyMirrored = True
                    If prevMirrored Then Exit For
                End If

                If pp.PrevPPoint Is Nothing Then Continue For
                If pp.PrevPPoint.mirroredPos IsNot Nothing Then
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
    Public Sub SVGSelectedPoints_OnRemovingRange(ByRef sender As ListWithEvents(Of PathPoint), start As Integer, count As Integer)
        For i As Integer = start + count - 1 To start Step -1
            If Not queueSelectedPts.Contains(SVG.selectedPoints(i)) Then Lb_selPoints.Items.RemoveAt(i)
            queueSelectedPts.Remove(SVG.selectedPoints(i))
        Next
        Pic_canvas.Invalidate()
    End Sub
    Public Sub SVGSelectedPoints_OnRemoving(ByRef sender As ListWithEvents(Of PathPoint), ByRef d As PathPoint)
        If Not queueSelectedPts.Contains(d) Then Lb_selPoints.Items.RemoveAt(sender.IndexOf(d))
        queueSelectedPts.Remove(d)
        Pic_canvas.Invalidate()
    End Sub
    Public Sub SVGSelectedPoints_OnClear(ByRef sender As ListWithEvents(Of PathPoint))
        queueSelectedPts.Clear()
        Lb_selPoints.Items.Clear()
        Pic_canvas.Invalidate()
    End Sub

    'Custom Events
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Subroutines and Functions

    Public Function MakeFigureName(ByRef fig As Figure) As String
        Dim figname As String = "Figure_" & fig.GetIndex() + 1
        If fig.IsOpen Then figname &= " [Open]"
        If Not fig.IsVisible Then figname &= " [H]"
        Return figname
    End Function

    Public Function MakePathName(ByRef path As SVGPath) As String
        Dim pathname As String = path.Id
        If Not path.IsVisible Then pathname &= " [H]"
        Return pathname
    End Function

    Public Sub RefreshTitle()
        Me.Text = IO.Path.GetFileNameWithoutExtension(filePath)

        If modsSinceLastSave <> 0 Then
            Me.Text &= "* - WeSP Editor"
        Else
            Me.Text &= " - WeSP Editor"
        End If

    End Sub

    Public Sub SaveProject(newName As Boolean)
        SaveFileDialog1.Filter = "WeSP SVG|*.wsvg|SVG|*.svg|All Formats|*"

        If filePath = defFilePath OrElse newName = True Then
            SaveFileDialog1.FileName = IO.Path.GetFileName(filePath)
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                filePath = SaveFileDialog1.FileName
                RecentFilesAdd(SaveFileDialog1.FileName)
                OpenFileDialog1.FileName = SaveFileDialog1.FileName
            Else
                Return
            End If
        End If

        IO.File.WriteAllText(filePath, SVG.GetHtml(optimizePath, noHV))
        modsSinceLastSave = 0
        RefreshTitle()
    End Sub

    Public Sub LoadSettings()
        Cb_htmlWrap.Checked = My.Settings.htmlWarp
        Cb_optimize.Checked = My.Settings.htmlOptimize
        If My.Settings.windowSize.Width > 0 AndAlso My.Settings.windowSize.Height > 0 Then
            Me.Size = My.Settings.windowSize
        End If

        If My.Settings.gridSize.Width > 0 AndAlso My.Settings.gridSize.Height > 0 Then
            Num_gridWidth.Value = My.Settings.gridSize.Width
            Num_gridHeight.Value = My.Settings.gridSize.Height
        End If

        If My.Settings.stickyGridSize.Width > 0 AndAlso My.Settings.stickyGridSize.Height > 0 Then
            Num_stikyGWidth.Value = My.Settings.stickyGridSize.Width
            Num_stickyGHeight.Value = My.Settings.stickyGridSize.Height
        End If

        Num_decimals.Value = My.Settings.decimals

        'Load Attributes
        For Each item As String In My.Settings.attrnames
            If item.Length <= 0 Then Continue For
            Combo_attrName.Items.Add(item)
        Next
        For Each item As String In My.Settings.attrvalues
            If item.Length <= 0 Then Continue For
            Combo_attrVal.Items.Add(item)
        Next

        'Load Recent Files
        For Each item As String In My.Settings.recentfiles
            If item = "/PLACEHOLDER\" Then
                My.Settings.recentfiles.Remove(item)
                Continue For
            End If
            RecentFilesToolStripMenuItem.DropDownItems.Add(New ToolStripMenuItem(item, Nothing, Sub(ByVal sender As Object, ByVal e As EventArgs)
                                                                                                    LoadVectorFile(item)
                                                                                                    RecentFilesAdd(item)
                                                                                                End Sub))
        Next
    End Sub

    Public Sub RecentFilesAdd(filePath)
        'If exists, delete to put at top
        If My.Settings.recentfiles.Contains(filePath) Then
            Dim ioi As Integer = My.Settings.recentfiles.IndexOf(filePath)
            If ioi >= 0 Then
                RecentFilesToolStripMenuItem.DropDownItems.RemoveAt(ioi)
            End If
            My.Settings.recentfiles.Remove(filePath)
        End If

        My.Settings.recentfiles.Insert(0, filePath)
        RecentFilesToolStripMenuItem.DropDownItems.Insert(0, New ToolStripMenuItem(filePath, Nothing, Sub(ByVal sender As Object, ByVal e As EventArgs)
                                                                                                          LoadVectorFile(filePath)
                                                                                                          RecentFilesAdd(filePath)
                                                                                                      End Sub))

        'Ma 10 items
        If My.Settings.recentfiles.Count > 10 Then
            My.Settings.recentfiles.RemoveAt(My.Settings.recentfiles.Count - 1)
            RecentFilesToolStripMenuItem.DropDownItems.RemoveAt(RecentFilesToolStripMenuItem.DropDownItems.Count - 1)
        End If
        My.Settings.Save()
    End Sub

    Public Sub RefreshAttributesList()
        'Load atributes into the listbox
        If SVG.SelectedPath Is Nothing Then Return
        Dim num As Integer = 0
        'Lb_attributes.Items.Clear()
        For Each attr In SVG.SelectedPath.Attributes
            If Lb_attributes.Items.Count > num Then
                Lb_attributes.Items(num) = attr.Key & ":" & attr.Value
            Else
                Lb_attributes.Items.Add(attr.Key & ":" & attr.Value)
            End If
            num += 1
        Next

        While Lb_attributes.Items.Count > SVG.SelectedPath.Attributes.Count
            Lb_attributes.Items.RemoveAt(Lb_attributes.Items.Count - 1)
        End While
    End Sub

    Public Sub RefreshFiguresList()
        'Load figures
        Lb_figures.Items.Clear()
        For Each fig In SVG.SelectedPath.GetFigures
            Lb_figures.Items.Add(MakeFigureName(fig))
        Next
        'Change selected figure
        Lb_figures.SelectionMode = SelectionMode.None
        Lb_figures.SelectionMode = SelectionMode.MultiExtended
        If SVG.SelectedPath IsNot Nothing Then
            For Each fig As Figure In SVG.SelectedPath.selectedFigures.Reverse
                Lb_figures.SelectedIndices.Add(fig.GetIndex())
            Next
        End If
    End Sub

    Public Sub LoadVectorFile(fpath As String)
        On Error Resume Next
        filePath = fpath
        Me.Text = IO.Path.GetFileNameWithoutExtension(filePath) & " - WeSP Editor"
        SVG.ParseString(IO.File.ReadAllText(filePath))
        SaveFileDialog1.FileName = fpath
        HistoryModsReset()
    End Sub

    Public Sub LoadBkgTemplateFile(fpath As String)
        On Error Resume Next
        Dim newBTemp As New BkgTemplate(fpath)
        SVG.AddBkgTemplate(newBTemp)
    End Sub

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
        lastSelectedTool = selectedTool
        selectedTool = tool

        Select Case selectedTool
            Case Tool.Selection
                HighlightButton(But_selection, True)
            Case Tool.Movement
                HighlightButton(But_movement, True)
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
        lastSelectedType = selectedType
        selectedType = typ
        SetSelectedTool(Tool.Drawing)
    End Sub

    'Refresh info panel
    Private Sub UpdateStats()
        Static mpos As PointF = GetMousePlacePos(Pic_canvas)
        Dim digits As Integer = Math.Max(Math.Ceiling(SVG.CanvasSize.Width).ToString.Length, Math.Ceiling(SVG.CanvasSize.Height).ToString.Length) + 3
        mpos = GetMousePlacePos(Pic_canvas)
        Lab_mposX.Text = Lab_mposX.Tag & FormatNumber(mpos.X, 2, TriState.True, TriState.False, TriState.False).ToString.PadLeft(digits, "0") & " (" & SVG.ApplyZoom(mpos).X & ")"
        Lab_mposY.Text = Lab_mposY.Tag & FormatNumber(mpos.Y, 2, TriState.True, TriState.False, TriState.False).ToString.PadLeft(digits, "0") & " (" & SVG.ApplyZoom(mpos).Y & ")"

        Static rc As RectangleF
        rc = SVG.GetBounds()
        Lab_sizeW.Text = Lab_sizeW.Tag & Math.Round(rc.Width, 2)
        Lab_sizeH.Text = Lab_sizeH.Tag & Math.Round(rc.Height, 2)
    End Sub

    Private Function GetVisibleCanvasRect() As Rectangle
        Dim rc As Rectangle = Pic_canvas.ClientRectangle
        rc.Intersect(New Rectangle(SVG.CanvasOffset.X, SVG.CanvasOffset.Y, SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height))
        Return rc
    End Function

    'Redraw the canva's back grid
    Public Sub RefreshBackGrid()
        canvasBack = New Bitmap(Math.Max(CInt(SVG.CanvasZoom * SVG.StikyGrid.Width), 1), Math.Max(CInt(SVG.CanvasZoom * SVG.StikyGrid.Height), 1))
        Dim grx As Graphics = Graphics.FromImage(canvasBack)
        Dim br As New SolidBrush(Color.FromArgb(0, 0, 0, 0))
        Dim pen As New Pen(Color.FromArgb(255, 30, 30, 30), 1)

        grx.FillRectangle(br, New Rectangle(0, 0, canvasBack.Width, canvasBack.Height))
        grx.DrawRectangle(pen, New Rectangle(0, 0, canvasBack.Width, canvasBack.Height))
        Pic_canvas.Invalidate()
    End Sub

    Public Sub RefreshClosestPointToMouse()
        Dim mpos As PointF = GetMousePlacePos(Pic_canvas)
        'Select closest point
        If SVG.selectedPoints.Count <= 1 Then
            SVG.SelectedPoint = SVG.SelectedPath.GetClosestPoint(mpos, False)
            Pic_canvas.Invalidate()
        End If
    End Sub

    Public Sub SaveSettingsAsync()
        Static tim As Timer = Nothing
        If tim Is Nothing Then
            tim = New Timer
            tim.Interval = 1000
            AddHandler tim.Tick, Sub(ByVal sender As Object, ByVal e As EventArgs)
                                     My.Settings.Save()
                                     CType(sender, Timer).Enabled = False
                                 End Sub
        End If
        'Reset timer
        tim.Enabled = False
        tim.Enabled = True
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
        HighlightButton(But_showGrid, showGrid)

        'Add first figure
        SVG.Init()

        Tb_html.Text = SVG.GetHtml(optimizePath, noHV)

        HistoryLockRestore()
        AddToHistory()
        HistoryModsReset()

        'Load setting from previous version
        If My.Settings.recentfiles.Count <= 0 Then My.Settings.Upgrade()

        'My.Settings.recentfiles = New Collections.Specialized.StringCollection
        'My.Settings.Reload()
        LoadSettings()

        'Center the canvas
        SVG.CanvasOffset = New Point(Pic_canvas.Width / 2 - SVG.CanvasSizeZoomed.Width / 2, Pic_canvas.Height / 2 - SVG.CanvasSizeZoomed.Height / 2)

        initializing = False
    End Sub

    'Initializations
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Canvas Stuff

    Private Sub Pic_canvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles Pic_canvas.MouseWheel
        If My.Computer.Keyboard.CtrlKeyDown Then
            'Zoom in and out with mouse wheel
            If SVG.CanvasZoom >= 2 Then
                SVG.CanvasZoom += e.Delta / 120
            Else
                SVG.CanvasZoom += e.Delta / 1200
            End If
        End If
    End Sub

    Private Sub Pic_canvas_Click(sender As Object, e As EventArgs) Handles Pic_canvas.Click
        'Dim mpos As Point = GetMousePlacePos(Pic_canvas)
        Pic_canvas.Focus()
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

                    'If pressedKey = Keys.Space Then
                    '    'Move the entire SVG (all paths)

                    'End If

            End Select
        ElseIf e.Button = MouseButtons.Middle Then
            'Move the canvas
            mouseLastPos = Cursor.Position
            movingCanvas = True
            Pic_canvas.Cursor = Cursors.SizeAll
        End If

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

        Pic_canvas.Cursor = Cursors.Default

        Pic_canvas.Invalidate()
        'Timer_canvasMMove.Enabled = False

        If pressedMButton = MouseButtons.Left Then
            AddToHistory()
        End If
        RefreshAttributesList()

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
                        'Move the entire Path (all figures in the selected paths)
                        For Each path As SVGPath In SVG.selectedPaths
                            path.Offset(mpos - mouseLastPos)
                        Next

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
            If movingCanvas = True Then
                'Move the canvas
                SVG.OffsetCanvas(Cursor.Position.X - mouseLastPos.X, Cursor.Position.Y - mouseLastPos.Y)
                mouseLastPos = Cursor.Position
            End If

        End If

        Pic_canvas.Refresh()
    End Sub

    Private Sub Pic_canvas_Paint(sender As Object, e As PaintEventArgs) Handles Pic_canvas.Paint
        Static penBigGrid As New Pen(Color.FromArgb(150, Color.Yellow), 1)
        Static penSelAxis As New Pen(Color.FromArgb(150, Color.Chocolate), 1)
        Static penCentralAxis As New Pen(Color.FromArgb(100, Color.White), 1)

        'Dim ht As New HiResTimer
        'ht.Start()

        'Draw the back grid (stiky grid)
        If showGrid Then
            Dim visibleRect As Rectangle = GetVisibleCanvasRect()
            Dim tb As New TextureBrush(canvasBack)
            tb.TranslateTransform(SVG.CanvasOffset.X, SVG.CanvasOffset.Y)
            e.Graphics.FillRectangle(tb, visibleRect)
        End If

        'Offset the position of everything
        e.Graphics.TranslateTransform(SVG.CanvasOffset.X, SVG.CanvasOffset.Y)

        'Set drawing quality to fast
        e.Graphics.CompositingMode = Drawing2D.CompositingMode.SourceCopy
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.AssumeLinear
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.Bilinear
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed

        'Draw the background templates --------------------------------------------------------------------------------------
        For Each bkgtemp As BkgTemplate In SVG.BkgTemplates
            If bkgtemp.visible = False Then Continue For
            'Dim cm As New Imaging.ColorMatrix
            'Dim ia As New Imaging.ImageAttributes
            'cm.Matrix33 = 25 'alpha
            'ia.SetColorMatrix(cm)
            Dim rc As New RectangleF(SVG.ApplyZoom(bkgtemp.position), SVG.ApplyZoom(bkgtemp.size))
            e.Graphics.DrawImage(bkgtemp.image, rc)
        Next

        'Set drawing to be smooth
        e.Graphics.CompositingMode = Drawing2D.CompositingMode.SourceOver
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.Bilinear
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

        'Draw 32x32 cells reference Grid --------------------------------------------------------------------------------------
        If showBigGrid Then
            penBigGrid.DashPattern = {4 * SVG.CanvasZoom, 4 * SVG.CanvasZoom}
            For ix As Integer = gridZoomed.Width To SVG.CanvasSizeZoomed.Width - 1 Step gridZoomed.Width
                If ix = SVG.CanvasSizeZoomed.Width / 2.0F Then Continue For
                e.Graphics.DrawLine(penBigGrid, ix, 0, ix, SVG.CanvasSizeZoomed.Height)
            Next
            For iy As Integer = gridZoomed.Height To SVG.CanvasSizeZoomed.Height - 1 Step gridZoomed.Height
                If iy = SVG.CanvasSizeZoomed.Height / 2.0F Then Continue For
                e.Graphics.DrawLine(penBigGrid, 0, iy, SVG.CanvasSizeZoomed.Width, iy)
            Next

            'Central axis
            penCentralAxis.DashPattern = {4 * SVG.CanvasZoom, 4 * SVG.CanvasZoom}
            penCentralAxis.DashOffset = -(SVG.CanvasSizeZoomed.Width / 2.0F / penCentralAxis.Width - (4 * SVG.CanvasZoom / 2)) 'keep the dashed line centered
            e.Graphics.DrawLine(penCentralAxis, 0, SVG.CanvasSizeZoomed.Height / 2.0F, SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height / 2.0F)
            penCentralAxis.DashOffset = -(SVG.CanvasSizeZoomed.Height / 2.0F / penCentralAxis.Width - (4 * SVG.CanvasZoom / 2)) 'keep the dashed line centered
            e.Graphics.DrawLine(penCentralAxis, SVG.CanvasSizeZoomed.Width / 2.0F, 0, SVG.CanvasSizeZoomed.Width / 2.0F, SVG.CanvasSizeZoomed.Height)
        End If

        'Draw SVG -------------------------------------------------------------------------------------------------------------
        'Dim grxCanvas As Graphics = Graphics.FromImage(canvasImg)
        'grxCanvas.Clear(Color.FromArgb(0, 0, 0, 0))

        For Each path As SVGPath In SVG.Paths
            path.Draw(e.Graphics)
        Next

        'DRAW POINTS IN HERE >>>>>>>>>

        Static penPointsRefIn As New Pen(Color.Orange, 1)
        Static penPointsSelIn As New Pen(Color.Lime, 1)
        Static penPointsClosestIn As New Pen(Color.LightSalmon, 1)
        Static penPointsMovetoIn As New Pen(Color.Violet, 1)
        Static penPointsIn As New Pen(Color.DeepSkyBlue, 1)
        Static penPointsOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)
        Static brushRefIn As New SolidBrush(Color.Orange)
        Static brushRefOut As New SolidBrush(Color.FromArgb(255, 40, 40, 40))

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
                    If pp.Pos Is Nothing OrElse pp.nonInteractve = True Then Continue For

                    penMirror.Color = ColorRotate(fig.parent.FillColor)
                    If pp.mirroredPos IsNot Nothing AndAlso pp.isMirrorOrigin Then
                        e.Graphics.DrawLine(penMirror, SVG.ApplyZoom(pp.Pos), SVG.ApplyZoom(pp.mirroredPos.Pos))
                        If pp.mirroredPP IsNot Nothing AndAlso pp.mirroredPos IsNot pp.mirroredPP Then
                            e.Graphics.DrawLine(penMirror, SVG.ApplyZoom(pp.Pos), SVG.ApplyZoom(pp.mirroredPP.Pos))
                        End If
                    End If

                    Dim rc As New RectangleF(New PointF(pp.Pos.X * SVG.CanvasZoom - 6, pp.Pos.Y * SVG.CanvasZoom - 6), New SizeF(12, 12))

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
        penSelAxis.DashPattern = {4 * SVG.CanvasZoom, 4 * SVG.CanvasZoom}

        If movingPoints = True AndAlso SVG.selectedPoints.Count = 1 AndAlso SVG.SelectedPoint.selPoint IsNot Nothing Then
            e.Graphics.DrawLine(penSelAxis, New PointF(SVG.ApplyZoom(SVG.SelectedPoint.selPoint).X, 0), New PointF(SVG.ApplyZoom(SVG.SelectedPoint.selPoint).X, SVG.CanvasSizeZoomed.Height))
            e.Graphics.DrawLine(penSelAxis, New PointF(0, SVG.ApplyZoom(SVG.SelectedPoint.selPoint).Y), New PointF(SVG.CanvasSizeZoomed.Width, SVG.ApplyZoom(SVG.SelectedPoint.selPoint).Y))
        End If

        'Draw selection -------------------------------------------------------------------------------------------------------
        penSelAxis.DashPattern = {3, 3}
        If selectedTool = Tool.Selection Then
            e.Graphics.DrawRectangle(penSelAxis, SVG.ApplyZoom(selectionRect).ToRectangle)
        End If

        ' ---------------------------------------------------------------------------------------------------------------------

        UpdateStats()

        'Dim ht As New HiResTimer
        'ht.Start()

        'If Not Tb_html.Focused Then Tb_html.Text = SVG.GetHtml(optimizePath)

        'Me.Text = ht.ElapsedTime

        'Clean
        'grxCanvas.Dispose()
        'e.Graphics.DrawImage(canvasImg, 0, 0)
        'Me.Text = ht.ElapsedTime / ht.Frequency & " (" & Math.Round(1 / (ht.ElapsedTime / ht.Frequency)) & " fps)"
        refreshHtml = True
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

        HistoryLock()

        Dim lastPP As PathPoint = SVG.GetSelectedPPLastInPath()
        SVG.SortSelectedPoints()

        If SVG.selectedPoints.Count > 0 Then
            Dim ph As PathPoint = Nothing
            If SVG.SelectedFigure.NumMirrored <= 0 Then
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
            Pic_canvas.Invalidate()
        End If

        HistoryLockRestore()
        AddToHistory()
    End Sub

    Private Sub But_mirrorVert_Click(sender As Object, e As EventArgs) Handles But_mirrorVert.Click
        If SVG.SelectedPath Is Nothing OrElse SVG.SelectedPath.selectedFigures.Count > 1 Then Return
        If SVG.SelectedFigure.mirrorOrient = Orientation.Horizontal Then Return

        HistoryLock()

        Dim lastPP As PathPoint = SVG.GetSelectedPPLastInPath()
        SVG.SortSelectedPoints()

        If SVG.selectedPoints.Count > 0 Then
            Dim ph As PathPoint = Nothing
            If SVG.SelectedFigure.NumMirrored <= 0 Then
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
            Pic_canvas.Invalidate()
        End If

        HistoryLockRestore()
        AddToHistory()
    End Sub

    Private Sub But_selection_Click(sender As Object, e As EventArgs) Handles But_selection.Click
        SetSelectedTool(Tool.Selection)
    End Sub

    Private Sub But_movement_Click(sender As Object, e As EventArgs) Handles But_movement.Click
        SetSelectedTool(Tool.Movement)
    End Sub

    Private Sub But_moveto_Click(sender As Object, e As EventArgs) Handles But_moveto.Click
        SetSelectedCommand(PointType.moveto)
    End Sub

    Private Sub But_lineto_Click(sender As Object, e As EventArgs) Handles But_lineto.Click
        SetSelectedCommand(PointType.lineto)
    End Sub

    Private Sub But_horLineto_Click(sender As Object, e As EventArgs) Handles But_horLineto.Click
        SetSelectedCommand(PointType.horizontalLineto)
    End Sub

    Private Sub But_vertLineto_Click(sender As Object, e As EventArgs) Handles But_vertLineto.Click
        SetSelectedCommand(PointType.verticalLineto)
    End Sub

    Private Sub But_curveto_Click(sender As Object, e As EventArgs) Handles But_curveto.Click
        SetSelectedCommand(PointType.curveto)
    End Sub

    Private Sub But_smoothCurveto_Click(sender As Object, e As EventArgs) Handles But_smoothCurveto.Click
        SetSelectedCommand(PointType.smoothCurveto)
    End Sub

    Private Sub But_bezier_Click(sender As Object, e As EventArgs) Handles But_bezier.Click
        SetSelectedCommand(PointType.quadraticBezierCurve)
    End Sub

    Private Sub But_smoothBezier_Click(sender As Object, e As EventArgs) Handles But_smoothBezier.Click
        SetSelectedCommand(PointType.smoothQuadraticBezierCurveto)
    End Sub

    Private Sub But_elliArc_Click(sender As Object, e As EventArgs) Handles But_elliArc.Click
        SetSelectedCommand(PointType.ellipticalArc)
    End Sub

    Private Sub But_closePath_Click(sender As Object, e As EventArgs) Handles But_closePath.Click, CreateFigureToolStripMenuItem.Click, But_addFigure.Click
        SVG.SelectedPath.AddNewFigure()
        EnableButton(But_moveto, True)

        SetSelectedCommand(PointType.moveto)

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
                SVG.selectedPoints.Clear()
                For Each fig As Figure In SVG.GetSelectedFigures()
                    fig.SelectAllPPoints(False)
                Next
                Pic_canvas.Invalidate()

            ElseIf e.KeyCode = Keys.D AndAlso e.Modifiers = Keys.Control Then
                'Clear selection
                SVG.selectedPoints.Clear()
                Pic_canvas.Invalidate()

            ElseIf e.KeyCode = Keys.Delete AndAlso e.Modifiers = Keys.None Then
                'Delete the selected points
                For Each pp As PathPoint In SVG.selectedPoints.Reverse
                    If pp.pointType = PointType.moveto AndAlso pp.parent.Count > 1 Then Continue For
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

        If e.KeyCode = Keys.S AndAlso e.Modifiers = Keys.Control Then
            SaveProject(False)
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
        For Each path In SVG.selectedPaths
            For Each pp As PathPoint In path.GetAllPPoints
                pp.RoundPosition()
            Next
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
        SVG.AddNewPath()
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

    Private Sub Num_canvasWidth_LostFocus(sender As Object, e As EventArgs) Handles Num_canvasWidth.LostFocus, Num_canvasHeight.LostFocus
        AddToHistory()
    End Sub

    Private Sub Num_zoom_ValueChanged(sender As Object, e As EventArgs) Handles Num_zoom.ValueChanged
        SVG.CanvasZoom = Num_zoom.Value
    End Sub

    Private Sub MoveTo00ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MoveTo00ToolStripMenuItem1.Click
        Dim bounds As RectangleF = SVG.GetBounds()
        SVG.Offset(bounds.X * -1, bounds.Y * -1)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub ClearToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem2.Click
        SVG.Clear()
        Pic_canvas.Invalidate()
    End Sub

    Private Sub CropToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CropToolStripMenuItem.Click
        Dim margin As Single = 0
        Dim response As String = InputBox("How much margin to leave around?" & vbCrLf & "Use decimal separator """ & Application.CurrentCulture.NumberFormat.CurrencyDecimalSeparator & """", "Cropping margin", "0")
        If response.Length <= 0 Then Exit Sub 'If canceled or no input, then exit the sub
        If IsNumeric(response) Then margin = response

        Dim bounds As RectangleF = SVG.GetBounds()
        SVG.Offset((bounds.X - margin) * -1, (bounds.Y - margin) * -1)
        SVG.CanvasSize = New SizeF(bounds.Width + margin * 2, bounds.Height + margin * 2)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Lb_figures_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_figures.SelectedIndexChanged
        If Lb_figures.Tag = LBLockMode.SVG Then Return
        Lb_figures.Tag = LBLockMode.User 'Modifying selection

        'Change selected figure
        If SVG.SelectedPath IsNot Nothing Then
            SVG.selectedPoints.Clear()

            'Select the last figure if nothing is selected
            If Lb_figures.SelectedIndices.Count <= 0 Then
                Lb_figures.SelectedIndex = Lb_figures.Items.Count - 1
            End If

            SVG.SelectedPath.selectedFigures.Clear()

            For Each i As Integer In Lb_figures.SelectedIndices
                Dim fig As Figure = SVG.SelectedPath.Figure(i)

                SVG.SelectedPath.selectedFigures.Add(fig)
            Next

            Pic_canvas.Invalidate()
        End If

        Lb_figures.Tag = LBLockMode.None 'Selection done
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
            'Dim oldZoom As Single = SVG.CanvasZoom
            'SVG.CanvasZoom = 1
            Select Case IO.Path.GetExtension(SaveFileDialog1.FileName).ToLower
                Case ".jpg"
                    SVG.GetBitmap(SVG.CanvasSizeZoomed).Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
                Case ".bmp"
                    SVG.GetBitmap(SVG.CanvasSizeZoomed).Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Bmp)
                Case ".tiff"
                    SVG.GetBitmap(SVG.CanvasSizeZoomed).Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Tiff)
                Case ".ico"
                    Dim ico As Drawing.Icon = Drawing.Icon.FromHandle(SVG.GetBitmap(SVG.CanvasSizeZoomed).GetHicon)
                    Dim oFileStream As New IO.FileStream(SaveFileDialog1.FileName, IO.FileMode.CreateNew)
                    ico.Save(oFileStream)
                    oFileStream.Close()
                Case Else
                    SVG.GetBitmap(SVG.CanvasSizeZoomed).Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
            End Select
            'SVG.CanvasZoom = oldZoom
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
        HTMLParser.GetElements(Tb_html.Text.ToString)

        SVG.ParseString(Tb_html.Text.ToString)
        Focus()
        Tb_html.Focus()
        AddToHistory()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveProject(False)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveProject(True)
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        OpenFileDialog1.Filter = "WeSP SVG|*.wsvg|SVG|*.svg|All Formats|*"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            If modsSinceLastSave <> 0 Then
                Dim answer As MsgBoxResult = MsgBox("Save changes to actual project, before opening the new one?", MsgBoxStyle.YesNoCancel)
                If answer = MsgBoxResult.Yes Then
                    SaveProject(False)
                ElseIf answer = MsgBoxResult.Cancel Then
                    Return
                End If
            End If

            LoadVectorFile(OpenFileDialog1.FileName)
            RecentFilesAdd(OpenFileDialog1.FileName)
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

        SetSettingValue(My.Settings.gridSize, New Size(grid.ToSize.Width, My.Settings.gridSize.Height))

        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num_gridHeight_ValueChanged(sender As Object, e As EventArgs) Handles Num_gridHeight.ValueChanged
        grid = New SizeF(grid.Width, Num_gridHeight.Value)
        gridZoomed = New SizeF(grid.Width * SVG.CanvasZoom, grid.Height * SVG.CanvasZoom)

        SetSettingValue(My.Settings.gridSize, New Size(My.Settings.gridSize.Width, grid.ToSize.Height))

        Pic_canvas.Invalidate()
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        Redo()
    End Sub

    Private Sub Num_stikyGWidth_ValueChanged(sender As Object, e As EventArgs) Handles Num_stikyGWidth.ValueChanged
        SVG.StikyGrid = New SizeF(Num_stikyGWidth.Value, SVG.StikyGrid.Height)
        SetSettingValue(My.Settings.stickyGridSize, New Size(SVG.StikyGrid.ToSize.Width, My.Settings.stickyGridSize.Height))
    End Sub

    Private Sub Num_stickyGHeight_ValueChanged(sender As Object, e As EventArgs) Handles Num_stickyGHeight.ValueChanged
        SVG.StikyGrid = New SizeF(SVG.StikyGrid.Width, Num_stickyGHeight.Value)
        SetSettingValue(My.Settings.stickyGridSize, New Size(My.Settings.stickyGridSize.Width, SVG.StikyGrid.ToSize.Height))
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
        For Each pp As PathPoint In SVG.selectedPoints
            pp.BreakMirror()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Cb_optimize_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_optimize.CheckedChanged
        optimizePath = Cb_optimize.Checked

        SetSettingValue(My.Settings.htmlOptimize, Cb_optimize.Checked)

        Pic_canvas.Invalidate()
    End Sub

    Private Sub Num_decimals_ValueChanged(sender As Object, e As EventArgs) Handles Num_decimals.ValueChanged
        decimalPlaces = Num_decimals.Value

        SetSettingValue(My.Settings.decimals, Num_decimals.Value)

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
        AddToHistory()
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
        AddToHistory()
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
        AddToHistory()
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
        AddToHistory()
    End Sub

    Private Sub Lb_paths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_paths.SelectedIndexChanged
        If Lb_paths.Tag = LBLockMode.SVG Then Return
        Lb_paths.Tag = LBLockMode.User 'Modifying selection

        'Select the last path if nothing is selected
        If Lb_paths.SelectedIndices.Count <= 0 Then
            Lb_paths.SelectedIndex = Lb_paths.Items.Count - 1
        End If

        SVG.selectedPaths.Clear()

        For Each i As Integer In Lb_paths.SelectedIndices
            Dim path As SVGPath = SVG.Paths(i)

            SVG.selectedPaths.Add(path)
        Next

        Pic_canvas.Invalidate()

        Lb_paths.Tag = LBLockMode.None 'Selection done
    End Sub

    Private Sub But_pathMoveUp_Click(sender As Object, e As EventArgs) Handles But_pathMoveUp.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx - 1)
        Next
        Lb_paths.SelectionMode = SelectionMode.None
        Lb_paths.SelectionMode = SelectionMode.MultiExtended
        For Each indx As Integer In selIndxes
            If indx - 1 < 0 Then Continue For
            Lb_paths.SelectedIndex = indx - 1
        Next
        AddToHistory()
    End Sub

    Private Sub But_pathMoveDown_Click(sender As Object, e As EventArgs) Handles But_pathMoveDown.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_paths.Items.Count - 1 Then Continue For
            SVG.ChangePathIndex(indx, indx + 1)
        Next
        Lb_paths.SelectionMode = SelectionMode.None
        Lb_paths.SelectionMode = SelectionMode.MultiExtended
        For Each indx As Integer In selIndxes
            If indx + 1 > Lb_paths.Items.Count - 1 Then Continue For
            Lb_paths.SelectedIndex = indx + 1
        Next
        AddToHistory()
    End Sub

    Private Sub But_pathMoveTop_Click(sender As Object, e As EventArgs) Handles But_pathMoveTop.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        Dim indxShift As Integer = selIndxes(0)
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx - indxShift)
        Next
        Lb_paths.SelectionMode = SelectionMode.None
        Lb_paths.SelectionMode = SelectionMode.MultiExtended
        For Each indx As Integer In selIndxes
            If indx - indxShift < 0 Then Continue For
            Lb_paths.SelectedIndex = indx - indxShift
        Next
        AddToHistory()
    End Sub

    Private Sub But_pathMoveBottom_Click(sender As Object, e As EventArgs) Handles But_pathMoveBottom.Click
        Dim selIndxes As List(Of Integer) = Lb_paths.SelectedIndices.ToList
        Dim indxShift As Integer = (Lb_paths.Items.Count - 1) - selIndxes.Last
        selIndxes.Reverse()
        For Each indx As Integer In selIndxes
            If indx + indxShift < 0 Then Continue For
            SVG.ChangePathIndex(indx, indx + indxShift)
        Next
        Lb_paths.SelectionMode = SelectionMode.None
        Lb_paths.SelectionMode = SelectionMode.MultiExtended
        For Each indx As Integer In selIndxes
            If indx + indxShift > Lb_paths.Items.Count - 1 Then Continue For
            Lb_paths.SelectedIndex = indx + indxShift
        Next
        AddToHistory()
    End Sub

    Private Sub But_pathRename_Click(sender As Object, e As EventArgs) Handles But_pathRename.Click
        If SVG.SelectedPath Is Nothing Then Return
        Dim newId = InputBox("Insert new path Id", "New Id", SVG.SelectedPath.Id)
        If newId.Length > 0 Then
            SVG.SelectedPath.Id = newId
            AddToHistory()
        End If
    End Sub

    Private Sub Timer_autoBackup_Tick(sender As Object, e As EventArgs) Handles Timer_autoBackup.Tick
        If modsSinceLastBkp > 0 Then
            IO.File.WriteAllText("backup.wsvg", SVG.GetHtml(optimizePath, noHV))
            Lab_lastBkp.Text = Lab_lastBkp.Tag & Now.ToShortTimeString
            modsSinceLastBkp = 0
        End If
    End Sub

    Private Sub LoadBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadBackupToolStripMenuItem.Click
        If modsSinceLastSave <> 0 Then
            Dim answer As MsgBoxResult = MsgBox("Save changes to actual project?", MsgBoxStyle.YesNoCancel)
            If answer = MsgBoxResult.Yes Then
                SaveProject(False)
            ElseIf answer = MsgBoxResult.Cancel Then
                Return
            End If
        End If

        If IO.File.Exists("backup.wsvg") Then
            SVG.ParseString(IO.File.ReadAllText("backup.wsvg"))
        End If
    End Sub

    Private Sub Form_main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If e.CloseReason = CloseReason.WindowsShutDown Then Return

        If e.CloseReason = CloseReason.UserClosing AndAlso modsSinceLastSave <> 0 Then
            Dim answer As MsgBoxResult = MsgBox("Save changes before exiting?", MsgBoxStyle.YesNoCancel)
            If answer = MsgBoxResult.Yes Then
                SaveProject(False)
            ElseIf answer = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Cb_htmlWrap_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_htmlWrap.CheckedChanged
        Tb_html.WordWrap = Cb_htmlWrap.Checked
        SetSettingValue(My.Settings.htmlWarp, Cb_htmlWrap.Checked)
    End Sub

    Private Sub But_removeSelPts_Click(sender As Object, e As EventArgs) Handles But_removeSelPts.Click
        'Delete the selected points
        For Each pp As PathPoint In SVG.selectedPoints.Reverse
            If pp.pointType = PointType.moveto Then Continue For
            pp.Delete()
        Next
        Pic_canvas.Invalidate()
    End Sub

    Private Sub But_addTemplate_Click(sender As Object, e As EventArgs) Handles But_addTemplate.Click
        OpenFileDialog1.Filter = "ALL|*.*|BMP|*.bmp|JPG, JPEG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tiff"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            LoadBkgTemplateFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Combo_templates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combo_templates.SelectedIndexChanged
        SVG.selectedBkgTemp = SVG.BkgTemplates(Combo_templates.SelectedIndex)
        Num_templateX.Value = SVG.selectedBkgTemp.position.X
        Num_templateY.Value = SVG.selectedBkgTemp.position.Y
        Num_templateW.Value = SVG.selectedBkgTemp.size.Width
        Num_templateH.Value = SVG.selectedBkgTemp.size.Height
        Cb_templateKeepAspect.Checked = SVG.selectedBkgTemp.keepAspect
        Cb_templateVisible.Checked = SVG.selectedBkgTemp.visible
    End Sub

    Private Sub Num_templateX_ValueChanged(sender As Object, e As EventArgs) Handles Num_templateX.ValueChanged
        If SVG.selectedBkgTemp Is Nothing Then Return
        SVG.selectedBkgTemp.position.X = Num_templateX.Value
        Pic_canvas.Refresh()
    End Sub

    Private Sub Num_templateY_ValueChanged(sender As Object, e As EventArgs) Handles Num_templateY.ValueChanged
        If SVG.selectedBkgTemp Is Nothing Then Return
        SVG.selectedBkgTemp.position.Y = Num_templateY.Value
        Pic_canvas.Refresh()
    End Sub

    Private Sub Num_templateW_ValueChanged(sender As Object, e As EventArgs) Handles Num_templateW.ValueChanged
        If SVG.selectedBkgTemp Is Nothing OrElse SVG.selectedBkgTemp.size.Width = Num_templateW.Value Then Return
        SVG.selectedBkgTemp.size.Width = Num_templateW.Value
        If SVG.selectedBkgTemp.keepAspect Then
            Dim ratio As Single = SVG.selectedBkgTemp.image.Width / SVG.selectedBkgTemp.image.Height
            Num_templateH.Value = SVG.selectedBkgTemp.size.Width / ratio
        End If
        Pic_canvas.Refresh()
    End Sub

    Private Sub Num_templateH_ValueChanged(sender As Object, e As EventArgs) Handles Num_templateH.ValueChanged
        If SVG.selectedBkgTemp Is Nothing OrElse SVG.selectedBkgTemp.size.Height = Num_templateH.Value Then Return
        SVG.selectedBkgTemp.size.Height = Num_templateH.Value
        If SVG.selectedBkgTemp.keepAspect Then
            Dim ratio As Single = SVG.selectedBkgTemp.image.Width / SVG.selectedBkgTemp.image.Height
            Num_templateW.Value = SVG.selectedBkgTemp.size.Height * ratio
        End If
        Pic_canvas.Refresh()
    End Sub

    Private Sub Num_templateXYWH_LostFocus(sender As Object, e As EventArgs) Handles Num_templateX.LostFocus, Num_templateY.LostFocus, Num_templateW.LostFocus, Num_templateH.LostFocus
        AddToHistory()
    End Sub

    Private Sub Cb_templateKeepAspect_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_templateKeepAspect.CheckedChanged
        If SVG.selectedBkgTemp Is Nothing Then Return
        SVG.selectedBkgTemp.keepAspect = Cb_templateKeepAspect.Checked

        AddToHistory()
        'Pic_canvas.Invalidate()
    End Sub

    Private Sub But_removeTemplate_Click(sender As Object, e As EventArgs) Handles But_removeTemplate.Click
        If Combo_templates.SelectedIndex < 0 Then Return
        SVG.RemoveBkgTemplateAt(Combo_templates.SelectedIndex)
    End Sub

    Private Sub Cb_templateVisible_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_templateVisible.CheckedChanged
        If SVG.selectedBkgTemp Is Nothing Then Return
        SVG.selectedBkgTemp.visible = Cb_templateVisible.Checked
        Pic_canvas.Refresh()

        AddToHistory()
    End Sub

    Private Sub ScaleToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ScaleToolStripMenuItem1.Click
        Me.Enabled = False
        Form_scale.Show()
        Form_scale.SetScalingObjective(Form_scale.ScalingObjective.Figures)
    End Sub

    Private Sub Tb_html_Enter(sender As Object, e As EventArgs) Handles Tb_html.Enter
        Tb_html.Text = SVG.GetHtml(optimizePath, noHV)
    End Sub

    Private Sub Timer_refresh_Tick(sender As Object, e As EventArgs) Handles Timer_refresh.Tick
        If Not Tb_html.Focused And refreshHtml Then
            Tb_html.Text = SVG.GetHtml(optimizePath, noHV)
        End If
        If refreshHtml Then
            'Dim oldZoom As Single = SVG.CanvasZoom
            'SVG.CanvasZoom = 1
            If Pic_preview.Image IsNot Nothing Then
                Pic_preview.Image.Dispose()
            End If
            Pic_preview.Image = SVG.GetBitmap(SVG.CanvasSize.ToSize)
            If Form_result.Visible Then
                If Form_result.Pic_realSize.Image IsNot Nothing Then Form_result.Pic_realSize.Image.Dispose()
                Form_result.Pic_realSize.Image = Pic_preview.Image
            End If
            'SVG.CanvasZoom = oldZoom
        End If
        refreshHtml = False
    End Sub

    Private Sub Pic_canvas_Resize(sender As Object, e As EventArgs) Handles Pic_canvas.Resize
        canvasImg = New Bitmap(Pic_canvas.Width, Pic_canvas.Height)
    End Sub

    Private Sub HScroll_canvasX_Scroll(sender As Object, e As ScrollEventArgs) Handles HScroll_canvasX.Scroll
        HScroll_canvasX.Tag = True
        SVG.CanvasOffset = New Point(-Math.Min(HScroll_canvasX.Maximum, Math.Max(HScroll_canvasX.Minimum, HScroll_canvasX.Value)), SVG.CanvasOffset.Y)
        HScroll_canvasX.Tag = False
    End Sub

    Private Sub VScroll_canvasY_Scroll(sender As Object, e As ScrollEventArgs) Handles VScroll_canvasY.Scroll
        VScroll_canvasY.Tag = True
        SVG.CanvasOffset = New Point(SVG.CanvasOffset.X, -Math.Min(VScroll_canvasY.Maximum, Math.Max(VScroll_canvasY.Minimum, VScroll_canvasY.Value)))
        VScroll_canvasY.Tag = False
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If modsSinceLastSave <> 0 Then
            Dim answer As MsgBoxResult = MsgBox("Save changes to actual project?", MsgBoxStyle.YesNoCancel)
            If answer = MsgBoxResult.Yes Then
                SaveProject(False)
            ElseIf answer = MsgBoxResult.Cancel Then
                Return
            End If
        End If

        SVG.CanvasSize = New SizeF(64, 64)
        SVG.AttributesSetDefaults()
        SVG.Clear()
        SVG.ClearBkgTemplates()

        filePath = defFilePath

        Me.Text = "untitled - WeSP Editor"
    End Sub

    Private Sub ScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScaleToolStripMenuItem.Click
        Me.Enabled = False
        Form_scale.Show()
        Form_scale.SetScalingObjective(Form_scale.ScalingObjective.Paths)
    End Sub

    Private Sub ScaleToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ScaleToolStripMenuItem2.Click
        Me.Enabled = False
        Form_scale.Show()
        Form_scale.SetScalingObjective(Form_scale.ScalingObjective.SVG)
    End Sub

    Private Sub Form_main_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Form_main_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim s() As String = e.Data.GetData("FileDrop", False)
        Dim i As Integer
        For i = 0 To s.Length - 1
            Dim extension As String = IO.Path.GetExtension(s(i))
            If extension = ".wsvg" Then
                LoadVectorFile(s(i))
            ElseIf Regex.IsMatch(s(i), ".bmp|.jpg|.jpeg|.png|.tiff") Then
                LoadBkgTemplateFile(s(i))
            End If
        Next i
    End Sub

    Private Sub But_bkgTempCenter_Click(sender As Object, e As EventArgs) Handles But_bkgTempCenter.Click
        If SVG.selectedBkgTemp Is Nothing Then Return
        Dim cvCenter As New PointF(SVG.CanvasSize.Width / 2, SVG.CanvasSize.Height / 2)
        Num_templateX.Value = cvCenter.X - (SVG.selectedBkgTemp.size.Width / 2)
        Num_templateY.Value = cvCenter.Y - (SVG.selectedBkgTemp.size.Height / 2)
        Pic_canvas.Refresh()

        AddToHistory()
    End Sub

    Private Sub But_bkgTempFill_Click(sender As Object, e As EventArgs) Handles But_bkgTempFill.Click
        If SVG.selectedBkgTemp Is Nothing Then Return
        Num_templateH.Value = SVG.CanvasSize.Height
        Num_templateW.Value = SVG.CanvasSize.Width
        Num_templateX.Value = 0
        Num_templateY.Value = 0
        AddToHistory()
    End Sub

    Private Sub ConfirmAttribute()
        If Not Combo_attrName.Items.Contains(Combo_attrName.Text) Then
            Combo_attrName.Items.Add(Combo_attrName.Text)
            My.Settings.attrnames.Add(Combo_attrName.Text)
        End If
        If Not Combo_attrVal.Items.Contains(Combo_attrVal.Text) Then
            Combo_attrVal.Items.Add(Combo_attrVal.Text)
            My.Settings.attrvalues.Add(Combo_attrVal.Text)
        End If

        'Set new values
        SVG.SelectedPath.SetAttribute(Combo_attrName.Text, Combo_attrVal.Text)

        '<<

        My.Settings.Save()
        AddToHistory()
        Pic_canvas.Refresh()
    End Sub

    Private Sub But_attrOk_Click(sender As Object, e As EventArgs) Handles But_attrOk.Click
        ConfirmAttribute()
    End Sub

    Private Sub Pic_attrColor_Click(sender As Object, e As EventArgs) Handles Pic_attrColor.Click
        ColorDialog1.Color = Pic_attrColor.BackColor
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            Pic_attrColor.BackColor = ColorDialog1.Color
            Combo_attrVal.Text = HTMLParser.ColorToHexString(ColorDialog1.Color)
        End If
    End Sub

    Private Sub Lb_attributes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_attributes.SelectedIndexChanged
        If Lb_attributes.SelectedIndex < 0 Then Return

        Dim parts As String() = Split(Lb_attributes.SelectedItem, ":")
        If parts.Length >= 2 Then
            Combo_attrName.Text = parts(0)
            Combo_attrVal.Text = parts(1)
            Pic_attrColor.BackColor = HTMLParser.HTMLStringToColor(parts(1))
        End If
    End Sub

    Private Sub Combo_attrVal_KeyUp(sender As Object, e As KeyEventArgs) Handles Combo_attrVal.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConfirmAttribute()
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        SVG.SelectedPath.RemoveAttribute(Combo_attrName.Text)
        RefreshAttributesList()
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Lb_attributes_MouseDown(sender As Object, e As MouseEventArgs) Handles Lb_attributes.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim index As Integer = Lb_attributes.IndexFromPoint(New Point(e.X, e.Y))
            Lb_attributes.SelectedIndex = index
        End If
    End Sub

    Private Sub But_figDuplicate_Click(sender As Object, e As EventArgs) Handles But_figDuplicate.Click
        Dim newFigs As New List(Of Figure)
        For Each fig As Figure In SVG.GetSelectedFigures
            newFigs.Add(fig.Clone(False))
        Next
        For Each fig As Figure In newFigs
            fig.parent.Add(fig)
        Next
    End Sub

    Private Sub But_pathDuplicate_Click(sender As Object, e As EventArgs) Handles But_pathDuplicate.Click
        Dim newPaths As New List(Of SVGPath)
        For Each path As SVGPath In SVG.selectedPaths
            newPaths.Add(path.Clone(False))
        Next
        For Each path As SVGPath In newPaths
            SVG.AddPath(path)
        Next
    End Sub

    Private Sub But_hideMain_Click(sender As Object, e As EventArgs) Handles But_hideMain.Click
        If Pan_main.Visible Then
            Pan_main.Hide()
            'Pan_main.Width = 50
            Pan_drawArea.Width = Me.ClientSize.Width - Pan_drawArea.Left - (But_hideMain.Width / 2)
            But_hideMain.BackgroundImage = My.Resources.show_left
        Else
            Pan_main.Show()
            'Pan_main.Width = 50
            Pan_drawArea.Width = Me.ClientSize.Width - Pan_drawArea.Left - Pan_main.Width - 2
            But_hideMain.BackgroundImage = My.Resources.hide_right
        End If

        But_hideMain.BringToFront()
    End Sub

    Private Sub But_hideHtml_Click(sender As Object, e As EventArgs) Handles But_hideHtml.Click
        If Pan_html.Visible Then
            Pan_html.Hide()
            'Pan_main.Width = 50
            Pan_drawArea.Height = Me.ClientSize.Height - Pan_drawArea.Top - (But_hideHtml.Height / 2)
            But_hideHtml.BackgroundImage = My.Resources.show_up
        Else
            Pan_html.Show()
            'Pan_main.Width = 50
            Pan_drawArea.Height = Me.ClientSize.Height - Pan_drawArea.Top - Pan_html.Height - 4
            But_hideHtml.BackgroundImage = My.Resources.hide_down
        End If

        But_hideHtml.BringToFront()
    End Sub

    Private Sub But_showGrid_Click(sender As Object, e As EventArgs) Handles But_showGrid.Click
        showGrid = Not showGrid
        HighlightButton(But_showGrid, showGrid)
        Pic_canvas.Refresh()
    End Sub

    Private Sub But_figOpen_Click(sender As Object, e As EventArgs) Handles But_figOpen.Click
        For Each fig As Figure In SVG.GetSelectedFigures
            fig.IsOpen = Not fig.IsOpen
        Next
        RefreshFiguresList()
        Pic_canvas.Refresh()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        'Dim str As String = ""
        'For Each fig As Figure In SVG.GetSelectedFigures
        '    str &= fig.GetString(True)
        'Next
        'My.Computer.Clipboard.SetText(str)
        myClipboard.Clear()

        For Each fig As Figure In SVG.GetSelectedFigures
            myClipboard.Add(fig)
        Next
        myClipboarOp = ClipOp.Copy
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        myClipboard.Clear()

        For Each fig As Figure In SVG.GetSelectedFigures
            myClipboard.Add(fig)
        Next
        myClipboarOp = ClipOp.Cut
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If SVG.SelectedPath Is Nothing Then Return
        For Each fig As Figure In myClipboard
            fig.Clone(True, SVG.SelectedPath)
        Next
        If myClipboarOp = ClipOp.Cut Then
            For Each fig As Figure In myClipboard
                fig.DeleteSelf()
            Next
        End If
    End Sub

    Private Sub CenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CenterToolStripMenuItem.Click
        Dim bounds As RectangleF = SVG.GetBounds()
        Dim bcenter As New PointF(bounds.Width / 2 + bounds.X, bounds.Height / 2 + bounds.Y)
        Dim cvcenter As New PointF(SVG.CanvasSize.Width / 2, SVG.CanvasSize.Height / 2)

        SVG.Offset(cvcenter.X - bcenter.X, cvcenter.Y - bcenter.Y)
        Pic_canvas.Invalidate()
    End Sub

    Private Sub Form_main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        SetSettingValue(My.Settings.windowSize, Me.Size)
    End Sub

    Private Sub SetSettingValue(ByRef settng As Object, val As Object)
        If initializing = False Then
            settng = val
            SaveSettingsAsync()
        End If
    End Sub

    Private Sub Combo_attrName_TextChanged(sender As Object, e As EventArgs) Handles Combo_attrName.TextChanged
        If Combo_attrName.Text.Contains("=") Then
            Dim str As String() = Split(Combo_attrName.Text, "=")
            If str.Length > 1 Then
                Combo_attrName.Text = str(0)
                Combo_attrVal.Text = str(1).Replace("""", "")
                'Focus on value and move carret to the end
                Combo_attrVal.Focus()
                Combo_attrVal.SelectionStart = Combo_attrVal.Text.Length
                Combo_attrVal.SelectionLength = 0
            End If
        End If
    End Sub

    Private Sub Cb_noHV_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_noHV.CheckedChanged
        noHV = Cb_noHV.Checked
        Pic_canvas.Refresh()
    End Sub

    Private Sub But_showBigGrid_Click(sender As Object, e As EventArgs) Handles But_showBigGrid.Click
        showBigGrid = Not showBigGrid
        HighlightButton(But_showBigGrid, showBigGrid)
        Pic_canvas.Refresh()
    End Sub

    Private Sub RoundPositionsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RoundPositionsToolStripMenuItem1.Click
        For Each pp As PathPoint In SVG.GetAllPPoints
            pp.RoundPosition()
        Next
        Pic_canvas.Refresh()
    End Sub

    Private Sub But_figHide_Click(sender As Object, e As EventArgs) Handles But_figHide.Click
        Dim newVal As Boolean = Not SVG.SelectedPath.selectedFigures(0).IsVisible

        Lb_figures.Tag = LBLockMode.SVG
        For Each fig As Figure In SVG.SelectedPath.selectedFigures
            fig.IsVisible = newVal
            Lb_figures.Items.Item(fig.GetIndex()) = MakeFigureName(fig)
        Next
        For Each fig As Figure In SVG.SelectedPath.selectedFigures
            Lb_figures.SelectedIndices.Add(fig.GetIndex)
        Next
        Lb_figures.Tag = LBLockMode.None

        Pic_canvas.Refresh()
    End Sub

    Private Sub But_pathHide_Click(sender As Object, e As EventArgs) Handles But_pathHide.Click
        Dim newVal As Boolean = Not SVG.SelectedPath.IsVisible

        Lb_paths.Tag = LBLockMode.SVG
        For Each path In SVG.selectedPaths
            path.IsVisible = newVal
            Lb_paths.Items.Item(path.GetIndex()) = MakePathName(path)
        Next
        For Each path In SVG.selectedPaths
            Lb_paths.SelectedIndices.Add(path.GetIndex)
        Next
        Lb_paths.Tag = LBLockMode.None

        Pic_canvas.Refresh()
    End Sub
End Class
