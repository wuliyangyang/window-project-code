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
using DMTec.TestUIFramework.Common;
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    public partial class DUT : UserControl
    {

        #region//Members

        /// <summary>
        /// 
        /// </summary>
        private bool _isSequencerHB_OK;

        /// <summary>
        /// DUT's Number
        /// For DUT Instance Statistics.
        /// </summary>
        private static int _dutNumber = 0;

        #endregion//Members

        #region//Constructors//

        /// <summary>
        /// 
        /// </summary>
        public DUT()
        {
            InitializeComponent();
            InitMember();
        }

        /// <summary>
        /// Constructor(You can assign the DUT's SN)
        /// </summary>
        /// <param name="i">DUT's No</param>
        public DUT(string dutName)
        {
            InitializeComponent();
            InitMember();
            
            DutName = dutName;
        }

        #endregion//Constructors//

        #region//MethodsAndEvents//
        
        /// <summary>
        /// 
        /// </summary>
        private void InitMember()
        {
            _dutNumber++;
            IsShowColorUpdate = true;
        }


        private void InitUI()
        {
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// DUT Control's Loading Event Handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DUT_Load(object sender, EventArgs e)
        {
            
        }

        private void DUT_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void lb_DutStatus_Resize(object sender, EventArgs e)
        {
//             int size = 0;
//             if (lb_DutStatus.Height > lb_DutStatus.Width) size = lb_DutStatus.Width;
//             else size = lb_DutStatus.Height;
//             Font font = new Font(lb_DutStatus.Font.FontFamily, size * 0.4f);
            lb_DutStatus.Font = GetLabelAutoSizeFont(lb_DutStatus);
            lb_DutStatus.Invalidate();
        }

        private void lb_DutSN_Resize(object sender, EventArgs e)
        {
            //lb_DutSN.Font = GetLabelAutoSizeFont(lb_DutSN);
            lb_DutSN.Invalidate();
        }

        private Font GetLabelAutoSizeFont(Label label)
        {
            Font newFont = new Font(label.Font.FontFamily,
                                    label.Font.Size,
                                    label.Font.Style,
                                    GraphicsUnit.Pixel);

            float emSize = newFont.FontFamily.GetEmHeight(newFont.Style);
            float lineSpacing = newFont.FontFamily.GetLineSpacing(newFont.Style);

            float newEmSize = (label.Height - 7) * emSize / lineSpacing;
            newFont = new Font(newFont.FontFamily, newEmSize, newFont.Style, GraphicsUnit.Pixel);
            return newFont;
        }

        #region//Sequence Event Handlers//

        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void SeqStartHandle(object sender, SeqStartArgs arg)
        {
            DutStatusString = DataModel.DutTestStatus.Testing.ToString();
            DUTColor = Color.Yellow;
            SetProgress(0);
        }

        /// <summary>
        /// Handle sequence end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void SeqEndHandle(object sender, SeqEndArgs arg)
        {
            //DutStatusString = DutTestStatus.TestDone.ToString();

            if ("1" == arg.result)
            {
                DUTColor = Color.Green;
                DutStatusString = "PASS";
            }
            else
            {
                DUTColor = Color.Red;
                DutStatusString = "FAIL";
            }

            SetProgress(progressBar.Maximum);
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void ItemStartHandle(object sender, SeqItemStartArgs arg)
        {

        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void ItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            int i = progressBar.Value;
            SetProgress(++i);
        }

        /// <summary>
        /// Item List Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="list"></param>
        public void ItemListHandle(object sender, SeqItemListArgs list)
        {
            int itemsCount = list.ItemList.Count;
            if (itemsCount > 0)
            {
                SetProgressMax(itemsCount);
            }
        }

        #endregion//Sequence Event Handlers...END//

        /// <summary>
        /// Set Progress's Max Value.
        /// </summary>
        /// <param name="max"></param>
        public void SetProgressMax(int max)
        {
            if (max < 1) return;

            if (InvokeRequired)
            {
                this.Invoke(new Action<int>((x) => SetProgressMax(x)), max);
            }
            else
            {
                progressBar.Maximum = max;
            }
        }

        /// <summary>
        /// Set ProgressBar Value with Assigned Int Value.
        /// </summary>
        /// <param name="value">Assigned Int Value</param>
        private void SetProgress(int value)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<int>(SetProgress), value);
                }
                else
                {
                    if (value > progressBar.Maximum || value < 0) return;
                    progressBar.Value = value;
                    progressBar.Invalidate();
                    Application.DoEvents();
                }
                
            }
            catch (Exception e) 
            { MessageBox.Show(e.Message,"Set Progress Exception",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
        
        #endregion//MethodsAndEvents//

        #region//CustomProperties//

        [Browsable(true), DefaultValue(false), Description("If enable internal timer(update ui)."), Category("CustomProperties")]
        public bool IsTimerEnable { get; set; }

        /// <summary>
        /// Get Serial Number of Current DUT Instance.
        /// </summary>
        public int DutNumber
        {
            get { return _dutNumber; }
        }

        /// <summary>
        /// Set and Get Control's Background Color.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color),"White"), Description("Set and Get Control's BackColor."), Category("CustomProperties")]
        public Color DUTColor
        {
            get
            {
                return _dutColor;
            }
            set
            {
                _dutColor = value;
                SetBackgroundColor(_dutColor);
            }
        }Color _dutColor = Color.White;

        private void SetBackgroundColor(Color c)
        {
            if (false == IsShowColorUpdate) return;
            if (this.InvokeRequired)
            {
                Invoke(new Action<Color>(SetBackgroundColor), c);
            }
            else
            {
                this.BackColor = c;
                this.Invalidate();
                Application.DoEvents();
            }
        }

        [Browsable(true), DefaultValue(true), Description("If the back color will change with test status."), Category("CustomProperties")]
        public bool IsShowColorUpdate { get; set; }

        /// <summary>
        /// Set and Get DUT's Status String.
        /// </summary>
        [Browsable(true), DefaultValue("IDLE"), Description("Set and Get DUT's Status String."), Category("CustomProperties")]
        public string DutStatusString
        {
            get
            {
                return lb_DutStatus.Text;
            }
            set
            {
                SetDutStatusDisplay(value);
            }
        }
        
        private void SetDutStatusDisplay(string s)
        {
            if (lb_DutStatus.InvokeRequired)
            {
                this.Invoke(new Action<string>(SetDutStatusDisplay), s);
                return;
            }
            else
            {
                lb_DutStatus.Text = s;
                lb_DutStatus.Invalidate();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Set and Get DutSN String.
        /// </summary>
        [Browsable(true), DefaultValue("N/A"), Description("Set and Get DUT's SerialNumber String."), Category("CustomProperties")]
        public string DutBarcode
        {
            get
            {
                return lb_DutSN.Text;
            }
            set
            {
                SetDutSnDisplay(value);
            }
        }

        /// <summary>
        /// Set and display dut SN(BarCode).
        /// </summary>
        /// <param name="sn"></param>
        private void SetDutSnDisplay(string sn)
        {
            if (lb_DutSN.InvokeRequired)
            {
                this.Invoke(new Action<string>(SetDutSnDisplay), sn);
            }
            else
            {
                lb_DutSN.Text = sn;
                lb_DutSN.Invalidate();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Set and Get DUT's Name.
        /// </summary>
        [Browsable(true), DefaultValue("DUT"), Description("Set and Get DUT's Name."), Category("CustomProperties")]
        public string DutName
        {
            get
            {
                return lb_DutSN.Text;
            }
            set
            {
                SetDutName(value);
            }
        }

        private void SetDutName(string s)
        {
            if (InvokeRequired)
            {
                this.Invoke(new StringHandler(SetDutName), s);
                return;
            }
            else
            {
                lb_DutSN.Text = s;
                lb_DutSN.Invalidate();
                Application.DoEvents();
            }
        }


        private void SetDutVisible(bool isVisible)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>((x) => this.Visible = x), isVisible);
                return;
            }
            else
            {
                this.Visible = isVisible;
                this.Invalidate();
                Application.DoEvents();
            }
        }

        #endregion//CustomProperties//

    }
}
