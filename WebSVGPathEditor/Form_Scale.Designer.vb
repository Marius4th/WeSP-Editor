<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_scale
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Track_scale = New System.Windows.Forms.TrackBar()
        Me.Num_scale = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Bu_apply = New System.Windows.Forms.Button()
        Me.Bu_cancel = New System.Windows.Forms.Button()
        CType(Me.Track_scale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_scale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Track_scale
        '
        Me.Track_scale.Location = New System.Drawing.Point(-1, 2)
        Me.Track_scale.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Track_scale.Maximum = 50
        Me.Track_scale.Minimum = -50
        Me.Track_scale.Name = "Track_scale"
        Me.Track_scale.Size = New System.Drawing.Size(636, 45)
        Me.Track_scale.TabIndex = 0
        '
        'Num_scale
        '
        Me.Num_scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Num_scale.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.Num_scale.Location = New System.Drawing.Point(8, 39)
        Me.Num_scale.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Num_scale.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.Num_scale.Minimum = New Decimal(New Integer() {500, 0, 0, -2147483648})
        Me.Num_scale.Name = "Num_scale"
        Me.Num_scale.Size = New System.Drawing.Size(95, 22)
        Me.Num_scale.TabIndex = 1
        Me.Num_scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Num_scale.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(104, 41)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "%"
        '
        'Bu_apply
        '
        Me.Bu_apply.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.Bu_apply.Location = New System.Drawing.Point(543, 41)
        Me.Bu_apply.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Bu_apply.Name = "Bu_apply"
        Me.Bu_apply.Size = New System.Drawing.Size(84, 25)
        Me.Bu_apply.TabIndex = 3
        Me.Bu_apply.Text = "Apply"
        Me.Bu_apply.UseVisualStyleBackColor = True
        '
        'Bu_cancel
        '
        Me.Bu_cancel.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.Bu_cancel.Location = New System.Drawing.Point(451, 41)
        Me.Bu_cancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Bu_cancel.Name = "Bu_cancel"
        Me.Bu_cancel.Size = New System.Drawing.Size(84, 25)
        Me.Bu_cancel.TabIndex = 4
        Me.Bu_cancel.Text = "Cancel"
        Me.Bu_cancel.UseVisualStyleBackColor = True
        '
        'Form_scale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 66)
        Me.Controls.Add(Me.Bu_cancel)
        Me.Controls.Add(Me.Bu_apply)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Num_scale)
        Me.Controls.Add(Me.Track_scale)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Form_scale"
        Me.Text = "Scale Path - WeSP"
        CType(Me.Track_scale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_scale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Track_scale As TrackBar
    Friend WithEvents Num_scale As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Bu_apply As Button
    Friend WithEvents Bu_cancel As Button
End Class
