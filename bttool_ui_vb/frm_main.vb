Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.IO

Public Class frm_main
    '***********************************************************************
    'DLL function and structure
    '***********************************************************************
    ''' <summary>
    ''' user32.dll function that captures user input
    ''' </summary>
    ''' <param name="plii"></param>
    ''' <returns></returns>
    <DllImport("user32.dll")> Shared Function GetLastInputInfo(ByRef plii As LASTINPUTINFO) As Boolean
    End Function
    <StructLayout(LayoutKind.Sequential)> Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)> Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)> Public dwTime As Integer
    End Structure
    '***********************************************************************
    'Instanciate class
    '***********************************************************************
    ''' <summary>
    ''' A class that contains custom functions.
    ''' </summary>
    Private cf As New markform.CustomClass
    ''' <summary>
    ''' A class for manipulating database (eg. INSERT,UPDATE,DELETE,SELECT)
    ''' </summary>
    Private db As New markform.SQLClass
    Private lastInputInf As New LASTINPUTINFO()
    '***********************************************************************
    'Setting variables
    '***********************************************************************
    ''' <summary>
    ''' Timer variable for checking if the user is still waiting for a file or not.
    ''' </summary>
    Private isWait As Boolean = False
    ''' <summary>
    ''' The time at which the user wait for the file.
    ''' </summary>
    Private waitSTime As String = ""
    Private el_tick As Integer
    Private user_idle As Boolean = False
    Private last_tick As Integer = 0
    ''' <summary>
    ''' This variable is used to store the query string if an error occured when trying to execute the query.
    ''' </summary>
    Private ErrorQuery As String = ""
    ''' <summary>
    ''' <see langword="True"/> if there's a file currently selected else <see langword="False"/>
    ''' </summary>
    Private FileIsSelected As Boolean = False
    ''' <summary>
    ''' The workflow of the selected file.
    ''' </summary>
    Private WorkFlow As String
    ''' <summary>
    ''' The current workflow base on the index of the WorkFlow that is split into array
    ''' </summary>
    Private CurrentFlow As Integer
    ''' <summary>
    ''' The user id of the assigned qa on the selected file
    ''' </summary>
    Private QAUserID As Integer = 0
    ''' <summary>
    ''' The row index of the last selected row.
    ''' </summary>
    Private LastSelectedRowIndex As Integer = vbNull
    '***********************************************************************
    'Events
    '***********************************************************************
    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Show()
        Dim login As DialogResult = frm_login.ShowDialog(Me)
        LoginDefault(login)
    End Sub
    ''' <summary>
    ''' Idle and Wait timer eventhandler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles IdleTimer.Tick, WaitTimer.Tick
        If Not BWTimer.IsBusy Then
            BWTimer.RunWorkerAsync(main_gridview)
        End If
    End Sub
    Private Sub btn_loginout_Click(sender As Object, e As EventArgs) Handles btn_loginout.Click
        Dim dr As DialogResult = MessageBox.Show("Are you sure you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dr = DialogResult.Yes Then
            Try
                Dim qry As Integer = db.nQuery("UPDATE dbo.LogData set timeOut=@date WHERE Id=@id;
                    UPDATE dbo.userData SET status=@online WHERE Id =@id", New String() {"id", CustomVariables.CurrentUserID, "date", Date.Now, "online", "False"})
                If qry > 0 Then
                    If CustomVariables.FileID <> 0 Then
                        db.nQuery("UPDATE dbo.Main SET activeUser=@activeUser,active=@active WHERE Id=" & CustomVariables.FileID, New String() {"activeUser", 0, "active", "False"})
                        CustomVariables.FileID = 0
                    End If
                End If
                Me.Text = "BT TOOL"

                For Each frm As Form In My.Application.OpenForms
                    If Not New String() {Me.Name, frm_login.Name}.Contains(frm.Name) Then
                        frm.Close()
                    End If
                Next
                ClearForm()
                main_gridview.DataSource = vbNull
                IdleTimer.Stop()
                WaitTimer.Stop()
                'show login form
                Dim login As DialogResult = frm_login.ShowDialog(Me)
                LoginDefault(login)
            Catch ex As Exception
                cf.WriteToFile("{0}==>" & ex.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
            End Try
        End If
    End Sub
    Private Sub main_gridview_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles main_gridview.CellDoubleClick
        'prevent getting error when user double click on the column header
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim SelectedRow As DataGridViewRow = dgv.SelectedRows(0)
            'prevent user from selecting the file when the file is disabled
            If Not SelectedRow.Cells(40).Value Then
                Dim topfile As String = dgv.Rows(0).Cells(8).Value.ToString()
                MessageBox.Show(Me, "You can't start on this file yet. " & vbNewLine & "Submit [" & topfile & "] first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            'prevent user from selecting the file when the file is on hold
            If SelectedRow.Cells(41).Value Then
                Dim isPass As Boolean = True : Dim msg As String = ""
                For Each row As DataGridViewRow In dgv.Rows
                    If SelectedRow.Index <> row.Index And row.Cells(36).Value And Integer.Parse(row.Cells(39).Value.ToString()) = CustomVariables.CurrentUserID Then
                        Dim curfile As String = row.Cells(8).Value.ToString()
                        msg = "You can't start on this file yet. " & vbNewLine & "Submit [" & curfile & "] first."
                        isPass = False
                        Exit For
                    End If
                Next
                If Not isPass Then
                    MessageBox.Show(Me, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
            'let the user select the file if the activeUser is equal to the current user or the activeUser is 0
            If SelectedRow.Cells(39).Value = CustomVariables.CurrentUserID Or SelectedRow.Cells(39).Value = 0 Then
                'prevent production users from selecting a file when there's still rush files left
                If CustomVariables.CurrentUserPosition.ToLower() = "prod" Then
                    Dim rushFile As List(Of String) = New List(Of String)
                    For Each row As DataGridViewRow In dgv.Rows
                        If CInt(row.Cells(2).Value) = True Then
                            rushFile.Add("- " & row.Cells(8).Value.ToString())
                        End If
                    Next
                    If SelectedRow.Cells(2).Value <> True And rushFile.Count <> 0 Then
                        If rushFile.Count <> 0 Then MessageBox.Show("You still have " & rushFile.Count & " rush file(s)." & vbNewLine & " Rush Files:" & vbNewLine & String.Join(vbNewLine, rushFile.ToArray()), "Rush found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
                    End If
                End If

                Dim FileID As Integer = Integer.Parse(SelectedRow.Cells(0).Value)
                'set the activeUser and active status if current user is not a scheduler
                If Not CustomVariables.CurrentUserPosition Like "sched" Then
                    Dim qry As String = "UPDATE dbo.Main SET activeUser=@activeUser,active=@active WHERE Id=" & FileID.ToString()
                    Try
                        db.nQuery(qry, New String() {"activeUser", CustomVariables.CurrentUserID, "active", "True"})
                    Catch ex As Exception
                        cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & qry, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
                        MessageBox.Show(Me, "Something went wrong while trying to update active status of the file.", "Error Encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    dgv(36, e.RowIndex).Value = True
                End If
                FileIsSelected = True
                'update file information fields, priorities, work file and assign user if current selected file id is not equal to the last selected file id
                If CustomVariables.FileID <> FileID Then
                    'making sure that the main gridview is not empty
                    If dgv.Rows.Count > 0 Then

                        If SelectedRow.Cells(2).Value Then chk_rush.Checked = True
                        txt_receive.Text = SelectedRow.Cells(3).Value.ToString()
                        Try
                            servPicker.Value = Date.Parse(SelectedRow.Cells(4).Value.ToString())
                        Catch

                        End Try

                        txt_submit.Text = SelectedRow.Cells(5).Value.ToString()
                        txt_client.Text = SelectedRow.Cells(6).Value.ToString()
                        txt_branch.Text = SelectedRow.Cells(7).Value.ToString()
                        txt_audio.Text = SelectedRow.Cells(8).Value.ToString()
                        txt_duration.Text = SelectedRow.Cells(11).Value.ToString()
                        txt_document.Text = SelectedRow.Cells(12).Value.ToString()

                        Select Case CustomVariables.CurrentUserPosition
                            Case "admin", "ts", "auditor"
                                txt_page.Text = SelectedRow.Cells(15).Value.ToString()

                                cbo_bt.SelectedValue = SelectedRow.Cells(24).Value
                                cbo_qabt.SelectedValue = SelectedRow.Cells(25).Value
                                cbo_pr.SelectedValue = SelectedRow.Cells(26).Value
                                cbo_qapr.SelectedValue = SelectedRow.Cells(27).Value
                                cbo_st.SelectedValue = SelectedRow.Cells(28).Value
                                cbo_qast.SelectedValue = SelectedRow.Cells(29).Value
                                cbo_cc.SelectedValue = SelectedRow.Cells(30).Value
                                cbo_qacc.SelectedValue = SelectedRow.Cells(31).Value
                            Case "sched"
                                txt_page.Text = SelectedRow.Cells(15).Value.ToString()
                                Select Case CustomVariables.CurrentUserDepartment
                                    Case "bt"
                                        cbo_bt.SelectedValue = SelectedRow.Cells(24).Value
                                        cbo_qabt.SelectedValue = SelectedRow.Cells(25).Value
                                    Case "pr"
                                        cbo_pr.SelectedValue = SelectedRow.Cells(26).Value
                                        cbo_qapr.SelectedValue = SelectedRow.Cells(27).Value
                                    Case "bet"
                                        If CustomVariables.CurrentUDept = "st" Then
                                            cbo_st.SelectedValue = SelectedRow.Cells(28).Value
                                            cbo_qast.SelectedValue = SelectedRow.Cells(29).Value
                                        Else
                                            cbo_cc.SelectedValue = SelectedRow.Cells(30).Value
                                            cbo_qacc.SelectedValue = SelectedRow.Cells(31).Value
                                        End If
                                End Select
                            Case "prod"
                                If CustomVariables.CurrentUserDepartment = "bet" Then
                                    txt_page.Text = SelectedRow.Cells(15).Value.ToString()
                                End If
                        End Select

                        CustomVariables.RecDate = SelectedRow.Cells(3).Value.ToString()
                        CustomVariables.Client = SelectedRow.Cells(6).Value.ToString()
                        CustomVariables.Branch = SelectedRow.Cells(7).Value.ToString()
                        CustomVariables.SoundFile = SelectedRow.Cells(8).Value.ToString()
                        CustomVariables.ServSound = SelectedRow.Cells(9).Value.ToString()
                        CustomVariables.DocName = SelectedRow.Cells(12).Value.ToString()
                        CustomVariables.ServDoc = SelectedRow.Cells(13).Value.ToString()
                        CustomVariables.BTID = Integer.Parse(SelectedRow.Cells(24).Value)
                        CustomVariables.PRID = Integer.Parse(SelectedRow.Cells(26).Value)
                        CustomVariables.QAPRID = Integer.Parse(SelectedRow.Cells(27).Value)
                        CustomVariables.RetDirectory = SelectedRow.Cells(38).Value.ToString()

                        WorkFlow = SelectedRow.Cells(34).Value.ToString()
                        CurrentFlow = Integer.Parse(SelectedRow.Cells(35).Value)

                        'set qa user variable
                        If CustomVariables.CurrentUserPosition = "prod" Then
                            If CustomVariables.CurrentUDept = "bt" Then
                                If SelectedRow.Cells(25).Value <> 0 Then QAUserID = Integer.Parse(SelectedRow.Cells(25).Value)
                            ElseIf CustomVariables.CurrentUDept = "pr" Then
                                If SelectedRow.Cells(27).Value <> 0 Then QAUserID = Integer.Parse(SelectedRow.Cells(27).Value)
                            ElseIf CustomVariables.CurrentUDept = "st" Then
                                If SelectedRow.Cells(29).Value <> 0 Then QAUserID = Integer.Parse(SelectedRow.Cells(29).Value)
                            ElseIf CustomVariables.CurrentUDept = "cc" Then
                                If SelectedRow.Cells(31).Value <> 0 Then QAUserID = Integer.Parse(SelectedRow.Cells(31).Value)
                            End If
                        End If

                        Dim qry As String = "UPDATE dbo.Main SET activeUser=@activeUser,active=@active WHERE Id=" & CustomVariables.FileID
                        Try
                            db.nQuery(qry, New String() {"activeUser", 0, "active", "False"})

                            Dim SelectedRowIndex As Integer = e.RowIndex
                            If LastSelectedRowIndex <> vbNull Then SelectedRowIndex = LastSelectedRowIndex
                            dgv(36, SelectedRowIndex).Value = False
                            If LastSelectedRowIndex <> vbNull Then LastSelectedRowIndex = e.RowIndex
                        Catch ex As Exception
                            cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & qry, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
                            MessageBox.Show(Me, "Something went wrong while trying to update active status of the file.", "Error Encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                End If

                CustomVariables.FileID = Integer.Parse(SelectedRow.Cells(0).Value)
                LoadData()
            End If
        End If
    End Sub
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        If CustomVariables.FileID <> 0 Then
            Dim AddWhere As String = ""
            If CustomVariables.CurrentUserPosition.ToLower() <> "admin" Then
                AddWhere = " And activeUser = " & CustomVariables.CurrentUserID
            End If
            Dim qry As String = "UPDATE dbo.Main SET activeUser=@activeUser,active=@active WHERE Id=" & CustomVariables.FileID & " " & AddWhere
            Try
                db.nQuery(qry, New String() {"activeUser", 0, "active", "False"})
                FileIsSelected = False
                For Each row As DataGridViewRow In Me.main_gridview.Rows
                    If CInt(row.Cells(0).Value) = CustomVariables.FileID Then
                        row.Cells(35).Value = False
                        Exit For
                    End If
                Next
                ClearForm()
                CustomVariables.FileID = 0
                LoadData()
            Catch ex As Exception
                cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & qry, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
            End Try
        End If
    End Sub
    '***********************************************************************
    'Sub routines
    '***********************************************************************
    ''' <summary>
    ''' The default variables and functions that to be set when the user is logged in.
    ''' </summary>
    ''' <param name="dr"></param>
    Private Sub LoginDefault(dr As DialogResult)
        If dr = DialogResult.OK Then
            Me.Text = "BT TOOL [" & CustomVariables.CurrentUserName.ToUpper() & "]"

            IdleTimer.Start()
            WaitTimer.Start()

            LoadComboBoxData()
            LoadData()
        Else
            Me.Dispose()
        End If
    End Sub
    ''' <summary>
    ''' Load data to main gridview.
    ''' </summary>
    Private Async Sub LoadData()
        Me.Cursor = Cursors.AppStarting
        lbl_gridview_status.Visible = True
        lbl_gridview_status.BackColor = Color.FromArgb(0, 122, 204)
        lbl_gridview_status.Text = "Fetching data..."

        Dim newDt As New Data.DataTable
        Dim dt As New Data.DataTable
        Dim selRow As Long = 0
        Dim whereClause As String = "status=@status"
        Dim bind() As String = New String() {}
        If Not IsNothing(Me.main_gridview.CurrentRow) Then
            selRow = Me.main_gridview.SelectedRows(0).Cells(0).Value
        End If
        Select Case CustomVariables.CurrentUserPosition
            Case "admin", "it", "opsup"
                whereClause = ""
            Case "ts"
                whereClause = "status NOT IN ('done','for audit','archive')"
            Case "sched"
                Select Case CustomVariables.CurrentUserDepartment
                    Case "bt"
                        whereClause = "status IN ('BT Sched','for bt', 'for bt qa')"
                    Case "pr"
                        whereClause = "status In ('PR Sched','for bt','for pr', 'for bt qa', 'for pr qa')"
                    Case "bet"
                        whereClause = "status IN ('" & CustomVariables.CurrentUDept & " Sched','for " & CustomVariables.CurrentUDept & "', 'for " & CustomVariables.CurrentUDept & " qa' )"
                End Select
            Case "prod"
                'whereClause = "(status=@status AND " & varMod.uDept & " = @uid) OR (qa" & varMod.uDept & " = CASE status WHEN 'for " & varMod.uDept & " qa' THEN @uid ELSE NULL END)"
                'bind = New String() {"status", "for " & varMod.uDept, "uid", varMod.uid}
                'db.bind("status", "for " & varMod.uDept)

                whereClause = "( " & CustomVariables.CurrentUDept & " = @uid)"
                bind = New String() {"uid", CustomVariables.CurrentUserID}'{varMod.uDept, "uid", varMod.uid}
            Case "auditor"
                bind = New String() {"status", "For ts"}
                'db.bind("status", "For ts")
            Case "tl"
                Select Case CustomVariables.CurrentUserDepartment
                    Case "bt"
                        whereClause = "status IN ('BT Sched','for bt')"
                    Case "pr"
                        whereClause = "status In ('PR Sched','for pr')"
                    Case "bet"
                        whereClause = "status IN ('" & CustomVariables.CurrentUDept & " Sched','for " & CustomVariables.CurrentUDept & "', 'for " & CustomVariables.CurrentUDept & " qa')"
                End Select
        End Select

        Dim additionalWhere As String = ""
        If CustomVariables.CurrentUserIsBTWork = True Then
            additionalWhere = " OR (bt = " & CustomVariables.CurrentUserID & " AND status LIKE 'For BT')"
        End If

        If whereClause IsNot "" Then
            whereClause = "WHERE " & whereClause & additionalWhere
        End If

