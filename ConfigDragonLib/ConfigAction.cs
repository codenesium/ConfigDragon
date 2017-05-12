namespace Codenesium.ConfigDragonLib
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a config action that can have multiple children actions
    /// </summary>
    public class ConfigAction
    {
        /// <summary>
        /// Gets or sets the config action name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of config items
        /// </summary>
        public List<ConfigItem> ConfigItems { get; set; } = new List<ConfigItem>();
    }
}
