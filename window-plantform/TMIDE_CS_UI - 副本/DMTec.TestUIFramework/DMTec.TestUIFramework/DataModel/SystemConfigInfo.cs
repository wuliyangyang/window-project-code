using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.DataModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SystemConfigInfo
    {
        /// <summary>
        /// How many slots with show in ui.
        /// </summary>
        [DescriptionAttribute("How many slots with show in ui.")]
        public int Slots { get; set; }
        /// <summary>
        /// How many modules with show in ui.
        /// </summary>
        [DescriptionAttribute("How many slots with show in ui.")]
        public int Modules { get; set; }
        /// <summary>
        /// csv file path string.
        /// </summary>
        [DescriptionAttribute("csv file path string.")]
        public string CsvFilePath { get; set; }
        /// <summary>
        /// UI start mode.
        /// </summary>
        [DescriptionAttribute("UI start mode.")]
        public int Mode { get; set; }
        /// <summary>
        /// ZMQ Port config file path string.
        /// </summary>
        [DescriptionAttribute("ZMQ Port config file path string.")]
        public string ZmqPortConfigFilePath { get; set; }

        public SystemConfigInfo() { }

        public SystemConfigInfo(int slotNumber,int moduleNumber)
            : this()
        {
            this.Slots = slotNumber;
            this.Modules = moduleNumber;
        }
    }

}
