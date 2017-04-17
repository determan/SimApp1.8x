using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimApp
{
    public partial class frmSpecName : Form
    {
        string strFileName;
        string strDataDirpath;
        string sExt;

        public frmSpecName(string strDirpath,bool bSim)
        {
            InitializeComponent();
            strDataDirpath = strDirpath;
            this.AcceptButton = btNameOK;
            sExt = "bin";
            if (!bSim)
            {
                labProvideName.Text = labProvideName.Text.Replace("simulation", "data file");
                sExt = "csv";
                //strDataDirpath = strDataDirpath.Replace("SimData", "CsvData");
            }
        }

        private void NameOKClicked(object sender, EventArgs e)
        {
            //Validate name - not used, force to end in "bin"
            int iPos = tbName.Text.LastIndexOf('.');
            if (iPos >= 0)
            {
                if (iPos + 1 < tbName.Text.Length && tbName.Text.Substring(iPos + 1) != sExt)
                    strFileName = tbName.Text + "."+sExt;
                else
                    strFileName = tbName.Text.Substring(0, iPos) + "."+sExt;
            }
            else
                strFileName = tbName.Text + "."+sExt;
            strFileName = strFileName.Replace(' ', '_');
            if (File.Exists(strDataDirpath + "\\" + strFileName))
            {
                MessageBox.Show("Specified file name exists, specify a different name.", "File Name Exists", MessageBoxButtons.OK);
                return;
            }
            if (strFileName.Length <= 4)
            {
                MessageBox.Show("No file name provided.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
            if (labProvideName.Text.IndexOf("data file")>=0)
                labProvideName.Text = labProvideName.Text.Replace("data file", "simulation");
        }

        public string FileName() 
        {
            return strDataDirpath + "\\" + strFileName; 
        }

        public string Memo()
        {
            return tbMemo.Text;
        }

        private void SpecFileNameClosing(object sender, FormClosingEventArgs e)
        {
            if (labProvideName.Text.IndexOf("data file") >= 0)
                labProvideName.Text = labProvideName.Text.Replace("data file", "simulation");
        }
    }
}
