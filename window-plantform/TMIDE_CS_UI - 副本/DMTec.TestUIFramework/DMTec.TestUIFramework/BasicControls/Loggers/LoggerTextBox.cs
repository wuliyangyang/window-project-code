using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TMListener.Interface;
using DMTec.TMListener;
using DMTec.TestUIFramework.Interface;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One Extended RichTextBox with some custom public functions for log display.
    /// It's designed to used in Logger System.
    /// It's inherited from RichTextBox.
    /// JimmyGong 2017.07.31...
    /// </summary>
    [DescriptionAttribute("One Extended RichTextBox with some custom public functions for log display.")]
    public class LoggerTextBox : RichTextBox, ILoggerTextBox
    {

        #region//Members//

        protected readonly object lockObj = new object();

        protected Action<Color, string> writeColorLogAction;

        protected ContextMenuStrip cms;

        #endregion//Members...END//

        #region//Constructors//

        public LoggerTextBox() : base()
        {
            Name = "Unknown";
            //Open DoubleBuffer.
            this.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, true, null);  

            this.BackColor = System.Drawing.Color.Black;
            this.Font      = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.Multiline = true;
            this.ReadOnly  = true;
            this.ScrollBars = RichTextBoxScrollBars.Both;
            this.writeColorLogAction = new Action<Color, string>(WriteTextBox);
            IsOutputTimeStamp = false;

            cms = new System.Windows.Forms.ContextMenuStrip();
            ToolStripItem tsItem;
            tsItem = AddContextMenu("Clear All", cms.Items, new EventHandler(Clear_click));
            this.ContextMenuStrip = cms;
        }

        public LoggerTextBox(string name ) : this()
        {
            base.Name = name;
        }

        #endregion//Constructors...END//

        #region//Properties//

        public bool IsOutputTimeStamp { get; set; }

        #endregion//Properties...END//

        #region//Private Methods//

        protected virtual void Clear_click(object sender, EventArgs e)
        {
            this.Clear();
        }

        /// <summary>  
        /// Add menu item.
        /// </summary>  
        /// <param name="text">item content text</param>  
        /// <param name="cms">the item collection</param>  
        /// <param name="callback">Click event callback function</param>  
        /// <returns>menu item to be add to menu</returns>  
        protected virtual ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                if (callback != null)
                {
                    tsmi.Click += callback;
                }
                cms.Add(tsmi);
                return tsmi;
            }
            return null;
        }

        /// <summary>
        /// Get the formatted time string.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTimeStampString()
        {
            string timeStampString = System.DateTime.Now.ToString(" yyyy-MM-dd HH:mm:ss:fffff ");
            return timeStampString;
        }

        /// <summary>
        /// Write specified text content to this control with color.
        /// </summary>
        /// <param name="c">Color</param>
        /// <param name="str">Text content</param>
        protected virtual void WriteTextBox(Color c, string str)
        {
            try
            {
                lock (lockObj)
                {
                    this.AppendText("\r\n");
                    this.SelectionColor = c;
                    this.AppendText(str);
                    this.SelectionStart = this.Text.Length + 1;
                    this.ScrollToCaret();
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "LoggerTextBox Write Text Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion//Private Methods...END//

        #region//Public API//

        /// <summary>
        /// Write one line text in log window with default color.
        /// </summary>
        /// <param name="str"></param>
        public virtual void WriteLine(string str)
        {
            WriteColorLine(this.ForeColor, str);
        }

        /// <summary>
        /// Write one line text with color in log window.
        /// </summary>
        /// <param name="foreColor">ForeColor</param>
        /// <param name="str"></param>
        public virtual void WriteColorLine(Color foreColor, string str)
        {
            if (this.InvokeRequired)
            {
                Invoke(writeColorLogAction, foreColor, str);
            }
            else
            {
                WriteTextBox(foreColor, str);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public virtual void LogInfo(object sender, string info)
        {
            WriteLine(GetTimeStampString() + "[Info] " + info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warn"></param>
        public virtual void LogWarn(object sender, string warn)
        {
            WriteColorLine(Color.Yellow, GetTimeStampString() + "[Warn] " + warn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public virtual void LogError(object sender, string error)
        {
            WriteColorLine(Color.Red, GetTimeStampString() + "[Error] " + error);
        }
        
        public virtual void LogClear()
        {
            this.Invoke(new Action(() =>
            {
                this.Clear();
            }));
        }

        #endregion//Public API...END//

    }
}
