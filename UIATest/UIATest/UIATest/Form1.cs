using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using WindowsInput;

namespace UIATest
{
    struct IndexValuePair
    {
        public string sName;
        public int iTCNum;
        public int iIndex;
        public float fValueC;
        public float fValueD;
        public float fResult;
    }

    public partial class Form1 : Form
    {

        public delegate void AddResult(string strMess);
        public AddResult delAddResult;
        public delegate void SetFocus();
        public SetFocus delSetFocus;

        private TreeWalker tw;
        private IndexValuePair[] ivpaPairs;
        private int iNumTCs;
        private int iNumItems;
        private int iSz;
        private StreamWriter strmW;
        private bool[] bInclude;
        private Thread thrTask;

        public void Execute(AutomationElement aeBase)
        {
            int ii = 0;
            //Stopwatch swTimer;
            //swTimer = new Stopwatch();
            try
            {
                AutomationElement ae, aeTemp, aeTemp2;
                ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
                ii = 1;
                while (ae != null)
                {
                ii = 2;
                    //swTimer.Start();
                    if ((ae.Current.AutomationId == "pnlSimParms"))
                    {
                        aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                        ii = 3;
                        while (aeTemp != null)
                        {
                            if ((aeTemp.Current.AutomationId == "btnExecute"))
                            {
                                ii = 4;
                                (aeTemp.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                                Thread.Sleep(2000);
                                ii = 5;
                                Condition pc = new PropertyCondition(AutomationElement.AutomationIdProperty, "frmMsgDlg", PropertyConditionFlags.None);
                                AutomationElement ae2 = aeBase.FindFirst(TreeScope.Element | TreeScope.Children, pc);
                                if (ae2 != null)
                                {
                                    ii = 6;
                                    aeTemp2 = TreeWalker.ControlViewWalker.GetFirstChild(ae2);
                                    while (aeTemp2 != null)
                                    {
                                        if ((aeTemp2.Current.AutomationId == "btMsgOK"))
                                        {
                                            (aeTemp2.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                                            break;
                                        }
                                        aeTemp2 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp2);
                                    }
                                }
                                break;
                            }
                            aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                        }
                    }
                    ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
                }//while
            }
            catch (Exception e)
            {
                string s = string.Format("E {0}: {1}",e.Message,ii);
                MessageBox.Show(s);
            }
        }

        public void MaxTime(AutomationElement aeBase, string sTime)
        {
            object valuePattern = null;
            AutomationElement ae, aeTemp;
            ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
            while (ae != null)
            {
                if ((ae.Current.AutomationId == "pnlSimParms"))
                {
                    aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                    while (aeTemp != null)
                    {
                        if (aeTemp.Current.AutomationId == "tbMaxTime")
                        {
                            if (aeTemp.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
                            {
                                aeTemp.SetFocus();
                                ((ValuePattern)valuePattern).SetValue(sTime);
                                break;
                            }
                        }
                        aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                    }//while
                    break;
                }//if
                ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
            }//while
        }

        public void AutoScale(AutomationElement aeBase)
        {
            AutomationElement ae, aeTemp;
            ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
            while (ae != null)
            {
                if ((ae.Current.AutomationId == "pnlPlotParms"))
                {
                    aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                    while (aeTemp != null)
                    {
                        if ((aeTemp.Current.AutomationId == "chkAutoScale"))
                        {
                            (aeTemp.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern).Toggle();
                            break;
                        }
                        aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                    }
                    break;
                }
                ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
            }
        }

        public void DelayPlot(AutomationElement aeBase)
        {
            AutomationElement ae, aeTemp;
            ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
            while (ae != null)
            {
                if ((ae.Current.AutomationId == "pnlPlotParms"))
                {
                    aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                    while (aeTemp != null)
                    {
                        if ((aeTemp.Current.AutomationId == "chkDelay"))
                        {
                            object oo = aeTemp.GetCurrentPattern(TogglePattern.Pattern);
                            TogglePattern tp = (oo as TogglePattern);
                            tp.Toggle();
                            break;
                        }
                        aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                    }
                    break;
                }
                ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
            }
        }

        public void SetIC(AutomationElement aeBase, string[] saParams)
        {
            AutomationElement ae, aeTemp, aeTemp2, aeTemp3, aeTemp4, aeTemp5;

            ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
            while (ae != null)
            {
                if ((ae.Current.AutomationId == "pnlInitVals"))
                {
                    aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                    while (aeTemp != null)
                    {
                        if ((aeTemp.Current.AutomationId == "tcInit"))
                        {
                            aeTemp2 = TreeWalker.ControlViewWalker.GetFirstChild(aeTemp);
                            while (aeTemp2 != null)
                            {
                                if (aeTemp2.Current.Name == saParams[0])
                                {
                                    (aeTemp2.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern).Select();
                                    aeTemp3 = TreeWalker.ControlViewWalker.GetFirstChild(aeTemp2);
                                    while (aeTemp3 != null)
                                    {
                                        if (aeTemp3.Current.Name == "DataGridView")
                                        {
                                            aeTemp4 = TreeWalker.ControlViewWalker.GetFirstChild(aeTemp3);
                                            while (aeTemp4 != null)
                                            {
                                                if (aeTemp4.Current.Name == saParams[1])
                                                {
                                                    aeTemp5 = TreeWalker.ControlViewWalker.GetFirstChild(aeTemp4);
                                                    while (aeTemp5 != null)
                                                    {
                                                        if (aeTemp5.Current.Name == saParams[2])
                                                        {
                                                            (aeTemp5.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue(saParams[3]);
                                                        }
                                                        aeTemp5 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp5);
                                                    }
                                                    break;
                                                }
                                                aeTemp4 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp4);
                                            }
                                            break;
                                        }
                                        aeTemp3 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp3);
                                    }
                                    break;
                                }
                                aeTemp2 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp2);
                            }
                            break;
                        }
                        aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                    }
                    break;
                }
                ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
            }
        }

