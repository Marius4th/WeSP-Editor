﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_main
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_main))
        Me.Lab_sizeH = New System.Windows.Forms.Label()
        Me.Lab_mposY = New System.Windows.Forms.Label()
        Me.Lab_sizeW = New System.Windows.Forms.Label()
        Me.Lab_mposX = New System.Windows.Forms.Label()
        Me.Lb_selPoints = New System.Windows.Forms.ListBox()
        Me.Context_selPoints = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeselectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.DelteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecentFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoadBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowRealSizePreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SVGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTo00ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RoundPositionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CropToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScaleToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveTo00ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RoundPositionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScaleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FigureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateFigureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DuplicateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.FlipHorizontallyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FlipVerticallyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScaleToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TESTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddLotsOPointsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pan_tools = New System.Windows.Forms.Panel()
        Me.But_movement = New System.Windows.Forms.Button()
        Me.But_closePath = New System.Windows.Forms.Button()
        Me.But_elliArc = New System.Windows.Forms.Button()
        Me.But_smoothBezier = New System.Windows.Forms.Button()
        Me.But_bezier = New System.Windows.Forms.Button()
        Me.But_smoothCurveto = New System.Windows.Forms.Button()
        Me.But_curveto = New System.Windows.Forms.Button()
        Me.But_vertLineto = New System.Windows.Forms.Button()
        Me.But_horLineto = New System.Windows.Forms.Button()
        Me.But_lineto = New System.Windows.Forms.Button()
        Me.But_moveto = New System.Windows.Forms.Button()
        Me.But_selection = New System.Windows.Forms.Button()
        Me.Pan_toggles = New System.Windows.Forms.Panel()
        Me.But_showBigGrid = New System.Windows.Forms.Button()
        Me.But_showGrid = New System.Windows.Forms.Button()
        Me.But_mirror = New System.Windows.Forms.Button()
        Me.But_mirrorVert = New System.Windows.Forms.Button()
        Me.But_mirrorHor = New System.Windows.Forms.Button()
        Me.But_showPoints = New System.Windows.Forms.Button()
        Me.But_placeBetClosest = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Lab_zoomedH = New System.Windows.Forms.Label()
        Me.Lab_zoomedW = New System.Windows.Forms.Label()
        Me.Lab_lastBkp = New System.Windows.Forms.Label()
        Me.Lab_zoom = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Lb_attributes = New System.Windows.Forms.ListBox()
        Me.Context_attributes = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Combo_attrVal = New System.Windows.Forms.ComboBox()
        Me.Combo_attrName = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.But_attrOk = New System.Windows.Forms.Button()
        Me.Pic_attrColor = New System.Windows.Forms.PictureBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Num_decimals = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Num_stickyGHeight = New System.Windows.Forms.NumericUpDown()
        Me.Num_stikyGWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Num_gridHeight = New System.Windows.Forms.NumericUpDown()
        Me.Num_gridWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Num_zoom = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Num_canvasHeight = New System.Windows.Forms.NumericUpDown()
        Me.Num_canvasWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.But_bkgTempFill = New System.Windows.Forms.Button()
        Me.But_bkgTempCenter = New System.Windows.Forms.Button()
        Me.Cb_templateVisible = New System.Windows.Forms.CheckBox()
        Me.Cb_templateKeepAspect = New System.Windows.Forms.CheckBox()
        Me.Num_templateH = New System.Windows.Forms.NumericUpDown()
        Me.Num_templateW = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Num_templateY = New System.Windows.Forms.NumericUpDown()
        Me.Num_templateX = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Combo_templates = New System.Windows.Forms.ComboBox()
        Me.But_addTemplate = New System.Windows.Forms.Button()
        Me.But_removeTemplate = New System.Windows.Forms.Button()
        Me.Tb_html = New System.Windows.Forms.TextBox()
        Me.Pan_html = New System.Windows.Forms.GroupBox()
        Me.Cb_htmlWrap = New System.Windows.Forms.CheckBox()
        Me.Cb_optimize = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Pic_preview = New System.Windows.Forms.PictureBox()
        Me.Cb_noHV = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Lb_figures = New System.Windows.Forms.ListBox()
        Me.Context_figures = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Lb_paths = New System.Windows.Forms.ListBox()
        Me.Timer_autoBackup = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_refresh = New System.Windows.Forms.Timer(Me.components)
        Me.VScroll_canvasY = New System.Windows.Forms.VScrollBar()
        Me.HScroll_canvasX = New System.Windows.Forms.HScrollBar()
        Me.Pan_main = New System.Windows.Forms.Panel()
        Me.But_figHide = New System.Windows.Forms.Button()
        Me.But_pathHide = New System.Windows.Forms.Button()
        Me.But_figOpen = New System.Windows.Forms.Button()
        Me.But_pathDuplicate = New System.Windows.Forms.Button()
        Me.But_removeFigure = New System.Windows.Forms.Button()
        Me.But_figDuplicate = New System.Windows.Forms.Button()
        Me.But_addFigure = New System.Windows.Forms.Button()
        Me.But_removeSelPts = New System.Windows.Forms.Button()
        Me.But_figMoveBottom = New System.Windows.Forms.Button()
        Me.But_pathRename = New System.Windows.Forms.Button()
        Me.But_figMoveTop = New System.Windows.Forms.Button()
        Me.But_pathMoveUp = New System.Windows.Forms.Button()
        Me.But_figMoveDown = New System.Windows.Forms.Button()
        Me.But_pathMoveDown = New System.Windows.Forms.Button()
        Me.But_figMoveUp = New System.Windows.Forms.Button()
        Me.But_pathMoveTop = New System.Windows.Forms.Button()
        Me.But_removePath = New System.Windows.Forms.Button()
        Me.But_pathMoveBottom = New System.Windows.Forms.Button()
        Me.But_addPath = New System.Windows.Forms.Button()
        Me.Pan_drawArea = New System.Windows.Forms.Panel()
        Me.Pic_canvas = New System.Windows.Forms.PictureBox()
        Me.But_hideHtml = New System.Windows.Forms.Button()
        Me.But_hideMain = New System.Windows.Forms.Button()
        Me.Pan_all = New System.Windows.Forms.Panel()
        Me.Context_selPoints.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Pan_tools.SuspendLayout()
        Me.Pan_toggles.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.Context_attributes.SuspendLayout()
        CType(Me.Pic_attrColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Num_decimals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_stickyGHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_stikyGWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_gridHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_gridWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_zoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_canvasHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_canvasWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Num_templateH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_templateW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_templateY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_templateX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_html.SuspendLayout()
        CType(Me.Pic_preview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Context_figures.SuspendLayout()
        Me.Pan_main.SuspendLayout()
        Me.Pan_drawArea.SuspendLayout()
        CType(Me.Pic_canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_all.SuspendLayout()
        Me.SuspendLayout()
        '
        'Lab_sizeH
        '
        resources.ApplyResources(Me.Lab_sizeH, "Lab_sizeH")
        Me.Lab_sizeH.Name = "Lab_sizeH"
        Me.Lab_sizeH.Tag = "Drawing H: "
        '
        'Lab_mposY
        '
        resources.ApplyResources(Me.Lab_mposY, "Lab_mposY")
        Me.Lab_mposY.Name = "Lab_mposY"
        Me.Lab_mposY.Tag = "Mouse Y: "
        '
        'Lab_sizeW
        '
        resources.ApplyResources(Me.Lab_sizeW, "Lab_sizeW")
        Me.Lab_sizeW.Name = "Lab_sizeW"
        Me.Lab_sizeW.Tag = "Drawing W: "
        '
        'Lab_mposX
        '
        resources.ApplyResources(Me.Lab_mposX, "Lab_mposX")
        Me.Lab_mposX.Name = "Lab_mposX"
        Me.Lab_mposX.Tag = "Mouse X: "
        '
        'Lb_selPoints
        '
        resources.ApplyResources(Me.Lb_selPoints, "Lb_selPoints")
        Me.Lb_selPoints.ContextMenuStrip = Me.Context_selPoints
        Me.Lb_selPoints.FormattingEnabled = True
        Me.Lb_selPoints.Name = "Lb_selPoints"
        Me.Lb_selPoints.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.Lb_selPoints.Tag = "0"
        '
        'Context_selPoints
        '
        Me.Context_selPoints.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeselectToolStripMenuItem, Me.ToolStripSeparator11, Me.DelteToolStripMenuItem})
        Me.Context_selPoints.Name = "Context_selPoints"
        resources.ApplyResources(Me.Context_selPoints, "Context_selPoints")
        '
        'DeselectToolStripMenuItem
        '
        Me.DeselectToolStripMenuItem.Name = "DeselectToolStripMenuItem"
        resources.ApplyResources(Me.DeselectToolStripMenuItem, "DeselectToolStripMenuItem")
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        '
        'DelteToolStripMenuItem
        '
        Me.DelteToolStripMenuItem.Name = "DelteToolStripMenuItem"
        resources.ApplyResources(Me.DelteToolStripMenuItem, "DelteToolStripMenuItem")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.SVGToolStripMenuItem, Me.PathToolStripMenuItem, Me.FigureToolStripMenuItem, Me.HelpToolStripMenuItem, Me.TESTToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripSeparator13, Me.LoadToolStripMenuItem, Me.RecentFilesToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripSeparator12, Me.LoadBackupToolStripMenuItem, Me.ToolStripSeparator7, Me.ExportAsToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        resources.ApplyResources(Me.NewToolStripMenuItem, "NewToolStripMenuItem")
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        resources.ApplyResources(Me.ToolStripSeparator13, "ToolStripSeparator13")
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        resources.ApplyResources(Me.LoadToolStripMenuItem, "LoadToolStripMenuItem")
        '
        'RecentFilesToolStripMenuItem
        '
        Me.RecentFilesToolStripMenuItem.Name = "RecentFilesToolStripMenuItem"
        resources.ApplyResources(Me.RecentFilesToolStripMenuItem, "RecentFilesToolStripMenuItem")
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        resources.ApplyResources(Me.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem")
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        resources.ApplyResources(Me.ToolStripSeparator12, "ToolStripSeparator12")
        '
        'LoadBackupToolStripMenuItem
        '
        Me.LoadBackupToolStripMenuItem.Name = "LoadBackupToolStripMenuItem"
        resources.ApplyResources(Me.LoadBackupToolStripMenuItem, "LoadBackupToolStripMenuItem")
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        '
        'ExportAsToolStripMenuItem
        '
        Me.ExportAsToolStripMenuItem.Name = "ExportAsToolStripMenuItem"
        resources.ApplyResources(Me.ExportAsToolStripMenuItem, "ExportAsToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.ToolStripSeparator9, Me.DeleteToolStripMenuItem2, Me.ToolStripSeparator8, Me.SelectAllToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        resources.ApplyResources(Me.UndoToolStripMenuItem, "UndoToolStripMenuItem")
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        resources.ApplyResources(Me.RedoToolStripMenuItem, "RedoToolStripMenuItem")
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        resources.ApplyResources(Me.ToolStripSeparator9, "ToolStripSeparator9")
        '
        'DeleteToolStripMenuItem2
        '
        Me.DeleteToolStripMenuItem2.Name = "DeleteToolStripMenuItem2"
        resources.ApplyResources(Me.DeleteToolStripMenuItem2, "DeleteToolStripMenuItem2")
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        resources.ApplyResources(Me.ToolStripSeparator8, "ToolStripSeparator8")
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        resources.ApplyResources(Me.SelectAllToolStripMenuItem, "SelectAllToolStripMenuItem")
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowRealSizePreviewToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolStripMenuItem, "ViewToolStripMenuItem")
        '
        'ShowRealSizePreviewToolStripMenuItem
        '
        Me.ShowRealSizePreviewToolStripMenuItem.Name = "ShowRealSizePreviewToolStripMenuItem"
        resources.ApplyResources(Me.ShowRealSizePreviewToolStripMenuItem, "ShowRealSizePreviewToolStripMenuItem")
        '
        'SVGToolStripMenuItem
        '
        Me.SVGToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveTo00ToolStripMenuItem1, Me.CenterToolStripMenuItem, Me.RoundPositionsToolStripMenuItem1, Me.CropToolStripMenuItem, Me.ScaleToolStripMenuItem2, Me.ToolStripSeparator5, Me.ClearToolStripMenuItem2})
        Me.SVGToolStripMenuItem.Name = "SVGToolStripMenuItem"
        resources.ApplyResources(Me.SVGToolStripMenuItem, "SVGToolStripMenuItem")
        '
        'MoveTo00ToolStripMenuItem1
        '
        Me.MoveTo00ToolStripMenuItem1.Name = "MoveTo00ToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTo00ToolStripMenuItem1, "MoveTo00ToolStripMenuItem1")
        '
        'CenterToolStripMenuItem
        '
        Me.CenterToolStripMenuItem.Name = "CenterToolStripMenuItem"
        resources.ApplyResources(Me.CenterToolStripMenuItem, "CenterToolStripMenuItem")
        '
        'RoundPositionsToolStripMenuItem1
        '
        Me.RoundPositionsToolStripMenuItem1.Name = "RoundPositionsToolStripMenuItem1"
        resources.ApplyResources(Me.RoundPositionsToolStripMenuItem1, "RoundPositionsToolStripMenuItem1")
        '
        'CropToolStripMenuItem
        '
        Me.CropToolStripMenuItem.Name = "CropToolStripMenuItem"
        resources.ApplyResources(Me.CropToolStripMenuItem, "CropToolStripMenuItem")
        '
        'ScaleToolStripMenuItem2
        '
        Me.ScaleToolStripMenuItem2.Name = "ScaleToolStripMenuItem2"
        resources.ApplyResources(Me.ScaleToolStripMenuItem2, "ScaleToolStripMenuItem2")
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'ClearToolStripMenuItem2
        '
        Me.ClearToolStripMenuItem2.Name = "ClearToolStripMenuItem2"
        resources.ApplyResources(Me.ClearToolStripMenuItem2, "ClearToolStripMenuItem2")
        '
        'PathToolStripMenuItem
        '
        Me.PathToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripSeparator4, Me.MoveTo00ToolStripMenuItem, Me.RoundPositionsToolStripMenuItem, Me.ScaleToolStripMenuItem, Me.ToolStripSeparator2, Me.ClearToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.PathToolStripMenuItem.Name = "PathToolStripMenuItem"
        resources.ApplyResources(Me.PathToolStripMenuItem, "PathToolStripMenuItem")
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'MoveTo00ToolStripMenuItem
        '
        Me.MoveTo00ToolStripMenuItem.Name = "MoveTo00ToolStripMenuItem"
        resources.ApplyResources(Me.MoveTo00ToolStripMenuItem, "MoveTo00ToolStripMenuItem")
        '
        'RoundPositionsToolStripMenuItem
        '
        Me.RoundPositionsToolStripMenuItem.Name = "RoundPositionsToolStripMenuItem"
        resources.ApplyResources(Me.RoundPositionsToolStripMenuItem, "RoundPositionsToolStripMenuItem")
        '
        'ScaleToolStripMenuItem
        '
        Me.ScaleToolStripMenuItem.Name = "ScaleToolStripMenuItem"
        resources.ApplyResources(Me.ScaleToolStripMenuItem, "ScaleToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        resources.ApplyResources(Me.ClearToolStripMenuItem, "ClearToolStripMenuItem")
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        resources.ApplyResources(Me.DeleteToolStripMenuItem, "DeleteToolStripMenuItem")
        '
        'FigureToolStripMenuItem
        '
        Me.FigureToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateFigureToolStripMenuItem, Me.DuplicateToolStripMenuItem, Me.ToolStripSeparator10, Me.FlipHorizontallyToolStripMenuItem, Me.FlipVerticallyToolStripMenuItem, Me.ScaleToolStripMenuItem1, Me.ToolStripSeparator3, Me.ClearToolStripMenuItem1, Me.DeleteToolStripMenuItem1})
        Me.FigureToolStripMenuItem.Name = "FigureToolStripMenuItem"
        resources.ApplyResources(Me.FigureToolStripMenuItem, "FigureToolStripMenuItem")
        '
        'CreateFigureToolStripMenuItem
        '
        Me.CreateFigureToolStripMenuItem.Name = "CreateFigureToolStripMenuItem"
        resources.ApplyResources(Me.CreateFigureToolStripMenuItem, "CreateFigureToolStripMenuItem")
        '
        'DuplicateToolStripMenuItem
        '
        Me.DuplicateToolStripMenuItem.Name = "DuplicateToolStripMenuItem"
        resources.ApplyResources(Me.DuplicateToolStripMenuItem, "DuplicateToolStripMenuItem")
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        resources.ApplyResources(Me.ToolStripSeparator10, "ToolStripSeparator10")
        '
        'FlipHorizontallyToolStripMenuItem
        '
        Me.FlipHorizontallyToolStripMenuItem.Name = "FlipHorizontallyToolStripMenuItem"
        resources.ApplyResources(Me.FlipHorizontallyToolStripMenuItem, "FlipHorizontallyToolStripMenuItem")
        '
        'FlipVerticallyToolStripMenuItem
        '
        Me.FlipVerticallyToolStripMenuItem.Name = "FlipVerticallyToolStripMenuItem"
        resources.ApplyResources(Me.FlipVerticallyToolStripMenuItem, "FlipVerticallyToolStripMenuItem")
        '
        'ScaleToolStripMenuItem1
        '
        Me.ScaleToolStripMenuItem1.Name = "ScaleToolStripMenuItem1"
        resources.ApplyResources(Me.ScaleToolStripMenuItem1, "ScaleToolStripMenuItem1")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'ClearToolStripMenuItem1
        '
        Me.ClearToolStripMenuItem1.Name = "ClearToolStripMenuItem1"
        resources.ApplyResources(Me.ClearToolStripMenuItem1, "ClearToolStripMenuItem1")
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        resources.ApplyResources(Me.DeleteToolStripMenuItem1, "DeleteToolStripMenuItem1")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewHelpToolStripMenuItem, Me.ToolStripSeparator6, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'ViewHelpToolStripMenuItem
        '
        Me.ViewHelpToolStripMenuItem.Name = "ViewHelpToolStripMenuItem"
        resources.ApplyResources(Me.ViewHelpToolStripMenuItem, "ViewHelpToolStripMenuItem")
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'TESTToolStripMenuItem
        '
        Me.TESTToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddLotsOPointsToolStripMenuItem})
        Me.TESTToolStripMenuItem.Name = "TESTToolStripMenuItem"
        resources.ApplyResources(Me.TESTToolStripMenuItem, "TESTToolStripMenuItem")
        '
        'AddLotsOPointsToolStripMenuItem
        '
        Me.AddLotsOPointsToolStripMenuItem.Name = "AddLotsOPointsToolStripMenuItem"
        resources.ApplyResources(Me.AddLotsOPointsToolStripMenuItem, "AddLotsOPointsToolStripMenuItem")
        '
        'Pan_tools
        '
        resources.ApplyResources(Me.Pan_tools, "Pan_tools")
        Me.Pan_tools.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Pan_tools.Controls.Add(Me.But_movement)
        Me.Pan_tools.Controls.Add(Me.But_closePath)
        Me.Pan_tools.Controls.Add(Me.But_elliArc)
        Me.Pan_tools.Controls.Add(Me.But_smoothBezier)
        Me.Pan_tools.Controls.Add(Me.But_bezier)
        Me.Pan_tools.Controls.Add(Me.But_smoothCurveto)
        Me.Pan_tools.Controls.Add(Me.But_curveto)
        Me.Pan_tools.Controls.Add(Me.But_vertLineto)
        Me.Pan_tools.Controls.Add(Me.But_horLineto)
        Me.Pan_tools.Controls.Add(Me.But_lineto)
        Me.Pan_tools.Controls.Add(Me.But_moveto)
        Me.Pan_tools.Controls.Add(Me.But_selection)
        Me.Pan_tools.Name = "Pan_tools"
        '
        'But_movement
        '
        Me.But_movement.BackColor = System.Drawing.Color.Snow
        Me.But_movement.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.move
        resources.ApplyResources(Me.But_movement, "But_movement")
        Me.But_movement.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_movement.Name = "But_movement"
        Me.But_movement.UseVisualStyleBackColor = False
        '
        'But_closePath
        '
        Me.But_closePath.BackColor = System.Drawing.Color.Snow
        Me.But_closePath.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.closePath
        resources.ApplyResources(Me.But_closePath, "But_closePath")
        Me.But_closePath.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_closePath.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_closePath.Name = "But_closePath"
        Me.But_closePath.UseVisualStyleBackColor = False
        '
        'But_elliArc
        '
        Me.But_elliArc.BackColor = System.Drawing.Color.Snow
        Me.But_elliArc.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.ellipticalArc
        resources.ApplyResources(Me.But_elliArc, "But_elliArc")
        Me.But_elliArc.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_elliArc.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_elliArc.Name = "But_elliArc"
        Me.But_elliArc.UseVisualStyleBackColor = False
        '
        'But_smoothBezier
        '
        Me.But_smoothBezier.BackColor = System.Drawing.Color.DarkOliveGreen
        resources.ApplyResources(Me.But_smoothBezier, "But_smoothBezier")
        Me.But_smoothBezier.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_smoothBezier.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_smoothBezier.Name = "But_smoothBezier"
        Me.But_smoothBezier.UseVisualStyleBackColor = False
        '
        'But_bezier
        '
        Me.But_bezier.BackColor = System.Drawing.Color.Snow
        Me.But_bezier.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.bezier
        resources.ApplyResources(Me.But_bezier, "But_bezier")
        Me.But_bezier.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_bezier.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_bezier.Name = "But_bezier"
        Me.But_bezier.UseVisualStyleBackColor = False
        '
        'But_smoothCurveto
        '
        Me.But_smoothCurveto.BackColor = System.Drawing.Color.DarkOliveGreen
        resources.ApplyResources(Me.But_smoothCurveto, "But_smoothCurveto")
        Me.But_smoothCurveto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_smoothCurveto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_smoothCurveto.Name = "But_smoothCurveto"
        Me.But_smoothCurveto.UseVisualStyleBackColor = False
        '
        'But_curveto
        '
        Me.But_curveto.BackColor = System.Drawing.Color.Snow
        Me.But_curveto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.curveto
        resources.ApplyResources(Me.But_curveto, "But_curveto")
        Me.But_curveto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_curveto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_curveto.Name = "But_curveto"
        Me.But_curveto.UseVisualStyleBackColor = False
        '
        'But_vertLineto
        '
        Me.But_vertLineto.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.But_vertLineto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.vertLineto
        resources.ApplyResources(Me.But_vertLineto, "But_vertLineto")
        Me.But_vertLineto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_vertLineto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_vertLineto.Name = "But_vertLineto"
        Me.But_vertLineto.UseVisualStyleBackColor = False
        '
        'But_horLineto
        '
        Me.But_horLineto.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.But_horLineto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.horLineto
        resources.ApplyResources(Me.But_horLineto, "But_horLineto")
        Me.But_horLineto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_horLineto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_horLineto.Name = "But_horLineto"
        Me.But_horLineto.UseVisualStyleBackColor = False
        '
        'But_lineto
        '
        Me.But_lineto.BackColor = System.Drawing.Color.Snow
        Me.But_lineto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.lineto
        resources.ApplyResources(Me.But_lineto, "But_lineto")
        Me.But_lineto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_lineto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_lineto.Name = "But_lineto"
        Me.But_lineto.UseVisualStyleBackColor = False
        '
        'But_moveto
        '
        Me.But_moveto.BackColor = System.Drawing.Color.Snow
        Me.But_moveto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveto
        resources.ApplyResources(Me.But_moveto, "But_moveto")
        Me.But_moveto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_moveto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_moveto.Name = "But_moveto"
        Me.But_moveto.UseVisualStyleBackColor = False
        '
        'But_selection
        '
        Me.But_selection.BackColor = System.Drawing.Color.Snow
        Me.But_selection.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.selection
        resources.ApplyResources(Me.But_selection, "But_selection")
        Me.But_selection.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_selection.Name = "But_selection"
        Me.But_selection.UseVisualStyleBackColor = False
        '
        'Pan_toggles
        '
        resources.ApplyResources(Me.Pan_toggles, "Pan_toggles")
        Me.Pan_toggles.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Pan_toggles.Controls.Add(Me.But_showBigGrid)
        Me.Pan_toggles.Controls.Add(Me.But_showGrid)
        Me.Pan_toggles.Controls.Add(Me.But_mirror)
        Me.Pan_toggles.Controls.Add(Me.But_mirrorVert)
        Me.Pan_toggles.Controls.Add(Me.But_mirrorHor)
        Me.Pan_toggles.Controls.Add(Me.But_showPoints)
        Me.Pan_toggles.Controls.Add(Me.But_placeBetClosest)
        Me.Pan_toggles.Name = "Pan_toggles"
        '
        'But_showBigGrid
        '
        Me.But_showBigGrid.BackColor = System.Drawing.Color.Snow
        Me.But_showBigGrid.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.grid_central
        resources.ApplyResources(Me.But_showBigGrid, "But_showBigGrid")
        Me.But_showBigGrid.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_showBigGrid.Name = "But_showBigGrid"
        Me.But_showBigGrid.UseVisualStyleBackColor = False
        '
        'But_showGrid
        '
        Me.But_showGrid.BackColor = System.Drawing.Color.Snow
        Me.But_showGrid.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.grid3
        resources.ApplyResources(Me.But_showGrid, "But_showGrid")
        Me.But_showGrid.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_showGrid.Name = "But_showGrid"
        Me.But_showGrid.UseVisualStyleBackColor = False
        '
        'But_mirror
        '
        resources.ApplyResources(Me.But_mirror, "But_mirror")
        Me.But_mirror.BackColor = System.Drawing.Color.Snow
        Me.But_mirror.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.noMirror
        Me.But_mirror.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_mirror.Name = "But_mirror"
        Me.But_mirror.UseVisualStyleBackColor = False
        '
        'But_mirrorVert
        '
        resources.ApplyResources(Me.But_mirrorVert, "But_mirrorVert")
        Me.But_mirrorVert.BackColor = System.Drawing.Color.Snow
        Me.But_mirrorVert.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.mirrorVert
        Me.But_mirrorVert.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_mirrorVert.Name = "But_mirrorVert"
        Me.But_mirrorVert.UseVisualStyleBackColor = False
        '
        'But_mirrorHor
        '
        resources.ApplyResources(Me.But_mirrorHor, "But_mirrorHor")
        Me.But_mirrorHor.BackColor = System.Drawing.Color.Snow
        Me.But_mirrorHor.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.mirrorHor
        Me.But_mirrorHor.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_mirrorHor.Name = "But_mirrorHor"
        Me.But_mirrorHor.UseVisualStyleBackColor = False
        '
        'But_showPoints
        '
        Me.But_showPoints.BackColor = System.Drawing.Color.Snow
        Me.But_showPoints.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.points2
        resources.ApplyResources(Me.But_showPoints, "But_showPoints")
        Me.But_showPoints.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_showPoints.Name = "But_showPoints"
        Me.But_showPoints.UseVisualStyleBackColor = False
        '
        'But_placeBetClosest
        '
        Me.But_placeBetClosest.BackColor = System.Drawing.Color.Snow
        Me.But_placeBetClosest.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.placeBetween1
        resources.ApplyResources(Me.But_placeBetClosest, "But_placeBetClosest")
        Me.But_placeBetClosest.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_placeBetClosest.Name = "But_placeBetClosest"
        Me.But_placeBetClosest.UseVisualStyleBackColor = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.Lab_sizeH)
        Me.TabPage1.Controls.Add(Me.Lab_zoomedH)
        Me.TabPage1.Controls.Add(Me.Lab_sizeW)
        Me.TabPage1.Controls.Add(Me.Lab_mposY)
        Me.TabPage1.Controls.Add(Me.Lab_zoomedW)
        Me.TabPage1.Controls.Add(Me.Lab_lastBkp)
        Me.TabPage1.Controls.Add(Me.Lab_mposX)
        Me.TabPage1.Controls.Add(Me.Lab_zoom)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Lab_zoomedH
        '
        resources.ApplyResources(Me.Lab_zoomedH, "Lab_zoomedH")
        Me.Lab_zoomedH.Name = "Lab_zoomedH"
        Me.Lab_zoomedH.Tag = "Zoomed CH: "
        '
        'Lab_zoomedW
        '
        resources.ApplyResources(Me.Lab_zoomedW, "Lab_zoomedW")
        Me.Lab_zoomedW.Name = "Lab_zoomedW"
        Me.Lab_zoomedW.Tag = "Zoomed CW: "
        '
        'Lab_lastBkp
        '
        resources.ApplyResources(Me.Lab_lastBkp, "Lab_lastBkp")
        Me.Lab_lastBkp.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Lab_lastBkp.Name = "Lab_lastBkp"
        Me.Lab_lastBkp.Tag = "Last Bkp: "
        '
        'Lab_zoom
        '
        resources.ApplyResources(Me.Lab_zoom, "Lab_zoom")
        Me.Lab_zoom.Name = "Lab_zoom"
        Me.Lab_zoom.Tag = "Zoom: "
        '
        'TabPage4
        '
        Me.TabPage4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage4.Controls.Add(Me.Lb_attributes)
        Me.TabPage4.Controls.Add(Me.Combo_attrVal)
        Me.TabPage4.Controls.Add(Me.Combo_attrName)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.But_attrOk)
        Me.TabPage4.Controls.Add(Me.Pic_attrColor)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Lb_attributes
        '
        Me.Lb_attributes.ContextMenuStrip = Me.Context_attributes
        Me.Lb_attributes.FormattingEnabled = True
        resources.ApplyResources(Me.Lb_attributes, "Lb_attributes")
        Me.Lb_attributes.Name = "Lb_attributes"
        '
        'Context_attributes
        '
        Me.Context_attributes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.Context_attributes.Name = "Context_attributes"
        resources.ApplyResources(Me.Context_attributes, "Context_attributes")
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        resources.ApplyResources(Me.RemoveToolStripMenuItem, "RemoveToolStripMenuItem")
        '
        'Combo_attrVal
        '
        Me.Combo_attrVal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Combo_attrVal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Combo_attrVal.FormattingEnabled = True
        resources.ApplyResources(Me.Combo_attrVal, "Combo_attrVal")
        Me.Combo_attrVal.Name = "Combo_attrVal"
        Me.Combo_attrVal.Sorted = True
        '
        'Combo_attrName
        '
        Me.Combo_attrName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Combo_attrName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Combo_attrName.FormattingEnabled = True
        resources.ApplyResources(Me.Combo_attrName, "Combo_attrName")
        Me.Combo_attrName.Name = "Combo_attrName"
        Me.Combo_attrName.Sorted = True
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'But_attrOk
        '
        resources.ApplyResources(Me.But_attrOk, "But_attrOk")
        Me.But_attrOk.BackColor = System.Drawing.Color.Snow
        Me.But_attrOk.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.ok2
        Me.But_attrOk.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_attrOk.Name = "But_attrOk"
        Me.But_attrOk.UseVisualStyleBackColor = False
        '
        'Pic_attrColor
        '
        Me.Pic_attrColor.BackColor = System.Drawing.Color.Gray
        Me.Pic_attrColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.Pic_attrColor, "Pic_attrColor")
        Me.Pic_attrColor.Name = "Pic_attrColor"
        Me.Pic_attrColor.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage2.Controls.Add(Me.Num_decimals)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Num_stickyGHeight)
        Me.TabPage2.Controls.Add(Me.Num_stikyGWidth)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Num_gridHeight)
        Me.TabPage2.Controls.Add(Me.Num_gridWidth)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Num_zoom)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Num_canvasHeight)
        Me.TabPage2.Controls.Add(Me.Num_canvasWidth)
        Me.TabPage2.Controls.Add(Me.Label5)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Num_decimals
        '
        resources.ApplyResources(Me.Num_decimals, "Num_decimals")
        Me.Num_decimals.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Num_decimals.Name = "Num_decimals"
        Me.Num_decimals.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Num_stickyGHeight
        '
        Me.Num_stickyGHeight.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_stickyGHeight, "Num_stickyGHeight")
        Me.Num_stickyGHeight.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_stickyGHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Num_stickyGHeight.Name = "Num_stickyGHeight"
        Me.Num_stickyGHeight.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Num_stikyGWidth
        '
        Me.Num_stikyGWidth.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_stikyGWidth, "Num_stikyGWidth")
        Me.Num_stikyGWidth.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_stikyGWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Num_stikyGWidth.Name = "Num_stikyGWidth"
        Me.Num_stikyGWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Num_gridHeight
        '
        Me.Num_gridHeight.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_gridHeight, "Num_gridHeight")
        Me.Num_gridHeight.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_gridHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_gridHeight.Name = "Num_gridHeight"
        Me.Num_gridHeight.Value = New Decimal(New Integer() {32, 0, 0, 0})
        '
        'Num_gridWidth
        '
        Me.Num_gridWidth.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_gridWidth, "Num_gridWidth")
        Me.Num_gridWidth.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_gridWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_gridWidth.Name = "Num_gridWidth"
        Me.Num_gridWidth.Value = New Decimal(New Integer() {32, 0, 0, 0})
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Num_zoom
        '
        Me.Num_zoom.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_zoom, "Num_zoom")
        Me.Num_zoom.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num_zoom.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Num_zoom.Name = "Num_zoom"
        Me.Num_zoom.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Num_canvasHeight
        '
        Me.Num_canvasHeight.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_canvasHeight, "Num_canvasHeight")
        Me.Num_canvasHeight.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_canvasHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_canvasHeight.Name = "Num_canvasHeight"
        Me.Num_canvasHeight.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Num_canvasWidth
        '
        Me.Num_canvasWidth.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_canvasWidth, "Num_canvasWidth")
        Me.Num_canvasWidth.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_canvasWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_canvasWidth.Name = "Num_canvasWidth"
        Me.Num_canvasWidth.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'TabPage3
        '
        Me.TabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage3.Controls.Add(Me.But_bkgTempFill)
        Me.TabPage3.Controls.Add(Me.But_bkgTempCenter)
        Me.TabPage3.Controls.Add(Me.Cb_templateVisible)
        Me.TabPage3.Controls.Add(Me.Cb_templateKeepAspect)
        Me.TabPage3.Controls.Add(Me.Num_templateH)
        Me.TabPage3.Controls.Add(Me.Num_templateW)
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Controls.Add(Me.Num_templateY)
        Me.TabPage3.Controls.Add(Me.Num_templateX)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.Combo_templates)
        Me.TabPage3.Controls.Add(Me.But_addTemplate)
        Me.TabPage3.Controls.Add(Me.But_removeTemplate)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'But_bkgTempFill
        '
        resources.ApplyResources(Me.But_bkgTempFill, "But_bkgTempFill")
        Me.But_bkgTempFill.Name = "But_bkgTempFill"
        Me.But_bkgTempFill.UseVisualStyleBackColor = True
        '
        'But_bkgTempCenter
        '
        resources.ApplyResources(Me.But_bkgTempCenter, "But_bkgTempCenter")
        Me.But_bkgTempCenter.Name = "But_bkgTempCenter"
        Me.But_bkgTempCenter.UseVisualStyleBackColor = True
        '
        'Cb_templateVisible
        '
        resources.ApplyResources(Me.Cb_templateVisible, "Cb_templateVisible")
        Me.Cb_templateVisible.Checked = True
        Me.Cb_templateVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cb_templateVisible.Name = "Cb_templateVisible"
        Me.Cb_templateVisible.UseVisualStyleBackColor = True
        '
        'Cb_templateKeepAspect
        '
        resources.ApplyResources(Me.Cb_templateKeepAspect, "Cb_templateKeepAspect")
        Me.Cb_templateKeepAspect.Checked = True
        Me.Cb_templateKeepAspect.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cb_templateKeepAspect.Name = "Cb_templateKeepAspect"
        Me.Cb_templateKeepAspect.UseVisualStyleBackColor = True
        '
        'Num_templateH
        '
        resources.ApplyResources(Me.Num_templateH, "Num_templateH")
        Me.Num_templateH.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_templateH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_templateH.Name = "Num_templateH"
        Me.Num_templateH.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Num_templateW
        '
        resources.ApplyResources(Me.Num_templateW, "Num_templateW")
        Me.Num_templateW.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_templateW.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_templateW.Name = "Num_templateW"
        Me.Num_templateW.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'Num_templateY
        '
        Me.Num_templateY.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_templateY, "Num_templateY")
        Me.Num_templateY.Maximum = New Decimal(New Integer() {9000, 0, 0, 0})
        Me.Num_templateY.Minimum = New Decimal(New Integer() {9000, 0, 0, -2147483648})
        Me.Num_templateY.Name = "Num_templateY"
        '
        'Num_templateX
        '
        Me.Num_templateX.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_templateX, "Num_templateX")
        Me.Num_templateX.Maximum = New Decimal(New Integer() {9000, 0, 0, 0})
        Me.Num_templateX.Minimum = New Decimal(New Integer() {9000, 0, 0, -2147483648})
        Me.Num_templateX.Name = "Num_templateX"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Combo_templates
        '
        Me.Combo_templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Combo_templates.FormattingEnabled = True
        resources.ApplyResources(Me.Combo_templates, "Combo_templates")
        Me.Combo_templates.Name = "Combo_templates"
        '
        'But_addTemplate
        '
        Me.But_addTemplate.BackColor = System.Drawing.Color.Snow
        Me.But_addTemplate.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.add
        resources.ApplyResources(Me.But_addTemplate, "But_addTemplate")
        Me.But_addTemplate.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_addTemplate.Name = "But_addTemplate"
        Me.But_addTemplate.UseVisualStyleBackColor = False
        '
        'But_removeTemplate
        '
        Me.But_removeTemplate.BackColor = System.Drawing.Color.Snow
        Me.But_removeTemplate.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.remove
        resources.ApplyResources(Me.But_removeTemplate, "But_removeTemplate")
        Me.But_removeTemplate.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_removeTemplate.Name = "But_removeTemplate"
        Me.But_removeTemplate.UseVisualStyleBackColor = False
        '
        'Tb_html
        '
        resources.ApplyResources(Me.Tb_html, "Tb_html")
        Me.Tb_html.Name = "Tb_html"
        '
        'Pan_html
        '
        resources.ApplyResources(Me.Pan_html, "Pan_html")
        Me.Pan_html.Controls.Add(Me.Cb_htmlWrap)
        Me.Pan_html.Controls.Add(Me.Cb_optimize)
        Me.Pan_html.Controls.Add(Me.Button1)
        Me.Pan_html.Controls.Add(Me.Pic_preview)
        Me.Pan_html.Controls.Add(Me.Tb_html)
        Me.Pan_html.Controls.Add(Me.Cb_noHV)
        Me.Pan_html.Name = "Pan_html"
        Me.Pan_html.TabStop = False
        '
        'Cb_htmlWrap
        '
        resources.ApplyResources(Me.Cb_htmlWrap, "Cb_htmlWrap")
        Me.Cb_htmlWrap.Checked = True
        Me.Cb_htmlWrap.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cb_htmlWrap.Name = "Cb_htmlWrap"
        Me.Cb_htmlWrap.UseVisualStyleBackColor = True
        '
        'Cb_optimize
        '
        resources.ApplyResources(Me.Cb_optimize, "Cb_optimize")
        Me.Cb_optimize.Checked = True
        Me.Cb_optimize.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cb_optimize.Name = "Cb_optimize"
        Me.Cb_optimize.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Pic_preview
        '
        resources.ApplyResources(Me.Pic_preview, "Pic_preview")
        Me.Pic_preview.BackColor = System.Drawing.Color.Black
        Me.Pic_preview.Name = "Pic_preview"
        Me.Pic_preview.TabStop = False
        '
        'Cb_noHV
        '
        resources.ApplyResources(Me.Cb_noHV, "Cb_noHV")
        Me.Cb_noHV.Name = "Cb_noHV"
        Me.Cb_noHV.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Lb_figures
        '
        resources.ApplyResources(Me.Lb_figures, "Lb_figures")
        Me.Lb_figures.ContextMenuStrip = Me.Context_figures
        Me.Lb_figures.FormattingEnabled = True
        Me.Lb_figures.Name = "Lb_figures"
        Me.Lb_figures.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.Lb_figures.Tag = "0"
        '
        'Context_figures
        '
        Me.Context_figures.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem})
        Me.Context_figures.Name = "Context_figures"
        resources.ApplyResources(Me.Context_figures, "Context_figures")
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        resources.ApplyResources(Me.CutToolStripMenuItem, "CutToolStripMenuItem")
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        resources.ApplyResources(Me.CopyToolStripMenuItem, "CopyToolStripMenuItem")
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        resources.ApplyResources(Me.PasteToolStripMenuItem, "PasteToolStripMenuItem")
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "png"
        Me.SaveFileDialog1.FileName = "vector"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Lb_paths
        '
        resources.ApplyResources(Me.Lb_paths, "Lb_paths")
        Me.Lb_paths.FormattingEnabled = True
        Me.Lb_paths.Name = "Lb_paths"
        Me.Lb_paths.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.Lb_paths.Tag = "0"
        '
        'Timer_autoBackup
        '
        Me.Timer_autoBackup.Enabled = True
        Me.Timer_autoBackup.Interval = 60000
        '
        'Timer_refresh
        '
        Me.Timer_refresh.Enabled = True
        Me.Timer_refresh.Interval = 1000
        '
        'VScroll_canvasY
        '
        resources.ApplyResources(Me.VScroll_canvasY, "VScroll_canvasY")
        Me.VScroll_canvasY.Name = "VScroll_canvasY"
        '
        'HScroll_canvasX
        '
        resources.ApplyResources(Me.HScroll_canvasX, "HScroll_canvasX")
        Me.HScroll_canvasX.Name = "HScroll_canvasX"
        '
        'Pan_main
        '
        resources.ApplyResources(Me.Pan_main, "Pan_main")
        Me.Pan_main.Controls.Add(Me.But_figHide)
        Me.Pan_main.Controls.Add(Me.But_pathHide)
        Me.Pan_main.Controls.Add(Me.But_figOpen)
        Me.Pan_main.Controls.Add(Me.But_pathDuplicate)
        Me.Pan_main.Controls.Add(Me.TabControl1)
        Me.Pan_main.Controls.Add(Me.But_removeFigure)
        Me.Pan_main.Controls.Add(Me.But_figDuplicate)
        Me.Pan_main.Controls.Add(Me.Label4)
        Me.Pan_main.Controls.Add(Me.But_addFigure)
        Me.Pan_main.Controls.Add(Me.But_removeSelPts)
        Me.Pan_main.Controls.Add(Me.Lb_figures)
        Me.Pan_main.Controls.Add(Me.But_figMoveBottom)
        Me.Pan_main.Controls.Add(Me.But_pathRename)
        Me.Pan_main.Controls.Add(Me.Label1)
        Me.Pan_main.Controls.Add(Me.But_figMoveTop)
        Me.Pan_main.Controls.Add(Me.But_pathMoveUp)
        Me.Pan_main.Controls.Add(Me.Label3)
        Me.Pan_main.Controls.Add(Me.But_figMoveDown)
        Me.Pan_main.Controls.Add(Me.But_pathMoveDown)
        Me.Pan_main.Controls.Add(Me.Lb_selPoints)
        Me.Pan_main.Controls.Add(Me.But_figMoveUp)
        Me.Pan_main.Controls.Add(Me.But_pathMoveTop)
        Me.Pan_main.Controls.Add(Me.Lb_paths)
        Me.Pan_main.Controls.Add(Me.But_removePath)
        Me.Pan_main.Controls.Add(Me.But_pathMoveBottom)
        Me.Pan_main.Controls.Add(Me.But_addPath)
        Me.Pan_main.Name = "Pan_main"
        '
        'But_figHide
        '
        resources.ApplyResources(Me.But_figHide, "But_figHide")
        Me.But_figHide.BackColor = System.Drawing.Color.Snow
        Me.But_figHide.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.visible
        Me.But_figHide.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figHide.Name = "But_figHide"
        Me.But_figHide.UseVisualStyleBackColor = False
        '
        'But_pathHide
        '
        resources.ApplyResources(Me.But_pathHide, "But_pathHide")
        Me.But_pathHide.BackColor = System.Drawing.Color.Snow
        Me.But_pathHide.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.visible
        Me.But_pathHide.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathHide.Name = "But_pathHide"
        Me.But_pathHide.UseVisualStyleBackColor = False
        '
        'But_figOpen
        '
        resources.ApplyResources(Me.But_figOpen, "But_figOpen")
        Me.But_figOpen.BackColor = System.Drawing.Color.Snow
        Me.But_figOpen.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.open
        Me.But_figOpen.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figOpen.Name = "But_figOpen"
        Me.But_figOpen.UseVisualStyleBackColor = False
        '
        'But_pathDuplicate
        '
        resources.ApplyResources(Me.But_pathDuplicate, "But_pathDuplicate")
        Me.But_pathDuplicate.BackColor = System.Drawing.Color.Snow
        Me.But_pathDuplicate.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.duplicate
        Me.But_pathDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathDuplicate.Name = "But_pathDuplicate"
        Me.But_pathDuplicate.UseVisualStyleBackColor = False
        '
        'But_removeFigure
        '
        resources.ApplyResources(Me.But_removeFigure, "But_removeFigure")
        Me.But_removeFigure.BackColor = System.Drawing.Color.Snow
        Me.But_removeFigure.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.remove
        Me.But_removeFigure.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_removeFigure.Name = "But_removeFigure"
        Me.But_removeFigure.UseVisualStyleBackColor = False
        '
        'But_figDuplicate
        '
        resources.ApplyResources(Me.But_figDuplicate, "But_figDuplicate")
        Me.But_figDuplicate.BackColor = System.Drawing.Color.Snow
        Me.But_figDuplicate.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.duplicate
        Me.But_figDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figDuplicate.Name = "But_figDuplicate"
        Me.But_figDuplicate.UseVisualStyleBackColor = False
        '
        'But_addFigure
        '
        resources.ApplyResources(Me.But_addFigure, "But_addFigure")
        Me.But_addFigure.BackColor = System.Drawing.Color.Snow
        Me.But_addFigure.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.add
        Me.But_addFigure.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_addFigure.Name = "But_addFigure"
        Me.But_addFigure.UseVisualStyleBackColor = False
        '
        'But_removeSelPts
        '
        resources.ApplyResources(Me.But_removeSelPts, "But_removeSelPts")
        Me.But_removeSelPts.BackColor = System.Drawing.Color.Snow
        Me.But_removeSelPts.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.remove
        Me.But_removeSelPts.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_removeSelPts.Name = "But_removeSelPts"
        Me.But_removeSelPts.UseVisualStyleBackColor = False
        '
        'But_figMoveBottom
        '
        resources.ApplyResources(Me.But_figMoveBottom, "But_figMoveBottom")
        Me.But_figMoveBottom.BackColor = System.Drawing.Color.Snow
        Me.But_figMoveBottom.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveBottom
        Me.But_figMoveBottom.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figMoveBottom.Name = "But_figMoveBottom"
        Me.But_figMoveBottom.UseVisualStyleBackColor = False
        '
        'But_pathRename
        '
        resources.ApplyResources(Me.But_pathRename, "But_pathRename")
        Me.But_pathRename.BackColor = System.Drawing.Color.Snow
        Me.But_pathRename.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.rename
        Me.But_pathRename.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathRename.Name = "But_pathRename"
        Me.But_pathRename.UseVisualStyleBackColor = False
        '
        'But_figMoveTop
        '
        resources.ApplyResources(Me.But_figMoveTop, "But_figMoveTop")
        Me.But_figMoveTop.BackColor = System.Drawing.Color.Snow
        Me.But_figMoveTop.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveTop
        Me.But_figMoveTop.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figMoveTop.Name = "But_figMoveTop"
        Me.But_figMoveTop.UseVisualStyleBackColor = False
        '
        'But_pathMoveUp
        '
        resources.ApplyResources(Me.But_pathMoveUp, "But_pathMoveUp")
        Me.But_pathMoveUp.BackColor = System.Drawing.Color.Snow
        Me.But_pathMoveUp.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveUp
        Me.But_pathMoveUp.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathMoveUp.Name = "But_pathMoveUp"
        Me.But_pathMoveUp.UseVisualStyleBackColor = False
        '
        'But_figMoveDown
        '
        resources.ApplyResources(Me.But_figMoveDown, "But_figMoveDown")
        Me.But_figMoveDown.BackColor = System.Drawing.Color.Snow
        Me.But_figMoveDown.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveDown
        Me.But_figMoveDown.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figMoveDown.Name = "But_figMoveDown"
        Me.But_figMoveDown.UseVisualStyleBackColor = False
        '
        'But_pathMoveDown
        '
        resources.ApplyResources(Me.But_pathMoveDown, "But_pathMoveDown")
        Me.But_pathMoveDown.BackColor = System.Drawing.Color.Snow
        Me.But_pathMoveDown.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveDown
        Me.But_pathMoveDown.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathMoveDown.Name = "But_pathMoveDown"
        Me.But_pathMoveDown.UseVisualStyleBackColor = False
        '
        'But_figMoveUp
        '
        resources.ApplyResources(Me.But_figMoveUp, "But_figMoveUp")
        Me.But_figMoveUp.BackColor = System.Drawing.Color.Snow
        Me.But_figMoveUp.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveUp
        Me.But_figMoveUp.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_figMoveUp.Name = "But_figMoveUp"
        Me.But_figMoveUp.UseVisualStyleBackColor = False
        '
        'But_pathMoveTop
        '
        resources.ApplyResources(Me.But_pathMoveTop, "But_pathMoveTop")
        Me.But_pathMoveTop.BackColor = System.Drawing.Color.Snow
        Me.But_pathMoveTop.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveTop
        Me.But_pathMoveTop.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathMoveTop.Name = "But_pathMoveTop"
        Me.But_pathMoveTop.UseVisualStyleBackColor = False
        '
        'But_removePath
        '
        resources.ApplyResources(Me.But_removePath, "But_removePath")
        Me.But_removePath.BackColor = System.Drawing.Color.Snow
        Me.But_removePath.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.remove
        Me.But_removePath.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_removePath.Name = "But_removePath"
        Me.But_removePath.UseVisualStyleBackColor = False
        '
        'But_pathMoveBottom
        '
        resources.ApplyResources(Me.But_pathMoveBottom, "But_pathMoveBottom")
        Me.But_pathMoveBottom.BackColor = System.Drawing.Color.Snow
        Me.But_pathMoveBottom.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.moveBottom
        Me.But_pathMoveBottom.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_pathMoveBottom.Name = "But_pathMoveBottom"
        Me.But_pathMoveBottom.UseVisualStyleBackColor = False
        '
        'But_addPath
        '
        resources.ApplyResources(Me.But_addPath, "But_addPath")
        Me.But_addPath.BackColor = System.Drawing.Color.Snow
        Me.But_addPath.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.add
        Me.But_addPath.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_addPath.Name = "But_addPath"
        Me.But_addPath.UseVisualStyleBackColor = False
        '
        'Pan_drawArea
        '
        resources.ApplyResources(Me.Pan_drawArea, "Pan_drawArea")
        Me.Pan_drawArea.Controls.Add(Me.HScroll_canvasX)
        Me.Pan_drawArea.Controls.Add(Me.VScroll_canvasY)
        Me.Pan_drawArea.Controls.Add(Me.Pic_canvas)
        Me.Pan_drawArea.Controls.Add(Me.Pan_toggles)
        Me.Pan_drawArea.Controls.Add(Me.Pan_tools)
        Me.Pan_drawArea.Name = "Pan_drawArea"
        '
        'Pic_canvas
        '
        resources.ApplyResources(Me.Pic_canvas, "Pic_canvas")
        Me.Pic_canvas.BackColor = System.Drawing.Color.Black
        Me.Pic_canvas.Name = "Pic_canvas"
        Me.Pic_canvas.TabStop = False
        '
        'But_hideHtml
        '
        resources.ApplyResources(Me.But_hideHtml, "But_hideHtml")
        Me.But_hideHtml.BackColor = System.Drawing.SystemColors.Control
        Me.But_hideHtml.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.hide_down
        Me.But_hideHtml.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_hideHtml.Name = "But_hideHtml"
        Me.But_hideHtml.UseVisualStyleBackColor = False
        '
        'But_hideMain
        '
        resources.ApplyResources(Me.But_hideMain, "But_hideMain")
        Me.But_hideMain.BackColor = System.Drawing.SystemColors.Control
        Me.But_hideMain.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.hide_right
        Me.But_hideMain.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_hideMain.Name = "But_hideMain"
        Me.But_hideMain.UseVisualStyleBackColor = False
        '
        'Pan_all
        '
        resources.ApplyResources(Me.Pan_all, "Pan_all")
        Me.Pan_all.Controls.Add(Me.But_hideHtml)
        Me.Pan_all.Controls.Add(Me.But_hideMain)
        Me.Pan_all.Controls.Add(Me.Pan_drawArea)
        Me.Pan_all.Controls.Add(Me.Pan_main)
        Me.Pan_all.Controls.Add(Me.Pan_html)
        Me.Pan_all.Name = "Pan_all"
        '
        'Form_main
        '
        Me.AllowDrop = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Pan_all)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_main"
        Me.Context_selPoints.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Pan_tools.ResumeLayout(False)
        Me.Pan_toggles.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.Context_attributes.ResumeLayout(False)
        CType(Me.Pic_attrColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.Num_decimals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_stickyGHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_stikyGWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_gridHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_gridWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_zoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_canvasHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_canvasWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.Num_templateH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_templateW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_templateY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_templateX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_html.ResumeLayout(False)
        Me.Pan_html.PerformLayout()
        CType(Me.Pic_preview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Context_figures.ResumeLayout(False)
        Me.Pan_main.ResumeLayout(False)
        Me.Pan_main.PerformLayout()
        Me.Pan_drawArea.ResumeLayout(False)
        CType(Me.Pic_canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_all.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Pic_canvas As PictureBox
    Friend WithEvents Lab_sizeW As Label
    Friend WithEvents Lab_mposY As Label
    Friend WithEvents Lab_sizeH As Label
    Friend WithEvents Lb_selPoints As ListBox
    Friend WithEvents Pic_preview As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PathToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoveTo00ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ClearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Pan_tools As Panel
    Friend WithEvents But_selection As Button
    Friend WithEvents But_elliArc As Button
    Friend WithEvents But_smoothBezier As Button
    Friend WithEvents But_bezier As Button
    Friend WithEvents But_smoothCurveto As Button
    Friend WithEvents But_curveto As Button
    Friend WithEvents But_vertLineto As Button
    Friend WithEvents But_horLineto As Button
    Friend WithEvents But_lineto As Button
    Friend WithEvents But_moveto As Button
    Friend WithEvents But_closePath As Button
    Friend WithEvents But_placeBetClosest As Button
    Friend WithEvents Pan_toggles As Panel
    Friend WithEvents But_showPoints As Button
    Friend WithEvents But_mirrorVert As Button
    Friend WithEvents But_mirrorHor As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents RoundPositionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Tb_html As TextBox
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FigureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents But_mirror As Button
    Friend WithEvents Pan_html As GroupBox
    Friend WithEvents TESTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddLotsOPointsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CreateFigureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Num_zoom As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Num_canvasHeight As NumericUpDown
    Friend WithEvents Num_canvasWidth As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents SVGToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoveTo00ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CropToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ClearToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Lb_figures As ListBox
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewHelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents But_movement As Button
    Friend WithEvents Lab_mposX As Label
    Friend WithEvents Lab_zoom As Label
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ExportAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowRealSizePreviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Num_gridHeight As NumericUpDown
    Friend WithEvents Num_gridWidth As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents Num_stickyGHeight As NumericUpDown
    Friend WithEvents Num_stikyGWidth As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents DuplicateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents FlipHorizontallyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FlipVerticallyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Context_selPoints As ContextMenuStrip
    Friend WithEvents DeselectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents DelteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Cb_optimize As CheckBox
    Friend WithEvents But_removeFigure As Button
    Friend WithEvents But_addFigure As Button
    Friend WithEvents But_figMoveBottom As Button
    Friend WithEvents But_figMoveTop As Button
    Friend WithEvents But_figMoveDown As Button
    Friend WithEvents But_figMoveUp As Button
    Friend WithEvents Num_decimals As NumericUpDown
    Friend WithEvents Label11 As Label
    Friend WithEvents Lb_paths As ListBox
    Friend WithEvents But_pathMoveUp As Button
    Friend WithEvents But_pathMoveDown As Button
    Friend WithEvents But_pathMoveTop As Button
    Friend WithEvents But_pathMoveBottom As Button
    Friend WithEvents But_addPath As Button
    Friend WithEvents But_removePath As Button
    Friend WithEvents But_pathRename As Button
    Friend WithEvents Timer_autoBackup As Timer
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents LoadBackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Lab_lastBkp As Label
    Friend WithEvents Cb_htmlWrap As CheckBox
    Friend WithEvents But_removeSelPts As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Cb_templateKeepAspect As CheckBox
    Friend WithEvents Num_templateH As NumericUpDown
    Friend WithEvents Num_templateW As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents Num_templateY As NumericUpDown
    Friend WithEvents Num_templateX As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents But_addTemplate As Button
    Friend WithEvents But_removeTemplate As Button
    Friend WithEvents Combo_templates As ComboBox
    Friend WithEvents Cb_templateVisible As CheckBox
    Friend WithEvents ScaleToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Timer_refresh As Timer
    Friend WithEvents VScroll_canvasY As VScrollBar
    Friend WithEvents HScroll_canvasX As HScrollBar
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ScaleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScaleToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents But_bkgTempFill As Button
    Friend WithEvents But_bkgTempCenter As Button
    Friend WithEvents But_attrOk As Button
    Friend WithEvents Pic_attrColor As PictureBox
    Friend WithEvents Combo_attrVal As ComboBox
    Friend WithEvents Combo_attrName As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Lb_attributes As ListBox
    Friend WithEvents Context_attributes As ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecentFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents But_figDuplicate As Button
    Friend WithEvents But_pathDuplicate As Button
    Friend WithEvents But_showGrid As Button
    Friend WithEvents Pan_main As Panel
    Friend WithEvents But_hideMain As Button
    Friend WithEvents Pan_drawArea As Panel
    Friend WithEvents But_hideHtml As Button
    Friend WithEvents But_figOpen As Button
    Friend WithEvents Context_figures As ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CenterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Lab_zoomedH As Label
    Friend WithEvents Lab_zoomedW As Label
    Friend WithEvents Cb_noHV As CheckBox
    Friend WithEvents But_showBigGrid As Button
    Friend WithEvents RoundPositionsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents But_pathHide As Button
    Friend WithEvents But_figHide As Button
    Friend WithEvents Pan_all As Panel
End Class
