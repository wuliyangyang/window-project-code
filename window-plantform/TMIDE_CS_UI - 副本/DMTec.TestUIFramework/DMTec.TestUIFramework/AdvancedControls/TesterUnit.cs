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
    public partial class TesterUnit : UserControl
    {
        #region//Members//

        Tester myFatherTester;

        ChamberUnit chamberUnit1;
        ChamberUnit chamberUnit2;


        /// <summary>
        /// Tester ID
        /// </summary>
        string _testerID = "N/A";

        /// <summary>
        /// Tester Type
        /// </summary>
        string _testerType = "N/A";

        /// <summary>
        /// Lot ID
        /// </summary>
        string _lotId = "N/A";

        /// <summary>
        /// For Binding between of DataGridView and DataSource.
        /// </summary>
        DataTable _dataTable;

        bool _canManualEnable = false;

        bool _isTesterEnable = true;

        bool _isEnableChamber_1 = true;

        bool _isEnableChamber_2 = true;

        int _uph_C1 = 0;

        int _uph_C2 = 0;


        #endregion//Members...End//

        #region//Properties//

        /// <summary>
        /// Is allow to enable tester or chambers manually.
        /// </summary>
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
                chamberUnit1.CanManualEnable = value;
                chamberUnit2.CanManualEnable = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTesterEnable
        {
            get
            {
                return _isTesterEnable;
            }
            set
            {
                //if (_isTesterEnable == value) return;
                //_isTesterEnable = value;
                
                ckb_IsTesterEnable.Checked = value;
                chamberUnit1.ChamberEnable = value;
                chamberUnit2.ChamberEnable = value;
            }
        }

        /// <summary>
        /// The Property of LotID
        /// </summary>
        public string LotID
        {
            get
            {
                return _lotId;
            }
            set
            {
                _lotId = value;
                chamberUnit1.LotID = value;
                chamberUnit2.LotID = value;
            }
        }

        /// <summary>
        /// The Property of TesterType
        /// </summary>
        public string TesterType
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

        /// <summary>
        /// The Property of TesterID.
        /// This Is The Only Identity Of The Control!
        /// </summary>
        public string TesterID
        {
            get
            {
                return _testerID;
            }
            set
            {
                _testerID = value;
                lb_TesterID.Text = value;
            }
        }

        public string Chamber1_Address
        {
            get
            {
                return chamberUnit1.SubAddress2Seq;
            }
            set
            {
                chamberUnit1.SubAddress2Seq = value;
            }
        }

        public string Chamber2_Address
        {
            get
            {
                return chamberUnit2.SubAddress2Seq;
            }
            set
            {
                chamberUnit2.SubAddress2Seq = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Chamber1_Enable
        {
            get
            {
                return chamberUnit1.ChamberEnable;
            }
            set
            {
                chamberUnit1.ChamberEnable = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Chamber2_Enable
        {
            get
            {
                return chamberUnit2.ChamberEnable;
            }
            set
            {
                chamberUnit2.ChamberEnable = value;
            }
        }

        public string Chamber1_ID
        {
            get
            {
                return chamberUnit1.ChamberName;
            }
            set
            {
                chamberUnit1.ChamberName = value;
            }
        }

        public string Chamber2_ID
        {
            get
            {
                return chamberUnit2.ChamberName;
            }
            set
            {
                chamberUnit2.ChamberName = value;
            }
        }

        public string Chamber1_SocketSN
        {
            get
            {
                return chamberUnit1.SocketSN;
            }
            set
            {
                chamberUnit1.SocketSN = value;
            }
        }

        public string Chamber2_SocketSN
        {
            get
            {
                return chamberUnit2.SocketSN;
            }
            set
            {
                chamberUnit2.SocketSN = value;
            }
        }

        public int Chamber1_DPH
        {
            get{
                return chamberUnit1.DPH;
            }
            set{
                chamberUnit1.DPH = value;
            }
        }

        public int Chamber2_DPH
        {
            get
            {
                return chamberUnit2.DPH;
            }
            set
            {
                chamberUnit2.DPH = value;
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
                chamberUnit1.YieldLowLimit = value;
                chamberUnit2.YieldLowLimit = value;
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
                chamberUnit1.YieldCountRange = value;
                chamberUnit2.YieldCountRange = value;
            }
        }

        #endregion//Properties...End//

        #region//Constructors//

        /// <summary>
        /// 
        /// </summary>
        public TesterUnit()
        {
            InitializeComponent();
        }

        public TesterUnit(Tester tester)
        {
            InitializeComponent();
            myFatherTester = tester;
            chamberUnit1 = new ChamberUnit(1, tester);
            chamberUnit2 = new ChamberUnit(2, tester);
            tlp_Units.Controls.Add(chamberUnit1, 0, 0);
            tlp_Units.Controls.Add(chamberUnit2, 1, 0);
        }

        #endregion//Constructors...End//

        #region//Methods//

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            _dataTable = new DataTable("TestItemList");
        }

        /// <summary>
        /// Binding TM Event For Automatic Notify.
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <param name="listener"></param>
        /// <param name="requester"></param>
        public void BindNetEvents(int chamberNo, ref SequencerListener listener, ref SeqRequester requester)
        {
            if (1 == chamberNo)
            {
                chamberUnit1.BindNetEvents(ref listener, ref requester);
            }
            else if (2 == chamberNo)
            {
                chamberUnit2.BindNetEvents(ref listener, ref requester);
            }
            else
            {
                //
            }
        }

        public void BindNetEvents(int chamberNo, ref SequencerListener listener)
        {
            if (1 == chamberNo)
            {
                chamberUnit1.BindSeqListenerEvents(ref listener);
            }
            else if (2 == chamberNo)
            {
                chamberUnit2.BindSeqListenerEvents(ref listener);
            }
            else
            {
                //
            }
        }

        /// <summary>
        /// Get The ChamberUnit Instance.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ChamberUnit GetChamberUnit(int num)
        {
            if (1 == num) return chamberUnit1;
            else if (2 == num) return chamberUnit2;
            else return null; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_IsTesterEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (CanManualEnable)
            {
                myFatherTester.IsTesterEnable = ckb_IsTesterEnable.Checked;
                //chamberUnit1.ChamberEnable = ckb_IsTesterEnable.Checked;
                //chamberUnit2.ChamberEnable = ckb_IsTesterEnable.Checked;
            }
        }
        #endregion//Methods...End//

    }
}