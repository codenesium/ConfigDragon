namespace Codenesium.ConfigDragonLib
{
    /// <summary>
    /// Represents a config item that contains a reusable package
    /// </summary>
    public class ConfigItem
    {
        /// <summary>
        /// Gets or sets the config item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the RelativeDirectory. This is relative to the repository root.
        /// </summary>
        public string RelativeDirectory { get; set; }

        /// <summary>
        /// Gets or sets the Package to apply.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the relative filename of the target filename relative to the location of <see href="ConfigDragon.json"/> 
        /// </summary>
        public string TargetFilename { get; set; }
    }
}
