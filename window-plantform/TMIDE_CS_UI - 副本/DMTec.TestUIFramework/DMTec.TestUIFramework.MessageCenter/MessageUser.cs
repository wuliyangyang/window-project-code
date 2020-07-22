using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.MessageCenter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate int InternalMessageHandler(InternalMessage msg);

    /// <summary>
    /// 
    /// </summary>
    public class MessageUser
    {
        /// <summary>
        /// Define which msg type will be handled.
        /// </summary>
        
        public string msgType { get; private set; }

        /// <summary>
        /// Define which handler will handle msg.
        /// </summary>
        public InternalMessageHandler msgHandler { get; private set; }
        
        /// <summary>
        /// Define who will receive this type msg.
        /// If null , all types message will be received.
        /// </summary>
        public object msgReceiver { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        /// <param name="msgReceiver"></param>
        public MessageUser(string name, InternalMessageHandler handler, object msgReceiver)
        {
            this.msgType = name;
            this.msgHandler = handler;
            this.msgReceiver = msgReceiver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        public MessageUser(string name, InternalMessageHandler handler)
        {
            this.msgType = name;
            this.msgHandler = handler;
            this.msgReceiver = null;
        }
    }
}
