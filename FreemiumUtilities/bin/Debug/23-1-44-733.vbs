'*************************************************
'* VBScript generated by the mScript class       *
'* for dynamic scripting of .NET applications    *
'* this file can be safely delted.               *
'*************************************************

outFile = "23-1-44-73323-1-44-733.txt"

Set oFsObj = CreateObject("Scripting.FileSystemObject")
Set oFHnd = oFsObj.CreateTextFile(outFile, true)

Function iv(variableName)
  iv = inpVal(variableName)
End Function ' iv()

Function inpVal(variableName)
  Select Case UCase(variableName)
    Case Else
      retVal = ""
  End Select
  inpVal = retVal
End Function ' inpVal()

Sub return(varName, retVal)
  oFHnd.WriteLine varName & vbTab & retVal
End Sub ' return()

strComputer = "." 
Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\default") 
Set objItem = objWMIService.Get("SystemRestore")
CSRP = objItem.createrestorepoint ("Free System Utilities", 0, 100)
return 1,1

oFHnd.Close