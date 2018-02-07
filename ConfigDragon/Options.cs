namespace Codenesium.ConfigDragon
{
    using CommandLine;

    [Verb("spec", HelpText = "Generate config file")]
    internal class SpecOptions
    {
       
    }
    /// <summary>
    /// The command line options class
    /// </summary>
    [Verb("run", HelpText = "Run config file transforms")]
    internal class RunOptions
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

        /// <summary>
        /// Gets or sets a value indicating whether logging will display to the console. If true all log messages will
        /// be displayed to the console. If false only Fatal messages will log. 
        /// </summary>
        [Option("LogLevel", Required = false,
  HelpText = "Loglevel for Program. Trace,Debug,Info,Warn,Error,Fatal")]
        public string LogLevel { get; set; } = "Info";
    }
}