Imports System.IO

Public Class frm_login

    Private db As New markform.SQLClass
    Private cf As New markform.CustomClass
    '*************************************************************
    'Events
    '*************************************************************
    Private Sub Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_username.KeyDown, txt_pass.KeyDown
        If e.KeyValue = 13 Then
            LoginValidation()
        End If
    End Sub
    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        LoginValidation()
    End Sub
    Private Sub frm_login_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Activate()
        txt_username.Select()
    End Sub
    '*************************************************************
    'Sub Routines
    '*************************************************************
    Private Sub LoginValidation()
        'set the default display of the login form
        lbl_err.Visible = False
        txt_username.BackColor = Color.White
        txt_pass.BackColor = Color.White
        'set variables
        Dim uname As String = txt_username.Text : Dim pass As String = txt_pass.Text
        Dim errmsg As List(Of String) = New List(Of String)() 'store error messages here
        Dim txterr As List(Of TextBox) = New List(Of TextBox)() 'store txtbox that is empty here
        'check if username is empty
        If cf.IsStringEmpty(uname) Then
            errmsg.Add("-Username field is empty.")
            txterr.Add(txt_username)
        End If
        'check if password is empty
        If cf.IsStringEmpty(pass) Then
            errmsg.Add("-Password field is empty.")
            txterr.Add(txt_pass)
        End If
        'check if there's an error and display the error
        If errmsg.Count > 0 Then
            DisplayError(errmsg, txterr)
        Else
            'set the error handler
            Try
                'check if username exist and fetch the data
                Dim UserData As String() = db.row("SELECT Id,username,uDept,lockTime,status,password,position,department,btwork 
                            FROM dbo.UserData WHERE username LIKE @uname", New String() {"uname", uname})
                'set the variable with the data from UserData
                Dim Uid As String = UserData(0)
                Dim UserName As String = UserData(1)
                Dim UDept As String = UserData(2)
                Dim LockTime As String = UserData(3)
                Dim Status As String = UserData(4)
                Dim Password As String = UserData(5)
                Dim Position As String = UserData(6)
                Dim Department As String = UserData(7)
                Dim BTWork As String = UserData(8)

                'check if username exist
                If cf.IsStringEmpty(Uid) Then
                    errmsg.Add("Username does not exist.")
                    txterr.Add(txt_username)
                Else
                    'check if password match
                    If pass <> Password Then
                        errmsg.Add("Incorrect password.")
                        txterr.Add(txt_pass)
                    Else
                        'check if online 
                        If Status = "True" Then
                            errmsg.Add("User already logged in.")
                            txterr.Add(txt_username)
                        End If
                    End If
                End If
                'check if there's an error from validating the above conditions and display error
                If errmsg.Count > 0 Then
                    DisplayError(errmsg, txterr)
                Else
                    'update the user status and insert a time log
                    Dim OtherChanges = db.nQuery("UPDATE dbo.userData SET status=@online WHERE Id = @uid;
                                                 INSERT INTO dbo.LogData (timeIn,uid) VALUES (@timeIn,@uid)",
                                                 New String() {"online", "False", "uid", Integer.Parse(Uid), "timeIn", Date.Now})
                    If OtherChanges = 0 Then
                        MessageBox.Show(Me, "Something went wrong while trying to login. Please call the developer for assistance.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        'set global variables to used throughout the whole application
                        CustomVariables.CurrentUserID = Integer.Parse(Uid)
                        CustomVariables.CurrentUserName = UserName
                        CustomVariables.CurrentUDept = UDept
                        CustomVariables.CurrentUserDepartment = Department
                        CustomVariables.CurrentUserPosition = Position
                        CustomVariables.CurrentUserIsActive = Boolean.Parse(Status)
                        CustomVariables.CurrentUserIsBTWork = Boolean.Parse(BTWork)
                        CustomVariables.LockTime = Integer.Parse(LockTime)
                        CustomVariables.FileLocalDirectory = Path.Combine(CustomVariables.MyDocumentsDirectory, "BT Tool", UDept)
                        txt_username.Text = "" : txt_pass.Text = ""
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    End If
                End If
            Catch ex As Exception
                cf.WriteToFile("{0}==>" & ex.ToString, CustomVariables.DebugDirectory & "\\" & DebugTextFile)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Displaying error message to the label.
    ''' </summary>
    ''' <param name="errmsg">List of error message</param>
    ''' <param name="txterr">list of textbox that cause the error message</param>
    Private Sub DisplayError(errmsg As List(Of String), txterr As List(Of TextBox))
        lbl_err.Visible = True : lbl_err.Text = String.Join(vbNewLine, errmsg.ToArray())
        txterr(0).Select() 'set focus on the first txtbox that has an error
        For Each txt As TextBox In txterr
            txt.BackColor = Color.FromArgb(233, 170, 170) 'change the color of all the txtbox that has an error
        Next
    End Sub
End Class