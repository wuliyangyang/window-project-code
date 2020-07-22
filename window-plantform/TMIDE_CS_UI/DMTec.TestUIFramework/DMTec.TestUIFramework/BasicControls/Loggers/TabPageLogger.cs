using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TMListener;
using DMTec.TestUIFramework.Interface;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// This is a extended TabPage control that packed one LoggerTextBox.
    /// </summary>
    [DescriptionAttribute("This is a extended TabPage control that packed one LoggerTextBox.")]
    public class TabPageLogger : TabPage, ILoggerTextBox
    {

        #region//Members//

        private static int _count = 0;

        protected LoggerTextBox _logger;

        protected bool isBindSeqListenerMode = false;
        
        #endregion//Members___END//
        
        #region//Constructors//

        public TabPageLogger()
            : base()
        {
            Name = _count++.ToString();
            _logger = new LoggerTextBox(Name);
            _logger.Dock = DockStyle.Fill;
            this.Controls.Add(_logger);
        }

        public TabPageLogger(string tabName)
            : base(tabName)
        {
            Name = tabName;
            _logger = new LoggerTextBox(Name);
            _logger.Dock = DockStyle.Fill;
            this.Controls.Add(_logger);
        }
        
        #endregion//Constructors___END//
        
        #region//ILoggerTextBox Members//

        public void WriteLine(string str)
        {
            _logger.WriteLine(str);
        }

        public void WriteColorLine(Color foreColor, string str)
        {
            _logger.WriteColorLine(foreColor, str);
        }

        public void LogInfo(object sender, string info)
        {
            _logger.LogInfo(sender, info);
        }

        public void LogWarn(object sender, string warn)
        {
            _logger.LogWarn(sender, warn);
        }

        public void LogError(object sender, string error)
        {
            _logger.LogError(sender, error);
        }

        public void LogClear()
        {
            _logger.LogClear();
        }

        #endregion//ILoggerTextBox Members___END//

    }
}