#Region "Gridview column number reference"
        '0  = id           | 1  = Prio      | 2  = Rush      | 3  = Receive      | 4  = Service    | 5  = Due        | 6 = Client
        '7  = Branch       | 8  = Soundfile | 9  = servSound | 10 = locSound     | 11 = Duration   | 12 = wFile      | 13 = servDoc
        '14 = servLoc      | 15 = page      | 16 =  BT       | 17 = QA BT        | 18 = PR         | 19 = QA PR      | 20 = S&T
        '21 = QA S&T       | 22 = CC        | 23 = QA CC     | 24 = btid         | 25 = qabtid     | 26 = prid       | 27 = qaprid
        '28 = stid         | 29 = qastid    | 30 = ccid      | 31 = qaccid       | 32 = Accuracy   | 33 = Status     | 34 = Workflow
        '35 = Current Flow | 36 = Active    | 37 = retFile   | 38 = retDirectory | 39 = activeUser | 40 = rowEnabled | 41 = isHold
#End Region
        Dim ColField() As String = New String() {"id", "Prio", "Rush", "Receive", "Service", "Due", "Client", "Branch", "Soundfile", "servSound", "locSound",'10
            "Duration", "wFile", "servDoc", "servLoc", "Page",'15
            "BT", "QA BT", "PR", "QA PR", "S&T", "QA ST", "CC", "QA CC",'23
            "btid", "qabtid", "prid", "qaprid", "stid", "qastid", "ccid", "qaccid",'31
            "Accuracy", "Status", "Workflow", "Current Flow", "Active", "retFile", "retDirectory", "activeUser", "rowEnabled", "isHold"} '41

