//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//
using DMTec.TMListener;
using DMTec.TMListener.Interface;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One extended led label control that implement the ISequencerListenerUser interface.
    /// </summary>
    [DescriptionAttribute("One extended led label control that implement the ISequencerListenerUser interface.")]
    public class SequenceHeartbeatLedLabel : LedLabel, ISequencerListenerUser
    {

        public SequenceHeartbeatLedLabel() 
            : base()
        {
            //
        }

        public SequenceHeartbeatLedLabel(string number) 
            : base(number)
        {
            //
        }

        #region//ISequencerListenerUser Members//

        public int BindSequencerListener(ref SequencerListener listener)
        {
            throw new NotImplementedException();
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            throw new NotImplementedException();
        }

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            //throw new NotImplementedException();
            isSignalOK = true;
        }

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {
            //isHeartbeatCome = true;
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            //
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            //
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            //
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            //
        }

        #endregion//ISequencerListenerUser Members___END//


    }
}
