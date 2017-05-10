namespace Codenesium.ConfigDragon
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using CommandLine;
    using ConfigDragonLib;

    /// <summary>
    /// Main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry points
        /// </summary>
        /// <param name="args">Program arguments</param>
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args)
              .WithParsed(options =>
              {
                  if (!File.Exists(options.ConfigFile))
                  {
                      Console.WriteLine($"The supplied config file {options.ConfigFile} does not exist.");
                      Environment.Exit(-1);
                  }

                  var configManager = new ConfigManager();
                  var configContainer = configManager.LoadConfig(options.ConfigFile);
                  var selectedConfig = configContainer.ConfigActions.FirstOrDefault(x => x.Name.ToUpper() == options.ConfigActionName.ToUpper());

                  if (selectedConfig == null)
                  {
                      Console.WriteLine($"The supplied project name {options.ConfigActionName} was not found in the config file {options.ConfigFile}.");
                      Environment.Exit(-1);
                  }

                  if (string.IsNullOrWhiteSpace(configContainer.RepositoryRootDirectory))
                  {
                      string userProfilePath = System.Environment.GetEnvironmentVariable("USERPROFILE");
                      string hgPath = Path.Combine(userProfilePath, @"AppData\Local\Atlassian\SourceTree\hg_local\hg.exe");
                      string gitPath = Path.Combine(userProfilePath, @"AppData\Local\Atlassian\SourceTree\git_local\bin\git.exe");

                      var hgRepositoryRoot = GetHGRepositoryRootPath(hgPath);

                      var gitRepositoryRoot = GetGitRepositoryRootPath(gitPath);

                      if (!string.IsNullOrWhiteSpace(hgRepositoryRoot))
                      {
                          configContainer.RepositoryRootDirectory = hgRepositoryRoot;
                      }

                      if (!string.IsNullOrWhiteSpace(gitRepositoryRoot))
                      {
                          configContainer.RepositoryRootDirectory = gitRepositoryRoot;
                      }
                  }

                  foreach (var configItem in selectedConfig.ConfigItems)
                  {
                      var package = configContainer.ConfigPackages.FirstOrDefault(x => x.Name.ToUpper() == configItem.PackageName.ToUpper());

                      if (package == null)
                      {
                          Console.WriteLine($"The package {configItem.PackageName} is not a valid package name");
                          Environment.Exit(-1);
                      }

                      configManager.ProcessConfig(Path.Combine(configContainer.RepositoryRootDirectory, configItem.Directory), configItem.RelativeFilename, package);
                  }
              })
              .WithNotParsed(errors => { });
        }

        /// <summary>
        /// Attempts to return the mercurial repository root
        /// </summary>
        /// <param name="hGExecutablePath">Location of the HG executable</param>
        /// <returns>Repository root directory or empty</returns>
        private static string GetHGRepositoryRootPath(string hgExecutablePath)
        {
            string response = string.Empty;

            if (File.Exists(hgExecutablePath))
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = hgExecutablePath,
                        Arguments = "root",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();

                proc.WaitForExit();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (!line.ToUpper().Contains("ABORT"))
                    {
                        response = line;
                        break;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Attempts to determine the root directory of a GIT repository
        /// </summary>
        /// <param name="gitExecutablePath">Location of the GIET executable</param>
        /// <returns>Repository root directory or empty</returns>
        private static string GetGitRepositoryRootPath(string gitExecutablePath)
        {
            string response = string.Empty;

            if (File.Exists(gitExecutablePath))
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = gitExecutablePath,
                        Arguments = "rev-parse --show-toplevel",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (!line.ToUpper().Contains("FATAL"))
                    {
                        response = line;
                        break;
                    }
                }
            }

            return response;
        }
    }
}
