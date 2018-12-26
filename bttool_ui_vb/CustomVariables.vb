﻿Module CustomVariables
    ''' <summary>
    ''' The directory on which the debug error text file will be placed.
    ''' </summary>
    Public DebugDirectory As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    ''' <summary>
    ''' The ID of the current user logged in.
    ''' </summary>
    Public CurrentUserID As Integer
    ''' <summary>
    ''' The name of the current user logged in.
    ''' </summary>
    Public CurrentUserName As String
    ''' <summary>
    ''' Similar with CurrentUserDepartment but is more specific for the BET. <para>This will return cc or st for the BET depatment.</para>
    ''' </summary>
    Public CurrentUDept As String
    ''' <summary>
    ''' The set time for the user before the app is lock
    ''' </summary>
    Public LockTime As Integer
    ''' <summary>
    ''' Returns <see langword="True"/> if the user is online, else <see langword="False"/>
    ''' </summary>
    Public CurrentUserIsActive As Boolean
    ''' <summary>
    ''' The position of the current logged in user
    ''' </summary>
    Public CurrentUserPosition As String
    ''' <summary>
    ''' The department of the current logged in user.
    ''' </summary>
    Public CurrentUserDepartment As String
    ''' <summary>
    ''' Returns <see langword="True"/> if the current user btwork is true, else <see langword="False"/>.
    ''' </summary>
    Public CurrentUserIsBTWork As Boolean
    ''' <summary>
    ''' The directory where all of the working files of the current user is located.
    ''' </summary>
    Public FileLocalDirectory As String
    ''' <summary>
    ''' The base directory that will be used for setting the FileLocalDirectory.
    ''' </summary>
    Public FileLocalBaseDirectory As String
    ''' <summary>
    ''' The directory of My Documents. A.K.A "baseLoc"
    ''' </summary>
    Public MyDocumentsDirectory As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    ''' <summary>
    ''' The directory of My Music. A.K.A "baseSound"
    ''' </summary>
    Public MyMusicDirectory As String = My.Computer.FileSystem.SpecialDirectories.MyMusic
    ''' <summary>
    ''' The server to use throughout the application
    ''' </summary>
    Public BaseServer As String
End Module