        public void SaveSim(AutomationElement aeBase, string sFileName)
        {
            AutomationElement ae, aeTemp, aeTemp2,aeTemp3,aeTemp4;
            bool bDone = false;
            do
            {
                Thread.Sleep(5000);
                ae = TreeWalker.ControlViewWalker.GetFirstChild(aeBase);
                while (ae != null)
                {
                    if ((ae.Current.AutomationId == "pnlSimParms"))
                    {
                        aeTemp = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                        while (aeTemp != null)
                        {
                            if ((aeTemp.Current.AutomationId == "btSaveSim" && aeTemp.Current.IsEnabled))
                            {
                                (aeTemp.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                                Thread.Sleep(2000);
                                Condition pc = new PropertyCondition(AutomationElement.AutomationIdProperty, "frmSpecName", PropertyConditionFlags.None);
                                AutomationElement ae2 = aeBase.FindFirst(TreeScope.Element | TreeScope.Children, pc);
                                if (ae2 != null)
                                {
                                    aeTemp2 = TreeWalker.ControlViewWalker.GetFirstChild(ae2);
                                    while (aeTemp2 != null)
                                    {
                                        if ((aeTemp2.Current.AutomationId == "tbName"))
                                        {
                                            (aeTemp2.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue(sFileName);
                                            break;
                                        }
                                        aeTemp2 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp2);
                                    }
                                    aeTemp2 = TreeWalker.ControlViewWalker.GetFirstChild(ae2);
                                    while (aeTemp2 != null)
                                    {
                                        if ((aeTemp2.Current.AutomationId == "btNameOK"))
                                        {
                                            (aeTemp2.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                                            break;
                                        }
                                        aeTemp2 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp2);
                                    }
                                    bDone = true;
                                }
                                break;
                            }
                            aeTemp = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp);
                        }//while
                        //break;
                    }
                    //NEED TO MULTITHREAD THE TEST APP SO UI CAN RECEIVE FOCUS
                    //ClickStart SHOULD BE A THREAD, WRITING BACK TO UI WITH pass/fail WOULD NEED INVOKE
                    //NOT DIFFICULT

                    //ALTERNATE APPROACH - spin off a notepad process and focus on that.
                    this.Invoke(this.delSetFocus);
                    //if ((ae.Current.AutomationId == "pnlPlotParms"))
                    //{
                    //    aeTemp3 = TreeWalker.ControlViewWalker.GetFirstChild(ae);
                    //    while (aeTemp3 != null)
                    //    {
                    //        if ((aeTemp3.Current.AutomationId == "chkAutoScale"))
                    //        {
                    //            InputSimulator.SimulateKeyPress(VirtualKeyCode.SPACE);
                    //            //(aeTemp3.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern).Toggle();
                    //            break;
                    //        }
                    //        aeTemp3 = TreeWalker.ControlViewWalker.GetNextSibling(aeTemp3);
                    //    }
                    //    break;
                    //}
                    ae = TreeWalker.ControlViewWalker.GetNextSibling(ae);
                }//while
            }while(!bDone);
        }

