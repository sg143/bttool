Imports System.IO
Public Class SplashScreen
    Private cf As New markform.CustomClass
    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        Select Case lbl_load.Text.Length
            Case 9
                lbl_load.Text = "Starting.."
            Case 10
                lbl_load.Text = "Starting..."
            Case 11
                lbl_load.Text = "Starting...."
            Case 12
                lbl_load.Text = "Starting....."
            Case Else
                lbl_load.Text = "Starting."
        End Select
        If cf.IsFormOpen("frm_login") Then
            Me.Dispose()
        End If
    End Sub

    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbl_load.Text = "Checking Directories..."
        CheckDirectory()
        tmr.Enabled = True
    End Sub
    Private Sub CheckDirectory()
        Try
            'create folder in my documents
            Dim BTDir As String = Path.Combine(CustomVariables.MyDocumentsDirectory, "BT Tool")
            'create "BT Tool" folder if not exist
            If Not Directory.Exists(BTDir) Then Directory.CreateDirectory(BTDir)

            Dim di As New DirectoryInfo(BTDir)

            If ((di.Attributes And FileAttributes.Hidden) <> FileAttributes.Hidden) Then File.SetAttributes(BTDir, FileAttributes.Hidden)

            If Not Directory.Exists(Path.Combine(BTDir, "BT")) Then Directory.CreateDirectory(Path.Combine(BTDir, "BT"))
            If Not Directory.Exists(Path.Combine(BTDir, "PR")) Then Directory.CreateDirectory(Path.Combine(BTDir, "PR"))
            If Not Directory.Exists(Path.Combine(BTDir, "TS")) Then Directory.CreateDirectory(Path.Combine(BTDir, "TS"))
            If Not Directory.Exists(Path.Combine(BTDir, "CC")) Then Directory.CreateDirectory(Path.Combine(BTDir, "CC"))
            If Not Directory.Exists(Path.Combine(BTDir, "ST")) Then Directory.CreateDirectory(Path.Combine(BTDir, "ST"))
            If Not Directory.Exists(Path.Combine(BTDir, "AUDITOR")) Then Directory.CreateDirectory(Path.Combine(BTDir, "AUDITOR"))

            If Directory.Exists("\\accomediasvr\MediaFiles-2") Then
                CustomVariables.BaseServer = "\\accomediasvr\MediaFiles-2"
            ElseIf Directory.Exists("\\172.16.3.54\MediaFiles-2") Then
                CustomVariables.BaseServer = "\\172.16.3.54\MediaFiles-2"
            Else
                MessageBox.Show("System cannot connect to server." & vbCrLf & "Contact your System Administrator.", "Connection issue to server", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
        Catch ex As Exception
            cf.WriteToFile("{0}==>" & ex.ToString, CustomVariables.DebugDirectory & "\\BTTOOL_ERROR_LOG.txt")
        End Try
    End Sub
End Class