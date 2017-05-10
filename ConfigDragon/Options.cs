namespace Codenesium.ConfigDragon
{
    using CommandLine;

    /// <summary>
    /// The command line options class
    /// </summary>
    internal class Options
    {
        /// <summary>
        /// Gets or sets the complete path to the config file
        /// </summary>
        [Option("ConfigFile", Required = true,
          HelpText = "The config file for this project")]
        public string ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets the action name in the config file to process
        /// </summary>
        [Option("ConfigActionName", Required = true,
  HelpText = "The proejct to run")]
        public string ConfigActionName { get; set; }
    }
}