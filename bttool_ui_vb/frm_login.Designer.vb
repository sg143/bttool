<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_login))
        Me.txt_username = New System.Windows.Forms.TextBox()
        Me.txt_pass = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_login = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_err = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txt_username
        '
        Me.txt_username.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txt_username.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.txt_username.BackColor = System.Drawing.SystemColors.Window
        Me.txt_username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_username.Location = New System.Drawing.Point(12, 115)
        Me.txt_username.Name = "txt_username"
        Me.txt_username.Size = New System.Drawing.Size(182, 21)
        Me.txt_username.TabIndex = 0
        '
        'txt_pass
        '
        Me.txt_pass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txt_pass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.txt_pass.BackColor = System.Drawing.SystemColors.Window
        Me.txt_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_pass.Location = New System.Drawing.Point(12, 158)
        Me.txt_pass.Name = "txt_pass"
        Me.txt_pass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_pass.Size = New System.Drawing.Size(182, 21)
        Me.txt_pass.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Username:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password:"
        '
        'btn_login
        '
        Me.btn_login.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btn_login.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_login.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_login.ForeColor = System.Drawing.Color.White
        Me.btn_login.Image = CType(resources.GetObject("btn_login.Image"), System.Drawing.Image)
        Me.btn_login.Location = New System.Drawing.Point(12, 194)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(182, 29)
        Me.btn_login.TabIndex = 4
        Me.btn_login.Text = "Login"
        Me.btn_login.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_login.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_login.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Old English Text MT", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 38)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Accolade"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Roboto", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(130, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "BT TOOL"
        '
        'lbl_err
        '
        Me.lbl_err.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl_err.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_err.ForeColor = System.Drawing.Color.White
        Me.lbl_err.Location = New System.Drawing.Point(12, 49)
        Me.lbl_err.Name = "lbl_err"
        Me.lbl_err.Size = New System.Drawing.Size(182, 47)
        Me.lbl_err.TabIndex = 7
        Me.lbl_err.Text = "Error Message"
        Me.lbl_err.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbl_err.Visible = False
        '
        'frm_login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(206, 236)
        Me.Controls.Add(Me.lbl_err)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_login)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_pass)
        Me.Controls.Add(Me.txt_username)
        Me.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_username As TextBox
    Friend WithEvents txt_pass As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_login As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbl_err As Label
End Class
