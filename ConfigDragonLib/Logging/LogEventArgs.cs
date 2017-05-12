namespace Codenesium.ConfigDragonLib.Logging
{
    using System;

    /// <summary>
    /// Log level for logging
    /// </summary>
    public enum EnumLogLevel
    {
        /// <summary>
        /// Trace log level
        /// </summary>
        TRACE,

        /// <summary>
        /// Debug log level
        /// </summary>
        DEBUG,

        /// <summary>
        /// Info log level
        /// </summary>
        INFO,

        /// <summary>
        /// Warn log level
        /// </summary>
        WARN,

        /// <summary>
        /// Error log level
        /// </summary>
        ERROR,

        /// <summary>
        /// Fatal log level
        /// </summary>
        FATAL
    }

    /// <summary>
    /// Event args for logging
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LogEventArgs class
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="message">Message to log</param>
        public LogEventArgs(EnumLogLevel logLevel, string message)
        {
            this.LogLevel = logLevel;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the log level
        /// </summary>
        public EnumLogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the logging message
        /// </summary>
        public string Message { get; set; }
    }
}
