#region#Copyright Information#
///Created by JimmyGong 2017.01.01
///For IA978 Project UI Design.
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using DMTec.TMListener;
using DMTec.TestUIFramework.BasicControls;
//
namespace DMTec.TestUIFramework.AdvancedControls
{
    public partial class TesterSimple : UserControl
    {
        #region//Members//

        Tester myTester;

        ChamberSimple chamberSimple1;
        ChamberSimple chamberSimple2;

        List<ChamberSimple> list_ChamberSimples;

        /// <summary>
        /// For ALC Call To Switch Detail View.
        /// </summary>
        public event EventHandler TestID_Clicked;


        /// <summary>
        /// LotID
        /// </summary>
        string _lotID = "N/A";

        /// <summary>
        /// The Type of Tester.
        /// </summary>
        string _testerType  = "N/A";

        /// <summary>
        /// Target Sequencer1 net address.
        /// </summary>
        private string _address1 = "N/A";

        /// <summary>
        /// Target Sequencer2 net address.
        /// </summary>
        private string _address2 = "N/A";

        /// <summary>
        /// 
        /// </summary>
        bool _canManualEnable = true;

        /// <summary>
        /// 
        /// </summary>
        bool _isTesterEnable = true;

        #endregion//Members...End//

        #region//Constructors//

        public TesterSimple()
        {
            InitializeComponent();
            //myTester = new Tester();
            chamberSimple1 = new ChamberSimple();
            chamberSimple2 = new ChamberSimple();

            InitUI();
            InitMembers();
        }

        public TesterSimple(Tester parent)
        {
            InitializeComponent();
            myTester = parent;
            list_ChamberSimples = new List<ChamberSimple>();
            chamberSimple1 = new ChamberSimple();
            chamberSimple1.ChamberNo = 1;
            chamberSimple1.SetTester(myTester);

            chamberSimple2 = new ChamberSimple();
            chamberSimple2.ChamberNo = 2;
            chamberSimple2.SetTester(myTester);

            //list_ChamberSimples.Add(chamberSimple1);
            //list_ChamberSimples.Add(chamberSimple2);

            InitUI();
            InitMembers();
        }

        public TesterSimple(ChamberSimple cs1, ChamberSimple cs2)
        {
            InitializeComponent();
            myTester = new Tester();
            chamberSimple1 = cs1;
            chamberSimple2 = cs2;

            InitUI();
            InitMembers();
        }

        #endregion//Constructors...End//

        #region//Properties//
        /// <summary>
        /// Is allow to enable tester or chambers manually.
        /// </summary>
        [Browsable(true), DefaultValue("false"), Description("Set and Read Can Manual Enable Tester or Chamber."), Category("CustomProperties")]
        public bool CanManualEnable
        {
            get
            {
                return _canManualEnable;
            }
            set
            {
                _canManualEnable = value;
                ckb_IsTesterEnable.Enabled = value;
                chamberSimple1.CanManualEnable = value;
                chamberSimple2.CanManualEnable = value;
            }
        }

        [Browsable(true), DefaultValue("false"), Description("Set and Read Tester's Enabled"), Category("CustomProperties")]
        public bool IsTesterEnable
        {
            get
            {
                return _isTesterEnable;
            }
            set
            {
                if (_isTesterEnable == value) return;
                _isTesterEnable = value;
                ckb_IsTesterEnable.Checked = value;
                chamberSimple1.IsChamberEnable = value;
                chamberSimple2.IsChamberEnable = value;
            }
        }

        [Browsable(true), DefaultValue(""), Description("Set and Read Tester's LotID string"), Category("CustomProperties")]
        public string LotID
        {
            get
            {
                return _lotID;
            }
            set
            {
                _lotID = value;
            }
        }

        [Browsable(true), DefaultValue("Type1"), Description("Set and Read Tester's Type string"), Category("CustomProperties")]
        public string TestType
        {
            get
            {
                return _testerType;
            }
            set
            {
                _testerType = value;
            }
        }

