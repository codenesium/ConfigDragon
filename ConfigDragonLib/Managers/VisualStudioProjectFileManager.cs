using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codenesium.ConfigDragonLib
{
    public class VisualStudioProjectFileManager
    {
        public void Process(string filename, string selector, string value)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The file {filename} was not found!");
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            //It is required to add a namespace because the project node in a project file
            // has the "http://schemas.microsoft.com/developer/msbuild/2003" namespace. Without it
            //XPath does not work. There is a work around for XPAth 2.0 but it's not supported by .NET

            var msManager = new XmlNamespaceManager(xDoc.NameTable);
            msManager.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode root = xDoc.DocumentElement;

            var node = root.SelectSingleNode(selector, msManager);
            if(node == null)
            {
                throw new NullReferenceException($"The selector {selector} did not return any nodes");
            }

            node.InnerText = value;
            xDoc.Save(filename);

        }
    }
}
