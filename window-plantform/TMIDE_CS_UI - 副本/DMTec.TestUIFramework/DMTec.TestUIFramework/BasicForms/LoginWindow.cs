using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class LoginWindow : Form
    {

        const string PSWD_OP = "123";
        const string PSWD_AD = "admin88";
        const string PSWD_EN = "engineer99";

        public int OperatorLevel{get;set;}

        

        public LoginWindow()
        {
            InitializeComponent();
            OperatorLevel = (int)DataModel.OperatorLevels.Operator;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
//             cbb_UsrName.SelectedIndex = 0;
//             cbb_UsrName.DataSource = Enum.GetValues(typeof(OperatorLevels))
//                 .Cast<Enum>().Select(value => new
//                 {
//                     (Attribute.GetCustomAttribute(value.GetType()
//                     .GetField(value.ToString())
//                     ,typeof(DescriptionAttribute)) as DescriptionAttribute)
//                     .Description
//                     , value
//                 })
//                 .OrderBy(item ==> item.value).ToList();

            cbb_UsrName.DataSource = System.Enum.GetNames(typeof(DataModel.OperatorLevels));
            //cbb_UsrName.DisplayMember = "Description";
            //cbb_UsrName.ValueMember = "value";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int index = cbb_UsrName.SelectedIndex;

            if (index == 0)
            {
                if (PSWD_OP == tb_Pswd.Text)
                {
                    OperatorLevel = (int)DataModel.OperatorLevels.Operator;
                }
                else
                {
                    MessageBox.Show("Password of Operator is Error, Please retry!", "Password Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (index == 1)
            {
                if(PSWD_AD == tb_Pswd.Text)
                {
                    OperatorLevel = (int)DataModel.OperatorLevels.Administrator;
                }
                else
                {
                    MessageBox.Show("Password of Administrator is Error, Please retry!", "Password Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (index == 2)
            {
                if(PSWD_EN == tb_Pswd.Text)
                {
                    OperatorLevel = (int)DataModel.OperatorLevels.Engineer;
                }
                else
                {
                    MessageBox.Show("Password of Engineer is Error, Please retry!", "Password Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                return;
            }

            tb_Pswd.Text = "";

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }


    }
}
