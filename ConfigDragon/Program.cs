namespace Codenesium.ConfigDragon
{
    using System;
    using System.IO;
    using System.Linq;
    using CommandLine;
    using ConfigDragonLib;
    using ConfigDragonLib.Logging;

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
                  var configManager = new ConfigManager();

                  configManager.Log += (sender, e) =>
                  {
                      if (options.DisplayLog || e.LogLevel == EnumLogLevel.FATAL)
                      {
                          Console.WriteLine(e.Message);
                      }
                  };

                  if (!File.Exists(options.ConfigFile))
                  {
                      configManager.Log(null, new LogEventArgs(EnumLogLevel.FATAL, $"The supplied config file {options.ConfigFile} does not exist."));
                      Environment.Exit(-1);
                  }

                  var configContainer = configManager.LoadConfig(options.ConfigFile);
                  var selectedConfig = configContainer.ConfigActions.FirstOrDefault(x => x.Name.ToUpper() == options.ConfigActionName.ToUpper());

                  if (selectedConfig == null)
                  {
                      configManager.Log(null, new LogEventArgs(EnumLogLevel.FATAL, $"The supplied project name {options.ConfigActionName} was not found in the config file {options.ConfigFile}."));
                      Environment.Exit(-1);
                  }

                  if (string.IsNullOrWhiteSpace(configContainer.RepositoryRootDirectory))
                  {
                      configManager.Log(null, new LogEventArgs(EnumLogLevel.DEBUG, "RepositoryRootDirectory was not set. Attempting to determine if we're in a repository"));
                      var hgRepositoryRoot = SourceControlHelper.GetHGRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.HgExecutablePath));
                      var gitRepositoryRoot = SourceControlHelper.GetGitRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.GitExecutablePath));

                      if (!string.IsNullOrWhiteSpace(hgRepositoryRoot))
                      {
                          configManager.Log(null, new LogEventArgs(EnumLogLevel.DEBUG, $"We are in a Mercurial repository. Root = {hgRepositoryRoot}."));
                          configContainer.RepositoryRootDirectory = hgRepositoryRoot;
                      }
                      else if (!string.IsNullOrWhiteSpace(gitRepositoryRoot))
                      {
                          configManager.Log(null, new LogEventArgs(EnumLogLevel.DEBUG, $"We are in a Mercurial repository. Root = {gitRepositoryRoot}."));
                          configContainer.RepositoryRootDirectory = gitRepositoryRoot;
                      }
                      else
                      {
                          configManager.Log(null, new LogEventArgs(EnumLogLevel.FATAL, $"We are not in a repository and the RepositoryRootDirectory isn't set. We cannot function like this. Exiting."));
                          Environment.Exit(-1);
                      }
                  }
                  else
                  {
                      configManager.Log(null, new LogEventArgs(EnumLogLevel.DEBUG, $"Repository is set in config file. {configContainer.RepositoryRootDirectory}"));
                  }

                  configManager.Log(null, new LogEventArgs(EnumLogLevel.INFO, $"Starting processing"));
                  foreach (var configItem in selectedConfig.ConfigItems)
                  {
                      configManager.Log(null, new LogEventArgs(EnumLogLevel.INFO, $"Processing configItem Name={configItem.Name}, PackageName={configItem.PackageName}, RelativeDirectory={configItem.RelativeDirectory}, TargetFilename={configItem.TargetFilename},"));
                      var package = configContainer.ConfigPackages.FirstOrDefault(x => x.Name.ToUpper() == configItem.PackageName.ToUpper());

                      if (package == null)
                      {
                          configManager.Log(null, new LogEventArgs(EnumLogLevel.FATAL, $"The package {configItem.PackageName} is not a valid package name"));
                          Environment.Exit(-1);
                      }

                      configManager.ProcessConfig(Path.Combine(configContainer.RepositoryRootDirectory, configItem.RelativeDirectory), configItem.TargetFilename, package);
                  }

                  configManager.Log(null, new LogEventArgs(EnumLogLevel.INFO, $"Processing complete"));
              })
              .WithNotParsed(errors => { });
        }
    }
}
