namespace Codenesium.ConfigDragonLib
{
    using NLog;

    /// <summary>
    /// Handles making changed to Visual Studio csproj files.
    /// </summary>
    public class StringManager
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This is basically a string replace function
        /// </summary>
        /// <param name="haystack">The string to search</param>
        /// <param name="setting">The settings we need to find a needle and replace it</param>
        /// <returns>A process result</returns>
        public ProcessResult Process(string haystack, StringSetting setting)
        { 
            if (haystack.Contains(setting.Needle))
            {
                logger.Trace($"Needle {setting.Needle} was found");
                var result = haystack.Replace(setting.Needle, setting.ReplacementValue);
                return new ProcessResult(true, result);
            }
            else
            {
                logger.Error($"Needle {setting.Needle} was not found");
                return new ProcessResult(false, string.Empty);
            }
        }
    }
}
