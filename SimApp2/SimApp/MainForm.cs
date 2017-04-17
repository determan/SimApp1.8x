using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Numerics;
using SimWrp;

namespace SimApp
{
    public partial class MainForm : Form
    {
        enum EX_STATE {EBS_EXECUTE=0, EBS_PAUSE, EBS_CONTINUE, EBS_ABORT};
        private EX_STATE iExBtState;
        private float fTimeStep;
        private int iNumSteps;
        private float fMaxTime;
        private int iFreq;
        private Array straSDNames;
        private int[] iaIndices;
        private float[] faData;
        private float[] faData2;
        private float[] faData3;
        private string[] sLabels;
        private int iNumVars;
        private CallBack delP;
        private System.Drawing.Color clExBt;
        private SimModel m1;
        private string strFilePath;
        private int[] iaInitInds;
        private double[] daInitVals;
        private string strDataStorePath;
        private bool bLive;
        private Thread thrProc;
        private Thread thrCons;
        private int iResultRow;
        private Size szWindowSize;
        private bool bSaveNorm = true;
        private bool bSaveMax = true;
        private int[] aSizes;
        private int[] aSizes2;
        private int iSRcnt = -1;
        private string strMsg = "";
        private bool bContinue;
        private bool bDone;
        private BinaryReader bnryIn;
        private Rectangle rectScreen;
        //private Version verInfo;
        private String sModName = "";


        private int iMult;
        private int iCnt;
        private SeriesChartType chartType = SeriesChartType.Line;
        PlotEventArgs ee;
        private PlotEventArgs[] aee;
        bool readerFlag = false;  // State flag
        public delegate void CallBack([In][MarshalAs(UnmanagedType.I4)] Int32 iNumS, [In][MarshalAs(UnmanagedType.R4)] float fX, [In][MarshalAs(UnmanagedType.R4)] float fY);

        private InitCondInfo[] aICs;
        private int iCSize;
        private int iNumIC;
        private int iOrder;
        private int iSkip;
        private string sPlotFilterStr;
        private bool bLoadGroup;

        private bool bFirstSave;
        private int iPlotCnt;
        private bool bAutoScale;
        private Color clrSaveASFColor;
        private Color clrSaveASBColor;
        private int iRetryCnt;
        private string sDefFileName;
        //private bool bResetAuto;
        private bool bDisplay;
        //private int[] iaCntr;
        private int iSyncRowNum;
        private float fDataTS;
        private int iNumCustIcTabs;
        private bool bPlotting;
        private bool bStabCalcsDone;
        private bool bDelayPlot;
        //private Point svLocation;

        private DataGridView[] aIcDgs;

        //private Stopwatch swTimer;
        //private long startT;
        //private long endT;
        private System.Windows.Forms.Timer timer;

        private bool bMultDisplayMode;

        private ScaleVals.ScaleType eScaleType;
        private double[] daStVals;
        private string sSbvPath;
        private string sFullName;

        private List<string> listNoPlotItems;

        //private int iCycles;

        public struct VarInfo
        {
            public string sVarName;
            public string sName;
            public string sGroup;
            public double dValue;
            public string sUnits;
            public int iArraySize;
        }

        public struct ConstInfo
        {
            public string sVarName;
            public string sName;
            public string sType;
            public double dValue;
            public string sUnits;
            public int iArraySize;

            public void Set(ConstInfo ci, string snm, int indx)
            {
                string str;
                sVarName = ci.sVarName;
                str = string.Format("{0} {1}", snm, indx);
                sName = str;
                sType = ci.sType;
                dValue = ci.dValue;
                sUnits = ci.sUnits;
                iArraySize = ci.iArraySize;
            }
        }
        ConstInfo[] ciaInfo;

        public class InitCondInfo
        {
            public int iSecNum;
            public int iRowNum;
            public int iIndex;
            public string strLabel;
            public double dValue;
            public string sUnits;
            public string sType;

            public InitCondInfo(int iSN, int iRN, int iI, string sl, double dV, string su, string st)
            {
                iSecNum = iSN;
                iRowNum = iRN;
                iIndex = iI;
                strLabel = sl;
                dValue = dV;
                sUnits = su;
                sType = st;
            }
        }

        public class PlotEventArgs : EventArgs
        {
            public PlotEventArgs(int iNumS, float fX, float fY)
            {
                iDataNum = iNumS;
                dX = fX;
                dY = fY;
            }
            public int iDataNum;
            public float dX;
            public float dY;
        }

        public void OnPlot(Int32 iNumS, float fX, float fY)
        {
            lock (this)  // Enter synchronization block
            {
                if (readerFlag)
                {      // Wait until Cell.ReadFromCell is done consuming.
                    try
                    {
                        Monitor.Wait(this);   // Wait for the Monitor.Pulse in
                        // ReadFromCell
                    }
                    catch (SynchronizationLockException e)
                    {
                        this.Invoke(this.delSetStatus, new Object[] { e.Message });
                    }
                    catch (ThreadInterruptedException e)
                    {
                        this.Invoke(this.delSetStatus, new Object[] { e.Message });
                    }
                }
                try
                {
                    faData2 = (float[])faData.Clone();
                    ee = new PlotEventArgs(iNumS, fX, fY);
                    readerFlag = true;
                    Monitor.Pulse(this);
                }
                catch (OutOfMemoryException e)
                {
                    MessageBox.Show("Out of Memory - OnPlot");
                    System.GC.Collect();
                }
                catch (Exception e)
                {
                    this.Invoke(this.delSetStatus, new Object[] { e.Message });
                }
            }
        }

        public void PlotValues()
        {
            int ii, jj;
            float fTime;
            try
            {
                iCnt = 0;
                int iNumPnts = iMult * iNumVars;
                aee = new PlotEventArgs[iNumPnts];
                bool bDone = false, bPlot;
                while (!bDone)
                {
                    bPlot = false;
                    lock (this)   // Enter synchronization block
                    {
                        if (!readerFlag)
                        {            // Wait until Cell.WriteToCell is done producing
                            try
                            {
                                // Waits for the Monitor.Pulse in WriteToCell
                                Monitor.Wait(this);
                            }
                            catch (SynchronizationLockException e)
                            {
                                this.Invoke(this.delSetStatus, new Object[] { e.Message });
                            }
                            catch (ThreadInterruptedException e)
                            {
                                this.Invoke(this.delSetStatus, new Object[] { e.Message });
                            }
                        }
                        if (-ee.iDataNum == 1)
                            this.Invoke(this.delUpdateGraph0, new Object[] { ee });
                        if (-ee.iDataNum == 2)
                        {
                            //endT = swTimer.ElapsedMilliseconds;
                            //labRunning.Visible = false;
                            timer2.Stop();

                            //this.Invoke(this.delSetStatus, new Object[] { string.Format("Elapsed time: {0}", (endT - startT) / 1000) });
                            this.Invoke(this.delUpdateGraph0, new Object[] { ee });
                            if(iCnt>0)
                                this.Invoke(this.delUpdateGraph, new Object[] { iCnt, aee });
                            bDone = true;
                        }
                        if (-ee.iDataNum == 3)
                        {
                            if (bFirstSave)
                            {
                                bFirstSave = false;
                                faData3 = (float[])faData2.Clone();
                                bPlot = true;
                                iPlotCnt = 0;
                            }
                            else
                            {
                                for (int i = 1; i < faData2.Length; i++)
                                {
                                    if(Math.Abs(faData2[0]-fMaxTime)/fTimeStep < 1F)
                                    { 
                                        bPlot = true;
                                        break;
                                    }
                                    //if (faData2[i] < 0.0001F)
                                    //    continue;
                                    if (iPlotCnt>=100000 || Math.Abs((faData2[i] - faData3[i]) / faData2[i]) >= 0.01)
                                    {
                                        bPlot = true;
                                        faData3 = (float[])faData2.Clone();
                                        iPlotCnt = 0;
                                        break;
                                    }

                                }
                            }
                            if (bPlot)
                            {
                                ii = 0;
                                jj = 0;

                                for (int i = 0; i < iMult; i++)
                                {
                                    fTime = faData2[jj++]; //time
                                    for (int k = 0; k < iNumVars; k++)
                                    {
                                        PlotEventArgs earg = new PlotEventArgs(k, fTime, faData2[jj++]);
                                        aee.SetValue(earg, ii++);
                                        //sw.Write("PlotValues, {0}, {1}, {2}, {3}, {4}", ii, jj, k, fTime, faData[jj - 1]);
                                        //sw.WriteLine();
                                    }
                                }
                                this.Invoke(this.delUpdateGraph, new Object[] { iNumPnts, aee });
                            }
                        }
                        readerFlag = false;
                        Monitor.Pulse(this);   // Pulse tells Cell.WriteToCell that
                        iPlotCnt++;
                    }// Exit synchronization block
                }
                //move temp file
                //sw.Close();
            }
            catch (OutOfMemoryException e)
            {
                MessageBox.Show("Out of Memory - PlotVlaues");
                System.GC.Collect();
            }
            catch (ThreadAbortException e)
            {
                Thread.ResetAbort();
            }
            catch (Exception e)
            {
                this.Invoke(this.delSetStatus, new Object[] { e.Message });
            }
        }

        private void InitTabControl()
        {
            int iRow,iDgIndex=0;
            //dg1.Rows.Clear();
            while(tcInit.TabPages.Count>0)
                tcInit.TabPages.RemoveAt(0);
            //tcInit.TabPages[0].BackColor = System.Drawing.SystemColors.ControlDark;
            //tcInit.TabPages[0].Text = "Physical Parameters";
            //dg1.AllowUserToAddRows = true;
            //dg1.Columns[0].Width = 200;
            foreach (InitCondInfo ici in aICs)
            {
                //if (ici.iSecNum == 0)
                //{
                //    iRow = dg1.Rows.Add();
                //    dg1.Rows[iRow].Cells[0].Value = ici.strLabel;
                //    dg1.Rows[iRow].Cells[1].Value = ici.dValue.ToString();
                //    dg1.Rows[iRow].Cells[2].Value = ici.sUnits;
                //    dg1.Rows[iRow].Cells[0].Style.ForeColor = Color.Black;
                //    dg1.Rows[iRow].Cells[1].Style.ForeColor = Color.Black;
                //    dg1.Rows[iRow].Cells[2].Style.ForeColor = Color.Black;
                //}
                //else
                //{
                    if(!tcInit.TabPages.ContainsKey(ici.sType))
                    {
                       tcInit.TabPages.Add(ici.sType,ici.sType);
                       tcInit.TabPages[iDgIndex].BackColor = System.Drawing.SystemColors.ControlDark;
                       aIcDgs[iDgIndex] = new DataGridView();
                       aIcDgs[iDgIndex].Parent = tcInit.TabPages[iDgIndex];
                       aIcDgs[iDgIndex].Visible = true;
                       aIcDgs[iDgIndex].Size = new Size(465,405);
                       aIcDgs[iDgIndex].AllowUserToAddRows = true;
                       aIcDgs[iDgIndex].AllowUserToDeleteRows = false;
                       aIcDgs[iDgIndex].AllowUserToResizeRows = false;
                       aIcDgs[iDgIndex].ForeColor = Color.Black;

                       aIcDgs[iDgIndex].Columns.Add("ItemLabel", "Parameter");
                       aIcDgs[iDgIndex].Columns.Add("ItemValue","Value");
                       aIcDgs[iDgIndex].Columns.Add("ItemUnits","Units");
                       aIcDgs[iDgIndex].Columns[0].Width = 200;
                       aIcDgs[iDgIndex].Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                       aIcDgs[iDgIndex].Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                       aIcDgs[iDgIndex].Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                       iDgIndex++;
                    }
                    iRow = aIcDgs[ici.iSecNum].Rows.Add();
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[0].Value = ici.strLabel;
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[1].Value = ici.dValue.ToString();
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[2].Value = ici.sUnits;
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[0].Style.ForeColor = Color.Black;
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[1].Style.ForeColor = Color.Black;
                    aIcDgs[ici.iSecNum].Rows[iRow].Cells[2].Style.ForeColor = Color.Black;
                //}
            }
            //dg1.AllowUserToAddRows = false;
            //dg1.AllowUserToDeleteRows = false;
            foreach(DataGridView dgv in aIcDgs)
                dgv.AllowUserToAddRows = false;
        }

        public void InitReplayList()
        {
            cbReplayList.Items.Clear();
            string s = Directory.GetCurrentDirectory();
            if (!Directory.Exists(strDataStorePath))
            {
                Directory.CreateDirectory(strDataStorePath);
            }
            cbReplayList.Items.AddRange(Directory.GetFiles(strDataStorePath));
        }

        public void SetICs()
        {
            try
            {
                int j = 0, iType = -1;
                iCSize = m1.CSize();
                double[] dICs = new double[iCSize];
                GCHandle handle2 = GCHandle.Alloc(dICs, GCHandleType.Pinned);
                var daptr = handle2.AddrOfPinnedObject();
                m1.ICs(daptr);
                handle2.Free();
                iNumIC = 0;
                for (int i = 0; i < iCSize; i++)
                {
                    ciaInfo[i].dValue = dICs[i];
                    if (ciaInfo[i].sType != "Constant")
                        iNumIC++;
                }
                m1.SetNumIC(iNumIC);
                aICs = new InitCondInfo[iNumIC];
                int iNextSecNum = 0;
                string[] sa = new string[20];
                int[] ia = new int[20];
                for (int i = 0; i < iCSize; i++)
                {
                    if (ciaInfo[i].sType != "Constant")
                    {
                        if (!sa.Contains<string>(ciaInfo[i].sType))
                        {
                            //type is not in sa
                            sa[iNextSecNum] = ciaInfo[i].sType;
                            ia[iNextSecNum]++;
                            iType = iNextSecNum++;
                            //o++;
                        }
                        else
                        {
                            //type is in sa
                            for (int k = 0; k < sa.Length; k++)
                            {
                                if (sa[k] == ciaInfo[i].sType)
                                {
                                    ia[k]++;
                                    iType = k;
                                    break;
                                }
                            }
                        }
                        if (iType >= 0)
                            aICs[j] = new InitCondInfo(iType, ia[iType], i, ciaInfo[i].sName, ciaInfo[i].dValue, ciaInfo[i].sUnits, ciaInfo[i].sType);
                        j++;
                    }
                }
                InitTabControl();
            }
            catch (Exception e)
            {
            }
        }

        private void ResetYScale()
        {
            float fSpan = m1.YScale();
            chDataGraph.ChartAreas[0].AxisY.Minimum = -fSpan;
            chDataGraph.ChartAreas[0].AxisY.Maximum = fSpan;
            chDataGraph.ChartAreas[0].AxisY.LabelStyle.Interval = fSpan / 4F;
            chDataGraph.ChartAreas[0].AxisY.MajorGrid.Interval = fSpan / 2F;
        }

        public void SetStabPlotDefaults(string sName)
        {
            switch(sName)
            {
                case "AD125":
                    daStVals = new double[] { -0.08, 0.08, 0.02, 0.04, -4.0, 4.0, 2.0, 1.0, 1e-3, 1e2, 1e-3, 1, 0, -0.5, 0.5, 0.125, 0.25, -4.0, 4.0, 2.0, 1.0, 1e-3, 1e2, 1e-3, 1, 0, -16.0, 16.0, 4.0, 8.0, -16.0, 16.0, 4.0, 8.0, -1.0, 1.0, 1e-4, 0, 0 };
                    break;
                case "AD135":
                    daStVals = new double[] { -3.0 / 20, 3.0 / 20, 3.0 / 80, 3.0 / 40, -4.0, 4.0, 2.0, 1.0, 1e-3, 1e2, 1e-3, 1, 0, -3.0 / 2, 3.0 / 2, 3.0 / 8, 3.0 / 4, -4.0, 4.0, 2.0, 1.0, 1e-3, 1e2, 1e-3, 1, 0, -16.0, 16.0, 4.0, 8.0, -16.0, 16.0, 4.0, 8.0, -1.0, 1.0, 1e-4, 0, 0 };
                    break;
                case "SupoModel":
                    daStVals = new double[] { -3.0 / 25, 3.0 / 25, 3.0 / 100, 3.0 / 50, -4.0, 4.0, 1.0, 2.0, 1e-3, 1e2, 1e-3, 1, 0, -2.0, 2.0, 0.5, 1.0, -4.0, 4.0, 1.0, 2.0, 1e-3, 1e2, 1e-3, 1, 0, -50.0, 50.0, 12.5, 25.0, -50.0, 50.0, 12.5, 25.0, -1.0, 1.0, 1e-4, 0, 0 };
                    break;
            }
        }

        public void SaveStabPlotValues(string sMName, bool append)
        {
            string sBak = sSbvPath.Replace(".sbv", ".bak");
            string sBufIn, sBufOut;
            bool bCurrentModel = false;
            int i=0, iPos;
            StreamReader strmR;
            StreamWriter strmW;
            if (!append)
            {//file is known to exist so replace content for provided name
                File.Copy(sSbvPath, sBak);
                Thread.Sleep(250);
                strmR = new StreamReader(sBak);
                strmW = new StreamWriter(sSbvPath, false);
                if (strmR.BaseStream != null && strmW.BaseStream != null)
                {
                    while (!strmR.EndOfStream)
                    {
                        sBufIn = strmR.ReadLine();
                        iPos = sBufIn.IndexOf("MODEL:");
                        if (iPos >= 0)
                        {
                            sBufOut = sBufIn;
                            sBufIn = sBufIn.Substring(iPos + 6).Trim();
                            bCurrentModel = (sBufIn == sMName);
                            strmW.WriteLine(sBufOut);
                            continue;
                        }
                        //only lines containing numbers below this point

                        if (bCurrentModel)
                            sBufOut = string.Format("{0}", daStVals[i++]);
                        else
                            sBufOut = sBufIn;
                        strmW.WriteLine(sBufOut);
                    }
                    strmR.Close();
                    strmW.Close();
                    Thread.Sleep(250);
                    File.Delete(sBak);
                }
            }
            else
            {   // file does not exist so append info for given name
                strmW = new StreamWriter(sSbvPath, true);
                if (strmW.BaseStream != null)
                {
                    strmW.BaseStream.Seek(0, SeekOrigin.End);
                    strmW.WriteLine("MODEL: " + sMName);
                    foreach (Double d in daStVals)
                    {//just putting out numbers from array
                        sBufOut = string.Format("{0}",d);
                        strmW.WriteLine(sBufOut);
                    }
                    strmW.Close();
                }
            }
        }

        public void GetStabPlotValues(string sMName)
        {
            string sBufIn;
            int i, iPos;
            bool bCurrentModel = false;
            StreamReader strmR = new StreamReader(sSbvPath);
            if (strmR.BaseStream != null)
            {
                i = 0;
                daStVals = new double[39];
                while (!strmR.EndOfStream)
                {
                    sBufIn = strmR.ReadLine();
                    iPos = sBufIn.IndexOf("MODEL:");
                    if (iPos >= 0)
                    {
                        sBufIn = sBufIn.Substring(iPos + 6).Trim();
                        bCurrentModel = (sBufIn == sMName);
                        continue;
                    }
                    if(bCurrentModel)
                    {
                        daStVals[i++] = Double.Parse(sBufIn);
                    }
                }
                strmR.Close();
            }
        }

        public MainForm()
        {
            try
            {
                InitializeComponent();
                //swTimer = new Stopwatch();
                m1 = new SimWrp.SimModel();
                m1.GetName(ref sModName);
                switch (sModName)
                {
                    case "AD125":       sFullName = ""; break;
                    case "AD135":       sFullName = "Subcritical Accelerator-Driven System"; break;
                    case "SupoModel":   sFullName = ""; break;
                    case "GODIVAQ":     sFullName = ""; break;
                    default:            sFullName = ""; break;
                }
                if (sFullName.Length > 0)
                    Text = sFullName;
                else
                    Text = sModName;
                sSbvPath = ".\\ICs\\stabvals.sbv";

                if (!File.Exists(sSbvPath))
                {
                    SetStabPlotDefaults("AD125");
                    SaveStabPlotValues("AD125", true);
                    SetStabPlotDefaults("AD135");
                    SaveStabPlotValues("AD135", true);
                    SetStabPlotDefaults("SupoModel");
                    SaveStabPlotValues("SupoModel", true);
                    SetStabPlotDefaults(sModName);
                }
                else
                {
                    GetStabPlotValues(sModName);
                }
                ciaInfo = new ConstInfo[m1.CSize()];
                iNumCustIcTabs = m1.NumCustIcTabs();
                aIcDgs = new DataGridView[iNumCustIcTabs];
                //dg1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dg1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

                listNoPlotItems = new List<string>();
                MakeStateLookup();

                fTimeStep = 0.0f;
                iNumSteps = 0;
                fMaxTime = 0.0f;
                iFreq = 1;
    //            iNum = 100;
                iExBtState = EX_STATE.EBS_EXECUTE;
                readerFlag = false;
                delUpdateGraph = new UpdateGraph(UpdateGraphMethod);
                delUpdateGraph0 = new UpdateGraph0(UpdateGraphMethod0);
                delSetStatus = new SetStatus(SetStatusMessage);
                //delSuspendAR = new SuspendAutoRescale(SuspendAR);


                chDataGraph.Series[0].ChartType = chartType;
                chDataGraph.BackColor = Color.Black;
                chDataGraph.ChartAreas[0].AxisX.Minimum = 0.0;
                chDataGraph.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                chDataGraph.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                chDataGraph.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chDataGraph.ChartAreas[0].AxisX.Title = "Time";
                chDataGraph.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                chDataGraph.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chDataGraph.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chDataGraph.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                chDataGraph.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chDataGraph.ChartAreas[0].AxisY.Title = "";
                chDataGraph.ChartAreas[0].AxisY.Crossing = 0.0;
                chDataGraph.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chDataGraph.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

                chBodeAmp.Series[0].ChartType = SeriesChartType.Line;
                chBodeAmp.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                chBodeAmp.BackColor = Color.Black;
                chBodeAmp.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                chBodeAmp.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chBodeAmp.ChartAreas[0].AxisX.Title = "";
                chBodeAmp.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                chBodeAmp.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chBodeAmp.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chBodeAmp.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                chBodeAmp.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chBodeAmp.ChartAreas[0].AxisY.Title = "";
                chBodeAmp.ChartAreas[0].AxisY.Crossing = 0.0;
                chBodeAmp.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chBodeAmp.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chBodeAmp.Titles.Add("0");
                chBodeAmp.Titles[0].Text = "Bode Amplitude Plot 1";
                chBodeAmp.Titles[0].ForeColor = System.Drawing.Color.Red;

                //chBodeAmp2.Series[0].ChartType = SeriesChartType.Line;
                //chBodeAmp2.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                //chBodeAmp2.BackColor = Color.Black;
                //chBodeAmp2.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                //chBodeAmp2.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chBodeAmp2.ChartAreas[0].AxisX.Title = "";
                //chBodeAmp2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                //chBodeAmp2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chBodeAmp2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chBodeAmp2.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                //chBodeAmp2.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chBodeAmp2.ChartAreas[0].AxisY.Title = "";
                //chBodeAmp2.ChartAreas[0].AxisY.Crossing = 0.0;
                //chBodeAmp2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chBodeAmp2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chBodeAmp2.Titles.Add("0");
                //chBodeAmp2.Titles[0].Text = "Bode Amplitude Plot 2";
                //chBodeAmp2.Titles[0].ForeColor = System.Drawing.Color.Red;

                chBodePhase.Series[0].ChartType = SeriesChartType.Line;
                chBodePhase.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                chBodePhase.BackColor = Color.Black;
                chBodePhase.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                chBodePhase.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chBodePhase.ChartAreas[0].AxisX.Title = "";
                chBodePhase.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                chBodePhase.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chBodePhase.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chBodePhase.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                chBodePhase.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chBodePhase.ChartAreas[0].AxisY.Title = "";
                chBodePhase.ChartAreas[0].AxisY.Crossing = 0.0;
                chBodePhase.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chBodePhase.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chBodePhase.Titles.Add("0");
                chBodePhase.Titles[0].Text = "Bode Phase Plot 1";
                chBodePhase.Titles[0].ForeColor = System.Drawing.Color.Red;

                //chBodePhase2.Series[0].ChartType = SeriesChartType.Line;
                //chBodePhase2.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                //chBodePhase2.BackColor = Color.Black;
                //chBodePhase2.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                //chBodePhase2.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chBodePhase2.ChartAreas[0].AxisX.Title = "";
                //chBodePhase2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                //chBodePhase2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chBodePhase2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chBodePhase2.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                //chBodePhase2.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chBodePhase2.ChartAreas[0].AxisY.Title = "";
                //chBodePhase2.ChartAreas[0].AxisY.Crossing = 0.0;
                //chBodePhase2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chBodePhase2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chBodePhase2.Titles.Add("0");
                //chBodePhase2.Titles[0].Text = "Bode Phase Plot 2";
                //chBodePhase2.Titles[0].ForeColor = System.Drawing.Color.Red;

                //chNyq.Series[0].ChartType = SeriesChartType.Line;
                //chNyq.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                //chNyq.BackColor = Color.Black;
                //chNyq.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                //chNyq.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chNyq.ChartAreas[0].AxisX.Title = "";
                //chNyq.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                //chNyq.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chNyq.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chNyq.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                //chNyq.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                //chNyq.ChartAreas[0].AxisY.Title = "";
                //chNyq.ChartAreas[0].AxisY.Crossing = 0.0;
                //chNyq.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                //chNyq.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                //chNyq.Titles.Add("0");
                //chNyq.Titles[0].Text = "Nyquist Plot";
                //chNyq.Titles[0].ForeColor = System.Drawing.Color.Red;

                chStab.Series[0].ChartType = SeriesChartType.Line;
                chStab.ChartAreas[0].BackColor = System.Drawing.Color.Black;
                chStab.BackColor = Color.Black;
                chStab.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
                chStab.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chStab.ChartAreas[0].AxisX.Title = "";
                chStab.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.White;
                chStab.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chStab.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chStab.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
                chStab.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red;
                chStab.ChartAreas[0].AxisY.Title = "";
                chStab.ChartAreas[0].AxisY.Crossing = 0.0;
                chStab.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                chStab.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chStab.Titles.Add("0");
                chStab.Titles[0].Text = "Stability Plot";
                chStab.Titles[0].ForeColor = System.Drawing.Color.Red;

                bStabCalcsDone = false;

                clExBt = btnExecute.BackColor;

                strDataStorePath = ".\\SimData";
                InitReplayList();
                bLive = true;

                timer1.Tick += timer1_Tick;
                timer2.Tick += timer2_Tick;
                chDataGraph.PreviewKeyDown += chDataGraph_PreviewKeyDown;
                szWindowSize = this.Size;
                rectScreen = Screen.FromControl(this).Bounds;

                //SetICs();
                //ToggleVisibility(true);
                //dg1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                //above call also sets scale value, so do Y Axis values here
                //ResetYScale();

                iSkip = 1;

                sPlotFilterStr = "";
                ClickMenuPlotGroups(this, null);
                bAutoScale = false;
                //rbAutoScale.Checked = false;
                //rbAutoScale.AutoCheck = false;
                chkAutoScale.Checked = false;
                chkAutoScale.AutoCheck = false;
                iRetryCnt = 0;
                sDefFileName = "__SimResults__Default.bin";
                //bResetAuto = false;
                strFilePath = ".\\" + sDefFileName;
                iSyncRowNum = -1;
                btSaveSim.Enabled = false;
                rbReplay.AutoCheck = true;
                rbLive.AutoCheck = true;
                rbLive.Checked = true;
                btAbort.Enabled = false;
                btPlot.Enabled = false;
                btData.Enabled = false;
                btCSV.Enabled = false;

                chDataGraph.ChartAreas[0].CursorX.IsUserEnabled = true;
                chDataGraph.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chDataGraph.ChartAreas[0].CursorX.Interval = 0;
                chDataGraph.ChartAreas[0].CursorX.LineColor = Color.Red;
                chDataGraph.ChartAreas[0].CursorX.LineWidth = 1;
                chDataGraph.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
                dgResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dgResults.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
                bDelayPlot = false;

                if (lbAllPlotItems.Items.Count > 0)
                    lbAllPlotItems.SelectedIndex = 0;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                ToggleVisibilityPlots(true);
                eScaleType = ScaleVals.ScaleType.None;
            }
            catch (Exception e)
            {
                tbStatus.Text = e.Message;
            }
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            labRunning.Visible = !labRunning.Visible;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int iSz = cbReplayList.Items.Count;
                InitReplayList();
                if (cbReplayList.Items.Count > iSz)
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = ee.Message;
            }
        }

