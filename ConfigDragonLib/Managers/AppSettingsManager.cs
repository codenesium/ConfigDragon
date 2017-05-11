namespace Codenesium.ConfigDragonLib
{
    using System.Configuration;
    using System.IO;

    /// <summary>
    /// Class that handles application settings in .NET configuration files
    /// </summary>
    public class AppSettingsManager
    {
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
                throw new FileNotFoundException($"The file {filename} was not found!");
            }

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = filename;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, value);
                config.Save();
            }
            else
            {
                //log this some how
            }
        }
    }
}
