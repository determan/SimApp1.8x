namespace ModelConverter
{
    partial class frmModConv
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
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.tbOutName = new System.Windows.Forms.TextBox();
            this.labDESIRE = new System.Windows.Forms.Label();
            this.labConverted = new System.Windows.Forms.Label();
            this.labModName = new System.Windows.Forms.Label();
            this.tbModName = new System.Windows.Forms.TextBox();
            this.btConvert = new System.Windows.Forms.Button();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btICs = new System.Windows.Forms.Button();
            this.btPlotGroups = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(10, 25);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(275, 20);
            this.tbFilePath.TabIndex = 1;
            // 
            // tbOutName
            // 
            this.tbOutName.Location = new System.Drawing.Point(10, 80);
            this.tbOutName.Name = "tbOutName";
            this.tbOutName.ReadOnly = true;
            this.tbOutName.Size = new System.Drawing.Size(480, 20);
            this.tbOutName.TabIndex = 2;
            // 
            // labDESIRE
            // 
            this.labDESIRE.AutoSize = true;
            this.labDESIRE.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labDESIRE.Location = new System.Drawing.Point(10, 5);
            this.labDESIRE.Name = "labDESIRE";
            this.labDESIRE.Size = new System.Drawing.Size(118, 13);
            this.labDESIRE.TabIndex = 3;
            this.labDESIRE.Text = "DESIRE Input File Path";
            // 
            // labConverted
            // 
            this.labConverted.AutoSize = true;
            this.labConverted.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labConverted.Location = new System.Drawing.Point(10, 60);
            this.labConverted.Name = "labConverted";
            this.labConverted.Size = new System.Drawing.Size(140, 13);
            this.labConverted.TabIndex = 4;
            this.labConverted.Text = "Converted Output File Paths";
            // 
            // labModName
            // 
            this.labModName.AutoSize = true;
            this.labModName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labModName.Location = new System.Drawing.Point(290, 5);
            this.labModName.Name = "labModName";
            this.labModName.Size = new System.Drawing.Size(157, 13);
            this.labModName.TabIndex = 5;
            this.labModName.Text = "Model Name (SUPO, AD125...) ";
            // 
            // tbModName
            // 
            this.tbModName.Location = new System.Drawing.Point(290, 25);
            this.tbModName.Name = "tbModName";
            this.tbModName.Size = new System.Drawing.Size(121, 20);
            this.tbModName.TabIndex = 6;
            this.tbModName.TextChanged += new System.EventHandler(this.ModNameTextChanged);
            // 
            // btConvert
            // 
            this.btConvert.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btConvert.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btConvert.Location = new System.Drawing.Point(10, 219);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(55, 20);
            this.btConvert.TabIndex = 7;
            this.btConvert.Text = "Convert";
            this.btConvert.UseVisualStyleBackColor = false;
            this.btConvert.Click += new System.EventHandler(this.ConvertClick);
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(10, 245);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(480, 20);
            this.tbStatus.TabIndex = 8;
            // 
            // btICs
            // 
            this.btICs.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btICs.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btICs.Location = new System.Drawing.Point(10, 110);
            this.btICs.Name = "btICs";
            this.btICs.Size = new System.Drawing.Size(70, 20);
            this.btICs.TabIndex = 9;
            this.btICs.Text = "ICs";
            this.btICs.UseVisualStyleBackColor = false;
            this.btICs.Click += new System.EventHandler(this.ClickICs);
            // 
            // btPlotGroups
            // 
            this.btPlotGroups.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btPlotGroups.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPlotGroups.Location = new System.Drawing.Point(10, 135);
            this.btPlotGroups.Name = "btPlotGroups";
            this.btPlotGroups.Size = new System.Drawing.Size(70, 20);
            this.btPlotGroups.TabIndex = 10;
            this.btPlotGroups.Text = "Plot Groups";
            this.btPlotGroups.UseVisualStyleBackColor = false;
            this.btPlotGroups.Click += new System.EventHandler(this.ClickPlotGroups);
            // 
            // frmModConv
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(511, 273);
            this.Controls.Add(this.btPlotGroups);
            this.Controls.Add(this.btICs);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.btConvert);
            this.Controls.Add(this.tbModName);
            this.Controls.Add(this.labModName);
            this.Controls.Add(this.labConverted);
            this.Controls.Add(this.labDESIRE);
            this.Controls.Add(this.tbOutName);
            this.Controls.Add(this.tbFilePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModConv";
            this.Text = "ModelConverter 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingModConv);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModConvDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModConvDragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.TextBox tbOutName;
        private System.Windows.Forms.Label labDESIRE;
        private System.Windows.Forms.Label labConverted;
        private System.Windows.Forms.Label labModName;
        private System.Windows.Forms.TextBox tbModName;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btICs;
        private System.Windows.Forms.Button btPlotGroups;
    }
}