        public string ExamineFiles(string sDir, string sPattern, string sTCid)
        {
            string s;
            try
            {
                foreach (string f in Directory.GetFiles(sDir, sPattern, SearchOption.TopDirectoryOnly))
                {
                    TimeSpan ts = DateTime.Now - File.GetCreationTime(f);
                    if (ts.Milliseconds < 60000)
                    {
                        sTCid = f;
                        return sTCid;
                    }
                }
                string[] dirs = Directory.GetDirectories(sDir, "*", SearchOption.TopDirectoryOnly);
                foreach (string d in dirs)
                {
                    s = ExamineFiles(d, sPattern, sTCid);
                    if (s != "")
                        return s;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return "";
        }

        public void TC0001(AutomationElement aeBase)
        {

            string sTCid = "TC0001";
            MaxTime(aeBase, "1000");
            DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.001";
            SetIC(aeBase,sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0002(AutomationElement aeBase)
        {

            string sTCid = "TC0002";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase,sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0003(AutomationElement aeBase)
        {

            string sTCid = "TC0003";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase,sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0004(AutomationElement aeBase)
        {

            string sTCid = "TC0004";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase,sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0005(AutomationElement aeBase)
        {

            string sTCid = "TC0005";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.001";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 4";
            sa[2] = "Value Row 4";
            sa[3] = "300";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0006(AutomationElement aeBase)
        {

            string sTCid = "TC0006";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 4";
            sa[2] = "Value Row 4";
            sa[3] = "300";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0007(AutomationElement aeBase)
        {

            string sTCid = "TC0007";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 4";
            sa[2] = "Value Row 4";
            sa[3] = "300";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0008(AutomationElement aeBase)
        {

            string sTCid = "TC0008";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 4";
            sa[2] = "Value Row 4";
            sa[3] = "300";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0009(AutomationElement aeBase)
        {

            string sTCid = "TC0009";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.001";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 4";
            sa[2] = "Value Row 4";
            sa[3] = "0";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "0.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0010(AutomationElement aeBase)
        {

            string sTCid = "TC0010";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0011(AutomationElement aeBase)
        {

            string sTCid = "TC0011";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0012(AutomationElement aeBase)
        {

            string sTCid = "TC0012";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0013(AutomationElement aeBase)
        {

            string sTCid = "TC0013";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0014(AutomationElement aeBase)
        {

            string sTCid = "TC0014";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "3";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0015(AutomationElement aeBase)
        {

            string sTCid = "TC0015";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "4";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0016(AutomationElement aeBase)
        {

            string sTCid = "TC0016";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            //bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0017(AutomationElement aeBase)
        {

            string sTCid = "TC0017";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "0.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0018(AutomationElement aeBase)
        {

            string sTCid = "TC0018";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0019(AutomationElement aeBase)
        {

            string sTCid = "TC0019";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0020(AutomationElement aeBase)
        {

            string sTCid = "TC0020";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0021(AutomationElement aeBase)
        {

            string sTCid = "TC0021";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.01F);
        }

        public void TC0022(AutomationElement aeBase)
        {
            int ii = 0;
            try
            {
                string sTCid = "TC0022";
                ii++;   //1
                MaxTime(aeBase, "1000");
                ii++;   //2
                //DelayPlot(aeBase);

                //set ICs
                string[] sa = new string[4];
                sa[0] = "Operational Parameters";
                sa[1] = "Row 3";
                sa[2] = "Value Row 3";
                sa[3] = "0.01";
                ii++;   //3
                SetIC(aeBase, sa);
                sa[0] = "Operational Parameters";
                sa[1] = "Row 5";
                sa[2] = "Value Row 5";
                sa[3] = "3";
                ii++;   //4
                SetIC(aeBase, sa);

                ii++;   //5
                Execute(aeBase);
                ii++;   //6
                SaveSim(aeBase, sTCid);

                Thread.Sleep(500);  //let file system settle

                //find file sTCid .bin (created within seconds)
                string strP = string.Format("*{0}.bin", sTCid), strF;
                ii++;   //7
                strF = ExamineFiles("C:\\", strP, sTCid);

                bInclude[9] = false;
                bInclude[10] = false;
                //bInclude[12] = false;
                bInclude[14] = false;

                ii++;   //8
                CompareResults(sTCid, strF, 0.1F);
            }
            catch (Exception e)
            {
                string s = string.Format("{0}: {1}",ii,e.Message);
                MessageBox.Show(s);
            }
        }

        public void TC0023(AutomationElement aeBase)
        {

            string sTCid = "TC0023";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "4";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0024(AutomationElement aeBase)
        {

            string sTCid = "TC0024";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.01";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[8] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0025(AutomationElement aeBase)
        {

            string sTCid = "TC0025";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "0.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0026(AutomationElement aeBase)
        {

            string sTCid = "TC0026";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0027(AutomationElement aeBase)
        {

            string sTCid = "TC0027";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0028(AutomationElement aeBase)
        {

            string sTCid = "TC0028";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0029(AutomationElement aeBase)
        {

            string sTCid = "TC0029";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0030(AutomationElement aeBase)
        {

            string sTCid = "TC0030";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "3";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[9] = false;
            bInclude[13] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0031(AutomationElement aeBase)
        {

            string sTCid = "TC0031";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "4";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[8] = false;
            bInclude[9] = false;
            bInclude[11] = false;
            bInclude[13] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0032(AutomationElement aeBase)
        {

            string sTCid = "TC0032";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "0.1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[8] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0033(AutomationElement aeBase)
        {

            string sTCid = "TC0033";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "0.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0034(AutomationElement aeBase)
        {

            string sTCid = "TC0034";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0035(AutomationElement aeBase)
        {

            string sTCid = "TC0035";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "1.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0036(AutomationElement aeBase)
        {

            string sTCid = "TC0036";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.0";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0037(AutomationElement aeBase)
        {

            string sTCid = "TC0037";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "2.5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.001F);
        }

        public void TC0038(AutomationElement aeBase)
        {

            string sTCid = "TC0038";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "3";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[9] = false;
            bInclude[10] = false;
            //bInclude[12] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0039(AutomationElement aeBase)
        {

            string sTCid = "TC0039";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "4";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[8] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public void TC0040(AutomationElement aeBase)
        {

            string sTCid = "TC0040";
            MaxTime(aeBase, "1000");
            //DelayPlot(aeBase);

            //set ICs
            string[] sa = new string[4];
            sa[0] = "Operational Parameters";
            sa[1] = "Row 3";
            sa[2] = "Value Row 3";
            sa[3] = "1";
            SetIC(aeBase, sa);
            sa[0] = "Operational Parameters";
            sa[1] = "Row 5";
            sa[2] = "Value Row 5";
            sa[3] = "5";
            SetIC(aeBase, sa);

            Execute(aeBase);
            SaveSim(aeBase, sTCid);

            Thread.Sleep(500);  //let file system settle

            //find file sTCid .bin (created within seconds)
            string strP = string.Format("*{0}.bin", sTCid), strF;
            strF = ExamineFiles("C:\\", strP, sTCid);

            bInclude[8] = false;
            bInclude[14] = false;

            CompareResults(sTCid, strF, 0.1F);
        }

        public bool CompareResults(string sID, string strFilePath, float fCrit)
        {
            int iNS, iNVars, iNumIC, iRecSize, iNumBytesSkip, iTestNum,iPrevDex, iIndex, k, iSav=-1;//iFreq
            //string sFile = "DESIRE_Comp_Data";
            float fAbsVal, fPerVal, fDiv;
            float[] faDataRec;
            string sVal;
            bool bResult=false;
            try
            {
                iTestNum = Int32.Parse(sID.Substring(2));
                BinaryReader bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
                iNS = bnryIn.ReadInt32();
                iNVars = bnryIn.ReadInt32();
                iNumIC = bnryIn.ReadInt32();

                iRecSize = iNVars * 4;
                iNumBytesSkip = iNumIC * 12 + 528 + (iNS-1) * iRecSize;
                bnryIn.BaseStream.Seek(iNumBytesSkip, SeekOrigin.Current);
                faDataRec = new float[iNVars];
                iIndex = 0;
                k = 0;
                for (int j = 0; j < iSz; j++)
                {
                    if (ivpaPairs[j].iTCNum == iTestNum)
                    {
                        if (iSav < 0)
                        {
                            iSav = j;
                            for (int jj = iSav; jj < iSav + iNumItems; jj++)
                            {
                                sVal = string.Format("{0},", ivpaPairs[jj].sName);
                                strmW.Write(sVal);
                            }
                            strmW.WriteLine();
                            for (int jj = iSav; jj < iSav + iNumItems; jj++)
                            {
                                sVal = string.Format("{0},", ivpaPairs[jj].fValueD);
                                strmW.Write(sVal);
                            }
                            strmW.WriteLine();
                        }
                        iPrevDex = iIndex;
                        iIndex = ivpaPairs[j].iIndex;
                        iNumBytesSkip = (iIndex-iPrevDex-1)*4;
                        bnryIn.BaseStream.Seek(iNumBytesSkip, SeekOrigin.Current);
                        ivpaPairs[j].fValueC = bnryIn.ReadSingle();
                        sVal = string.Format("{0},", ivpaPairs[j].fValueC);
                        strmW.Write(sVal);
                        if (ivpaPairs[j].fValueD == 0)
                            fDiv = 1.0F;
                        else
                            fDiv = ivpaPairs[j].fValueD;
                        fAbsVal = Math.Abs((ivpaPairs[j].fValueD - ivpaPairs[j].fValueC));
                        fPerVal = Math.Abs(fAbsVal / fDiv * 100.0F);
                        ivpaPairs[j].fResult = fPerVal;
                        k++;
                    }
                    if(k==iNumItems)
                        break;
                }
                strmW.WriteLine();
                float fMax = 0;
                for (int jj = iSav; jj < iSav + iNumItems; jj++)
                {
                    if (bInclude[jj-iSav] && ivpaPairs[jj].fResult > fMax)
                        fMax = ivpaPairs[jj].fResult;
                    sVal = string.Format("{0},", ivpaPairs[jj].fResult);
                    strmW.Write(sVal);
                }
                strmW.WriteLine();
                bResult = fMax < fCrit;
                sVal = string.Format("{0},{1},{2}", sID, fMax, bResult ? "pass" : "fail");
                strmW.WriteLine(sVal);
                strmW.WriteLine();
                bnryIn.BaseStream.Close();
                bnryIn.Close();
                sVal = string.Format("{0}: {1}", sID, bResult ? "pass" : "fail");
                this.Invoke(this.delAddResult, new Object[] { sVal });
                //lbResults.Items.Add(sVal);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return bResult;
        }

        public string GetNthToken(string sLine, int iTokeNum)
        {
            int iPos;
            string sResult, sTmp = sLine;
            try
            {
                for(int i=0; i<iTokeNum; i++)
                {
                    iPos = sTmp.IndexOf(',');
                    if(iPos<0)
                    {
                        sResult = "";
                        return sResult;
                    }
                    sTmp = sTmp.Substring(iPos+1);
                }
                iPos = sTmp.IndexOf(',');
                if(iPos>=0)
                    sResult = sTmp.Substring(0,iPos);
                else
                    sResult = sTmp;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                sResult = "";
            }
            return sResult;
        }

        public void InitPairs(string sSetName)
        {
            int iLineNum, iDex, iTCNum, kk;
            string sLine, sTmp;
            try
            {
                iLineNum = iNumTCs = iNumItems = iSz = 0;
                StreamReader strmR = new StreamReader("..\\..\\DESIRE_Comp_Data.csv");
                kk = 0;
                while (!strmR.EndOfStream)
                {
                    sLine = strmR.ReadLine();
                    if (GetNthToken(sLine, 0) == sSetName)
                    {
                        if (iLineNum == 0)
                        {
                            sTmp = GetNthToken(sLine, 1);
                            iNumTCs = Int32.Parse(sTmp);
                            sTmp = GetNthToken(sLine, 2);
                            iNumItems = Int32.Parse(sTmp);
                            iSz = iNumTCs * iNumItems;
                            ivpaPairs = new IndexValuePair[iSz];
                        }
                        if (iLineNum == 1)
                        {
                            for(int j=0; j<iNumItems; j++)
                                for (int k = j; k < iSz; k += 25)
                                    ivpaPairs[k].sName = GetNthToken(sLine, 2 + j);
                        }
                        if (iLineNum == 2)
                        {
                            for(int j=0; j<iNumItems; j++)
                            {
                                sTmp = GetNthToken(sLine, 2+j);
                                iDex = Int32.Parse(sTmp);
                                for (int k = j; k < iSz; k += 25)
                                    ivpaPairs[k].iIndex = iDex;
                            }
                            kk = 0;
                        }
                        if (iLineNum > 2)
                        {
                            iTCNum = Int32.Parse(GetNthToken(sLine, 1));
                            for (int l = 0; l < iNumItems; l++)
                            {
                                ivpaPairs[kk+l].iTCNum = iTCNum;
                                ivpaPairs[kk+l].fValueD = Single.Parse(GetNthToken(sLine, 2 + l));
                            }
                            kk += 25;
                        }
                        iLineNum++;
                    }//if
                }//while
                strmR.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitPairs("Supo1");
            bInclude = new bool[iNumItems];
            for (int i = 0; i < iNumItems; i++ )
                bInclude[i] = true;
            delAddResult = new AddResult(AddResultMessage);
            delSetFocus = new SetFocus(SetFocusAction);
        }

        void AddResultMessage(string sMessage)
        {
            lbResults.Items.Add(sMessage);
        }

        void SetFocusAction()
        {
            //this.Focus();
            //tbReceiver.Focus();
            InputSimulator.SimulateKeyPress(VirtualKeyCode.CONTROL);
        }

        private void RunTest()
        {
            Condition propCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "MainForm", PropertyConditionFlags.None);
            AutomationElement aeBase = AutomationElement.RootElement.FindFirst(TreeScope.Element | TreeScope.Children, propCondition);
            strmW = new StreamWriter("..\\..\\Supo1report.csv", false);

            TC0001(aeBase);
            strmW.Flush();
            TC0002(aeBase);
            strmW.Flush();
            TC0003(aeBase);
            strmW.Flush();
            TC0004(aeBase);
            strmW.Flush();
            TC0005(aeBase);
            strmW.Flush();
            TC0006(aeBase);
            strmW.Flush();
            TC0007(aeBase);
            strmW.Flush();
            TC0008(aeBase);
            strmW.Flush();
            TC0009(aeBase);
            strmW.Flush();
            TC0010(aeBase);
            strmW.Flush();
            TC0011(aeBase);
            strmW.Flush();
            TC0012(aeBase);
            strmW.Flush();
            TC0013(aeBase);
            strmW.Flush();
            TC0014(aeBase);
            strmW.Flush();
            TC0015(aeBase);
            strmW.Flush();
            TC0016(aeBase);
            strmW.Flush();
            TC0017(aeBase);
            strmW.Flush();
            TC0018(aeBase);
            strmW.Flush();
            TC0019(aeBase);
            strmW.Flush();
            TC0020(aeBase);
            strmW.Flush();
            TC0021(aeBase);
            strmW.Flush();
            TC0022(aeBase);
            strmW.Flush();
            TC0023(aeBase);
            strmW.Flush();
            TC0024(aeBase);
            strmW.Flush();
            TC0025(aeBase);
            strmW.Flush();
            TC0026(aeBase);
            strmW.Flush();
            TC0027(aeBase);
            strmW.Flush();
            TC0028(aeBase);
            strmW.Flush();
            TC0029(aeBase);
            strmW.Flush();
            TC0030(aeBase);
            strmW.Flush();
            TC0031(aeBase);
            strmW.Flush();
            TC0032(aeBase);
            strmW.Flush();
            TC0033(aeBase);
            strmW.Flush();
            TC0034(aeBase);
            strmW.Flush();
            TC0035(aeBase);
            strmW.Flush();
            TC0036(aeBase);
            strmW.Flush();
            TC0037(aeBase);
            strmW.Flush();
            TC0038(aeBase);
            strmW.Flush();
            TC0039(aeBase);
            strmW.Flush();
            TC0040(aeBase);
            strmW.Flush();

            strmW.Close();
        }

        private void ClickStart(object sender, EventArgs e)
        {
            try
            {
                thrTask = new Thread(new ThreadStart(RunTest));
                thrTask.Start();
            }
            catch (Exception ee)
            {
                strmW.Close();
                MessageBox.Show(ee.Message);
            }

        }
    }
}
