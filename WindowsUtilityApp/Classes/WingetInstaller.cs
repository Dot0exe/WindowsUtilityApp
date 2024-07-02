using System.Diagnostics;
using System.Collections.Generic;
using WindowsUtilityApp;

public class WingetInstaller
{
    public void InstallApps(List<ApplicationInfo> apps)
    {
        foreach (var app in apps)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "winget";
                process.StartInfo.Arguments = $"install {app.Id}";
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
