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
