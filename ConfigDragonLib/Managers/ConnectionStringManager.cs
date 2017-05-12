namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.Configuration;
    using System.IO;
    using Logging;

    /// <summary>
    /// Handles modifying .NET connection strings
    /// </summary>
    public class ConnectionStringManager
    {
        /// <summary>
        /// Gets or sets the logging event handler
        /// </summary>
        public EventHandler<LogEventArgs> Log { get; set; }

        /// <summary>
        /// Parses the supplied filename and replaces the connection string key with the supplied value
        /// </summary>
        /// <param name="filename">Target filename to process</param>
        /// <param name="key">The connection string key to modify</param>
        /// <param name="value">The value to replace the existing value with</param>
        public void Process(string filename, string key, string value)
        {
            if (!File.Exists(filename))
            {
                this?.Log(this, new LogEventArgs(EnumLogLevel.ERROR, $"The file { filename } was not found!"));
                return;
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
                this?.Log(this, new LogEventArgs(EnumLogLevel.INFO, $"Key {key} processed"));
            }
            else
            {
                this?.Log(this, new LogEventArgs(EnumLogLevel.WARN, $"Key {key} not found"));
            }
        }
    }
}
