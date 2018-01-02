Public Class Form_result
    Public onTop As Boolean = True

    Private Sub Form_Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Pic_realSize.Size = SVG.CanvasSize.ToSize
        Me.ClientSize = Pic_realSize.Size
    End Sub

    Private Sub Pic_realSize_Resize(sender As Object, e As EventArgs) Handles Pic_realSize.Resize
        Me.ClientSize = Pic_realSize.Size
    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        onTop = Not onTop
        If onTop Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
        AlwaysOnTopToolStripMenuItem.Checked = onTop
    End Sub
End Class