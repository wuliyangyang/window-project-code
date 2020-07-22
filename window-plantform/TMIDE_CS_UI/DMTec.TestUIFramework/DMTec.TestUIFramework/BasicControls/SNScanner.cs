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
    public partial class SNScanner : UserControl
    {

        #region//Members//



        #endregion//Members___END//

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot"></param>
        public SNScanner(int slot)
        {
            InitializeComponent();
            InitUUTSNInputBar(slot);
        }

        private void InitUUTSNInputBar(int slots)
        {
            //
        }


    }
}
