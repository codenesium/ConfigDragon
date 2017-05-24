namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An xml setting that we will replace. 
    /// </summary>
    public class XmlSetting
    {
        /// <summary>
        /// Initializes a new instance of the XmlSetting class 
        /// </summary>
        /// <param name="description">Description of the setting</param>
        /// <param name="selector">XPath selector for the node to change</param>
        /// <param name="value">Value to replace with</param>
        /// <param name="namespaces">Namespaces to add to the XML selector</param>
        public XmlSetting(string description, string selector, string value, Dictionary<string, string> namespaces)
        {
            this.Description = description;
            this.Selector = selector;
            this.Value = value;
            this.Namespaces = namespaces;
        }

        /// <summary>
        /// Gets or sets the setting description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets the XPath selector for the node to modify
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        ///  Gets or sets the setting value to replace
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///  Gets or sets the list of namespace
        /// </summary>
        public Dictionary<string, string> Namespaces { get; set; }
    }
}
