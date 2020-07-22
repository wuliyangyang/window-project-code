using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.MessageCenter
{

    /// <summary>
    /// The unique instance of global message handle center.
    /// You can use it in all parts of one .net assembly.
    /// </summary>
    [DescriptionAttribute("The global message handle center.")]
    public static class GlobalMessageCenter
    {
        /// <summary>
        /// The registered message user list.
        /// </summary>
        static private List<MessageUser> myUserList = new List<MessageUser>();

        /// <summary>
        /// Register one message user with assigned message handler and receiver.
        /// </summary>
        /// <param name="msgName">Assigned message name</param>
        /// <param name="msgHandler">Assigned message handler</param>
        /// <param name="msgReceiver">Assigend message received</param>
        /// <returns></returns>
        static public int RegisterMessageUser(string msgName, InternalMessageHandler msgHandler, object msgReceiver)
        {
            MessageUser item = new MessageUser(msgName, msgHandler, msgReceiver);
            myUserList.Add(item);
            return 0;
        }

        /// <summary>
        /// Post one message via global message center with assigned message type/message content/message receiver.
        /// </summary>
        /// <param name="msgType">The type of the message to be post.</param>
        /// <param name="msgContext">The content of the message to be post.</param>
        /// <param name="receiver">The reciever of the message to be post</param>
        /// <returns></returns>
        static public int PostMessage(string msgType, object msgContext, object receiver)
        {
            InternalMessage message = new InternalMessage(msgType, msgContext);
            foreach (MessageUser user in myUserList)
            {
                if (msgType == user.msgType)
                {
                    if (user.msgReceiver != null)
                    {
                        if (user.msgReceiver != receiver) continue;
                    }
                    user.msgHandler(message);
                }
            }
            return 0;
        }

        /// <summary>
        /// Post one message via global message center with assigned message type/message content.
        /// </summary>
        /// <param name="msgType">The type of the message to be post.</param>
        /// <param name="msgContext">The content of the message to be post.</param>
        /// <returns></returns>
        static public int PostMessage(string msgType, object msgContext)
        {
            return PostMessage(msgType, msgContext, null);
        }

    }

}
