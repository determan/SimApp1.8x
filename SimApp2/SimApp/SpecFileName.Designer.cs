namespace SimApp
{
    partial class frmSpecName
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
            this.labProvideName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btNameOK = new System.Windows.Forms.Button();
            this.labMemo = new System.Windows.Forms.Label();
            this.tbMemo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labProvideName
            // 
            this.labProvideName.AutoSize = true;
            this.labProvideName.Location = new System.Drawing.Point(5, 10);
            this.labProvideName.Name = "labProvideName";
            this.labProvideName.Size = new System.Drawing.Size(179, 13);
            this.labProvideName.TabIndex = 0;
            this.labProvideName.Text = "Provide a file name for the simulation";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(5, 30);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(275, 20);
            this.tbName.TabIndex = 1;
            // 
            // btNameOK
            // 
            this.btNameOK.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btNameOK.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btNameOK.Location = new System.Drawing.Point(350, 30);
            this.btNameOK.Name = "btNameOK";
            this.btNameOK.Size = new System.Drawing.Size(40, 20);
            this.btNameOK.TabIndex = 2;
            this.btNameOK.Text = "OK";
            this.btNameOK.UseVisualStyleBackColor = false;
            this.btNameOK.Click += new System.EventHandler(this.NameOKClicked);
            // 
            // labMemo
            // 
            this.labMemo.AutoSize = true;
            this.labMemo.Location = new System.Drawing.Point(5, 60);
            this.labMemo.Name = "labMemo";
            this.labMemo.Size = new System.Drawing.Size(385, 13);
            this.labMemo.TabIndex = 3;
            this.labMemo.Text = "Enter a descriptive memo (notable IC values, etc).  Truncated at 512 characters.";
            this.labMemo.Visible = false;
            // 
            // tbMemo
            // 
            this.tbMemo.Location = new System.Drawing.Point(5, 80);
            this.tbMemo.MaxLength = 512;
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.Size = new System.Drawing.Size(385, 20);
            this.tbMemo.TabIndex = 4;
            this.tbMemo.Visible = false;
            // 
            // frmSpecName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 59);
            this.Controls.Add(this.tbMemo);
            this.Controls.Add(this.labMemo);
            this.Controls.Add(this.btNameOK);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.labProvideName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Specify File Name";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpecFileNameClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labProvideName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btNameOK;
        private System.Windows.Forms.Label labMemo;
        private System.Windows.Forms.TextBox tbMemo;
    }
}