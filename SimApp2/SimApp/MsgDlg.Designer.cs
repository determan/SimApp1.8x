namespace SimApp
{
    partial class frmMsgDlg
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
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btMsgOK = new System.Windows.Forms.Button();
            this.btMsgCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMsg
            // 
            this.tbMsg.Location = new System.Drawing.Point(12, 4);
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.ReadOnly = true;
            this.tbMsg.Size = new System.Drawing.Size(534, 20);
            this.tbMsg.TabIndex = 0;
            // 
            // btMsgOK
            // 
            this.btMsgOK.Location = new System.Drawing.Point(168, 56);
            this.btMsgOK.Name = "btMsgOK";
            this.btMsgOK.Size = new System.Drawing.Size(75, 23);
            this.btMsgOK.TabIndex = 1;
            this.btMsgOK.Text = "OK";
            this.btMsgOK.UseVisualStyleBackColor = true;
            this.btMsgOK.Click += new System.EventHandler(this.ClickMsgOK);
            // 
            // btMsgCancel
            // 
            this.btMsgCancel.Location = new System.Drawing.Point(258, 56);
            this.btMsgCancel.Name = "btMsgCancel";
            this.btMsgCancel.Size = new System.Drawing.Size(75, 23);
            this.btMsgCancel.TabIndex = 2;
            this.btMsgCancel.Text = "Cancel";
            this.btMsgCancel.UseVisualStyleBackColor = true;
            this.btMsgCancel.Click += new System.EventHandler(this.ClickMsgCancel);
            // 
            // frmMsgDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 81);
            this.Controls.Add(this.btMsgCancel);
            this.Controls.Add(this.btMsgOK);
            this.Controls.Add(this.tbMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMsgDlg";
            this.Text = "Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btMsgOK;
        private System.Windows.Forms.Button btMsgCancel;
    }
}