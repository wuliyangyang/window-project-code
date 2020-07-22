using DMTec.TMListener;
using DMTec.TMListener.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public class SequencerListenerUserLogTextBox : LoggerTextBox, ISequencerListenerUser
    {
        public SequencerListenerUserLogTextBox():base()
        {
            //
        }

        #region ISequencerListenerUser 成员

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {
            //WriteLine(arg.ToString());
        }

        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {
            WriteLine(arg.ToString());
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            LogError(sender, msg);
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            LogInfo(sender, msg);
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            LogWarn(sender, msg);
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            WriteLine(msg);
        }

        public int BindSequencerListenerEvents(ref SequencerListener listener)
        {
            if (null == listener) return -1;
            return listener.RegisterUser(this);
        }

        public int BindSequencerListener(ref SequencerListener listener)
        {
            if (null == listener) return -1;
            return listener.RegisterUser(this);
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            if (null == listener) return -1;
            return listener.RemoveUser(this);
        }

        #endregion
    }
}
