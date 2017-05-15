namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.IO;
    using Logging;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// Handles loading and processing on config objects
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Loads the config container from the supplied file
        /// </summary>
        /// <param name="filename">The config file to load from disk</param>
        /// <returns>The loaded config container</returns>
        public ConfigContainer LoadConfig(string filename)
        {
            string contents = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<ConfigContainer>(contents);
        }

        /// <summary>
        /// Executes the selected config
        /// </summary>
        /// <param name="configFileDirectory">The path the config file was loaded from</param>
        /// <param name="relativeFileName">The path relative to the config file to modify</param>
        /// <param name="config">The selected config to run</param>
        public void ProcessConfig(string configFileDirectory, string relativeFileName, ConfigPackage config)
        {
            var appSettingsManager = new AppSettingsManager();
            var connectionStringManager = new ConnectionStringManager();
            var visualStudioProjectFileManagerManager = new VisualStudioProjectFileManager();

            foreach (var key in config.AppSettings.Keys)
            {
                appSettingsManager.Process(Path.Combine(configFileDirectory, relativeFileName), key, config.AppSettings[key]);
            }

            foreach (var key in config.ConnectionStrings.Keys)
            {
                connectionStringManager.Process(Path.Combine(configFileDirectory, relativeFileName), key, config.ConnectionStrings[key]);
            }

            foreach (var key in config.VisualStudioProjectSettings.Keys)
            {
                visualStudioProjectFileManagerManager.Process(Path.Combine(configFileDirectory, relativeFileName), key, config.VisualStudioProjectSettings[key]);
            }
        }      
    }
}
