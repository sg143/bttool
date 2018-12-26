Public Class frm_main
    Private cf As New markform.CustomClass


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Show()
        Dim login As DialogResult = frm_login.ShowDialog(Me)
        If login = DialogResult.OK Then
            If CustomVariables.CurrentUserPosition = "prod" Then cf.DisabledControl(Me)
        Else
            Me.Dispose()
        End If
    End Sub
End Class
