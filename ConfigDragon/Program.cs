namespace Codenesium.ConfigDragon
{
    using System;
    using System.IO;
    using System.Linq;
    using CommandLine;
    using ConfigDragonLib;
    using NLog;

    /// <summary>
    /// Main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Program entry points
        /// </summary>
        /// <param name="args">Program arguments</param>
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<RunOptions, SpecOptions>(args)
              .MapResult(
              (SpecOptions options) => {

                  var configContainer = new ConfigContainer();

                  var hgRepositoryRoot = SourceControlHelper.GetHGRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.HgExecutablePath));
                  var gitRepositoryRoot = SourceControlHelper.GetGitRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.GitExecutablePath));

                  if (!string.IsNullOrWhiteSpace(hgRepositoryRoot))
                  {
                      logger.Debug($"We are in a Mercurial repository. Root = {hgRepositoryRoot}.");
                      configContainer.RepositoryRootDirectory = hgRepositoryRoot;
                  }
                  else if (!string.IsNullOrWhiteSpace(gitRepositoryRoot))
                  {
                      logger.Debug($"We are in a Git repository. Root = {gitRepositoryRoot}.");
                      configContainer.RepositoryRootDirectory = gitRepositoryRoot;
                  }
                  else
                  {
                      logger.Debug($"We are not in a repository and the RepositoryRootDirectory isn't set. We cannot function like this. Exiting.");
                      Environment.Exit(-1);
                  }

                  //iterate all files and directories looking for app.config and web.config

                  return 1;

              },
              (RunOptions options) =>
              {
                  if (!string.IsNullOrWhiteSpace(options.LogLevel))
                  {
                      if (options.LogLevel.ToUpper() == "TRACE")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Fatal));
                      }
                      else if (options.LogLevel.ToUpper() == "DEBUG")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevels(LogLevel.Debug, LogLevel.Fatal));
                      }
                      else if (options.LogLevel.ToUpper() == "INFO")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevels(LogLevel.Info, LogLevel.Fatal));
                      }
                      else if (options.LogLevel.ToUpper() == "WARN")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevels(LogLevel.Warn, LogLevel.Fatal));
                      }
                      else if (options.LogLevel.ToUpper() == "ERROR")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevels(LogLevel.Error, LogLevel.Fatal));
                      }
                      else if (options.LogLevel.ToUpper() == "FATAL")
                      {
                          var loggers = NLog.LogManager.Configuration.LoggingRules.ToList();
                          loggers.ForEach(x => x.EnableLoggingForLevel(LogLevel.Fatal));
                      }
                      else
                      {
                          Console.WriteLine($"Invalid log level {options.LogLevel}");
                          Environment.Exit(-1);
                      }
                  }


                  var configManager = new ConfigManager();

                  if (!File.Exists(options.ConfigFile))
                  {
                      logger.Fatal($"The supplied config file {options.ConfigFile} does not exist.");
                      Environment.Exit(-1);
                  }

                  var configContainer = configManager.LoadConfigFromFile(options.ConfigFile);
                  var selectedConfig = configContainer.ConfigActions.FirstOrDefault(x => x.Name.ToUpper() == options.ConfigActionName.ToUpper());

                  if (selectedConfig == null)
                  {
                      logger.Fatal($"The supplied project name {options.ConfigActionName} was not found in the config file {options.ConfigFile}.");
                      Environment.Exit(-1);
                  }

                  if (string.IsNullOrWhiteSpace(configContainer.RepositoryRootDirectory))
                  {
                      logger.Debug($"RepositoryRootDirectory was not set. Attempting to determine if we're in a repository");

                      logger.Debug($"Config HgExecutablePath={configContainer.HgExecutablePath},Expanded={Environment.ExpandEnvironmentVariables(configContainer.HgExecutablePath)}");

                      logger.Debug($"Config GitExecutablePath={configContainer.GitExecutablePath},Expanded={Environment.ExpandEnvironmentVariables(configContainer.GitExecutablePath)}");

                      var hgRepositoryRoot = SourceControlHelper.GetHGRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.HgExecutablePath));
                      var gitRepositoryRoot = SourceControlHelper.GetGitRepositoryRootPath(Environment.ExpandEnvironmentVariables(configContainer.GitExecutablePath));

                      if (!string.IsNullOrWhiteSpace(hgRepositoryRoot))
                      {
                          logger.Debug($"We are in a Mercurial repository. Root = {hgRepositoryRoot}.");
                          configContainer.RepositoryRootDirectory = hgRepositoryRoot;
                      }
                      else if (!string.IsNullOrWhiteSpace(gitRepositoryRoot))
                      {
                          logger.Debug($"We are in a Git repository. Root = {gitRepositoryRoot}.");
                          configContainer.RepositoryRootDirectory = gitRepositoryRoot;
                      }
                      else
                      {
                          logger.Debug($"We are not in a repository and the RepositoryRootDirectory isn't set. We cannot function like this. Exiting.");
                          Environment.Exit(-1);
                      }
                  }
                  else
                  {
                      logger.Debug("$Repository is set in config file. {configContainer.RepositoryRootDirectory}");
                  }

                  logger.Debug($"Starting processing");
                  foreach (var configItem in selectedConfig.ConfigItems)
                  {
                      logger.Info($"Processing configItem Name={configItem.Name}, PackageName={configItem.PackageName}, RelativeDirectory={configItem.RelativeDirectory}, TargetFilename={configItem.TargetFilename}");
                      var package = configContainer.ConfigPackages.FirstOrDefault(x => x.Name.ToUpper() == configItem.PackageName.ToUpper());

                      if (package == null)
                      {
                          logger.Fatal($"The package {configItem.PackageName} is not a valid package name");
                          Environment.Exit(-1);
                      }

                      configManager.ProcessConfig(Path.Combine(configContainer.RepositoryRootDirectory, configItem.RelativeDirectory), configItem.TargetFilename, package, new FileSystem());
                  }

                  logger.Info($"Processing complete");

                  return 1;
              },
              errs => 1);


    
        }
    }
}
