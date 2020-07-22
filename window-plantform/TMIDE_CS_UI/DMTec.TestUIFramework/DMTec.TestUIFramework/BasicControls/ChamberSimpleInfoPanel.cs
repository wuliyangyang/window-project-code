using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using DMTec.TMListener.Interface;
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    public partial class ChamberSimpleInfoPanel : UserControl
        , ISequencerListenerUser
    {
        public ChamberSimpleInfoPanel()
        {
            InitializeComponent();
        }


        #region ISequencerListenerUser 成员

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

        public void OnSequenceItemList(object sender, SeqItemListArgs arg)
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

        public void OnSequencerListenerLog(object logSender, int logLevel, string logString)
        {
            ((ISequencerListenerUser)statisticInfoPanel).OnSequencerListenerLog(logSender, logLevel, logString);
        }

        #endregion
    }
}
