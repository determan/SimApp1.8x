namespace SimApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labSimParms = new System.Windows.Forms.Label();
            this.labMaxTime = new System.Windows.Forms.Label();
            this.tbMaxTime = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.labVarsToPlot = new System.Windows.Forms.Label();
            this.pnlSimParms = new System.Windows.Forms.Panel();
            this.labRunning = new System.Windows.Forms.Label();
            this.btAbort = new System.Windows.Forms.Button();
            this.rbReplay = new System.Windows.Forms.RadioButton();
            this.rbLive = new System.Windows.Forms.RadioButton();
            this.btSaveSim = new System.Windows.Forms.Button();
            this.cbReplayList = new System.Windows.Forms.ComboBox();
            this.labDone = new System.Windows.Forms.Label();
            this.labReplay = new System.Windows.Forms.Label();
            this.pnlPlotParms = new System.Windows.Forms.Panel();
            this.chkDelay = new System.Windows.Forms.CheckBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.chkAutoScale = new System.Windows.Forms.CheckBox();
            this.btCSV = new System.Windows.Forms.Button();
            this.btClearToPlot = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.labLess = new System.Windows.Forms.Label();
            this.btData = new System.Windows.Forms.Button();
            this.labMore = new System.Windows.Forms.Label();
            this.btPlot = new System.Windows.Forms.Button();
            this.btPlotScale = new System.Windows.Forms.Button();
            this.lbItemsToPlot = new System.Windows.Forms.ListBox();
            this.tbarDataScale = new System.Windows.Forms.TrackBar();
            this.lbAllPlotItems = new System.Windows.Forms.ListBox();
            this.tbPlotFilterStr = new System.Windows.Forms.TextBox();
            this.labPlotFilterStr = new System.Windows.Forms.Label();
            this.labPlotDisplayGroups = new System.Windows.Forms.Label();
            this.chDataGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btReset = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlInitVals = new System.Windows.Forms.Panel();
            this.btICSets = new System.Windows.Forms.Button();
            this.tcInit = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.ItemLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemUnits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labInitVals = new System.Windows.Forms.Label();
            this.dgResults = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataPlotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stabilityPlotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panStabPlots = new System.Windows.Forms.Panel();
            this.chStab = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chNyq = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chBodePhase = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chBodeAmp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pnlSimParms.SuspendLayout();
            this.pnlPlotParms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarDataScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chDataGraph)).BeginInit();
            this.pnlInitVals.SuspendLayout();
            this.tcInit.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panStabPlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chStab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chNyq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chBodePhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chBodeAmp)).BeginInit();
            this.SuspendLayout();
            // 
            // labSimParms
            // 
            this.labSimParms.AutoSize = true;
            this.labSimParms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labSimParms.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labSimParms.Location = new System.Drawing.Point(7, 5);
            this.labSimParms.Name = "labSimParms";
            this.labSimParms.Size = new System.Drawing.Size(111, 13);
            this.labSimParms.TabIndex = 6;
            this.labSimParms.Text = "Simulation Parameters";
            // 
            // labMaxTime
            // 
            this.labMaxTime.AutoSize = true;
            this.labMaxTime.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labMaxTime.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labMaxTime.Location = new System.Drawing.Point(7, 25);
            this.labMaxTime.Name = "labMaxTime";
            this.labMaxTime.Size = new System.Drawing.Size(91, 13);
            this.labMaxTime.TabIndex = 9;
            this.labMaxTime.Text = "Maximum Time (s)";
            // 
            // tbMaxTime
            // 
            this.tbMaxTime.Location = new System.Drawing.Point(100, 22);
            this.tbMaxTime.Name = "tbMaxTime";
            this.tbMaxTime.Size = new System.Drawing.Size(95, 20);
            this.tbMaxTime.TabIndex = 3;
            this.tbMaxTime.TextChanged += new System.EventHandler(this.MaxTimeTextChanged);
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExecute.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExecute.Location = new System.Drawing.Point(210, 23);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 22);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.ClickExecute);
            this.btnExecute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ExecuteMouseClick);
            // 
            // labVarsToPlot
            // 
            this.labVarsToPlot.AutoSize = true;
            this.labVarsToPlot.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labVarsToPlot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labVarsToPlot.Location = new System.Drawing.Point(365, 5);
            this.labVarsToPlot.Name = "labVarsToPlot";
            this.labVarsToPlot.Size = new System.Drawing.Size(83, 13);
            this.labVarsToPlot.TabIndex = 12;
            this.labVarsToPlot.Text = "Variables to Plot";
            // 
            // pnlSimParms
            // 
            this.pnlSimParms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlSimParms.Controls.Add(this.labRunning);
            this.pnlSimParms.Controls.Add(this.labSimParms);
            this.pnlSimParms.Controls.Add(this.labMaxTime);
            this.pnlSimParms.Controls.Add(this.tbMaxTime);
            this.pnlSimParms.Controls.Add(this.btAbort);
            this.pnlSimParms.Controls.Add(this.btnExecute);
            this.pnlSimParms.Controls.Add(this.rbReplay);
            this.pnlSimParms.Controls.Add(this.rbLive);
            this.pnlSimParms.Controls.Add(this.btSaveSim);
            this.pnlSimParms.Controls.Add(this.cbReplayList);
            this.pnlSimParms.Controls.Add(this.labDone);
            this.pnlSimParms.Controls.Add(this.labReplay);
            this.pnlSimParms.Location = new System.Drawing.Point(3, 26);
            this.pnlSimParms.Name = "pnlSimParms";
            this.pnlSimParms.Size = new System.Drawing.Size(300, 169);
            this.pnlSimParms.TabIndex = 0;
            // 
            // labRunning
            // 
            this.labRunning.AutoSize = true;
            this.labRunning.BackColor = System.Drawing.SystemColors.ControlText;
            this.labRunning.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labRunning.Location = new System.Drawing.Point(125, 5);
            this.labRunning.Name = "labRunning";
            this.labRunning.Size = new System.Drawing.Size(67, 13);
            this.labRunning.TabIndex = 47;
            this.labRunning.Text = "RUNNING...";
            this.labRunning.Visible = false;
            // 
            // btAbort
            // 
            this.btAbort.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btAbort.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btAbort.Location = new System.Drawing.Point(210, 49);
            this.btAbort.Name = "btAbort";
            this.btAbort.Size = new System.Drawing.Size(75, 22);
            this.btAbort.TabIndex = 5;
            this.btAbort.Text = "Abort";
            this.btAbort.UseVisualStyleBackColor = false;
            this.btAbort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AbortMouseClick);
            // 
            // rbReplay
            // 
            this.rbReplay.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbReplay.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbReplay.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbReplay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbReplay.Location = new System.Drawing.Point(229, 140);
            this.rbReplay.Name = "rbReplay";
            this.rbReplay.Size = new System.Drawing.Size(65, 22);
            this.rbReplay.TabIndex = 46;
            this.rbReplay.TabStop = true;
            this.rbReplay.Text = "Replay";
            this.rbReplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbReplay.UseVisualStyleBackColor = false;
            this.rbReplay.CheckedChanged += new System.EventHandler(this.CheckChangedReplay);
            this.rbReplay.Click += new System.EventHandler(this.ClickReplay);
            // 
            // rbLive
            // 
            this.rbLive.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLive.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbLive.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLive.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbLive.Location = new System.Drawing.Point(229, 114);
            this.rbLive.Name = "rbLive";
            this.rbLive.Size = new System.Drawing.Size(65, 22);
            this.rbLive.TabIndex = 45;
            this.rbLive.TabStop = true;
            this.rbLive.Text = "Live";
            this.rbLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLive.UseVisualStyleBackColor = false;
            this.rbLive.CheckedChanged += new System.EventHandler(this.CheckChangedLive);
            this.rbLive.Click += new System.EventHandler(this.ClickLive);
            // 
            // btSaveSim
            // 
            this.btSaveSim.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btSaveSim.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btSaveSim.Location = new System.Drawing.Point(100, 50);
            this.btSaveSim.Name = "btSaveSim";
            this.btSaveSim.Size = new System.Drawing.Size(95, 22);
            this.btSaveSim.TabIndex = 26;
            this.btSaveSim.Text = "Save Simulation";
            this.btSaveSim.UseVisualStyleBackColor = false;
            this.btSaveSim.Click += new System.EventHandler(this.ClickSaveSim);
            // 
            // cbReplayList
            // 
            this.cbReplayList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReplayList.FormattingEnabled = true;
            this.cbReplayList.Location = new System.Drawing.Point(5, 140);
            this.cbReplayList.Name = "cbReplayList";
            this.cbReplayList.Size = new System.Drawing.Size(190, 21);
            this.cbReplayList.TabIndex = 23;
            this.cbReplayList.SelectedIndexChanged += new System.EventHandler(this.ReplayListSelIndChanged);
            // 
            // labDone
            // 
            this.labDone.AutoSize = true;
            this.labDone.BackColor = System.Drawing.SystemColors.ControlText;
            this.labDone.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labDone.Location = new System.Drawing.Point(244, 5);
            this.labDone.Name = "labDone";
            this.labDone.Size = new System.Drawing.Size(38, 13);
            this.labDone.TabIndex = 10;
            this.labDone.Text = "DONE";
            this.labDone.Visible = false;
            // 
            // labReplay
            // 
            this.labReplay.AutoSize = true;
            this.labReplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labReplay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labReplay.Location = new System.Drawing.Point(4, 120);
            this.labReplay.Name = "labReplay";
            this.labReplay.Size = new System.Drawing.Size(59, 13);
            this.labReplay.TabIndex = 22;
            this.labReplay.Text = "Replay List";
            // 
            // pnlPlotParms
            // 
            this.pnlPlotParms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlPlotParms.Controls.Add(this.chkDelay);
            this.pnlPlotParms.Controls.Add(this.tbStatus);
            this.pnlPlotParms.Controls.Add(this.chkAutoScale);
            this.pnlPlotParms.Controls.Add(this.labVarsToPlot);
            this.pnlPlotParms.Controls.Add(this.btCSV);
            this.pnlPlotParms.Controls.Add(this.btClearToPlot);
            this.pnlPlotParms.Controls.Add(this.labStatus);
            this.pnlPlotParms.Controls.Add(this.labLess);
            this.pnlPlotParms.Controls.Add(this.btData);
            this.pnlPlotParms.Controls.Add(this.labMore);
            this.pnlPlotParms.Controls.Add(this.btPlot);
            this.pnlPlotParms.Controls.Add(this.btPlotScale);
            this.pnlPlotParms.Controls.Add(this.lbItemsToPlot);
            this.pnlPlotParms.Controls.Add(this.tbarDataScale);
            this.pnlPlotParms.Controls.Add(this.lbAllPlotItems);
            this.pnlPlotParms.Controls.Add(this.tbPlotFilterStr);
            this.pnlPlotParms.Controls.Add(this.labPlotFilterStr);
            this.pnlPlotParms.Controls.Add(this.labPlotDisplayGroups);
            this.pnlPlotParms.Location = new System.Drawing.Point(305, 26);
            this.pnlPlotParms.Name = "pnlPlotParms";
            this.pnlPlotParms.Size = new System.Drawing.Size(815, 170);
            this.pnlPlotParms.TabIndex = 14;
            // 
            // chkDelay
            // 
            this.chkDelay.AutoSize = true;
            this.chkDelay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkDelay.Location = new System.Drawing.Point(5, 125);
            this.chkDelay.Name = "chkDelay";
            this.chkDelay.Size = new System.Drawing.Size(74, 17);
            this.chkDelay.TabIndex = 48;
            this.chkDelay.Text = "Delay Plot";
            this.chkDelay.UseVisualStyleBackColor = true;
            this.chkDelay.CheckStateChanged += new System.EventHandler(this.CheckChangedDelay);
            this.chkDelay.Click += new System.EventHandler(this.ClickDelay);
            // 
            // tbStatus
            // 
            this.tbStatus.BackColor = System.Drawing.SystemColors.Info;
            this.tbStatus.Location = new System.Drawing.Point(45, 140);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(310, 20);
            this.tbStatus.TabIndex = 10;
            // 
            // chkAutoScale
            // 
            this.chkAutoScale.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoScale.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkAutoScale.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkAutoScale.Location = new System.Drawing.Point(5, 50);
            this.chkAutoScale.Name = "chkAutoScale";
            this.chkAutoScale.Size = new System.Drawing.Size(65, 22);
            this.chkAutoScale.TabIndex = 47;
            this.chkAutoScale.Text = "AutoScale";
            this.chkAutoScale.UseVisualStyleBackColor = false;
            this.chkAutoScale.CheckedChanged += new System.EventHandler(this.ChkChgAutoScale);
            this.chkAutoScale.Click += new System.EventHandler(this.ClickAutoScale);
            // 
            // btCSV
            // 
            this.btCSV.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btCSV.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btCSV.Location = new System.Drawing.Point(648, 128);
            this.btCSV.Name = "btCSV";
            this.btCSV.Size = new System.Drawing.Size(65, 22);
            this.btCSV.TabIndex = 46;
            this.btCSV.Text = "CSV";
            this.btCSV.UseVisualStyleBackColor = false;
            this.btCSV.Click += new System.EventHandler(this.ClickCSV);
            // 
            // btClearToPlot
            // 
            this.btClearToPlot.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btClearToPlot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btClearToPlot.Location = new System.Drawing.Point(450, 5);
            this.btClearToPlot.Name = "btClearToPlot";
            this.btClearToPlot.Size = new System.Drawing.Size(39, 19);
            this.btClearToPlot.TabIndex = 45;
            this.btClearToPlot.Text = "Clear";
            this.btClearToPlot.UseVisualStyleBackColor = false;
            this.btClearToPlot.Visible = false;
            this.btClearToPlot.Click += new System.EventHandler(this.ClickClearToPlot);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labStatus.Location = new System.Drawing.Point(5, 144);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(37, 13);
            this.labStatus.TabIndex = 15;
            this.labStatus.Text = "Status";
            // 
            // labLess
            // 
            this.labLess.AutoSize = true;
            this.labLess.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labLess.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labLess.Location = new System.Drawing.Point(758, 75);
            this.labLess.Name = "labLess";
            this.labLess.Size = new System.Drawing.Size(55, 13);
            this.labLess.TabIndex = 35;
            this.labLess.Text = "Less Data";
            // 
            // btData
            // 
            this.btData.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btData.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btData.Location = new System.Drawing.Point(648, 100);
            this.btData.Name = "btData";
            this.btData.Size = new System.Drawing.Size(65, 22);
            this.btData.TabIndex = 43;
            this.btData.Text = "Data";
            this.btData.UseVisualStyleBackColor = false;
            this.btData.Click += new System.EventHandler(this.ClickData);
            // 
            // labMore
            // 
            this.labMore.AutoSize = true;
            this.labMore.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labMore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labMore.Location = new System.Drawing.Point(653, 75);
            this.labMore.Name = "labMore";
            this.labMore.Size = new System.Drawing.Size(57, 13);
            this.labMore.TabIndex = 34;
            this.labMore.Text = "More Data";
            // 
            // btPlot
            // 
            this.btPlot.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btPlot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPlot.Location = new System.Drawing.Point(5, 100);
            this.btPlot.Name = "btPlot";
            this.btPlot.Size = new System.Drawing.Size(65, 22);
            this.btPlot.TabIndex = 42;
            this.btPlot.Text = "Plot";
            this.btPlot.UseVisualStyleBackColor = false;
            this.btPlot.Click += new System.EventHandler(this.ClickPlot);
            // 
            // btPlotScale
            // 
            this.btPlotScale.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btPlotScale.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPlotScale.Location = new System.Drawing.Point(5, 75);
            this.btPlotScale.Name = "btPlotScale";
            this.btPlotScale.Size = new System.Drawing.Size(65, 22);
            this.btPlotScale.TabIndex = 41;
            this.btPlotScale.Text = "Scale...";
            this.btPlotScale.UseVisualStyleBackColor = false;
            this.btPlotScale.Click += new System.EventHandler(this.ClickScale);
            // 
            // lbItemsToPlot
            // 
            this.lbItemsToPlot.FormattingEnabled = true;
            this.lbItemsToPlot.Location = new System.Drawing.Point(367, 25);
            this.lbItemsToPlot.Name = "lbItemsToPlot";
            this.lbItemsToPlot.Size = new System.Drawing.Size(275, 134);
            this.lbItemsToPlot.TabIndex = 39;
            this.lbItemsToPlot.DoubleClick += new System.EventHandler(this.ItemsToPlotDoubleClick);
            // 
            // tbarDataScale
            // 
            this.tbarDataScale.Location = new System.Drawing.Point(664, 30);
            this.tbarDataScale.Name = "tbarDataScale";
            this.tbarDataScale.Size = new System.Drawing.Size(130, 42);
            this.tbarDataScale.TabIndex = 33;
            this.tbarDataScale.ValueChanged += new System.EventHandler(this.DataScaleValueChanged);
            // 
            // lbAllPlotItems
            // 
            this.lbAllPlotItems.FormattingEnabled = true;
            this.lbAllPlotItems.Location = new System.Drawing.Point(80, 25);
            this.lbAllPlotItems.Name = "lbAllPlotItems";
            this.lbAllPlotItems.Size = new System.Drawing.Size(275, 108);
            this.lbAllPlotItems.TabIndex = 38;
            this.lbAllPlotItems.SelectedIndexChanged += new System.EventHandler(this.AllPlotItemsSelIndChanged);
            // 
            // tbPlotFilterStr
            // 
            this.tbPlotFilterStr.Location = new System.Drawing.Point(5, 25);
            this.tbPlotFilterStr.Name = "tbPlotFilterStr";
            this.tbPlotFilterStr.Size = new System.Drawing.Size(70, 20);
            this.tbPlotFilterStr.TabIndex = 34;
            this.tbPlotFilterStr.Visible = false;
            this.tbPlotFilterStr.TextChanged += new System.EventHandler(this.TextChangedPlotFilterStr);
            // 
            // labPlotFilterStr
            // 
            this.labPlotFilterStr.AutoSize = true;
            this.labPlotFilterStr.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labPlotFilterStr.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labPlotFilterStr.Location = new System.Drawing.Point(5, 8);
            this.labPlotFilterStr.Name = "labPlotFilterStr";
            this.labPlotFilterStr.Size = new System.Drawing.Size(29, 13);
            this.labPlotFilterStr.TabIndex = 33;
            this.labPlotFilterStr.Text = "Filter";
            this.labPlotFilterStr.Visible = false;
            // 
            // labPlotDisplayGroups
            // 
            this.labPlotDisplayGroups.AutoSize = true;
            this.labPlotDisplayGroups.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labPlotDisplayGroups.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labPlotDisplayGroups.Location = new System.Drawing.Point(80, 5);
            this.labPlotDisplayGroups.Name = "labPlotDisplayGroups";
            this.labPlotDisplayGroups.Size = new System.Drawing.Size(99, 13);
            this.labPlotDisplayGroups.TabIndex = 33;
            this.labPlotDisplayGroups.Text = "Plot Display Groups";
            // 
            // chDataGraph
            // 
            this.chDataGraph.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chDataGraph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chDataGraph.Legends.Add(legend1);
            this.chDataGraph.Location = new System.Drawing.Point(3, 200);
            this.chDataGraph.Name = "chDataGraph";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chDataGraph.Series.Add(series1);
            this.chDataGraph.Size = new System.Drawing.Size(635, 450);
            this.chDataGraph.TabIndex = 16;
            this.chDataGraph.Text = "chart1";
            this.chDataGraph.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.DataGraphCurPosChanged);
            this.chDataGraph.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGraphKeyDown);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btReset.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btReset.Location = new System.Drawing.Point(400, 0);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 20);
            this.btReset.TabIndex = 17;
            this.btReset.Text = "Defaults";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.ClickResetICs);
            // 
            // pnlInitVals
            // 
            this.pnlInitVals.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlInitVals.Controls.Add(this.btICSets);
            this.pnlInitVals.Controls.Add(this.btReset);
            this.pnlInitVals.Controls.Add(this.tcInit);
            this.pnlInitVals.Controls.Add(this.labInitVals);
            this.pnlInitVals.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlInitVals.Location = new System.Drawing.Point(640, 200);
            this.pnlInitVals.Name = "pnlInitVals";
            this.pnlInitVals.Size = new System.Drawing.Size(480, 453);
            this.pnlInitVals.TabIndex = 12;
            // 
            // btICSets
            // 
            this.btICSets.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btICSets.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btICSets.Location = new System.Drawing.Point(320, 0);
            this.btICSets.Name = "btICSets";
            this.btICSets.Size = new System.Drawing.Size(75, 20);
            this.btICSets.TabIndex = 18;
            this.btICSets.Text = "IC Options...";
            this.btICSets.UseVisualStyleBackColor = false;
            this.btICSets.Click += new System.EventHandler(this.ClickICSetOptions);
            // 
            // tcInit
            // 
            this.tcInit.Controls.Add(this.tabPage1);
            this.tcInit.Location = new System.Drawing.Point(0, 20);
            this.tcInit.Name = "tcInit";
            this.tcInit.SelectedIndex = 0;
            this.tcInit.Size = new System.Drawing.Size(477, 435);
            this.tcInit.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dg1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(469, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dg1
            // 
            this.dg1.AllowUserToAddRows = false;
            this.dg1.AllowUserToDeleteRows = false;
            this.dg1.AllowUserToResizeRows = false;
            this.dg1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemLabel,
            this.ItemValue,
            this.ItemUnits});
            this.dg1.Location = new System.Drawing.Point(3, 0);
            this.dg1.Name = "dg1";
            this.dg1.Size = new System.Drawing.Size(465, 405);
            this.dg1.TabIndex = 14;
            this.dg1.Visible = false;
            // 
            // ItemLabel
            // 
            this.ItemLabel.Frozen = true;
            this.ItemLabel.HeaderText = "Parameter";
            this.ItemLabel.Name = "ItemLabel";
            this.ItemLabel.ReadOnly = true;
            this.ItemLabel.Width = 220;
            // 
            // ItemValue
            // 
            this.ItemValue.Frozen = true;
            this.ItemValue.HeaderText = "Value";
            this.ItemValue.Name = "ItemValue";
            // 
            // ItemUnits
            // 
            this.ItemUnits.Frozen = true;
            this.ItemUnits.HeaderText = "Units";
            this.ItemUnits.Name = "ItemUnits";
            this.ItemUnits.ReadOnly = true;
            // 
            // labInitVals
            // 
            this.labInitVals.AutoSize = true;
            this.labInitVals.Location = new System.Drawing.Point(2, 4);
            this.labInitVals.Name = "labInitVals";
            this.labInitVals.Size = new System.Drawing.Size(66, 13);
            this.labInitVals.TabIndex = 0;
            this.labInitVals.Text = "Initial Values";
            // 
            // dgResults
            // 
            this.dgResults.AllowUserToAddRows = false;
            this.dgResults.AllowUserToDeleteRows = false;
            this.dgResults.AllowUserToResizeRows = false;
            this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResults.Location = new System.Drawing.Point(640, 658);
            this.dgResults.Name = "dgResults";
            this.dgResults.ReadOnly = true;
            this.dgResults.Size = new System.Drawing.Size(478, 450);
            this.dgResults.TabIndex = 25;
            this.dgResults.Visible = false;
            this.dgResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Results_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.HelpMenuStripItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1116, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageSetupToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // pageSetupToolStripMenuItem
            // 
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.pageSetupToolStripMenuItem.Text = "Page Setup";
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.ClickPageSetup);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.ClickPrintPreview);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.CLickPrint);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedToolStripMenuItem,
            this.plotGroupsToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.advancedToolStripMenuItem.Text = "All Data";
            this.advancedToolStripMenuItem.Click += new System.EventHandler(this.ClickMenuAllData);
            // 
            // plotGroupsToolStripMenuItem
            // 
            this.plotGroupsToolStripMenuItem.Name = "plotGroupsToolStripMenuItem";
            this.plotGroupsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.plotGroupsToolStripMenuItem.Text = "Plot Groups";
            this.plotGroupsToolStripMenuItem.Click += new System.EventHandler(this.ClickMenuPlotGroups);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulationControlToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.toolStripSeparator1,
            this.dataPlotsToolStripMenuItem,
            this.stabilityPlotsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // simulationControlToolStripMenuItem
            // 
            this.simulationControlToolStripMenuItem.Name = "simulationControlToolStripMenuItem";
            this.simulationControlToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.simulationControlToolStripMenuItem.Text = "Initial Conditions";
            this.simulationControlToolStripMenuItem.Click += new System.EventHandler(this.ClickSimControl);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.databaseToolStripMenuItem.Text = "Database";
            this.databaseToolStripMenuItem.Click += new System.EventHandler(this.ClickDB);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // dataPlotsToolStripMenuItem
            // 
            this.dataPlotsToolStripMenuItem.Name = "dataPlotsToolStripMenuItem";
            this.dataPlotsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dataPlotsToolStripMenuItem.Text = "Data Plots";
            this.dataPlotsToolStripMenuItem.Click += new System.EventHandler(this.ClickDataPlots);
            // 
            // stabilityPlotsToolStripMenuItem
            // 
            this.stabilityPlotsToolStripMenuItem.Name = "stabilityPlotsToolStripMenuItem";
            this.stabilityPlotsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.stabilityPlotsToolStripMenuItem.Text = "Stability Plots";
            this.stabilityPlotsToolStripMenuItem.Click += new System.EventHandler(this.ClickStabilityPlots);
            // 
            // HelpMenuStripItem
            // 
            this.HelpMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpDocumentToolStripMenuItem,
            this.AboutMenuStripItem});
            this.HelpMenuStripItem.Name = "HelpMenuStripItem";
            this.HelpMenuStripItem.Size = new System.Drawing.Size(40, 20);
            this.HelpMenuStripItem.Text = "Help";
            // 
            // helpDocumentToolStripMenuItem
            // 
            this.helpDocumentToolStripMenuItem.Name = "helpDocumentToolStripMenuItem";
            this.helpDocumentToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.helpDocumentToolStripMenuItem.Text = "Help Document";
            this.helpDocumentToolStripMenuItem.Click += new System.EventHandler(this.ClickHelpDocument);
            // 
            // AboutMenuStripItem
            // 
            this.AboutMenuStripItem.Name = "AboutMenuStripItem";
            this.AboutMenuStripItem.Size = new System.Drawing.Size(146, 22);
            this.AboutMenuStripItem.Text = "About...";
            this.AboutMenuStripItem.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // panStabPlots
            // 
            this.panStabPlots.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panStabPlots.Controls.Add(this.chStab);
            this.panStabPlots.Controls.Add(this.chNyq);
            this.panStabPlots.Controls.Add(this.chBodePhase);
            this.panStabPlots.Controls.Add(this.chBodeAmp);
            this.panStabPlots.Location = new System.Drawing.Point(3, 658);
            this.panStabPlots.Name = "panStabPlots";
            this.panStabPlots.Size = new System.Drawing.Size(635, 450);
            this.panStabPlots.TabIndex = 26;
            // 
            // chStab
            // 
            chartArea2.Name = "ChartArea1";
            this.chStab.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chStab.Legends.Add(legend2);
            this.chStab.Location = new System.Drawing.Point(5, 227);
            this.chStab.Name = "chStab";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chStab.Series.Add(series2);
            this.chStab.Size = new System.Drawing.Size(310, 218);
            this.chStab.TabIndex = 3;
            this.chStab.Text = "chart4";
            this.chStab.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.ToolTipStab);
            this.chStab.Click += new System.EventHandler(this.ClickStab);
            // 
            // chNyq
            // 
            chartArea3.Name = "ChartArea1";
            this.chNyq.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chNyq.Legends.Add(legend3);
            this.chNyq.Location = new System.Drawing.Point(320, 227);
            this.chNyq.Name = "chNyq";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chNyq.Series.Add(series3);
            this.chNyq.Size = new System.Drawing.Size(310, 218);
            this.chNyq.TabIndex = 2;
            this.chNyq.Text = "chart3";
            this.chNyq.Visible = false;
            this.chNyq.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.ToolTipNyq);
            this.chNyq.Click += new System.EventHandler(this.ClickNyq);
            // 
            // chBodePhase
            // 
            chartArea4.Name = "ChartArea1";
            this.chBodePhase.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chBodePhase.Legends.Add(legend4);
            this.chBodePhase.Location = new System.Drawing.Point(320, 5);
            this.chBodePhase.Name = "chBodePhase";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chBodePhase.Series.Add(series4);
            this.chBodePhase.Size = new System.Drawing.Size(310, 218);
            this.chBodePhase.TabIndex = 1;
            this.chBodePhase.Text = "chart2";
            this.chBodePhase.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.ToolTipBodePhase);
            this.chBodePhase.Click += new System.EventHandler(this.ClickBodePhase);
            // 
            // chBodeAmp
            // 
            chartArea5.Name = "ChartArea1";
            this.chBodeAmp.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chBodeAmp.Legends.Add(legend5);
            this.chBodeAmp.Location = new System.Drawing.Point(5, 5);
            this.chBodeAmp.Name = "chBodeAmp";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chBodeAmp.Series.Add(series5);
            this.chBodeAmp.Size = new System.Drawing.Size(310, 218);
            this.chBodeAmp.TabIndex = 0;
            this.chBodeAmp.Text = "chart1";
            this.chBodeAmp.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.ToolTipBodeAmp);
            this.chBodeAmp.Click += new System.EventHandler(this.ClickBodeAmp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 655);
            this.Controls.Add(this.panStabPlots);
            this.Controls.Add(this.dgResults);
            this.Controls.Add(this.chDataGraph);
            this.Controls.Add(this.pnlSimParms);
            this.Controls.Add(this.pnlPlotParms);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlInitVals);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Simulation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.MainFormResize);
            this.pnlSimParms.ResumeLayout(false);
            this.pnlSimParms.PerformLayout();
            this.pnlPlotParms.ResumeLayout(false);
            this.pnlPlotParms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarDataScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chDataGraph)).EndInit();
            this.pnlInitVals.ResumeLayout(false);
            this.pnlInitVals.PerformLayout();
            this.tcInit.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panStabPlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chStab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chNyq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chBodePhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chBodeAmp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labSimParms;
        private System.Windows.Forms.Label labMaxTime;
        private System.Windows.Forms.TextBox tbMaxTime;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label labVarsToPlot;
        private System.Windows.Forms.Panel pnlSimParms;
        private System.Windows.Forms.Panel pnlPlotParms;
        private System.Windows.Forms.DataVisualization.Charting.Chart chDataGraph;
        private System.Windows.Forms.Button btAbort;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.ComboBox cbReplayList;
        private System.Windows.Forms.Label labReplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlInitVals;
        private System.Windows.Forms.Label labInitVals;
        private System.Windows.Forms.DataGridView dgResults;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuStripItem;
        private System.Windows.Forms.Label labDone;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulationControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbarDataScale;
        private System.Windows.Forms.Label labLess;
        private System.Windows.Forms.Label labMore;
        private System.Windows.Forms.ListBox lbItemsToPlot;
        private System.Windows.Forms.ListBox lbAllPlotItems;
        private System.Windows.Forms.TextBox tbPlotFilterStr;
        private System.Windows.Forms.Label labPlotFilterStr;
        private System.Windows.Forms.Label labPlotDisplayGroups;
        private System.Windows.Forms.Button btPlotScale;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotGroupsToolStripMenuItem;
        private System.Windows.Forms.Button btData;
        private System.Windows.Forms.Button btPlot;
        private System.Windows.Forms.Button btSaveSim;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.RadioButton rbReplay;
        private System.Windows.Forms.RadioButton rbLive;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.Button btClearToPlot;
        private System.Windows.Forms.ToolStripMenuItem helpDocumentToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemUnits;
        private System.Windows.Forms.TabControl tcInit;
        private System.Windows.Forms.Button btCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem dataPlotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stabilityPlotsToolStripMenuItem;
        private System.Windows.Forms.Panel panStabPlots;
        private System.Windows.Forms.DataVisualization.Charting.Chart chNyq;
        private System.Windows.Forms.DataVisualization.Charting.Chart chBodePhase;
        private System.Windows.Forms.DataVisualization.Charting.Chart chBodeAmp;
        private System.Windows.Forms.CheckBox chkAutoScale;
        private System.Windows.Forms.CheckBox chkDelay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chStab;
        private System.Windows.Forms.Button btICSets;
        private System.Windows.Forms.Label labRunning;
        private System.Windows.Forms.Timer timer2;
    }
}

