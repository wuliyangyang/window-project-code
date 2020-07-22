using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DMTec.TestUIFramework.DataModel;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class ChamberConfigPanel : UserControl
    {
        private ChamberConfig myConfig = new ChamberConfig();


        public ChamberConfigPanel()
        {
            InitializeComponent();
            BindConfigSource();
        }

        /// <summary>
        /// 
        /// </summary>
        public ChamberConfig Config
        {
            get
            {
                return myConfig;
            }
            set
            {
                this.myConfig = value;
            }
        }


        private void BindConfigSource()
        {
            //cbb_ChamberType.DataSource = System.Enum.GetNames(typeof(ChamberType));
            //cbb_ChamberType.DataSource = GetDsTableFromEnum(Enum e);
            //cbb_ChamberType.DisplayMember = "Name";
            //cbb_ChamberType.DataValueField = "Value";
            //cbb_ChamberType.DataBind();  

            //cbb_ChamberType.SelectedIndex = this.cbb_ChamberType.FindString(myConfig.ChambersType.ToString());
            
            //ck_IsChamberEnable.DataBindings.Add("Checked", myConfig.IsEnable, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            //tb_ChamberID.DataBindings.Add("Text", myConfig.ChamberID, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            //cbb_ChamberType.DataBindings.Add("Text", myConfig.ChambersType, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
            //tb_ChamberType.DataBindings.Add("Text", myConfig.ChamberID, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            
            this.tb_ChamberID.DataBindings.Add("Text", "myConfig", "ChamberID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public class EnumTextByDescription
        {
            public static string GetEnumDesc(Enum e)
            {
                FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.
                    GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
                return e.ToString();
            }
        }

        public ChamberConfig GetChamberConfig()
        {
            myConfig.ChamberID = tb_ChamberID.Text;
            //myConfig.ChambersType = (ChamberType)tb_ChamberType.Text;
            

            return myConfig;
        }

        public void SetChamberConfig(ChamberConfig cfg)
        {
            myConfig = cfg;
            ck_IsChamberEnable.Checked = myConfig.IsEnable;
            ck_IsOriginalOutput.Checked = myConfig.IsOriginalMsgOutput;

        }
    }
}
