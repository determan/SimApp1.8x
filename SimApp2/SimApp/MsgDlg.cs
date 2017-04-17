using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimApp
{
    public partial class frmMsgDlg : Form
    {
        public frmMsgDlg(string sMsg)
        {
            InitializeComponent();
            tbMsg.Text = sMsg;
        }

        private void ClickMsgOK(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ClickMsgCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
