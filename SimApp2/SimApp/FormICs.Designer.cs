namespace SimApp
{
    partial class frmICOptions
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
            this.btSaveICs = new System.Windows.Forms.Button();
            this.tbNameIcSet = new System.Windows.Forms.TextBox();
            this.labIcSave = new System.Windows.Forms.Label();
            this.labICLoad = new System.Windows.Forms.Label();
            this.cbIcFiles = new System.Windows.Forms.ComboBox();
            this.btOK_IC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSaveICs
            // 
            this.btSaveICs.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btSaveICs.Enabled = false;
            this.btSaveICs.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btSaveICs.Location = new System.Drawing.Point(270, 23);
            this.btSaveICs.Name = "btSaveICs";
            this.btSaveICs.Size = new System.Drawing.Size(75, 22);
            this.btSaveICs.TabIndex = 0;
            this.btSaveICs.Text = "Save ICs";
            this.btSaveICs.UseVisualStyleBackColor = false;
            this.btSaveICs.Click += new System.EventHandler(this.SaveICsClick);
            // 
            // tbNameIcSet
            // 
            this.tbNameIcSet.Location = new System.Drawing.Point(6, 25);
            this.tbNameIcSet.Name = "tbNameIcSet";
            this.tbNameIcSet.Size = new System.Drawing.Size(250, 20);
            this.tbNameIcSet.TabIndex = 1;
            this.tbNameIcSet.TextChanged += new System.EventHandler(this.ICSetNameTextChanged);
            // 
            // labIcSave
            // 
            this.labIcSave.AutoSize = true;
            this.labIcSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labIcSave.Location = new System.Drawing.Point(5, 10);
            this.labIcSave.Name = "labIcSave";
            this.labIcSave.Size = new System.Drawing.Size(138, 13);
            this.labIcSave.TabIndex = 2;
            this.labIcSave.Text = "Name of new IC set to save";
            // 
            // labICLoad
            // 
            this.labICLoad.AutoSize = true;
            this.labICLoad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labICLoad.Location = new System.Drawing.Point(5, 80);
            this.labICLoad.Name = "labICLoad";
            this.labICLoad.Size = new System.Drawing.Size(104, 13);
            this.labICLoad.TabIndex = 3;
            this.labICLoad.Text = "Select IC Set to load";
            // 
            // cbIcFiles
            // 
            this.cbIcFiles.FormattingEnabled = true;
            this.cbIcFiles.Location = new System.Drawing.Point(5, 95);
            this.cbIcFiles.Name = "cbIcFiles";
            this.cbIcFiles.Size = new System.Drawing.Size(265, 21);
            this.cbIcFiles.TabIndex = 4;
            this.cbIcFiles.SelectionChangeCommitted += new System.EventHandler(this.IcSelChangeComm);
            // 
            // btOK_IC
            // 
            this.btOK_IC.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btOK_IC.Enabled = false;
            this.btOK_IC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btOK_IC.Location = new System.Drawing.Point(270, 95);
            this.btOK_IC.Name = "btOK_IC";
            this.btOK_IC.Size = new System.Drawing.Size(75, 22);
            this.btOK_IC.TabIndex = 5;
            this.btOK_IC.Text = "Set ICs";
            this.btOK_IC.UseVisualStyleBackColor = false;
            this.btOK_IC.Click += new System.EventHandler(this.SetICClick);
            // 
            // frmICOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(352, 243);
            this.Controls.Add(this.btOK_IC);
            this.Controls.Add(this.cbIcFiles);
            this.Controls.Add(this.labICLoad);
            this.Controls.Add(this.labIcSave);
            this.Controls.Add(this.tbNameIcSet);
            this.Controls.Add(this.btSaveICs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmICOptions";
            this.Text = "IC Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSaveICs;
        private System.Windows.Forms.TextBox tbNameIcSet;
        private System.Windows.Forms.Label labIcSave;
        private System.Windows.Forms.Label labICLoad;
        private System.Windows.Forms.ComboBox cbIcFiles;
        private System.Windows.Forms.Button btOK_IC;
    }
}