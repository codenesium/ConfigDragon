namespace Codenesium.ConfigDragonLib
{
    /// <summary>
    /// A result class that indicates success and returns the transformed text
    /// </summary>
    public class ProcessResult
    {
        /// <summary>
        /// Initializes a new instance of the ProcessResult class
        /// </summary>
        /// <param name="success">A value indicating success</param>
        /// <param name="content">The transformed xml</param>
        public ProcessResult(bool success, string content)
        {
            this.Success = success;
            this.Content = content;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the action succeeded
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the processed string with the transform applied
        /// </summary>
        public string Content { get; set; }
    }
}
