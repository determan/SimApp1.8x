using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ModelConverter
{
    public struct VariableInfo
    {
        public string sVarName;
        public string sType;
        public string sEngName;
        public string sUnits;
        public bool bStabItem;

        public VariableInfo(ArrayInfo ai)
        {
            sVarName = string.Format("{0}[{1}]", ai.sName, ai.iSize);
            sType = ai.sType;
            sEngName = ai.sEngName;
            sUnits = ai.sUnits;
            bStabItem = false;
        }
    };

    public struct lineElements
    {
        public string sCode;
        public string sComment;
    };

    public struct ArrayInfo
    {
        public String sName;
        public int iSize;
        public string sType;
        public string sEngName;
        public string sUnits;

        public ArrayInfo(String sn, int isz)
        {
            sName = sn;
            iSize = isz;
            sType = "";
            sEngName = "";
            sUnits = "";
        }

        public void AddInfo(string st, string sen, string su)
        {
            sType = st;
            sEngName = sen;
            sUnits = su;
        }

        public string DumpInfo()
        {
            string str = string.Format("{0}[{1}],{2},{3},{4}", sName, iSize, sType, sEngName, sUnits);
            return str;
        }
    }

    public partial class frmModConv : Form
    {
        public enum varType {MC_CONSTANT=0, MC_IC, MC_DATA, MC_STATE};
        private string sModName, sOutFileNameCpp, sOutFileNameH, sPath="";
        private string sFileName;
        private string[] straStateVars;
        private string[] straVars;
        private lineElements[] lines;
        private int iSvInd;
        private int iVInd;
        private int iInd;
        //System.IO.StreamWriter strmW2;
        //private Stopwatch sw;
        private bool bDyn;
        private FormICs formIC;
        private FormICs formPG;
        private int iNumVars;
        private int iNumState;
        private int iNumConst;
        private int iNumLines;
        private int iNumCA;
        private int iNumNSA;
        private int iNumSA;
        private string sSVars;
        private string sVars;
        private string sConsts;
        private string sFormals;
        private string sSizes;
        private int iNumTypes;
        private int iNumGroups;
        private VariableInfo[] viaConsts;
        private VariableInfo[] viaData;
        private VariableInfo[] viaStateData;
        private string[] saTypes;
        private string[] saGroups;
        private List<ArrayInfo> listStateArrays;
        private List<ArrayInfo> listNonStateArrays;
        private List<ArrayInfo> listConstArrays;
        private List<SubModelInfo> listSubModels;
        private List<FuncInfo> listFuncs;
        private StreamReader strmRfc;
        private string sSMformals;
        private List<string> listDataItems;
        private string[] loopstrs;
        private List<string> listNonPlotGroups;

        public frmModConv()
        {
            InitializeComponent();
            iSvInd = 0;
            iVInd = 0;
            iInd = 0;
            bDyn = false;
            iNumTypes = 1;  //default
            iNumGroups = 1;
            saGroups = new string[50];
            saGroups[0] = "User";
            saTypes = new string[20];
            saTypes[0] = "Constant";
//            saTypes[1] = "Physical Parameters";
            listStateArrays = new List<ArrayInfo>();
            listNonStateArrays = new List<ArrayInfo>();
            listConstArrays = new List<ArrayInfo>();
            listSubModels = new List<SubModelInfo>();
            listFuncs = new List<FuncInfo>();
            listNonPlotGroups = new List<string>();
            //           sw = new Stopwatch();
        }

        private void ModConvDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ModConvDragDrop(object sender, DragEventArgs e)
        {
            int iArrLen, iPos;
            String sBackupName, sApp;
            System.Object arData = e.Data.GetData(DataFormats.FileDrop);
            iArrLen = ((System.Array)(arData)).GetLength(0);
            for (int j = 0; j < iArrLen; j++)
            {
                sFileName = tbFilePath.Text = ((System.Array)(arData)).GetValue(j).ToString();
            }
//            sw.Start();
            if (sFileName.IndexOf(".LST") >= 0 || sFileName.IndexOf(".lst") >= 0)
            {
                DisplayNames();
                AnalyzeVariables();
            }
            else if (sFileName.IndexOf(".DAT") >= 0 || sFileName.IndexOf(".dat") >= 0)
            {
                iPos = sFileName.LastIndexOf('.');
                if(iPos>=0)
                    sBackupName = sFileName.Substring(0,iPos);
                else
                    sBackupName = sFileName;
                sApp = DateTime.Now.ToString("_MMddyy_Hmmss")+".dat";
                sBackupName = sBackupName + sApp;
                File.Copy(sFileName, sBackupName);
                ResumeConversion();
            }
            int iRoundedUp;
            iRoundedUp = ((iNumState + 1000) / 1000) * 1000;
            straStateVars = new string[iRoundedUp];
            iRoundedUp = ((iNumVars + iNumConst + 1000) / 1000) * 1000;
            straVars = new string[iRoundedUp];
            iRoundedUp = ((iNumLines + 1000) / 1000) * 1000;
            lines = new lineElements[iRoundedUp];
//            sw.Stop();
        }

        private void ResumeConversion()
        {
            string str, sTmp, st, sen, su, sn, sc, scm, sGrp;
            int iPos, iPos2, iSz, iNumFuncs, iNumSMs, iBits;
            long lRPos;
            ArrayInfo ai;
            sModName = sFileName.Substring(0, sFileName.Length - 4);
            iPos = sModName.LastIndexOf('\\');
            if (iPos >= 0)
            {
                sPath = sModName.Substring(0, iPos + 1);
                tbModName.Text = sModName = sModName.Substring(iPos + 1);
            }
            StreamReader strmR = new StreamReader(sFileName);
            tbFilePath.Text = sFileName = strmR.ReadLine();
            sOutFileNameCpp = sPath+sModName + ".cpp";
            sOutFileNameH = sPath+sModName + ".h";
            tbOutName.Text = sOutFileNameH + ", " + sOutFileNameCpp;
            listConstArrays = new List<ArrayInfo>();
            listNonStateArrays = new List<ArrayInfo>();
            listStateArrays = new List<ArrayInfo>();
            listFuncs = new List<FuncInfo>();
            listSubModels = new List<SubModelInfo>();

            str = strmR.ReadLine();
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                iNumLines = Int32.Parse(str.Substring(iPos + 2));
            }
            str = strmR.ReadLine();
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                iNumTypes = Int32.Parse(str.Substring(iPos + 2))+1;   //number custom types, so far
                str = strmR.ReadLine();
                int iCnt = 1;
                if(iNumTypes>0)
                {
                    while (str != "")
                    {
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            saTypes[iCnt++] = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        else
                        {
                            saTypes[iCnt++] = str;
                            str = "";
                        }
                    }
                }
                //iNumTypes = iCnt; //actual number of types
            }
            str = strmR.ReadLine();
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                iNumGroups = Int32.Parse(str.Substring(iPos + 2));   //number custom groups, so far
                str = strmR.ReadLine();
                int iCnt = 1;
                if (iNumGroups > 0)
                {
                    while (str != "")
                    {
                        sGrp = "";
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sGrp = str.Substring(0, iPos);
                            saGroups[iCnt++] = sGrp;
                            str = str.Substring(iPos + 1);
                        }
                        else
                        {
                            sGrp = str;
                            saGroups[iCnt++] = sGrp;
                            str = "";
                        }
                        if (sGrp.IndexOf('$') >= 0)
                        {
                            listNonPlotGroups.Add(sGrp.Replace("$",""));
                        }
                    }
                }
                iNumGroups = iCnt; //actual number of types
            }
            lRPos = strmR.BaseStream.Position;
            str = strmR.ReadLine(); //Constants section
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                sTmp = str.Substring(0,iPos);
                if (sTmp == "Constants")
                {
                    iPos2 = str.IndexOf(",");
                    if (iPos2 >= 0)
                    {
                        iNumConst = Int32.Parse(str.Substring(iPos + 1, iPos2 - iPos - 1).Trim());
                        iNumCA = Int32.Parse(str.Substring(iPos2 + 1).Trim());
                    }
                    else
                    {
                        iNumConst = Int32.Parse(str.Substring(iPos + 1).Trim());
                        iNumCA = 0;
                    }
                    viaConsts = new VariableInfo[iNumConst];
                    for (int i = 0; i < iNumConst; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaConsts[i].sVarName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaConsts[i].sType = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaConsts[i].sEngName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaConsts[i].sUnits = str.Substring(0, iPos);
                            viaConsts[i].bStabItem = true;
                        }
                        else
                        {
                            viaConsts[i].sUnits = str.Substring(iPos + 1);
                            viaConsts[i].bStabItem = false;
                        }
                    }
                    for (int i = 0; i < iNumCA; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sTmp = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                            iPos = sTmp.IndexOf("[");
                            iPos2 = sTmp.IndexOf("]");
                            iSz = Int32.Parse(sTmp.Substring(iPos + 1, iPos2 - iPos - 1));
                            sTmp = sTmp.Substring(0, iPos);
                        }
                        else 
                            iSz = 0;
                        st = sen = su = "";
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            st = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sen = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            su = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        else
                        {
                            su = str.Substring(iPos + 1);
                        }
                        ai = new ArrayInfo(sTmp, iSz);
                        ai.AddInfo(st, sen, su);
                        listConstArrays.Add(ai);
                    }//for
                }
                else
                {//push read position back as line was not used
                    strmR.BaseStream.Seek(lRPos, SeekOrigin.Begin);
                }
            }
            lRPos = strmR.BaseStream.Position;
            str = strmR.ReadLine();     //nonstate section
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                sTmp = str.Substring(0,iPos);
                if (sTmp == "NSData")
                {
                    iPos2 = str.IndexOf(",");
                    if (iPos2 >= 0)
                    {
                        iNumVars = Int32.Parse(str.Substring(iPos + 1, iPos2 - iPos - 1).Trim());
                        iNumNSA = Int32.Parse(str.Substring(iPos2 + 1).Trim());
                    }
                    else
                    {
                        iNumVars = Int32.Parse(str.Substring(iPos + 1).Trim());
                        iNumNSA = 0;
                    }
                    viaData = new VariableInfo[iNumVars];
                    for (int i = 0; i < iNumVars; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaData[i].sVarName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaData[i].sType = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaData[i].sEngName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaData[i].sUnits = str.Substring(0, iPos);
                            viaData[i].bStabItem = true;
                        }
                        else
                        {
                            viaData[i].sUnits = str.Substring(iPos + 1);
                            viaData[i].bStabItem = false;
                        }
                    }//for
                    for (int i = 0; i < iNumNSA; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sTmp = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                            iPos = sTmp.IndexOf("[");
                            iPos2 = sTmp.IndexOf("]");
                            iSz = Int32.Parse(sTmp.Substring(iPos + 1, iPos2 - iPos - 1));
                            sTmp = sTmp.Substring(0, iPos);
                            listNonStateArrays.Add(new ArrayInfo(sTmp, iSz));
                        }
                        st = sen = su = "";
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            st = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sen = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            su = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        else
                        {
                            su = str.Substring(iPos + 1);
                        }
                        listNonStateArrays[listNonStateArrays.Count - 1].AddInfo(st, sen, su);
                    }//for
                }
                else
                {//push read position back as line was not used
                    strmR.BaseStream.Seek(lRPos, SeekOrigin.Begin);
                }
            }
            lRPos = strmR.BaseStream.Position;
            str = strmR.ReadLine();     //state data section
            iPos = str.IndexOf(':');
            if (iPos >= 0 && iPos + 2 <= str.Length)
            {
                sTmp = str.Substring(0,iPos);
                if (sTmp == "StateData")
                {
                    iPos2 = str.IndexOf(",");
                    if (iPos2 >= 0)
                    {
                        iNumState = Int32.Parse(str.Substring(iPos + 1, iPos2 - iPos - 1).Trim());
                        iNumSA = Int32.Parse(str.Substring(iPos2 + 1).Trim());
                    }
                    else
                    {
                        iNumState = Int32.Parse(str.Substring(iPos + 1).Trim());
                        iNumSA = 0;
                    }
                    viaStateData = new VariableInfo[iNumState];
                    for (int i = 0; i < iNumState; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaStateData[i].sVarName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaStateData[i].sType = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaStateData[i].sEngName = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            viaStateData[i].sUnits = str.Substring(0, iPos);
                            viaStateData[i].bStabItem = true;
                        }
                        else
                        {
                            viaStateData[i].sUnits = str.Substring(iPos + 1);
                            viaStateData[i].bStabItem = false;
                        }
                    }//for
                    for (int i = 0; i < iNumSA; i++)
                    {
                        str = strmR.ReadLine();
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sTmp = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                            iPos = sTmp.IndexOf("[");
                            iPos2 = sTmp.IndexOf("]");
                            iSz = Int32.Parse(sTmp.Substring(iPos + 1, iPos2 - iPos - 1));
                            sTmp = sTmp.Substring(0, iPos);
                            listStateArrays.Add(new ArrayInfo(sTmp, iSz));
                        }
                        st = sen = su = "";
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            st = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            sen = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        iPos = str.IndexOf(',');
                        if (iPos >= 0)
                        {
                            su = str.Substring(0, iPos);
                            str = str.Substring(iPos + 1);
                        }
                        else
                        {
                            su = str.Substring(iPos + 1);
                        }
                        listStateArrays[listStateArrays.Count - 1].AddInfo(st, sen, su);
                    }//for
                }
                else
                {//push read position back as line was not used
                    strmR.BaseStream.Seek(lRPos, SeekOrigin.Begin);
                }
            }
            if (!strmR.EndOfStream)
            {
                lRPos = strmR.BaseStream.Position;
                str = strmR.ReadLine();     //functions data section
                iPos = str.IndexOf(':');
                if (iPos >= 0 && iPos + 2 <= str.Length)
                {
                    sTmp = str.Substring(0, iPos);
                    if (sTmp == "Functions")
                    {
                        iNumFuncs = Int32.Parse(str.Substring(iPos + 1).Trim());
                        FuncInfo fi;
                        for (int i = 0; i < iNumFuncs; i++)
                        {
                            str = strmR.ReadLine();
                            iPos = str.IndexOf("!&");
                            if (iPos >= 0)
                            {
                                sn = str.Substring(0, iPos).Trim();
                                str = str.Substring(iPos + 2).Trim();
                                iPos = str.IndexOf("!&");
                                if (iPos >= 0)
                                {
                                    sc = str.Substring(0, iPos).Trim();
                                    str = str.Substring(iPos + 2).Trim();
                                    iPos = str.IndexOf("!&");
                                    if (iPos >= 0)
                                    {
                                        scm = str.Substring(0, iPos).Trim();
                                        str = str.Substring(iPos + 2).Trim();//now the comma sep list of fps
                                        fi = new FuncInfo(sn, sc, scm);
                                        while (str.Length > 0)
                                        {
                                            iPos = str.IndexOf(",");
                                            if (iPos >= 0)
                                            {
                                                sTmp = str.Substring(0, iPos).Trim();
                                                str = str.Substring(iPos + 2).Trim();
                                                iPos = str.IndexOf("!&");
                                            }
                                            else
                                            {
                                                sTmp = str;
                                                str = "";
                                            }
                                            fi.AddParam(sTmp);
                                        }//while
                                        listFuncs.Add(fi);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {//push read position back as line was not used
                        strmR.BaseStream.Seek(lRPos, SeekOrigin.Begin);
                    }
                }
            }
            if (!strmR.EndOfStream)
            {
                str = strmR.ReadLine();     //submodels section
                iPos = str.IndexOf(':');
                if (iPos >= 0 && iPos + 2 <= str.Length)
                {
                    sTmp = str.Substring(0, iPos);
                    if (sTmp == "Submodels")
                    {
                        iNumSMs = Int32.Parse(str.Substring(iPos + 1).Trim());
                        SubModelInfo smi;
                        for (int i = 0; i < iNumSMs; i++)
                        {
                            str = strmR.ReadLine();
                            iPos = str.IndexOf("!&");
                            if (iPos >= 0)
                            {
                                sn = str.Substring(0, iPos).Trim();
                                str = str.Substring(iPos + 2).Trim();
                                iPos = str.IndexOf("!&");
                                if (iPos >= 0)
                                {
                                    sTmp = str.Substring(0, iPos).Trim();
                                    iBits = Int32.Parse(str.Substring(iPos + 2).Trim());
                                    smi = new SubModelInfo(sn, sTmp, iBits);
                                    listSubModels.Add(smi);
                                    //iPos = str.IndexOf("!&");
                                    //if (iPos >= 0)
                                    //{
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            strmR.Close();
        }

        private void DisplayNames()
        {
            int iPos = sFileName.LastIndexOf('.');
            if (iPos >= 0)
            {
                sModName = sFileName.Substring(0, iPos);
                sOutFileNameCpp = sModName + ".cpp";
                sOutFileNameH = sModName + ".h";
            }
            else
            {
                sModName = sFileName;
                sOutFileNameCpp = sFileName + ".cpp";
                sOutFileNameH = sFileName + ".h";
            }
            iPos = sModName.LastIndexOf('\\');
            if(iPos>=0)
            {
                sPath = sModName.Substring(0, iPos + 1);
                sModName = sModName.Substring(iPos + 1);
            }
            tbOutName.Text = sOutFileNameH + ", " + sOutFileNameCpp;
            tbModName.Text = sModName;
        }

        private string Var(string sIn)
        {
            return "m_sd" + sIn;
        }

        private string StateVar(string sIn, bool bVal)
        {
            string rStr;
            if (bVal)
            {
                if(bDyn)
                    rStr = String.Format("V(m_sd{0}, iCol)", sIn);
                else
                    rStr = String.Format("V(m_sd{0}, 0)", sIn);
            }
            else
                rStr = "m_sd" + sIn;
            return rStr;
        }

        private bool FPatEnd(string sCode, out string sfp, out int iPos)
        {
            string sTmp = sSMformals;
            while (sTmp != null && sTmp.Length > 0)
            {
                iPos = sTmp.IndexOf(",");
                if (iPos >= 0)
                {
                    sfp = sTmp.Substring(0, iPos);
                    sTmp = sTmp.Substring(iPos + 1);
                }
                else
                {
                    sfp = sTmp;
                    sTmp = "";
                }
                iPos = sCode.IndexOf(sfp);
                if (iPos >= 0 && (iPos + sfp.Length == sCode.Length))
                    return true;
            }
            sfp = "";
            iPos = -1;
            return false;
        }

        private string FindOperand1(string strCode, out int iLoc)
        {
            //find operand preceding an operator
            //cases include function, group (final char is ')')
            string strSeps = " =()+-*/;^", strOp="", sFp;
            int iRP=0,iPos, j, iPos2, il;
            bool bNum = false;
            iLoc = -1;
            iPos = strCode.Length-1;
            if (iPos < 0)
                return strOp;
            if (strCode.ElementAt(iPos) == ')')
            {
                iRP = 1;
                //loop backwards through strCode counting parens
                for (j = iPos - 1; j >= 0; j--)
                {
                    if (strCode.ElementAt(j) == ')')
                        iRP++;
                    else if (strCode.ElementAt(j) == '(')
                        iRP--;
                    if (iRP == 0)
                    {
                        //balancing LP was found
                        break;
                    }
                }
                char ch;
                if (j > 0)
                {
                    ch = strCode.ElementAt(--j);
                    if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                    {
                        //name token, so function name, preceeds (), so push j back to start of name
                        for (; j >= 0; j--)
                        {
                            if (strSeps.IndexOf(strCode.ElementAt(j)) >= 0)
                            {
                                break;
                            }
                        }
                    }
                    j++;    //either went negative or one too far, either way increment
                    iLoc = j;
                    strOp = strCode.Substring(j, iPos - j + 1);
                }
                //else condition not possible - unbalanced parens
            }
            else if (FPatEnd(strCode, out sFp, out il))
            {//formal param
                iLoc = il;
                strOp = sFp;
            }
            //variable - m_sd name, no ')'
            else if ((iPos2 = strCode.LastIndexOf("m_sd")) >= 0)
            {
                //there is a variable in this code segment, and this is the last such variable
                //if a sep follows this variable, no var in our operand, so it is a number
                for (int ii = iPos2; ii < iPos; ii++)
                {
                    if (strSeps.IndexOf(strCode.ElementAt(ii)) >= 0)
                    {
                        if (strCode.ElementAt(ii) != '^')
                        {
                            bNum = true;
                            break;
                        }
                    }
                    //no break, so variable operand between ipos2 and iPos
                    iLoc = iPos2;
                    strOp = strCode.Substring(iPos2, iPos - iPos2 + 1);
                }
            }
            //number - simple or scientific notation
            else
                bNum = true;
            if (bNum)
            {
                iLoc = 0;
                string str = strCode;
                float fNum = (float)double.NaN; //set to NAN
                while (str.Length > 0)
                {
                    try
                    {
                        if (str[0] != '+' && str[0] != '-')
                        {
                            fNum = Single.Parse(str);
                            break;
                        }
                        iLoc++;
                        str = str.Substring(1);
                    }
                    catch (Exception e)
                    {
                        iLoc++;
                        str = str.Substring(1);
                    }
                }
                if (double.IsNaN(fNum) || double.IsInfinity(fNum))
                {//error if here, should not get here
                }
                else
                {
                    strOp = str;
                }
            }
            return strOp;
        }

        private string FindOperand2(ref string strCode)
        {
            string strSeps1 = " +-*/;^", strOp="";
            int iPos, iEnd = 0, iParen = 0, iStart = 1; 
            bool bEndOp=false;
            for (int i = 1; i < strCode.Length; i++)
            {
                if (i==iStart && (strCode[i]==' '|| strCode[i]=='\t'))
                {
                    iStart++;
                    continue;
                }
                if (iParen==0)
                { 
                    iPos = strSeps1.IndexOf(strCode[i]);
                    if (iPos >= 0)
                    {
                        bEndOp = true;
                        //in most cases, we have found an operand (variable or number)
                        //special case for +/- - found a portion of a sci. not. #, or start of regular signed number
                        if (strCode[i] == '+' || strCode[i] == '-')
                        {
                            if (i >= 1 && strCode[i - 1] == 'e')
                            {
                                //sci. not. # - +/- was not end of operand
                                bEndOp = false;
                            }
                            else
                            {
                                int j;
                                //nothing more than whitespace between '^' and [+/-]
                                for (j = 1; j < iPos; j++)
                                {
                                    if (strCode[j] > ' ')
                                        break;
                                }
                                if (j == iPos)
                                    //only white space present - a signed number
                                    bEndOp = false;
                            }
                        }
                    }
                }
                if (strCode[i] == '(')
                {
                    iParen++;
                }
                else if (strCode[i] == ')')
                {
                    iParen--;
                    if (iParen <= 0)
                    {
                        bEndOp = true;
                        i++;    //necessary to include closing paren in operand
                    }
                }
                if (bEndOp)
                {
                    iEnd = i;
                    // take actions for end of operand here and possibly break
                    break;
                }
            }//for
            if (iEnd > iStart)
            {
                strOp = strCode.Substring(iStart, iEnd - iStart);
            }
            //find operand following an operator
            //cases include function, group (final char is ')')
            //variable - m_sd name, no ')'
            //number - simple or scientific notation

            if (strOp[0] == '(' && strOp[strOp.Length - 1] == ')')
            {
                int i, j;
                j = strOp.Length - 1;
                if (strOp[1] == '+' || strOp[1] == '-')
                    i = 2;
                else
                    i = 1;
                //if is int num with leading 0, remove 0
                try
                {
                    int iNum = Int32.Parse(strOp.Substring(i, j - i));
                    if (strOp[i] == '0')
                    {
                        strOp = strOp.Substring(0, i) + strOp.Substring(i + 1);
                        strCode = strCode.Substring(0, iStart + i) + strCode.Substring(iStart + i+1);
                    }
                }
                catch (Exception e)
                {
                }
            }
            return strOp;
        }

        private void ReplaceLastToken(ref string strIn, string tk1, string tk2)
        {
            string strSeps = " =()+-*/;^";
            int[] locs = new int[10];
            int iDex = -1, iPos1=0, iPos2;
            while(true)
            {
                iPos2 = strIn.IndexOf(tk1,iPos1);
                if (iPos2 > iPos1)
                {
                    if (strSeps.IndexOf(strIn[iPos2 - 1]) >= 0)
                        locs[++iDex] = iPos2;
                    iPos1 = iPos2 + tk1.Length;
                }
                else
                    break;
            }
            if (iDex >= 0)
                strIn = strIn.Substring(0, locs[iDex]) + tk2 + strIn.Substring(locs[iDex] + tk1.Length);
        }

        private string ProcessLine(string sLine, int j)
        {
            int iPos, iPos1, iDex=0, iLoc=0;
            bool bDer = false, bState = false;
            string sToken="", sVal;
            char[] chSeps = { '=', '(', ')', '+', '-', '*', '/', ';', '^', ' ', ',' ,'[',']'};
            //char[] chSeps2 = { '=', '(', ')', '*', '/', ';', '^', ' ', ',' };
            string sNewLine = "";
            //if (sLine.Contains("VOGO"))
            //    iPos = 0;
            string sTemp = sLine, strOp="", strOpNew, strOp2="",str1="",str2="";
            string[] straRep = new string[10];
            if (sLine.Contains("Sav"))
                iDex = 0;
            sTemp = sLine.Replace("\t", "");
            iPos = sTemp.IndexOf("invoke");
            if (iPos >= 0)
            {
                sTemp = sTemp.Substring(iPos + 7).Trim();
                iPos = sTemp.IndexOf("(");
                if(iPos >= 0)
                {
                    sNewLine = sTemp.Substring(0, iPos+1);
                    sTemp = sTemp.Substring(iPos+1);
                    iPos = sTemp.IndexOf(")");
                    if (iPos >= 0)
                    {
                        sTemp = sTemp.Substring(0, iPos);
                        while (sTemp.Length > 0)
                        {
                            string sTmp;
                            iPos = sTemp.IndexOf(",");
                            if (iPos >= 0)
                            {
                                sTmp = sTemp.Substring(0, iPos).Trim();
                                if (straStateVars.Contains(sTmp))
                                    sTmp = StateVar(sTmp, true);
                                else
                                    sTmp = Var(sTmp);
                                sNewLine += (sTmp + ",");
                                sTemp = sTemp.Substring(iPos+1);
                            }
                            else
                            {
                                sTmp = sTemp.Trim();
                                if (straStateVars.Contains(sTmp))
                                    sTmp = StateVar(sTmp, true);
                                else
                                    sTmp = Var(sTmp);
                                sNewLine += sTmp;
                                sTemp = "";
                            }
                        }
                        sNewLine += ");";
                        return sNewLine;
                    }
                }
            }
            if (sTemp.IndexOf("d/dt") >= 0)
            {
                sTemp = sTemp.Substring(5);
                bDer = true;
            }
//            strmW2.Write("B.0:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
 //           strmW2.WriteLine();
            while (sTemp.Length > 0)
            {
                iPos = sTemp.IndexOfAny(chSeps);
                if (iPos >= 0)
                {
                    if (iPos > 0)
                    {
                        sToken = sTemp.Substring(0, iPos).Trim();
                        if (sToken[sToken.Length - 1] == 'e' && sToken[0] >= '0' && sToken[0] <= '9')
                        {
                            iPos1 = sTemp.IndexOfAny(chSeps, iPos + 1);
                            if (iPos1 >= 0)
                            {
                                sToken = sTemp.Substring(0, iPos1).Trim();
                                sTemp = sTemp.Substring(iPos1);
                            }
                            else
                            {
                                sToken = sTemp;
                                sTemp = "";
                            }
                            iPos = sTemp.IndexOfAny(chSeps);
                        }
                        //else if (iPos >= 0)
                        //    sTemp = sTemp.Substring(iPos + 1);
                        //else
                        //    sTemp = "";
                        if (sToken == "STATE" || sToken == "ARRAY")
                        {
                            sTemp = "";
                            return sTemp;
                        }
                        if (sToken == "data")
                        {
                            //remember list of data items for next statement, return "" as this line produces nothing
                            listDataItems = new List<string>();
                            sTemp = sTemp.Substring(iPos + 1).Trim();
                            while (sTemp.Length > 0)
                            {
                                iPos = sTemp.IndexOf(',');
                                if (iPos >= 0)
                                {
                                    listDataItems.Add(sTemp.Substring(0,iPos));
                                    sTemp = sTemp.Substring(iPos+1);
                                }
                                else
                                {
                                    iPos = sTemp.IndexOf(";");
                                    if (iPos >= 0)
                                        sTemp = sTemp.Substring(0, iPos);
                                    listDataItems.Add(sTemp);
                                    sTemp = "";
                                }
                            }
                            return "";
                        }
                        if (sToken == "read")
                        {
                            //asumming for now that only values are in data, not expressions (but expressions generally possible)
                            string str = sTemp.Substring(iPos + 1).Trim(), sTmp;
                            iPos = str.IndexOf(";");
                            if (iPos >= 0)
                                str = str.Substring(0, iPos);
                            sTmp = str;  //upper limit
                            if (j >= 0 && straVars.Contains(sTmp))
                                sTmp = Var(sTmp);
                            else if (j >= 0 && straStateVars.Contains(sTmp))
                                sTmp = StateVar(sTmp, true);
                            sTemp = "";
                            for(int i = 1; i<listDataItems.Count+1; i++)
                                sTemp += string.Format("{0}[{1}] = {2};#", sTmp, i, listDataItems[i-1]);
                            return sTemp;
                        }
                        if (sToken == "for")
                        {
                            //find components, reformat as for loop initial line
                            string sTmp;
                            loopstrs = new string[4];
                            sTemp = sTemp.Substring(iPos + 1).Trim();
                            iPos = sTemp.IndexOf("=");
                            if(iPos>=0)
                            {
                                loopstrs[0] = sTemp.Substring(0, iPos).Trim();  //var name
                                sTemp = sTemp.Substring(iPos + 1).Trim();
                                iPos = sTemp.IndexOf(" ");
                                if (iPos >= 0)
                                {
                                    sTmp = sTemp.Substring(0, iPos).Trim(); //lower limit
                                    loopstrs[1] = sTmp;  
                                    sTemp = sTemp.Substring(iPos + 1).Trim().Substring(3).Trim();
                                    iPos = sTemp.IndexOf(" ");
                                    if (iPos >= 0)
                                    {//step will likely exist
                                        sTmp = sTemp.Substring(0, iPos).Trim();  //upper limit
                                        if (j >= 0 && straVars.Contains(sTmp))
                                            sTmp = Var(sTmp);
                                        else if (j >= 0 && straStateVars.Contains(sTmp))
                                            sTmp = StateVar(sTmp, true);
                                        loopstrs[2] = sTmp;
                                        sTemp = sTemp.Substring(iPos + 1).Trim().Substring(5).Trim();
                                        iPos = sTemp.IndexOf(";");
                                        if (iPos >= 0)
                                            sTemp = sTemp.Substring(0, iPos);
                                        loopstrs[3] = sTemp;    //step
                                    }
                                    else
                                    {//no step size
                                        iPos = sTemp.IndexOf(";");
                                        if (iPos >= 0)
                                            sTemp = sTemp.Substring(0, iPos);
                                        sTmp = sTemp;  //upper limit
                                        if (j >= 0 && straVars.Contains(sTmp))
                                            sTmp = Var(sTmp);
                                        else if (j >= 0 && straStateVars.Contains(sTmp))
                                            sTmp = StateVar(sTmp, true);
                                        loopstrs[2] = sTmp;
                                        sTemp = "";
                                        loopstrs[3] = "1";  //step
                                    }
                                    //remeber current position
                                    int jj = j + 1;
                                    //seek ahead in buffer line by line until "next" or target found - target is [lv]
                                    while (j < iNumLines)
                                    {
                                        string str = string.Format("[{0}]",loopstrs[0]);
                                        string strLine = lines[jj].sCode;
                                        iPos = strLine.IndexOf(str);
                                        if (iPos >= 0 || strLine.IndexOf("next") >= 0)
                                            break;
                                    }
                                    //if [lv] found, then must use integer lv and appropriate code, otherwise use float lv
                                    if(iPos>=0)
                                        sTemp = string.Format("int {0} = {1};#while ( {2} <= {3})#%", loopstrs[0], loopstrs[1], loopstrs[0], loopstrs[2]);
                                    else
                                        sTemp = string.Format("float {0} = {1};#while ( fabs({2}-{3})>1e-6)#%", loopstrs[0], loopstrs[1], loopstrs[0], loopstrs[2]);
                                    sTemp = sTemp.Replace("%","{");
                                    return sTemp;
                                }
                            }
                            return "";  // a failed for loop conversion - for loop will disappear on failure
                        }
                        if (sToken == "next")
                        {
                            sTemp = string.Format("{0} += {1};#%", loopstrs[0], loopstrs[3]);
                            sTemp = sTemp.Replace("%", "}");
                            return sTemp;
                        }
                        if (j>=0 && straVars.Contains(sToken))
                            sToken = Var(sToken);
                        else if (j >= 0 && straStateVars.Contains(sToken))
                            sToken = StateVar(sToken, sTemp[iPos] != '=');
                        else if (sToken == "t")
                            sToken = "(SIM_DATA)(m_sdT)";
                        else if (sToken == "ln")
                            sToken = "log";
                        else if (sToken == "log")
                            sToken = "log10";
                        //else - a system function, most likely, or a scientific number portion
                        sNewLine += sToken;
                        if (iPos >= 0 && sTemp.Length>0)
                            sTemp = sTemp.Substring(iPos);
                        else
                            sTemp = "";
                        if (sTemp.Length == 0)
                            break;
                    }
                    if (sTemp.ElementAt(0)=='/')
                    {
                        //numerator is at tail of sNewLine
                        //same thinking applies to '^' 
                        //need a FindOperand(string) function
                        strOp = FindOperand1(sNewLine, out iLoc);
                        if (strOp != "")
                        {
                            //strOpNew = String.Format("(SIM_DATA)({0})", strOp);
                            if (str1 != "" && str1.Contains(strOp + "/"))
                            {
                                straRep[iDex] = String.Format("(SIM_DATA)({0})", strOp);
                                strOpNew = String.Format("m_sd#{0}", iDex++);//make the op look like a variable for now
                                sNewLine = sNewLine.Substring(0, iLoc) + strOpNew + sNewLine.Substring(iLoc + strOp.Length);
                                string token = strOp + "/", token2 = strOpNew + "/";
                                ReplaceLastToken(ref str1, token, token2);
                                ReplaceLastToken(ref str2, token, token2);
                            }
                            else
                            {
                                strOpNew = String.Format("(SIM_DATA)({0})", strOp);//make the op look like a variable for now
                                sNewLine = sNewLine.Substring(0, iLoc) + strOpNew + sNewLine.Substring(iLoc + strOp.Length);
                            }
                        }
                    }
                    else if (sTemp.ElementAt(0) == '^')
                    {
                        strOp = FindOperand1(sNewLine, out iLoc);
                        strOp2 = FindOperand2(ref sTemp);
                        str1 = strOp + "^" + strOp2;
                        str2 = string.Format("pow({0},{1})", strOp, strOp2);
                    }
                    sNewLine += sTemp.Substring(0, 1);
                    if (1 < sTemp.Length)
                        sTemp = sTemp.Substring(1);
                    else
                        sTemp = "";
                    if (str1!="" && sNewLine.IndexOf(str1) >= 0)
                    {
                        sNewLine = sNewLine.Replace(str1, str2);
                        str1 = "";
                        str2 = "";
                    }
                }
            }//while
            //strmW2.Write("B.1:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            for (int i = iDex - 1; i >= 0; i--)
            {
                strOp = string.Format("m_sd#{0}",i);
                sNewLine = sNewLine.Replace(strOp, straRep[i]);
            }
            //strmW2.Write("B.2:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            if (j >= 0)
            {
                sTemp = "";
                while (sNewLine.Length > 0)
                {
                    iPos = sNewLine.IndexOf('=');
                    if (iPos >= 0)
                    {
                        sToken = sNewLine.Substring(0, iPos);
                        iPos1 = sNewLine.IndexOf(';');
                        if (iPos1 >= 0)
                        {
                            sVal = sNewLine.Substring(iPos + 1, iPos1 - iPos - 1);
                            if (iPos + 1 < sNewLine.Length)
                            {
                                sNewLine = sNewLine.Substring(iPos1 + 1).Trim();
                                bState = false;
                            }
                            else
                                sNewLine = "";
                        }
                        else
                        {
                            sVal = sNewLine.Substring(iPos + 1).Trim();
                            sNewLine = "";
                        }
                        if (sToken.Contains("m_sd") && straStateVars.Contains(sToken.Substring(4)))
                            bState = true;

                    }
                    else
                        sNewLine = sToken = sVal = "";
                    if (bDer)
                    {
                        if (iPos >= 0)
                            sTemp += String.Format("D({0}, {1}, iCol);", sToken, sVal);
                    }
                    else if (bState)
                    {
                        if (iPos >= 0)
                            sTemp += String.Format("S2({0}, {1});", sToken, sVal);
                    }
                    else
                        sTemp += String.Format("{0} = {1};", sToken, sVal);
                }
            }
            else
            {
                sTemp = sNewLine.Substring(6);  //remove "dummy="
            }
            //strmW2.Write("B.3:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            return sTemp;
        }

        public string DeclareFormals(ref string sTemp, SubModelInfo smi)
        {
            int iPos;
            string snm, sDec;
            iPos = sTemp.IndexOf(",");
            if (iPos >= 0)
            {
                snm = sTemp.Substring(0, iPos);
                if (smi.GetParmBit(snm))
                sDec = "SIM_DATA& ";
                else
                sDec = "SIM_DATA ";
                snm = sDec + snm + ",";
                sTemp = sTemp.Substring(iPos + 1);
            }
            else
            {
                snm = sTemp;
                if (smi.GetParmBit(snm))
                sDec = "SIM_DATA& ";
                else
                sDec = "SIM_DATA ";
                snm = sDec + snm;
                sTemp = "";
            }
            return snm;
        }

        public void ConvertFile()
        {
            string sLine, sLHS, sTemp, sBuf, sSMName;
            bool bS2 = false;
            int iNestingLevel = 0;
            //strmW2 = new System.IO.StreamWriter("C:\\ModelConverter\\MC.csv");
            lineElements stLineOut;
            int iPos, iPos1, iPos2, iPos3;
            StreamWriter strmW = new StreamWriter(sOutFileNameCpp);
            strmRfc = new StreamReader(sFileName);

            strmW.WriteLine("#include \"stdafx.h\"");
            sBuf = String.Format("#include \"{0}.h\"\r\n", sModName);
            strmW.WriteLine(sBuf);
            sBuf = String.Format("Sim::{0}::{1}()\r\n^", sModName, sModName);
            sBuf = sBuf.Replace('^', '{');
            strmW.WriteLine(sBuf);
            sBuf = String.Format("\tm_iaNSAsizes = NULL;\r\n\tm_iaSAsizes = NULL;\r\n\tm_iaCAsizes = NULL;\r\n\tm_baNSisG = NULL;\r\n\tm_baCisIC = NULL;\r\n\tm_baNSAisG = NULL;\r\n\tm_baSAisG = NULL;\r\n\tm_baCAisIC = NULL;");
            strmW.WriteLine(sBuf);
            if(listNonStateArrays.Count > 0)
            {
                sBuf = String.Format("\tm_iaNSAsizes = new int[{0}];\r\n\tint szs1[] = ({1}",listNonStateArrays.Count,listNonStateArrays[0].iSize);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listNonStateArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", listNonStateArrays[i].iSize);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^;\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_iaNSAsizes[i] = szs1[i];", listNonStateArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
                sBuf = String.Format("\tm_baNSAisG = new bool[{0}];\r\n\tint szs8[] = ({1}", listNonStateArrays.Count, (listNonStateArrays[0].sType == "User") ? 0 : 1);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listNonStateArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", (listNonStateArrays[i].sType == "User") ? 0 : 1);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^;\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_baNSAisG[i] = szs8[i];", listNonStateArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
            }
            sBuf = String.Format("\tm_baNSisG = new bool[NonStateSize()];\r\n\tint szs6[] = ^{0}", (viaData[0].sType == "User") ? 0 : 1);
            sBuf = sBuf.Replace('^', '{');
            strmW.Write(sBuf);
            for (int i = 1; i < viaData.Length; i++)
            {
                sBuf = String.Format(",{0}", (viaData[i].sType == "User") ? 0 : 1);
                strmW.Write(sBuf);
            }
            sBuf = String.Format("^;\r\n\tfor(int i = 0; i < NonStateSize(); i++)\r\n\t\tm_baNSisG[i] = szs6[i];\r\n");
            sBuf = sBuf.Replace('^', '}');
            strmW.WriteLine(sBuf);
            if (listStateArrays.Count > 0)
            {
                sBuf = String.Format("\tm_iaSAsizes = new int[{0}];\r\n\tint szs2[] = ({1}", listStateArrays.Count, listStateArrays[0].iSize);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listStateArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", listStateArrays[i].iSize);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^];\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_iaSAsizes[i] = szs2[i];", listStateArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
                sBuf = String.Format("\tm_baSAisG = new bool[{0}];\r\n\tint szs9[] = ({1}", listStateArrays.Count, (listStateArrays[0].sType == "User") ? 0 : 1);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listStateArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", (listStateArrays[i].sType == "User") ? 0 : 1);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^;\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_baSAisG[i] = szs9[i];", listStateArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
            }
            sBuf = String.Format("\tm_baSisG = new bool[StateSize()];\r\n\tint szs7[] = ^{0}", (viaStateData[0].sType == "User") ? 0 : 1);
            sBuf = sBuf.Replace('^', '{');
            strmW.Write(sBuf);
            for (int i = 1; i < viaStateData.Length; i++)
            {
                sBuf = String.Format(",{0}", (viaStateData[i].sType == "User") ? 0 : 1);
                strmW.Write(sBuf);
            }
            sBuf = String.Format("^;\r\n\tfor(int i = 0; i < StateSize(); i++)\r\n\t\tm_baSisG[i] = szs7[i];\r\n");
            sBuf = sBuf.Replace('^', '}');
            strmW.WriteLine(sBuf);
            if (listConstArrays.Count > 0)
            {
                sBuf = String.Format("\tm_iaCAsizes = new int[{0}];\r\n\tint szs3[] = ({1}", listConstArrays.Count, listConstArrays[0].iSize);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listConstArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", listConstArrays[i].iSize);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^;\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_iaCAsizes[i] = szs3[i];", listConstArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
                sBuf = String.Format("\tm_baCAisIC = new bool[{0}];\r\n\tint szs5[] = ({1}", listConstArrays.Count, (listConstArrays[0].sType == "Constant")?0:1);
                sBuf = sBuf.Replace('(', '{');
                strmW.Write(sBuf);
                for (int i = 1; i < listConstArrays.Count; i++)
                {
                    sBuf = String.Format(",{0}", (listConstArrays[i].sType == "Constant")?0:1);
                    strmW.Write(sBuf);
                }
                sBuf = String.Format("^;\r\n\tfor(int i = 0; i < {0}; i++)\r\n\t\tm_baCAisIC[i] = szs5[i];", listConstArrays.Count);
                sBuf = sBuf.Replace('^', '}');
                strmW.WriteLine(sBuf);
            }
            sBuf = String.Format("\tm_baCisIC = new bool[ConstSize()];\r\n\tint szs4[] = ^{0}", (viaConsts[0].sType=="Constant")?0:1);
            sBuf = sBuf.Replace('^', '{');
            strmW.Write(sBuf);
            for (int i = 1; i < viaConsts.Length; i++)
            {
                sBuf = String.Format(",{0}", (viaConsts[i].sType == "Constant") ? 0 : 1);
                strmW.Write(sBuf);
            }
            sBuf = String.Format("^;\r\n\tfor(int i = 0; i < ConstSize(); i++)\r\n\t\tm_baCisIC[i] = szs4[i];\r\n^\r\n");
            sBuf = sBuf.Replace('^', '}');

            strmW.WriteLine(sBuf);

            sBuf = String.Format("Sim::{0}::~{1}()\r\n^\r\n\tdelete [] m_iaNSAsizes;\r\n\tdelete [] m_iaSAsizes;\r\n\tdelete [] m_iaCAsizes;\r\n\tdelete [] m_baNSisG;\r\n\tdelete [] m_baSisG;\r\n\tdelete [] m_baCisIC;\r\n\tdelete [] m_baNSAisG;\r\n\tdelete [] m_baSAisG;\r\n\tdelete [] m_baCAisIC;\r\n%\r\n", sModName, sModName);
            sBuf = sBuf.Replace('^', '{');
            sBuf = sBuf.Replace('%', '}');
            strmW.WriteLine(sBuf);
            sBuf = String.Format("void Sim::{0}::SetModelParameters()\r\n[", sModName);
            sBuf = sBuf.Replace('[', '{');
            iNestingLevel = 1;
            strmW.WriteLine(sBuf);
            //strmW2.Write("Res:, {0}, {1}",Stopwatch.IsHighResolution?"true":"false",Stopwatch.Frequency);
            //strmW2.Write("A:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            sSMName = "";
            while (!strmRfc.EndOfStream)
            {
                sLine = strmRfc.ReadLine();
                sLine = sLine.Trim();
                sLine = sLine.Replace("\t", "");
                if (sLine.Contains("read "))
                {
                    sLHS = sLine.Substring(5).Trim();
                    if (!straVars.Contains(sLHS))
                        straVars[iVInd++] = sLHS;
                }
                if (sLine.Contains("FUNCTION "))
                    continue;
                if (sLine.Contains("SUBMODEL "))
                {
                    sTemp = sLine.Substring(9).Trim();
                    iPos = sTemp.IndexOf("(");
                    if (iPos >= 0)
                        sSMName = sTemp.Substring(0, iPos);
                }
                if ((sSMName.Length > 0) && (sLine == "end;"))
                    sSMName = "";
                iPos2 = sLine.IndexOf("irule");
                iPos3 = sLine.IndexOf("|");
                if(iPos2>=0 && iPos3>=0)
                {
                    if(iPos2>iPos3)
                    {
                        //irule follows first |
                        int it = iPos2;
                        while (it > iPos3 && sLine[it] != '|') it--;
                        iPos3 = it;
                        it = iPos2+5;
                        while (it < sLine.Length && (sLine[it] == ' ' || sLine[it] == '\t')) it++;
                        while (it < sLine.Length && (sLine[it] >= '0' || sLine[it] <= '9')) it++;
                        if (it >= sLine.Length)
                            iPos2 = sLine.Length-1;
                        else
                            iPos2 = it;
                        sLine = sLine.Replace(sLine.Substring(iPos3, iPos2 - iPos3 + 1), "");
                    }
                    else
                    {
                        //irule precedes first | - remove from start of irule to and including |
                        sLine = sLine.Replace(sLine.Substring(iPos2,iPos3-iPos2+1),"");
                    }
                    sLine = sLine.Trim();
                }
                if (sLine == "DYNAMIC")
                    bDyn = false;
                if (sLine.Length > 0)
                {
                    if (sLine[0] == '-' && sLine[1] == '-')
                    {
                        stLineOut.sComment = sLine;   //purely a comment line
                        stLineOut.sCode = "";
                    }
                    else     //starts with code
                    {
                        if (sLine.Contains("| --") || sLine.Contains("|--") || sLine.Contains("--"))     //ends with comment
                        {
                            iPos = sLine.IndexOf("--");
                           // if (iPos < 0)
                            //{
                                //    iPos = sLine.IndexOf("--");
                                stLineOut.sCode = sLine.Substring(0, iPos).Trim() + ";";
                                stLineOut.sComment = sLine.Substring(iPos + 2).Trim();
                            //}
                                if (stLineOut.sCode.IndexOf('|') >= 0) //multiple code lines in line, put in ';'
                                {
                                    stLineOut.sCode = stLineOut.sCode.Replace('|', ';');
                                    stLineOut.sCode = stLineOut.sCode.Replace(";;", ";");
                                }

                        }
                        else if (sLine.IndexOf('|') >= 0)   //code with no comments, possibly multiple code lines
                        {
                            stLineOut.sCode = sLine.Replace('|', ';') + ";";
                            stLineOut.sComment = "";
                        }
                        else //if (sLine.IndexOf('|') < 0)    //not explicitly terminated, so a single statement, add ';'
                        {
                            stLineOut.sCode = sLine + ";";
                            stLineOut.sComment = "";
                        }
                        //process code for information
                        sTemp = stLineOut.sCode;
                        while (sTemp.Length > 0)
                        {
                            iPos = sTemp.IndexOf('=');
                            if (iPos > 0)
                            {
                                sLHS = sTemp.Substring(0, iPos).Trim();
                                iPos = sTemp.IndexOf("d/dt");
                                iPos1 = sTemp.IndexOf(';');
                                if (iPos >= 0)
                                {   //add to state var list
                                    iPos = sLHS.IndexOf(' ');
                                    if (iPos > 0)
                                    {
                                        sLHS = sLHS.Substring(iPos + 1);
                                        bool bBlock = false;
                                        if (sSMName.Length > 0)
                                        {
                                            SubModelInfo smi = listSubModels.Find(item => item.Name() == sSMName);
                                            if(smi != null)
                                                bBlock = smi.IsFormal(sLHS);
                                        }
                                        if (!bBlock && !straStateVars.Contains(sLHS))
                                            straStateVars[iSvInd++] = sLHS;
                                        if ((iPos = Array.IndexOf(straVars, sLHS)) >= 0)
                                        {
                                            for (int i = iPos; i < iVInd - 1; i++)
                                                straVars[i] = straVars[i + 1];
                                            iVInd--;
                                        }
                                    }
                                    sTemp = "";
                                }
                                else
                                {   //add to var list
                                    bool bBlock = false;
                                    if (sSMName.Length > 0)
                                    {
                                        SubModelInfo smi = listSubModels.Find(item => item.Name() == sSMName);
                                        if (smi != null)
                                            bBlock = smi.IsFormal(sLHS);
                                    }
                                    iPos = sLHS.IndexOf("[");
                                    if (iPos >= 0)
                                        sLHS = sLHS.Substring(0,iPos);
                                    if (!bBlock && !straVars.Contains(sLHS))
                                        straVars[iVInd++] = sLHS;
                                    if (iPos1 + 1 < sTemp.Length)
                                        sTemp = sTemp.Substring(iPos1 + 1);
                                    else
                                        sTemp = "";
                                }
                            }
                            else
                                sTemp = "";
                        }//while
                    }//else starts with code
                    while (stLineOut.sCode.Contains(" ;"))
                        stLineOut.sCode = stLineOut.sCode.Replace(" ;", ";");
                    lines[iInd++] = stLineOut;
                }//if line not empty
            }//while read/write loop
            //strmW2.Write("B:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            sSMName = "";
            string sComm = "";
            for (int j = 0; j < iInd; j++)
            {
                if(lines[j].sCode.Contains("SUBMODEL "))
                {
                    // get name from line
                    sTemp = lines[j].sCode.Substring(9).Trim();
                    if (j > 0 && lines[j - 1].sComment.Trim().IndexOf("--") == 0)
                    {
                        sComm = lines[j - 1].sComment;
                        lines[j - 1].sComment = "";
                    }
                    iPos = sTemp.IndexOf("(");
                    if (iPos > 0)
                    {
                        sSMName = sTemp.Substring(0, iPos);
                        SubModelInfo smi;
                        smi = listSubModels.Find(item => item.Name() == sSMName);
                        sSMformals = smi.GetParams();
                        smi.AddComment(sComm);
                    }
                }
                if(sSMName.Length > 0 && lines[j].sCode == "end;")
                    sSMName = "";
                if (lines[j].sCode.Contains("invoke"))
                {
                }
                if (lines[j].sCode == "DYNAMIC;")
                {
                    bDyn = true;
                    //end init function
                    if (!bS2)
                    {
                        //end parameters function
                        strmW.WriteLine("}\r\n");
                        iNestingLevel = 0;
                        //start init func
                        sBuf = string.Format("void Sim::{0}::InitializeModelVars()", sModName);
                        strmW.WriteLine(sBuf);
                        strmW.WriteLine("{");
                        iNestingLevel = 1;
                        bS2 = true;     //do only once
                    }
                    strmW.WriteLine("}\r\n");
                    iNestingLevel = 0;
                    //start deriv func
                    sBuf = string.Format("void Sim::{0}::ModelDerivatives(int iCol)", sModName);
                    strmW.WriteLine(sBuf);
                    strmW.WriteLine("{");
                    iNestingLevel = 1;
                }
                if (lines[j].sCode.Length > 0)
                {
                    lines[j].sCode = ProcessLine(lines[j].sCode, j);
                    //if (lines[j].sCode.IndexOf("+=") < 0 && lines[j].sCode.IndexOf("-=") < 0)
                    //    lines[j].sCode = lines[j].sCode.Replace("=", " = ");
                    if (!bS2 && lines[j].sCode.IndexOf("S2(")>=0)
                    {
                        //end parameters function
                        strmW.WriteLine("}\r\n");
                        iNestingLevel = 0;
                        //start init func
                        sBuf = string.Format("void Sim::{0}::InitializeModelVars()",sModName);
                        strmW.WriteLine(sBuf);
                        strmW.WriteLine("{");
                        iNestingLevel = 1;
                        bS2 = true;     //do only once
                    }
                    else if (!bDyn)  // not DYNAMIC section, find IC vars, modify line
                    {
                        string sLineTmp;
                        sTemp = lines[j].sCode;
                        lines[j].sCode = "";
                        while (sTemp.Length > 0)
                        {
                            iPos1 = sTemp.IndexOf(";");
                            if (iPos1 >= 0)
                            {
                                sLineTmp = sTemp.Substring(0, iPos1 + 1);
                                if (iPos1 + 1 < sTemp.Length)
                                    sTemp = sTemp.Substring(iPos1 + 1);
                                else
                                    sTemp = "";
                                iPos = sLineTmp.IndexOf('=');
                                if (iPos >= 0)
                                {
                                    string sName = sLineTmp.Substring(0, iPos).Trim();
                                    sName = sName.Replace("#", "");
                                    sName = sName.Replace("m_sd", "");
                                    if (sName != "")
                                    {
                                        bool bFound = false;
                                        foreach (VariableInfo via in viaConsts)
                                        {
                                            if (via.sVarName == sName && via.sType != "Constant")
                                            {
                                                string s = "if (m_bIC) ";
                                                if (sLineTmp[0] == '#')
                                                {
                                                    sLineTmp = sLineTmp.Substring(1);
                                                    s = "#if (m_bIC) ";
                                                }
                                                lines[j].sCode += s + sLineTmp;
                                                bFound = true;
                                                break;
                                            }
                                        }
                                        if (!bFound)
                                        {
                                            foreach (ArrayInfo ai in listConstArrays)
                                            {
                                                iPos = sName.IndexOf("[");
                                                if (iPos >= 0)
                                                    sName = sName.Substring(0, iPos);
                                                if ((ai.sName == sName))
                                                {
                                                    if ((ai.sType != "Constant"))
                                                    {
                                                        string s = "if (m_bIC) ";
                                                        if (sLineTmp[0] == '#')
                                                        {
                                                            sLineTmp = sLineTmp.Substring(1);
                                                            s = "#if (m_bIC) ";
                                                        }
                                                        lines[j].sCode += s + sLineTmp;
                                                        bFound = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        if (!bFound)
                                            lines[j].sCode += sLineTmp;
                                    }
                                }
                                else
                                {
                                    lines[j].sCode += sLineTmp;
                                }
                            }
                            else
                            {
                                lines[j].sCode += sTemp;
                                sTemp = "";
                            }
                        }
                    }
                    if (lines[j].sCode != " = ;")
                    {
                        string strx;
                        while (lines[j].sCode.Length > 0)
                        {
                            iPos = lines[j].sCode.IndexOf("#");
                            if (iPos >= 0)
                            {
                                strx = lines[j].sCode.Substring(0, iPos);
                                lines[j].sCode = lines[j].sCode.Substring(iPos + 1);
                            }
                            else
                            {
                                strx = lines[j].sCode;
                                lines[j].sCode = "";
                            }
                            if (strx.IndexOf("]") >= 0 && !strx.Contains("STATE") && !strx.Contains("ARRAY"))
                                strx = strx.Replace("]", "-1]");
                            if (sSMName.Length > 0)
                            {
                                //find submodel object by name, add code and comments
                                listSubModels.Find(item => item.Name() == sSMName).AddCodeLine(strx, lines[j].sComment);
                            }
                            else
                            {
                                if (strx.Contains("}"))
                                    iNestingLevel--;
                                for (int ii = 0; ii < iNestingLevel; ii++)
                                    strmW.Write("\t");
                                strmW.WriteLine(strx);
                                if (strx.Contains("{"))
                                    iNestingLevel++;
                            }
                        }
                    }
                }
                if (sSMName.Length == 0 && lines[j].sComment.Length > 0)
                {
                    if (lines[j].sCode.Length > 0)
                        strmW.Write("\t\t");
                    strmW.Write("\t//");
                    strmW.Write(lines[j].sComment);
                    strmW.WriteLine();
                }
            }
            //end deriv function
            strmW.WriteLine("}\r\n");
            strmW.Close();
            int iAddS=0, iAddNS=0, iAddC=0;
            foreach (ArrayInfo ai in listConstArrays)
                iAddC += ai.iSize;
            foreach (ArrayInfo ai in listNonStateArrays)
                iAddNS += ai.iSize;
            foreach (ArrayInfo ai in listStateArrays)
                iAddS += ai.iSize;
            strmW = new System.IO.StreamWriter(sOutFileNameH);
            sBuf = string.Format("#pragma once\r\n#include \"SimDll13.h\"\r\n\r\nnamespace Sim");
            strmW.WriteLine(sBuf);
            strmW.WriteLine("{\r\n");
            sBuf = string.Format("\tclass SIMDLL_EXPORTS {0} : public Model",sModName);
            strmW.WriteLine(sBuf);
            strmW.WriteLine("\t{");
            sBuf = string.Format("\tpublic:\r\n\t\t{0}();\r\n\t\t~{1}();\r\n\t\tvoid SetModelParameters();\r\n\t\tvoid InitializeModelVars();\r\n", sModName, sModName);
            strmW.WriteLine(sBuf);
            sBuf = string.Format("\t\tvoid ModelDerivatives(int iCol);\r\n\t\tSIM_DATA* Base()[ return &m_sdBase; ]\r\n\t\tSIM_DATA* ConstBase()[ return &m_sdConstBase; ]");
            sBuf = sBuf.Replace('[', '{');
            sBuf = sBuf.Replace(']', '}');
            strmW.WriteLine(sBuf);
            sBuf = string.Format("\t\tchar* Name()[ return \"{0}\";]\r\n\t\tint StateSize()[ return {1};]\r\n\t\tint NonStateSize()[ return {2};]", sModName, iNumState + iAddS, iNumVars + iAddNS);
            sBuf = sBuf.Replace('[', '{');
            sBuf = sBuf.Replace(']', '}');
            strmW.WriteLine(sBuf);
            sBuf = string.Format("\t\tint ConstSize()[ return {0};]\r\n\t\tSIM_DATA YScale()[ return m_sdscale;]\r\n\t\tint NumCustIcTabs()[ return {1};]\r\n\r\n\t\t//FUNCTIONS\r\n", iNumConst + iAddC, iNumTypes - 1);
            sBuf = sBuf.Replace('[', '{');
            sBuf = sBuf.Replace(']', '}');
            strmW.WriteLine(sBuf);
            foreach(FuncInfo fi in listFuncs)
            {
                char[] ca = {','};
                sBuf = string.Format("\t\tSIM_DATA {0}(",fi.sName);
                foreach(string s in fi.sFormals)
                    sBuf += ("SIM_DATA " + s + ",");
                sBuf = sBuf.Trim(ca) + "){ return " + fi.sCode + "}\t//" + fi.sComment + "\r\n";
                strmW.WriteLine(sBuf);
            }
            sBuf = string.Format("\r\n\t\t//SUBMODELS\r\n");
            strmW.WriteLine(sBuf);
            foreach (SubModelInfo smi in listSubModels)
            {
                sBuf = string.Format("\tinline void {0}(",smi.Name());
                sTemp = smi.GetParams();
                while (sTemp.Length > 0)
                    sBuf += DeclareFormals(ref sTemp, smi);
                sBuf += ");";
                strmW.WriteLine(sBuf);
            }
            sBuf = string.Format("\r\n\tprivate:\r\n\t\tSIM_DATA m_sdBase;\r\n\t\t//define NS_VARS here");
            strmW.WriteLine(sBuf);
            //strmW2.Write("C:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            string str, str2;
            for (int j = 0; j < iNumVars; j++)
            {
                str = viaData[j].sType;
                if (listNonPlotGroups.Contains(str))
                {
                    listNonPlotGroups.Remove(str);
                    str += "$";
                }
                str = (str == "") ? "User" : str;
                str2 = viaData[j].sEngName;
                str2 = (str2 == "") ? viaData[j].sVarName:str2;
                strmW.Write("\t\tSIM_DATA {0};           //NS_VAR [{1}] [{2}] [{3}]", Var(viaData[j].sVarName), str2, str, viaData[j].sUnits);
                if (viaData[j].bStabItem)
                    strmW.Write(" stability");
                strmW.WriteLine();
            }
            foreach (ArrayInfo ai in listNonStateArrays)
            {
                strmW.Write("\t\tSIM_DATA {0}[{1}];           //NS_VAR [{2}] [{3}] [{4}]", Var(ai.sName), ai.iSize, ai.sEngName, ai.sType, ai.sUnits);//[{1}] [{2}] [{3}]", str2, str, viaData[j].sUnits);
                strmW.WriteLine();
            }
            sBuf = string.Format("\r\n\t\t//define state variables here");
            strmW.WriteLine(sBuf);
            for (int j = 0; j < iNumState; j++)
            {
                str = viaStateData[j].sType;
                if (listNonPlotGroups.Contains(str))
                {
                    listNonPlotGroups.Remove(str);
                    str += "$";
                }
                str = (str == "") ? "User" : str;
                str2 = viaStateData[j].sEngName;
                str2 = (str2 == "") ? viaStateData[j].sVarName : str2;
                strmW.Write("\t\tSIM_DATA *{0};           //STATE_VAR [{1}] [{2}] [{3}]", StateVar(viaStateData[j].sVarName, false), str2, str, viaStateData[j].sUnits);
                if (viaStateData[j].bStabItem)
                    strmW.Write(" stability");
                strmW.WriteLine();
            }
            foreach (ArrayInfo ai in listStateArrays)
            {
                strmW.Write("\t\tSIM_DATA* {0}[{1}];           //STATE_VAR [{2}] [{3}] [{4}]", StateVar(ai.sName, false), ai.iSize,ai.sEngName, ai.sType, ai.sUnits);//[{1}] [{2}] [{3}]", str2, str, viaData[j].sUnits);
                strmW.WriteLine();
            }
            sBuf = string.Format("\tprivate:\r\n\t\tSIM_DATA m_sdConstBase;\r\n\t\t//define CONST here");
            strmW.WriteLine(sBuf);
            for (int j = 0; j < iNumConst; j++)
            {
                str = viaConsts[j].sType;
                str=(str=="")?"Constant":str;
//                strmW.Write("\t\tSIM_DATA {0};           //CONST [{1}] [{2}] {3}", Var(viaConsts[j].sVarName), viaConsts[j].sEngName, str, viaConsts[j].sValue);
                strmW.Write("\t\tSIM_DATA {0};           //CONST [{1}] [{2}] [{3}]", Var(viaConsts[j].sVarName), viaConsts[j].sEngName, str, viaConsts[j].sUnits);
                if (viaConsts[j].bStabItem)
                    strmW.Write(" stability");
                strmW.WriteLine();
            }
            foreach (ArrayInfo ai in listConstArrays)
            {
                strmW.Write("\t\tSIM_DATA {0}[{1}];           //CONST [{2}] [{3}] [{4}]", Var(ai.sName), ai.iSize, ai.sEngName, ai.sType, ai.sUnits);
                strmW.WriteLine();
            }
            //strmW2.Write("D:,{0}", (double)sw.ElapsedTicks / Stopwatch.Frequency * 1000);
            //strmW2.WriteLine();
            strmW.WriteLine("\t};");
            strmW.WriteLine("}\r\n");
            foreach (SubModelInfo smi in listSubModels)
            {
                sBuf = smi.Comment();
                if (sBuf.Length > 0)
                {
                    sBuf = sBuf.Replace("--", "//");
                    strmW.WriteLine(sBuf);
                }
                sBuf = string.Format("void Sim::{0}::{1}(", sModName, smi.Name());
                sTemp = smi.GetParams();
                while (sTemp.Length > 0)
                    sBuf += DeclareFormals(ref sTemp, smi);
                sBuf += ")\r\n[\r\n";
                sBuf = sBuf.Replace('[', '{'); 
                strmW.WriteLine(sBuf);
                foreach(lineElements le in smi.listCode)
                {
                    sBuf = string.Format("\t{0}\t//{1}",le.sCode,le.sComment);
                    strmW.WriteLine(sBuf);
                }
                sBuf = "}\r\n";
                strmW.WriteLine(sBuf);
            }
            strmRfc.Close();
            strmW.Close();
            //strmW2.Close();
        }

        private void AddStateVar(string sLHS)
        {
            string sTmp;
            sTmp = "^" + sLHS + "^";
            if (sSVars.IndexOf(sTmp) < 0)
            {
                sSVars += (sLHS + "^");
                iNumState++;
            }
            if (sVars.IndexOf(sTmp) >= 0)
            {
                sVars = sVars.Replace(sTmp, "^");
                iNumVars--;
            }
            if (sConsts.IndexOf(sTmp) >= 0)
            {
                sConsts = sConsts.Replace(sTmp, "^");
                iNumConst--;
            }
        }

        private bool CheckMultiCommands(string sLine, int iPosX, out string sTemp)
        {
            int iPos;
            bool bResult = false;
            iPos = sLine.IndexOf("|");
            if (iPos >= 0)
            {
                sTemp = sLine.Substring(iPosX + 6, iPos - iPosX + 6).Trim();
                sLine = sLine.Substring(iPos + 1);
                bResult = true;
            }
            else
                sTemp = sLine.Substring(iPosX + 6).Trim();
            iPos = sTemp.IndexOf("--");
            if (iPos >= 0)
                sTemp = sTemp.Substring(0, iPos).Trim();
            return bResult;
        }

        private int GetNextArraySize()
        {
            int iPos, iSz;
            sSizes = sSizes.Substring(1);
            iPos = sSizes.IndexOf("^");
            iSz = Int32.Parse(sSizes.Substring(0, iPos).Trim());
            sSizes = sSizes.Substring(iPos);
            return iSz;
        }

        private void AppendSize(int iPos, int iPos1, String sTemp)
        {
            String sTmp;
            sTmp = sTemp.Substring(iPos + 1, iPos1 - iPos - 1);
            sSizes += (sTmp + "^");
        }

        private void GetArrayInfo(String sTemp, int iPosX, bool bStateFlag)
        {
            int iPos, iPos1, iPos2, iSz;
            String sTmp, sLHS;
            while (sTemp.Length > 0)
            {
                iPos2 = sTemp.IndexOf(",");
                if (iPos2 >= 0)
                {//still multiple elements in list
                    iPos = sTemp.IndexOf("[");
                    if (iPos >= 0 && iPos < iPos2)
                    {//an array
                        iPos1 = sTemp.IndexOf("]");
                        if (iPos1 > iPos)
                        {//array, get size var or const
                            sTmp = sTemp.Substring(0, iPos);
                            sTemp = sTemp.Substring(iPos + 1).Trim();
                            iPos = sTemp.IndexOf(",");
                            if (iPos >= 0)
                                sTemp = sTemp.Substring(iPos + 1).Trim();
                            iSz = GetNextArraySize();
                            //save sTmp, iSz
                            SaveArrayInfo(sTmp, iSz, bStateFlag ? varType.MC_STATE : varType.MC_CONSTANT);
                        }
                    }
                    else
                    {//not an array
                        sLHS = sTemp.Substring(0, iPos2).Trim();
                        sTemp = sTemp.Substring(iPos2 + 1).Trim();
                        AddStateVar(sLHS);
                    }
                }
                else
                {//last element
                    iPos = sTemp.IndexOf("[");
                    if (iPos >= 0)
                    {//an array
                        iPos1 = sTemp.IndexOf("]");
                        if (iPos1 > iPos)
                        {//array, get size var or const
                            sTmp = sTemp.Substring(0, iPos);
                            sTemp = "";
                            iSz = GetNextArraySize();
                            //save sTmp, iSz
                            SaveArrayInfo(sTmp, iSz, bStateFlag ? varType.MC_STATE : varType.MC_CONSTANT);
                        }
                    }
                    else
                    {//not an array
                        sLHS = sTemp.Substring(0, iPos2).Trim();
                        sTemp = "";
                        AddStateVar(sLHS);
                    }
                }
            }
        }

        private void SaveArrayInfo(String sName, int iSz, varType eVT)
        {
            switch(eVT)
            {
                case varType.MC_CONSTANT:
                    listConstArrays.Add(new ArrayInfo(sName, iSz));
                    break;
                case varType.MC_DATA:
                    listNonStateArrays.Add(new ArrayInfo(sName, iSz));
                    break;
                case varType.MC_STATE:
                    listStateArrays.Add(new ArrayInfo(sName, iSz));
                    break;
            }
        }

        private void UpdateArrayInfo(String sName)
        {
            foreach (ModelConverter.ArrayInfo ai in listConstArrays)
            {
                if (ai.sName == sName)
                {
                    listConstArrays.Remove(ai);
                    listNonStateArrays.Add(ai);
                }
            }
        }

        private void AnalyzeVariables()
        {
            //determine types and numbers of variables
            int iLineNum = 0;
            bool bSupressRead = false;
            int iLocDyn=1000000;
            string sLine, sLHS, sTemp, sTmp, sRemLine = "", sRHS, sSMName=""; 
            iNumVars = 0;
            iNumState = 0;
            iNumConst = 0;
            sSVars = "^";
            sVars = "^";
            sConsts = "^";
            sFormals = "";
            sSizes = "^";
            int iPos, iPos1, iPos2, iPos3, iPos4, iPos5, iPos6, iPos7, iPos8, iPos9, iPos10, iPos11;
            System.IO.StreamReader strmR = new System.IO.StreamReader(sFileName);

            //first pass to isolate arrays
            try
            {
                while (!strmR.EndOfStream)
                {
                    sLine = strmR.ReadLine().Trim();
                    if (sLine.IndexOf("ARRAY ") >= 0)
                    {
                        sTemp = sLine;
                        iPos = 0;
                        while (iPos >= 0)
                        {
                            iPos = sTemp.IndexOf("[");
                            if (iPos >= 0)
                            {
                                iPos1 = sTemp.IndexOf("]");
                                if (iPos1 > iPos)
                                {
                                    AppendSize(iPos, iPos1, sTemp);
                                    sTemp = sTemp.Substring(iPos1 + 1);
                                }
                                else
                                    iPos = -1;
                            }
                        }
                    }
                    if (sLine.IndexOf("STATE ") >= 0)
                    {
                        sTemp = sLine;
                        iPos = 0;
                        while (sTemp.Length > 0)
                        {
                            iPos2 = sTemp.IndexOf(",");
                            if (iPos2 >= 0)
                            {//still multiple elements in list
                                iPos = sTemp.IndexOf("[");
                                if (iPos >= 0 && iPos < iPos2)
                                {//an array
                                    iPos1 = sTemp.IndexOf("]");
                                    if (iPos1 > iPos)
                                    {//array, get size var or const
                                        AppendSize(iPos, iPos1, sTemp);
                                        sTemp = sTemp.Substring(iPos2 + 1);
                                    }
                                }
                                else
                                {//not an array
                                    sTemp = sTemp.Substring(iPos2 + 1);
                                }
                            }
                            else
                            {//last element, either type
                                iPos = sTemp.IndexOf("[");
                                if (iPos >= 0)
                                {//an array
                                    iPos1 = sTemp.IndexOf("]");
                                    if (iPos1 > iPos)
                                    {//array, get size var or const
                                        AppendSize(iPos, iPos1, sTemp);
                                        sTemp = "";
                                    }
                                }
                            }
                        }
                    }
                    if (sLine == "DYNAMIC")
                        break;
                }//while first pass
                strmR.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                int ii = 0;
            }
            try
            {
                while (!strmR.EndOfStream)
                {
                    if (!bSupressRead)
                        sLine = strmR.ReadLine();
                    else
                    {
                        sLine = sRemLine;
                        bSupressRead = false;
                    }
                    sLine = sLine.Trim();
                    sLine = sLine.Replace("\t", "");
                    iPos2 = sLine.IndexOf("irule");
                    iPos3 = sLine.IndexOf("|");
                    if (iPos2 >= 0 && iPos3 >= 0)
                    {
                        if (iPos2 > iPos3)
                        {
                            //irule follows first |
                            int it = iPos2;
                            while (it > iPos3 && sLine[it] != '|') it--;
                            iPos3 = it;
                            it = iPos2 + 5;
                            while (it < sLine.Length && (sLine[it] == ' ' || sLine[it] == '\t')) it++;
                            while (it < sLine.Length && (sLine[it] >= '0' || sLine[it] <= '9')) it++;
                            if (it >= sLine.Length)
                                iPos2 = sLine.Length - 1;
                            else
                                iPos2 = it;
                            sLine = sLine.Replace(sLine.Substring(iPos3, iPos2 - iPos3 + 1), "");
                        }
                        else
                        {
                            //irule precedes first | - remove from start of irule to and including |
                            sLine = sLine.Replace(sLine.Substring(iPos2, iPos3 - iPos2 + 1), "");
                        }
                        sLine = sLine.Trim();
                    }
                    if (sLine == "DYNAMIC")
                    {
                        iLocDyn = iLineNum;
                        bDyn = false;
                    }
                    if (sLine.Contains("| --") || sLine.Contains("|--") || sLine.Contains("--"))     //ends with comment
                    {
                        iPos = sLine.IndexOf("--");
                        if (iPos >= 0)
                            sLine = sLine.Substring(0, iPos);
                        else
                            sLine = "";
                    }
                    iPos4 = sLine.IndexOf("FUNCTION ");
                    iPos5 = sLine.IndexOf("SUBMODEL ");
                    iPos6 = sLine.IndexOf("end");
                    iPos7 = sLine.IndexOf("STATE ");
                    iPos8 = sLine.IndexOf("ARRAY ");
                    iPos9 = sLine.IndexOf("for ");
                    iPos10 = sLine.IndexOf("next");
                    iPos11 = sLine.IndexOf("invoke ");
                    if (iPos9 > 0)
                    {//see if "for" is a token or not
                        sTmp = String.Format("{0}for", sLine[iPos9 - 1]);
                        if (sTmp.Trim() != "for")
                            iPos9 = -1; //not a for loop, but var name containing "for"
                    }
                    if (iPos10 == 0)
                    {
                        if (sLine.Length > 4)
                        {
                            sTmp = String.Format("next{0}", sLine[4]);
                            if (sTmp.Trim() != "next")
                                iPos10 = -1; //"next" part of variable
                        }
                    }
                    else if (iPos10 > 0)
                    {//see if "next" is a token or not
                        sTmp = String.Format("{0}next", sLine[iPos10 - 1]);
                        if (sLine.Length > iPos10 + 4)
                            sTmp = String.Format("{0}next{1}", sLine[iPos10 - 1], sLine[iPos10 + 4]);
                        if (sTmp.Trim() != "next")
                            iPos10 = -1; //not a for loop, but var name containing "for"
                    }
                    if (iPos6 >= 0)
                    {//line contains "end"
                        if (sLine.Length > 3)
                            iPos6 = -1; //part of a variable, ignore
                    }
                    if (sLine.Length > 0)
                    {
                        if (sLine[0] == '-' && sLine[1] == '-')
                        {
                            //purely a comment line
                        }
                        else if (iPos4 >= 0)
                        {//function def, skip this line, no new variables here
                            //Collect list FUNCTION names
                            string sname="", scode="", sparams="", scomm="";
                            sTemp = sLine.Substring(iPos4 + 9).Trim();
                            iPos4 = sTemp.IndexOf("(");
                            if (iPos4 >= 0)
                            {
                                sname = sTemp.Substring(0, iPos4);
                                sTemp = sTemp.Substring(iPos4 + 1);
                                iPos4 = sTemp.IndexOf(")");
                                if(iPos4>=0)
                                {
                                    sparams = sTemp.Substring(0, iPos4).Trim();
                                    sTemp = sTemp.Substring(iPos4 + 1).Trim();
                                    iPos4 = sTemp.IndexOf("|");
                                    if (iPos4 >= 0)
                                    {
                                        scode = sTemp.Substring(0, iPos4).Trim();
                                        sTemp = sTemp.Substring(iPos4);
                                    }
                                    else
                                    {
                                        iPos4 = sTemp.IndexOf("--");
                                        if (iPos4 >= 0)
                                        {
                                            scode = sTemp.Substring(0, iPos4).Trim();
                                            sTemp = sTemp.Substring(iPos4);
                                        }
                                        else
                                        {
                                            scode = sTemp.Trim();
                                            sTemp = "";
                                        }
                                    }
                                    if (sTemp.Length>2)
                                    {
                                        iPos4 = sTemp.IndexOf("--");
                                        if (iPos4 >= 0)
                                            scomm = sTemp.Substring(iPos4 + 2).Trim();
                                        else
                                            scomm = "";
                                    }
                                }
                                scode = "dummy" + scode+";";
                                scode = ProcessLine(scode,-1);
                                if (scode.IndexOf(";") < 0)
                                    scode += ";";
                                FuncInfo fi = new FuncInfo(sname,scode,scomm);
                                //figure # of params, from sparams length and # of commas
                                while (sparams.Length > 0)
                                {
                                    iPos4 = sparams.IndexOf(',');
                                    if (iPos4 >= 0)
                                    {
                                        fi.AddParam(sparams.Substring(0, iPos4));
                                        sparams = sparams.Substring(iPos4 + 1);
                                    }
                                    else
                                    {
                                        fi.AddParam(sparams);
                                        sparams = "";
                                    }
                                }
                                listFuncs.Add(fi);
                            }
                            else
                            {
                            }
                            
                        }
                        else if (iPos5 >= 0)
                        {//submodel def, treat dummies appropriately,can still be variables created here
                            //identify dummies as these should be skipped in lines that follow
                            //make list of dummies, check all new variables against these until "end"
                            sFormals = "^";
                            iPos4 = sLine.IndexOf("(");
                            if (iPos4 >= 0)
                            {
                                string sFPList;
                                int ii = iPos5 + 9;
                                sSMName = sLine.Substring(ii, iPos4 - ii);
                                iPos5 = sLine.IndexOf(")");
                                if (iPos5 >= 0)
                                {
                                    sFPList = sLine.Substring(iPos4 + 1, iPos5 - iPos4 - 1);
                                    listSubModels.Add(new SubModelInfo(sSMName, sFPList,0));
                                    sFormals += sFPList.Replace(',', '^');
                                    sFormals += "^";
                                }
                            }
                        }
                        else if (iPos6 >= 0)
                        {//end of submodel reached
                            sFormals = "";
                            sSMName = "";
                        }
                        else if (iPos7 >= 0)
                        {//STATE declaration, add contents to state list
                            bSupressRead = CheckMultiCommands(sLine, iPos7, out sTemp);
                            GetArrayInfo(sTemp, iPos7, true);
                            if (bSupressRead)
                                continue;
                        }
                        else if (iPos8 >= 0)
                        {//ARRAY declaration, save name, size, type info
                            bSupressRead = CheckMultiCommands(sLine, iPos8, out sTemp);
                            GetArrayInfo(sTemp, iPos8, false);
                            if (bSupressRead)
                                continue;
                        }
                        else if (iPos9 >= 0)
                        {//for loop 
                        }
                        else if (iPos10 >= 0)
                        {//next
                        }
                        else if (iPos11 >= 0)
                        {//invoke
                            string sp, spl = "";
                            int ii = iPos11 + 7;
                            iPos1 = sLine.IndexOf("(");
                            if (iPos1 >= 0)
                            {
                                SubModelInfo smi;
                                sSMName = sLine.Substring(ii, iPos1 - ii);
                                smi = listSubModels.Find(item => item.Name() == sSMName);
                                if (smi != null)
                                {
                                    iPos2 = sLine.IndexOf(")");
                                    if (iPos2 >= 0)
                                        spl = sLine.Substring(iPos1 + 1, iPos2 - iPos1 - 1);
                                    ii = 0;
                                    while (spl.Length > 0)
                                    {
                                        iPos1 = spl.IndexOf(",");
                                        if (iPos1 >= 0)
                                        {
                                            sp = spl.Substring(0, iPos1);
                                            spl = spl.Substring(iPos1 + 1);
                                        }
                                        else
                                        {
                                            sp = spl;
                                            spl = "";
                                        }
                                        if (smi.GetParmBit(ii))
                                        {//corresponding fp was assigned to, so ap is assigned to
                                            iPos1 = sp.IndexOf("[");
                                            iPos2 = sp.IndexOf("]");
                                            if (iPos1 >= 0 && iPos2 > iPos1)
                                            {// an array, do nothing
                                            }
                                            else
                                            {//not an array
                                                sTmp = "^" + sp + "^";
                                                if (sSVars.IndexOf(sTmp) < 0 && sVars.IndexOf(sTmp) < 0)
                                                {//not in either, belongs in sVars; if belonged in sSVars, it would be there due to a STATE dec.
                                                    sVars += (sp + "^");
                                                    iNumVars++;
                                                }
                                            }
                                        }
                                        ii++;
                                    }//while
                                }//if smi!=null
                            }//if iPos1 >= 0
                            sSMName = "";
                        }
                        else     //starts with code
                        {
                            sTemp = sLine;
                            if (sTemp[sTemp.Length - 1] != '|')
                                sTemp += "|";
                            //process code for variables
                            while (sTemp.Length > 0)
                            {
                                iPos = sTemp.IndexOf('=');
                                if (iPos > 0)
                                {
                                    //replace variable in sizes with its value (expect number or simple variable)
                                    sLHS = sTemp.Substring(0, iPos).Trim();
                                   // if (sLHS == "TXSA")
                                    //    sLHS = "TXSA";
                                    iPos3 = sLHS.IndexOf("[");
                                    iPos4 = sLHS.IndexOf("]");
                                    if (iPos3 >= 0 && iPos4 > iPos3)
                                    {//sLHS is an array reference, so associated name is an array
                                        sLHS = sLHS.Substring(0, iPos3);    //remove the subscript reference from name to get variable name
                                        if (iLineNum > iLocDyn)
                                        { //array being assigned to in DYNAMIC saection, array can't be CONST, update
                                            UpdateArrayInfo(sLHS);
                                        }
                                        sTemp = "";
                                    }
                                    else
                                    {//sLHS is not an array
                                        sTmp = "^" + sLHS + "^";
                                        if (sSizes.IndexOf(sTmp) >= 0)
                                        {
                                            iPos1 = sTemp.IndexOf("|");
                                            if (iPos1 < 0)
                                            {
                                                iPos1 = sTemp.IndexOf("--");
                                                if (iPos1 < 0)
                                                    iPos1 = sTemp.Length;
                                            }
                                            sRHS = sTemp.Substring(iPos + 1, iPos1 - iPos - 1).Trim();
                                            sSizes = sSizes.Replace(sTmp, "^" + sRHS + "^");
                                            sSizes = sSizes.Replace(sTmp, "^" + sRHS + "^");    //do twice because overlapping patterns
                                        }
                                        //if (sFormals == "" || sFormals.IndexOf(sTmp) < 0)
                                        //{//do these steps if LHS var not in formals list
                                        iPos = sTemp.IndexOf("d/dt");
                                        iPos1 = sTemp.IndexOf('|');
                                        if (iPos >= 0)
                                        {   //add to state var list
                                            iPos = sLHS.IndexOf(' ');
                                            if (iPos > 0)
                                            {
                                                sLHS = sLHS.Substring(iPos + 1);
                                                AddStateVar(sLHS);
                                            }
                                            sTemp = "";
                                        }
                                        else
                                        {   //add to var list
                                            //sTmp = "^" + sLHS + "^";
                                            bool bIsFormal = sFormals.Length > 0 && sFormals.IndexOf(sTmp) >= 0;
                                            if (sVars.IndexOf(sTmp) < 0)
                                            {
                                                if (bIsFormal)
                                                {
                                                    listSubModels.Find(item => item.Name() == sSMName).SetParmBit(sLHS, true);
                                                }
                                                else
                                                {
                                                    sVars += (sLHS + "^");
                                                    iNumVars++;
                                                }
                                            }
                                            if (iLineNum < iLocDyn)
                                            { //may be constant
                                                if (sFormals.Length == 0 && sConsts.IndexOf(sTmp) < 0)
                                                {
                                                    sConsts += (sLHS + "^");
                                                    iNumConst++;
                                                }
                                            }
                                            else //in DYNAMIC section
                                            {
                                                if (sConsts.IndexOf(sTmp) >= 0)
                                                {
                                                    sConsts = sConsts.Replace(sTmp, "^");
                                                    iNumConst--;
                                                }
                                            }
                                            if (iPos1 + 1 < sTemp.Length)
                                                sTemp = sTemp.Substring(iPos1 + 1);
                                            else
                                                sTemp = "";
                                        }//if iPos
                                        //}//if sFormals
                                        //else
                                        //    sTemp = "";
                                    }
                                }//if iPos
                                else
                                    sTemp = "";
                            }//while
                        }//else starts with code
                    }//if line not empty
                    iLineNum++;
                }//while read loop
                iNumLines = iLineNum;
                strmR.Close();
            }
            catch (Exception e)
            {
                int ii = 0;
            }

            try
            {
                string str;
                int iIndex = 0;
                viaConsts = new VariableInfo[iNumConst];
                sConsts = sConsts.Substring(1);
                while (sConsts.Length > 0)
                {
                    iPos = sConsts.IndexOf('^');
                    if (iPos >= 0)
                    {
                        str = sConsts.Substring(0, iPos);
                        viaConsts[iIndex++].sVarName = str;
                        sConsts = sConsts.Substring(iPos + 1);
                        if (sVars.IndexOf("^" + str + "^") >= 0)
                        {
                            sVars = sVars.Replace("^" + str + "^", "^");
                            iNumVars--;
                        }
                    }
                }

                iIndex = 0;
                viaData = new VariableInfo[iNumVars];
                sVars = sVars.Substring(1);
                while (sVars.Length > 0)
                {
                    iPos = sVars.IndexOf('^');
                    if (iPos >= 0)
                    {
                        viaData[iIndex++].sVarName = sVars.Substring(0, iPos);
                        sVars = sVars.Substring(iPos + 1);
                    }
                }

                iIndex = 0;
                viaStateData = new VariableInfo[iNumState];
                sSVars = sSVars.Substring(1);
                while (sSVars.Length > 0)
                {
                    iPos = sSVars.IndexOf('^');
                    if (iPos >= 0)
                    {
                        viaStateData[iIndex++].sVarName = sSVars.Substring(0, iPos);
                        sSVars = sSVars.Substring(iPos + 1);
                    }
                }
            }// while, final processing
            catch (Exception e)
            {
                int ii = 0;
            }
        }

        private void ConvertClick(object sender, EventArgs e)
        {
            ConvertFile();
        }

        private void ModNameTextChanged(object sender, EventArgs e)
        {
            sModName = tbModName.Text;
            sOutFileNameCpp = sPath+sModName+".cpp";
            sOutFileNameH = sPath + sModName + ".h";
            tbOutName.Text = sOutFileNameH + ", " + sOutFileNameCpp;
        }

        private void ClickICs(object sender, EventArgs e)
        {
            formIC = new FormICs("Initial Conditions", sFileName, viaConsts, saTypes,iNumTypes);
            if (formIC.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            iNumTypes = formIC.NumTypes();
        }

        private void VIout(ref string str, VariableInfo vi, bool bConst)
        {
            string sType, sName, sUnits;
            if (vi.sType != null)
                sType = vi.sType;
            else
                sType = "";
            if (sType == "" && bConst)
                sType = "Constant";
            else if (sType == "" && !bConst)
                sType = "User";
            if (vi.sEngName != null)
                sName = vi.sEngName;
            else
                sName = "";
            if (vi.sUnits != null)
                sUnits = vi.sUnits;
            else
                sUnits = "";
            str = string.Format("{0},{1},{2},{3}", vi.sVarName, sType, sName, sUnits);
            if (vi.bStabItem)
                str += ",stability";
        }

        private void WriteFuncInfo(StreamWriter strmW)
        {
            string str = string.Format("Functions: {0}", listFuncs.Count);
            strmW.WriteLine(str);
            foreach (FuncInfo fi in listFuncs)
                strmW.WriteLine(fi.DumpInfo());
        }

        private void WriteSMInfo(StreamWriter strmW)
        {
            string str = string.Format("Submodels: {0}", listSubModels.Count);
            strmW.WriteLine(str);
            foreach (SubModelInfo smi in listSubModels)
                strmW.WriteLine(smi.DumpInfo());
        }

        private void WriteVarinfo(frmModConv.varType vt, StreamWriter sw)
        {
            List<ArrayInfo> lai;
            VariableInfo[] via;
            int iSum = 0, iNum;
            string str, sLabel;
            bool bFlag;
            switch (vt)
            {
                case varType.MC_DATA:
                    lai = listNonStateArrays;
                    via = viaData;
                    iNum = iNumVars;
                    sLabel = "NSData";
                    bFlag = false;
                    break;
                case varType.MC_STATE:
                    lai = listStateArrays;
                    via = viaStateData;
                    iNum = iNumState;
                    sLabel = "StateData";
                    bFlag = false;
                    break;
                default: 
                    lai = listConstArrays;
                    via = viaConsts;
                    iNum = iNumConst;
                    sLabel = "Constants";
                    bFlag = true;
                    break;
            }
            foreach (ArrayInfo ai in lai)
                iSum += ai.iSize;
            str = string.Format("{0}: {1},{2}", sLabel, iNum, lai.Count);
            sw.WriteLine(str);
            if (via != null)
            {
                foreach (VariableInfo vi in via)
                {
                    VIout(ref str, vi, bFlag);
                    sw.WriteLine(str);
                }
                foreach (ArrayInfo ai in lai)
                {
                    VariableInfo vi = new VariableInfo(ai);
                    VIout(ref str, vi, bFlag);
                    sw.WriteLine(str);
                }
            }
            else
                sw.WriteLine("");
        }

        private void FormClosingModConv(object sender, FormClosingEventArgs e)
        {
            string str;
            StreamWriter strmW=null;
            int iCnt = 0;
            while (iCnt < 3)
            {
                try
                {
                    strmW = new StreamWriter(sPath + sModName + ".dat", false);
                    strmW.WriteLine(sFileName);
                    str = string.Format("NumLines: {0}", iNumLines);
                    strmW.WriteLine(str);
                    str = string.Format("NumCustTypes: {0}", iNumTypes-1);
                    strmW.WriteLine(str);
                    for (int i = 1; i < iNumTypes; i++)
                    {
                        str = string.Format("{0}", saTypes[i]);
                        if (i < iNumTypes - 1)
                            str += ",";
                        strmW.Write(str);
                    }
                    strmW.WriteLine();
                    str = string.Format("NumCustGroups: {0}", iNumGroups - 1);
                    strmW.WriteLine(str);
                    for (int i = 1; i < iNumGroups; i++)
                    {
                        str = string.Format("{0}", saGroups[i]);
                        if (i < iNumGroups - 1)
                            str += ",";
                        strmW.Write(str);
                    }
                    strmW.WriteLine();
                    WriteVarinfo(varType.MC_CONSTANT, strmW);
                    WriteVarinfo(varType.MC_DATA, strmW);
                    WriteVarinfo(varType.MC_STATE, strmW);
                    WriteFuncInfo(strmW);
                    WriteSMInfo(strmW);
                    strmW.Close();
                    iCnt = 3;
                }
                catch (Exception ee)
                {
                    if (strmW != null)
                        strmW.Close();
                    MessageBox.Show(ee.Message);
                    iCnt++;
                }
            }//while iCnt
        }

        private void ClickPlotGroups(object sender, EventArgs e)
        {
            formPG = new FormICs("Plot Groups", sFileName, viaData, saGroups, iNumGroups);
            formPG.AddStateVars(viaStateData);
            if (formPG.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            iNumGroups = formPG.NumTypes();
        }
    }


    public class FuncInfo
    {
        public string sName;
        public string sCode;
        public string sComment;
        public List<string> sFormals;

        public FuncInfo(String sn, string sc, string scm)
        {
            sName = sn;
            sCode = sc;
            sComment = scm;
            sFormals = new List<string>();
        }

        public void AddParam(string sn)
        {
            sFormals.Add(sn);
        }

        public string DumpInfo()
        {
            string str;
            str = string.Format("{0}!&{1}!&{2}!&", sName, sCode, sComment);
            while (sFormals.Count > 0)
            {
                str += sFormals[0];
                sFormals.RemoveAt(0);
                if (sFormals.Count > 1)
                    str += "!&";
            }
            return str;
        }

    }

    public class SubModelInfo
    {
        private string sName;
        private int iParamBits;  //each bit (up to 32) represents a formal param, 1 means assigned to
        private string sFormalList;
        private string sComment;
        public List<lineElements> listCode;

        public SubModelInfo(string snm, string sfl, int iBits)
        {
            sName = snm;
            sFormalList = sfl;
            iParamBits = iBits;
            listCode = new List<lineElements>();
        }

        public string DumpInfo()
        {
            string str = string.Format("{0}!&{1}!&{2}", sName, sFormalList, iParamBits);
            return str;
        }

        public void AddComment(string scm)
        {
            sComment = scm;
        }

        public string Comment()
        {
            return sComment;
        }

        public void ResetBits()
        {
            iParamBits = 0;
        }

        public String Name()
        {
            return sName;
        }

        public string GetParams()
        {
            return sFormalList;
        }

        public bool IsFormal(string sn)
        {
            string sfp, sTmp;
            sTmp = sFormalList;
            while (sTmp != null && sTmp.Length > 0)
            {
                int iPos = sTmp.IndexOf(",");
                if (iPos >= 0)
                {
                    sfp = sTmp.Substring(0, iPos);
                    sTmp = sTmp.Substring(iPos + 1);
                }
                else
                {
                    sfp = sTmp;
                    sTmp = "";
                }
                if (sfp == sn)
                    return true;
            }
            return false;
        }

        public void AddCodeLine(string sLine, string sComm)
        {
            lineElements le = new lineElements();
            le.sCode = sLine;
            le.sComment = sComm;
            listCode.Add(le);
        }

        public int GetFplPos(string sfp)
        {
            int i = 0, iPos;
            string sp, sTmp = sFormalList;
            while (sTmp != null && sTmp.Length > 0)
            {
                iPos = sTmp.IndexOf(",");
                if (iPos >= 0)
                {
                    sp = sTmp.Substring(0, iPos);
                    sTmp = sTmp.Substring(iPos + 1);
                }
                else
                {
                    sp = sTmp;
                    sTmp = "";
                }
                if (sp == sfp)
                    break;
                i++;
            }
            return i;
        }

        public void SetParmBit(string sfp, bool bAssignedTo)
        {
            SetParmBit(GetFplPos(sfp), bAssignedTo);
        }

        public void SetParmBit(int iBit, bool bAssignedTo)
        {
            int iBitVal = 1 << iBit;
            if (bAssignedTo)
                iParamBits |= iBitVal;
            else if ((iParamBits & iBitVal) != 0)
                iParamBits -= iBitVal;
        }

        public bool GetParmBit(string sfp)
        {
            return GetParmBit(GetFplPos(sfp));
        }

        public bool GetParmBit(int iBit)
        {
            int iBitVal = 1 << iBit;
            return (iParamBits & iBitVal) != 0;
        }

    }

}
