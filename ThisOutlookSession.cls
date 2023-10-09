VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ThisOutlookSession"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
Option Explicit
Private WithEvents inboxItems As Outlook.Items
Attribute inboxItems.VB_VarHelpID = -1
Private Sub Application_Startup()
  Dim outlookApp As Outlook.Application
  Dim objectNS As Outlook.NameSpace
  
  Set outlookApp = Outlook.Application
  Set objectNS = outlookApp.GetNamespace("MAPI")
  Set inboxItems = objectNS.GetDefaultFolder(olFolderInbox).Items
End Sub
Private Sub inboxItems_ItemAdd(ByVal msItem As Object)

Dim pathFileMessageBody As String
Dim fileProgrammPath As String
Dim FileToCreate
Dim fileMessageBody As String
Dim Command As String

Dim FSO As New FileSystemObject
Set FSO = CreateObject("Scripting.FileSystemObject")
fileMessageBody = "messageBody.txt"

' path for console programm
fileProgrammPath = "C:\Users\tymoshchukmn\Source\Repos\HTMLprocess\HTMLprocess\bin\Debug\HTMLprocess.exe"

' path for saving message body file
pathFileMessageBody = "E:\"

Set FileToCreate = FSO.CreateTextFile(pathFileMessageBody & fileMessageBody, , True)

    If TypeName(msItem) = "MailItem" Then
        If msItem.Subject = "test" Then
            If Dir(fileMessageBody) <> "" Then
                Kill fileMessageBody
            End If
            
            ' create message boby file
            FileToCreate.Write msItem.HTMLBody
            FileToCreate.Close
            
            'set command for execution
            Command = fileProgrammPath
            Shell "cmd /c " & Command, vbMaximizedFocus
            
        End If
    End If
End Sub
