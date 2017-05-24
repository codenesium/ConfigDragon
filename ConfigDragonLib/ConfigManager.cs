namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.IO;
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
        public ConfigContainer LoadConfigFromFile(string filename)
        {
            return this.LoadConfigFromString(File.ReadAllText(filename));
        }

        /// <summary>
        /// Loads the config container from the supplied string
        /// </summary>
        /// <param name="contents">The config string</param>
        /// <returns>The loaded config container</returns>
        public ConfigContainer LoadConfigFromString(string contents)
        {
            return JsonConvert.DeserializeObject<ConfigContainer>(contents);
        }

        /// <summary>
        /// Executes the selected config
        /// </summary>
        /// <param name="configFileDirectory">The path the config file was loaded from</param>
        /// <param name="relativeFileName">The path relative to the config file to modify</param>
        /// <param name="config">The selected config to run</param>
        /// <param name="fileSystem">File system interface</param>
        public void ProcessConfig(string configFileDirectory, string relativeFileName, ConfigPackage config, IFileSystem fileSystem)
        {
            var xmlManager = new XmlFileManager();

            foreach (var key in config.AppSettings.Keys)
            {
                string filename = Path.Combine(configFileDirectory, relativeFileName);
                var setting = new XmlSetting(string.Empty, $"/configuration/appSettings/add[@key = '{key}']/@value", config.AppSettings[key], null);
                logger.Trace($"Processing app setting {key}");
                var result = xmlManager.Process(File.ReadAllText(filename), setting);
                if (result.Success)
                {
                    fileSystem.WriteAllText(filename, result.Content);
                }
            }

            foreach (var key in config.ConnectionStrings.Keys)
            {
                string filename = Path.Combine(configFileDirectory, relativeFileName);
                var setting = new XmlSetting(string.Empty, $"/configuration/connectionStrings/add[@name = '{key}']/@connectionString", config.ConnectionStrings[key], null);
                logger.Trace($"Processing connection string setting {key}");
                var result = xmlManager.Process(File.ReadAllText(filename), setting);
                if (result.Success)
                {
                    fileSystem.WriteAllText(filename, result.Content);
                }
            }

            foreach (var setting in config.XmlSettings)
            {
                string filename = Path.Combine(configFileDirectory, relativeFileName);
                logger.Trace($"Processing setting {setting.Description}");
                var result = xmlManager.Process(File.ReadAllText(filename), setting);
                if (result.Success)
                {
                    fileSystem.WriteAllText(filename, result.Content);
                }
            }
        }      
    }
}
