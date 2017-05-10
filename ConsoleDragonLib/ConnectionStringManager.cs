namespace Codenesium.ConfigDragonLib
{
    using System.Configuration;
    using System.IO;

    /// <summary>
    /// Handles modifying .NET connection strings
    /// </summary>
    public class ConnectionStringManager
    {
        /// <summary>
        /// Parses the supplied filename and replaces the connection string key with the supplied value
        /// </summary>
        /// <param name="filename">Target filename to process</param>
        /// <param name="key">The connection string key to modify</param>
        /// <param name="value">The value to replace the existing value with</param>
        public void SetConnectionString(string filename, string key, string value)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The configuration file {filename} was not found!");
            }

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = filename;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            if (config.ConnectionStrings.ConnectionStrings[key] != null)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(key);
                var connectionStringSetting = new ConnectionStringSettings(key, value);
                config.ConnectionStrings.ConnectionStrings.Remove(key);
                config.ConnectionStrings.ConnectionStrings.Add(connectionStringSetting);
                config.Save();
            }
        }
    }
}
