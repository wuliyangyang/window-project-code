
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using DMTec.TMListener;
using DMTec.TMListener.Interface;
using System.ComponentModel;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One extended led label control that impliment the IStateMachineConnectorUser interface.
    /// </summary>
    [DescriptionAttribute("One extended led label control that impliment the IStateMachineConnectorUser interface.")]
    public class StateMachineHeartbeatLedLabel : LedLabel, IStateMachineConnectorUser
    {
        public StateMachineHeartbeatLedLabel() 
            : base()
        {
            //TODO
        }

        public StateMachineHeartbeatLedLabel(string number) 
            : base(number)
        {
            //TODO
        }


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
