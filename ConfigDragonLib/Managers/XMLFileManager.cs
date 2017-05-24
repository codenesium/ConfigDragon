namespace Codenesium.ConfigDragonLib
{
    using System;
    using System.IO;
    using System.Xml;
    using NLog;

    /// <summary>
    /// Handles making changed to Visual Studio csproj files.
    /// </summary>
    public class XmlFileManager
    {
        /// <summary>
        /// NLog logging class
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Processes the specified file using the XPath selector and replaces the found element with the supplied value/
        /// </summary>
        /// <param name="contents">file to modify</param>
        /// <param name="setting">XMl setting object</param>
        /// <returns>Returns a process result for the current action</returns>
        public ProcessResult Process(string contents, XmlSetting setting)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(contents);

            XmlNode root = doc.DocumentElement;

            var namespaceManager = new XmlNamespaceManager(doc.NameTable);

            if (setting.Namespaces != null)
            {
                foreach (var key in setting.Namespaces.Keys)
                {
                    logger.Trace($"Adding namespace {key} with value { setting.Namespaces[key]}");
                    namespaceManager.AddNamespace(key, setting.Namespaces[key]);
                }
            }

            var node = root.SelectSingleNode(setting.Selector, namespaceManager);

            if (node != null)
            {
                logger.Trace($"Selector returned node with value of {node.OuterXml}");
                node.InnerText = setting.Value;
                logger.Info($"Selector {setting.Selector} processed. Node value set to {setting.Value}");
                return new ProcessResult(true, doc.OuterXml);
            }
            else
            {
                logger.Warn($"The selector {setting.Selector} did not return any nodes");
                return new ProcessResult(false, string.Empty);
            }
        }
    }
}
