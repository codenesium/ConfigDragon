using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.ConfigDragonLib
{
    /// <summary>
    /// Represents a config action that can have multiple children actions
    /// </summary>
    public class ConfigAction
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of config items
        /// </summary>
        public List<ConfigItem> ConfigItems { get; set; } = new List<ConfigItem>();

    }
}
