namespace Gurux.DeviceSuite.Ami
{
    partial class GXAMICommandPromptTab
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
            this.CommandPromptSettingsPanel = new System.Windows.Forms.Panel();
            this.HexCB = new System.Windows.Forms.CheckBox();
            this.AddNewLineCB = new System.Windows.Forms.CheckBox();
            this.CommandPromptClearBtn = new System.Windows.Forms.Button();
            this.CommandPromptTB = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.CommandPromptSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandPromptSettingsPanel
            // 
            this.CommandPromptSettingsPanel.Controls.Add(this.HexCB);
            this.CommandPromptSettingsPanel.Controls.Add(this.AddNewLineCB);
            this.CommandPromptSettingsPanel.Controls.Add(this.CommandPromptClearBtn);
            this.CommandPromptSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CommandPromptSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this.CommandPromptSettingsPanel.Name = "CommandPromptSettingsPanel";
            this.CommandPromptSettingsPanel.Size = new System.Drawing.Size(456, 37);
            this.CommandPromptSettingsPanel.TabIndex = 21;
            // 
            // HexCB
            // 
            this.HexCB.AutoSize = true;
            this.HexCB.Location = new System.Drawing.Point(12, 11);
            this.HexCB.Name = "HexCB";
            this.HexCB.Size = new System.Drawing.Size(45, 17);
            this.HexCB.TabIndex = 0;
            this.HexCB.Text = "Hex";
            this.HexCB.UseVisualStyleBackColor = true;
            // 
            // AddNewLineCB
            // 
            this.AddNewLineCB.AutoSize = true;
            this.AddNewLineCB.Location = new System.Drawing.Point(77, 11);
            this.AddNewLineCB.Name = "AddNewLineCB";
            this.AddNewLineCB.Size = new System.Drawing.Size(118, 17);
            this.AddNewLineCB.TabIndex = 2;
            this.AddNewLineCB.Text = "Add NewLine (\\r\\n)";
            this.AddNewLineCB.UseVisualStyleBackColor = true;
            // 
            // CommandPromptClearBtn
            // 
            this.CommandPromptClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CommandPromptClearBtn.Location = new System.Drawing.Point(369, 7);
            this.CommandPromptClearBtn.Name = "CommandPromptClearBtn";
            this.CommandPromptClearBtn.Size = new System.Drawing.Size(75, 23);
            this.CommandPromptClearBtn.TabIndex = 3;
            this.CommandPromptClearBtn.Text = "Clear";
            this.CommandPromptClearBtn.UseVisualStyleBackColor = true;
            this.CommandPromptClearBtn.Click += new System.EventHandler(this.CommandPromptClearBtn_Click);
            // 
            // CommandPromptTB
            // 
            this.CommandPromptTB.BackColor = System.Drawing.Color.Black;
            this.CommandPromptTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandPromptTB.ForeColor = System.Drawing.Color.White;
            this.CommandPromptTB.Location = new System.Drawing.Point(0, 37);
            this.CommandPromptTB.Multiline = true;
            this.CommandPromptTB.Name = "CommandPromptTB";
            this.CommandPromptTB.Size = new System.Drawing.Size(456, 184);
            this.CommandPromptTB.TabIndex = 22;
            this.CommandPromptTB.MouseCaptureChanged += new System.EventHandler(this.CommandPromptTB_MouseCaptureChanged);
            this.CommandPromptTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandPromptTB_KeyDown);
            this.CommandPromptTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommandPromptTB_KeyUp);
            this.CommandPromptTB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommandPromptTB_MouseDown);
            this.CommandPromptTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandPromptTB_KeyPress);
            this.CommandPromptTB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CommandPromptTB_MouseUp);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(168, 227);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 24;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Visible = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // GXAMICommandPromptTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 262);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.CommandPromptTB);
            this.Controls.Add(this.CommandPromptSettingsPanel);
            this.Name = "GXAMICommandPromptTab";
            this.Text = "GXAMICommandPromptTab";
            this.CommandPromptSettingsPanel.ResumeLayout(false);
            this.CommandPromptSettingsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel CommandPromptSettingsPanel;
        private System.Windows.Forms.CheckBox HexCB;
        private System.Windows.Forms.CheckBox AddNewLineCB;
        private System.Windows.Forms.Button CommandPromptClearBtn;
        private System.Windows.Forms.TextBox CommandPromptTB;
        private System.Windows.Forms.Button CancelBtn;
    }
}