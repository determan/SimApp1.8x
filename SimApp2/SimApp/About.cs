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
    public partial class frmAboutSUPO : Form
    {
        public frmAboutSUPO(string sName)
        {
            InitializeComponent();
            if (sName == "SupoModel")
            {
                labTitle.Text = "Generic SYSTEM MODEL FOR A FISSILE SOLUTION SYSTEM (SUPO EXAMPLE)";
                labVersion.Text = "Version: 1.8.0.4";
            }
            if (sName == "AD125")
            {
                labTitle.Text = "SYSTEM MODEL FOR GENERIC ACCELERATOR-DRIVEN FISSILE SOLUTION SYSTEM (AD125)";
                labVersion.Text = "Version: 1.8.0.4";
            }
            if (sName == "AD135")
            {
                labTitle.Text = "SYSTEM MODEL FOR GENERIC ACCELERATOR-DRIVEN FISSILE SOLUTION SYSTEM (AD135)";
                labVersion.Text = "Version: 1.8.0.2";
            }
            if (sName == "GODIVAQ")
            {
                labTitle.Text = "SYSTEM MODEL FOR GENERIC ACCELERATOR-DRIVEN FISSILE SOLUTION SYSTEM (AD135)";
                labVersion.Text = "Version: 1.8.0.2";
            }
        }

        private void labCR_Click(object sender, EventArgs e)
        {

        }
    }
}
