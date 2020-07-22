using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TestUIFramework.Common;
using DMTec.TMListener;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One ui control used to show dut state.
    /// </summary>
    [DescriptionAttribute("One ui control used to show dut state.")]
    public partial class UutStateArrayPanel : UserControl
    {

        #region//Members//

        private List<UUT> myUutList;

        private string[] myBarcodeList;

        private Dictionary<string, bool> dict_UutEnable;

        private int uutSlots = 1;

        private int uutSlotsOfSM = 1;

        private int currentInputIndex = 0;

        private TextBox barcodeInputBox;

        public List<StateMachineConnector> myStateMachineConnectorList { get; set; }

        #endregion//Members___END//

        #region//Constructors//

        /// <summary>
        /// One Multi-UUT Panel that can display UUT array in one column bar.
        /// </summary>
        /// <param name="slots"></param>
        public UutStateArrayPanel()
            : this(1) { }

        public UutStateArrayPanel(int slots)
        {
            InitializeComponent();
            InitMembers();
            Slots = slots;
        }
        public UutStateArrayPanel(List<SequencerListener> list)
        {
            InitializeComponent();
            InitMembers();
            if (list.Count <= 0) return;
            else Slots = list.Count;
        }

        #endregion//Constructors___END//

        #region//Inderer//

        /// <summary>
        /// Indexer of this class to get or set special UUT.
        /// </summary>
        /// <param name="i">The index of UUT.</param>
        /// <returns></returns>
        public UUT this[int i]
        {
            get
            {
                if (i < 0 || i > myUutList.Count) return null;
                else return myUutList[i];
            }
            set
            {
                if (i < 0 || i > myUutList.Count) return;
                myUutList[i] = value;
            }
        }

        #endregion//Inderer___END//

        #region//Properties//

        [BrowsableAttribute(true)]
        [DefaultValueAttribute(typeof(int), "1")]
        [DescriptionAttribute("How many uut will display.")]
        [CategoryAttribute("CustomProperties")]
        public int Slots
        {
            get { return uutSlots; }
            set { InitUUTArray(value); }
        }
        public int SM_Slots
        {
            get { return uutSlotsOfSM; }
            set { uutSlotsOfSM = value; }
        }

        //public TextBox InputBox
        //{
        //    get { return barcodeInputBox; }
        //    set { barcodeInputBox = value; }
        //}

        /// <summary>
        /// Get the list of UUT.
        /// </summary>
        public List<UUT> UUTList
        {
            get { return myUutList; }
        }

        #endregion//Properties___END//

        #region//Private Methods//
        private void InsertOneSn(string sn)
        {
            if (currentInputIndex >= myUutList.Count || currentInputIndex < 0)
            {
                currentInputIndex = 0;
            }
            myUutList[currentInputIndex++].DutSN = sn;
        }

        protected void InitUUTArray(int slots)
        {
            uutSlots = slots;

            if (uutSlots <= 0) uutSlots = 1;
            if (uutSlots > Consts.Fixture_Station_Slots_Max) uutSlots = Consts.Fixture_Station_Slots_Max;

            tlp_Main.SuspendLayout();
            this.SuspendLayout();

            this.tlp_Main.Controls.Clear();
            this.tlp_Main.RowStyles.Clear();
            this.dict_UutEnable.Clear();
            this.tlp_Main.RowCount = uutSlots + 1;
            float percentPerRow = (float)100 / uutSlots;
            float heightPerRow = (float)tlp_Main.Height / uutSlots;

            myUutList = new List<UUT>(uutSlots);

            for (int i = 0; i < uutSlots; i++)
            {
                UUT uut = new UUT();
                uut.ProgressValue = 0;
                uut.DutNo = i;
                uut.DutSN = "UUT" + i.ToString();
                if (heightPerRow > tlp_Main.Width / 2)
                    uut.MaximumSize = new Size(uut.MaximumSize.Width, tlp_Main.Width / 2);

                uut.Dock = DockStyle.Fill;
                myUutList.Add(uut);
                //this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, percentPerRow));
                this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));

                this.tlp_Main.Controls.Add(uut);
                this.tlp_Main.SetColumn(uut, 0);
                this.tlp_Main.SetRow(uut, i);

                uut.IsUUTEnable = true;
                uut.Name = "UUT" + i.ToString();
                this.dict_UutEnable.Add(uut.Name, uut.IsUUTEnable);

                //yy
                //uut.mycheckBox.CheckedChanged += checkBox_CheckedChanged1;
            }

            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            barcodeInputBox.Dock = DockStyle.Fill;
            this.tlp_Main.Controls.Add(barcodeInputBox);
            this.tlp_Main.SetColumn(barcodeInputBox, 0);
            this.tlp_Main.SetRow(barcodeInputBox, uutSlots);

            //this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            //checkBox.Dock = DockStyle.Top;
            //this.tlp_Main.Controls.Add(checkBox);
            //this.tlp_Main.SetColumn(checkBox, 0);
            //this.tlp_Main.SetRow(checkBox, uutSlots + 1);

            this.tlp_Main.ResumeLayout();
            this.tlp_Main.PerformLayout();
            this.ResumeLayout();
        }

        private void MycheckBox_EnabledChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitMembers()
        {
            dict_UutEnable = new Dictionary<string, bool>();

            barcodeInputBox = new TextBox() { Text = "Input sn here!" };
            barcodeInputBox.TextAlign = HorizontalAlignment.Center;
            barcodeInputBox.Margin = new System.Windows.Forms.Padding(1);
            barcodeInputBox.Font = new Font("YaHei", 20);
            barcodeInputBox.ForeColor = Color.Blue;
            barcodeInputBox.KeyDown += barcodeInputBox_KeyDown;
            barcodeInputBox.Enter += barcodeInputBox_Enter;

        }

        private void barcodeInputBox_Enter(object sender, EventArgs e)
        {
            barcodeInputBox.Text = "";
        }

        private void barcodeInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //if (currentInputIndex >= myUutList.Count || currentInputIndex < 0)
            //{
            //    currentInputIndex = 0;
            //}
            //if (myUutList[currentInputIndex].TestState != DMTec.TestUIFramework.BasicControls.UUT.TestStatus.TESTING)
            //{
            //    InsertOneSn(barcodeInputBox.Text);
            //    barcodeInputBox.Text = "";
            //}
            //}
        }

        private void SetInputBoxDisable()
        {
            barcodeInputBox.Enabled = false;
        }

        private void SetInputBoxEnable()
        {
            if (barcodeInputBox.Enabled == false)
            {
                barcodeInputBox.Enabled = true;
                barcodeInputBox.Focus();
                barcodeInputBox.Select(0, 0);
                barcodeInputBox.ScrollToCaret();
            }
        }

        #endregion//Private Methods___END//

        #region//Public Methods//

        public void DisableInputBox()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(SetInputBoxDisable));
            }
            else
            {
                SetInputBoxDisable();
            }
        }

        public void EnableInputBox()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(SetInputBoxEnable));
            }
            else
            {
                SetInputBoxEnable();
            }
        }

        public int inputNewBarcode()
        {
            if (barcodeInputBox.Text == "")
            {
                MessageBox.Show("sn can not be empty!!!");
                return -1;
            }
            else if (barcodeInputBox.Text.Length < 0 || barcodeInputBox.Text.Length > 5)
            {
                barcodeInputBox.Text = "";
                MessageBox.Show("sn length should be more than 17 and less than 20!!!");
                return -2;
            }

            int validSlots = 0;

            for (int i = 0; i < UUTList.Count; i++)
            {
                if (UUTList[i].IsUUTEnable) validSlots++;
            }
            Console.WriteLine("validSlots:" + validSlots);
            bool isTest = false;
            for (int i = 0; i < validSlots; i++)
            {
                if (currentInputIndex >= myUutList.Count || currentInputIndex < 0)
                {
                    currentInputIndex = 0;
                }

                if (myUutList[currentInputIndex].TestState != DMTec.TestUIFramework.BasicControls.UUT.TestStatus.TESTING && myUutList[currentInputIndex].IsUUTEnable)
                {
                    InsertOneSn(barcodeInputBox.Text);
                    barcodeInputBox.Text = "";
                    isTest = true;
                    break;
                }
                else
                {
                    currentInputIndex++;
                }
            }

            if (!isTest)
            {
                barcodeInputBox.Text = "";
                MessageBox.Show("no slot is enable or idle");
                return -5;
            }

            /*
                 if (currentInputIndex % SM_Slots == 0)//to confirm this slot is last one of current SM
                 {
                     // bool isDisableInputBox = true;
                     for (int i = 0; i < validSlots; i++)
                     {
                         if (i >= currentInputIndex - SM_Slots && i < currentInputIndex)
                         {
                             if (myUutList[i].DutSN == "")
                             {
                                 return -3;
                             }
                         }
                         else//other slots
                         {
                             // if (myUutList[i].TestState != DMTec.TestUIFramework.BasicControls.UUT.TestStatus.TESTING  && myUutList[i].IsUUTEnable)
                             // {
                             //     isDisableInputBox = false;
                             // }
                         }
                     }

                     // if(isDisableInputBox)
                     // {
                     //     DisableInputBox();
                     // }

                     return (currentInputIndex - 1) / SM_Slots;
                 }

                 return -4
         */
            if (currentInputIndex >= validSlots) return 0;
            return -6;
        }

        public void BindSeqListeners(ref List<SequencerListener> listeners)
        {
            for (int i = 0; i < this.Slots; i++)
            {
                SequencerListener listener = listeners[i];
                int rst = myUutList[i].BindSequencerListener(ref listener);
                //int rst = listeners[i].BindEventHandler(myUutList[i]);
                if (0 != rst) MessageBox.Show("UUT Bind Sequencer Listener[" + listener.SubscribeAddress + "] Event Failed!!!");
            }
        }

        public void BindLoggerListeners(ref List<LoggerListener> listeners)
        {
            for (int i = 0; i < this.Slots; i++)
            {
                LoggerListener listener = listeners[i];
                int rst = myUutList[i].BindLoggerListener(ref listener);
                //int rst = listeners[i].BindEventHandler(myUutList[i]);
                if (0 != rst) MessageBox.Show("UUT Bind Logger Listener[" + listener.SubscribeAddress + "] Event Failed!!!");
            }
        }

        /// <summary>
        /// Set the count of test items.
        /// The count is used to calculate test progress.
        /// </summary>
        /// <param name="itemCount"></param>
        public void SetTestItemCount(int itemCount)
        {
            if (null == myUutList || 0 == myUutList.Count) return;
            foreach (UUT uut in myUutList)
            {
                uut.ItemCount = itemCount;
            }
        }

        public void FillUutSn(string[] snList)
        {
            if (Slots != snList.Length) return;

            for (int i = 0; i < snList.Length; i++)
            {
                this[i].DutSN = snList[i];
            }
        }


        public List<string> GetSnList()
        {
            List<string> sns = new List<string>();

            foreach (UUT uut in UUTList)
            {
                if (uut.DutSN == "UUT" + uut.DutNo.ToString())
                {
                    sns.Add("");
                }
                else
                {
                    sns.Add(uut.DutSN);
                }

            }

            return sns;
        }

        #endregion//Public Methods___END//

        #region//Event Handlers//

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UUT uut in myUutList)
            {
                uut.IsUUTEnable = checkBox.Checked;
            }
            for (int i = 0; i < myStateMachineConnectorList.Count; i++)
            {
                string reqMsg = "UIUUT";
                for (int j = 0; j < myUutList.Count; j++)
                {
                    if (myUutList[j].IsUUTEnable)
                    {
                        reqMsg = reqMsg + ":" + j.ToString() + "_" + "";
                    }
                }
                Console.WriteLine(reqMsg);
                myStateMachineConnectorList[i].RequestMsg(reqMsg);
            }
        }
        private void checkBox_CheckedChanged1(object sender, EventArgs e)
        {
            //foreach (UUT uut in myUutList)
            //{
            //    uut.IsUUTEnable = checkBox.Checked;
            //}
            for (int i = 0; i < myStateMachineConnectorList.Count; i++)
            {
                string reqMsg = "UIUUT";
                for (int j = 0; j < myUutList.Count; j++)
                {
                    if (myUutList[j].IsUUTEnable)
                    {
                        reqMsg = reqMsg + ":" + j.ToString() + "_" + "";
                    }
                }
                Console.WriteLine(reqMsg);
                myStateMachineConnectorList[i].RequestMsg(reqMsg);
            }
        }


        #endregion//Event Handlers___END//

    }
}
