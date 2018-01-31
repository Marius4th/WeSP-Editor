<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.Group_info = New System.Windows.Forms.GroupBox()
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
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.CropToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.But_mirror = New System.Windows.Forms.Button()
        Me.But_mirrorVert = New System.Windows.Forms.Button()
        Me.But_mirrorHor = New System.Windows.Forms.Button()
        Me.But_showPoints = New System.Windows.Forms.Button()
        Me.But_placeBetClosest = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Lab_zoom = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Num_strokeWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Col_fill = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Col_stroke = New System.Windows.Forms.PictureBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
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
        Me.Num6_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num4_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num2_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num6_1 = New System.Windows.Forms.NumericUpDown()
        Me.Num5_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num5_1 = New System.Windows.Forms.NumericUpDown()
        Me.Num4_1 = New System.Windows.Forms.NumericUpDown()
        Me.Num3_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num3_1 = New System.Windows.Forms.NumericUpDown()
        Me.Num2_1 = New System.Windows.Forms.NumericUpDown()
        Me.Num1_2 = New System.Windows.Forms.NumericUpDown()
        Me.Num1_1 = New System.Windows.Forms.NumericUpDown()
        Me.Tb_html = New System.Windows.Forms.TextBox()
        Me.Box_html = New System.Windows.Forms.GroupBox()
        Me.Cb_optimize = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Pic_preview = New System.Windows.Forms.PictureBox()
        Me.Pan_canvas = New System.Windows.Forms.Panel()
        Me.Pic_canvas = New System.Windows.Forms.PictureBox()
        Me.Combo_path = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Lb_figures = New System.Windows.Forms.ListBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Group_info.SuspendLayout()
        Me.Context_selPoints.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Pan_tools.SuspendLayout()
        Me.Pan_toggles.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Num_strokeWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Col_fill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Col_stroke, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Num_stickyGHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_stikyGWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_gridHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_gridWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_zoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_canvasHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_canvasWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Num6_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num4_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num2_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num6_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num5_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num5_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num4_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num3_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num3_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num2_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num1_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num1_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Box_html.SuspendLayout()
        CType(Me.Pic_preview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_canvas.SuspendLayout()
        CType(Me.Pic_canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Group_info
        '
        Me.Group_info.Controls.Add(Me.Lab_sizeH)
        Me.Group_info.Controls.Add(Me.Lab_mposY)
        Me.Group_info.Controls.Add(Me.Lab_sizeW)
        Me.Group_info.Controls.Add(Me.Lab_mposX)
        resources.ApplyResources(Me.Group_info, "Group_info")
        Me.Group_info.Name = "Group_info"
        Me.Group_info.TabStop = False
        '
        'Lab_sizeH
        '
        resources.ApplyResources(Me.Lab_sizeH, "Lab_sizeH")
        Me.Lab_sizeH.Name = "Lab_sizeH"
        '
        'Lab_mposY
        '
        resources.ApplyResources(Me.Lab_mposY, "Lab_mposY")
        Me.Lab_mposY.Name = "Lab_mposY"
        '
        'Lab_sizeW
        '
        resources.ApplyResources(Me.Lab_sizeW, "Lab_sizeW")
        Me.Lab_sizeW.Name = "Lab_sizeW"
        '
        'Lab_mposX
        '
        resources.ApplyResources(Me.Lab_mposX, "Lab_mposX")
        Me.Lab_mposX.Name = "Lab_mposX"
        '
        'Lb_selPoints
        '
        resources.ApplyResources(Me.Lb_selPoints, "Lb_selPoints")
        Me.Lb_selPoints.ContextMenuStrip = Me.Context_selPoints
        Me.Lb_selPoints.FormattingEnabled = True
        Me.Lb_selPoints.Name = "Lb_selPoints"
        Me.Lb_selPoints.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
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
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.LoadToolStripMenuItem, Me.ToolStripSeparator7, Me.ExportAsToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
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
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        resources.ApplyResources(Me.LoadToolStripMenuItem, "LoadToolStripMenuItem")
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
        Me.SVGToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveTo00ToolStripMenuItem1, Me.CropToolStripMenuItem, Me.ToolStripSeparator5, Me.ClearToolStripMenuItem2})
        Me.SVGToolStripMenuItem.Name = "SVGToolStripMenuItem"
        resources.ApplyResources(Me.SVGToolStripMenuItem, "SVGToolStripMenuItem")
        '
        'MoveTo00ToolStripMenuItem1
        '
        Me.MoveTo00ToolStripMenuItem1.Name = "MoveTo00ToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTo00ToolStripMenuItem1, "MoveTo00ToolStripMenuItem1")
        '
        'CropToolStripMenuItem
        '
        Me.CropToolStripMenuItem.Name = "CropToolStripMenuItem"
        resources.ApplyResources(Me.CropToolStripMenuItem, "CropToolStripMenuItem")
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
        Me.FigureToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateFigureToolStripMenuItem, Me.DuplicateToolStripMenuItem, Me.ToolStripSeparator10, Me.FlipHorizontallyToolStripMenuItem, Me.FlipVerticallyToolStripMenuItem, Me.ToolStripSeparator3, Me.ClearToolStripMenuItem1, Me.DeleteToolStripMenuItem1})
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
        Me.But_smoothBezier.BackColor = System.Drawing.Color.Snow
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
        Me.But_smoothCurveto.BackColor = System.Drawing.Color.Snow
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
        Me.But_vertLineto.BackColor = System.Drawing.Color.Snow
        Me.But_vertLineto.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.vertLineto
        resources.ApplyResources(Me.But_vertLineto, "But_vertLineto")
        Me.But_vertLineto.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_vertLineto.ForeColor = System.Drawing.Color.DarkOrange
        Me.But_vertLineto.Name = "But_vertLineto"
        Me.But_vertLineto.UseVisualStyleBackColor = False
        '
        'But_horLineto
        '
        Me.But_horLineto.BackColor = System.Drawing.Color.Snow
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
        Me.Pan_toggles.Controls.Add(Me.But_mirror)
        Me.Pan_toggles.Controls.Add(Me.But_mirrorVert)
        Me.Pan_toggles.Controls.Add(Me.But_mirrorHor)
        Me.Pan_toggles.Controls.Add(Me.But_showPoints)
        Me.Pan_toggles.Controls.Add(Me.But_placeBetClosest)
        Me.Pan_toggles.Name = "Pan_toggles"
        '
        'But_mirror
        '
        Me.But_mirror.BackColor = System.Drawing.Color.Snow
        Me.But_mirror.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.noMirror
        resources.ApplyResources(Me.But_mirror, "But_mirror")
        Me.But_mirror.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_mirror.Name = "But_mirror"
        Me.But_mirror.UseVisualStyleBackColor = False
        '
        'But_mirrorVert
        '
        Me.But_mirrorVert.BackColor = System.Drawing.Color.Snow
        Me.But_mirrorVert.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.mirrorVert
        resources.ApplyResources(Me.But_mirrorVert, "But_mirrorVert")
        Me.But_mirrorVert.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.But_mirrorVert.Name = "But_mirrorVert"
        Me.But_mirrorVert.UseVisualStyleBackColor = False
        '
        'But_mirrorHor
        '
        Me.But_mirrorHor.BackColor = System.Drawing.Color.Snow
        Me.But_mirrorHor.BackgroundImage = Global.WebSVGPathEditor.My.Resources.Resources.mirrorHor
        resources.ApplyResources(Me.But_mirrorHor, "But_mirrorHor")
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
        Me.TabPage1.Controls.Add(Me.Lab_zoom)
        Me.TabPage1.Controls.Add(Me.Group_info)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Lab_zoom
        '
        resources.ApplyResources(Me.Lab_zoom, "Lab_zoom")
        Me.Lab_zoom.Name = "Lab_zoom"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Num_strokeWidth)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.Col_fill)
        Me.TabPage4.Controls.Add(Me.Label8)
        Me.TabPage4.Controls.Add(Me.Label7)
        Me.TabPage4.Controls.Add(Me.Col_stroke)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Num_strokeWidth
        '
        Me.Num_strokeWidth.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_strokeWidth, "Num_strokeWidth")
        Me.Num_strokeWidth.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num_strokeWidth.Name = "Num_strokeWidth"
        Me.Num_strokeWidth.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'Col_fill
        '
        Me.Col_fill.BackColor = System.Drawing.Color.White
        Me.Col_fill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.Col_fill, "Col_fill")
        Me.Col_fill.Name = "Col_fill"
        Me.Col_fill.TabStop = False
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'Col_stroke
        '
        Me.Col_stroke.BackColor = System.Drawing.Color.Gray
        Me.Col_stroke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.Col_stroke, "Col_stroke")
        Me.Col_stroke.Name = "Col_stroke"
        Me.Col_stroke.TabStop = False
        '
        'TabPage2
        '
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
        'Num_stickyGHeight
        '
        Me.Num_stickyGHeight.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_stickyGHeight, "Num_stickyGHeight")
        Me.Num_stickyGHeight.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_stickyGHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_stickyGHeight.Name = "Num_stickyGHeight"
        Me.Num_stickyGHeight.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Num_stikyGWidth
        '
        Me.Num_stikyGWidth.DecimalPlaces = 1
        resources.ApplyResources(Me.Num_stikyGWidth, "Num_stikyGWidth")
        Me.Num_stikyGWidth.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.Num_stikyGWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
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
        Me.Num_zoom.Value = New Decimal(New Integer() {10, 0, 0, 0})
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
        Me.TabPage3.Controls.Add(Me.Num6_2)
        Me.TabPage3.Controls.Add(Me.Num4_2)
        Me.TabPage3.Controls.Add(Me.Num2_2)
        Me.TabPage3.Controls.Add(Me.Num6_1)
        Me.TabPage3.Controls.Add(Me.Num5_2)
        Me.TabPage3.Controls.Add(Me.Num5_1)
        Me.TabPage3.Controls.Add(Me.Num4_1)
        Me.TabPage3.Controls.Add(Me.Num3_2)
        Me.TabPage3.Controls.Add(Me.Num3_1)
        Me.TabPage3.Controls.Add(Me.Num2_1)
        Me.TabPage3.Controls.Add(Me.Num1_2)
        Me.TabPage3.Controls.Add(Me.Num1_1)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Num6_2
        '
        resources.ApplyResources(Me.Num6_2, "Num6_2")
        Me.Num6_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num6_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num6_2.Name = "Num6_2"
        Me.Num6_2.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Num4_2
        '
        resources.ApplyResources(Me.Num4_2, "Num4_2")
        Me.Num4_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num4_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num4_2.Name = "Num4_2"
        Me.Num4_2.Value = New Decimal(New Integer() {270, 0, 0, -2147483648})
        '
        'Num2_2
        '
        resources.ApplyResources(Me.Num2_2, "Num2_2")
        Me.Num2_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num2_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num2_2.Name = "Num2_2"
        Me.Num2_2.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Num6_1
        '
        resources.ApplyResources(Me.Num6_1, "Num6_1")
        Me.Num6_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num6_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num6_1.Name = "Num6_1"
        Me.Num6_1.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Num5_2
        '
        resources.ApplyResources(Me.Num5_2, "Num5_2")
        Me.Num5_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num5_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num5_2.Name = "Num5_2"
        Me.Num5_2.Value = New Decimal(New Integer() {270, 0, 0, -2147483648})
        '
        'Num5_1
        '
        resources.ApplyResources(Me.Num5_1, "Num5_1")
        Me.Num5_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num5_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num5_1.Name = "Num5_1"
        Me.Num5_1.Value = New Decimal(New Integer() {180, 0, 0, 0})
        '
        'Num4_1
        '
        resources.ApplyResources(Me.Num4_1, "Num4_1")
        Me.Num4_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num4_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num4_1.Name = "Num4_1"
        Me.Num4_1.Value = New Decimal(New Integer() {180, 0, 0, 0})
        '
        'Num3_2
        '
        resources.ApplyResources(Me.Num3_2, "Num3_2")
        Me.Num3_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num3_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num3_2.Name = "Num3_2"
        Me.Num3_2.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Num3_1
        '
        resources.ApplyResources(Me.Num3_1, "Num3_1")
        Me.Num3_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num3_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num3_1.Name = "Num3_1"
        Me.Num3_1.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Num2_1
        '
        resources.ApplyResources(Me.Num2_1, "Num2_1")
        Me.Num2_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num2_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num2_1.Name = "Num2_1"
        Me.Num2_1.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Num1_2
        '
        resources.ApplyResources(Me.Num1_2, "Num1_2")
        Me.Num1_2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num1_2.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num1_2.Name = "Num1_2"
        Me.Num1_2.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'Num1_1
        '
        resources.ApplyResources(Me.Num1_1, "Num1_1")
        Me.Num1_1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Num1_1.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.Num1_1.Name = "Num1_1"
        Me.Num1_1.Value = New Decimal(New Integer() {150, 0, 0, 0})
        '
        'Tb_html
        '
        resources.ApplyResources(Me.Tb_html, "Tb_html")
        Me.Tb_html.Name = "Tb_html"
        '
        'Box_html
        '
        resources.ApplyResources(Me.Box_html, "Box_html")
        Me.Box_html.Controls.Add(Me.Cb_optimize)
        Me.Box_html.Controls.Add(Me.Button1)
        Me.Box_html.Controls.Add(Me.Pic_preview)
        Me.Box_html.Controls.Add(Me.Tb_html)
        Me.Box_html.Name = "Box_html"
        Me.Box_html.TabStop = False
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
        'Pan_canvas
        '
        resources.ApplyResources(Me.Pan_canvas, "Pan_canvas")
        Me.Pan_canvas.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Pan_canvas.Controls.Add(Me.Pic_canvas)
        Me.Pan_canvas.Name = "Pan_canvas"
        '
        'Pic_canvas
        '
        Me.Pic_canvas.BackColor = System.Drawing.Color.Black
        resources.ApplyResources(Me.Pic_canvas, "Pic_canvas")
        Me.Pic_canvas.Name = "Pic_canvas"
        Me.Pic_canvas.TabStop = False
        '
        'Combo_path
        '
        resources.ApplyResources(Me.Combo_path, "Combo_path")
        Me.Combo_path.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Combo_path.FormattingEnabled = True
        Me.Combo_path.Name = "Combo_path"
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
        Me.Lb_figures.FormattingEnabled = True
        Me.Lb_figures.Name = "Lb_figures"
        Me.Lb_figures.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
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
        'Form_main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Lb_figures)
        Me.Controls.Add(Me.Combo_path)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Pan_canvas)
        Me.Controls.Add(Me.Box_html)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Pan_toggles)
        Me.Controls.Add(Me.Pan_tools)
        Me.Controls.Add(Me.Lb_selPoints)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_main"
        Me.Group_info.ResumeLayout(False)
        Me.Group_info.PerformLayout()
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
        CType(Me.Num_strokeWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Col_fill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Col_stroke, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.Num_stickyGHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_stikyGWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_gridHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_gridWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_zoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_canvasHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_canvasWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Num6_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num4_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num2_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num6_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num5_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num5_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num4_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num3_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num3_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num2_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num1_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num1_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Box_html.ResumeLayout(False)
        Me.Box_html.PerformLayout()
        CType(Me.Pic_preview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_canvas.ResumeLayout(False)
        CType(Me.Pic_canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Pic_canvas As PictureBox
    Friend WithEvents Group_info As GroupBox
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
    Friend WithEvents ScaleToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents Box_html As GroupBox
    Friend WithEvents Pan_canvas As Panel
    Friend WithEvents TESTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddLotsOPointsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Combo_path As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CreateFigureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Col_fill As PictureBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Col_stroke As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Num_zoom As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Num_canvasHeight As NumericUpDown
    Friend WithEvents Num_canvasWidth As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Num6_2 As NumericUpDown
    Friend WithEvents Num4_2 As NumericUpDown
    Friend WithEvents Num2_2 As NumericUpDown
    Friend WithEvents Num6_1 As NumericUpDown
    Friend WithEvents Num5_2 As NumericUpDown
    Friend WithEvents Num5_1 As NumericUpDown
    Friend WithEvents Num4_1 As NumericUpDown
    Friend WithEvents Num3_2 As NumericUpDown
    Friend WithEvents Num3_1 As NumericUpDown
    Friend WithEvents Num2_1 As NumericUpDown
    Friend WithEvents Num1_2 As NumericUpDown
    Friend WithEvents Num1_1 As NumericUpDown
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
    Friend WithEvents Num_strokeWidth As NumericUpDown
    Friend WithEvents Label9 As Label
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
End Class
