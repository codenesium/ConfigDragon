namespace Codenesium.ConfigDragonLib
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains the list of user created configurations
    /// </summary>
    public class ConfigContainer
    {
        /// <summary>
        /// Gets or sets the RepositoryRootDirectory
        /// </summary>
        public string RepositoryRootDirectory { get; set; }

        /// <summary>
        /// Gets or sets the list of config actions
        /// </summary>
        public List<ConfigAction> ConfigActions{ get; set; } = new List<ConfigAction>();

        /// <summary>
        /// Gets or sets the list of config packages
        /// </summary>
        public List<ConfigPackage> ConfigPackages { get; set; } = new List<ConfigPackage>();
    }
}
