namespace Codenesium.ConfigDragonLib
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a user created configuration package. These
    /// are reusable in a project. 
    /// </summary>
    public class ConfigPackage
    {
        /// <summary>
        /// Gets or sets the name of the configuration
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the configuration
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets the dictionary of app settings to modify
        /// </summary>
        public Dictionary<string, string> AppSettings { get; private set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// Gets the dictionary of connection strings to modify
        /// </summary>
        public Dictionary<string, string> ConnectionStrings { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the dictionary of xml strings to modify. First ordinal is the selector. Second ordinal is the new value
        /// </summary>
        public Dictionary<string, string> VisualStudioProjectSettings { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the dictionary of xml strings to modify. First ordinal is the selector. Second ordinal is the new value
        /// </summary>
        public List<XmlSetting> XmlSettings { get; private set; } = new List<XmlSetting>();
    }
}
