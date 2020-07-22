using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.BasicControls
{
    public class TMHeartbeatLamp : SignalLamp
    {
        private const int CounterMax = 10;
        private int counter = CounterMax;

        public TMHeartbeatLamp() : base()
        {
            //
        }

        /// <summary>
        /// You can call this method to update heartbeat state at a certain frequency(timer drive or manual trigger). 
        /// </summary>
        [Description("You can call this method to update heartbeat state at a certain frequency(timer drive or manual trigger).")]
        public void UpdateDisply()
        {
            //
            counter--;

            if ( counter < 0 )
            {
                counter = 0;
                if (SignalState.Abnormal != CurrentState)
                    CurrentState = SignalState.Abnormal;
            }
            else if( counter < 4)
            {
                if (SignalState.Warnning != CurrentState)
                    CurrentState = SignalState.Warnning;
            }
            else
            {
                if (SignalState.Normal != CurrentState)
                    CurrentState = SignalState.Normal;
            }

            IsLampOn = !IsLampOn;
        }

        /// <summary>
        /// You must feed dog in time by this method or the state will change to abnormal.
        /// </summary>
        [Description("You must feed dog in time by this method or the state will change to abnormal.")]
        public void FeedDog()
        {
            counter = CounterMax;
        }

    }
}
