# VBA-DLL-WMI-EXECUTION
Call your own DLL from VBA and execute code under process svchost.exe with WMI

This PoC show how one can create a DLL in C# and call it form VBA, the C# code is compiled with batch script described and entrypoint is inserted.

It consist of 4 files

VBA macro newmacros.vba
C# code wmiexec.cs
C# code DateAdd.cs
C code shell.cpp

Execution flow: VBA ---> DateAdd.dll ---> rundll32.exe shell32,Control_RunDLL C:\Windows\Tasks\shell.cpl

Some note regarding embedded base64, the binary files wmiexec.exe and shell.cpl, I remove MZ / hex 4D5A from file header BEFORE
creating base64 string with certutil -encode wmiexec.exe vmiexec.b64

Edit vmiexec.b64 with notepad++ and replace fx / with # and insert ! or other character of choise. Do the same with shell.cpl

Why all the fuzz ?, well you don't have to, but if one know what AV/EDR looks for, this is the first step when creating binary files.

PoC vid: https://www.youtube.com/watch?v=0dEMQ_Iht98