        [Browsable(true), DefaultValue("Tester#01"), Description("Set and Read Tester ID string"), Category("CustomProperties")]
        public string TesterID
        {
            get
            {
                return btn_TesterID.Text;
            }
            set
            {
                btn_TesterID.Text = value;
            }
        }

        /// <summary>
        /// The property of net address(sequencer) 
        /// </summary>
        [Browsable(true), Description("Set and get <NetAddress1> string"), Category("CustomProperties")]
        public string SubAddress1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
                chamberSimple1.SeqSubAddress = _address1;
            }
        }

        /// <summary>
        /// The property of net address(sequencer) 
        /// </summary>
        [Browsable(true), Description("Set and get <NetAddress2> string"), Category("CustomProperties")]
        public string SubAddress2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
                chamberSimple2.SeqSubAddress = _address2;
            }
        }
        
        public string Chamber1_SocketSN
        {
            get
            {
                return chamberSimple1.SocketSN;
            }
            set
            {
                chamberSimple1.SocketSN = value;
            }
        }

        public string Chamber2_SocketSN
        {
            get
            {
                return chamberSimple2.SocketSN;
            }
            set
            {
                chamberSimple2.SocketSN = value;
            }
        }

        /// <summary>
        /// ID string of Chamber1
        /// </summary>
        public string Chamber1_ID
        {
            get
            {
                return chamberSimple1.ChamberName;
            }
            set
            {
                chamberSimple1.ChamberName = value;
            }
        }

        /// <summary>
        /// ID string of Chamber2
        /// </summary>
        public string Chamber2_ID
        {
            get
            {
                return chamberSimple2.ChamberName;
            }
            set
            {
                chamberSimple2.ChamberName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Chamber1_Enable
        {
            get
            {
                return chamberSimple1.IsChamberEnable;
            }
            set
            {
                chamberSimple1.IsChamberEnable = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Chamber2_Enable
        {
            get
            {
                return chamberSimple2.IsChamberEnable;
            }
            set
            {
                chamberSimple2.IsChamberEnable = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Chamber1_Address
        {
            get
            {
                return chamberSimple1.SeqSubAddress;
            }
            set
            {
                chamberSimple1.SeqSubAddress = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Chamber2_Address
        {
            get
            {
                return chamberSimple2.SeqSubAddress;
            }
            set
            {
                chamberSimple2.SeqSubAddress = value;
            }
        }

        private int _yieldLowLimit = 0;
        public int YieldLowLimit
        {
            get
            {
                return _yieldLowLimit;
            }
            set
            {
                _yieldLowLimit = value;
                chamberSimple1.YieldLowLimit = value;
                chamberSimple2.YieldLowLimit = value;
            }
        }


        private int _yieldCountRange = 100;
        public int YieldCountRange
        {
            get
            {
                return _yieldCountRange;
            }
            set
            {
                _yieldCountRange = value;
                chamberSimple1.YieldCountRange = value;
                chamberSimple2.YieldCountRange = value;
            }
        }
        #endregion//Properties...End//

        #region//Methods//

        private void InitUI()
        {
            tlp_Chambers.Controls.Add(chamberSimple1, 0, 0);
            tlp_Chambers.Controls.Add(chamberSimple2, 0, 1);
            Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitMembers()
        {
            //BindNetEvent();
            //initTimer();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BindNetEvents(int chamberNo, ref SequencerListener listener)
        {
            if(1 == chamberNo)
            {
                //listener.event_Start += chamberSimple1.event_Start
                //listener.event_Start = new SeqStartHandler()
                listener.event_SeqStart += chamberSimple1.OnSeqStartHandle;

                listener.event_SeqEnd += chamberSimple1.OnSeqEndHandle;
                listener.event_ItemStart += chamberSimple1.OnSeqItemStartHandle;
                listener.event_ItemEnd += chamberSimple1.OnSeqItemEndHandle;
                listener.event_AttributeFound += chamberSimple1.OnSeqAttributeFoundHandle;
            }
            else if(2 == chamberNo)
            {
                listener.event_SeqStart += chamberSimple2.OnSeqStartHandle;
                listener.event_SeqEnd += chamberSimple2.OnSeqEndHandle;
                listener.event_ItemStart += chamberSimple2.OnSeqItemStartHandle;
                listener.event_ItemEnd += chamberSimple2.OnSeqItemEndHandle;
                listener.event_AttributeFound += chamberSimple2.OnSeqAttributeFoundHandle;
            }
            else
            {
                //
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public ChamberSimple GetChamberSimple(int num)
        {
            if (1 == num) return chamberSimple1;
            else if (2 == num) return chamberSimple2;
            else return null; 
            //if(null == list_ChamberSimples[i])
            //{
            //    return null;
            //}
            //else
            //{
            //    return list_ChamberSimples[i];
            //}
        }

        #endregion//Methods...End//

        #region//Events//

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TesterID_Click(object sender, EventArgs e)
        {
            if (TestID_Clicked != null)
            {
                TestID_Clicked.Invoke(this, e);
            }
        }

        public void SeqStartHandle_C1(object sender, SeqStartArgs arg)
        {
            chamberSimple1.OnSeqStartHandle(sender, arg);
        }

        public void SeqStartHandle_C2(object sender, SeqStartArgs arg)
        {
            chamberSimple2.OnSeqStartHandle(sender, arg);
        }
        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqStartHandle(object sender, SeqStartArgs arg)
        {
            string msg = ">>>Seq Start Event Happened!\r\n";
            string name = arg.name;
            string version = arg.version;
            string module = arg.Publisher;

            msg += "publisher: " + module + "\r\n";
            msg += "name: " + name + "\r\n";
            msg += "version: " + version + "\r\n\r\n";

            //MsgPrint(this, msg);
        }

        /// <summary>
        /// Handle sequence end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqEndHandle(object sender, SeqEndArgs arg)
        {
            string msg = ">>>Seq End Event Happened!\r\n";
            string result = arg.result.ToString();
            string log = arg.logs;
            string error = arg.error;
            string publisher = arg.Publisher;

            msg += "publisher : " + publisher + "\r\n";
            msg += "result: " + result + "\r\n";
            msg += "log: " + log + "\r\n";
            msg += "error: " + error + "\r\n\r\n";

            //MsgPrint(this, msg);
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemStartHandle(object sender, SeqItemStartArgs arg)
        {
            string msg = ">>>Item Start Event Happened!\r\n";
            string publisher = arg.Publisher;
            string group = arg.group;
            string tid = arg.tid;
            string high = arg.high;
            string low = arg.low;
            string unit = arg.unit;
            string to_pdca = arg.to_pdca;

            msg += "publisher : " + publisher + "\r\n";
            msg += "group : " + group + "\r\n";
            msg += "tid : " + tid + "\r\n";
            msg += "high : " + high + "\r\n";
            msg += "low : " + low + "\r\n";
            msg += "unit : " + unit + "\r\n";
            msg += "to_pdca : " + to_pdca + "\r\n\r\n";

            //MsgPrint(this, msg);
        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            string msg = ">>>Item End Event Happened!\r\n";

            string publisher = arg.Publisher;
            string tid = arg.tid;
            string result = arg.result.ToString();
            string value = arg.value;
            string to_pdca = arg.to_pdca;
            string error = arg.error;

            msg += "publisher : " + publisher + "\r\n";
            msg += "tid : " + tid + "\r\n";
            msg += "result : " + result + "\r\n";
            msg += "value : " + value + "\r\n";
            msg += "error : " + error + "\r\n";
            msg += "to_pdca : " + to_pdca + "\r\n\r\n";

            //MsgPrint(this, msg);
        }

        /// <summary>
        /// Handle Attribute Found event
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void AttributeFoundHandle(object sender, SeqAttrFoundArgs arg)
        {
            //string msg = ">>>New Attribute Found!\r\n";

            //string module = arg.publisher;
            //string name = arg.name;
            //string value = arg.value;

            //msg += "Publisher: " + module + "\r\nname: " + name + "\r\nvalue: " + value + "\r\n\r\n";

            //MsgPrint(this,msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_IsTesterEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(CanManualEnable)
            {
                myTester.IsTesterEnable = ckb_IsTesterEnable.Checked;
            }
        }

#endregion//Events...End//

    }
}
