namespace ModelConverter
{
    partial class FormICs
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
            this.cbConstants = new System.Windows.Forms.ComboBox();
            this.cbCTypes = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.rtbContext = new System.Windows.Forms.RichTextBox();
            this.labConstant = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.labContext = new System.Windows.Forms.Label();
            this.btAddType = new System.Windows.Forms.Button();
            this.labUnits = new System.Windows.Forms.Label();
            this.tbUnits = new System.Windows.Forms.TextBox();
            this.chbStability = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbConstants
            // 
            this.cbConstants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConstants.FormattingEnabled = true;
            this.cbConstants.Location = new System.Drawing.Point(10, 20);
            this.cbConstants.Name = "cbConstants";
            this.cbConstants.Size = new System.Drawing.Size(121, 21);
            this.cbConstants.TabIndex = 0;
            this.cbConstants.SelectedIndexChanged += new System.EventHandler(this.ConstantSelIndChanged);
            // 
            // cbCTypes
            // 
            this.cbCTypes.Enabled = false;
            this.cbCTypes.FormattingEnabled = true;
            this.cbCTypes.Location = new System.Drawing.Point(150, 20);
            this.cbCTypes.Name = "cbCTypes";
            this.cbCTypes.Size = new System.Drawing.Size(220, 21);
            this.cbCTypes.TabIndex = 1;
            this.cbCTypes.SelectedIndexChanged += new System.EventHandler(this.SelindChangedType);
            this.cbCTypes.TextChanged += new System.EventHandler(this.TextChangedType);
            // 
            // tbName
            // 
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(390, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(250, 20);
            this.tbName.TabIndex = 2;
            this.tbName.TextChanged += new System.EventHandler(this.TextChangedName);
            // 
            // rtbContext
            // 
            this.rtbContext.Location = new System.Drawing.Point(10, 100);
            this.rtbContext.Name = "rtbContext";
            this.rtbContext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbContext.Size = new System.Drawing.Size(700, 150);
            this.rtbContext.TabIndex = 3;
            this.rtbContext.Text = "";
            // 
            // labConstant
            // 
            this.labConstant.AutoSize = true;
            this.labConstant.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labConstant.Location = new System.Drawing.Point(10, 4);
            this.labConstant.Name = "labConstant";
            this.labConstant.Size = new System.Drawing.Size(27, 13);
            this.labConstant.TabIndex = 4;
            this.labConstant.Text = "Item";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labType.Location = new System.Drawing.Point(150, 4);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(31, 13);
            this.labType.TabIndex = 5;
            this.labType.Text = "Type";
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labName.Location = new System.Drawing.Point(390, 4);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(35, 13);
            this.labName.TabIndex = 6;
            this.labName.Text = "Name";
            // 
            // labContext
            // 
            this.labContext.AutoSize = true;
            this.labContext.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labContext.Location = new System.Drawing.Point(10, 84);
            this.labContext.Name = "labContext";
            this.labContext.Size = new System.Drawing.Size(43, 13);
            this.labContext.TabIndex = 7;
            this.labContext.Text = "Context";
            // 
            // btAddType
            // 
            this.btAddType.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btAddType.Enabled = false;
            this.btAddType.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btAddType.Location = new System.Drawing.Point(162, 60);
            this.btAddType.Name = "btAddType";
            this.btAddType.Size = new System.Drawing.Size(100, 23);
            this.btAddType.TabIndex = 8;
            this.btAddType.Text = "Add Custom Type";
            this.btAddType.UseVisualStyleBackColor = false;
            this.btAddType.Click += new System.EventHandler(this.ClickAddType);
            // 
            // labUnits
            // 
            this.labUnits.AutoSize = true;
            this.labUnits.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labUnits.Location = new System.Drawing.Point(660, 4);
            this.labUnits.Name = "labUnits";
            this.labUnits.Size = new System.Drawing.Size(31, 13);
            this.labUnits.TabIndex = 11;
            this.labUnits.Text = "Units";
            // 
            // tbUnits
            // 
            this.tbUnits.Enabled = false;
            this.tbUnits.Location = new System.Drawing.Point(660, 20);
            this.tbUnits.Name = "tbUnits";
            this.tbUnits.Size = new System.Drawing.Size(50, 20);
            this.tbUnits.TabIndex = 12;
            this.tbUnits.TextChanged += new System.EventHandler(this.UnitsTextChanged);
            // 
            // chbStability
            // 
            this.chbStability.AutoSize = true;
            this.chbStability.Enabled = false;
            this.chbStability.Location = new System.Drawing.Point(390, 65);
            this.chbStability.Name = "chbStability";
            this.chbStability.Size = new System.Drawing.Size(111, 17);
            this.chbStability.TabIndex = 13;
            this.chbStability.Text = "Stability Data Item";
            this.chbStability.UseVisualStyleBackColor = true;
            this.chbStability.CheckedChanged += new System.EventHandler(this.StabCheckChanged);
            // 
            // FormICs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(734, 273);
            this.Controls.Add(this.chbStability);
            this.Controls.Add(this.tbUnits);
            this.Controls.Add(this.labUnits);
            this.Controls.Add(this.btAddType);
            this.Controls.Add(this.labContext);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.labConstant);
            this.Controls.Add(this.rtbContext);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.cbCTypes);
            this.Controls.Add(this.cbConstants);
            this.Name = "FormICs";
            this.Text = "FormICs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbConstants;
        private System.Windows.Forms.ComboBox cbCTypes;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.RichTextBox rtbContext;
        private System.Windows.Forms.Label labConstant;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labContext;
        private System.Windows.Forms.Button btAddType;
        private System.Windows.Forms.Label labUnits;
        private System.Windows.Forms.TextBox tbUnits;
        private System.Windows.Forms.CheckBox chbStability;
    }
}