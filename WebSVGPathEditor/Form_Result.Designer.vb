<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_result
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AlwaysOnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pic_realSize = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Pic_realSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlwaysOnTopToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(154, 26)
        '
        'AlwaysOnTopToolStripMenuItem
        '
        Me.AlwaysOnTopToolStripMenuItem.Checked = True
        Me.AlwaysOnTopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AlwaysOnTopToolStripMenuItem.Name = "AlwaysOnTopToolStripMenuItem"
        Me.AlwaysOnTopToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.AlwaysOnTopToolStripMenuItem.Text = "Always On Top"
        '
        'Pic_realSize
        '
        Me.Pic_realSize.BackColor = System.Drawing.Color.Black
        Me.Pic_realSize.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Pic_realSize.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Pic_realSize.Location = New System.Drawing.Point(0, 0)
        Me.Pic_realSize.Name = "Pic_realSize"
        Me.Pic_realSize.Size = New System.Drawing.Size(64, 64)
        Me.Pic_realSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Pic_realSize.TabIndex = 10
        Me.Pic_realSize.TabStop = False
        '
        'Form_result
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(48, 50)
        Me.Controls.Add(Me.Pic_realSize)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form_result"
        Me.Text = "RS Preview - WeSP"
        Me.TopMost = True
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Pic_realSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Pic_realSize As PictureBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AlwaysOnTopToolStripMenuItem As ToolStripMenuItem
End Class
