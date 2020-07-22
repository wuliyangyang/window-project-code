using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.DataModel;
using DMTec.TMListener;
using DMTec.TMListener.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One extended label control that can simulate led blink.
    /// </summary>
    [DescriptionAttribute("One extended label control that can simulate led blink.")]
    public class LedLabel : Label, IStateMachineConnectorUser, IEngineConnectorUser, ISequencerListenerUser
    {

        #region//Members//

        protected bool isLedOnFlag;
        protected bool isSignalOK;
        protected int  timeCounter;
        protected Color _backColor;

        #endregion//Members___END//

        #region//Constructors//

        public LedLabel(): this("0"){}

        public LedLabel(string number)
            : base()
        {
            InitMember(number);
        }

        #endregion//Constructors___END//

        #region//Properties//

        public LedState State { get; set; }

        #endregion//Properties___END//

        #region//Protected Methods//

        protected virtual void InitMember(string number)
        {
            this.Text = number;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SetBackgroundColor(Consts.LED_ON_UNDEFINED_COLOR);
            this.TextAlign = ContentAlignment.MiddleCenter;
            State = LedState.ERROR;
            isLedOnFlag = false;
            timeCounter = 0;
            isSignalOK  = false;
        }

        #endregion//Protected Methods___END//

        #region//Public Methods//

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public virtual void SetBackgroundColor(Color c)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Color>((x) => this.BackColor = x));
            }
            else
            {
                this.BackColor = c;
            }
        }

        public virtual void UpdateState()
        {
            if(isLedOnFlag)
            {
                if(LedState.OK == State)
                {
                    _backColor = Consts.LED_ON_OK_COLOR;
                }
                else if(LedState.WARN == State)
                {
                    _backColor = Consts.LED_ON_WARN_COLOR;
                }
                else if (LedState.ERROR == State)
                {
                    _backColor = Consts.LED_ON_ERROR_COLOR;
                }
                else
                {
                    _backColor = Consts.LED_ON_UNDEFINED_COLOR;
                }
            }
            else
            {
                _backColor = Consts.LED_OFF_COLOR;
            }
            isLedOnFlag = !isLedOnFlag;
            SetBackgroundColor(_backColor);

            if (isSignalOK) State = LedState.OK;

            if(timeCounter++ > Consts.HeartbeatCheckInterval)
            {
                if (false == isSignalOK)
                {
                    State = LedState.ERROR;
                }
                else
                {
                    isSignalOK = false;
                }
                timeCounter = 0;
            }
            else
            {
                return;
            }
        }

        #endregion//Public Methods___END//

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

        #region//IEngineConnectorUser Members//

        public void OnEngineConnectorLogError(object sender, string msg)
        {

        }

        public void OnEngineConnectorLogInfo(object sender, string msg)
        {

        }

        public void OnEngineConnectorLogWarn(object sender, string msg)
        {

        }

        public void OnEngineHeartbeat(object sender, TMListener.EngineHeartBeatEventArgs arg)
        {
            isSignalOK = true;
        }

        public void OnEngineOriginalMsg(object sender, string msg)
        {

        }

        #endregion//IEngineConnectorUser Members___END//

        #region//IStateMachineConnectorUser Members//

        public void OnSMConnectorError(object sender, string msg)
        {

        }

        public void OnSMConnectorInfo(object sender, string msg)
        {

        }

        public void OnSMConnectorWarn(object sender, string msg)
        {

        }

        public void OnSMHeartbeat(object sender, SMHeartBeatEventArgs arg)
        {
            isSignalOK = true;
        }

        public void OnSMNoTest(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMNoTestFailCSV(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMNoTestFailCnt(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMNoTestFailLoad(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMNoTestFailReq(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMNoTestFailUnknown(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMOriginalMsg(object sender, string msg)
        {

        }

        public void OnSMStateError(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMStateIdle(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMStateLoad(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMStateTestDoneEvent(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMStateTestingEvent(object sender, SMStateEventArgs arg)
        {

        }

        public void OnSMStateUnload(object sender, SMStateEventArgs arg)
        {

        }

        #endregion//IStateMachineConnectorUser Members___END//

    }
}
