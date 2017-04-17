using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelConverter
{
    public partial class FormICs : Form
    {
        private string sDesireFileName;
        //private bool bSelChng;
        private VariableInfo[] via;
        private VariableInfo[] viaExtra;
        private string[] saTy;
        private int iIndex;
        private int iXdex;
        private int iNumTypes;
        private bool bExtra;

        public FormICs(string sDlgName, string sDFileName, VariableInfo[] viaConsts, string[] saTypes, int iNT)
        {
            InitializeComponent();
            iIndex = -1;
            Text = sDlgName;
            sDesireFileName = sDFileName;
            via = viaConsts;
            saTy = saTypes;
            cbConstants.Items.Clear();
            foreach (VariableInfo vi in via)
                cbConstants.Items.Add(vi.sVarName);
            iNumTypes = iNT;
            cbCTypes.Items.Clear();
            foreach (string sType in saTypes)
            {
                cbCTypes.Items.Add(sType);
                if (cbCTypes.Items.Count == iNumTypes)
                    break;
            }
            bExtra = false;
        }

        public void AddStateVars(VariableInfo[] viaEx)
        {
            viaExtra = viaEx;
            foreach (VariableInfo vi in viaExtra)
                cbConstants.Items.Add(vi.sVarName);
            bExtra = true;
        }

        public int NumTypes() { return iNumTypes; }

        private void ConstantSelIndChanged(object sender, EventArgs e)
        {
            string[] saLines = new string[5];
            string sLine, sTmp;
            string sItem = cbConstants.SelectedItem.ToString();
            int iPos, iPos2, iTL, iMaxLines = 5;
            iIndex = cbConstants.SelectedIndex;
            if (iIndex >= 0)
            {
                cbCTypes.Enabled = true;
                tbName.Enabled = true;
                tbUnits.Enabled = true;
                chbStability.Enabled = true;
                if (bExtra)
                {
                    if (iIndex < via.Length)
                    {
                        iXdex = -1;
                        cbCTypes.Text = via[iIndex].sType;
                        tbName.Text = via[iIndex].sEngName;
                        tbUnits.Text = via[iIndex].sUnits;
                        chbStability.Checked = via[iIndex].bStabItem;
                    }
                    else
                    {
                        iXdex = iIndex - via.Length;
                        cbCTypes.Text = viaExtra[iXdex].sType;
                        tbName.Text = viaExtra[iXdex].sEngName;
                        tbUnits.Text = viaExtra[iXdex].sUnits;
                        chbStability.Checked = viaExtra[iXdex].bStabItem;
                    }
                    if (cbCTypes.Text == "")
                    {
                        cbCTypes.SelectedIndex = 0;
                        cbCTypes.Text = "User";
                    }
                    StreamReader strmR = new StreamReader(sDesireFileName);
                    saLines[0] = saLines[1] = saLines[2] = saLines[3] = saLines[4] = "\n";
                    while (!strmR.EndOfStream)
                    {
                        sLine = strmR.ReadLine();
                        sLine = sLine.Trim();
                        if (sLine.Length == 0)
                            continue;
                        iPos2 = -1;
                        if (sLine[0] != '-' && sLine[1] != '-' && (((iPos = sLine.IndexOf(sItem)) == 0) || ((iPos2 = sLine.IndexOf("d/dt")) == 0)))
                        {
                            if (iPos2 == 0)
                            {
                                for (int i = iPos2 + 4; i < sLine.Length; i++)
                                {
                                    if (sLine[i] != ' ' && sLine[i] != '\t')
                                    {
                                        iPos = iPos2 + i;
                                        break;
                                    }
                                }
                                iPos2 = sLine.IndexOf('=');
                                if(iPos2>0 && iPos2>iPos)
                                {
                                    sTmp = sLine.Substring(iPos, iPos2 - iPos).Trim();
                                    if (sTmp != sItem)
                                        continue;
                                }
                            }
                            for (int i = iPos + sItem.Length; i < sLine.Length; i++)
                            {
                                if (sLine[i] == ' ' || sLine[i] == '\t')
                                {
                                    if (sLine.Substring(iPos, i - iPos) != sItem)
                                        break;
                                    continue;
                                }
                                if (sLine[i] == '=')
                                {
                                    if (sLine.Substring(iPos, i - iPos) != sItem)
                                        break;
                                    rtbContext.Text = "";
                                    foreach (string strLine in saLines)
                                        rtbContext.AppendText(strLine);
                                    iTL = rtbContext.TextLength;
                                    rtbContext.AppendText(sLine + "\n");
                                    rtbContext.Select(iTL, sLine.Length);
                                    rtbContext.SelectionColor = Color.Red;
                                    for (int j = 0; j < 5; j++)
                                    {
                                        rtbContext.AppendText(strmR.ReadLine() + "\n");
                                    }
                                    strmR.Close();
                                    return;
                                }
                            }//end for
                        }
                        for (int k = iMaxLines; k > 1; k--)
                            saLines[iMaxLines - k] = saLines[iMaxLines - k + 1];
                        saLines[4] = sLine + "\n";
                    }//end while
                    strmR.Close();
                }//if bExtra
                else
                {
                    iXdex = -1;
                    cbCTypes.Text = via[iIndex].sType;
                    tbName.Text = via[iIndex].sEngName;
                    tbUnits.Text = via[iIndex].sUnits;
                    chbStability.Checked = via[iIndex].bStabItem;
                    if (cbCTypes.Text == "")
                    {
                        cbCTypes.SelectedIndex = 0;
                        cbCTypes.Text = "Constant";
                    }
                    StreamReader strmR = new StreamReader(sDesireFileName);
                    saLines[0] = saLines[1] = saLines[2] = saLines[3] = saLines[4] = "\n";
                    while (!strmR.EndOfStream)
                    {
                        sLine = strmR.ReadLine();
                        sLine = sLine.Trim();
                        if (sLine.Length == 0)
                            continue;
                        if (sLine[0] != '-' && sLine[1] != '-' && (iPos = sLine.IndexOf(sItem)) == 0)
                        {

                            for (int i = iPos + sItem.Length; i < sLine.Length; i++)
                            {
                                if (sLine[i] == ' ' || sLine[i] == '\t')
                                {
                                    if (sLine.Substring(iPos, i - iPos) != sItem)
                                        break;
                                    continue;
                                }
                                if (sLine[i] == '=')
                                {
                                    if (sLine.Substring(iPos, i - iPos) != sItem)
                                        break;
                                    rtbContext.Text = "";
                                    foreach (string strLine in saLines)
                                        rtbContext.AppendText(strLine);
                                    iTL = rtbContext.TextLength;
                                    rtbContext.AppendText(sLine + "\n");
                                    rtbContext.Select(iTL, sLine.Length);
                                    rtbContext.SelectionColor = Color.Red;
                                    for (int j = 0; j < 5; j++)
                                    {
                                        rtbContext.AppendText(strmR.ReadLine() + "\n");
                                    }
                                    strmR.Close();
                                    return;
                                }
                            }//end for
                        }
                        for (int k = iMaxLines; k > 1; k--)
                            saLines[iMaxLines - k] = saLines[iMaxLines - k + 1];
                        saLines[4] = sLine + "\n";
                    }//end while
                    strmR.Close();
                }//else bExtra
            }//if iIndex
        }

        private void TextChangedType(object sender, EventArgs e)
        {
 //           if (!bSelChng)
                btAddType.Enabled = true;
        }

        private void SelindChangedType(object sender, EventArgs e)
        {
            btAddType.Enabled = false;
            int ii = cbCTypes.SelectedIndex;
            if (iIndex >= 0 && cbCTypes.SelectedItem != null)
            {
                if (iXdex<0)
                    via[iIndex].sType = cbCTypes.SelectedItem.ToString();
                else
                    viaExtra[iXdex].sType = cbCTypes.SelectedItem.ToString();
            }
//            bSelChng = true;
        }

        private void ClickAddType(object sender, EventArgs e)
        {
            btAddType.Enabled = false;
            if (cbCTypes.Items.IndexOf(cbCTypes.Text) < 0 && iNumTypes < saTy.Length)
            {
                cbCTypes.Items.Add(cbCTypes.Text);
                saTy[iNumTypes++] = cbCTypes.Text;
            }
        }

        private void TextChangedName(object sender, EventArgs e)
        {
            if (iIndex >= 0)
            {
                if (iXdex < 0)
                    via[iIndex].sEngName = tbName.Text;
                else
                    viaExtra[iXdex].sEngName = tbName.Text;
            }
        }

        private void UnitsTextChanged(object sender, EventArgs e)
        {
            if (iIndex >= 0)
            {
                if (iXdex < 0)
                    via[iIndex].sUnits = tbUnits.Text;
                else
                    viaExtra[iXdex].sUnits = tbUnits.Text;
            }
        }

        private void StabCheckChanged(object sender, EventArgs e)
        {
            if (iIndex >= 0)
            {
                if (iXdex < 0)
                    via[iIndex].bStabItem = chbStability.Checked;
                else
                    viaExtra[iXdex].bStabItem = chbStability.Checked;
            }
        }
    }
}
