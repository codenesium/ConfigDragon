namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.IO;
    using System.Xml;
    using Logging;
    using NLog;

    /// <summary>
    /// Handles making changed to Visual Studio csproj files.
    /// </summary>
    public class VisualStudioProjectFileManager
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Processes the specified file using the XPath selector and replaces the found element with the supplied value/
        /// </summary>
        /// <param name="filename">file to modify</param>
        /// <param name="selector">XPath selector to use</param>
        /// <param name="value">Value to replace with</param>
        public void Process(string filename, string selector, string value)
        {
            if (!File.Exists(filename))
            {
                logger.Error($"The file { filename } was not found!");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            /* It is required to add a namespace because the project node in a project file
              has the "http://schemas.microsoft.com/developer/msbuild/2003" namespace. Without it
              XPath does not work. There is a work around for XPAth 2.0 but it's not supported by .NET
            */

            var namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode root = doc.DocumentElement;

            var node = root.SelectSingleNode(selector, namespaceManager);

            if (node != null)
            {
                node.InnerText = value;
                doc.Save(filename);
                logger.Info($"Selector {selector} processed");
            }
            else
            {
                logger.Warn($"The selector {selector} did not return any nodes");
            }
        }
    }
}
