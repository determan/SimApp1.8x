namespace SimApp
{
    partial class frmAboutSUPO
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
            this.labTitle = new System.Windows.Forms.Label();
            this.labNames = new System.Windows.Forms.Label();
            this.labCR = new System.Windows.Forms.Label();
            this.labContact = new System.Windows.Forms.Label();
            this.labVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(10, 40);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(256, 13);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "A Generic System Model for a Fissile Solution System";
            // 
            // labNames
            // 
            this.labNames.AutoSize = true;
            this.labNames.Location = new System.Drawing.Point(10, 60);
            this.labNames.Name = "labNames";
            this.labNames.Size = new System.Drawing.Size(275, 13);
            this.labNames.TabIndex = 1;
            this.labNames.Text = "Kimpland, Robert H., Klein Steven K., Determan, John C.";
            // 
            // labCR
            // 
            this.labCR.AutoSize = true;
            this.labCR.Location = new System.Drawing.Point(10, 80);
            this.labCR.Name = "labCR";
            this.labCR.Size = new System.Drawing.Size(266, 13);
            this.labCR.TabIndex = 2;
            this.labCR.Text = "Copyright - Los Alamos National Laboratory, 2013-2015";
            this.labCR.Click += new System.EventHandler(this.labCR_Click);
            // 
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(10, 100);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(368, 13);
            this.labContact.TabIndex = 3;
            this.labContact.Text = "Contact: determan@lanl.gov, 505-665-1914, if you experience any problems.";
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Location = new System.Drawing.Point(10, 120);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(42, 13);
            this.labVersion.TabIndex = 4;
            this.labVersion.Text = "Version";
            // 
            // frmAboutSUPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 146);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.labContact);
            this.Controls.Add(this.labCR);
            this.Controls.Add(this.labNames);
            this.Controls.Add(this.labTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAboutSUPO";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Label labNames;
        private System.Windows.Forms.Label labCR;
        private System.Windows.Forms.Label labContact;
        private System.Windows.Forms.Label labVersion;
    }
}