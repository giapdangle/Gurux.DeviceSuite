namespace Gurux.DeviceSuite.Ami
{
    partial class GXAmiCommandPromptForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.MediaCB = new System.Windows.Forms.ComboBox();
            this.MediaLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DefaultBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ConnectingPanel = new System.Windows.Forms.Panel();
            this.ConnectCancelBtn = new System.Windows.Forms.Button();
            this.MediaFrame = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ConnectingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MediaCB);
            this.panel2.Controls.Add(this.MediaLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(318, 32);
            this.panel2.TabIndex = 0;
            // 
            // MediaCB
            // 
            this.MediaCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MediaCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaCB.FormattingEnabled = true;
            this.MediaCB.Location = new System.Drawing.Point(65, 6);
            this.MediaCB.Name = "MediaCB";
            this.MediaCB.Size = new System.Drawing.Size(241, 21);
            this.MediaCB.TabIndex = 0;
            this.MediaCB.SelectedIndexChanged += new System.EventHandler(this.MediaCB_SelectedIndexChanged);
            // 
            // MediaLbl
            // 
            this.MediaLbl.AutoSize = true;
            this.MediaLbl.Location = new System.Drawing.Point(8, 9);
            this.MediaLbl.Name = "MediaLbl";
            this.MediaLbl.Size = new System.Drawing.Size(39, 13);
            this.MediaLbl.TabIndex = 132;
            this.MediaLbl.Text = "Media:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DefaultBtn);
            this.panel1.Controls.Add(this.OkBtn);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 43);
            this.panel1.TabIndex = 2;
            // 
            // DefaultBtn
            // 
            this.DefaultBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DefaultBtn.Location = new System.Drawing.Point(12, 8);
            this.DefaultBtn.Name = "DefaultBtn";
            this.DefaultBtn.Size = new System.Drawing.Size(75, 23);
            this.DefaultBtn.TabIndex = 2;
            this.DefaultBtn.Text = "Default";
            this.DefaultBtn.Click += new System.EventHandler(this.DefaultBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkBtn.Location = new System.Drawing.Point(150, 8);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 3;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(231, 8);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // ConnectingPanel
            // 
            this.ConnectingPanel.Controls.Add(this.ConnectCancelBtn);
            this.ConnectingPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConnectingPanel.Location = new System.Drawing.Point(0, 217);
            this.ConnectingPanel.Name = "ConnectingPanel";
            this.ConnectingPanel.Size = new System.Drawing.Size(318, 32);
            this.ConnectingPanel.TabIndex = 3;
            this.ConnectingPanel.Visible = false;
            // 
            // ConnectCancelBtn
            // 
            this.ConnectCancelBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectCancelBtn.Location = new System.Drawing.Point(0, 0);
            this.ConnectCancelBtn.Name = "ConnectCancelBtn";
            this.ConnectCancelBtn.Size = new System.Drawing.Size(318, 32);
            this.ConnectCancelBtn.TabIndex = 3;
            this.ConnectCancelBtn.Text = "Cancel";
            this.ConnectCancelBtn.Click += new System.EventHandler(this.ConnectCancelBtn_Click);
            // 
            // MediaFrame
            // 
            this.MediaFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaFrame.Location = new System.Drawing.Point(0, 32);
            this.MediaFrame.Name = "MediaFrame";
            this.MediaFrame.Size = new System.Drawing.Size(318, 185);
            this.MediaFrame.TabIndex = 6;
            // 
            // GXAmiCommandPromptForm
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(318, 292);
            this.ControlBox = false;
            this.Controls.Add(this.MediaFrame);
            this.Controls.Add(this.ConnectingPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "GXAmiCommandPromptForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Command Prompt Settings";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ConnectingPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox MediaCB;
        private System.Windows.Forms.Label MediaLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button DefaultBtn;
        private System.Windows.Forms.Panel ConnectingPanel;
        private System.Windows.Forms.Button ConnectCancelBtn;
        private System.Windows.Forms.Panel MediaFrame;
    }
}