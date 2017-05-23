namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.Configuration;
    using System.IO;
    using NLog;

    /// <summary>
    /// Class that handles application settings in .NET configuration files
    /// </summary>
    public class AppSettingsManager
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Will create a setting if it does not exist
        /// </summary>
        /// <param name="filename">The target file to be modified</param>
        /// <param name="key">The app setting key</param>
        /// <param name="value">The app setting new value</param>
        public void Process(string filename, string key, string value)
        {
            if (!File.Exists(filename))
            {
                logger.Error($"The file { filename } was not found!");
                return;
            }

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = filename;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, value);
                config.Save();
                logger.Info($"Key {key} processed");
            }
            else
            {
                logger.Warn($"Key {key} not found");
            }
        }
    }
}
