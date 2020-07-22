using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class TitleBar : UserControl, ITitleBar
    {
        public TitleBar()
        {
            InitializeComponent();
        }

        #region//Properties//

        /// <summary>
        /// Set or get the text property of title label.
        /// </summary>
        
        public string TitleString
        {
            get
            {
                return lb_Title.Text;
            }
            set
            {
                SetTitleString(value);
            }
        }
        private void SetTitleString(string str)
        {
            if(this.lb_Title.InvokeRequired)
            {
                this.lb_Title.Invoke(new Action<string>(SetTitleString), str);
            }
            else
            {
                lb_Title.Text = str;
            }
        }

        /// <summary>
        /// Set or get the foreColor property of title label.
        /// </summary>
        public Color TitleColor
        {
            get
            {
                return this.lb_Title.ForeColor;
            }
            set
            {
                SetTitleColor(value);
            }
        }
        private void SetTitleColor(Color c)
        {
            if (this.lb_Title.InvokeRequired)
            {
                this.lb_Title.Invoke(new Action<Color>(SetTitleColor), c);
            }
            else
            {
                lb_Title.ForeColor = c;
            }
        }

        /// <summary>
        /// Set or get the visible property of title label.
        /// </summary>
        public bool IsShowTitle
        {
            get { return this.lb_Title.Visible;}
            set { SetTitleVisible(value); }
        }
        private void SetTitleVisible(bool isVisible)
        {
            if(this.lb_Title.InvokeRequired)
            {
                this.lb_Title.Invoke(new Action<bool>(SetTitleVisible), isVisible);
            }
            else
            {
                this.lb_Title.Visible = isVisible;
            }
        }

        /// <summary>
        /// Set or get the text property of version label.
        /// </summary>
        public string VersionString 
        {
            get
            {
                return this.lb_Version.Text;
            }
            set
            {
                SetVersionString(value);
            }
        }
        private void SetVersionString(string str)
        {
            if (this.lb_Version.InvokeRequired)
            {
                this.lb_Version.Invoke(new Action<string>(SetVersionString), str);
            }
            else
            {
                this.lb_Version.Text = str;
            }
        }

        /// <summary>
        /// Set or get the foreColor property of version label.
        /// </summary>
        public Color VersionColor
        {
            get
            {
                return this.lb_Version.ForeColor;
            }
            set
            {
                SetVersionColor(value);
            }
        }
        private void SetVersionColor(Color c)
        {
            if(this.lb_Version.InvokeRequired)
            {
                this.lb_Version.Invoke(new Action<Color>(SetVersionColor), c);
            }
            else
            {
                this.lb_Version.ForeColor = c;
            }
        }

        /// <summary>
        /// Set or get the visible property of version label.
        /// </summary>
        public bool IsShowVersion
        {
            get { return this.lb_Version.Visible; }
            set { SetVersionVisible(value); }
        }
        private void SetVersionVisible(bool isVisible)
        {
            if (this.lb_Version.InvokeRequired)
            {
                this.lb_Version.Invoke(new Action<bool>(SetVersionVisible), isVisible);
            }
            else
            {
                this.lb_Version.Visible = isVisible;
            }
        }

        /// <summary>
        /// Set or get the text property of operator role label.
        /// </summary>
        public string OperatorRole 
        {
            get{ return lb_OperatorRole.Text; }
            set{ SetOperatorString(value); }
        }
        private void SetOperatorString(string str)
        {
            if (this.lb_OperatorRole.InvokeRequired)
            {
                this.lb_OperatorRole.Invoke(new Action<string>(SetOperatorString), str);
            }
            else
            {
                this.lb_OperatorRole.Text = str;
            }
        }

        /// <summary>
        /// Set or get the fore color property of operator role label.
        /// </summary>
        public Color OperatorRoleColor
        {
            get{ return this.lb_OperatorRole.ForeColor; }
            set{ SetOperatorRoleColor(value); }
        }
        private void SetOperatorRoleColor(Color c)
        {
            if(this.lb_OperatorRole.InvokeRequired)
            {
                this.lb_OperatorRole.Invoke(new Action<Color>(SetOperatorRoleColor), c);
            }
            else
            {
                this.lb_OperatorRole.ForeColor = c;
            }
        }

        /// <summary>
        /// Set or get the operator role label visible.
        /// </summary>
        public bool IsShowOperatorRole
        {
            get { return this.lb_OperatorRole.Visible; }
            set { SetOperatorRoleVisible(value); }
        }
        private void SetOperatorRoleVisible(bool isVisible)
        {
            if(this.lb_OperatorRole.InvokeRequired)
            {
                Invoke(new Action<bool>(SetOperatorRoleVisible), isVisible);
            }
            else
            {
                this.lb_OperatorRole.Visible = isVisible;
            }
        }

        /// <summary>
        /// Set or get the text property of operator name label.
        /// </summary>
        public string OperatorName
        {
            get { return lb_OperatorName.Text; }
            set { SetOperatorNameString(value); }
        }
        private void SetOperatorNameString(string str)
        {
            if (this.lb_OperatorName.InvokeRequired)
            {
                this.lb_OperatorName.Invoke(new Action<string>(SetOperatorNameString), str);
            }
            else
            {
                this.lb_OperatorName.Text = str;
            }
        }

        /// <summary>
        /// Set or get the fore color property of operator name label.
        /// </summary>
        public Color OperatorNameColor
        {
            get { return this.lb_OperatorName.ForeColor; }
            set { SetOperatorNameColor(value); }
        }
        private void SetOperatorNameColor(Color c)
        {
            if (this.lb_OperatorName.InvokeRequired)
            {
                this.lb_OperatorName.Invoke(new Action<Color>(SetOperatorNameColor), c);
            }
            else
            {
                this.lb_OperatorName.ForeColor = c;
            }
        }

        /// <summary>
        /// Set or get the operator name label visible.
        /// </summary>
        public bool IsShowOperatorName
        {
            get { return this.lb_OperatorName.Visible; }
            set { SetOperatorNameVisible(value); }
        }
        private void SetOperatorNameVisible(bool isVisible)
        {
            if (this.lb_OperatorName.InvokeRequired)
            {
                Invoke(new Action<bool>(SetOperatorNameVisible), isVisible);
            }
            else
            {
                this.lb_OperatorName.Visible = isVisible;
            }
        }


        #endregion//Properties...End//

        #region//Public Methods//

        #endregion//Public Methods...End//
    }
}
