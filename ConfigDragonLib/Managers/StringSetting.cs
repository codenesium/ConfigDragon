namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A string that we will search for and replace
    /// </summary>
    public class StringSetting
    {
        /// <summary>
        /// Initializes a new instance of the StringSetting class 
        /// </summary>
        /// <param name="description">The setting description</param>
        /// <param name="needle">The value to search for</param>
        /// <param name="replacementValue">The value to replace the needle with</param>
        public StringSetting(string description, string needle, string replacementValue)
        {
            this.Description = description;
            this.Needle = needle;
            this.ReplacementValue = replacementValue;
        }

        /// <summary>
        /// Gets or sets the setting description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets value to search for
        /// </summary>
        public string Needle { get; set; }

        /// <summary>
        ///  Gets or sets the setting value to replace
        /// </summary>
        public string ReplacementValue { get; set; }
    }
}
