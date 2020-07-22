using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.MultiThreadComponent
{
    public class AsyncTaskMessageBox
    {
        /// <summary>
        /// All kinds of MessageBox.
        /// </summary>
        [DescriptionAttribute("All kinds of MessageBox.")]
        public enum AsyncMessageBoxType
        {
            [DescriptionAttribute("Information")]
            InformationBox = 1,
            [DescriptionAttribute("Waring")]
            WarningBox = 2,
            [DescriptionAttribute("Error")]
            ErrorBox = 3,
            [DescriptionAttribute("Question")]
            QuestionBox = 4
        }

        /// <summary>
        /// MessageBox String Context.
        /// </summary>
        [DescriptionAttribute("MessageBox String Context.")]
        public string Message { get; set; }

        /// <summary>
        /// MessageBox Window Title.
        /// </summary>
        [DescriptionAttribute("MessageBox Window Title.")]
        public string Title { get; set; }

        /// <summary>
        /// MessageBox Type.
        /// </summary>
        [DescriptionAttribute("MessageBox Type.")]
        public AsyncMessageBoxType MessageType { get; set; }

        /// <summary>
        /// Show One MessageBox in workerThread.
        /// </summary>
        /// <param name="msg">Message content to be show.</param>
        /// <param name="title">MessageBox Title</param>
        /// <param name="type">MessageBox Type</param>
        public AsyncTaskMessageBox(string msg, string title, AsyncMessageBoxType type)
        {
            Message = msg;
            Title = title;
            MessageType = type;
            Thread worker = new Thread(OnThreadRun);
            worker.Start();
        }

        private void OnThreadRun()
        {
            if (AsyncMessageBoxType.InformationBox == MessageType)
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (AsyncMessageBoxType.WarningBox == MessageType)
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (AsyncMessageBoxType.ErrorBox == MessageType)
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
