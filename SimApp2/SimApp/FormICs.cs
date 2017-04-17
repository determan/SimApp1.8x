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
    public partial class frmICOptions : Form
    {
        private double[] daIcVals;

        public frmICOptions(double[] daVals)
        {
            string str;
            int iPos;
            InitializeComponent();
            daIcVals = daVals;
            cbIcFiles.Items.Clear();
            if (Directory.Exists("ICs"))
            {
                foreach(string sFile in Directory.EnumerateFiles("ICs"))
                    if ((iPos = sFile.IndexOf(".icv")) >= 0)
                    {
                        str = sFile.Substring(0, iPos).Replace('_', ' ');
                        iPos = str.LastIndexOf('\\');
                        if (iPos >= 0)
                            str = str.Substring(iPos + 1);
                        cbIcFiles.Items.Add(str);
                    }
            }
            btOK_IC.Enabled = false;
            btSaveICs.Enabled = false;

        }

        private void ICSetNameTextChanged(object sender, EventArgs e)
        {
            btSaveICs.Enabled = true;
        }

        private void SaveICsClick(object sender, EventArgs e)
        {
            string strFile = tbNameIcSet.Text;
            strFile = "ICs\\"+strFile.Replace(' ', '_')+".icv";
            if(!Directory.Exists("ICs"))
                Directory.CreateDirectory("ICs");
            else
            {
                if(File.Exists(strFile))
                {
                    string str = string.Format("File {0} already in use. Overwrite?", strFile);
                    if(MessageBox.Show(str,"File Exists",MessageBoxButtons.YesNo)==DialogResult.No)
                        return;
                }
            }
            StreamWriter sw = new StreamWriter(strFile,false);
            foreach (double dV in daIcVals)
                sw.WriteLine(dV);
            sw.Close();
            btSaveICs.Enabled = false;
        }

        private void SetICClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void IcSelChangeComm(object sender, EventArgs e)
        {
            int i = 0;
            string sFile;
            if (cbIcFiles.SelectedItem != null)
            {
                sFile = cbIcFiles.SelectedItem.ToString();
                sFile = "ICs\\" + sFile.Replace(' ', '_') + ".icv";
                if (File.Exists(sFile))
                {
                    StreamReader sr = new StreamReader(sFile);
                    while (!sr.EndOfStream)
                        daIcVals[i++] = Double.Parse(sr.ReadLine());
                    sr.Close();
                    btOK_IC.Enabled = true;
                }
            }
        }
    }
}
