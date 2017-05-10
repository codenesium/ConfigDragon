using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.ConfigDragonLib
{
    /// <summary>
    /// Represents a config item that contains a reusable package
    /// </summary>
    public class ConfigItem
    {
        public string Name { get; set; }
        public string RelativeDirectory { get; set; }
        public string PackageName { get; set; }
        /// <summary>
        /// Gets or sets the relative filename of the target filename relative to the location of <see href="ConfigDragon.json"/> 
        /// </summary>
        public string TargetFilename { get; set; }
    }
}
