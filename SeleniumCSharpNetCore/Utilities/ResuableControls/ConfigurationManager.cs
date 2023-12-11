using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dynamitey;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace SeleniumCSharpNetCore.Utilities.ResuableControls
{
    public class ConfigProp
    {
        private JObject _appSettings;

        String pathVFile;
        Process process;

        public string GetExecSetting(string key)
        {
            // Load the JSON file into a JObject
            _appSettings = JObject.Parse(File.ReadAllText(getPathBeforeBin("execSettings.json", "Utilities//Properties")));
            // Retrieve the value for the given key
            return _appSettings[key]?.ToString();
        }



        public string getPathBeforeBin(string fileNameWithExet, string fwdPath = null)
        {
            // Get the assembly (which represents the currently executing code)
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the directory where the assembly (executable) is located
            string assemblyPath = Path.GetDirectoryName(assembly.Location);

            // Find the index of the 'bin' directory in the path
            int binIndex = assemblyPath.IndexOf("bin", StringComparison.OrdinalIgnoreCase);

            string fullPath = "";
            // Check if 'bin' directory is found in the path
            if (binIndex != -1)
            {
                // Extract the path before the 'bin' directory
                string projectPath = assemblyPath.Substring(0, binIndex).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                pathVFile = projectPath;

                // Specify the additional path segment
                if (fwdPath == null)
                {
                    fullPath = Path.Combine(projectPath, fileNameWithExet);
                }
                else
                {
                    string additionalPath = @fwdPath;

                    // Combine the project path with the relative path
                    fullPath = Path.Combine(projectPath, additionalPath, fileNameWithExet);

                    Console.WriteLine("Full path to the target file: " + fullPath);
                }


            }
            else
            {
                Console.WriteLine("Unable to determine project path. 'bin' directory not found in the path.");
            }
            return fullPath;
        }

        public void runCmd()
        {
            if (GetExecSetting("browser").Contains("grid"))
            {

                // Replace with the path to your Bash script
                //  string bashScriptPath = getPathBeforeBin("StartSeleniumGrid2.bat");

                process = new Process
                {
                    StartInfo = new ProcessStartInfo("cmd.exe", $"/c java -jar .\\selenium-server-4.16.1.jar standalone")
                    {
                        // WorkingDirectory = "C:\\Users\\Admin\\Downloads\\SeleniumCSharpNetCore-main\\SeleniumCSharpNetCore-main\\SeleniumCSharpNetCore",
                        WorkingDirectory = pathVFile,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    }
                };

                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("output :: " + e.Data);

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("error :: " + e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("ExitCode: {0}", process.ExitCode);

            }

        }

        public void CloseCmd()
        {
            process.Close();
        }

    }
}
