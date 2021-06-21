using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AutoViewWebAdsCSharp.IO
{
    public class CommandLine
    {
        public static void ExecuteAsAdmin(string fileName)
        {
            Process proc = new();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }

        // Thực thi lệnh command với kết quả trả về là chuỗi
        public static string ExecuteCommandLine(string commandTxt)
        {
            string result;
            try
            {
                Process process = new();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    FileName = @"D:\LDPlayer\LDPlayer3.0\ldconsole.exe",
                    CreateNoWindow = true,
                    Arguments = commandTxt,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                };
                process.Start();
                process.StandardInput.Flush();
                process.StandardInput.Close();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}
