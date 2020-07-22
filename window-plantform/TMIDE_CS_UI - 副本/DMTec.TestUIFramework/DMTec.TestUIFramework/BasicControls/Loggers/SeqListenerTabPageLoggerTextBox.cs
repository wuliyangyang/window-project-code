using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMTec.TMListener.Interface;
using System.ComponentModel;
using System.Drawing;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One Extented TabPageLoggerTextBox control that impliment ISequencerListenerUser.
    /// </summary>
    [DescriptionAttribute("One Extented TabPageLoggerTextBox control that impliment ISequencerListenerUser.")]
    public class SeqListenerTabPageLogger : TabPageLogger, ISequencerListenerUser
    {

        #region//Constructors//

        public SeqListenerTabPageLogger(string name)
            : base(name)
        {
            //
        }

        public SeqListenerTabPageLogger()
            : base()
        {
            //
        }

        #endregion//Constructors___END//


        #region//ISequencerListenerUser Members//

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            LogError(sender, arg.ToString());
        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {
            LogInfo(sender, arg.ToString());
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            LogError(sender, " [SeqListener Report] " + msg);
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            LogInfo(sender, " [SeqListener Report] " + msg);
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            LogWarn(sender, " [SeqListener Report] " + msg);
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            WriteColorLine(Color.Green, msg);
        }

        public int BindSequencerListener(ref TMListener.SequencerListener listener)
        {
            return 0;
        }

        public int UnbindSequencerListener(ref TMListener.SequencerListener listener)
        {
            return 0;
        }

        #endregion//ISequencerListenerUser Members//

    }
}
