using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Editor_Mod;
using System.Windows.Forms;
using Microsoft.Win32;
namespace starter
{
    static class Program
    {
        public static string GamePath = "";
        static List<Process> GetProcessesByName(string machine, string filter, RegexOptions options)
        {
            List<Process> processList = new List<Process>();
            Process[] runningProcesses = Process.GetProcesses(machine);
            Regex processFilter = new Regex(filter, options);
            foreach (Process current in runningProcesses)
            {
                if (processFilter.IsMatch(current.ProcessName))
                {
                    processList.Add(current);
                }

                else current.Dispose();
            }
            return processList;
        }
        static bool FindGame()
        {
            var steamCandidates = GetProcessesByName(".", "steam", RegexOptions.IgnoreCase);
            foreach (var p in steamCandidates)
            {
                try
                {
                    if (p.MainModule.ModuleName.ToLower() == "steam.exe")
                    {
                        try
                        {

                            GamePath = (string)Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("Valve").OpenSubKey("Steam").GetValue("SteamPath");
                            if (GamePath != "")
                            {
                                GamePath += @"\steamapps\common\terraria\";
                                return true;
                            }

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error accessing registry: " + e);
                            MessageBox.Show("Will proceed with detecting executable");
                        }
                        FileInfo f = new FileInfo(p.MainModule.FileName);
                        var d = f.Directory.GetDirectories("steamapps");
                        if (d.Count() > 0)
                        {
                            d = d[0].GetDirectories("common");
                            if (d.Count() > 0)
                            {
                                d = d[0].GetDirectories("terraria");
                                if (d.Count() > 0)
                                {
                                    GamePath = d[0].FullName;
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // Ignore this...means we were looking at SteamService.
                }
            }
            return false;
        }
        static void setfolder()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("Terraria"))
            {

                var asm = Assembly.LoadFile(GamePath + @"\Terraria.exe");
                return asm;
            }
            return null;
        }
        [STAThread]
        static void Main()
        {
            if (!FindGame())
            {
                MessageBox.Show("Steam if off or you have not downloaded the game.", "Steam is not running or couldn't find the Terraria.exe",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            setfolder();
            Directory.SetCurrentDirectory(GamePath);
            try
            {
                Editor_Mod.Start.Entry(GamePath);
            }
            finally
            {

            }
        }

    }
}