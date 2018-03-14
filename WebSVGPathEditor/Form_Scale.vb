Public Class Form_scale
    Public Enum ScalingObjective
        Figures
        Paths
        SVG
    End Enum

    Private pivotPt As CPointF = Nothing
    Public scaleObjective As ScalingObjective = ScalingObjective.Figures

    'Now it's working great ^^

    Private Sub RefreshFiguresApparentScale()
        Dim scale As Double = 1.0F + ((Num_scale.Value) / 100.0F)
        'Set apparent scaling (not actually scaling the figure's points' position
        If Cb_onePivot.Checked Then
            If Cb_center.Checked Then
                pivotPt = New CPointF(0, 0)
                For Each fig As Figure In SVG.GetSelectedFigures
                    pivotPt.X += fig.GetCenterPoint().X
                    pivotPt.Y += fig.GetCenterPoint().Y
                Next

                pivotPt.X /= SVG.GetSelectedFigures.Count
                pivotPt.Y /= SVG.GetSelectedFigures.Count
            ElseIf SVG.GetSelectedFigures.Count > 0 Then
                Dim moveto As PathPoint = SVG.GetSelectedFigures(0).GetMoveto()
                If moveto IsNot Nothing Then pivotPt = moveto.Pos
            End If
        Else
            pivotPt = Nothing
        End If

        For Each fig As Figure In SVG.GetSelectedFigures
            fig.TransformScale(scale, scale, Cb_center.Checked, pivotPt)
        Next

        Form_main.Pic_canvas.Invalidate()
    End Sub

    Private Sub RefreshPathsApparentScale()
        Dim scale As Double = 1.0F + ((Num_scale.Value) / 100.0F)
        'Set apparent scaling (not actually scaling the figure's points' position

        pivotPt = New CPointF(0, 0)
        Dim ptsTotal As Integer = 0

        If Cb_onePivot.Checked Then
            If Cb_center.Checked Then
                For Each path As SVGPath In SVG.selectedPaths
                    For Each fig As Figure In path.GetFigures
                        pivotPt.X += fig.GetCenterPoint().X
                        pivotPt.Y += fig.GetCenterPoint().Y
                    Next
                    ptsTotal += path.GetFigures.Count
                Next

                pivotPt.X /= ptsTotal
                pivotPt.Y /= ptsTotal
            ElseIf SVG.GetSelectedFigures.Count > 0 Then
                Dim moveto As PathPoint = SVG.GetSelectedFigures(0).GetMoveto()
                If moveto IsNot Nothing Then pivotPt = moveto.Pos
            End If
        Else
            pivotPt = Nothing
        End If

        For Each path As SVGPath In SVG.selectedPaths
            For Each fig As Figure In path.GetFigures
                fig.TransformScale(scale, scale, Cb_center.Checked, pivotPt)
            Next
        Next

        Form_main.Pic_canvas.Invalidate()
    End Sub

    Private Sub RefreshSVGApparentScale()
        Dim scale As Double = 1.0F + ((Num_scale.Value) / 100.0F)
        'Set apparent scaling (not actually scaling the figure's points' position

        pivotPt = New CPointF(0, 0)
        Dim ptsTotal As Integer = 0

        If Cb_onePivot.Checked Then
            If Cb_center.Checked Then
                For Each path As SVGPath In SVG.Paths
                    For Each fig As Figure In path.GetFigures
                        pivotPt.X += fig.GetCenterPoint().X
                        pivotPt.Y += fig.GetCenterPoint().Y
                    Next
                    ptsTotal += path.GetFigures.Count
                Next

                pivotPt.X /= ptsTotal
                pivotPt.Y /= ptsTotal
            ElseIf SVG.GetSelectedFigures.Count > 0 Then
                Dim moveto As PathPoint = SVG.GetSelectedFigures(0).GetMoveto()
                If moveto IsNot Nothing Then pivotPt = moveto.Pos
            End If
        Else
            pivotPt = Nothing
        End If

        For Each path As SVGPath In SVG.Paths
            For Each fig As Figure In path.GetFigures
                fig.TransformScale(scale, scale, Cb_center.Checked, pivotPt)
            Next
        Next

        Form_main.Pic_canvas.Invalidate()
    End Sub

    Private Sub RefreshScale()
        Select Case scaleObjective
            Case ScalingObjective.Paths
                RefreshPathsApparentScale()
            Case ScalingObjective.Figures
                RefreshFiguresApparentScale()
            Case ScalingObjective.SVG
                RefreshSVGApparentScale()
        End Select
    End Sub

    Private Sub ApplyScale()
        Dim scale As Double = 1.0F + ((Num_scale.Value) / 100.0F)

        Select Case scaleObjective
            Case ScalingObjective.Paths
                For Each path As SVGPath In SVG.selectedPaths
                    For Each fig As Figure In path.GetFigures
                        'Set real scaling
                        fig.Scale(scale, scale, Cb_center.Checked, pivotPt)
                    Next
                Next
            Case ScalingObjective.Figures
                For Each fig As Figure In SVG.GetSelectedFigures
                    'Set real scaling
                    fig.Scale(scale, scale, Cb_center.Checked, pivotPt)
                Next
            Case ScalingObjective.SVG
                For Each path As SVGPath In SVG.Paths
                    For Each fig As Figure In path.GetFigures
                        'Set real scaling
                        fig.Scale(scale, scale, Cb_center.Checked, pivotPt)
                    Next
                Next
        End Select
    End Sub

    Private Sub Track_scale_Scroll(sender As Object, e As EventArgs) Handles Track_scale.Scroll
        Num_scale.Value = Track_scale.Value * 10
    End Sub

    Private Sub Num_scale_ValueChanged(sender As Object, e As EventArgs) Handles Num_scale.ValueChanged
        Track_scale.Value = Num_scale.Value / 10

        RefreshScale()
    End Sub

    Private Sub Bu_apply_Click(sender As Object, e As EventArgs) Handles Bu_apply.Click
        ApplyScale()
        Me.Close()
    End Sub

    Private Sub Form_Scale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Num_scale.Value = 0
    End Sub

    Private Sub Form_Scale_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Reset apparent scaling
        Num_scale.Value = 0
        Form_main.Enabled = True
    End Sub

    Private Sub Bu_cancel_Click(sender As Object, e As EventArgs) Handles Bu_cancel.Click
        'Reset apparent scaling
        Num_scale.Value = 0
        Me.Close()
    End Sub

    Private Sub Cb_onePivot_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_onePivot.CheckedChanged
        RefreshScale()
    End Sub

    Private Sub Cb_center_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_center.CheckedChanged
        RefreshScale()
    End Sub

    Public Sub SetScalingObjective(objective As ScalingObjective)
        scaleObjective = objective
    End Sub
End Class