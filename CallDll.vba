Declare Function DateDiff Lib "C:\Windows\Tasks\DateFunc.dll" Alias "DateAdd" () As Integer

Sub AutoOpen()
  res = DateDiff()
End Sub