        private void RunSim()
        {
            try
            {
                IntPtr intptr_delegate = Marshal.GetFunctionPointerForDelegate(delP);
                faData = new float[iMult * (iNumVars + 1)];
                GCHandle handle = GCHandle.Alloc(iaIndices, GCHandleType.Pinned);
                var iaptr = handle.AddrOfPinnedObject();
                GCHandle handle2 = GCHandle.Alloc(faData, GCHandleType.Pinned);
                var faptr = handle2.AddrOfPinnedObject();
                GCHandle handle3 = GCHandle.Alloc(iaInitInds, GCHandleType.Pinned);
                var iaptr2 = handle3.AddrOfPinnedObject();
                GCHandle handle4 = GCHandle.Alloc(daInitVals, GCHandleType.Pinned);
                var daptr2 = handle4.AddrOfPinnedObject();
                m1.Run(intptr_delegate, fTimeStep, iNumSteps, fMaxTime, false, iFreq, iMult, iNumVars, iaptr, faptr, iaptr2, daptr2);
                handle.Free();
                handle2.Free();
                handle3.Free();
                handle4.Free();
            }
            catch (ThreadAbortException e)
            {
                Thread.ResetAbort();
            }
            catch (Exception e)
            {
                this.Invoke(this.delSetStatus, new Object[] { e.Message });
            }
        }

        private void RunReplay()
        {
            int iNS, iNVars, iNSF, iRecSize, iNumBytesSkip, iOff;
            long lBR;
            float fTS, fMT;
            float[] faDataRec;
            try
            {
                faData = new float[iMult * (iNumVars + 1)];
                //open file
                FileInfo fi = new FileInfo(strFilePath);

                bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
                //read Header, mainly SFs
                iNS = bnryIn.ReadInt32();
                iNVars = bnryIn.ReadInt32();
                bnryIn.ReadInt32();    //numIC
                iFreq = bnryIn.ReadInt32();
                fTS = bnryIn.ReadSingle();
                fMT = bnryIn.ReadSingle();
                //read in ICs
                for (int i = 0; i < iNumIC; i++)
                {
                    bnryIn.ReadInt32();
                    bnryIn.ReadDouble();
                }
                iNSF = iNVars - 1;
                iRecSize = iNVars * 4;
                faDataRec = new float[iNVars];
                bnryIn.ReadBytes(512);  //skip memo field
                lBR = 24 + iNumIC*12 + 512; 
                iNumBytesSkip = (iFreq / iMult - 1) * iRecSize;
                if (iNumBytesSkip < 0) iNumBytesSkip = 0;
                bDone = false;
                while (!bDone)
                {
                    if (bContinue)
                    {
                        for (int i = 0; i < iMult; i++)
                        {
                            if (fi.Length - lBR < iRecSize)
                                break;
                            //read next plotted record
                            for (int ii = 0; ii < iNVars; ii++)
                            {
                                faDataRec[ii] = bnryIn.ReadSingle();
                            }
                            //sw.Write("RunReplay1, 0, {0}", faDataRec[0]);
                            //sw.WriteLine();
                            lBR += iRecSize;
                            iOff = i * (iNumVars + 1);
                            faData[iOff] = faDataRec[0];
                            //sw.Write("RunReplay2, 0, {0}",faData[0]);
                            //sw.WriteLine();
                            for (int j = 0; j < iNumVars; j++)
                            {
                                for (int k = 1; k < iNVars; k++)
                                {
                                    if (k - 1 == iaIndices[j])
                                    {
                                        faData[iOff + j + 1] = faDataRec[k] /*/ faSFs[iaIndices[j]]*/;
                                        //sw.Write("RunReplay3, {0}, {1}, {2}, {3}", j + 1, faData[j + 1], k, faDataRec[k]);
                                        //sw.WriteLine();
                                    }
                                }
                            }
                            //skip non-plotted records
                            if (iNumBytesSkip > 0)
                            {
                                if (fi.Length - lBR <= iNumBytesSkip)
                                    break;
                                bnryIn.BaseStream.Seek(iNumBytesSkip, SeekOrigin.Current);
                            }
                            lBR += iNumBytesSkip;
                        }//for
                        delP(-3, 0, 0);
                        if (fi.Length - lBR < iRecSize)
                            break;
                    }
                }//while
                //close file
                bnryIn.BaseStream.Close();
                bnryIn.Close();
                bnryIn.Dispose();
                delP(-2, 0, 0);
            }
            catch (Exception e)
            {
                this.Invoke(this.delSetStatus, new Object[] { e.Message });
            }
        }

        private void ExecuteMouseClick(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    fTimeStep = Constants.constants.DelT(sModName);
            //    iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
            //    bDisplay = false;
            //    iOrder = (int)(Math.Log10(iNumSteps));
            //    tbarDataScale.Minimum = 0;
            //    tbarDataScale.Maximum = iOrder + 1;
            //    tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
            //    iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
            //    chDataGraph.ChartAreas[0].CursorX.Position = -1000F;
            //    dgResults.Rows.Clear();
            //    bPlotting = true;
            //    Execute();
            //}
            //catch (Exception ee)
            //{
            //}
        }

        private void Execute()
        {
            try 
            {
                labDone.Visible = false;
                switch (iExBtState)
                {
                    case EX_STATE.EBS_EXECUTE:
                        if (!bDisplay)
                        {
                            if (bLive && timer1.Enabled)
                                timer1.Stop();
                            if (tbMaxTime.Text != "")
                            {
                                fMaxTime = Single.Parse(tbMaxTime.Text);
                                iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
                                iOrder = (int)(Math.Log10(iNumSteps));
                                tbarDataScale.Minimum = 0;
                                tbarDataScale.Maximum = iOrder + 1;
                                tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                                iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                            }
                            else
                                fMaxTime = 0;
                            if (fMaxTime <= Constants.constants.fEpsilon)
                            {
                                tbStatus.Text = "Set Max Simulation Time prior to execution";
                                return;
                            }
                            if (bLive && File.Exists(".\\" + sDefFileName))
                            {
                                frmMsgDlg frmMsg = new frmMsgDlg("Default Simulation file exists: OK - delete file and continue, Cancel - Do not continue new simulation");
                                //if (MessageBox.Show("Default Simulation file exists: OK - delete file and continue, Cancel - Do not continue new simulation", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                if (frmMsg.ShowDialog() == DialogResult.OK)
                                        File.Delete(strDataStorePath + "\\" + sDefFileName);
                                else
                                    return;
                            }
                        }
                        if (!ValidateInputs())
                            return;
                        float fMaxSave = fMaxTime;
                        int iIntSize, iNumInts = 1, iMlt = 1;
                        while (fMaxTime < 1)
                        {
                            iMlt *= 10;
                            fMaxTime *= 10;
                        }
                        iIntSize = (int)fMaxTime;
                        //plot time scale calcs
                        while (iNumInts < 4)
                        {
                            if (iIntSize % 2 == 0)
                            {// even, split in half
                                iIntSize = iIntSize / 2;
                                iNumInts *= 2;
                            }
                            else
                            {// odd, check odds
                                for (int ii = 3; ii < 9; ii += 2)
                                {
                                    if (iIntSize % ii == 0)
                                    {// even, split in half
                                        iIntSize = iIntSize / ii;
                                        iNumInts *= ii;
                                        break;
                                    }
                                    if (ii == 7)
                                    {
                                        //2, 3, 4, 5, 6, 7, 8, 9, 10 are all out, so must be a larger odd, probably prime
                                        //convert interval to a larger even interval and start over
                                        iIntSize = (iIntSize / 2 + 1) * 2;
                                        fMaxTime = (float)iIntSize;
                                    }
                                }//for
                            }
                        }//while
                        //sw = new StreamWriter("C:\\SimApp\\outputcs.csv");
                        while ((double)(iIntSize * iNumInts) / iMlt - (2 * fMaxSave) > -1e-6)
                            iMlt *= 2;
                        fMaxTime = (float)(iIntSize * iNumInts)/iMlt;
                        chDataGraph.ChartAreas[0].AxisX.Minimum = 0.0;
                        chDataGraph.ChartAreas[0].AxisX.Maximum = (double)fMaxTime;
                        chDataGraph.ChartAreas[0].AxisX.LabelStyle.Interval = (double)iIntSize/iMlt;
                        chDataGraph.ChartAreas[0].AxisX.MajorGrid.Interval = (double)(2 * iIntSize)/iMlt;
                        //iaCntr = new int[iNumVars];
                        chDataGraph.Series.Clear();
                        GCHandle gch;
                        //bAddResultsHeaders = true;
                        //rbAutoScale.Enabled = false;//freaky behavior - had to disable this button and re-enable below to prevent error from statements in between
                        chkAutoScale.Enabled = false;//freaky behavior - had to disable this button and re-enable below to prevent error from statements in between
                            btPlot.Enabled = false;
                            btData.Enabled = false;
                            btCSV.Enabled = false;
                        chkAutoScale.Enabled = true;
                        //rbAutoScale.Enabled = true;
                        btSaveSim.Enabled = false;
                        if (bLive)
                            tbMaxTime.Enabled = false;
                        else
                            cbReplayList.Enabled = false;
                        bFirstSave = true;
                        if (bLive)
                        {
                            if (bDisplay)
                            {
                                btAbort.Enabled = false;
                                btnExecute.Enabled = false;
                            }
                            else
                            {
                                btAbort.Enabled = true;
                                btnExecute.Enabled = true;
                            }
                        }
                        rbLive.Enabled = false;
                        rbReplay.Enabled = false;
                        if (bLive && !bDisplay)
                        {
                            UpdateParameters();
                            thrProc = new Thread(new ThreadStart(RunSim));
                            iMult = 1;
                            timer1.Interval = 500;
                            timer1.Enabled = true;
                            timer1.Start();
                        }
                        else
                        {
                            thrProc = new Thread(new ThreadStart(RunReplay));
                            iMult = 1;
                        }
                        gch = GCHandle.Alloc(delP);
                        delP = new CallBack(OnPlot);
                        thrCons = new Thread(new ThreadStart(PlotValues));
                        bContinue = true;
                        if (bDelayPlot)
                            chDataGraph.Visible = false;
                        else
                            chDataGraph.Visible = true;
                        thrProc.Start(); 
                        thrCons.Start();
                        gch.Free();
                        iExBtState = EX_STATE.EBS_PAUSE;
                        btnExecute.Text = "Pause";
                        break;
                    case EX_STATE.EBS_PAUSE:
                        iExBtState = EX_STATE.EBS_CONTINUE;
                         btnExecute.Text = "Continue";
                         if (bLive)
                             m1.Continue(false);
                         else
                             bContinue = false;
                       break;
                    case EX_STATE.EBS_CONTINUE:
                       iExBtState = EX_STATE.EBS_PAUSE;
                       btnExecute.Text = "Pause";
                       if (bLive)
                           m1.Continue(true);
                       else
                           bContinue = true;
                       break;
                    case EX_STATE.EBS_ABORT:
                       iExBtState = EX_STATE.EBS_EXECUTE;
                       btnExecute.Text = "Execute";
                       btnExecute.BackColor = clExBt;
                       if (bLive)
                       {
                           thrProc.Abort();
                           thrCons.Abort();
                       }
                       else
                       {
                           bDone = true;
                       }
                       break;
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = ee.Message;
            }
        }

        public void MakeStateLookup()
        {
            //read SimDll13.cpp, Derivatives routine to get state variable names and order in array
            //two passes: first pass just count state vars
            try
            {
                int iStart2, iLen2, iIndex, j = 0, iPos, iPos2, iPos3, iArrSz = 0;
                string strBuffer;
                string strFileName = ".\\"+sModName+".h";
                StreamReader strReader = new StreamReader(strFileName);
                for (int i = 0; i < 2; i++)
                {
                    iIndex = 0;
                    while (!strReader.EndOfStream)
                    {
                        strBuffer = strReader.ReadLine();
                        if (strBuffer.Contains("//STATE_VAR") || strBuffer.Contains("//NS_VAR"))
                        {
                            iPos = strBuffer.IndexOf("];");
                            if(iPos >= 0)
                            {
                                iPos2 = strBuffer.IndexOf("[");
                                iArrSz = Int32.Parse(strBuffer.Substring(iPos2 + 1, iPos - iPos2 - 1));
                            }
                            else
                                iArrSz = 0;
                            if(i == 0)
                                iIndex += ((iArrSz > 0) ? iArrSz : 1);   //just count SD items on first pass
                            else // i == 1
                            {   //second pass, so acquire sd names
                                VarInfo varx = (VarInfo)(straSDNames.GetValue(iIndex));

                                iStart2 = strBuffer.IndexOf("SIM_DATA ");
                                if (iStart2 >= 0)
                                    iStart2 = iStart2 + 9;
                                else
                                {
                                    iStart2 = strBuffer.IndexOf("SIM_DATA* ");
                                    if (iStart2 >= 0)
                                        iStart2 = iStart2 + 10;
                                }
                                iLen2 = strBuffer.IndexOf(';') - iStart2;
                                varx.sVarName = strBuffer.Substring(iStart2, iLen2);
                                varx.sVarName = varx.sVarName.Replace("*", "");
                                varx.sVarName = varx.sVarName.Replace("m_sd", "");

                                //iPos = varx.sVarName.IndexOf("[");
                                //if (iPos >= 0)
                                //{
                                //    iPos2 = varx.sVarName.IndexOf("]", iPos + 1);
                                //    if (iPos2 >= 0)
                                //    {
                                //        iArrSz = Int32.Parse(varx.sVarName.Substring(iPos + 1, iPos2 - iPos - 1));
                                //        varx.sVarName = varx.sVarName.Substring(0, iPos);
                                //    }

                                //}
                                //else
                                //    iArrSz = 0;
                                varx.iArraySize = iArrSz;
                                iPos3 = strBuffer.IndexOf(";");
                                iPos = strBuffer.IndexOf('[', iPos3+1);
                                if (iPos >= 0)
                                {
                                    iPos2 = strBuffer.IndexOf(']',iPos+1);
                                    if (iPos2 >= 0)
                                    {
                                        varx.sName = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                        strBuffer = strBuffer.Substring(iPos2 + 1);
                                    }
                                    iPos = strBuffer.IndexOf('[');
                                    if (iPos >= 0)
                                    {
                                        iPos2 = strBuffer.IndexOf(']');
                                        if (iPos2 >= 0)
                                        {
                                            varx.sGroup = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                            strBuffer = strBuffer.Substring(iPos2 + 1);
                                            if(varx.sGroup.Contains('$'))
                                            {
                                                varx.sGroup = varx.sGroup.Replace("$", "");
                                                listNoPlotItems.Add(varx.sGroup);
                                            }
                                        }
                                    }
                                    iPos = strBuffer.IndexOf('[');
                                    if (iPos >= 0)
                                    {
                                        iPos2 = strBuffer.IndexOf(']');
                                        if (iPos2 >= 0)
                                            varx.sUnits = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                    }
                                    if (iArrSz == 0)
                                    {
                                        straSDNames.SetValue(varx, iIndex);
                                        iIndex++;
                                    }
                                    else
                                    {
                                        string snm = varx.sName;
                                        string svnm = varx.sVarName;
                                        for (int ii = 1; ii <= iArrSz; ii++)
                                        {
                                            string str = string.Format("[{0}]", ii);
                                            varx.sName = snm + str;
                                            varx.sVarName = svnm + str;
                                            straSDNames.SetValue(varx, iIndex++);
                                        }
                                    }
                                }
                            }
                        }
                        else if (strBuffer.Contains("//CONST"))
                        {
                            if (i == 1)
                            {   //second pass, so acquire sd names
                                iStart2 = strBuffer.IndexOf("SIM_DATA ");
                                if (iStart2 >= 0)
                                    iStart2 = iStart2 + 9;
                                iLen2 = strBuffer.IndexOf(';') - iStart2;
                                ciaInfo[j].sVarName = strBuffer.Substring(iStart2, iLen2);
                                ciaInfo[j].sVarName = ciaInfo[j].sVarName.Replace("m_sd", "");
                                iPos = ciaInfo[j].sVarName.IndexOf("[");
                                iArrSz = 0;
                                if (iPos >= 0)
                                {
                                    iPos2 = ciaInfo[j].sVarName.IndexOf("]", iPos);
                                    if (iPos2 >= 0)
                                    {
                                        iArrSz = Int32.Parse(ciaInfo[j].sVarName.Substring(iPos + 1, iPos2 - iPos - 1));
                                        ciaInfo[j].sVarName = ciaInfo[j].sVarName.Substring(0, iPos);
                                    }
                                }
                                ciaInfo[j].iArraySize = iArrSz;
                                iPos3 = strBuffer.IndexOf(";");
                                iPos = strBuffer.IndexOf('[',iPos3+1);
                                if (iPos >= 0)
                                {
                                    iPos2 = strBuffer.IndexOf(']',iPos+1);
                                    if (iPos2 >= 0)
                                    {
                                        ciaInfo[j].sName = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                        strBuffer = strBuffer.Substring(iPos2 + 1);
                                    }
                                    iPos = strBuffer.IndexOf('[');
                                    if (iPos >= 0)
                                    {
                                        iPos2 = strBuffer.IndexOf(']');
                                        if (iPos2 >= 0)
                                        {
                                            ciaInfo[j].sType = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                            strBuffer = strBuffer.Substring(iPos2 + 1);
                                        }
                                    }
                                    iPos = strBuffer.IndexOf('[');
                                    if (iPos >= 0)
                                    {
                                        iPos2 = strBuffer.IndexOf(']');
                                        if (iPos2 >= 0)
                                            ciaInfo[j].sUnits = strBuffer.Substring(iPos + 1, iPos2 - iPos - 1);
                                    }
                                    string s = ciaInfo[j].sName;
                                    for (int k = 0; k < iArrSz; k++ )
                                        ciaInfo[j + k].Set(ciaInfo[j], s, k+1);
                                    j += (iArrSz>0?iArrSz:1);
                                }
                            }
                        }//else CONST
                    }//while
                    if(i==0)
                    {   //first pass so create array
                        straSDNames = Array.CreateInstance(typeof(VarInfo), iIndex);
                        strReader.Close();
                        strReader = new StreamReader(strFileName);
                    }
                }//for
                strReader.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void MaxTimeTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbMaxTime.Text != "")
                    fMaxTime = Single.Parse(tbMaxTime.Text);
                else
                    fMaxTime = 0.0F;
                fTimeStep = Constants.constants.DelT(sModName);
                iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
                iOrder = (int)(Math.Log10(iNumSteps));
                tbarDataScale.Minimum = 0;
                tbarDataScale.Maximum = iOrder + 1;
                tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                if (bLive)
                    btnExecute.Enabled = true;
            }
            catch (Exception ee)
            {
            }
        }

        private int IndexOfName(string strName)
        {
            int  iDex=0;
            foreach (VarInfo v in straSDNames)
            {
                if (strName == v.sName)
                    return iDex;
                iDex++;
            }
            return -1;
        }

        private int IndexOfConst(string strName)
        {
            foreach (InitCondInfo ic in aICs)
            {
                if (strName == ic.strLabel)
                    return ic.iIndex;
            }
            return -1;
        }

        private bool ValidateInputs()
        {
            string strSummary, strName, strNames="";
            bool bRetVal = true;
            if (fMaxTime < Constants.constants.fEpsilon)
            {
                strSummary = "Set the Maximum time";
                bRetVal = false;
            }
            else
            {
                iNumVars = lbItemsToPlot.Items.Count;
                if (bPlotting && iNumVars > 10)
                {
                    iNumVars = 10;
                    strSummary = "More than 10 plot variables selected, only first 10 will be plotted";
                }
                if (iNumVars == 0)
                {
                    strSummary = "Select state variables to plot";
                    bRetVal = false;
                }
                else
                {
                    if (bLoadGroup && listNoPlotItems.Contains(lbAllPlotItems.SelectedItem.ToString()))
                    {
                        tbStatus.Text = "Selected Group name is not plottable - select another Group";
                        return false;
                    }
                    iaIndices = new int[iNumVars];
                    sLabels = new string[iNumVars];
                    for (int i = 0; i < iNumVars; i++)
                    {
                        strName = lbItemsToPlot.Items[i].ToString();
                        iaIndices[i] = IndexOfName(strName);
                        sLabels[i] = strName;
                        strNames += strName;
                        if (i < iNumVars - 1)
                            strNames += ", ";
                    }
                    strSummary = string.Format("{0} will be plotted", strNames);//
                    iOrder = (int)(Math.Log10(iNumSteps));
                    tbarDataScale.Minimum = 0;
                    tbarDataScale.Maximum = iOrder + 1;
                    tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                    iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                }
            }
            tbStatus.Text = strSummary;
            return bRetVal;
        }

        public delegate void SetStatus(string strMess);
        public SetStatus delSetStatus;

        public void SetStatusMessage(string strMess)
        {
            tbStatus.Text = strMess;
        }

        public delegate void UpdateGraph0(PlotEventArgs ee);
        public UpdateGraph0 delUpdateGraph0;

