using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.MessageCenter
{
    /// <summary>
    /// The message data model of global message center.
    /// You can use it to handle some system message like login message.
    /// </summary>
    [DescriptionAttribute("The message data model of global message center.")]
    public class InternalMessage
    {
        private string msgType;
        private object msgContext;

        /// <summary>
        /// New one message model to load message data.
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgContext"></param>
        [DescriptionAttribute("New one message model to load message data.")]
        public InternalMessage(string msgType, object msgContext)
        {
            this.msgType = msgType;
            this.msgContext = msgContext;
        }

        /// <summary>
        /// 
        /// </summary>
        [DescriptionAttribute("The message data model of global message center.")]
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        public object MsgContext
        {
            get { return msgContext; }
            set { msgContext = value; }
        }
    }
}
