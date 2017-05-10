namespace Codenesium.ConfigDragonLib
{
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Handles loading and processing on config objects
    /// </summary>
    public class ConfigManager
    {
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
        /// <param name="config">The selected config to run</param>
        public void ProcessConfig(string configFileDirectory,string relativeFileName, ConfigPackage config)
        {
            var appSettingsManager = new AppSettingsManager();
            var connectionStringManager = new ConnectionStringManager();

            foreach (var key in config.AppSettings.Keys)
            {
                appSettingsManager.SetAppSetting(Path.Combine(configFileDirectory, relativeFileName), key, config.AppSettings[key]);
            }

            foreach (var key in config.ConnectionStrings.Keys)
            {
                connectionStringManager.SetConnectionString(Path.Combine(configFileDirectory, relativeFileName), key, config.ConnectionStrings[key]);
            }
        }
    }
}
