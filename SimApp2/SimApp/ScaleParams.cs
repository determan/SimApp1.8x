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
using System.Threading;

namespace SimApp
{

    public partial class ScaleVals : Form
    {
        public enum ScaleType {None=0, BodeAmp, BodePhase, StabMarg, Any};

        private double[] daValues;
        private double[] daSaved;
        int iOffset;

        public ScaleVals(double[] daScVals, ScaleType eType)
        {
            try
            {
                //DataGridViewRow row;
                InitializeComponent();
                iOffset = 0;
                int iRow;
                daValues = daScVals;
                daSaved = new double[daScVals.Length];
                daScVals.CopyTo(daSaved,0);

                if (eType > ScaleType.None)
                {
                    rbBodeAmp.Enabled = true;
                    rbBodePhase.Enabled = true;
                    rbStabMargin.Enabled = true;
                    rbLinX.Enabled = true;
                    rbLogX.Enabled = true;
                    switch (eType)
                    {
                        case ScaleType.BodeAmp:
                            iOffset = 0;
                            break;
                        case ScaleType.BodePhase:
                            iOffset = 13;
                            break;
                        case ScaleType.StabMarg:
                            iOffset = 26;
                            break;
                        default: //(ScaleType.Any)
                            iOffset = 0;
                            break;
                    }
                }
                else
                {
                    rbBodeAmp.Enabled = false;
                    rbBodePhase.Enabled = false;
                    rbStabMargin.Enabled = false;
                    rbLinX.Enabled = false;
                    rbLogX.Enabled = false;
                }
                //sName = sModName;
                // row = (DataGridViewRow)dgScaleValues.Rows[0].Clone();
                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "Y Axis Mininimum";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "Y Axis Maximum";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset+1]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "Y Axis Label Interval";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 2]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "Y Axis Major Grid";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 3]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "X Axis Mininimum";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 4]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "X Axis Maximum";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 5]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "X Axis Label Interval";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 6]);

                iRow = dgScaleValues.Rows.Add();
                dgScaleValues.Rows[iRow].Cells[0].Value = "X Axis Major Grid";
                dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 7]);

                if (eType > ScaleType.None)
                {
                    iRow = dgScaleValues.Rows.Add();
                    dgScaleValues.Rows[iRow].Cells[0].Value = "X Range Start";
                    dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 8]);

                    iRow = dgScaleValues.Rows.Add();
                    dgScaleValues.Rows[iRow].Cells[0].Value = "X Range End";
                    dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 9]);

                    iRow = dgScaleValues.Rows.Add();
                    dgScaleValues.Rows[iRow].Cells[0].Value = "X Range Step";
                    dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daScVals[iOffset + 10]);

                    if (Clean(daValues[iOffset + 11]) > 0.5)
                    {
                        rbLogX.Checked = true;
                    }
                    else
                    {
                        rbLinX.Checked = true;
                    }
                    switch (eType)
                    {
                        case ScaleType.BodeAmp:
                            rbBodeAmp.Checked = true;
                            break;
                        case ScaleType.BodePhase:
                            rbBodePhase.Checked = true;
                            break;
                        case ScaleType.StabMarg:
                            rbStabMargin.Checked = true;
                            break;
                        default: //(ScaleType.Any)
                            rbBodeAmp.Checked = true;
                            break;
                    }
                }

                foreach (DataGridViewColumn dgvc in dgScaleValues.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.AcceptButton = btOK;
                this.CancelButton = btCancel;
            }
            catch (Exception e)
            {
            }
        }

        public double Clean(double dVal)
        {
            double dTmp, dTmp2, dDiff = 1.0F, dOut;
            dOut = dVal;
            int iSign, iMult = 1;
            if (dVal < 0)
                iSign = -1;
            else
                iSign = 1;
            dTmp = Math.Abs(dVal);
            if (dTmp < 1)
            {
                dTmp2 = dTmp;
                dDiff = 1.0F;
                while(dDiff/dTmp > 0.0001)
                {
                    iMult *= 10;
                    dTmp2 = dTmp*iMult;
                    dDiff = dTmp2 - (int)(dTmp2);
                }
                dOut = (double)((int)(dTmp2)) / iMult * iSign;
            }
            return dOut;
        }

        public void SaveValues()
        {
            int j;
            for (int i = 0; i < dgScaleValues.Rows.Count; i++)
            {
                j = i + iOffset;
                daValues[j] = Clean(Double.Parse(dgScaleValues.Rows[i].Cells[1].Value.ToString()));
            }
            if (dgScaleValues.Rows.Count > 8)
                daValues[iOffset + 11] = (rbLogX.Checked) ? 1.0 : 0.0;
        }

        private void OKClick(object sender, EventArgs e)
        {
            SaveValues();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            daSaved.CopyTo(daValues,0);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ClearAllClick(object sender, EventArgs e)
        {
            int iCnt = dgScaleValues.Rows.Count;
            for (int i = 0; i < iCnt; i++)
            {
                dgScaleValues.Rows[i].Cells[1].Value = (object)"";
            }

        }

        //private void SetExtraRows()
        //{
        //    int i = dgScaleValues.Rows.Count, iRow, k;
        //    string sType = "";
        //    string[] saRowLabels = {"X Start", "X End", "X Step", "X Log", "Y Log"};
        //    SaveValues();
        //    //while (i > 8)
        //    //    dgScaleValues.Rows.RemoveAt(--i);
        //    switch (iStart)
        //    {
        //        case 8:     sType = "Bode Amp ";    break;
        //        case 21:    sType = "Bode Phase ";  break;
        //        case 34:    sType = "Stab Marg ";   break;
        //        default: break;
        //    }
        //    for (int j = 0; j < 3; j++)
        //    {
        //        k = iStart + j;
        //        //iRow = dgScaleValues.Rows.Add();
        //        if (k < daValues.Length)
        //        {
        //            dgScaleValues.Rows[iRow].Cells[0].Value = sType + saRowLabels[j];
        //            dgScaleValues.Rows[iRow].Cells[1].Value = Clean(daValues[k]);
        //        }
        //    }
        //    if (Clean(daValues[iStart + 3]) > 0.5)
        //    {
        //        rbLogX.Checked = true;
        //    }
        //    else
        //    {
        //        rbLinX.Checked = true;
        //    }
        //}

        private void ResetValues(int iVal)
        {
            int k;
            SaveValues();
            iOffset = iVal;
            for (int i = 0; i < dgScaleValues.Rows.Count; i++)
            {
                k = i + iOffset;
                dgScaleValues.Rows[i].Cells[1].Value = Clean(daValues[k]);
            }
            if (Clean(daValues[iOffset + 11]) > 0.5)
            {
                rbLogX.Checked = true;
            }
            else
            {
                rbLinX.Checked = true;
            }

        }

        private void CheckChangeBodeAmp(object sender, EventArgs e)
        {
            if (rbBodeAmp.Checked)
            {
                ResetValues(0);
            }
        }

        private void CheckChangeBodePhase(object sender, EventArgs e)
        {
            if (rbBodePhase.Checked)
            {
                ResetValues(13);
            }
        }

        private void CheckChangeStabMarg(object sender, EventArgs e)
        {
            if(rbStabMargin.Checked)
            {
                ResetValues(26);
            }
        }

        private void CheckChangeLinX(object sender, EventArgs e)
        {

        }

        private void CheckChangeLogX(object sender, EventArgs e)
        {

        }
    }
}
