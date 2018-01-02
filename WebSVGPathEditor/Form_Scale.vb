Public Class Form_scale
    Private lastScale As Single = 0

    'This is not working properly because of floating point errors/inaccuracies or so I think.
    'Will change this in the future

    Private Sub Track_scale_Scroll(sender As Object, e As EventArgs) Handles Track_scale.Scroll
        Num_scale.Value = Track_scale.Value * 10
    End Sub

    Private Sub Num_scale_ValueChanged(sender As Object, e As EventArgs) Handles Num_scale.ValueChanged
        Track_scale.Value = Num_scale.Value / 10
        SVG.SelectedPath.Scale(1.0F + ((Num_scale.Value - lastScale) / 100.0F))
        lastScale = Num_scale.Value
        Form_main.Pic_canvas.Refresh()

        'Change the size of the 
        'If Form1.Pic_realSize.Image IsNot Nothing Then Form1.Pic_realSize.Image.Dispose()
        'Form1.Pic_realSize.Image = SVG.CanvasImg
    End Sub

    Private Sub Bu_apply_Click(sender As Object, e As EventArgs) Handles Bu_apply.Click
        Me.Close()
    End Sub

    Private Sub Form_Scale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lastScale = 0
        Num_scale.Value = 0
    End Sub

    Private Sub Form_Scale_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Num_scale.Value = 0
        End If
    End Sub

    Private Sub Bu_cancel_Click(sender As Object, e As EventArgs) Handles Bu_cancel.Click
        Num_scale.Value = 0
        Me.Close()
    End Sub
End Class