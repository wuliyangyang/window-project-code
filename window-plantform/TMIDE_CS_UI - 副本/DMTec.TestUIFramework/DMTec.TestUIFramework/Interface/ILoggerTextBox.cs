using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.Interface
{
    /// <summary>
    /// Interface that include LogInfo/LogWarn/LogError and WriteLine/WriteColorLine.
    /// </summary>
    [DescriptionAttribute("Interface that include LogInfo/LogWarn/LogError and WriteLine/WriteColorLine.")]
    public interface ILoggerTextBox : ILogger
    {
        /// <summary>
        /// The Name Property.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Write one line text in log window with default color.
        /// </summary>
        /// <param name="str"></param>
        void WriteLine(string str);

        /// <summary>
        /// Write one line text with color in log window.
        /// </summary>
        /// <param name="foreColor">ForeColor</param>
        /// <param name="str"></param>
        void WriteColorLine(Color foreColor, string str);
        
    }
}