#Region "Gridview Column properties"
        Dim headers As New List(Of GridHeaders)() : Dim colIndex As Integer = 0
        For Each col As String In ColField
            Dim colType As Type = GetType(String) : Dim colVisible As Boolean = True
            'Set the column type of specific columns
            If New String() {"Prio", "Rush", "Active", "rowEnabled", "isHold"}.Contains(col) Then colType = GetType(Boolean)
            'Set the visibility of the specific columns
            If New String() {"id", "Prio", "servSound", "locSound", "wFile", "servDoc", "servLoc", "btid", "qabtid", "prid", "qaprid", "stid", "qastid", "ccid", "qaccid", "Accuracy",
                "Workflow", "Current Flow", "retFile", "retDirectory", "activeUser", "rowEnabled", "isHold"}.Contains(col) Then colVisible = False

            Dim colVFalse() As String = New String() {} : Dim colVTrue() As String = New String() {}
            Select Case CustomVariables.CurrentUserPosition
                Case "admin"
                    colVFalse = New String() {}
                    colVTrue = New String() {"Receive", "Page", "BT", "QA BT", "PR", "QA PR"}
                Case "prod"
                    colVFalse = New String() {"Receive", "servLoc", "Page", "BT", "QA BT", "PR", "QA PR", "S&T", "QA ST", "CC", "QA CC", "btid", "qabtid", "prid", "qaprid", "stid",
                        "qastid", "ccid", "qaccid", "Accuracy", "Active", "retFile"}
                    colVTrue = New String() {}
                Case "sched"
                    Select Case CustomVariables.CurrentUserDepartment
                        Case "bt"
                            colVFalse = New String() {"PR", "QA PR", "S&T", "QA ST", "CC", "QA CC"}
                            colVTrue = New String() {"Page", "BT", "QA BT"}
                        Case "pr"
                            colVFalse = New String() {"Page", "S&T", "QA ST", "CC", "QA CC"}
                            colVTrue = New String() {"BT", "QA BT", "PR", "QA PR"}
                        Case "bet"
                            colVFalse = New String() {"Page", "BT", "QA BT", "PR", "QA PR"}
                            If CustomVariables.CurrentUDept = "st" Then
                                colVTrue = New String() {"S&T", "QA ST"}
                            Else
                                colVTrue = New String() {"CC", "QA CC"}
                            End If
                    End Select
            End Select
            If colVFalse.Contains(col) Then colVisible = False
            If colVTrue.Contains(col) Then colVisible = True
            'Set column width
            Dim colW As Integer = 0
            If col = "Client" Then colW = 60
            If col = "Branch" Then colW = 85
            If col = "Soundfile" Then colW = 145
            If col = "Due" Then colW = 120
            If New String() {"Receive", "Service", "Duration"}.Contains(col) Then colW = 75
            If New String() {"Rush", "Page", "BT", "QA BT", "PR", "QA PR", "S&T", "QA ST", "CC", "QA CC", "Status", "Active"}.Contains(col) Then colW = 50

            headers.Add(New GridHeaders() With {.index = colIndex, .text = col, .type = colType, .visible = colVisible, .width = colW})

            colIndex = colIndex + 1
        Next

        For Each itm As GridHeaders In headers
            newDt.Columns.Add(itm.text, itm.type)
        Next