        public void UpdateGraphMethod0(PlotEventArgs ee)
        {
            try
            {
                if (ee.iDataNum < 0)
                {
                    //special plot command
                    switch (-ee.iDataNum)
                    {
                        case 1: //clear all existing data from plot
                            chDataGraph.Series.Clear();
                            break;
                        case 2:
                            //bDone = true;
                            iExBtState = EX_STATE.EBS_EXECUTE;
                            btnExecute.Text = "Execute";
                            btnExecute.BackColor = clExBt;
                            chDataGraph.Series.ResumeUpdates();
                            labDone.Visible = true;
                            btPlot.Enabled = true;
                            btData.Enabled = true;
                            btCSV.Enabled = true;
                            if (bLive)
                                tbMaxTime.Enabled = true;
                            else
                                cbReplayList.Enabled = true;
                            if(bLive && File.Exists(".\\"+sDefFileName))
                                btSaveSim.Enabled = true;
                            if (bLive && bDisplay)
                                btnExecute.Enabled = true;
                            btAbort.Enabled = false;
                            rbLive.Enabled = true;
                            rbReplay.Enabled = true;
                            bDisplay = false;
                            if (bDelayPlot)
                                chDataGraph.Visible = true;
                            labRunning.Visible = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                tbStatus.Text = e.Message;
            }
        }

        public delegate void UpdateGraph(int iCnt, PlotEventArgs[] aee);
        public UpdateGraph delUpdateGraph;

        public void UpdateGraphMethod(int iCnt, PlotEventArgs[] aee)
        {
            int iSerNum;
            float fX, fY;
            try
            {
                bool bAbort = true;
                if (iSRcnt<0)
                    chDataGraph.Series.SuspendUpdates();
                iSRcnt++;
                if (iSRcnt == 10)
                {
                    chDataGraph.Series.ResumeUpdates();
                    chDataGraph.Series.SuspendUpdates();
                    iSRcnt = 0;
                }
                if (aee != null)
                {
                    for (int k = 0; k < iCnt; k++)
                    {
                        iSerNum = aee[k].iDataNum;
                        fX = aee[k].dX;
                        fY = aee[k].dY;
                        bAbort = float.IsNaN(fY) || float.IsInfinity(fY);  
                        if (bAbort)
                            break;
                        if (iSerNum < 0)
                        {
                            //special plot command
                            switch (-iSerNum)
                            {
                                case 1: //clear all existing data from plot
                                    chDataGraph.Series.Clear();
                                    break;
                                case 2:
                                    break;
                            }
                        }
                        else
                        {   // normal plot data command
                            //plot x in control
                            if (iSerNum == chDataGraph.Series.Count)
                            {
                                int iStrLen, iGrp, iItem, iSz = sLabels.GetLength(0);
                                chDataGraph.Series.Add("Series" + (iSerNum + 1).ToString());
                                chDataGraph.Series[iSerNum].ChartType = chartType;
                                if (iSz > 0)
                                {
                                    iGrp = iSerNum / iSz + 1;
                                    iItem = iSerNum % iSz;
                                    iStrLen = sLabels[iItem].Length;
                                    iStrLen = (iStrLen > 20) ? 20 : iStrLen;
                                    chDataGraph.Series[iSerNum].LegendText = sLabels[iItem].Substring(0, iStrLen);// +" " + iGrp.ToString();
                                }
                            }
                            if (chDataGraph.Series.Count > iSerNum)
                            {
                                if (bAutoScale)
                                {
                                    AutoYScale(fY);
                                    //if (AutoYScale(fY))
                                    //{
                                    //    iaCntr[k]++;
                                    //    if (iaCntr[k] > 30)
                                    //    {
                                    //        rbAutoScale.Checked = false;
                                    //        ResetYScale();
                                    //    }
                                    //}
                                }
                                //else
                                //{
                                //    iaCntr[k] = 0;
                                //}
                                fY = (fY > 6.0e28F) ? 6.0e28F : fY;
                                fY = (fY < -6.0e28F) ? -6.0e28F : fY;

                                chDataGraph.Series[iSerNum].Points.AddXY((double)fX, (double)fY);
                            }
                        }
                    }//for

                    if (bAbort)
                    {
                        strMsg = "NaN results have forced simulation to abort";
                        AbortMouseClick(this, null);
                        iRetryCnt++;
                        if (iRetryCnt < 4)
                        {
                            fTimeStep /= 2;
                            string str = string.Format("NaN results, - reducing time step ({0})", fTimeStep);
                            iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
                            iOrder = (int)(Math.Log10(iNumSteps));
                            tbarDataScale.Minimum = 0;
                            tbarDataScale.Maximum = iOrder + 1;
                            tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                            iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                            m1.CloseFile();
                            MessageBox.Show(str);
                            File.Delete(".\\" + sDefFileName);
                            ResetYScale();
                            Execute();
                        }
                        else
                        {
                            fTimeStep = Constants.constants.DelT(sModName);
                            iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
                            iOrder = (int)(Math.Log10(iNumSteps));
                            tbarDataScale.Minimum = 0;
                            tbarDataScale.Maximum = iOrder + 1;
                            tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                            iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                            iRetryCnt = 0;
                        }

                    }
                }
                //iCycles++;
                //if (iCycles % 10 == 0)
                //    labRunning.Visible = !labRunning.Visible;
            }
            catch (Exception e)
            {
                tbStatus.Text = e.Message;
            }
        }

        private void AbortMouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (iExBtState == EX_STATE.EBS_CONTINUE || iExBtState == EX_STATE.EBS_PAUSE)
                {
                    if (strMsg == "")
                    {
                        strMsg = "User elected to abort the simulation";
                        if (MessageBox.Show(string.Format("{0}: OK - abort simulation, Cancel - continue with simulation", strMsg), "Abort Simulation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            strMsg = "";
                            return;
                        }
                    }
                    strMsg = "";
                    iExBtState = EX_STATE.EBS_EXECUTE;
                    btnExecute.Text = "Execute";
                    thrProc.Abort();
                    thrCons.Abort();
                    Thread.Sleep(500);
                    aee = null;
                    readerFlag = false;
                    if (bLive)
                        m1.CloseFile();
                    else
                    {
                        bnryIn.BaseStream.Close();
                        bnryIn.Close();
                        bnryIn.Dispose();
                    }
                    btData.Enabled = true;
                    btCSV.Enabled = true;
                    btPlot.Enabled = true;
                    if(bAutoScale)
                        //rbAutoScale.Checked = false;
                        chkAutoScale.Checked = false;
                    btAbort.Enabled = false;
                    if (bLive)
                        tbMaxTime.Enabled = true;
                    else
                        cbReplayList.Enabled = true;


                    chDataGraph.Series.ResumeUpdates();
                    //if (bLive)
                    tbMaxTime.Enabled = true;
                    btAbort.Enabled = false;
                    rbLive.Enabled = true;
                    rbReplay.Enabled = true;
                    bDisplay = false;
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = ee.Message;
            }
        }

        private void UpdateParameters()
        {
            int i2, iHiRow, iSum, j=-1;
            iaInitInds = new int[iNumIC];
            daInitVals = new double[iNumIC];
            iHiRow = 0;// aIcDgs[0].Rows.Count;//dg1.Rows.Count;
            iSum = iHiRow;
            for (int i = 0; i < iNumIC; i++)
            {
                //if (i < dg1.Rows.Count)
                //{
                //    string s = dg1.Rows[i].Cells[0].Value.ToString();
                //    iaInitInds[i] = IndexOfConst(dg1.Rows[i].Cells[0].Value.ToString());// cbStateVarList.Items.IndexOf(dg1.Rows[i].Cells[0].Value.ToString());
                //    daInitVals[i] = Double.Parse(dg1.Rows[i].Cells[1].Value.ToString());
                //}
                //else
                //{
                    if (i == iHiRow)
                    {
                        iSum = iHiRow;
                        j++;
                        iHiRow += aIcDgs[j].Rows.Count;
                    }
                    if (i < iHiRow)
                    {
                        i2 = i - iSum;
                        iaInitInds[i] = IndexOfConst(aIcDgs[j].Rows[i2].Cells[0].Value.ToString());
                        daInitVals[i] = Double.Parse(aIcDgs[j].Rows[i2].Cells[1].Value.ToString());
                    }
                //}
            }
        }

        private void OverWriteParameters()
        {
            int i2, iHiRow, iSum, j = -1;
            iHiRow = aIcDgs[0].Rows.Count;//dg1.Rows.Count;
            iSum = iHiRow;
            for (int i = 0; i < iNumIC; i++)
            {
                //if (i < dg1.Rows.Count)
                //    dg1.Rows[i].Cells[1].Value = daInitVals[i].ToString();
                //else
                //{
                    if (i == iHiRow)
                    {
                        iSum = iHiRow;
                        j++;
                        iHiRow += aIcDgs[j].Rows.Count;
                    }
                    if (i < iHiRow)
                    {
                        i2 = i - iSum;
                        aIcDgs[j].Rows[i2].Cells[1].Value = daInitVals[i].ToString();
                    }
                //}
            }
        }

        private void ReplayListSelIndChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbReplayList.Items.Count <= 0 || cbReplayList.SelectedIndex<0)
                    return;
                cbReplayList.Select(cbReplayList.SelectedIndex, 1);
                strFilePath = cbReplayList.SelectedItem.ToString();
                bLive = false;
                int iNVars;
                bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
                //read Header, mainly SFs
                iNumSteps = bnryIn.ReadInt32();
                iNVars = bnryIn.ReadInt32();    //not used here, only for reading rest of file
                bnryIn.ReadInt32();    //numIC
                iFreq = bnryIn.ReadInt32();
                fTimeStep = bnryIn.ReadSingle();
                fMaxTime = bnryIn.ReadSingle();
                //read in ICs
                for (int i = 0; i < iNumIC; i++)
                {
                    int ii = bnryIn.ReadInt32();    //may want info in temp array
                    foreach (InitCondInfo ici in aICs)
                    {
                        if (ici.iIndex == ii)
                            ici.dValue = bnryIn.ReadDouble();//may want info in temp array
                    }
                }
                InitTabControl();
                bnryIn.BaseStream.Close();
                bnryIn.Close();
                bnryIn.Dispose();
                tbMaxTime.Text = fMaxTime.ToString();
                iOrder = (int)(Math.Log10(iNumSteps));
                tbarDataScale.Minimum = 0;
                tbarDataScale.Maximum = iOrder + 1;
                tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                bFirstSave = true;
                btPlot.Enabled = true;
                btData.Enabled = true;
                btCSV.Enabled = true;
                chDataGraph.ChartAreas[0].CursorX.Position = -1000F;
                chDataGraph.Series.Clear();
                dgResults.Rows.Clear();
                lbAllPlotItems.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                tbStatus.Text = ee.Message;
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmAboutSUPO formAbout = new frmAboutSUPO(sModName);
                DialogResult dr = formAbout.ShowDialog(this);
            }
            catch (Exception ee)
            {
            }
        }

        void ToggleVisibility(bool bShowSimControl)
        {
            pnlInitVals.Visible = bShowSimControl;
            labInitVals.Visible = bShowSimControl;
            tcInit.Visible = bShowSimControl;
            //dg1.Visible = bShowSimControl;
            foreach (DataGridView dgv in aIcDgs)
                if(dgv!=null)
                    dgv.Visible = bShowSimControl;

            dgResults.Visible = !bShowSimControl;
            dgResults.BringToFront();
        }

        private void ClickSimControl(object sender, EventArgs e)
        {
            try
            {
                ToggleVisibility(true);
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickDB(object sender, EventArgs e)
        {
            try
            {
                ToggleVisibility(false);
                if (WindowState == FormWindowState.Normal)
                {
                    if (dgResults.Top > 400)
                    {
                        //pnlDB.Top -= 456;
                        dgResults.Top -= 456;
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void OnTime(Object o, EventArgs e)
        {
            try
            {
                timer.Stop();
                //StreamWriter strmW = new StreamWriter("TimeOut.csv", false);
                //string strOut;
                //swTimer.Start();
                //strOut = string.Format("A,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);
                int ii, jj;
                //int[] iaTimes = new int[5];
                //iaTimes[0] = iaTimes[1] = iaTimes[2] = iaTimes[3] = iaTimes[4] = 0;
                int[] iaDbIndices = new int[lbItemsToPlot.Items.Count + 1];
                jj = 1;
                iaDbIndices[0] = 0; //time at index 0
                foreach (object oItem in lbItemsToPlot.Items)
                {
                    ii = 1;
                    foreach (VarInfo v in straSDNames)
                    {
                        if (oItem.ToString() == v.sName)
                        {
                            iaDbIndices[jj++] = ii;
                            break;
                        }
                        ii++;
                    }
                }
                float fTmp;//fSF, 
                long lSum;
                FileInfo fi = new FileInfo(strFilePath);            //open file, skip header stuff
                bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
                lSum = (long)(24 + iNumIC * 12 + 512);
                bnryIn.BaseStream.Seek(lSum, SeekOrigin.Begin);

                dgResults.Columns.Clear();
                dgResults.Rows.Clear(); //make starts empty                                
                dgResults.Columns.Add("colTime", "Time (s)");
                string strHeader;

                for (int j = 1; j < iaDbIndices.Length; j++)
                {
                    strHeader = String.Format("{0}", lbItemsToPlot.Items[j - 1].ToString());// (sf={1:G}), fSF
                    dgResults.Columns.Add("col" + j.ToString(), strHeader);
                }
                int iRow = 0;
                bool bShowRow;
                long lOff, lRowOff = 4 * (straSDNames.Length + 1);
                long lTest = fi.Length - lRowOff;
                lOff = 0;
                //strOut = string.Format("B:,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);
                while (lSum < fi.Length)
                {
                    //iaTimes[0] -= (int)(swTimer.ElapsedMilliseconds);
                    bShowRow = false;
                    if (iSkip >= 0) //show nth row if filtered
                        bShowRow = (iRow % iSkip) == 0;
                    if (lSum >= lTest)  //always show final row
                        bShowRow = true;

                    //iaTimes[0] += (int)(swTimer.ElapsedMilliseconds);
                    if (bShowRow)
                    {
                        //iaTimes[1] -= (int)(swTimer.ElapsedMilliseconds);
                        if (lOff > 0)
                        {
                            bnryIn.BaseStream.Seek(lOff, SeekOrigin.Current);
                            lOff = 0;
                        }
                        fTmp = bnryIn.ReadSingle();
                        //iaTimes[1] += (int)(swTimer.ElapsedMilliseconds);
                        //iaTimes[4] -= (int)(swTimer.ElapsedMilliseconds);
                        iResultRow = dgResults.Rows.Add();
                        dgResults.Rows[iResultRow].Cells[0].Value = fTmp.ToString();
                        int j = 1, iI;
                        //iaTimes[4] += (int)(swTimer.ElapsedMilliseconds);
                        //iaTimes[2] -= (int)(swTimer.ElapsedMilliseconds);
                        int iSk = 0, iPrevIndex = 0;
                        for (int i = 1; i < iaDbIndices.Length; i++)
                        {
                            iI = iaDbIndices[i];
                            //if (iI > iPrevIndex)
                            //    iSk = (iI - iPrevIndex - 1) * 4;    //go forward
                            //else
                            //    iSk = (iPrevIndex - iI + 1) * -4;   //go back
                            iSk = (iI - iPrevIndex - 1) * 4;    //go either way
                            if (iSk != 0)
                                bnryIn.BaseStream.Seek(iSk, SeekOrigin.Current);
                            iPrevIndex = iI;
                            fTmp = bnryIn.ReadSingle();
                            dgResults.Rows[iResultRow].Cells[j++].Value = fTmp.ToString();
                        }//for
                        iSk = (straSDNames.Length - iPrevIndex) * 4;
                        bnryIn.BaseStream.Seek(iSk, SeekOrigin.Current);    //get to start of next row
                        //iaTimes[2] += (int)(swTimer.ElapsedMilliseconds);
                    }//if
                    else
                        lOff += lRowOff;
                    //iaTimes[3] -= (int)(swTimer.ElapsedMilliseconds);
                    iRow++;
                    lSum += lRowOff;
                    //iaTimes[3] += (int)(swTimer.ElapsedMilliseconds);
                }//while
                //strOut = string.Format("C:,{0},{1},{2},{3}", swTimer.ElapsedMilliseconds, iaTimes[1], iaTimes[2], iaTimes[4]);
                // strOut = string.Format("C:,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);

                //swTimer.Stop();
                //strmW.Close();
                bnryIn.BaseStream.Close();
                bnryIn.Close();
                bnryIn.Dispose();
                UseWaitCursor = false;
                foreach (DataGridViewColumn dgvc in dgResults.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                bDisplay = false;
            }
            catch(Exception ee)
            {

            }
        }

        private void DataScaleValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iVal = tbarDataScale.Value;
            
                if (iVal == tbarDataScale.Maximum)
                    iSkip = -1;
                else
                    iSkip = (int)(Math.Pow(10, iVal));
            }
            catch (Exception ee)
            {
            }
        }

        private void LoadPlotItems()
        {
            string s;
            lbAllPlotItems.Items.Clear();
            if (bLoadGroup)
                lbItemsToPlot.Items.Clear();
            foreach(VarInfo v in straSDNames)
            {
                s = "";
                if (bLoadGroup)
                {
                    if (v.sGroup != "User" && !lbAllPlotItems.Items.Contains(v.sGroup))
                        s = v.sGroup;
                }
                else if (sPlotFilterStr == null || sPlotFilterStr=="")
                    s = v.sName;
                else if(v.sName.IndexOf(sPlotFilterStr) >= 0)
                    s = v.sName;
                if (s != "")
                {
                    lbAllPlotItems.Items.Add(s);
                }
            }
        }

        private void AllPlotItemsSelIndChanged(object sender, EventArgs e)
        {
            string s, strSelItem;
            try
            {
                if (lbAllPlotItems.SelectedIndex >= 0)
                {
                    strSelItem = lbAllPlotItems.SelectedItem.ToString();
                    if (bLoadGroup)
                    {
                        lbItemsToPlot.Items.Clear();
                        foreach (VarInfo v in straSDNames)
                        {
                            s = v.sGroup;
                            if (s == strSelItem)
                                lbItemsToPlot.Items.Add(v.sName);
                        }
                        btPlot.Enabled = !listNoPlotItems.Contains(strSelItem);
                    }
                    else
                    {
                        btPlot.Enabled = true;
                        foreach (VarInfo v in straSDNames)
                        {
                            s = v.sName;
                            if (s == strSelItem && !lbItemsToPlot.Items.Contains(s))
                            {
                                lbItemsToPlot.Items.Add(s);
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = ee.Message;
            }
        }

        private void ItemsToPlotDoubleClick(object sender, EventArgs e)
        {
            try
            {
                lbItemsToPlot.Items.Remove(lbItemsToPlot.SelectedItem);
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickResetICs(object sender, EventArgs e)
        {
            try
            {
                SetICs();
            }
            catch (Exception ee)
            {
            }
        }

        private void TextChangedPlotFilterStr(object sender, EventArgs e)
        {
            try
            { 
                sPlotFilterStr = tbPlotFilterStr.Text;
                LoadPlotItems();
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickMenuAllData(object sender, EventArgs e)
        {
            try
            { 
                labPlotFilterStr.Visible = true;
                tbPlotFilterStr.Visible = true;
                tbPlotFilterStr.Enabled = true;
                bLoadGroup = false;
                btClearToPlot.Visible = true;
                labPlotDisplayGroups.Text = "Model Variables";
                LoadPlotItems();
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickMenuPlotGroups(object sender, EventArgs e)
        {
            try
            {
                labPlotFilterStr.Visible = false;
                tbPlotFilterStr.Visible = false;
                tbPlotFilterStr.Enabled = false;
                bLoadGroup = true;
                btClearToPlot.Visible = false;
                labPlotDisplayGroups.Text = "Plot Display Groups";
                LoadPlotItems();
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickPlot(object sender, EventArgs e)
        {
            try
            {
                bDisplay = true;
                bPlotting = true;
                if (bDelayPlot)
                {
                    bDelayPlot = false;
                    chkDelay.Checked = false;
                }
                Execute();
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickData(object sender, EventArgs e)
        {
            try
            {
                if (bDelayPlot)
                {
                    bDelayPlot = false;
                    chkDelay.Checked = false;
                }
                //Cursor = new System.Windows.Forms.Cursor(GetType(), Cursors.WaitCursor.ToString());
                bPlotting = false;
                if (lbItemsToPlot.Items.Count == 0)
                {
                    tbStatus.Text = "Select state variables to plot";
                    return;
                }
                iSyncRowNum = -1;
                ClickDB(this, null);
                UseWaitCursor = true;
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 1;
                timer.Tick += new EventHandler(OnTime);
                timer.Start();
                bDisplay = true;
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickScale(object sender, EventArgs e)
        {
            try
            {
                ScaleVals dlgScalVals;
                double[] daScaleValues;
                if (eScaleType == ScaleVals.ScaleType.None)
                {
                    daScaleValues = new double[8];
                    daScaleValues[0] = chDataGraph.ChartAreas[0].AxisY.Minimum;
                    daScaleValues[1] = chDataGraph.ChartAreas[0].AxisY.Maximum;
                    daScaleValues[2] = chDataGraph.ChartAreas[0].AxisY.LabelStyle.Interval;
                    daScaleValues[3] = chDataGraph.ChartAreas[0].AxisY.MajorGrid.Interval;
                    daScaleValues[4] = chDataGraph.ChartAreas[0].AxisX.Minimum;
                    daScaleValues[5] = chDataGraph.ChartAreas[0].AxisX.Maximum;
                    daScaleValues[6] = chDataGraph.ChartAreas[0].AxisX.LabelStyle.Interval;
                    daScaleValues[7] = chDataGraph.ChartAreas[0].AxisX.MajorGrid.Interval;
                    dlgScalVals = new ScaleVals(daScaleValues, eScaleType);
                    if (dlgScalVals.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        chDataGraph.ChartAreas[0].AxisY.Minimum = daScaleValues[0];
                        chDataGraph.ChartAreas[0].AxisY.Maximum = daScaleValues[1];
                        chDataGraph.ChartAreas[0].AxisY.LabelStyle.Interval = daScaleValues[2];
                        chDataGraph.ChartAreas[0].AxisY.MajorGrid.Interval = daScaleValues[3];
                        chDataGraph.ChartAreas[0].AxisX.Minimum = daScaleValues[4];
                        chDataGraph.ChartAreas[0].AxisX.Maximum = daScaleValues[5];
                        chDataGraph.ChartAreas[0].AxisX.LabelStyle.Interval = daScaleValues[6];
                        chDataGraph.ChartAreas[0].AxisX.MajorGrid.Interval = daScaleValues[7];
                    }
                }
                else
                {
                    int i = 0;
                    daScaleValues = new double[39];
                    foreach (Double d in daStVals)
                        daScaleValues[i++] = d;
                    dlgScalVals = new ScaleVals(daScaleValues, eScaleType);
                    if (dlgScalVals.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        i = 0;
                        foreach (Double d in daScaleValues)
                            daStVals[i++] = d;
                        SaveStabPlotValues(sModName, false);
                        CallStabCalcs();
                    }
                }

                if (bAutoScale)
                {
                    //rbAutoScale.Checked = false;
                    chkAutoScale.Checked = false;
                }
            }
            catch (Exception ee)
            {
            }
        }

        private bool AutoYScale(float fVal)
        {
            float fmin = (float)chDataGraph.ChartAreas[0].AxisY.Minimum;
            float fmax = (float)chDataGraph.ChartAreas[0].AxisY.Maximum;
            float fLI = (float)chDataGraph.ChartAreas[0].AxisY.LabelStyle.Interval;
            float fMGI = (float)chDataGraph.ChartAreas[0].AxisY.MajorGrid.Interval;
            if (fVal >= fmin && fVal <= fmax)
                return false;
            //out of bounds
            if (fVal < fmin)
                fmin -= fMGI;
            if (fVal > fmax)
                fmax += fMGI;
            if (Math.Abs(Math.Abs(fmax - fmin) / fMGI - 10) < 0.00001F)
            {
                fMGI *= 2;
                fLI *= 2;
            }
            chDataGraph.ChartAreas[0].AxisY.Minimum = fmin;
            chDataGraph.ChartAreas[0].AxisY.Maximum = fmax;
            chDataGraph.ChartAreas[0].AxisY.LabelStyle.Interval = fLI;
            chDataGraph.ChartAreas[0].AxisY.MajorGrid.Interval = fMGI;
            return true;
        }

        private void PushAutoScale(object sender, EventArgs e)
        {
            try
            {
                string str;
                if (e.GetType().ToString() == "MouseEventArgs")
                {
                    str = string.Format("{0} {1} {2} {3} {4}", ((MouseEventArgs)(e)).X, ((MouseEventArgs)(e)).Y, ((MouseEventArgs)(e)).Clicks, ((MouseEventArgs)(e)).Delta, ((MouseEventArgs)(e)).Button.ToString());
                }
                else
                {
                    str = string.Format("{0} {1}",e.ToString(),e.GetType().ToString());
                }
                MessageBox.Show(str);
                //rbAutoScale.Checked = !rbAutoScale.Checked;
                chkAutoScale.Checked = !chkAutoScale.Checked;
                //bResetAuto = false;
            }
            catch (Exception ee)
            {
            }
        }

        private void CheckChangedAutoScale(object sender, EventArgs e)
        {
            //try
            //{
            //    if (rbAutoScale.Checked)
            //    {
            //        MessageBox.Show("1");
            //        ResetYScale();
            //        clrSaveASFColor = rbAutoScale.ForeColor;
            //        clrSaveASBColor = rbAutoScale.BackColor;
            //        rbAutoScale.ForeColor = Color.Black;
            //        rbAutoScale.BackColor = Color.LightGray;
            //        bAutoScale = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("2");
            //        rbAutoScale.ForeColor = clrSaveASFColor;
            //        rbAutoScale.BackColor = clrSaveASBColor;
            //        bAutoScale = false;
            //    }
            //}
            //catch (Exception ee)
            //{
            //}
        }

        private void ClickSaveSim(object sender, EventArgs e)
        {
            int iPos, iLen;
            string sMemo, sDefFilePath;
            try
            {
                frmSpecName formName = new frmSpecName(strDataStorePath,true);
                formName.Location = Location;
                if (formName.ShowDialog() == DialogResult.OK)
                {
                    strFilePath = formName.FileName();
                    sMemo = formName.Memo();
                    iPos = strFilePath.IndexOf(".bin");
                    if(iPos<0)
                        iPos = strFilePath.IndexOf(".BIN");
                    if (iPos >= 0)
                    {
                        if (iPos + 4 == strFilePath.Length)
                        {
                            //".bin" at end of file, so suffix
                        }
                        else
                            iPos = -1;
                    }
                    if (iPos < 0)  //append .bin
                        strFilePath += ".bin";
                    sDefFilePath = ".\\" + sDefFileName;
                    if (sMemo != "")
                    {
                        //write memo to header
                        BinaryWriter bnryOut = new BinaryWriter(File.Open(sDefFilePath, FileMode.Open));
                        bnryOut.BaseStream.Seek(24 + iNumIC * 12, SeekOrigin.Begin);
                        iLen = (sMemo.Length > 512) ? 512 : sMemo.Length;
                        for (int i = iLen; i < 512; i++)
                            sMemo += " ";
                        bnryOut.Write(sMemo);
                        bnryOut.Close();
                        Thread.Sleep(500);
                    }
                    Directory.CreateDirectory(strDataStorePath);
                    Directory.Move(sDefFilePath, strFilePath);//".\\__SimResults__Default.bin"
                    btSaveSim.Enabled = false;  //once moved, can't be re-done, so disable

                }
                else
                {
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickReplay(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickLive(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ee)
            {
            }
        }

        private void CheckChangedLive(object sender, EventArgs e)
        {
            try
            { 
                if (rbLive.Checked)
                {
                    tbMaxTime.ReadOnly = false;
                    cbReplayList.Enabled = false;
                    bLive = true;
                    fTimeStep = Constants.constants.DelT(sModName);
                    tbMaxTime.Text = "";
                    fMaxTime = 0F;
                    strFilePath = ".\\" + sDefFileName;
                    cbReplayList.Text = "";
                    cbReplayList.SelectedIndex = -1;
                    bFirstSave = true;
                    rbLive.ForeColor = Color.Black;
                    rbLive.BackColor = Color.LightGray;
                    btnExecute.Enabled = false;
                    btPlot.Enabled = false;
                    btData.Enabled = false;
                    btCSV.Enabled = false;
                    btSaveSim.Enabled = false;
                    chDataGraph.ChartAreas[0].CursorX.Position = -1000F;
                    chDataGraph.Series.Clear();
                    dgResults.Rows.Clear();
                    lbAllPlotItems.SelectedIndex = 0;
                    SetICs();
                    ToggleVisibility(true);
                    ResetYScale();
                }
                else
                {
                    rbLive.ForeColor = rbReplay.ForeColor;
                    rbLive.BackColor = rbReplay.BackColor;
                    bDisplay = false;
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void CheckChangedReplay(object sender, EventArgs e)
        {
            try
            {
                if (rbReplay.Checked)
                {
                    if (timer1.Enabled)
                        timer1.Stop();

                    if (File.Exists(".\\" + sDefFileName))
                        File.Delete(".\\" + sDefFileName);
                    tbMaxTime.ReadOnly = true;
                    cbReplayList.Enabled = true;
                    bLive = false;
                    rbReplay.ForeColor = Color.Black;
                    rbReplay.BackColor = Color.LightGray;
                    btnExecute.Enabled = false;
                    btPlot.Enabled = false;
                    btData.Enabled = false;
                    btCSV.Enabled = false;
                    btSaveSim.Enabled = false;
                    bDisplay = false;
                    chDataGraph.ChartAreas[0].CursorX.Position = -1000F;
                    chDataGraph.Series.Clear();
                    dgResults.Rows.Clear();
                    lbAllPlotItems.SelectedIndex = 0;
                    if (bDelayPlot)
                    {
                        bDelayPlot = false;
                        chkDelay.Checked = false;
                    }
                }
                else
                {
                    rbReplay.ForeColor = rbLive.ForeColor;
                    rbReplay.BackColor = rbLive.BackColor;
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void DataGraphCurPosChanged(object sender, CursorEventArgs e)
        {
            try
            {
                double dXPos = chDataGraph.ChartAreas[0].CursorX.Position;
                int iCnt = dgResults.Rows.Count;
                float fT1 = -1F, fT2 = -1F, fDelT = -1;
                dgResults.ClearSelection();
                bool bDone = false;
                for (int i = 0; i < iCnt; i++)
                {
                    if (dgResults.Rows[i].Cells[0].Value != null)
                    {
                        double dVal = Double.Parse(dgResults.Rows[i].Cells[0].Value.ToString());
                        if (i % 2 == 0)
                            fT1 = (float)dVal;
                        else
                            fT2 = (float)dVal;
                        if (fT1 >= 0 && fT2 >= 0)
                        {
                            fDelT = Math.Abs(fT2 - fT1);
                            fDataTS = fDelT;
                        }
                        if (!bDone && (dVal > dXPos || Math.Abs(dVal - dXPos) < 0.001F))
                        {
                            //Center and highlight this line in table
                            chDataGraph.ChartAreas[0].CursorX.Position = dVal;
                            iSyncRowNum = i;
                            dgResults.CurrentCell = dgResults[0, i];
                            int iRow = i - dgResults.DisplayedRowCount(false) / 2;
                            dgResults.FirstDisplayedScrollingRowIndex = (iRow < 0) ? 0 : iRow;
                            bDone = true;
                        }
                        if (bDone && fDelT > 0)
                            break;
                    }
                }
                chDataGraph.Focus();
            }
            catch (Exception ee)
            {
            }
        }

        private void chDataGraph_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                        e.IsInputKey = true;
                        break;
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void DataGraphKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (iSkip >= 0)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            chDataGraph.ChartAreas[0].CursorX.Position -= fDataTS;
                            break;
                        case Keys.Right:
                            chDataGraph.ChartAreas[0].CursorX.Position += fDataTS;
                            break;
                        case Keys.Up:
                        case Keys.Down:
                            break;
                    }
                    DataGraphCurPosChanged(this, null);
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = "Failure during KeyDown";
            }
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (thrProc != null && thrProc.IsAlive)
                    thrProc.Abort();
                if (thrCons != null && thrCons.IsAlive)
                    thrCons.Abort();
            }
            catch(Exception ee)
            {
                tbStatus.Text = "Failure to close form";
            }
        }

        private void MainFormResize(object sender, EventArgs e)
        {
            Size szMax;
            try
            {
                Rectangle rect = Screen.FromControl(this).Bounds;
                if (rect != rectScreen)
                {
                    //window has moved to new screen
                    bSaveNorm = true;
                    bSaveMax = true;
                    rectScreen = rect;
                }

                int iBotMarg = 50;
                int iRightMarg = 10;
                int iDeltaR1, iDeltaL1;
                float fRatioX, fRatioY;
                if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Minimized)
                {
                    if (bSaveNorm)
                    {
                        aSizes = new int[39+4*iNumCustIcTabs];
                        aSizes[0] = chDataGraph.Height;
                        aSizes[1] = chDataGraph.Width;
                        aSizes[2] = chDataGraph.Top;
                        aSizes[3] = dgResults.Height;
                        aSizes[4] = dgResults.Width;
                        aSizes[5] = dgResults.Top;
                        aSizes[6] = dgResults.Left;
                        aSizes[7] = pnlInitVals.Height;
                        aSizes[8] = pnlInitVals.Width;
                        aSizes[9] = pnlInitVals.Left;
                        aSizes[10] = tcInit.Height;
                        aSizes[11] = tcInit.Width;
                        aSizes[12] = tcInit.Left;
                        aSizes[13] = tabPage1.Width;
                        aSizes[14] = tabPage1.Height;
                        aSizes[15] = dg1.Width;
                        aSizes[16] = dg1.Height;
                        aSizes[17] = btReset.Left;
                        aSizes[18] = panStabPlots.Height;
                        aSizes[19] = panStabPlots.Width;
                        aSizes[20] = chDataGraph.Top;
                        aSizes[21] = panStabPlots.Left;
                        //aSizes[22] = chBodeAmp.Height;
                        //aSizes[23] = chBodeAmp.Width;
                        //aSizes[24] = chBodeAmp.Top;
                        //aSizes[25] = chBodeAmp.Left;
                        //aSizes[26] = chBodePhase.Height;
                        //aSizes[27] = chBodePhase.Width;
                        //aSizes[28] = chBodePhase.Top;
                        //aSizes[29] = chBodePhase.Left;
                        //aSizes[30] = chNyq.Height;
                        //aSizes[31] = chNyq.Width;
                        //aSizes[32] = chNyq.Top;
                        //aSizes[33] = chNyq.Left;
                        //aSizes[34] = chStab.Height;
                        //aSizes[35] = chStab.Width;
                        //aSizes[36] = chStab.Top;
                        //aSizes[37] = chStab.Left;
                        aSizes[38] = btICSets.Left;
                        int i = 39;
                        foreach (TabPage tp in tcInit.TabPages)
                        {
                            aSizes[i] = tp.Width;
                            aSizes[i+1] = tp.Height;
                            i+=2;
                        }
                        i = 40;
                        foreach (DataGridView dgv in aIcDgs)
                        {
                            aSizes[i] = dgv.Width;
                            aSizes[i + 1] = dgv.Height;
                            i += 2;
                        }
                        bSaveNorm = false;
                    }
                    if (WindowState == FormWindowState.Maximized)
                    {
                        if (bSaveMax)
                        {
                            szMax = this.Size;
                            fRatioX = ((float)szMax.Width) / szWindowSize.Width;
                            fRatioY = ((float)szMax.Height) / szWindowSize.Height;
                            chDataGraph.Height = (int)(this.Size.Height - pnlSimParms.Height - menuStrip1.Height - iBotMarg);
                            chDataGraph.Width = (int)(chDataGraph.Width * fRatioX);
                            //chDataGraph.Top = this.Size.Height - chDataGraph.Height - iBotMarg;
                            dgResults.Height = chDataGraph.Height;
                            dgResults.Width = (int)(dgResults.Width * fRatioX);
                            dgResults.Top = chDataGraph.Top;
                            dgResults.Left = (this.Right - this.Left) - dgResults.Width - iRightMarg;
                            iDeltaR1 = pnlInitVals.Right - (pnlInitVals.Left + btReset.Right);
                            iDeltaL1 = tcInit.Left - pnlInitVals.Left;
                            pnlInitVals.Height = chDataGraph.Height;
                            pnlInitVals.Width = (int)(pnlInitVals.Width * fRatioX);
                            pnlInitVals.Left = (this.Right - this.Left) - pnlInitVals.Width - iRightMarg;
                            tcInit.Height = (int)(chDataGraph.Height - btReset.Height - 5);
                            tcInit.Width = (int)(tcInit.Width * fRatioX);
                            tabPage1.Height = (int)(tcInit.Height);
                            tabPage1.Width = (int)(tabPage1.Width * fRatioX);
                            dg1.Height = (int)(dg1.Height * chDataGraph.Height / aSizes[14]);
                            dg1.Width = (int)(dg1.Width * fRatioX) + 50;
                            btReset.Left = pnlInitVals.Right - btReset.Width - pnlInitVals.Left;
                            panStabPlots.Height = chDataGraph.Height;
                            panStabPlots.Width = chDataGraph.Width;
                            panStabPlots.Top = chDataGraph.Top;
                            panStabPlots.Left = chDataGraph.Left;
                            int h, w, x1, y1, x2, y2, x3, y3, x4, y4;
                            if (bMultDisplayMode)
                            {
                                h = panStabPlots.Height / 2 - 7;
                                w = panStabPlots.Width / 2 - 7;
                                x1 = y1 = 5;
                                x2 = w + 10; 
                                y2 = 5;
                                x3 = 5; 
                                y3 = h + 10;
                                x4 = w + 10; 
                                y4 = h + 10;
                            }
                            else
                            {
                                h = panStabPlots.Height;
                                w = panStabPlots.Width;
                                x1 = y1 = 0;
                                x2 = y2 = 0;
                                x3 = y3 = 0;
                                x4 = y4 = 0;
                            }
                            //if (chBodeAmp.Visible)
                            //{
                                chBodeAmp.Height = h;
                                chBodeAmp.Width = w;
                                chBodeAmp.Top = y1;
                                chBodeAmp.Left = x1;
                            //}
                            //if (chBodePhase.Visible)
                            //{
                                chBodePhase.Height = h;
                                chBodePhase.Width = w;
                                chBodePhase.Top = y2;
                                chBodePhase.Left = x2;
                            //}
                            //if (chNyq.Visible)
                            //{
                                //chNyq.Height = h;
                                //chNyq.Width = w;
                                //chNyq.Top = y3;
                                //chNyq.Left = x3;
                            //}
                            //if (chStab.Visible)
                            //{
                                chStab.Height = h;
                                chStab.Width = w;
                                chStab.Top = y4;
                                chStab.Left = x4;
                            //}
                            btICSets.Left = pnlInitVals.Right - pnlInitVals.Left - btReset.Width - btICSets.Width -5;
                            foreach (TabPage tp in tcInit.TabPages)
                            {
                                tp.Height = (int)(tcInit.Height);
                                tp.Width = (int)(tp.Width * fRatioX);
                            }
                            foreach (DataGridView dgv in aIcDgs)
                            {
                                dgv.Height = (int)(dgv.Height * chDataGraph.Height / aSizes[14]);
                                dgv.Width = (int)(dgv.Width * fRatioX) + 50;
                            }

                            aSizes2 = new int[39 + 4 * iNumCustIcTabs];
                            aSizes2[0] = chDataGraph.Height;
                            aSizes2[1] = chDataGraph.Width;
                            aSizes2[2] = chDataGraph.Top;
                            aSizes2[3] = dgResults.Height;
                            aSizes2[4] = dgResults.Width;
                            aSizes2[5] = dgResults.Top;
                            aSizes2[6] = dgResults.Left;
                            aSizes2[7] = pnlInitVals.Height;
                            aSizes2[8] = pnlInitVals.Width;
                            aSizes2[9] = pnlInitVals.Left;
                            aSizes2[10] = tcInit.Height;
                            aSizes2[11] = tcInit.Width;
                            aSizes2[12] = tcInit.Left;
                            aSizes2[13] = tabPage1.Width;
                            aSizes2[14] = tabPage1.Height;
                            aSizes2[15] = dg1.Width;
                            aSizes2[16] = dg1.Height;
                            aSizes2[17] = btReset.Left;
                            aSizes2[18] = panStabPlots.Height;
                            aSizes2[19] = panStabPlots.Width;
                            aSizes2[20] = panStabPlots.Top;
                            aSizes2[21] = panStabPlots.Left;
                            aSizes2[22] = chBodeAmp.Height;
                            aSizes2[23] = chBodeAmp.Width;
                            aSizes2[24] = chBodeAmp.Top;
                            aSizes2[25] = chBodeAmp.Left;
                            aSizes2[26] = chBodePhase.Height;
                            aSizes2[27] = chBodePhase.Width;
                            aSizes2[28] = chBodePhase.Top;
                            aSizes2[29] = chBodePhase.Left;
                            //aSizes2[30] = chNyq.Height;
                            //aSizes2[31] = chNyq.Width;
                            //aSizes2[32] = chNyq.Top;
                            //aSizes2[33] = chNyq.Left;
                            aSizes2[34] = chStab.Height;
                            aSizes2[35] = chStab.Width;
                            aSizes2[36] = chStab.Top;
                            aSizes2[37] = chStab.Left;
                            aSizes2[38] = btICSets.Left;
                            int i = 39;
                            foreach (TabPage tp in tcInit.TabPages)
                            {
                                aSizes2[i] = tp.Width;
                                aSizes2[i+1] = tp.Height;
                                i += 2;
                            }
                            i = 40;
                            foreach (DataGridView dgv in aIcDgs)
                            {
                                aSizes2[i] = dgv.Width;
                                aSizes2[i+1] = dgv.Height;
                                i += 2;
                            }
                        }
                        if (!bSaveMax)
                        {
                            chDataGraph.Height = aSizes2[0];
                            chDataGraph.Width = aSizes2[1];
                            chDataGraph.Top = aSizes2[2];
                            dgResults.Height = aSizes2[3];
                            dgResults.Width = aSizes2[4];
                            dgResults.Top = aSizes2[5];
                            dgResults.Left = aSizes2[6];
                            pnlInitVals.Height = aSizes2[7];
                            pnlInitVals.Width = aSizes2[8];
                            pnlInitVals.Left = aSizes2[9];
                            tcInit.Height = aSizes2[10];
                            tcInit.Width = aSizes2[11];
                            tcInit.Left = aSizes2[12];
                            tabPage1.Width = aSizes2[13];
                            tabPage1.Height = aSizes2[14];
                            dg1.Width = aSizes2[15];
                            dg1.Height = aSizes2[16];
                            btReset.Left = aSizes2[17];
                            panStabPlots.Height = aSizes2[18];
                            panStabPlots.Width = aSizes2[19];
                            panStabPlots.Top = aSizes2[20];
                            panStabPlots.Left = aSizes2[21];
                            chBodeAmp.Height = aSizes2[22];
                            chBodeAmp.Width = aSizes2[23];
                            chBodeAmp.Top = aSizes2[24];
                            chBodeAmp.Left = aSizes2[25];
                            chBodePhase.Height = aSizes2[26];
                            chBodePhase.Width = aSizes2[27];
                            chBodePhase.Top = aSizes2[28];
                            chBodePhase.Left = aSizes2[29];
                            //chNyq.Height = aSizes2[30];
                            //chNyq.Width = aSizes2[31];
                            //chNyq.Top = aSizes2[32];
                            //chNyq.Left = aSizes2[33];
                            chStab.Height = aSizes2[34];
                            chStab.Width = aSizes2[35];
                            chStab.Top = aSizes2[36];
                            chStab.Left = aSizes2[37];
                            btICSets.Left = aSizes2[38];
                            int i = 39;
                            foreach (TabPage tp in tcInit.TabPages)
                            {
                                tp.Width = aSizes2[i];
                                tp.Height = aSizes2[i+1];
                                i += 2;
                            }
                            i = 40;
                            foreach (DataGridView dgv in aIcDgs)
                            {
                                dgv.Width = aSizes2[i];
                                dgv.Height = aSizes2[i+1];
                                i += 2;
                            }
                        }
                        bSaveMax = false;
                    }
                }
                if (WindowState == FormWindowState.Normal)
                {
                    chDataGraph.Height = aSizes[0];
                    chDataGraph.Width = aSizes[1];
                    chDataGraph.Top = aSizes[2];
                    dgResults.Height = aSizes[3];
                    dgResults.Width = aSizes[4];
                    dgResults.Top = aSizes[5];
                    dgResults.Left = aSizes[6];
                    pnlInitVals.Height = aSizes[7];
                    pnlInitVals.Width = aSizes[8];
                    pnlInitVals.Left = aSizes[9];
                    tcInit.Height = aSizes[10];
                    tcInit.Width = aSizes[11];
                    tcInit.Left = aSizes[12];
                    tabPage1.Width = aSizes[13];
                    tabPage1.Height = aSizes[14];
                    dg1.Width = aSizes[15];
                    dg1.Height = aSizes[16];    
                    btReset.Left = aSizes[17];
                    panStabPlots.Height = aSizes[18];
                    panStabPlots.Width = aSizes[19];
                    panStabPlots.Top = aSizes[20];
                    panStabPlots.Left = aSizes[21];
                    int h, w, x1, y1, x2, y2, x3, y3, x4, y4;
                    if (bMultDisplayMode)
                    {
                        h = panStabPlots.Height / 2 - 7;
                        w = panStabPlots.Width / 2 - 7;
                        x1 = y1 = 5;
                        x2 = w + 10;
                        y2 = 5;
                        x3 = 5;
                        y3 = h + 10;
                        x4 = w + 10;
                        y4 = h + 10;
                    }
                    else
                    {
                        h = panStabPlots.Height;
                        w = panStabPlots.Width;
                        x1 = y1 = 0;
                        x2 = y2 = 0;
                        x3 = y3 = 0;
                        x4 = y4 = 0;
                    }
                    //if (chBodeAmp.Visible)
                    //{
                    chBodeAmp.Height = h;
                    chBodeAmp.Width = w;
                    chBodeAmp.Top = y1;
                    chBodeAmp.Left = x1;
                    //}
                    //if (chBodePhase.Visible)
                    //{
                    chBodePhase.Height = h;
                    chBodePhase.Width = w;
                    chBodePhase.Top = y2;
                    chBodePhase.Left = x2;
                    //}
                    //if (chNyq.Visible)
                    //{
                    //chNyq.Height = h;
                    //chNyq.Width = w;
                    //chNyq.Top = y3;
                    //chNyq.Left = x3;
                    //}
                    //if (chStab.Visible)
                    //{
                    chStab.Height = h;
                    chStab.Width = w;
                    chStab.Top = y4;
                    chStab.Left = x4;
                    //}
                    //chBodeAmp.Height = aSizes[22];
                    //chBodeAmp.Width = aSizes[23];
                    //chBodeAmp.Top = aSizes[24];
                    //chBodeAmp.Left = aSizes[25];
                    //chBodePhase.Height = aSizes[26];
                    //chBodePhase.Width = aSizes[27];
                    //chBodePhase.Top = aSizes[28];
                    //chBodePhase.Left = aSizes[29];
                    //chNyq.Height = aSizes[30];
                    //chNyq.Width = aSizes[31];
                    //chNyq.Top = aSizes[32];
                    //chNyq.Left = aSizes[33];
                    //chStab.Height = aSizes[34];
                    //chStab.Width = aSizes[35];
                    //chStab.Top = aSizes[36];
                    //chStab.Left = aSizes[37];
                    btICSets.Left = aSizes[38];
                    int i = 39;
                    foreach (TabPage tp in tcInit.TabPages)
                    {
                        tp.Width = aSizes[i];
                        tp.Height = aSizes[i+1];
                        i += 2;
                    }
                    i = 40;
                    foreach (DataGridView dgv in aIcDgs)
                    {
                        dgv.Width = aSizes[i];
                        dgv.Height = aSizes[i+1];
                        i += 2;
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickPageSetup(object sender, EventArgs e)
        {
            try
            {
                if (chDataGraph.Visible)
                    chDataGraph.Printing.PageSetup();
                else
                {
                    chBodePhase.Printing.PageSetup();
                    chBodeAmp.Printing.PageSetup();
                    //chNyq.Printing.PageSetup();
                    //chNichols.Printing.PageSetup();
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = "Failed on print page setup";
            }
        }

        private void ClickPrintPreview(object sender, EventArgs e)
        {
            try
            {
                if (chDataGraph.Visible)
                {
                    chDataGraph.ChartAreas[0].BackColor = Color.White;
                    chDataGraph.BackColor = Color.LightGray;
                    chDataGraph.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chDataGraph.Printing.PrintPreview();
                    chDataGraph.ChartAreas[0].BackColor = Color.Black;
                    chDataGraph.BackColor = Color.Black;
                    chDataGraph.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                }
                else
                {
                    chBodePhase.ChartAreas[0].BackColor = Color.White;
                    chBodePhase.BackColor = Color.LightGray;
                    chBodePhase.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chBodePhase.Printing.PrintPreview();
                    chBodePhase.ChartAreas[0].BackColor = Color.Black;
                    chBodePhase.BackColor = Color.Black;
                    chBodePhase.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    chBodeAmp.ChartAreas[0].BackColor = Color.White;
                    chBodeAmp.BackColor = Color.LightGray;
                    chBodeAmp.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chBodeAmp.Printing.PrintPreview();
                    chBodeAmp.ChartAreas[0].BackColor = Color.Black;
                    chBodeAmp.BackColor = Color.Black;
                    chBodeAmp.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    //chNyq.ChartAreas[0].BackColor = Color.White;
                    //chNyq.BackColor = Color.LightGray;
                    //chNyq.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    //chNyq.Printing.PrintPreview();
                    //chNyq.ChartAreas[0].BackColor = Color.Black;
                    //chNyq.BackColor = Color.Black;
                    //chNyq.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    //chNichols.ChartAreas[0].BackColor = Color.White;
                    //chNichols.BackColor = Color.LightGray;
                    //chNichols.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    //chNichols.Printing.PrintPreview();
                    //chNichols.ChartAreas[0].BackColor = Color.Black;
                    //chNichols.BackColor = Color.Black;
                    //chNichols.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = "Failed to preview plot";
            }
        }

        private void CLickPrint(object sender, EventArgs e)
        {
            try
            {
                if (chDataGraph.Visible)
                {
                    chDataGraph.ChartAreas[0].BackColor = Color.White;
                    chDataGraph.BackColor = Color.LightGray;
                    chDataGraph.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chDataGraph.Printing.Print(true);
                    chDataGraph.ChartAreas[0].BackColor = Color.Black;
                    chDataGraph.BackColor = Color.Black;
                    chDataGraph.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                }
                else
                {
                    chBodePhase.ChartAreas[0].BackColor = Color.White;
                    chBodePhase.BackColor = Color.LightGray;
                    chBodePhase.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chBodePhase.Printing.Print(true);
                    chBodePhase.ChartAreas[0].BackColor = Color.Black;
                    chBodePhase.BackColor = Color.Black;
                    chBodePhase.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    chBodeAmp.ChartAreas[0].BackColor = Color.White;
                    chBodeAmp.BackColor = Color.LightGray;
                    chBodeAmp.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    chBodeAmp.Printing.Print(true);
                    chBodeAmp.ChartAreas[0].BackColor = Color.Black;
                    chBodeAmp.BackColor = Color.Black;
                    chBodeAmp.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    //chNyq.ChartAreas[0].BackColor = Color.White;
                    //chNyq.BackColor = Color.LightGray;
                    //chNyq.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    //chNyq.Printing.Print(true);
                    //chNyq.ChartAreas[0].BackColor = Color.Black;
                    //chNyq.BackColor = Color.Black;
                    //chNyq.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    //chNichols.ChartAreas[0].BackColor = Color.White;
                    //chNichols.BackColor = Color.LightGray;
                    //chNichols.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                    //chNichols.Printing.Print(true);
                    //chNichols.ChartAreas[0].BackColor = Color.Black;
                    //chNichols.BackColor = Color.Black;
                    //chNichols.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                tbStatus.Text = "Failed to print plot";
            }
        }

        private void ClickClearToPlot(object sender, EventArgs e)
        {
            lbItemsToPlot.Items.Clear();
        }

        private void ClickHelpDocument(object sender, EventArgs e)
        {
            try
            {
                string[] sPaths;
                sPaths = Directory.GetFiles(".", "Simulation Model Help File*.pdf", SearchOption.TopDirectoryOnly);
                Process.Start(sPaths[0]);//".\\Simulation Model Help File.pdf"
            }
            catch (Exception ee)
            {
                tbStatus.Text = "failed to open help file";
            }
        }

        private void ClickCSV(object sender, EventArgs e)
        {
            string strCsvFilePath;
            try
            {
                strCsvFilePath = strDataStorePath.Replace("SimData", "CsvData");
                if (!Directory.Exists(strCsvFilePath))
                {
                    Directory.CreateDirectory(strCsvFilePath);
                }
                frmSpecName formName = new frmSpecName(strCsvFilePath, false);
                if (formName.ShowDialog() == DialogResult.OK)
                {
                    strCsvFilePath = formName.FileName();
                }
                else
                {
                    strCsvFilePath = strCsvFilePath + "\\__OUT$$DATA.csv";
                }
                UseWaitCursor = true;
                StreamWriter strmW = new StreamWriter(strCsvFilePath, false);
                int ii, jj;
                //int[] iaTimes = new int[5];
                //iaTimes[0] = iaTimes[1] = iaTimes[2] = iaTimes[3] = iaTimes[4] = 0;
                int[] iaDbIndices = new int[lbItemsToPlot.Items.Count + 1];
                jj = 1;
                iaDbIndices[0] = 0; //time at index 0
                foreach (object oItem in lbItemsToPlot.Items)
                {
                    ii = 1;
                    foreach (VarInfo v in straSDNames)
                    {
                        if (oItem.ToString() == v.sName)
                        {
                            iaDbIndices[jj++] = ii;
                            break;
                        }
                        ii++;
                    }
                }
                float fTmp;//fSF, 
                long lSum;
                FileInfo fi = new FileInfo(strFilePath);            //open file, skip header stuff
                bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
                lSum = (long)(24 + iNumIC * 12 + 512);
                bnryIn.BaseStream.Seek(lSum, SeekOrigin.Begin);

                string strHeader;

                strmW.Write("Time,");
                for (int j = 1; j < iaDbIndices.Length; j++)
                {
                    strHeader = String.Format("{0}", lbItemsToPlot.Items[j - 1].ToString());// (sf={1:G}), fSF
                    strmW.Write(strHeader + ",");
                }
                strmW.WriteLine();
                int iRow = 0;
                bool bShowRow;
                long lOff, lRowOff = 4 * (straSDNames.Length + 1);
                long lTest = fi.Length - lRowOff;
                lOff = 0;
                //strOut = string.Format("B:,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);
                while (lSum < fi.Length)
                {
                    //iaTimes[0] -= (int)(swTimer.ElapsedMilliseconds);
                    bShowRow = false;
                    if (iSkip >= 0) //show nth row if filtered
                        bShowRow = (iRow % iSkip) == 0;
                    if (lSum >= lTest)  //always show final row
                        bShowRow = true;

                    //iaTimes[0] += (int)(swTimer.ElapsedMilliseconds);
                    if (bShowRow)
                    {
                        //iaTimes[1] -= (int)(swTimer.ElapsedMilliseconds);
                        if (lOff > 0)
                        {
                            bnryIn.BaseStream.Seek(lOff, SeekOrigin.Current);
                            lOff = 0;
                        }
                        fTmp = bnryIn.ReadSingle();
                        //iaTimes[1] += (int)(swTimer.ElapsedMilliseconds);
                        //iaTimes[4] -= (int)(swTimer.ElapsedMilliseconds);
                        strmW.Write(fTmp.ToString() + ",");
                        int iI;
                        //iaTimes[4] += (int)(swTimer.ElapsedMilliseconds);
                        //iaTimes[2] -= (int)(swTimer.ElapsedMilliseconds);
                        int iSk = 0, iPrevIndex = 0;
                        for (int i = 1; i < iaDbIndices.Length; i++)
                        {
                            iI = iaDbIndices[i];
                            //if (iI > iPrevIndex)
                            //    iSk = (iI - iPrevIndex - 1) * 4;    //go forward
                            //else
                            //    iSk = (iPrevIndex - iI + 1) * -4;   //go back
                            iSk = (iI - iPrevIndex - 1) * 4;    //go forward
                            if (iSk != 0)
                                bnryIn.BaseStream.Seek(iSk, SeekOrigin.Current);
                            iPrevIndex = iI;
                            fTmp = bnryIn.ReadSingle();
                            strmW.Write(fTmp.ToString() + ",");
                        }//foreach
                        strmW.WriteLine();
                        iSk = (straSDNames.Length - iPrevIndex) * 4;
                        bnryIn.BaseStream.Seek(iSk, SeekOrigin.Current);    //get to start of next row
                        //iaTimes[2] += (int)(swTimer.ElapsedMilliseconds);
                    }//if
                    else
                        lOff += lRowOff;
                    //iaTimes[3] -= (int)(swTimer.ElapsedMilliseconds);
                    iRow++;
                    lSum += lRowOff;
                    //iaTimes[3] += (int)(swTimer.ElapsedMilliseconds);
                }//while
                //strOut = string.Format("C:,{0},{1},{2},{3}", swTimer.ElapsedMilliseconds, iaTimes[1], iaTimes[2], iaTimes[4]);
                // strOut = string.Format("C:,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);

                //swTimer.Stop();
                //strmW.Close();
                bnryIn.BaseStream.Close();
                bnryIn.Close();
                bnryIn.Dispose();
                bDisplay = false;

                strmW.Close();
                UseWaitCursor = false;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                UseWaitCursor = false;
            }
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void Results_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    int iSR = dgResults.SelectedRows.Count;
                    int iC = dgResults.Columns.Count;
                    float fNumMBytes = (float)(iSR * iC * 4)/(1024*1024);
                    if (fNumMBytes > 2)
                    {
                        MessageBox.Show("Using Control-C on large data tables may cause a memory exception -- if this occurs, Click the 'Continue' button in the error dialog.  Select less data or use the 'CSV' button to save currently specified data to a file");
                    }
                    e.Handled = false;
                }
            }
            catch (Exception ee)
            {
            }

        }

        private void ToggleVisibilityPlots(bool bDataPlots)
        {
            chDataGraph.Visible = bDataPlots;

            panStabPlots.Visible = !bDataPlots;
        }

        private void ClickDataPlots(object sender, EventArgs e)
        {
            ToggleVisibilityPlots(true);
            eScaleType = ScaleVals.ScaleType.None;
        }

        private bool SupoModelStabCalcs()
        {
            int iCRs = 10;
            int iDNGs = 6;

            double[] daAl = new double[6];
            double[] daF = new double[6];
            double[] dIMP = new double[iCRs];
            double[] dFRA = new double[iCRs];
            double[] dG = new double[iCRs];
            double[] dTau = new double[iCRs];
            double[] dg1v = new double[iCRs];
            //double[] dpf = new double[iCRs];
            double[] dg1m = new double[iCRs];
            double[] dK = new double[iCRs];
            double dGv, dTAU, dLAMBDA, dBeta, dAlf, dPhi, dR0, dEM, dCpf, dCpw, dMCL, dAlf2, dMW, dELM, dMCLUMP, dMT1, dMS1, dWC2, dCPT, dMT, dCPS, dMS;
            double dCpc, dWC, dWF, dPo, dGAMMA, dGAMMA9, dGAM1, dGAM2, dGAM3;
            double dWp, dM1p, dTheta, dW, dWMax, dWstep, BAGT, BPGT;
            double g1p, g1, g2, g3, g4, g5, g6, g7, g8, g11, g21, g31, g41, g51, g61, g71, g81;
            double NicA, NicP;
            Complex cxTmp, cxTmp2, cxTmp3, cxTmp4, cxTmp5, cxTmp6, cxTmp7, cxTmp8, cxTmp9, cxTmp10, cxTmp11, cxTmp12, cxTmp13, cxTmp14, cxTmp15, cxTmp16, cxTmp17, cxTmp18, cxTmp19, cxTmp20, cxTmp21, cxTmp22, cxTmp23, cxTmp24, cxTmp25, cxTmp26, cxTmp27, cxTmp28;
            Complex Nic1, Nic2, Nic;
            Complex s, cxJ, LSUM, Grvpsum, Grvp, Gvvprod, Gt, Gtprod;
            Complex[] Grvpf2 = new Complex[iCRs];
            Complex[] Grvvsum = new Complex[iCRs];
            Complex[] Grvvsum2 = new Complex[iCRs];
            Complex[] Gvpsum = new Complex[iCRs];
            Complex[] Gvp = new Complex[iCRs];
            Complex[] Gvv = new Complex[iCRs];
            Complex[] Grvpf = new Complex[iCRs];
            Complex[] Grvpf1 = new Complex[iCRs];
            Complex[] Grvv = new Complex[iCRs];
            Complex[] Grpf = new Complex[iCRs];
            Complex[] Gpf = new Complex[iCRs];
            Complex[] Gtf = new Complex[iCRs];
            Complex[] Grtf = new Complex[iCRs];
            Complex[] Gpfsumf = new Complex[iCRs];
            Complex[] Grtsumf = new Complex[iCRs];
            Complex[] Grpsum2 = new Complex[iCRs];
            Complex Gpsum, Gp, Grtsum, Grt, Grpsum, Grpsum3 ,Grp, Grvpsum2;
            Complex A1n,A1d,A1,B1n,B1,C1,D1d,D1,E1,F1n,F1d,F1,G1n,G1d,G1f,H1n,H1d,H1,I1d,I1,J1n,J1d,J1,K1d,K1,L1n,L1d,L1,M1n,M1d,M1;
            Complex An,Ad,A,Bn,B,C,Dd,D,E,Fn,Fd,Ff,Gn,Gd,Gf,Hn,Hd,H,Id,I,Jn,Jd,J,Kd,Kf,Ln,Ld,L,Md,M,GA,GB,GC,GD;
            Complex G1,G2,G3,G4,G5,G6,G7,G8,G9,G10,G11,G12,G13,G14,G15,G16,G17,G18,G19,G20,G21,HT,GT,StabGT,UC;

            try
            {
                for (int i = 0; i < iDNGs; i++)
                {
                    daF[i] = ciaInfo[i * 2 + 12].dValue;
                    daAl[i] = ciaInfo[i * 2 + 11].dValue;
                }
                dGv = 5e-5;
                dAlf2 = -.0003;  //computed value
                dCpf = ciaInfo[29].dValue;          //CV
                dMW = ciaInfo[37].dValue;           //Mass cooling wall
                dCpw = ciaInfo[38].dValue;          //CPW
                dAlf = -ciaInfo[39].dValue;        //-ALF
                dPhi = -100 * ciaInfo[40].dValue;  //-100*PHI
                dLAMBDA = ciaInfo[61].dValue;      //MNT in model, mean neutron generation time
                dBeta = ciaInfo[62].dValue;        //BETA
                dTAU = ciaInfo[63].dValue;
                dMCLUMP = ciaInfo[71].dValue;          //MCLUMP
                dR0 = ciaInfo[74].dValue;          //A
                dEM = ciaInfo[84].dValue;          //mass of fuel for 1 core level (EM)
                dELM = ciaInfo[85].dValue;          //ELM (mass in BL)

                dCpc = GetSSVal(212);               //CPC
                dWC = GetSSVal(57);                 //WC 
                dWF = GetSSVal(544);               //total core flow (WF)
                dPo = GetSSVal(54) / 1000;            //kw/1000
                dGAMMA = GetSSVal(76);              //GAMMA
                dGAMMA9 = GetSSVal(77);             //GAMMA9
                dMT1 = GetSSVal(273);               //HX tube side fluid mass
                dGAM1 = GetSSVal(275);              //gam used in HX primary
                dGAM2 = GetSSVal(281);              //gam used in HX primary
                dGAM3 = GetSSVal(282);              //gam used in HX primary
                dMS1 = GetSSVal(279);               //HX sec fluid mass
                dWC2 = GetSSVal(278);              //HX sec flow
                dMT = GetSSVal(283);                //HX tube mass
                dCPT = GetSSVal(284);                 //Cp of HX tubes
                dMS = GetSSVal(285);                //MS, mass of secondary wall
                dCPS = 5e-3;                        //Cp of HX sec shell wall, can't use model variable

                for (int i = 0; i < iCRs; i++)
                {
                    dIMP[i] = ciaInfo[50 - i].dValue;
                    dFRA[i] = ciaInfo[60 - i].dValue;
                    dG[i] = dFRA[i] * dGv;
                    dTau[i] = dTAU;
                    dg1v[i] = 1 / dTau[i];
                    dg1m[i] = dWF / dEM;
                    dK[i] = dFRA[i] / (dEM * dCpf);
                }
                g1 = 2 * dWC / dMT1;
                g2 = dGAM1 / dMT1 / dCpc;
                g3 = 2 * dWC2 / dMS1;
                g4 = dGAM2 / dMS1 / dCpc;
                g5 = dGAM3 / dMS1 / dCpc;
                g6 = dGAM1 / dMT / dCPT;
                g7 = dGAM2 / dMT / dCPT;
                g8 = dGAM3 / dMS / dCPS;

                g11 = 2 * dWF / dELM;
                g21 = dGAMMA / (dELM * dCpf);
                g31 = 2 * dWC / dMCLUMP;
                g41 = dGAMMA9 / (dMCLUMP * dCpc);
                g51 = 0;                                    //dGAMMA / (dMCLUMP * dCpc); 
                g61 = dGAMMA / (dMW * dCpw);
                g71 = dGAMMA9 / (dMW * dCpw);
                g81 = 0;

                dWp = 0.2161;
                dM1p = 0.2;
                g1p = dWp / dM1p;

                cxJ = Complex.ImaginaryOne;

                int iScale;
                double dScale;

                chBodePhase.Series.SuspendUpdates();
                //chBodePhase2.Series.SuspendUpdates();
                chBodeAmp.Series.SuspendUpdates();
                //chBodeAmp2.Series.SuspendUpdates();
                //chNyq.Series.SuspendUpdates();
                chStab.Series.SuspendUpdates();

                dScale = 3;
                chBodeAmp.ChartAreas[0].AxisX.Minimum = daStVals[4];
                chBodeAmp.ChartAreas[0].AxisX.Maximum = daStVals[5];
                chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[7];
                chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[6];
                chBodeAmp.ChartAreas[0].AxisY.Minimum = daStVals[0];
                chBodeAmp.ChartAreas[0].AxisY.Maximum = daStVals[1];
                chBodeAmp.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[3];
                chBodeAmp.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[2];
                chBodeAmp.Series.Clear();

                dScale = 3;
                chBodePhase.ChartAreas[0].AxisX.Minimum = daStVals[17];
                chBodePhase.ChartAreas[0].AxisX.Maximum = daStVals[18];
                chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[20];
                chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[19];
                chBodePhase.ChartAreas[0].AxisY.Minimum = daStVals[13];
                chBodePhase.ChartAreas[0].AxisY.Maximum = daStVals[14];
                chBodePhase.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[16];
                chBodePhase.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[15];
                chBodePhase.Series.Clear();

                //dScale = 4;
                //chNyq.ChartAreas[0].AxisX.Minimum = -dScale;
                //chNyq.ChartAreas[0].AxisX.Maximum = dScale;
                //chNyq.ChartAreas[0].AxisY.Minimum = -dScale;
                //chNyq.ChartAreas[0].AxisY.Maximum = dScale;
                //chNyq.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5 * dScale;
                //chNyq.ChartAreas[0].AxisY.LabelStyle.Interval = dScale;
                //chNyq.Series.Clear();

                dScale = 50;//angle=98.64 at w=0.493
                chStab.ChartAreas[0].AxisX.Minimum = daStVals[30];//
                chStab.ChartAreas[0].AxisX.Maximum = daStVals[31];//
                chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[33];//
                chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[32];//
                chStab.ChartAreas[0].AxisY.Minimum = daStVals[26];//
                chStab.ChartAreas[0].AxisY.Maximum = daStVals[27];//
                chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[29];//
                chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[28];//
                chStab.Series.Clear();

                chBodeAmp.Series.Add("1");
                chBodeAmp.Series[0].ChartType = SeriesChartType.Line;
                //chBodeAmp2.Series.Add("1");
                //chBodeAmp2.Series[0].ChartType = SeriesChartType.Line;
                chBodePhase.Series.Add("1");
                chBodePhase.Series[0].ChartType = SeriesChartType.Line;
                //chBodePhase2.Series.Add("1");
                //chBodePhase2.Series[0].ChartType = SeriesChartType.Line;
                //chNyq.Series.Add("1");
                //chNyq.Series[0].ChartType = SeriesChartType.Point;
                //chNyq.Series.Add("2");
                //chNyq.Series[1].ChartType = SeriesChartType.Point;
                chStab.Series.Add("1");
                chStab.Series[0].ChartType = SeriesChartType.Point;
                chStab.Series.Add("2");
                chStab.Series[1].ChartType = SeriesChartType.Point;
                chStab.Series[0].MarkerStyle = MarkerStyle.Circle;
                chStab.Series[1].MarkerStyle = MarkerStyle.Circle;
                chStab.Series[0].MarkerSize = 2;
                chStab.Series[1].MarkerSize = 2;

                int iBLLim, iCRm1, iCRm2;
                iCRm1 = iCRs - 1;
                iCRm2 = iCRs - 2;
                //strOut = string.Format("B,{0}", swTimer.ElapsedMilliseconds);
                //strmW.WriteLine(strOut);
                for (int ii = 0; ii < 3; ii++)
                {
                    if (ii < 2)
                        iBLLim = 2;
                    else
                        iBLLim = 3;
                    for (int iBL = 1; iBL < iBLLim; iBL++)
                    {
                        switch (ii)
                        {
                            case 0:
                                dW = daStVals[8]; dWMax = daStVals[9]; dWstep = daStVals[10];
                                break;
                            case 1:
                                dW = daStVals[21]; dWMax = daStVals[22]; dWstep = daStVals[23];
                                break;
                            case 2:
                                dW = daStVals[34]; dWMax = daStVals[35]; dWstep = daStVals[36];//dW = 0.1; dWMax = 1000; dWstep = 1e-3;
                                break;
                            case 3:
                                dW = -1; dWMax = 1; dWstep = 1e-4;  //dW = 0.1; dWMax = 1000; dWstep = 1e-3;
                                break;
                            default:
                                dW = 1; dWMax = 0; dWstep = 1;  //required by compiler, not used
                                break;
                        }
                        //strOut = string.Format("C,{0}", swTimer.ElapsedMilliseconds);
                        //strmW.WriteLine(strOut);
                        while (dW < dWMax)
                        {
                            if (iBLLim == 2)
                                s = cxJ * dW;
                            else
                            {
                                dTheta = dW * Math.PI / 2;
                                s = (iBL == 1) ? cxJ * dW : (1.0e-04 * Complex.Exp(cxJ * dTheta));
                            }
                            LSUM = 0;
                            for (int k = 0; k < iDNGs; k++)
                                LSUM += (daF[k] / (s + daAl[k]));
                            Grvpsum = 0;
                            Gpsum = 0;
                            Grtsum = 0;
                            Grpsum = 0;
                            Grvp = 0;
                            Grp = 0;
                            Grvpsum2 = 0;
                            Grpsum3 = 0;
                            for (int i = 0; i < iCRs; i++)
                            {
                                cxTmp = s + dg1v[i];
                                cxTmp2 = dPhi * dIMP[i];
                                cxTmp3 = dG[i] / cxTmp;
                                cxTmp4 = dg1v[i] / cxTmp;
                                Gvp[i] = cxTmp3;
                                Gvv[i] = cxTmp4;
                                Grvpf[i] = cxTmp2 * cxTmp3;
                                Grvv[i] = cxTmp2 * cxTmp4;
                                Grvpsum2 = Grvpsum2 + Grvpf[i];
                            }
                            for (int k = 0; k < iCRm2; k++)
                            {
                                Grvpsum = 0;
                                for (int i = k + 2; i < iCRs; i++)
                                {
                                    Gvvprod = 1;
                                    for (int jj = k + 1; jj < i; jj++)
                                    {
                                        Gvvprod = Gvvprod * Gvv[jj];
                                    }
                                    Grvpsum = Grvpsum + Grvv[i] * Gvvprod;
                                }
                                Grvp = Grvp + Gvp[k + 1] * (Grvv[k + 1] + Grvpsum);
                            }
                            Grvp = Grvp + Gvp[iCRm2] * Grvv[iCRm1] + Grvpsum2;
                            //--- In Core Multi-Region; Grp,Gp,Gt,Grt
                            Gt = 1;
                            for (int i = 0; i < iCRs; i++)
                            {
                                cxTmp = s + dg1m[i];
                                cxTmp2 = dAlf * dIMP[i];
                                cxTmp3 = dg1m[i] / cxTmp;
                                cxTmp4 = dK[i] / cxTmp;
                                Grpf[i] = cxTmp2 * cxTmp4;
                                Gpf[i] = cxTmp4;
                                Gtf[i] = cxTmp3;
                                Grtf[i] = cxTmp2 * cxTmp3;
                                Gt = Gt * cxTmp3;
                            }
                            //StreamWriter sw = new StreamWriter("c:\\mydesire\\other.txt");
                            //String str;
                                //str = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",i,Gt.Real,Gt.Imaginary,Gtf[i].Real,Gtf[i].Imaginary,Grpf[i].Real,Grpf[i].Imaginary,dK[i],Grtf[i].Real,Grtf[i].Imaginary);
                                //sw.WriteLine(str);
                            //sw.Close(); 
                            for (int i = 0; i < iCRm1; i++)
                            {
                                Gtprod = 1;
                                for (int jj = i + 1; jj < iCRs; jj++)
                                {
                                    Gtprod = Gtprod * Gtf[jj];
                                }
                                Gpsum = Gpsum + Gpf[i] * Gtprod;
                            }
                            Gp = Gpf[iCRm1] + Gpsum;
                            for (int i = 1; i < iCRs; i++)
                            {
                                Gtprod = 1;
                                for (int jj = 0; jj < i; jj++)
                                {
                                    Gtprod = Gtprod * Gtf[jj];
                                }
                                Grtsum = Grtsum + Grtf[i] * Gtprod;
                            }
                            Grt = Grtf[0] + Grtsum;
                            for (int i = 0; i < iCRs; i++)
                                Grpsum = Grpsum + Grpf[i];
                            for (int k = 0; k < iCRm2; k++)
                            {
                                Grtsum = 0;
                                for (int i = k + 2; i < iCRs; i++)
                                {
                                    Gtprod = 1;
                                    for (int jj = k + 1; jj < i; jj++)
                                    {
                                        Gtprod = Gtprod * Gtf[jj];
                                    }
                                    Grtsum = Grtsum + Grtf[i] * Gtprod;
                                }
                                Grpsum3 = Grpsum3 + Gpf[k] * (Grtf[k + 1] + Grtsum);
                            }
                            Grp=Grpsum3+Gpf[iCRm2]*Grtf[iCRm1]+Grpsum;

                            cxTmp = s + g61 + g71;
                            cxTmp22 = s + g11;
                            cxTmp2 = cxTmp22 + g21;
                            cxTmp3 = s + g81;
                            cxTmp6 = s + g1;
                            cxTmp4 = cxTmp6 + g2;
                            cxTmp5 = s + g3;
                            cxTmp7 = s + g31;
                            cxTmp11 = g51 * g81;
                            cxTmp12 = g41 * g71;
                            cxTmp13 = g41 * g61;
                            cxTmp14 = s + g6 + g7;
                            cxTmp15 = s + g8;
                            cxTmp16 = g5 * g8 * cxTmp14;
                            cxTmp17 = g4 * g7 * cxTmp15;
                            cxTmp18 = g2 * g6;
                            cxTmp19 = g4 * g6;
                            cxTmp20 = g21 * g61;
                            cxTmp28 = g4 + g5;
                            
                            //--- Thermal Groups
                            A1n = cxTmp2 * cxTmp - cxTmp20;
                            A1d = cxTmp2 * cxTmp;
                            A1 = A1n / A1d;
                            B1n = cxTmp20 + (g11 - g21) * cxTmp;
                            B1 = B1n / cxTmp;
                            C1 = g21 * g71 / cxTmp;
                            D1d = cxTmp7 + g41 + g51;
                            D1 = 1 / D1d;
                            E1 = cxTmp13 / cxTmp;
                            F1n = cxTmp12 * cxTmp3 + cxTmp11 * cxTmp;
                            F1d = cxTmp * cxTmp3;
                            F1 = F1n / F1d;
                            G1n = A1 * (1 - D1 * F1) * cxTmp2 - D1 * E1 * C1;
                            G1d = A1 * (1 - D1 * F1) * cxTmp2;
                            G1f = G1n / G1d;
                            H1n = (g31 - g41 - g51) * cxTmp3 * cxTmp + cxTmp12 * cxTmp3 + cxTmp11 * cxTmp;
                            H1d = cxTmp3 * cxTmp;
                            H1 = H1n / H1d;
                            I1d = cxTmp2 * cxTmp * A1;
                            I1 = cxTmp13 / I1d;
                            J1n = (C1 * I1 + H1) * D1;
                            J1d = G1f * (1 - D1 * F1);
                            J1 = J1n / J1d;
                            K1d = cxTmp2 * A1;
                            K1 = J1 * E1 * g11 / K1d;
                            L1n = C1 * (B1 + A1 * cxTmp2);
                            L1d = A1 * cxTmp2;
                            L1 = L1n / L1d;
                            M1n = g11 * E1 * L1 * D1;
                            M1d = A1 * cxTmp2 * G1f * (1 - D1 * F1);
                            An = cxTmp4 * cxTmp14 - cxTmp18;
                            Ad = cxTmp4 * cxTmp14;
                            A = An / Ad;
                            Bn = cxTmp18 + (g1 - g2) * cxTmp14;
                            B = Bn / cxTmp14;
                            C = g2 * g7 / cxTmp14;
                            Dd = (s + g3 + cxTmp28);
                            D = 1 / Dd;
                            E = cxTmp19 / cxTmp14;
                            Fn = cxTmp17 + cxTmp16;
                            Fd = cxTmp14 * cxTmp15;
                            Ff = Fn / Fd;
                            Gn = A * (1 - D * Ff) * cxTmp4 - D * E * C;
                            Gd = A * (1 - D * Ff) * cxTmp4;
                            Gf = Gn / Gd;
                            Hn = (g3 - cxTmp28) * cxTmp15 * cxTmp14 + cxTmp17 + cxTmp16;
                            Hd = cxTmp15 * cxTmp14;
                            H = Hn / Hd;
                            Id = cxTmp4 * cxTmp14 * A;
                            I = cxTmp19 / Id;
                            Jn = (C * I + H) * D;
                            Jd = Gf * (1 - D * Ff);
                            J = Jn / Jd;
                            Kd = cxTmp4 * A;
                            Kf = g1 * J * E / cxTmp4 * A;
                            Ln = C * (B + A * cxTmp4);
                            Ld = A * cxTmp4; L = Ln / Ld;
                            Md = A * cxTmp4 * Gf * (1 - D * Ff);
                            M = g1 * E * L * D / Md;
                            //--- Transfer Functions
                            G1 = dPo / (s * ((dLAMBDA / dBeta) + LSUM));
                            M1 = g11 * E1 * L1 * D1 / (cxTmp2 * A1 * G1 * (1 - D1 * F1));
                            G2 = Grvp;
                            G5 = Gp;
                            G20 = dAlf2 * g11 * A1 * E1 * D1 * (1 - D1 * F1) / (G1f * cxTmp2);
                            G9 = (K1 + g11 * I1) / cxTmp7;
                            G10 = g31 * J1 / cxTmp7;
                            G11 = g1p / (s + g1p);
                            G12 = G11;
                            G17 = G11;
                            G18 = G11;
                            G13 = (cxTmp4 * A * M + g1 * B) / (cxTmp6 * cxTmp4 * A);
                            G14 = g3 * D * L / (cxTmp6 * Gf * (1 - D * Ff));
                            G15 = (Kf + g1 * I) / (s + g3);
                            G16 = g3 * J / (s + g3);
                            // G19=1 
                            G19 = 0;
                            GA = G17 * G18 * G19 / (1 - G16 * G17 * G18 * G19);
                            GB = G13 + G14 * G15 * GA;
                            GC = G11 * G12 * GB / (1 - G10 * G11 * G12 * GB);
                            G21 = dAlf2 * g31 * D1 * (1 - D1 * F1) / G1;
                            G3 = Grp + G5 * (G20 + G9 * GC * G21);
                            G6 = Gt;
                            G4 = Grt + G6 * (G20 + G9 * GC * G21);
                            G7 = (cxTmp2 * A1 * M1 + g11 * B1) / ((cxTmp22) * cxTmp2 * A1);
                            G8 = g31 * D1 * L1 / ((cxTmp22) * G1f * (1 - D1 * F1));
                            G9 = (K1 + g11 * I1) / cxTmp7;
                            GD = (G7 + G8 * G9 * GC) / (1 - G6 * (G7 + G8 * G9 * GC));
                            HT = G2 + G3 + G4 * G5 * GD;
                            StabGT = -G1 * HT;
                            GT = G1 / (1 - G1 * HT);
                            Nic1 = G5 / Grp;
                            Nic2 = 1 + Nic1 * (G20 + G9 * GC * G21);             //not used, but shows the 1+GH form
                            Nic = Nic1*(G20+G9*GC*G21);                          //This is GH
                            NicA = Complex.Abs(-Nic);
                            NicP = (-Nic).Phase;
                            //StreamWriter sw = new StreamWriter("c:\\mydesire\\other.txt");
                            //String str;
                            //str = String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19} {20} {21} {22}", GT.Real, GT.Imaginary, G1.Real, G1.Imaginary, HT.Real, HT.Imaginary, G2.Real, G2.Imaginary, G3.Real, G3.Imaginary, G4.Real, G4.Imaginary, G5.Real, G5.Imaginary, GD.Real, GD.Imaginary, dPo, s.Real, s.Imaginary, dLAMBDA, dBeta, LSUM.Real, LSUM.Imaginary);
                            //sw.WriteLine(str);
                            //str = String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19} {20} {21}", Grvp.Real, Grvp.Imaginary, Gp.Real, Gp.Imaginary, Grp.Real, Grp.Imaginary, Grt.Real, Grt.Imaginary, G6.Real, G6.Imaginary, GC.Real, GC.Imaginary, G9.Real, G9.Imaginary, G20.Real, G20.Imaginary, G21.Real, G21.Imaginary, G7.Real, G7.Imaginary, G8.Real, G8.Imaginary);
                            //sw.WriteLine(str);
                            //str = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19} {20} {21} {22} {23} {24} {25}",Gd.Real,Gd.Imaginary,Ff.Real,Ff.Imaginary,A.Real,A.Imaginary,D.Real,D.Imaginary,A1.Real,A1.Imaginary,M1.Real,M1.Imaginary,B1.Real,B1.Imaginary,E1.Real,E1.Imaginary,L1.Real,L1.Imaginary,D1.Real,D1.Imaginary,G1.Real,G1.Imaginary,F1.Real,F1.Imaginary,C1.Real,C1.Imaginary);
                            //sw.WriteLine(str);
                            //sw.Close();
                            BAGT = Complex.Abs(GT);
                            BPGT = GT.Phase;
                            double dVal = 0.0;
                            switch (ii)
                            {
                                case 0:
                                    if (daStVals[11] < 0.5)
                                        dVal = dW;
                                    else
                                        dVal = Math.Log10(dW);
                                    chBodeAmp.Series[0].Points.AddXY(dVal, BAGT);
                                    break;
                                case 1:
                                    if (daStVals[24] < 0.5)
                                        dVal = dW;
                                    else
                                        dVal = Math.Log10(dW);
                                    chBodePhase.Series[0].Points.AddXY(dVal, BPGT);
                                    break;
                                case 2:
                                    UC = Complex.Exp(cxJ * dW / dWMax * Math.PI * 2);
                                    chStab.Series[0].Points.AddXY(UC.Real, UC.Imaginary);
                                    //chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);//new plot needed --> chStab
                                    chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);
                                    break;
                                case 3:
                                    //chNyq.Series[0].Points.AddXY(NicP, Math.Log10(NicA));
                                    break;
                            }
                            dW += dWstep;
                        }//while
                    }//for
                }//for
            }
            catch (Exception e)
            {
            }

            //double[] daAl = new double[6];
            //double[] daF = new double[6];
            //double[] daKw = new double[5];
            //double[] daP0 = new double[5];
            //double[] daKBar = new double[5];
            //double[] daGBar = new double[5];
            //double dBeta, dMnt, dTau, dAlf, dPhi;
            //double dR0, dK, dG; 
            //double dLStar, dAlfStar, dPhiStar, dGamma, dSigma; 
            //double dWMax, dW, dWg, dBg, dC, dNicA, dNicP;

            //Complex cxJ, cxW, cxVal, cxB1, cxB2;
            //Complex cxGp, cxHT, cxHG, cxNy;
            //Complex cxT1, cxT2, cxT3;
            //Complex[] cxaB3 = new Complex[5];
            //Complex[] cxaB4 = new Complex[5];

            //daAl[0] = ciaInfo[11].dValue;
            //daAl[1] = ciaInfo[13].dValue;
            //daAl[2] = ciaInfo[15].dValue;
            //daAl[3] = ciaInfo[17].dValue;
            //daAl[4] = ciaInfo[19].dValue;
            //daAl[5] = ciaInfo[21].dValue;
            //daF[0] = ciaInfo[12].dValue;
            //daF[1] = ciaInfo[14].dValue;
            //daF[2] = ciaInfo[16].dValue;
            //daF[3] = ciaInfo[18].dValue;
            //daF[4] = ciaInfo[20].dValue;
            //daF[5] = ciaInfo[22].dValue;
            //dBeta = ciaInfo[62].dValue;
            //dMnt = ciaInfo[61].dValue;
            //dTau = ciaInfo[63].dValue;
            //dAlf = -ciaInfo[39].dValue;
            //dPhi = -100 * ciaInfo[40].dValue;
            //dR0 = -0.0;
            //daKw[0] = 1.0;
            //daKw[1] = 5.0;
            //daKw[2] = 10.0;
            //daKw[3] = 25.0;
            //daKw[4] = 50.0;
            //dK = 23;
            //dG = 5e-5;
            //dLStar = dMnt / dBeta;
            //dAlfStar = dAlf;
            //dPhiStar = dPhi;
            //for (int k = 0; k < 5; k++)
            //{
            //    daP0[k] = daKw[k] / 1000;
            //    daKBar[k] = dK * dAlfStar * daP0[k];
            //    daGBar[k] = dG * dPhiStar * daP0[k];
            //}
            //chBodePhase.ChartAreas[0].AxisX.Minimum = -2.0;
            //chBodePhase.ChartAreas[0].AxisX.Maximum = 2.0;
            //chBodePhase.ChartAreas[0].AxisY.Minimum = -2.0;
            //chBodePhase.ChartAreas[0].AxisY.Maximum = 2.0;
            //chBodePhase.ChartAreas[0].AxisY.MajorGrid.Interval = 1;
            //chBodePhase.ChartAreas[0].AxisY.LabelStyle.Interval = 2;
            //chBodePhase.Series.Clear();
            //chBodeAmp.ChartAreas[0].AxisX.Minimum = -2.0;
            //chBodeAmp.ChartAreas[0].AxisX.Maximum = 2.0;
            //chBodeAmp.ChartAreas[0].AxisY.Minimum = -2;
            //chBodeAmp.ChartAreas[0].AxisY.Maximum = 2;
            //chBodeAmp.ChartAreas[0].AxisY.MajorGrid.Interval = 1;
            //chBodeAmp.ChartAreas[0].AxisY.LabelStyle.Interval = 2;
            //chBodeAmp.Series.Clear();

            //chBodePhase.Series.SuspendUpdates();
            //chBodeAmp.Series.SuspendUpdates();
            //chBodePhase.Series.Add("1");
            //chBodePhase.Series.Add("5");
            //chBodePhase.Series.Add("10");
            //chBodePhase.Series.Add("25");
            //chBodePhase.Series.Add("50");
            //chBodePhase.Series[0].ChartType = SeriesChartType.Line;
            //chBodePhase.Series[1].ChartType = SeriesChartType.Line;
            //chBodePhase.Series[2].ChartType = SeriesChartType.Line;
            //chBodePhase.Series[3].ChartType = SeriesChartType.Line;
            //chBodePhase.Series[4].ChartType = SeriesChartType.Line;
            //chBodeAmp.Series.Add("1");
            //chBodeAmp.Series.Add("5");
            //chBodeAmp.Series.Add("10");
            //chBodeAmp.Series.Add("25");
            //chBodeAmp.Series.Add("50");
            //chBodeAmp.Series[0].ChartType = SeriesChartType.Line;
            //chBodeAmp.Series[1].ChartType = SeriesChartType.Line;
            //chBodeAmp.Series[2].ChartType = SeriesChartType.Line;
            //chBodeAmp.Series[3].ChartType = SeriesChartType.Line;
            //chBodeAmp.Series[4].ChartType = SeriesChartType.Line;
            //dGamma = 5e-4;
            //dSigma = 1;

            //cxJ = Complex.ImaginaryOne;
            //dWMax = 0.2;
            //dW = 0.00001;
            //while (dW < dWMax)
            //{
            //    cxW = cxJ * dW;
            //    cxB1 = cxW * dLStar -dR0;
            //    cxT1 = 0 * cxJ;
            //    for (int k = 0; k < 6; k++)
            //        cxT1 += (daF[k] / (cxW + daAl[k]));
            //    cxB2 = cxW * cxT1;
            //    cxT3 = cxB1 + cxB2;
            //    dWg = (2 * dW / dWMax) - 1;
            //    for (int k = 0; k < 5; k++)
            //    {
            //        cxT1 = cxW + dGamma;
            //        cxT2 = cxW + dSigma;
            //        cxaB3[k] = daKBar[k] / cxT1;
            //        cxaB4[k] = daGBar[k] / cxT2;
            //        cxT2 = cxaB3[k] + cxaB4[k];
            //        cxT1 = cxT3 - cxT2;
            //        dBg = Complex.Abs(1 / cxT1) / 10;
            //        cxVal = 1 / cxT1;
            //        dC = cxVal.Phase;
            //        chBodePhase.Series[k].Points.AddXY(dWg, dC);
            //        chBodeAmp.Series[k].Points.AddXY(dWg, dBg);
            //    }
            //    dW += .00001;
            //}//while
            //chBodePhase.Series.ResumeUpdates();
            //chBodeAmp.Series.ResumeUpdates();
            //chNyq.ChartAreas[0].AxisX.Minimum = -5.0;
            //chNyq.ChartAreas[0].AxisX.Maximum = 5.0;
            //chNyq.ChartAreas[0].AxisY.Minimum = -5;
            //chNyq.ChartAreas[0].AxisY.Maximum = 5;
            //chNyq.ChartAreas[0].AxisY.MajorGrid.Interval = 1;
            //chNyq.ChartAreas[0].AxisY.LabelStyle.Interval = 2;
            //chNyq.Series.Clear();
            //chNyq.Series.SuspendUpdates();
            //chNyq.Series.Add("1");
            //chNyq.Series.Add("5");
            //chNyq.Series.Add("10");
            //chNyq.Series.Add("25");
            //chNyq.Series.Add("50");
            //chNyq.Series[0].ChartType = SeriesChartType.Line;
            //chNyq.Series[1].ChartType = SeriesChartType.Line;
            //chNyq.Series[2].ChartType = SeriesChartType.Line;
            //chNyq.Series[3].ChartType = SeriesChartType.Line;
            //chNyq.Series[4].ChartType = SeriesChartType.Line;
            //dW = -0.1;
            //while (dW < 0.1)
            //{
            //    cxW = cxJ * dW;
            //    cxB1 = cxW * dLStar - dR0;
            //    cxT1 = 0 * cxJ;
            //    for (int k = 0; k < 6; k++)
            //    {
            //        cxT1 += (daF[k] / (cxW + daAl[k]));
            //    }
            //    cxB2 = cxW * cxT1;
            //    cxGp = 1 / (cxB1 + cxB2);
            //    for (int k = 0; k < 5; k++)
            //    {
            //        cxT1 = cxW + dGamma;
            //        cxT2 = cxW + dSigma;
            //        cxHT = -daKBar[k] / cxT1;
            //        cxHG = -daGBar[k] / cxT2;
            //        cxNy = 1 + cxGp * (cxHT + cxHG);
            //        chNyq.Series[k].Points.AddXY(cxNy.Real, cxNy.Imaginary);
            //    }

            //    dW += .00001;
            //}//while
            //chNyq.Series.ResumeUpdates();

            return true;
        }

        private float GetSSVal(int iIndex)
        {
            int iNS, iNVars, iRecSize;
            float fTS, fMT;
            float[] faDataRec;
            long lSum, lNS;
            float fRetVal = 0.0F;
            bnryIn = new BinaryReader(File.Open(strFilePath, FileMode.Open));
            //read Header, mainly SFs
            iNS = bnryIn.ReadInt32();
            iNVars = bnryIn.ReadInt32();
            iNumIC = bnryIn.ReadInt32();    //numIC
            iFreq = bnryIn.ReadInt32();
            fTS = bnryIn.ReadSingle();
            fMT = bnryIn.ReadSingle();
            //read in ICs
            iRecSize = iNVars * 4;
            lNS = iNS;
            lSum = (long)(iNumIC * 12 + 512 + (lNS - 1) * iRecSize);
            bnryIn.BaseStream.Seek(lSum, SeekOrigin.Current);
            faDataRec = new float[iNVars];
            for (int ii = 0; ii < iNVars; ii++)
                faDataRec[ii] = bnryIn.ReadSingle();
            bnryIn.BaseStream.Close(); 
            bnryIn.Close();
            bnryIn.Dispose();
            fRetVal = faDataRec[iIndex];
            return fRetVal;
        }

        private bool AD135StabCalcs()
        {
            //StreamWriter strmW = new StreamWriter("TimeOut.csv", false);
            //string strOut;
            //swTimer = new Stopwatch();
            //swTimer.Start();
            //strOut = string.Format("A,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //StreamWriter sw;
            int iCRs = 10;
            int iDNGs = 6;
            Complex[] Gvp = new Complex[iCRs], Gvv = new Complex[iCRs],Grvpf = new Complex[iCRs],Grvpf1 = new Complex[iCRs],Grvv = new Complex[iCRs];
            Complex[] Grvpf2 = new Complex[iCRs], Grvvsum = new Complex[iCRs], Grvvsum2 = new Complex[iCRs], Gvpsum = new Complex[iCRs];
            Complex[] Grpf = new Complex[iCRs], Gpf = new Complex[iCRs], Gtf = new Complex[iCRs], Grtf = new Complex[iCRs], Gpfsumf = new Complex[iCRs], Grtsumf = new Complex[iCRs], Grpsum2 = new Complex[iCRs];
            Complex s,LSUM,Gvvprod,Grvp,Grvpsum,Gt,Gtprod,j;
            Complex Gpsum,Gp,Grtsum,Grt,Grpsum,Grpsum3,Grp,Grvpsum2;
            Complex A1n,A1d,A1,B1n,B1,C1,D1d,D1,E1,F1n,F1d,F1,G1n,G1d,G1f,H1n,H1d,H1,I1d,I1,J1n,J1d,J1,K1d,K1,L1n,L1d,L1,M1n,M1d,M1;
            Complex An,Ad,A,Bn,B,C,Dd,D,E,Fn,Fd,Ff,Gn,Gd,Gf,Hn,Hd,H,Id,I,Jn,Jd,J,Kd,Kf,Ln,Ld,L,Md,M,GA,GB,GC,GD;
            Complex G1,G2,G3,G4,G5,G6,G7,G8,G9,G10,G11,G12,G13,G14,G15,G16,G17,G18,G19,G20,G21,HT,GT,StabGT,UC;

            Complex cxTmp, cxTmp2, cxTmp3, cxTmp4, cxTmp5, cxTmp6, cxTmp7, cxTmp8, cxTmp9, cxTmp10, cxTmp11, cxTmp12, cxTmp13, cxTmp14, cxTmp15, cxTmp16, cxTmp17, cxTmp18, cxTmp19, cxTmp20, cxTmp21, cxTmp22, cxTmp23, cxTmp24, cxTmp25, cxTmp26, cxTmp27, cxTmp28;

            double[] sLAMBDA = new double[iDNGs];
            double[] F = new double[iDNGs];
            double[] IMP = new double[iCRs];
            double[] FRA = new double[iCRs];
            double[] G = new double[iCRs];
            double[] tau = new double[iCRs];
            double[] g1v = new double[iCRs];
            double[] dpf = new double[iCRs];
            double[] g1m = new double[iCRs];
            double[] K = new double[iCRs];
            double LAMBDA=6.810e-05,BETA=0.00794,Gv=5.0e-05,PHI=-3061,TAU=0.7835,Wf=0.4912,Cpf=0.0036,Mf=3.3193,ALF=-0.0413,Wl=0.4912,
            Ml=0.40128,GAMMAl=0.002,Wc=0.7751,Mc=0.2603,GAMMAc=0.0005,Cpc=0.0042,GAMMAsw=0.004921,Mw=4.4921,Cpw=0.0003,
            Msw = 4.4921, Cpsw = 0.0003, Wp = 0.7751, M1p = 0.2, R0 = -0.36, TWC, MCL, MCL2, MCL3, Cpw2, Cpw3, Wc2, Wc3, GAMMA, GAMMA2,
            GAMMA3, GAMMA9, GAMMA90, GAMMA95, MW, MW2, MW3, MT1, GAM1, GAM2, GAM3, MS1, WCS2, MT, CPT, MS, CPS, MWSUM, MCSUM, Ml2, Ml3;

            double P0 = 63.962 * 0.001;
            F[0] = 0.033;F[1] = 0.219;F[2] = 0.196;F[3] = 0.395;F[4] = 0.115;F[5] = 0.042;
            sLAMBDA[0] = 0.0124;sLAMBDA[1] = 0.0305;sLAMBDA[2] = 0.1110;sLAMBDA[3] = 0.3010;sLAMBDA[4] = 1.1400;sLAMBDA[5] = 3.0100;
            //IMP[0] = 0.0306;IMP[1] = 0.0748;IMP[2] = 0.1103;IMP[3] = 0.1354;IMP[4] =  0.1489;IMP[5] =  0.1477;IMP[6] = 0.1328;IMP[7] = 0.1088;IMP[8] = 0.0761;IMP[9] = 0.0347;
            //FRA[0] = 0.0085;FRA[1] = 0.0457; FRA[2] = 0.1039; FRA[3] = 0.1562; FRA[4] = 0.1882; FRA[5] = 0.1857; FRA[6] = 0.1505; FRA[7] = 0.0997; FRA[8] = 0.0490; FRA[9] = 0.0107;

            TAU = ciaInfo[106].dValue;
            Cpf = ciaInfo[112].dValue;          //CV
            BETA = ciaInfo[115].dValue;        //BETA
            LAMBDA = ciaInfo[114].dValue;      //MNT in model, mean neutron generation time
            ALF = -ciaInfo[84].dValue;        //-ALF
            PHI = -100 * ciaInfo[85].dValue;  //-100*PHI
            R0 = ciaInfo[113].dValue;          //A
            TWC = ciaInfo[79].dValue * 1.01;    //TWC increased by 1%, as flow components are in model - ignores possibiity of coolant being shut off
            MCL = ciaInfo[64].dValue;          //MCLUMP
            MCL2 = ciaInfo[65].dValue;         //MCLUMP2
            MCL3 = ciaInfo[66].dValue;         //MCLUMP3

            Cpw = GetSSVal(2);          
            Cpw2 = GetSSVal(4);          
            Cpw3= GetSSVal(6);          
            Mf = GetSSVal(17);          //mass of fuel for 1 core level (EM)
            Ml = GetSSVal(18);          //BL mass of fuel for 1 core level (ELM) now 1 for each channel
            Ml2 = GetSSVal(19);          //BL mass of fuel for 1 core level (ELM) now 1 for each channel
            Ml3 = GetSSVal(20);          //BL mass of fuel for 1 core level (ELM) now 1 for each channel
            Cpc = GetSSVal(301);               //CPC
            Wc = GetSSVal(153);                   //WC
            Wc2 = GetSSVal(154);                   //WC2
            Wc3 = GetSSVal(155);                   //WC3
            Wf = GetSSVal(1024);              //total core flow (WF)
            P0 = GetSSVal(146) / 1000;            //kw/1000
            GAMMA = GetSSVal(174);             //GAMMA
            GAMMA2 = GetSSVal(415);           //GAMMA2
            GAMMA3 = GetSSVal(557);           //GAMMA3
            GAMMA9 = GetSSVal(175);             //GAMMA9
            GAMMA90 = GetSSVal(416);           //GAMMA90
            GAMMA95 = GetSSVal(558);           //GAMMA95
            MW = GetSSVal(1);                  //Mass cooling wall, inner
            MW2 = GetSSVal(3);                 //Mass cooling wall, tubes
            MW3 = GetSSVal(5);                 //Mass cooling wall, outer
            MT1 = GetSSVal(362);               //HX tube side fluid mass
            GAM1 = GetSSVal(364);              //gam used in HX primary
            GAM2 = GetSSVal(370);              //gam used in HX secondary
            GAM3 = GetSSVal(371);              //gam used at HX secondary wall
            MS1 = GetSSVal(368);               //HX sec fluid mass
            WCS2 = GetSSVal(367);              //HX sec flow
            MT = GetSSVal(372);                //HX tube mass
            CPT = GetSSVal(373);               //Cp of HX tubes
            MS = GetSSVal(374);                //HX Shell mass
            CPS = 5e-4;        //Cp of HX sec shell wall, can't use model variable

            double ALF2=-0.0003;double g1p=Wp/M1p, g11, g21, g31, g41, g51, g61, g71, g81, g1, g2, g3, g4, g5, g6, g7, g8;
            //double g11=2*Wl/Ml;double g21=GAMMAl/(Ml*Cpf);double g31=2*Wc/Mc;double g41 =GAMMAc/(Mc*Cpc);double g51=GAMMAsw/(Mc*Cpc);double g61=GAMMAl/(Mw*Cpw);
            //double g71=GAMMAc/(Mw*Cpw);double g81=GAMMAsw/(Msw*Cpsw);
            //double g1=1.33;double g2=4.90;double g3=1.33;double g4=2.45;double g5=2.45;double g6=1.33;double g7=1.33;double g8=0.666;
            MWSUM = MW + MW2 + MW3;
            MCSUM = MCL + MCL2 + MCL3;

            g1 = 2 * (Wc + Wc2 + Wc3) / MT1;    //total coolant flow and total coolant mass at 1 level
            g2 = GAM1 / MT1 / Cpc;
            g3 = 2 * WCS2 / MS1;
            g4 = GAM2 / MS1 / Cpc;
            g5 = GAM3 / MS1 / Cpc;
            g6 = GAM1 / MT / CPT;
            g7 = GAM2 / MT / CPT;
            g8 = GAM3 / MS / CPS;

            g11 = 2 * Wf / (Ml+Ml2+Ml3);
            g21 = (GAMMA + GAMMA2 + GAMMA3) / ((Ml+Ml2+Ml3) * Cpf);
            g31 = 2 * (Wc + Wc2 + Wc3) / MCSUM;
            g41 = (GAMMA9 + GAMMA90 + GAMMA95) / (MCSUM * Cpc);
            g51 = (GAMMA + GAMMA2 + GAMMA3) / (MCSUM * Cpc);
            g61 = GAMMA / (MWSUM * Cpw) + GAMMA2 / (MWSUM * Cpw2) + GAMMA3 / (MWSUM * Cpw3);
            g71 = GAMMA9 / (MWSUM * Cpw) + GAMMA90 / (MWSUM * Cpw2) + GAMMA95 / (MWSUM * Cpw3);
            g81 = g61;

            for (int i = 0; i < iCRs; i++)
            {
                IMP[i] = ciaInfo[105 - i].dValue;
                FRA[i] = ciaInfo[95 - i].dValue;
                G[i] = FRA[i] * Gv;
                tau[i]=TAU;
                g1v[i]=1.0/tau[i];
                g1m[i]=Wf/Mf;
                K[i]=FRA[i]/(Mf*Cpf);
            }
            j = Complex.ImaginaryOne;

            //int iScale;
            //double dScale;

            chBodePhase.Series.SuspendUpdates();
            chBodeAmp.Series.SuspendUpdates();
            //chNyq.Series.SuspendUpdates();
            chStab.Series.SuspendUpdates();

            //dScale = 3;
            chBodeAmp.ChartAreas[0].AxisX.Minimum = daStVals[4];
            chBodeAmp.ChartAreas[0].AxisX.Maximum = daStVals[5];
            chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[7];
            chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[6];
            chBodeAmp.ChartAreas[0].AxisY.Minimum = daStVals[0];
            chBodeAmp.ChartAreas[0].AxisY.Maximum = daStVals[1];
            chBodeAmp.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[3];
            chBodeAmp.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[2];
            chBodeAmp.Series.Clear();

            //dScale = 3;
            chBodePhase.ChartAreas[0].AxisX.Minimum = daStVals[17];
            chBodePhase.ChartAreas[0].AxisX.Maximum = daStVals[18];
            chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[20];
            chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[19];
            chBodePhase.ChartAreas[0].AxisY.Minimum = daStVals[13];
            chBodePhase.ChartAreas[0].AxisY.Maximum = daStVals[14];
            chBodePhase.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[16];
            chBodePhase.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[15];
            chBodePhase.Series.Clear();

            //dScale = 25;
            //chNyq.ChartAreas[0].AxisX.Minimum = -dScale;
            //chNyq.ChartAreas[0].AxisX.Maximum = dScale;
            //chNyq.ChartAreas[0].AxisY.Minimum = -dScale;
            //chNyq.ChartAreas[0].AxisY.Maximum = dScale;
            //chNyq.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5 * dScale;
            //chNyq.ChartAreas[0].AxisY.LabelStyle.Interval = dScale;
            //chNyq.Series.Clear();

            //dScale = 15;//angle=98.64 at w=0.493
            chStab.ChartAreas[0].AxisX.Minimum = daStVals[30];
            chStab.ChartAreas[0].AxisX.Maximum = daStVals[31];
            chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[33];//
            chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[32];//
            chStab.ChartAreas[0].AxisY.Minimum = daStVals[26];
            chStab.ChartAreas[0].AxisY.Maximum = daStVals[27];
            chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[29];
            chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[28];
            chStab.Series.Clear();

            chBodeAmp.Series.Add("1");
            chBodeAmp.Series[0].ChartType = SeriesChartType.Line;
            chBodePhase.Series.Add("1");
            chBodePhase.Series[0].ChartType = SeriesChartType.Line;
            //chNyq.Series.Add("1");
            //chNyq.Series[0].ChartType = SeriesChartType.Point;
            chStab.Series.Add("1");
            chStab.Series[0].ChartType = SeriesChartType.Point;
            chStab.Series.Add("2");
            chStab.Series[1].ChartType = SeriesChartType.Point;
            chStab.Series[0].MarkerStyle = MarkerStyle.Circle;
            chStab.Series[1].MarkerStyle = MarkerStyle.Circle;
            chStab.Series[0].MarkerSize = 3;
            chStab.Series[1].MarkerSize = 3;

            int iBLLim, iCRm1, iCRm2;
            iCRm1 = iCRs - 1;
            iCRm2 = iCRs - 2;
            //strOut = string.Format("B,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            double dW, dWMax, dWstep, dTheta;
            //strOut = string.Format("B,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //long[] lTimes = new long[21];
            for (int ii = 0; ii < 3; ii++)
            {
                if (ii < 2)
                    iBLLim = 2;
                else
                    iBLLim = 3;
                for (int iBL = 1; iBL < iBLLim; iBL++)
                {
                    switch(ii)
                    {
                        case 0:
                            dW = daStVals[8]; dWMax = daStVals[9]; dWstep = daStVals[10];
                            break;
                        case 1:
                            dW = daStVals[21]; dWMax = daStVals[22]; dWstep = daStVals[23];
                            break;
                        case 2:
                            dW = daStVals[34]; dWMax = daStVals[35]; dWstep = daStVals[36];//dW = 0.1; dWMax = 1000; dWstep = 1e-3;
                            break;
                        default:
                            dW = 1; dWMax = 0; dWstep = 1;  //required by compiler, not used
                            break;
                    }
                    //strOut = string.Format("C,{0}", swTimer.ElapsedMilliseconds);
                    //strmW.WriteLine(strOut);
                    while (dW < dWMax)
                    {
                        //lTimes[0] = swTimer.ElapsedMilliseconds;
                        if (iBLLim == 2)
                            s = j*dW;
                        else
                        {
                            dTheta=dW*Math.PI/2;
                            s = (iBL == 1) ? j * dW : (1.0e-04 * Complex.Exp(j * dTheta));
                        }

                        //lTimes[3] = swTimer.ElapsedMilliseconds;
                        LSUM = 0; Grvpsum = 0; Gpsum = 0; Grtsum = 0; Grpsum = 0; Grvp = 0; Grp = 0; Grvpsum2 = 0; Grpsum3 = 0;
                        for (int i = 0; i < iDNGs; i++)
                            LSUM = LSUM + (F[i] / (s + sLAMBDA[i]));
                        for(int i=0; i<iCRs; i++)
                        {
                            cxTmp = s + g1v[i];
                            cxTmp2 = PHI * IMP[i];
                            cxTmp3 = G[i] / cxTmp;
                            cxTmp4 = g1v[i] / cxTmp;
                            Gvp[i] = cxTmp3;
                            Gvv[i] = cxTmp4;
                            Grvpf[i] = cxTmp2 * cxTmp3;
                            Grvv[i] = cxTmp2 * cxTmp4;
                            Grvpsum2=Grvpsum2+Grvpf[i];
                        }
                        for (int k = 0; k < iCRm2; k++)
                        {
                            int kp1 = k + 1, kp2 = k + 2;
                            Grvpsum = 0;
                            for(int i=kp2; i<iCRs; i++)
                            {
                                Gvvprod=1;
                                for(int jj=kp1; jj<i; jj++)
                                    Gvvprod=Gvvprod*Gvv[jj];
                                Grvpsum=Grvpsum+Grvv[i]*Gvvprod;
                            }
                            Grvp=Grvp+Gvp[kp1]*(Grvv[kp1]+Grvpsum);
                        }
                        Grvp = Grvp + Gvp[iCRm2] * Grvv[iCRm1] + Grvpsum2;
                        Gt = 1;
                        for(int i=0; i < iCRs; i++)
                        {
                            cxTmp = s + g1m[i];
                            cxTmp2 = ALF * IMP[i];
                            cxTmp3 = g1m[i] / cxTmp;
                            cxTmp4 = K[i] / cxTmp;
                            Grpf[i] = cxTmp2 * cxTmp4;
                            Gpf[i] = cxTmp4;
                            Gtf[i] = cxTmp3;
                            Grtf[i] = cxTmp2 * cxTmp3;
                            Gt = Gt * cxTmp3;
                        }
                        for (int i = 0; i < iCRm1; i++)
                        {
                            Gtprod=1;
                            for(int jj=i+1; jj<iCRs; jj++)
                                Gtprod=Gtprod*Gtf[jj];
                            Gpsum=Gpsum+Gpf[i]*Gtprod;
                        }
                        Gp=Gpf[iCRs-1]+Gpsum;
                        for(int i=1; i < iCRs; i++)
                        {
                            Gtprod=1;
                            for(int jj=0; jj < i; jj++)
                                Gtprod=Gtprod*Gtf[jj];
                            Grtsum=Grtsum+Grtf[i]*Gtprod;
                        }
                        Grt=Grtf[0]+Grtsum;
                        for(int i=0; i<iCRs; i++)
                            Grpsum=Grpsum+Grpf[i];
                        for (int k = 0; k < iCRm2; k++)
                        {
                            int kp1 = k + 1, kp2 = k + 2;
                            Grtsum=0;
                            for(int i=kp2; i < iCRs; i++)
                            {
                                Gtprod=1;
                                for (int jj = kp1; jj < i; jj++)
                                    Gtprod = Gtprod * Gtf[jj];
                                Grtsum=Grtsum+Grtf[i]*Gtprod;
                            }
                            Grpsum3=Grpsum3+Gpf[k]*(Grtf[kp1]+Grtsum);
                        }
                        Grp = Grpsum3 + Gpf[iCRm2] * Grtf[iCRm1] + Grpsum;
                        //lTimes[4] = swTimer.ElapsedMilliseconds;
                        //lTimes[5] += (lTimes[4] - lTimes[3]);

                        cxTmp = s + g61 + g71;
                        cxTmp22 = s + g11;
                        cxTmp2 = cxTmp22 + g21;
                        cxTmp3 = s + g81;
                        cxTmp6 = s + g1;
                        cxTmp4 = cxTmp6 + g2;
                        cxTmp5 = s + g3;
                        cxTmp7 = s + g31;
                        cxTmp11 = g51 * g81;
                        cxTmp12 = g41 * g71;
                        cxTmp13 = g41 * g61;
                        cxTmp14 = s + g6 + g7;
                        cxTmp15 = s + g8;
                        cxTmp16 = g5 * g8 * cxTmp14;
                        cxTmp17 = g4 * g7 * cxTmp15;
                        cxTmp18 = g2 * g6;
                        cxTmp19 = g4 * g6;
                        cxTmp20 = g21 * g61;
                        cxTmp28 = g4 + g5;

                        A1n=(cxTmp2)*(s+g61+g71)-cxTmp20;
                        A1d=(cxTmp2)*(cxTmp);
                        A1=A1n/A1d;
                        B1n=cxTmp20+(g11-g21)*(cxTmp);
                        B1=B1n/(cxTmp);
                        C1=g21*g71/(cxTmp);
                        D1d=cxTmp7+g41+g51;
                        D1=1/D1d;
                        E1=cxTmp13/(cxTmp);
                        F1n=cxTmp12*(cxTmp3)+cxTmp11*(cxTmp);
                        F1d=(cxTmp)*(cxTmp3);
                        F1=F1n/F1d;
                        G1n=A1*(1-D1*F1)*(cxTmp2)-D1*E1*C1;
                        G1d=A1*(1-D1*F1)*(cxTmp2);
                        G1f=G1n/G1d;
                        H1n=(g31-g41-g51)*(cxTmp3)*(cxTmp)+cxTmp12*(cxTmp3)+cxTmp11*(cxTmp);
                        H1d=(cxTmp3)*(cxTmp);
                        H1=H1n/H1d;
                        I1d=(cxTmp2)*(cxTmp)*A1;
                        I1=cxTmp13/I1d;
                        J1n=(C1*I1+H1)*D1;
                        J1d=G1f*(1-D1*F1);
                        J1=J1n/J1d;
                        K1d=(cxTmp2)*A1;
                        K1=J1*E1*g11/K1d;
                        L1n=C1*(B1+A1*(cxTmp2));
                        L1d=A1*(cxTmp2);
                        L1=L1n/L1d;
                        M1n=g11*E1*L1*D1;
                        M1d=A1*(cxTmp2)*G1f*(1-D1*F1);
                        An = (cxTmp4) * (cxTmp14) - g2 * g6;
                        Ad = (cxTmp4) * (cxTmp14);
                        A = An / Ad;
                        Bn = g2 * g6 + (g1 - g2) * (cxTmp14);
                        B = Bn / (cxTmp14);
                        C=g2*g7/(cxTmp14);
                        Dd = (s + g3 + cxTmp28);
                        D = 1 / Dd;
                        E=cxTmp19/(cxTmp14);
                        Fn = g4 * g7 * (cxTmp15) + cxTmp16;
                        Fd = (cxTmp14) * (cxTmp15);
                        Ff = Fn / Fd;
                        Gn = A * (1 - D * Ff) * (cxTmp4) - D * E * C;
                        Gd = A * (1 - D * Ff) * (cxTmp4);
                        Gf = Gn / Gd;
                        Hn = (g3 - g4 - g5) * (cxTmp15) * (cxTmp14) + g4 * g7 * (cxTmp15) + cxTmp16;
                        Hd = (cxTmp15) * (cxTmp14);
                        H = Hn / Hd;
                        Id = (cxTmp4) * (cxTmp14) * A;
                        I = g4 * g6 / Id;
                        Jn = (C * I + H) * D;
                        Jd = Gf * (1 - D * Ff);
                        J = Jn / Jd;
                        Kd = (cxTmp4) * A;
                        Kf = g1 * J * E / (cxTmp6 + g2) * A;
                        Ln = C * (B + A * (cxTmp6 + g2));
                        Ld = A * (cxTmp6 + g2);
                        L = Ln / Ld;
                        Md = A * (cxTmp6 + g2) * Gf * (1 - D * Ff);
                        M = g1 * E * L * D / Md;

                        G1=P0/(s*((LAMBDA/BETA)+LSUM)-R0);
                        M1 = g11 * E1 * L1 * D1 / (A1 * (cxTmp2)*G1 *(1-D1*F1));     //was not in calc
                        G2 = Grvp;
                        G5=Gp;
                        G20=ALF2*g11*A1*E1*D1*(1-D1*F1)/(G1f*(cxTmp2));
                        G9=(K1+g11*I1)/(cxTmp7);
                        G10=g31*J1/(cxTmp7);
                        G11 = g1p / (s + g1p);
                        G12 = G11;
                        G17 = G11;
                        G18 = G11;
                        G13=((cxTmp4)*A*M+g1*B)/((cxTmp6)*(cxTmp4)*A);
                        G14=g3*D*L/((cxTmp6)*Gf*(1-D*Ff));
                        G15=(Kf+g1*I)/(cxTmp5);
                        G16=g3*J/(cxTmp5);
                        // G19=1 ;
                        G19=0;
                        GA=G17*G18*G19/(1-G16*G17*G18*G19);
                        GB=G13+G14*G15*GA;
                        GC=G11*G12*GB/(1-G10*G11*G12*GB);
                        G21=ALF2*g31*D1*(1-D1*F1)/G1;
                        G3=Grp+G5*(G20+G9*GC*G21);
                        G6=Gt;                                                      //was out of order
                        G4=Grt+G6*(G20+G9*GC*G21);
                        G7=((cxTmp2)*A1*M1+g11*B1)/((cxTmp22)*(cxTmp2)*A1);
                        G8=g31*D1*L1/((cxTmp22)*G1f*(1-D1*F1));
                        G9=(K1+g11*I1)/(cxTmp7);
                        GD=(G7+G8*G9*GC)/(1-G6*(G7+G8*G9*GC));
                        HT=G2+G3+G4*G5*GD;
                        StabGT=-G1*HT;
                        GT = G1 / (1 - G1 * HT);

                        double BAGT = Complex.Abs(GT);
                        double BPGT = GT.Phase;
                        double dVal=0.0;
                        switch (ii)
                        {
                            case 0:
                                if (daStVals[11] < 0.5)
                                    dVal = dW;
                                else
                                    dVal = Math.Log10(dW);
                                chBodeAmp.Series[0].Points.AddXY(dVal, BAGT);
                                break;
                            case 1:
                                if (daStVals[24] < 0.5)
                                    dVal = dW;
                                else
                                    dVal = Math.Log10(dW);
                                chBodePhase.Series[0].Points.AddXY(dVal, BPGT);
                                break;
                            case 2:
                                UC = Complex.Exp(j * dW / dWMax * Math.PI * 2);
                                chStab.Series[0].Points.AddXY(UC.Real, UC.Imaginary);
                                //chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);//new plot needed --> chStab
                                chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);
                                break;
                        }
                        dW += dWstep;
                        //lTimes[1] = swTimer.ElapsedMilliseconds;
                        //lTimes[2] += (lTimes[1] - lTimes[0]);
                    }//while
                }//iBL for loop
            }//plot for loop
            //strOut = string.Format("C1,{0}", lTimes[2]);
            //strmW.WriteLine(strOut);
            //strOut = string.Format("C2,{0}", lTimes[5]);
            //strmW.WriteLine(strOut);
            //strOut = string.Format("C,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            chBodeAmp.Series.ResumeUpdates();
            chBodePhase.Series.ResumeUpdates();
            chStab.Series.ResumeUpdates();
            //strOut = string.Format("D,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //swTimer.Stop();
            //strmW.Close();
            return true;
        }

        private bool AD125StabCalcs()
        {
            //StreamWriter strmW = new StreamWriter("TimeOut.csv", false);
            //string strOut;
            //swTimer = new Stopwatch();
            //swTimer.Start();
            //strOut = string.Format("A,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //StreamWriter sw;
            int iCRs = 10;
            int iDNGs = 6;

            double[] daAl = new double[iDNGs];
            double[] daF = new double[iDNGs];
            double[] dIMP = new double[iCRs];
            double[] dFRA = new double[iCRs];
            double[] dG = new double[iCRs];
            double[] dTau = new double[iCRs];
            double[] dg1v = new double[iCRs];
            double[] dpf = new double[iCRs];
            double[] dg1m = new double[iCRs];
            double[] dK = new double[iCRs];
            double dTheta, dWMax, dW, dWstep, dBeta, dLAMBDA, dAlf, dPhi, dR0, dAlf2,dMT1,dGAM1,dGAM2,dGAM3, dMS1, dWCS2, dMT, dCPT, dMS, dCPS;
            double dGv, dPo, dGAMMA9, dGAMMA90, dGAMMA95, dMW, dMW2, dMW3, dMWSUM, dMCSUM,dMCL,dMCL2,dMCL3, dWc, dWc2, dWc3, dELM;
            double dTAU, dCpf, dEM, dWF, dGAMMA,dGAMMA2,dGAMMA3, dCpc, dCpw, dCpw2, dCpw3, dWp, dM1p,dTWC;
            double g1p, g1, g2, g3, g4, g5, g6, g7, g8, g11, g21, g31, g41, g51, g61, g71, g81;
            double BAGT, BPGT;
            double NicA, NicP;
            //double BAG1,BAG2,BAG3,BAG4,BAG5,BAG6,BAG7,BAG8,BAG9,BAG10,BAG11,BAG12,BAG13,BAG14,BAG15,BAG16,BAG17,BAG18,BAG19,BAG20,BAG21,BAGA,BAGB,BAGC,BAGD,BAHT;
            //double BPG1,BPG2,BPG3,BPG4,BPG5,BPG6,BPG7,BPG8,BPG9,BPG10,BPG11,BPG12,BPG13,BPG14,BPG15,BPG16,BPG17,BPG18,BPG19,BPG20,BPG21,BPGA,BPGB,BPGC,BPGD,BPHT;

            Complex cxTmp, cxTmp2, cxTmp3, cxTmp4, cxTmp5, cxTmp6, cxTmp7, cxTmp8, cxTmp9, cxTmp10, cxTmp11, cxTmp12, cxTmp13, cxTmp14, cxTmp15, cxTmp16, cxTmp17, cxTmp18, cxTmp19, cxTmp20, cxTmp21, cxTmp22, cxTmp23, cxTmp24, cxTmp25, cxTmp26, cxTmp27, cxTmp28;
            Complex Nic1, Nic2, Nic;
            Complex[] Gvp = new Complex[iCRs], Gvv = new Complex[iCRs], Grvpf = new Complex[iCRs], Grvpf1 = new Complex[iCRs], Grvv = new Complex[iCRs]; 
            Complex[] Grvpf2 = new Complex[iCRs], Grvvsum = new Complex[iCRs], Grvvsum2 = new Complex[iCRs], Gvpsum = new Complex[iCRs];
            Complex[] Grpf = new Complex[iCRs], Gpf = new Complex[iCRs], Gtf = new Complex[iCRs], Grtf = new Complex[iCRs], Gpfsumf = new Complex[iCRs];
            Complex[] Grtsumf = new Complex[iCRs], Grpsum2 = new Complex[iCRs];
            Complex s,cxJ, LSUM,Grvpsum,Gvvprod,Grvp,Gt,Gtprod,Gpsum,Gp,Grpsum3,Grp,Grtsum,Grt,Grpsum,Grvpsum2;
            Complex A1n,A1d,A1,B1n,B1,C1,D1d,D1,E1,F1n,F1d,F1,G1n,G1d,G1f,H1n,H1d,H1,I1d,I1,J1n,J1d,J1,K1d,K1,L1n,L1d,L1,M1n,M1d,M1;
            Complex An,Ad,A,Bn,B,C,Dd,D,E,Fn,Fd,Ff,Gn,Gd,Gf,Hn,Hd,H,Id,I,Jn,Jd,J,Kd,Kf,Ln,Ld,L,Md,M,GA,GB,GC,GD;
            Complex G1,G2,G3,G4,G5,G6,G7,G8,G9,G10,G11,G12,G13,G14,G15,G16,G17,G18,G19,G20,G21,HT,GT,StabGT,UC;

            //dLAMBDA=6.810e-05;
            //dBeta = 0.00794; 
            //dGv=5.0e-05;
            //dPhi=-3061;
            //dTAU=0.7835;
            //dWF=0.4912;
            //dCpf=0.0036;
            //dEM=3.3193;
            //dAlf=-0.0413;
            //Wl=0.4912;
            //dELM=0.40128;
            //double GAMMAl=0.002;
            //dWc=0.7751;
            //double Mc = 0.2603;
            //double GAMMAc = 0.0005;
            //dCpc=0.0042;
            //double GAMMAsw = 0.004921;
            //double Mw = 4.4921;
            //dCpw=0.0003;
            //double Msw = 4.4921;
            //double Cpsw = 0.0003; 
            //dWp = 0.7751; 
            //dM1p = 0.2; 
            //dR0 = -0.36;

            for (int i = 0; i < iDNGs; i++)
            {
                daAl[i] = ciaInfo[i * 2 + 10].dValue;
                daF[i] = ciaInfo[i * 2 + 11].dValue;
            }
            dGv = 5e-5;
            dAlf2 = -.0003;                     //Computed value
            dTAU = ciaInfo[131].dValue;
            dEM = ciaInfo[174].dValue;          //mass of fuel for 1 core level (EM)
            dELM = ciaInfo[174].dValue;          //BL mass of fuel for 1 core level (EM)
            dCpf = ciaInfo[41].dValue;          //CV
            dBeta = ciaInfo[139].dValue;        //BETA
            dLAMBDA = ciaInfo[138].dValue;      //MNT in model, mean neutron generation time
            dAlf = -ciaInfo[109].dValue;        //-ALF
            dPhi = -100 * ciaInfo[110].dValue;  //-100*PHI
            dR0 = ciaInfo[137].dValue;          //A
            dTWC = ciaInfo[104].dValue * 1.01;    //TWC increased by 1%, as flow components are in model - ignores possibiity of coolant being shut off
            dMCL = ciaInfo[89].dValue;          //MCLUMP
            dMCL2 = ciaInfo[90].dValue;         //MCLUMP2
            dMCL3 = ciaInfo[91].dValue;         //MCLUMP3
            dCpw = ciaInfo[61].dValue;          //CPW
            dCpw2 = ciaInfo[71].dValue;          //CPW2
            dCpw3 = ciaInfo[83].dValue;          //CPW3

            dCpc = GetSSVal(202);               //CPC
            dWc = GetSSVal(54);                   //WC
            dWc2 = GetSSVal(55);                   //WC2
            dWc3 = GetSSVal(56);                   //WC3
            dWF = GetSSVal(1025);              //total core flow (WF)
            dPo = GetSSVal(47) / 1000;            //kw/1000
            dGAMMA = GetSSVal(75);             //GAMMA
            dGAMMA2 = GetSSVal(316);           //GAMMA2
            dGAMMA3 = GetSSVal(458);           //GAMMA3
            dGAMMA9 = GetSSVal(76);             //GAMMA9
            dGAMMA90 = GetSSVal(317);           //GAMMA90
            dGAMMA95 = GetSSVal(459);           //GAMMA95
            dMW = GetSSVal(1);                  //Mass cooling wall, inner
            dMW2 = GetSSVal(2);                 //Mass cooling wall, tubes
            dMW3 = GetSSVal(3);                 //Mass cooling wall, outer
            dMT1 = GetSSVal(263);               //HX tube side fluid mass
            dGAM1 = GetSSVal(265);              //gam used in HX primary
            dGAM2 = GetSSVal(271);              //gam used in HX secondary
            dGAM3 = GetSSVal(272);              //gam used at HX secondary wall
            dMS1 = GetSSVal(269);               //HX sec fluid mass
            dWCS2 = GetSSVal(268);              //HX sec flow
            dMT = GetSSVal(273);                //HX tube mass
            dCPT = GetSSVal(274);               //Cp of HX tubes
            dMS = GetSSVal(275);                //HX Shell mass
            dCPS = 5e-4;        //Cp of HX sec shell wall, can't use model variable

            for (int i = 0; i < iCRs; i++)
            {
                dIMP[i] = ciaInfo[130 - i].dValue;
                dFRA[i] = ciaInfo[120 - i].dValue;
                dG[i] = dFRA[i] * dGv;
                dTau[i]=dTAU;
                dg1v[i]=1/dTau[i];
                dg1m[i] = dWF / dEM;
                dK[i] = dFRA[i] / (dEM * dCpf);
            }
            dMWSUM = dMW + dMW2 + dMW3;
            dMCSUM = dMCL + dMCL2 + dMCL3;

            g1 = 2 * (dWc + dWc2 + dWc3) / dMT1;
            g2 = dGAM1 / dMT1 / dCpc;
            g3 = 2 * dWCS2 / dMS1;
            g4 = dGAM2 / dMS1 / dCpc;
            g5 = dGAM3 / dMS1 / dCpc;
            g6 = dGAM1 / dMT / dCPT;
            g7 = dGAM2 / dMT / dCPT;
            g8 = dGAM3 / dMS / dCPS;

            g11 = 2 * dWF / dELM;
            g21 = (dGAMMA + dGAMMA2 + dGAMMA3) / (dELM * dCpf);
            g31 = 2 * (dWc + dWc2 + dWc3) / dMCSUM;
            g41 = (dGAMMA9 + dGAMMA90 + dGAMMA95) / (dMCSUM * dCpc);
            g51 = (dGAMMA + dGAMMA2 + dGAMMA3) / (dMCSUM * dCpc);
            g61 = dGAMMA / (dMWSUM * dCpw) + dGAMMA2 / (dMWSUM * dCpw2) + dGAMMA3 / (dMWSUM * dCpw3);
            g71 = dGAMMA9 / (dMWSUM * dCpw) + dGAMMA90 / (dMWSUM * dCpw2) + dGAMMA95 / (dMWSUM * dCpw3);
            g81 = g61;
            dPo = 63.962 * 0.001;
            //g1 = 1.33; g2 = 4.90; g3 = 1.33; g4 = 2.45; g5 = 2.45; g6 = 1.33; g7 = 1.33; g8 = 0.666;

            //g11=2*dWF/dELM;
            //g21 = GAMMAl / (dELM * dCpf);
            //g31=2*dWc/Mc;
            //g41=GAMMAc/(Mc*dCpc);
            //g51=GAMMAsw/(Mc*dCpc);
            //g61=GAMMAl/(Mw*dCpw);
            //g71=GAMMAc/(Mw*dCpw);
            //g81 = GAMMAsw / (Msw * Cpsw);

            dWp = 0.2161;
            dM1p = 0.2;
            g1p = dWp / dM1p;
            //dAlf2 = -.0003;

            cxJ = Complex.ImaginaryOne;

            int iScale;
            double dScale;

            chBodePhase.Series.SuspendUpdates();
            //chBodePhase2.Series.SuspendUpdates();
            chBodeAmp.Series.SuspendUpdates();
            //chBodeAmp2.Series.SuspendUpdates();
            //chNyq.Series.SuspendUpdates();
            chStab.Series.SuspendUpdates();
            
            dScale = 4;
            chBodeAmp.ChartAreas[0].AxisX.Minimum = daStVals[4];// - dScale;
            chBodeAmp.ChartAreas[0].AxisX.Maximum = daStVals[5];// dScale;
            chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[7];
            chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[6];
            chBodeAmp.ChartAreas[0].AxisY.Minimum = daStVals[0];// -dScale;
            chBodeAmp.ChartAreas[0].AxisY.Maximum = daStVals[1];// dScale;
            chBodeAmp.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[3];//0.5*dScale;
            chBodeAmp.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[2];//dScale;
            chBodeAmp.Series.Clear();

            dScale = 4;
            chBodePhase.ChartAreas[0].AxisX.Minimum = daStVals[17];//
            chBodePhase.ChartAreas[0].AxisX.Maximum = daStVals[18];//
            chBodeAmp.ChartAreas[0].AxisX.MajorGrid.Interval = daStVals[20];
            chBodeAmp.ChartAreas[0].AxisX.LabelStyle.Interval = daStVals[19];
            chBodePhase.ChartAreas[0].AxisY.Minimum = daStVals[13];//
            chBodePhase.ChartAreas[0].AxisY.Maximum = daStVals[14];//
            chBodePhase.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[16];//
            chBodePhase.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[15];//
            chBodePhase.Series.Clear();

            //dScale = 25;
            //chNyq.ChartAreas[0].AxisX.Minimum = -dScale;
            //chNyq.ChartAreas[0].AxisX.Maximum = dScale;
            //chNyq.ChartAreas[0].AxisY.Minimum = -dScale;
            //chNyq.ChartAreas[0].AxisY.Maximum = dScale;
            //chNyq.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5 * dScale;
            //chNyq.ChartAreas[0].AxisY.LabelStyle.Interval = dScale;
            //chNyq.Series.Clear();

            dScale = 15;//angle=98.64 at w=0.493
            chStab.ChartAreas[0].AxisX.Minimum = daStVals[30];//
            chStab.ChartAreas[0].AxisX.Maximum = daStVals[31];//
            chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[33];//
            chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[32];//
            chStab.ChartAreas[0].AxisY.Minimum = daStVals[26];//
            chStab.ChartAreas[0].AxisY.Maximum = daStVals[27];//
            chStab.ChartAreas[0].AxisY.MajorGrid.Interval = daStVals[29];//
            chStab.ChartAreas[0].AxisY.LabelStyle.Interval = daStVals[28];//
            chStab.Series.Clear();

            chBodeAmp.Series.Add("1");
            chBodeAmp.Series[0].ChartType = SeriesChartType.Line;
            //chBodeAmp2.Series.Add("1");
            //chBodeAmp2.Series[0].ChartType = SeriesChartType.Line;
            chBodePhase.Series.Add("1");
            chBodePhase.Series[0].ChartType = SeriesChartType.Line;
            //chBodePhase2.Series.Add("1");
            //chBodePhase2.Series[0].ChartType = SeriesChartType.Line;
            //chNyq.Series.Add("1");
            //chNyq.Series[0].ChartType = SeriesChartType.Point;
            //chNyq.Series.Add("2");
            //chNyq.Series[1].ChartType = SeriesChartType.Point;
            chStab.Series.Add("1");
            chStab.Series[0].ChartType = SeriesChartType.Point;
            chStab.Series.Add("2");
            chStab.Series[1].ChartType = SeriesChartType.Point;
            chStab.Series[0].MarkerStyle = MarkerStyle.Circle;
            chStab.Series[1].MarkerStyle = MarkerStyle.Circle;
            chStab.Series[0].MarkerSize = 3;
            chStab.Series[1].MarkerSize = 3;

            //StreamWriter sw;
            int iBLLim, iCRm1, iCRm2;
            iCRm1 = iCRs - 1;
            iCRm2 = iCRs - 2;
            //strOut = string.Format("B,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            for (int ii = 0; ii < 3; ii++)
            {
                if (ii < 2)
                    iBLLim = 2;
                else
                    iBLLim = 3;
                for (int iBL = 1; iBL < iBLLim; iBL++)
                {
                    switch(ii)
                    {
                        case 0:
                            dW = daStVals[8]; dWMax = daStVals[9]; dWstep = daStVals[10];
                            break;
                        case 1:
                            dW = daStVals[21]; dWMax = daStVals[22]; dWstep = daStVals[23];
                            break;
                        case 2:
                            dW = daStVals[34]; dWMax = daStVals[35]; dWstep = daStVals[36];//dW = 0.1; dWMax = 1000; dWstep = 1e-3;
                            break;
                        case 3:
                            dW = -1; dWMax = 1; dWstep = 1e-4;
                            break;
                        default:
                            dW = 1; dWMax = 0; dWstep = 1;  //required by compiler, not used
                            break;
                    }
                    //strOut = string.Format("C,{0}", swTimer.ElapsedMilliseconds);
                    //strmW.WriteLine(strOut);
                    while (dW < dWMax)
                    {
                        if (iBLLim == 2)
                            s = cxJ*dW;
                        else
                        {
                            dTheta=dW*Math.PI/2;
                            s = (iBL == 1) ? cxJ * dW : (1.0e-04 * Complex.Exp(cxJ * dTheta));
                        }
                        LSUM = 0;
                        for (int k = 0; k < iDNGs; k++)
                            LSUM += (daF[k] / (s + daAl[k]));
                        Grvpsum = 0;
                        Gpsum = 0;
                        Grtsum = 0;
                        Grpsum = 0;
                        Grvp = 0;
                        Grp = 0;
                        Grvpsum2 = 0;
                        Grpsum3 = 0;
                        //G1
                        //G2
                        for (int i=0; i<iCRs; i++)
                        {
                            cxTmp = s + dg1v[i];
                            cxTmp2 = dPhi * dIMP[i];
                            cxTmp3 = dG[i] / cxTmp;
                            cxTmp4 = dg1v[i] / cxTmp;
                            Gvp[i] = cxTmp3;
                            Gvv[i] = cxTmp4;
                            Grvpf[i] = cxTmp2 * cxTmp3;
                            Grvv[i] = cxTmp2 * cxTmp4;
                            Grvpsum2 = Grvpsum2 + Grvpf[i];
                        } 
                        
                        for (int k=0; k <iCRm2; k++)
                        {
                            Grvpsum=0;
                            for (int i=k+2; i<iCRs; i++)
                            {
                                Gvvprod=1;
                                for (int jj=k+1; jj<i; jj++)    //removed -1 from upper limit
                                {
                                    Gvvprod=Gvvprod*Gvv[jj];
                                }
                                Grvpsum=Grvpsum+Grvv[i]*Gvvprod;
                            }
                            Grvp=Grvp+Gvp[k+1]*(Grvv[k+1]+Grvpsum);
                        }
                        Grvp=Grvp+Gvp[iCRm2]*Grvv[iCRm1]+Grvpsum2;
                        // In Core Multi-Region; Grp,Gp,Gt,Grt
                        Gt = 1;
                        for(int i=0; i <iCRs; i++)
                        {
                            cxTmp = s+dg1m[i];
                            cxTmp2 = dAlf * dIMP[i];
                            cxTmp3 = dg1m[i] / cxTmp;
                            cxTmp4 = dK[i] / cxTmp;
                            Grpf[i] = cxTmp2 * cxTmp4;
                            Gpf[i] = cxTmp4;
                            Gtf[i] = cxTmp3;
                            Grtf[i] = cxTmp2 * cxTmp3;
                            Gt = Gt * cxTmp3;
                        }
                        for(int i=0; i <iCRm1; i++)
                        {
                            Gtprod=1;
                            for (int jj=i+1; jj<iCRs; jj++)
                            {
                                Gtprod=Gtprod*Gtf[jj];
                            }
                            Gpsum=Gpsum+Gpf[i]*Gtprod;
                        }
                        Gp=Gpf[iCRm1]+Gpsum;
                        for (int i=1; i<iCRs; i++)
                        {
                            Gtprod=1;
                            for(int jj=0; jj<i; jj++)   // removed -1 from UL
                            {
                                Gtprod=Gtprod*Gtf[jj];
                            }
                            Grtsum=Grtsum+Grtf[i]*Gtprod;
                        }
                        Grt=Grtf[0]+Grtsum;
                        for (int i=0; i<iCRs; i++)
                        {
                            Grpsum=Grpsum+Grpf[i];
                        }
                        for(int k=0; k<iCRm2; k++)
                        {
                            Grtsum=0;
                            for(int i=k+2; i< iCRs; i++)
                            {
                                Gtprod=1;
                                for(int jj=k+1; jj<i; jj++) 
                                {
                                    Gtprod=Gtprod*Gtf[jj];
                                }
                                Grtsum=Grtsum+Grtf[i]*Gtprod;
                            }
                            Grpsum3=Grpsum3+Gpf[k]*(Grtf[k+1]+Grtsum);
                        }
                        Grp=Grpsum3+Gpf[iCRm2]*Grtf[iCRm1]+Grpsum;

                        cxTmp = s + g61 + g71;
                        cxTmp22 = s + g11;
                        cxTmp2 = cxTmp22 + g21;
                        cxTmp3 = s + g81;
                        cxTmp6 = s + g1;
                        cxTmp4 = cxTmp6 + g2;
                        cxTmp5 = s + g3;
                        cxTmp7 = s + g31;
                        cxTmp11 = g51 * g81;
                        cxTmp12 = g41 * g71;
                        cxTmp13 = g41 * g61;
                        cxTmp14 = s + g6 + g7;
                        cxTmp15 = s + g8;
                        cxTmp16 = g5 * g8 * cxTmp14;
                        cxTmp17 = g4 * g7 * cxTmp15;
                        cxTmp18 = g2 * g6;
                        cxTmp19 = g4 * g6;
                        cxTmp20 = g21 * g61;
                        cxTmp28 = g4 + g5;

                        // Thermal Groups
                        A1d=(cxTmp2)*(cxTmp);
                        A1n = A1d - cxTmp20;
                        A1=A1n/A1d;
                        cxTmp23 = cxTmp2 * A1;
                        B1n=cxTmp20+(g11-g21)*(cxTmp);
                        B1=B1n/(cxTmp);
                        C1=g21*g71/(cxTmp);
                        D1d=cxTmp7+g41+g51;
                        D1=1/D1d;
                        E1=cxTmp13/(cxTmp);
                        F1n=cxTmp12*(cxTmp3)+cxTmp11*(cxTmp);
                        F1d=(cxTmp)*(cxTmp3);
                        F1=F1n/F1d;
                        cxTmp21 = 1 - D1 * F1;
                        G1d = cxTmp23 * cxTmp21;
                        G1n = G1d - D1 * E1 * C1;
                        G1f=G1n/G1d;
                        H1d=(cxTmp3)*(cxTmp);
                        H1n = (g31 - g41 - g51) * H1d + F1n;
                        H1=H1n/H1d;
                        I1d = cxTmp23 * (cxTmp);
                        I1=cxTmp13/I1d;
                        J1n=(C1*I1+H1)*D1;
                        J1d=G1f*cxTmp21;
                        J1=J1n/J1d;
                        K1d = cxTmp23;
                        K1=J1*E1*g11/K1d;
                        L1d=A1*(cxTmp2);
                        L1n = C1 * (B1 + L1d);
                        L1=L1n/L1d;
                        M1n=g11*E1*L1*D1;
                        M1d = G1d * G1f;
                        Ad=(cxTmp4)*(cxTmp14);
                        An=Ad-cxTmp18;
                        A=An/Ad;
                        Bn=cxTmp18+(g1-g2)*(cxTmp14);
                        B=Bn/(cxTmp14);
                        C=g2*g7/(cxTmp14);
                        Dd = (cxTmp5 + cxTmp28);
                        D=1/Dd;
                        E=cxTmp19/(cxTmp14);
                        Fn=cxTmp17 + cxTmp16;
                        Fd=cxTmp14*cxTmp15;
                        Ff=Fn/Fd;
                        cxTmp26 = 1 - D * Ff;
                        cxTmp25 = cxTmp4*A;
                        Gd = cxTmp25 * (cxTmp26);
                        Gn = Gd - D * E * C;
                        Gf=Gn/Gd;
                        Hn = (g3 - cxTmp28) * Fd + Fn;
                        H = Hn / Fd;
                        Id = cxTmp25 * (cxTmp14);
                        I=cxTmp19/Id;
                        Jn=(C*I+H)*D;
                        Jd=Gf*(cxTmp26);
                        J=Jn/Jd;
                        Kd = cxTmp25;
                        Kf=g1*J*E/(cxTmp4)*A;
                        Ln = C * (B + cxTmp25);
                        Ld = cxTmp25;
                        L=Ln/Ld;
                        Md = cxTmp25 * Gf * (cxTmp26);
                        M=g1*E*L*D/Md;
                        // Transfer Functions
                        G1=dPo/(s*((dLAMBDA/dBeta)+LSUM)-dR0);
                        M1 = g11 * E1 * L1 * D1 / (cxTmp23 * G1 * (cxTmp21));
                        G2=Grvp;
                        G5=Gp;
                        cxTmp27 = dAlf2*D1;
                        G20 = cxTmp27 * g11 * A1 * E1 * cxTmp21 / (G1f * cxTmp2);
                        G9=(K1+g11*I1)/cxTmp7;
                        G10=g31*J1/cxTmp7;
                        G11=g1p/(s+g1p);
                        G12=G11;
                        G17=G11;
                        G18=G11;
                        G13 = (cxTmp25 * M + g1 * B) / (cxTmp6 * cxTmp25);
                        G14=g3*D*L/(cxTmp6*Gf*cxTmp26);
                        G15=(Kf+g1*I)/(cxTmp5);
                        G16=g3*J/(cxTmp5);
                        // G19=1; 
                        G19=0;
                        cxTmp9 = G17 * G18 * G19;
                        GA = cxTmp9 / (1 - G16 * cxTmp9);
                        GB=G13+G14*G15*GA;
                        cxTmp10 = G11 * G12 * GB;
                        GC = cxTmp10 / (1 - G10 * cxTmp10);
                        G21 = cxTmp27 * g31 * cxTmp21 / G1;
                        cxTmp24 = G20 + G9 * GC * G21;
                        G3=Grp+G5*cxTmp24;
                        G6=Gt;
                        G4=Grt+G6*cxTmp24;
                        G7 = (cxTmp23 * M1 + g11 * B1) / (cxTmp22 * cxTmp23);
                        G8=g31*D1*L1/(cxTmp22*G1f*cxTmp21);
                        cxTmp8 = G7 + G8 * G9 * GC;
                        GD = cxTmp8 / (1 - G6 * cxTmp8);
                        HT=G2+G3+G4*G5*GD;
                        StabGT=-G1*HT;
                        GT = G1 / (1 + StabGT);
                        Nic1 = G5 / Grp;
                        Nic2 = 1 + Nic1 * cxTmp24;             //not used, but shows the 1+GH form
                        Nic = Nic1 * cxTmp24;                          //This is GH
                        NicA = Complex.Abs(-Nic);
                        NicP = (-Nic).Phase;
                        //BAG1 = Complex.Abs(G1);
                        //BAG2 = Complex.Abs(G2);
                        //BAG3 = Complex.Abs(G3);
                        //BAG4 = Complex.Abs(G4);
                        //BAG5 = Complex.Abs(G5);
                        //BAG6 = Complex.Abs(G6);
                        //BAG7 = Complex.Abs(G7);
                        //BAG8 = Complex.Abs(G8);
                        //BAG9 = Complex.Abs(G9);
                        //BAG10 = Complex.Abs(G10);
                        //BAG11 = Complex.Abs(G11);
                        //BAG12 = Complex.Abs(G12);
                        //BAG13 = Complex.Abs(G13);
                        //BAG14 = Complex.Abs(G14);
                        //BAG15 = Complex.Abs(G15);
                        //BAG16 = Complex.Abs(G16);
                        //BAG17 = Complex.Abs(G17);
                        //BAG18 = Complex.Abs(G18);
                        //BAG19 = Complex.Abs(G19);
                        //BAG20 = Complex.Abs(G20);
                        //BAG21 = Complex.Abs(G21);
                        //BAGA = Complex.Abs(GA);
                        //BAGB = Complex.Abs(GB);
                        //BAGC = Complex.Abs(GC);
                        //BAGD = Complex.Abs(GD);
                        //BAHT = Complex.Abs(HT);
                        BAGT = Complex.Abs(GT);
                        //if (ii == 0)
                        //{
                        //    sw = new StreamWriter("C:\\mydesire\\plot.txt", false);

                        //    string str = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19} {20} {21} {22}\n", BAGT, GT.Real, GT.Imaginary, G1.Real, G1.Imaginary, HT.Real, HT.Imaginary,G2.Real,G2.Imaginary,G3.Real,G3.Imaginary,G4.Real,G4.Imaginary,G5.Real,G5.Imaginary,GD.Real,GD.Imaginary,dPo,dLAMBDA,dBeta,LSUM.Real,LSUM.Imaginary,dR0);
                        //    sw.WriteLine(str);
                        //    sw.Close();
                        //}
                        //BPG1 = G1.Phase;
                        //BPG2 = G2.Phase;
                        //BPG3 = G3.Phase;
                        //BPG4 = G4.Phase;
                        //BPG5 = G5.Phase;
                        //BPG6 = G6.Phase;
                        //BPG7 = G7.Phase;
                        //BPG8 = G8.Phase;
                        //BPG9 = G9.Phase;
                        //BPG10 = G10.Phase;
                        //BPG11 = G11.Phase;
                        //BPG12 = G12.Phase;
                        //BPG13 = G13.Phase;
                        //BPG14 = G14.Phase;
                        //BPG15 = G15.Phase;
                        //BPG16 = G16.Phase;
                        //BPG17 = G17.Phase;
                        //BPG18 = G18.Phase;
                        //BPG19 = G19.Phase;
                        //BPG20 = G20.Phase;
                        //BPG21 = G21.Phase;
                        //BPGA = GA.Phase;
                        //BPGB = GB.Phase;
                        //BPGC = GC.Phase;
                        //BPGD = GD.Phase;
                        //BPHT = HT.Phase;
                        BPGT = GT.Phase;
                        double dVal=0.0;
                        switch (ii)
                        {
                            case 0:
                                if (daStVals[11] < 0.5)
                                    dVal = dW;
                                else
                                    dVal = Math.Log10(dW);
                                chBodeAmp.Series[0].Points.AddXY(dVal, BAGT);
                                break;
                            case 1:
                                if (daStVals[24] < 0.5)
                                    dVal = dW;
                                else
                                    dVal = Math.Log10(dW);
                                chBodePhase.Series[0].Points.AddXY(dVal, BPGT);
                                break;
                            case 2:
                                UC = Complex.Exp(cxJ * dW / dWMax * Math.PI * 2);
                                chStab.Series[0].Points.AddXY(UC.Real, UC.Imaginary);
                                //chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);//new plot needed --> chStab
                                chStab.Series[1].Points.AddXY(StabGT.Real, StabGT.Imaginary);
                                break;
                            //case 3:
                                //chNyq.Series[0].Points.AddXY(NicP, Math.Log10(NicA));
                                //break;
                        }

                        //An = (s+g1+g2)*(s+g6+g7)-g2*g6;
                        //Ad = (s+g1+g2)*(s+g6+g7);
                        //A = An/Ad;
                        //Bn = (g2*g6+(g1-g2)*(s+g6+g7));
                        //Bd = s+g6+g7;
                        //B = Bn/Bd;
                        //C = g2*g7/(s+g6+g7);
                        //D = 1/(s+g3+g4+g5);
                        //E = g4 * g6 / (s + g6 + g7);
                        //Fn = g4 * g7 * (cxTmp15) + g5 * g8 * (s + g6 + g7);
                        //Fd = (s+g6+g7)*(s+g8);
                        //F = Fn/Fd;
                        //Gn = A*(1-D*F)*(s+g1+g2)-D*E*C;
                        //Gd = A*(1-D*F)*(s+g1+g2);
                        //G = Gn/Gd;
                        //Hn = (g3-g4-g5)*(s+g8)*(s+g6+g7)+g4*g7*(s+g8)+g5*g8*(s+g6+g7);
                        //Hd = (s+g8)*(s+g6+g7);
                        //H = Hn/Hd;
                        //Id = (s+g1+g2)*(s+g6+g7)*A;
                        //I = g4*g6/Id;
                        //J = (C*I+H)*D/(G*(1-D*F));
                        //K = g1*J*E/((s+g1+g2)*A);
                        //Ln = C*(B+A*(s+g1+g2));
                        //Ld = A*(s+g1+g2);
                        //L = Ln/Ld;
                        //Md = A*(s+g1+g2)*G*(1-D*F);
                        //M = g1*E*D/Md;
                        //Afn = (s+g1f)*(s+g2f+g3f)-g1f*g2f;
                        //Afd = (s+g1f)*(s+g2f+g3f);
                        //Af = Afn/Afd;
                        //Bf = g1f/(s+g2f+g3f);
                        //Cf = g5f/(s+g2f+g3f);
                        //Df = g2f*g5f/Afn;
                        //Efn = s+g4f+g5f-g3f*(Df*Bf+Cf);
                        //Ef = Efn/(s+g4f+g5f);
                        //Gf = 1/Efn;
                        //Hf = dKf*(1+g3f*Bf*Gf*Df);
                        //If = g5f/(s+g2f+g3f);
                        //Jfn = (g4f-g5f)*(s+g2f+g3f)+g3f*g5f;
                        //Jf = Jfn/(s+g2f+g3f);
                        //Lf = g2f*If/(Af*(s+g1f));
                        //Mf = g4f*Gf*(Jf+g3f+Lf*Bf);
                        //Nf = dKf*Df*(Jf*Gf+Hf);
                        //RSUM = 0 * cxJ;
                        //R = (dLAMBDA/dBeta)+RSUM;
                        //G1 = dPo/(s*R-dR0);
                        //G2 = dPhi*dGv/(s+g1v);
                        //G3n = dAlf*Hf+dAlf2*Af*Gf*Df*dKf*(s+g1f);
                        //G3 = G3n/(Af*(s+g1f));
                        //G4n = g4f*Gf*(dAlf*g3f*Bf+dAlf2*Af*(s+g1f));
                        //G4 = G4n/(Af*(s+g1f));
                        //G5 = Nf/(s+g4f);
                        //G6 = Mf/(s+g4f);
                        //G7 = g1p / (s + g1p); G8 = G7; G14 = G7; G15 = G7;
                        //G9n = (s+g1+g2)*A*M+g1*B;
                        //G9d = (s+g1)*(s+g1+g2)*A;
                        //G9 = G9n/G9d;
                        //G10 = (K+g1*I)/(s+g3);
                        //G11 = g3*D*L/((s+g1)*G*(1-D*F));
                        //G12 = g3*J/(s+g3);
                        //G13 = 0; // for SUPO
                        //GA = G13*G14*G15/(1-G12*G13*G14*G15);
                        //GB = (G7*G8*(G9+G10*G11*GA))/(1-G6*G7*G8*(G9+G10*G11*G12));
                        //GT = G1/(dPo*(1+G1*(G2+G3+G4*G5*GB)));
                        //double BAG1 = Complex.Abs(G1); 
                        //double BAG2 = Complex.Abs(G2); 
                        //double BAG3 = Complex.Abs(G3); 
                        //double BAG4 = Complex.Abs(G4); 
                        //double BAG5 = Complex.Abs(G5);
                        //double BAG6 = Complex.Abs(G6); 
                        //double BAG7 = Complex.Abs(G7); 
                        //double BAG9 = Complex.Abs(G9); 
                        //double BAG10 = Complex.Abs(G10); 
                        //double BAG11 = Complex.Abs(G11); 
                        //double BAG12 = Complex.Abs(G12);
                        //double BAGA = Complex.Abs(GA); 
                        //double BAGB = Complex.Abs(GB); 
                        //double BAGT = Complex.Abs(GT);
                        //double BPG1 = G1.Phase; 
                        //double BPG2 = G2.Phase; 
                        //double BPG3 = G3.Phase; 
                        //double BPG4 = G4.Phase; 
                        //double BPG5 = G5.Phase;
                        //double BPG6 = G6.Phase; 
                        //double BPG7 = G7.Phase; 
                        //double BPG9 = G9.Phase; 
                        //double BPG10 = G10.Phase; 
                        //double BPG11 = G11.Phase;
                        //double BPG12 = G12.Phase;
                        //double BPGA = GA.Phase;
                        //double BPGB = GB.Phase; 
                        //double BPGT = GT.Phase;
                        //double PlotGT;
                        //switch(i)
                        //{
                        //    case 0:
                        //        PlotGT=BAGT;
                        //        chBodeAmp.Series[0].Points.AddXY(100*dW-1, 5*PlotGT-1);
                        //        break;
                        //    case 1:
                        //        PlotGT=BAGT;
                        //        chBodeAmp2.Series[0].Points.AddXY(dW / 10 - 1, PlotGT);
                        //        break;
                        //    case 2:
                        //        PlotGT=BPGT;
                        //        chBodePhase.Series[0].Points.AddXY(5000*dW-40,10*PlotGT-4);
                        //        break;
                        //    case 3:
                        //        PlotGT=BPGT;
                        //        string str = string.Format("{0} {1}\n", dW - 9, 4 * PlotGT + 4);
                        //        sw.WriteLine(str);
                        //        chBodePhase2.Series[0].Points.AddXY(dW-9,4*PlotGT+4);
                        //        break;
                        //    case 4:
                        //        NyGT = 1+G1*(G2+G3+G4*G5*GB);
                        //        chNyq.Series[0].Points.AddXY(NyGT.Real, NyGT.Imaginary);
                        //        break;
                        //    case 5:
                        //        Stab = G1*(G2+G3+G4*G5*GB);
                        //        chStab.Series[0].Points.AddXY(Stab.Real, Stab.Imaginary);//new plot needed --> chStab
                        //        break;
                        //}
                        dW += dWstep;
                    }//while
                    //strOut = string.Format("D,{0}", swTimer.ElapsedMilliseconds);
                    //strmW.WriteLine(strOut);
                }//iBL for loop
            }//plot for loop
            //strOut = string.Format("E,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //sw.Close();
            chBodeAmp.Series.ResumeUpdates();
            //chBodeAmp2.Series.ResumeUpdates();
            chBodePhase.Series.ResumeUpdates();
            //chBodePhase2.Series.ResumeUpdates();
            //chNyq.Series.ResumeUpdates();
            chStab.Series.ResumeUpdates();

            //chStab.ChartAreas[0].AxisX.Minimum = -2.0;
            //chStab.ChartAreas[0].AxisX.Maximum = 2.0;
            //chStab.ChartAreas[0].AxisY.Minimum = -2;
            //chStab.ChartAreas[0].AxisY.Maximum = 2;
            //chStab.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;
            //chStab.ChartAreas[0].AxisY.LabelStyle.Interval = 1;
            //chStab.Series.Clear();
            //dW = -0.1e-1;
            //while (dW < 1e-1)
            //{
            //    cxW = cxJ * dW;
            //    cxB1 = cxW * dLStar - dR0;
            //    cxT1 = 0 * cxJ;
            //    for (int k = 0; k < 6; k++)
            //    {
            //        cxT1 += (daF[k] / (cxW + daAl[k]));
            //    }
            //    cxB2 = cxW * cxT1;
            //    cxGp = 1 / (cxB1 + cxB2);
            //    for (int k = 0; k < 5; k++)
            //    {
            //        cxT2 = cxW + dGamma;
            //        cxT3 = cxW + dSigma;
            //        cxHT = -daKBar[k] / cxT2;
            //        cxHG = -daGBar[k] / cxT3;
            //        cxNy = 1 + cxGp * (cxHT + cxHG);
            //        cxT3 = cxGp / cxNy;
            //        dNicA = Complex.Abs(cxT3);
            //        dNicP = cxT3.Phase;
            //        chNichols.Series[k].Points.AddXY(dNicP, Math.Log10(dNicA));
            //    }
            //    dW += 1e-5;
            //}//while
            //chNichols.Series.ResumeUpdates();
            //strOut = string.Format("F,{0}", swTimer.ElapsedMilliseconds);
            //strmW.WriteLine(strOut);
            //swTimer.Stop();
            //strmW.Close();
            return true;
        }

        private void CallStabCalcs()
        {

            if (sModName == "SupoModel")
            {
                //if (!bStabCalcsDone)
                //{
                    bStabCalcsDone = SupoModelStabCalcs();
                //}
            }//end if SupoModel
            if (sModName == "AD125")
            {
                //if (!bStabCalcsDone)
                //{
                    bStabCalcsDone = AD125StabCalcs();
                //}
            }//end if SupoModel
            if (sModName == "AD135")
            {
                //if (!bStabCalcsDone)
                //{
                bStabCalcsDone = AD135StabCalcs();
                //}
            }//end if SupoModel
        }

        private void ClickStabilityPlots(object sender, EventArgs e)
        {
            try
            {
                ToggleVisibilityPlots(false);
                if (WindowState == FormWindowState.Normal)
                {
                    if (panStabPlots.Top > 400)
                    {
                        panStabPlots.Top -= 458;
                    }
                }
                CallStabCalcs();
                eScaleType = ScaleVals.ScaleType.Any;
            }
            catch (Exception ee)
            {
            }
        }

        private void ClickExecute(object sender, EventArgs e)
        {
            try
            {
                //swTimer.Start();
                //startT = swTimer.ElapsedMilliseconds;
                fTimeStep = Constants.constants.DelT(sModName);
                iNumSteps = (int)(fMaxTime / fTimeStep + 1.01);
                bDisplay = false;
                iOrder = (int)(Math.Log10(iNumSteps));
                tbarDataScale.Minimum = 0;
                tbarDataScale.Maximum = iOrder + 1;
                tbarDataScale.Value = (tbarDataScale.Minimum + tbarDataScale.Maximum) / 2;
                iSkip = (int)(Math.Pow(10, tbarDataScale.Value));
                chDataGraph.ChartAreas[0].CursorX.Position = -1000F;
                dgResults.Rows.Clear();
                bPlotting = true;
                labRunning.Visible = true;
                timer2.Interval = 1000;
                timer2.Enabled = true;
                timer2.Start();
                //iCycles = 0;
                Execute();
            }
            catch (Exception ee)
            {
            }

        }

        private void ClickAutoScale(object sender, EventArgs e)
        {
            try
            {
                chkAutoScale.Checked = !chkAutoScale.Checked;
                //bResetAuto = false;
            }
            catch (Exception ee)
            {
            }
        }

        private void ChkChgAutoScale(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoScale.Checked)
                {
                    ResetYScale();
                    clrSaveASFColor = chkAutoScale.ForeColor;
                    clrSaveASBColor = chkAutoScale.BackColor;
                    chkAutoScale.ForeColor = Color.Black;
                    chkAutoScale.BackColor = Color.LightGray;
                    bAutoScale = true;
                }
                else
                {
                    chkAutoScale.ForeColor = clrSaveASFColor;
                    chkAutoScale.BackColor = clrSaveASBColor;
                    bAutoScale = false;
                }
            }
            catch (Exception ee)
            {
            }

        }

        private void ClickDelay(object sender, EventArgs e)
        {
            bDelayPlot = !bDelayPlot;
        }

        private void CheckChangedDelay(object sender, EventArgs e)
        {

        }

        private void ClickICSetOptions(object sender, EventArgs e)
        {
            UpdateParameters();//daInitVals[i]
            frmICOptions dlg = new frmICOptions(daInitVals);
            if (dlg.ShowDialog() == DialogResult.OK && bLive)
                OverWriteParameters();
            else if (dlg.ShowDialog() == DialogResult.OK && !bLive)
                tbStatus.Text = "Cannot reset IC values while in Replay Mode";
        }

        private void StabPlotGeom()
        {
            chBodeAmp.Location = new Point(5, 5);
            chBodeAmp.Width = (panStabPlots.Width - 15) / 2;
            chBodeAmp.Height = (panStabPlots.Height - 14) / 2;
            chBodePhase.Location = new Point(chBodeAmp.Width+10, 5);
            chBodePhase.Width = (panStabPlots.Width - 15) / 2;
            chBodePhase.Height = (panStabPlots.Height - 14) / 2;
            //chNyq.Location = new Point(5, chBodeAmp.Height+9);
            //chNyq.Width = (panStabPlots.Width - 15) / 2;
            //chNyq.Height = (panStabPlots.Height - 14) / 2;
            chStab.Location = new Point(5, chBodeAmp.Height + 9);
            chStab.Width = (panStabPlots.Width - 15) / 2;
            chStab.Height = (panStabPlots.Height - 14) / 2;
            chBodeAmp.Visible = true;
            chBodePhase.Visible = true;
            //chNyq.Visible = true;
            chStab.Visible = true;
            bMultDisplayMode = true;
        }

        private void ClickBodeAmp(object sender, EventArgs e)
        {
            chBodeAmp.Visible = false;
            if (chBodeAmp.Width < panStabPlots.Width)
            {
                chBodeAmp.Location = new Point(0, 0);
                chBodeAmp.Width = panStabPlots.Width;
                chBodeAmp.Height = panStabPlots.Height;
                chBodePhase.Visible = false;
                //chNyq.Visible = false;
                chStab.Visible = false;
                bMultDisplayMode = false;
                eScaleType = ScaleVals.ScaleType.BodeAmp;
            }
            else
            {
                StabPlotGeom();
            }
            chBodeAmp.Visible = true;
            rectScreen = new Rectangle(0, 0, 0, 0);
        }

        private void ClickBodePhase(object sender, EventArgs e)
        {
            chBodePhase.Visible = false;
            if (chBodePhase.Width < panStabPlots.Width)
            {
                chBodePhase.Location = new Point(0, 0);
                chBodePhase.Width = panStabPlots.Width;
                chBodePhase.Height = panStabPlots.Height;
                chBodeAmp.Visible = false;
                //chNyq.Visible = false;
                chStab.Visible = false;
                bMultDisplayMode = false;
                eScaleType = ScaleVals.ScaleType.BodePhase;
            }
            else
            {
                StabPlotGeom();
            }
            chBodePhase.Visible = true;
            rectScreen = new Rectangle(0, 0, 0, 0);
        }

        private void ClickNyq(object sender, EventArgs e)
        {
            //chNyq.Visible = false;
            //if (chNyq.Width < panStabPlots.Width)
            //{
            //    chNyq.Location = new Point(0, 0);
            //    chNyq.Width = panStabPlots.Width;
            //    chNyq.Height = panStabPlots.Height;
            //    chBodeAmp.Visible = false;
            //    chBodePhase.Visible = false;
            //    chStab.Visible = false;
            //    bMultDisplayMode = false;
            //}
            //else
            //{
            //    StabPlotGeom();
            //}
            //chNyq.Visible = true;
            //rectScreen = new Rectangle(0, 0, 0, 0);
        }

        private void ClickStab(object sender, EventArgs e)
        {
            chStab.Visible = false;
            if (chStab.Width < panStabPlots.Width)
            {
                //svLocation = chStab.Location;
                chStab.Location = new Point(0, 0);
                chStab.Width = panStabPlots.Width;
                chStab.Height = panStabPlots.Height;
                chBodeAmp.Visible = false;
                chBodePhase.Visible = false;
                //chNyq.Visible = false;
                bMultDisplayMode = false;
                eScaleType = ScaleVals.ScaleType.StabMarg;
            }
            else
            {
                StabPlotGeom();
                //chStab.Location = svLocation;
                //chStab.Width = (panStabPlots.Width - 15) / 2;
                //chStab.Height = (panStabPlots.Height - 14) / 2;
                //chBodeAmp.Visible = true;
                //chBodePhase.Visible = true;
                ////chNyq.Visible = true;
                //bMultDisplayMode = true;
            }
            chStab.Visible = true;
            rectScreen = new Rectangle(0, 0, 0, 0);
        }

        private void ToolTipBodeAmp(object sender, ToolTipEventArgs e)
        {
            if (chBodeAmp.Width < 635)
                e.Text = "Click plot to enlarge";
            else
                e.Text = "Click plot to shrink";
        }

        private void ToolTipBodePhase(object sender, ToolTipEventArgs e)
        {
            if (chBodePhase.Width < 635)
                e.Text = "Click plot to enlarge";
            else
                e.Text = "Click plot to shrink";
        }

        private void ToolTipNyq(object sender, ToolTipEventArgs e)
        {
            //if (chNyq.Width < 635)
            //    e.Text = "Click plot to enlarge";
            //else
            //    e.Text = "Click plot to shrink";
        }

        private void ToolTipStab(object sender, ToolTipEventArgs e)
        {
            if (chStab.Width < 635)
                e.Text = "Click plot to enlarge";
            else
                e.Text = "Click plot to shrink";
        }
    }
}
