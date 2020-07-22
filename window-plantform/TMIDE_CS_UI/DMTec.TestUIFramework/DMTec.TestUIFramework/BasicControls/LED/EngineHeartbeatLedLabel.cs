using DMTec.TMListener.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One extended led label control that impliment the IEngineConnectorUser interface.
    /// </summary>
    [DescriptionAttribute("One extended led label control that impliment the IEngineConnectorUser interface.")]
    public class EngineHeartbeatLedLabel : LedLabel, IEngineConnectorUser
    {
        public EngineHeartbeatLedLabel() 
            : base()
        {
            //TODO
        }

        public EngineHeartbeatLedLabel(string number) 
            : base(number)
        {
            //TODO
        }


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

    }
}
