namespace SimApp
{
    partial class ScaleVals
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgScaleValues = new System.Windows.Forms.DataGridView();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btClearAll = new System.Windows.Forms.Button();
            this.panType = new System.Windows.Forms.Panel();
            this.labPanType = new System.Windows.Forms.Label();
            this.rbBodeAmp = new System.Windows.Forms.RadioButton();
            this.rbBodePhase = new System.Windows.Forms.RadioButton();
            this.rbStabMargin = new System.Windows.Forms.RadioButton();
            this.ModelVars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlotLabels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labLLX = new System.Windows.Forms.Label();
            this.rbLinX = new System.Windows.Forms.RadioButton();
            this.rbLogX = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgScaleValues)).BeginInit();
            this.panType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgScaleValues
            // 
            this.dgScaleValues.AllowUserToAddRows = false;
            this.dgScaleValues.AllowUserToDeleteRows = false;
            this.dgScaleValues.AllowUserToResizeRows = false;
            this.dgScaleValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgScaleValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgScaleValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgScaleValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModelVars,
            this.PlotLabels});
            this.dgScaleValues.Location = new System.Drawing.Point(0, 2);
            this.dgScaleValues.Name = "dgScaleValues";
            this.dgScaleValues.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgScaleValues.Size = new System.Drawing.Size(369, 320);
            this.dgScaleValues.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btOK.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btOK.Location = new System.Drawing.Point(300, 330);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = false;
            this.btOK.Click += new System.EventHandler(this.OKClick);
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btCancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btCancel.Location = new System.Drawing.Point(385, 330);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = false;
            this.btCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // btClearAll
            // 
            this.btClearAll.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btClearAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btClearAll.Location = new System.Drawing.Point(4, 330);
            this.btClearAll.Name = "btClearAll";
            this.btClearAll.Size = new System.Drawing.Size(75, 23);
            this.btClearAll.TabIndex = 3;
            this.btClearAll.Text = "Clear All";
            this.btClearAll.UseVisualStyleBackColor = false;
            this.btClearAll.Visible = false;
            // 
            // panType
            // 
            this.panType.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panType.Controls.Add(this.rbStabMargin);
            this.panType.Controls.Add(this.rbBodePhase);
            this.panType.Controls.Add(this.rbBodeAmp);
            this.panType.Controls.Add(this.labPanType);
            this.panType.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panType.Location = new System.Drawing.Point(375, 2);
            this.panType.Name = "panType";
            this.panType.Size = new System.Drawing.Size(90, 105);
            this.panType.TabIndex = 5;
            // 
            // labPanType
            // 
            this.labPanType.AutoSize = true;
            this.labPanType.Location = new System.Drawing.Point(4, 4);
            this.labPanType.Name = "labPanType";
            this.labPanType.Size = new System.Drawing.Size(31, 13);
            this.labPanType.TabIndex = 0;
            this.labPanType.Text = "Type";
            // 
            // rbBodeAmp
            // 
            this.rbBodeAmp.AutoSize = true;
            this.rbBodeAmp.Location = new System.Drawing.Point(5, 25);
            this.rbBodeAmp.Name = "rbBodeAmp";
            this.rbBodeAmp.Size = new System.Drawing.Size(74, 17);
            this.rbBodeAmp.TabIndex = 5;
            this.rbBodeAmp.TabStop = true;
            this.rbBodeAmp.Text = "Bode Amp";
            this.rbBodeAmp.UseVisualStyleBackColor = true;
            this.rbBodeAmp.CheckedChanged += new System.EventHandler(this.CheckChangeBodeAmp);
            // 
            // rbBodePhase
            // 
            this.rbBodePhase.AutoSize = true;
            this.rbBodePhase.Location = new System.Drawing.Point(5, 45);
            this.rbBodePhase.Name = "rbBodePhase";
            this.rbBodePhase.Size = new System.Drawing.Size(83, 17);
            this.rbBodePhase.TabIndex = 6;
            this.rbBodePhase.TabStop = true;
            this.rbBodePhase.Text = "Bode Phase";
            this.rbBodePhase.UseVisualStyleBackColor = true;
            this.rbBodePhase.CheckedChanged += new System.EventHandler(this.CheckChangeBodePhase);
            // 
            // rbStabMargin
            // 
            this.rbStabMargin.AutoSize = true;
            this.rbStabMargin.Location = new System.Drawing.Point(5, 65);
            this.rbStabMargin.Name = "rbStabMargin";
            this.rbStabMargin.Size = new System.Drawing.Size(82, 17);
            this.rbStabMargin.TabIndex = 7;
            this.rbStabMargin.TabStop = true;
            this.rbStabMargin.Text = "Stab Margin";
            this.rbStabMargin.UseVisualStyleBackColor = true;
            this.rbStabMargin.CheckedChanged += new System.EventHandler(this.CheckChangeStabMarg);
            // 
            // ModelVars
            // 
            this.ModelVars.HeaderText = "Item";
            this.ModelVars.Name = "ModelVars";
            this.ModelVars.ReadOnly = true;
            this.ModelVars.Width = 150;
            // 
            // PlotLabels
            // 
            this.PlotLabels.HeaderText = "Value";
            this.PlotLabels.Name = "PlotLabels";
            this.PlotLabels.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbLogX);
            this.panel1.Controls.Add(this.rbLinX);
            this.panel1.Controls.Add(this.labLLX);
            this.panel1.Location = new System.Drawing.Point(375, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 65);
            this.panel1.TabIndex = 6;
            // 
            // labLLX
            // 
            this.labLLX.AutoSize = true;
            this.labLLX.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labLLX.Location = new System.Drawing.Point(4, 4);
            this.labLLX.Name = "labLLX";
            this.labLLX.Size = new System.Drawing.Size(60, 13);
            this.labLLX.TabIndex = 0;
            this.labLLX.Text = "Log / Lin X";
            // 
            // rbLinX
            // 
            this.rbLinX.AutoSize = true;
            this.rbLinX.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbLinX.Location = new System.Drawing.Point(5, 25);
            this.rbLinX.Name = "rbLinX";
            this.rbLinX.Size = new System.Drawing.Size(54, 17);
            this.rbLinX.TabIndex = 1;
            this.rbLinX.TabStop = true;
            this.rbLinX.Text = "Linear";
            this.rbLinX.UseVisualStyleBackColor = true;
            this.rbLinX.CheckedChanged += new System.EventHandler(this.CheckChangeLinX);
            // 
            // rbLogX
            // 
            this.rbLogX.AutoSize = true;
            this.rbLogX.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbLogX.Location = new System.Drawing.Point(5, 45);
            this.rbLogX.Name = "rbLogX";
            this.rbLogX.Size = new System.Drawing.Size(43, 17);
            this.rbLogX.TabIndex = 2;
            this.rbLogX.TabStop = true;
            this.rbLogX.Text = "Log";
            this.rbLogX.UseVisualStyleBackColor = true;
            this.rbLogX.CheckedChanged += new System.EventHandler(this.CheckChangeLogX);
            // 
            // ScaleVals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(468, 358);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panType);
            this.Controls.Add(this.btClearAll);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.dgScaleValues);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScaleVals";
            this.Text = "Set Plot Scale Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.dgScaleValues)).EndInit();
            this.panType.ResumeLayout(false);
            this.panType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgScaleValues;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btClearAll;
        private System.Windows.Forms.Panel panType;
        private System.Windows.Forms.RadioButton rbStabMargin;
        private System.Windows.Forms.RadioButton rbBodePhase;
        private System.Windows.Forms.RadioButton rbBodeAmp;
        private System.Windows.Forms.Label labPanType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelVars;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlotLabels;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbLogX;
        private System.Windows.Forms.RadioButton rbLinX;
        private System.Windows.Forms.Label labLLX;
    }
}