#End Region
        Try
            Dim ndt As DataTable = Await Task.Run(
                Function()
#Region "Table column number reference"
                    '0  = Id       | 1  = prio   | 2  = rush      | 3  = dateRec      | 4  = dateServ   | 5  = dateDue  | 6 = client
                    '7  = branch   | 8  = sFile  | 9  = servSound | 10 = locSound     | 11 = duration   | 12 = wFile    | 13 = servDoc
                    '14 = locDoc   | 15 = page   | 16 =  btname   | 17 = qabtname     | 18 = prname     | 19 = qaprname | 20 = stname
                    '21 = qastname | 22 = ccname | 23 = qaccname  | 24 = bt           | 25 = qabt       | 26 = pr       | 27 = qapr
                    '28 = st       | 29 = qast   | 30 = cc        | 31 = qacc         | 32 = accuracy   | 33 = status   | 34 = flow
                    '35 = cFlow    | 36 = active | 37 = retFile   | 38 = retDirectory | 39 = activeUser | 40 = timeDue  | 41 = onhold
#End Region
                    Dim qry As String = "SELECT Id,prio,rush,dateRec,dateServ,dateDue,client,branch,sFile,servSound,locSound," & '10
                    "duration,wFile,servDoc,locDoc,page,(SELECT username FROM dbo.UserData WHERE Id = bt) AS btname," &'16
                    "(SELECT username FROM dbo.UserData WHERE Id = qabt) AS qabtname," &'17
                    "(SELECT username FROM dbo.UserData WHERE Id = pr) AS prname," &'18
                    "(SELECT username FROM dbo.UserData WHERE Id = qapr) AS qaprname," &'19
                    "(SELECT username FROM dbo.UserData WHERE Id = st) AS stname," &'20
                    "(SELECT username FROM dbo.UserData WHERE Id = qast) AS qastname," &'21
                    "(SELECT username FROM dbo.UserData WHERE Id = cc) AS ccname," &'22
                    "(SELECT username FROM dbo.UserData WHERE Id = qacc) AS qaccname,bt,qabt,pr,qapr,st,qast,cc,qacc," &'31
                    "accuracy,status,flow,cFlow,active,retFile,retDirectory,activeUser,timeDue,onhold FROM dbo.Main " &'41
                    whereClause & " ORDER BY CASE rush WHEN 1 THEN 0 ELSE 1 END,CASE WHEN dateDue IS NULL THEN 1 ELSE 0 END, CAST(dateDue AS date),
                    CASE WHEN timeDue IS NULL THEN 1 ELSE 0 END,CAST(timeDue AS time)"
                    Try
                        dt = db.query(qry, bind)
                    Catch
                        ErrorQuery = qry
                        Throw
                    End Try
                    Dim rowIndex As Integer = 0
                    For Each rows As DataRow In dt.Rows
                        Dim row As Object() = rows.ItemArray
                        Dim dtTime As DateTime : Dim convertedTime As String
                        Try
                            dtTime = DateTime.Parse(row(40))
                            convertedTime = dtTime.ToString("h:mm tt")
                        Catch ex As Exception
                            convertedTime = ""
                        End Try
                        '*****************************
                        Dim rowEnabled As Boolean = True
                        Dim isHold As Boolean = Boolean.Parse(row(41))
                        If rowIndex > 0 And New String() {"prod"}.Contains(CustomVariables.CurrentUserPosition) And Not isHold Then rowEnabled = False
                        '****************************

                        newDt.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5) & " " & convertedTime, row(6), row(7), row(8), row(9), row(10), row(11), row(12), row(13), row(14), row(15), row(16), row(17), row(18),
                            row(19), row(20), row(21), row(22), row(23), row(24), row(25), row(26), row(27), row(28), row(29), row(30), row(31), row(32), row(33), row(34), row(35), row(36), row(37), row(38), row(39), rowEnabled, isHold)

                        rowIndex = rowIndex + 1
                    Next
                    Return newDt
                End Function)

            main_gridview.DataSource = ndt
            For Each itm As GridHeaders In headers
                main_gridview.Columns(itm.index).Width = itm.width
                main_gridview.Columns(itm.index).Visible = itm.visible
            Next

            cf.RowsNumber(main_gridview)
            For Each row As DataGridViewRow In Me.main_gridview.Rows
                If row.Cells(0).Value.ToString = selRow Then
                    Dim index As Integer = row.Index
                    row.Selected = True 'either this or the bottom line would work
                    Me.main_gridview.Rows(row.Index).Selected = True
                End If

                If row.Cells(41).Value Then 'check if on hold
                    row.DefaultCellStyle.BackColor = Color.DarkRed
                    row.DefaultCellStyle.ForeColor = Color.White
                End If
                If Not row.Cells(40).Value Then 'check if row is enabled
                    row.DefaultCellStyle.ForeColor = Color.Gray
                End If
            Next
            lbl_gridview_status.Visible = False
        Catch ex As Exception
            lbl_gridview_status.Text = "Error encountered while trying to retrieve the data."
            lbl_gridview_status.BackColor = Color.DarkRed
            cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & ErrorQuery, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
        End Try

        Me.Cursor = Cursors.Default
    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    Private Sub LoadComboBoxData()
        GboxUList.Text = "Assign User: [Fetching data...]"
        GboxUList.ForeColor = Color.Black

        Dim dt As New DataTable
        Dim qry As String = "SELECT Id,UPPER(LEFT(username,1))+LOWER(SUBSTRING(username,2,LEN(username))) AS uname,uDept,department,btwork,position FROM dbo.UserData WHERE deactivated = 0 ORDER BY department,username"
        Try
            dt = db.query(qry)

            Dim dv As New DataView(dt)
            dt.Rows.Add(0, "", "", "bt", 0, "prod")
            dt.Rows.Add(0, "", "", "pr", 0, "prod")
            dt.Rows.Add(0, "", "st", "bet", 0, "prod")
            dt.Rows.Add(0, "", "cc", "bet", 0, "prod")
            dt.AcceptChanges()
            '**********bt************
            dv.RowFilter = "department LIKE 'bt' OR btwork = 1 AND position LIKE 'prod'"

            cbo_bt.DisplayMember = "uname"
            cbo_bt.ValueMember = "Id"
            cbo_bt.DataSource = dv
            cbo_bt.SelectedValue = 0


            dv = New DataView(dt)
            dv.RowFilter = "department LIKE 'bt' AND position LIKE 'prod'"

            cbo_qabt.DisplayMember = "uname"
            cbo_qabt.ValueMember = "Id"
            cbo_qabt.DataSource = dv
            cbo_qabt.SelectedValue = 0

            '**********pr************
            dv = New DataView(dt)
            dv.RowFilter = "department LIKE 'pr' AND position LIKE 'prod'"

            cbo_pr.DisplayMember = "uname"
            cbo_pr.ValueMember = "Id"
            cbo_pr.DataSource = dv
            cbo_pr.SelectedValue = 0

            dv = New DataView(dt)
            dv.RowFilter = "department LIKE 'pr' AND position LIKE 'prod'"

            cbo_qapr.DisplayMember = "uname"
            cbo_qapr.ValueMember = "Id"
            cbo_qapr.DataSource = dv
            cbo_qapr.SelectedValue = 0

            '*********s&t***********
            dv = New DataView(dt)
            dv.RowFilter = "uDept LIKE 'st' AND position LIKE 'prod'"

            cbo_st.DisplayMember = "uname"
            cbo_st.ValueMember = "Id"
            cbo_st.DataSource = dv
            cbo_st.SelectedValue = 0

            dv = New DataView(dt)
            dv.RowFilter = "uDept LIKE 'st' AND position LIKE 'prod'"

            cbo_qast.DisplayMember = "uname"
            cbo_qast.ValueMember = "Id"
            cbo_qast.DataSource = dv
            cbo_qast.SelectedValue = 0

            '*********cc***********
            dv = New DataView(dt)
            dv.RowFilter = "uDept LIKE 'cc' AND position LIKE 'prod'"

            cbo_cc.DisplayMember = "uname"
            cbo_cc.ValueMember = "Id"
            cbo_cc.DataSource = dv
            cbo_cc.SelectedValue = 0

            dv = New DataView(dt)
            dv.RowFilter = "uDept LIKE 'cc' AND position LIKE 'prod'"

            cbo_qacc.DisplayMember = "uname"
            cbo_qacc.ValueMember = "Id"
            cbo_qacc.DataSource = dv
            cbo_qacc.SelectedValue = 0

            GboxUList.Text = "Assign User:"
        Catch ex As Exception
            GboxUList.Text = "Assign User: [Error encountered]"
            GboxUList.ForeColor = Color.DarkRed
            cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & qry, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
        End Try
    End Sub
    ''' <summary>
    ''' This will clear textbox and combobox
    ''' </summary>
    Private Sub ClearForm()
        'checkboxes
        chk_blank.Checked = False : chk_rush.Checked = False
        'textbox
        txt_submit.Text = "" : txt_client.Text = "" : txt_branch.Text = ""
        txt_receive.Text = "" : txt_duration.Text = "" : txt_page.Text = ""
        txt_accuracy.Text = "" : txt_audio.Text = "" : txt_document.Text = ""
        'comboboxes
        cbo_bt.SelectedValue = 0 : cbo_pr.SelectedValue = 0 : cbo_cc.SelectedValue = 0 : cbo_st.SelectedValue = 0
        cbo_qabt.SelectedValue = 0 : cbo_qapr.SelectedValue = 0 : cbo_qacc.SelectedValue = 0 : cbo_qast.SelectedValue = 0
        'datepicker
        servPicker.Value = DateTime.Now.ToShortDateString()
    End Sub
    '***********************************************************************
    'Functions
    '***********************************************************************

    '***********************************************************************
    'Background Workers
    '***********************************************************************
    ''' <summary>
    ''' Background Worker for idle and wait timer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BWTimer_DoWork(sender As Object, e As DoWorkEventArgs) Handles BWTimer.DoWork
        Dim dgv As DataGridView = CType(e.Argument, DataGridView)
        If dgv.Rows.Count = 0 Then
            e.Result = "wait"
            If Not isWait Then
                waitSTime = Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                isWait = True
            End If
        Else
            If isWait Then
                db.nQuery("INSERT INTO dbo.tbl_wait (uid,stime,etime) VALUES (" & CustomVariables.CurrentUserID & ",'" & waitSTime & "','" & Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) & "')")
                isWait = False
            End If
            e.Result = "idle"

            If CustomVariables.LockTime > 0 Then
                'lock user after five minutes
                lastInputInf.cbSize = Marshal.SizeOf(lastInputInf)
                lastInputInf.dwTime = 0
                GetLastInputInfo(lastInputInf)
                last_tick = el_tick
                el_tick = CInt((Environment.TickCount - lastInputInf.dwTime) / 1000)

                If user_idle Then
                    If CInt((Environment.TickCount - lastInputInf.dwTime) / 1000) = 0 Then
                        user_idle = False
                        Dim idle_exist As String() = db.row("SELECT id FROM dbo.tbl_idle WHERE uid=" & CustomVariables.CurrentUserID & "AND time=0")
                        If idle_exist(0) <> "" Then
                            db.nQuery("UPDATE dbo.tbl_idle SET time=@time WHERE id=" & idle_exist(0), New String() {"time", last_tick - (CustomVariables.LockTime * 60)})
                        End If
                    End If
                Else
                    If el_tick > (CustomVariables.LockTime * 60) Then
                        db.nQuery("INSERT INTO dbo.tbl_idle (time,date,uid,itime) VALUES (@time,@date,@uid,@itime)", New String() {"date", Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), "time", 0, "itime", Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture), "uid", CustomVariables.CurrentUserID})
                        user_idle = True
                    End If
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Background Worker for idle and wait timer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BWTimer_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BWTimer.RunWorkerCompleted
        If e.Error Is Nothing Then
            If CStr(e.Result) = "wait" Then
                IdleTimer.Enabled = False
            Else
                IdleTimer.Enabled = True
            End If
        ElseIf e.Cancelled Then
            MessageBox.Show(Me, "The process has been cancelled", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            MessageBox.Show(Me, e.Error.ToString, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        LoadData()
    End Sub

    Private Sub btn_file_Click(sender As Object, e As EventArgs) Handles btn_file.Click
        If Not cf.IsStringEmpty(txt_receive.Text) Then
            Try
                Dim CurDT As DateTime = DateTime.Parse(txt_receive.Text)
                Dim DownloadFolderName As String = "DOWNLOAD-" & Me.txt_client.Text & "-" & CurDT.ToString("MMddyyyy", CultureInfo.InvariantCulture)
                Dim DocumentDirectory As String = Path.Combine(CustomVariables.BaseServer, "FROOT\TRANSCRIPT", "BT", main_gridview.SelectedRows(0).Cells(16).Value, DownloadFolderName, txt_branch.Text, txt_document.Text)
                Dim DesktopDirectory As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Desktop, txt_document.Text)
                If File.Exists(DocumentDirectory) Then
                    File.Copy(DocumentDirectory, DesktopDirectory, True)
                    Process.Start(DesktopDirectory)
                Else
                    MessageBox.Show(Me, "File does not exist", "Missing BT File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                cf.WriteToFile("{0}==>" & ex.ToString() & vbNewLine & ErrorQuery, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\\" & DebugTextFile)
                MessageBox.Show(Me, "Something went wrong while trying to copy the BT File from the server.", "Error Encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub
End Class
