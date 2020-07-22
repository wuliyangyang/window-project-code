using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TMListener;
using DMTec.TMListener.Interface;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class LoggerTabControl : TabControl, ISequencerListenerUser
    {
        List<TabPageLogger> myLoggersList;
        List<SequencerListener> mySeqListenerList;

        public LoggerTabControl() : base()
        {
            InitializeComponent();
            InitLoggers();
        }

        private void InitLoggers()
        {
            myLoggersList = new List<TabPageLogger>();
        }

        /// <summary>
        /// Add one new logger tabpage in tabcontrol and return the new logger.
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public virtual SeqListenerTabPageLogger AddSeqListenerLogger(string loggerName)
        {
            SeqListenerTabPageLogger logger = new SeqListenerTabPageLogger(loggerName);
            logger.Name = loggerName;
            myLoggersList.Add(logger);
            
            this.TabPages.Add(logger);
            return logger;
        }

        /// <summary>
        /// Delete one new logger tabpage in tabcontrol.
        /// </summary>
        /// <param name="loggerName"></param>
        public virtual int DeleteSeqListenerLogger(string loggerName)
        {
            if (null == myLoggersList || myLoggersList.Count <= 0) return -1;
            try
            {
                foreach (TabPageLogger logger in myLoggersList)
                {
                    if (logger.Name == loggerName)//If exist, remove.
                    {
                        myLoggersList.Remove(logger);
                        if (this.TabPages.IndexOf(logger) >= 0)//If exist, remove.
                        {
                            this.TabPages.Remove(logger);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -2;
            }
            return 0;
        }

        public virtual TabPageLogger AddLogger(string loggerName)
        {
            TabPageLogger logger = new TabPageLogger(loggerName);
            logger.Name = loggerName;
            myLoggersList.Add(logger);
            this.TabPages.Add(logger);
            return logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public virtual void BindSeqListeners(ref List<SequencerListener> list)
        {
            if (null == list || 0 >= list.Count) return;

            this.mySeqListenerList = list;
            int i = 0;
            foreach (SequencerListener listener in mySeqListenerList)
            {
                SeqListenerTabPageLogger logger = AddSeqListenerLogger("SeqListener" + i++.ToString());
                listener.RegisterUser(logger);               
            }
        }

        #region ISequencerListenerUser Members

        public void OnSequenceAttributeFound(object sender, SeqAttrFoundArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceEnd(object sender, SeqEndArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceErrorReport(object sender, SeqReportErrorArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceHeartbeat(object sender, SeqHeartBeatArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceItemEnd(object sender, SeqItemEndArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceItemList(object sender, SeqItemListArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceItemStart(object sender, SeqItemStartArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceStart(object sender, SeqStartArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceUOPDetect(object sender, SeqUopDetectArgs arg)
        {
            throw new NotImplementedException();
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            throw new NotImplementedException();
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            throw new NotImplementedException();
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            throw new NotImplementedException();
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            throw new NotImplementedException();
        }

        public int BindSequencerListener(ref SequencerListener listener)
        {
            throw new NotImplementedException();
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
