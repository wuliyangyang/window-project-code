
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One custom signal lamp control that can show the state of signal with assigned color.
    /// JIMMY.GONG DESIGNED 2018.01.24.
    /// </summary>
    [Description("One custom signal lamp control that can show the state of signal with assigned color.")]
    public class SignalLamp : UserControl
    {

        #region//Fields//
        #endregion//Fields......END//

        #region//Constructors//

        public SignalLamp() : base()
        {
            Init();
        }

        #endregion//Constructors......END//

        #region//Properties//

        /// <summary>
        /// Is switch state when double clicked.
        /// </summary>
        private bool _isDoubleClickSwitch = false;
        [Browsable(true), Category("CustomProperty"), Description("Is double click switch state.")]
        public bool IsDoubleClickSwitch
        {
            get { return _isDoubleClickSwitch; }
            set { _isDoubleClickSwitch = value; }
        }

        /// <summary>
        /// Get and set lamp on state.
        /// </summary>
        private bool _isLampOn = false;
        [Browsable(true), Category("CustomProperty"), Description("Is lamp on.")]
        public bool IsLampOn
        {
            get { return _isLampOn; }
            set { _isLampOn = value; this.Invalidate(); }
        }

        /// <summary>
        /// The center color when lamp is on.
        /// </summary>
        private Color _onCenterColor = Color.LightYellow;
        [Browsable(true), Category("CustomProperty"), Description("The center color when lamp is on.")]
        public Color OnCenterColor
        {
            get { return _onCenterColor; }
            set { _onCenterColor = value; }
        }

        /// <summary>
        /// The surround color when lamp is on.
        /// </summary>
        private Color _onSurroundColor = Color.LightYellow;
        [Browsable(true), Category("CustomProperty"), Description("The surround color when lamp is on.")]
        public Color OnSurroundColor
        {
            get { return _onSurroundColor; }
            set { _onSurroundColor = value; }
        }

        /// <summary>
        /// The center color when lamp is off.
        /// </summary>
        private Color _offCenterColor = Color.LightGray;
        [Browsable(true), Category("CustomProperty"), Description("The center color when lamp is off.")]
        public Color OffCenterColor
        {
            get { return _offCenterColor; }
            set { _offCenterColor = value; }
        }

        /// <summary>
        /// The surround color when lamp is off.
        /// </summary>
        private Color _offSurroundColor = Color.Gray;
        [Browsable(true), Category("CustomProperty"), Description("The surround color when lamp is off.")]
        public Color OffSurroundColor
        {
            get { return _offSurroundColor; }
            set { _offSurroundColor = value; }
        }

        /// <summary>
        /// The border line color of control.
        /// </summary>
        private Color _borderColor = Color.Transparent;
        [Browsable(true), Category("CustomProperty"), Description("The border line color of control.")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        /// <summary>
        /// Get and set the signal state.
        /// </summary>
        private SignalState _state = SignalState.Normal;
        [Browsable(true), Category("CustomProperty"), Description("The signal state.")]
        public SignalState CurrentState
        {
            get { return _state; }
            set { _state = value; SetCurrentOnColor(value); }
        }

        private void SetCurrentOnColor(SignalState state)
        {
            switch(state)
            {
                case SignalState.Abnormal:
                    OnCenterColor = StateAbnormalCenterColor;
                    OnSurroundColor = StateAbnormalSurroundColor;
                    break;
                case SignalState.Warnning:
                    OnCenterColor = StateWarnCenterColor;
                    OnSurroundColor = StateWarnSurroundColor;
                    break;
                case SignalState.Normal:
                    OnCenterColor = StateNormalCenterColor;
                    OnSurroundColor = StateNormalSurroundColor;
                    break;
                default:
                    break;
            }
        }

        #region//Normal State Color//

        /// <summary>
        /// The center color when signal state is normal.
        /// </summary>
        private Color _normalCenterColor = Color.LightGreen;
        [Browsable(true), Category("CustomProperty"), Description("The center color when signal state is normal.")]
        public Color StateNormalCenterColor
        {
            get { return _normalCenterColor; }
            set { _normalCenterColor = value; }
        }

        /// <summary>
        /// The surround color when signal is normal.
        /// </summary>
        private Color _normalSurroundColor = Color.Green;
        [Browsable(true), Category("CustomProperty"), Description("The surround color when signal is normal.")]
        public Color StateNormalSurroundColor
        {
            get { return _normalSurroundColor; }
            set { _normalSurroundColor = value; }
        }

        #endregion//Normal State Color......END//

        #region//Warning State Color//

        /// <summary>
        /// The center color when signal state is warning.
        /// </summary>
        private Color _warnStateCenterColor = Color.LightYellow;
        [Browsable(true), Category("CustomProperty"), Description("The center color when signal state is warning.")]
        public Color StateWarnCenterColor
        {
            get { return _warnStateCenterColor; }
            set { _warnStateCenterColor = value; }
        }

        /// <summary>
        /// The surround color when signal state is warning.
        /// </summary>
        private Color _warnSurroundColor = Color.Yellow;
        [Browsable(true), Category("CustomProperty"), Description("The surround color when signal state is warning.")]
        public Color StateWarnSurroundColor
        {
            get { return _warnSurroundColor; }
            set { _warnSurroundColor = value; }
        }

        #endregion//Warning State Color......END//

        #region//Abnormal State Color//

        /// <summary>
        /// The center color when signal state is abnormal.
        /// </summary>
        private Color _abnormalCenterColor = Color.Red;
        [Browsable(true), Category("CustomProperty"), Description("The center color when signal state is abnormal.")]
        public Color StateAbnormalCenterColor
        {
            get { return _abnormalCenterColor; }
            set { _abnormalCenterColor = value; }
        }

        /// <summary>
        /// The surround color when signal state is abnormal.
        /// </summary>
        private Color _abnormalSurroundColor = Color.DarkRed;
        [Browsable(true), Category("CustomProperty"), Description("The surround color when signal state is abnormal.")]
        public Color StateAbnormalSurroundColor
        {
            get { return _abnormalSurroundColor; }
            set { _abnormalSurroundColor = value; }
        }

        #endregion//Abnormal State Color......END//

        #endregion//Properties......END//

        #region//Public Methods//

        public void RefreshDisplay()
        {
            this.Invalidate();
        }

        #endregion//Public Methods......END//

        #region//Private Methods//

        private void Init()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "SignalLamp";
            this.Size = new System.Drawing.Size(100, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        #endregion//Private Methods//

        #region//Event Handlers//

        private void OnPaint(object sender, PaintEventArgs e)
        {
            int ledSize;
            Color centerColor;
            Color surroundColor;

            // 获取控件的大小，设置圆直径
            if (this.Width > this.Height)
            {
                ledSize = this.Height - 4;
            }
            else
            {
                ledSize = this.Width - 4;
            }

            // 设置GDI+模式为精细模式
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // 获取要绘制的颜色
            if (IsLampOn)
            {

                centerColor   = OnCenterColor;
                surroundColor = OnSurroundColor;

            }
            else
            {
                centerColor   = OffCenterColor;
                surroundColor = OffSurroundColor;
            }

            // 创建一个变色圆形的区域
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(2, 2, ledSize, ledSize);

            PathGradientBrush pthGrBrush = new PathGradientBrush(path);

            // 设置中间颜色
            pthGrBrush.CenterColor = centerColor;

            // 设置边缘颜色
            Color[] colors = { surroundColor };
            pthGrBrush.SurroundColors = colors;

            // 绘制变色圆形
            e.Graphics.FillEllipse(pthGrBrush, 2, 2, ledSize, ledSize);

            // 绘制圆形边框
            Pen p = new Pen(new SolidBrush(BorderColor), 2);
            e.Graphics.DrawEllipse(p, 2, 2, ledSize, ledSize);

            // 控件区域为圆形
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, (ledSize + 4), (ledSize + 4));
            this.Region = new Region(gp);//这句就是设置圆形的规格区域的 
        }

        #endregion//Events Handlers......END//

    }

    /// <summary>
    /// All kinds of signal state.
    /// </summary>
    [Description("All kinds of signal state.")]
    public enum SignalState
    {
        [Description("Normal State")]
        Normal,
        [Description("Warnning State")]
        Warnning,
        [Description("Abnormal State")]
        Abnormal,
    }
}
