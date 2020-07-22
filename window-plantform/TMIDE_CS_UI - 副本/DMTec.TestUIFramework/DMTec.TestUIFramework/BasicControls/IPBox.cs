using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class IPBox : UserControl
    {
        private string _ip = "192.168.0.1";

        public IPBox()
        {
            InitializeComponent();
        }


        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                if("" != value && null != value)
                {
                    try
                    {
                        string[] pieces = new string[4];
                        pieces = value.ToString().Split(".".ToCharArray(), 4);
                        asftb1.Text = pieces[0];
                        asftb2.Text = pieces[1];
                        asftb3.Text = pieces[2];
                        asftb4.Text = pieces[3];

                    }
                    catch
                    {
                        asftb1.Text = "";
                        asftb2.Text = "";
                        asftb3.Text = "";
                        asftb4.Text = "";
                    }
                }
                else
                {
                    asftb1.Text = "";
                    asftb2.Text = "";
                    asftb3.Text = "";
                    asftb4.Text = "";
                }
            }
        }
    }
}
