//
// --------------------------------------------------------------------------
//  Gurux Ltd
// 
//
//
// Filename:        $HeadURL$
//
// Version:         $Revision$,
//                  $Date$
//                  $Author$
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
//  DESCRIPTION
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation; version 2 of the License.
// Gurux Device Framework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
//
// This code is licensed under the GNU General Public License v2. 
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

namespace Gurux.DeviceSuite
{
    partial class GXOptionsForm
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
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.MaximimTraceCountTB = new System.Windows.Forms.TextBox();
            this.MaximimTraceCountLbl = new System.Windows.Forms.Label();
            this.MaximimErrorCountTB = new System.Windows.Forms.TextBox();
            this.MaximimErrorCountLbl = new System.Windows.Forms.Label();
            this.DeviceEditorTab = new System.Windows.Forms.TabPage();
            this.DirectorTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowPropertyValueCB = new System.Windows.Forms.CheckBox();
            this.ShowCategoriesCB = new System.Windows.Forms.CheckBox();
            this.ShowTablesCB = new System.Windows.Forms.CheckBox();
            this.ShowPropertiesCB = new System.Windows.Forms.CheckBox();
            this.ShowDevicesCB = new System.Windows.Forms.CheckBox();
            this.AmiTab = new System.Windows.Forms.TabPage();
            this.EnableAMICB = new System.Windows.Forms.CheckBox();
            this.AMISettings = new System.Windows.Forms.GroupBox();
            this.TestBtn = new System.Windows.Forms.Button();
            this.DatabaseSettingsBtn = new System.Windows.Forms.Button();
            this.PortTB = new System.Windows.Forms.TextBox();
            this.PortLbl = new System.Windows.Forms.Label();
            this.HostTB = new System.Windows.Forms.TextBox();
            this.HostLbl = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.DirectorTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.AmiTab.SuspendLayout();
            this.AMISettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.Location = new System.Drawing.Point(209, 262);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 7;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(290, 262);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.GeneralTab);
            this.tabControl1.Controls.Add(this.DeviceEditorTab);
            this.tabControl1.Controls.Add(this.DirectorTab);
            this.tabControl1.Controls.Add(this.AmiTab);
            this.tabControl1.Location = new System.Drawing.Point(1, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 253);
            this.tabControl1.TabIndex = 9;
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.MaximimTraceCountTB);
            this.GeneralTab.Controls.Add(this.MaximimTraceCountLbl);
            this.GeneralTab.Controls.Add(this.MaximimErrorCountTB);
            this.GeneralTab.Controls.Add(this.MaximimErrorCountLbl);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTab.Size = new System.Drawing.Size(356, 227);
            this.GeneralTab.TabIndex = 3;
            this.GeneralTab.Text = "General Settings";
            this.GeneralTab.UseVisualStyleBackColor = true;
            // 
            // MaximimTraceCountTB
            // 
            this.MaximimTraceCountTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximimTraceCountTB.Location = new System.Drawing.Point(139, 40);
            this.MaximimTraceCountTB.Name = "MaximimTraceCountTB";
            this.MaximimTraceCountTB.Size = new System.Drawing.Size(85, 20);
            this.MaximimTraceCountTB.TabIndex = 40;
            // 
            // MaximimTraceCountLbl
            // 
            this.MaximimTraceCountLbl.Location = new System.Drawing.Point(7, 40);
            this.MaximimTraceCountLbl.Name = "MaximimTraceCountLbl";
            this.MaximimTraceCountLbl.Size = new System.Drawing.Size(126, 16);
            this.MaximimTraceCountLbl.TabIndex = 39;
            this.MaximimTraceCountLbl.Text = "Maximim Trace Count";
            // 
            // MaximimErrorCountTB
            // 
            this.MaximimErrorCountTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximimErrorCountTB.Location = new System.Drawing.Point(139, 14);
            this.MaximimErrorCountTB.Name = "MaximimErrorCountTB";
            this.MaximimErrorCountTB.Size = new System.Drawing.Size(85, 20);
            this.MaximimErrorCountTB.TabIndex = 38;
            // 
            // MaximimErrorCountLbl
            // 
            this.MaximimErrorCountLbl.Location = new System.Drawing.Point(7, 14);
            this.MaximimErrorCountLbl.Name = "MaximimErrorCountLbl";
            this.MaximimErrorCountLbl.Size = new System.Drawing.Size(126, 16);
            this.MaximimErrorCountLbl.TabIndex = 37;
            this.MaximimErrorCountLbl.Text = "Maximim Error Count";
            // 
            // DeviceEditorTab
            // 
            this.DeviceEditorTab.Location = new System.Drawing.Point(4, 22);
            this.DeviceEditorTab.Name = "DeviceEditorTab";
            this.DeviceEditorTab.Padding = new System.Windows.Forms.Padding(3);
            this.DeviceEditorTab.Size = new System.Drawing.Size(356, 227);
            this.DeviceEditorTab.TabIndex = 0;
            this.DeviceEditorTab.Text = "Device Editor";
            this.DeviceEditorTab.UseVisualStyleBackColor = true;
            // 
            // DirectorTab
            // 
            this.DirectorTab.Controls.Add(this.groupBox1);
            this.DirectorTab.Location = new System.Drawing.Point(4, 22);
            this.DirectorTab.Name = "DirectorTab";
            this.DirectorTab.Padding = new System.Windows.Forms.Padding(3);
            this.DirectorTab.Size = new System.Drawing.Size(356, 227);
            this.DirectorTab.TabIndex = 1;
            this.DirectorTab.Text = "Director";
            this.DirectorTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShowPropertyValueCB);
            this.groupBox1.Controls.Add(this.ShowCategoriesCB);
            this.groupBox1.Controls.Add(this.ShowTablesCB);
            this.groupBox1.Controls.Add(this.ShowPropertiesCB);
            this.groupBox1.Controls.Add(this.ShowDevicesCB);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 198);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device Tree Settings";
            // 
            // ShowPropertyValueCB
            // 
            this.ShowPropertyValueCB.AutoSize = true;
            this.ShowPropertyValueCB.Location = new System.Drawing.Point(16, 111);
            this.ShowPropertyValueCB.Name = "ShowPropertyValueCB";
            this.ShowPropertyValueCB.Size = new System.Drawing.Size(82, 17);
            this.ShowPropertyValueCB.TabIndex = 47;
            this.ShowPropertyValueCB.Text = "Show value";
            this.ShowPropertyValueCB.UseVisualStyleBackColor = true;
            // 
            // ShowCategoriesCB
            // 
            this.ShowCategoriesCB.AutoSize = true;
            this.ShowCategoriesCB.Checked = true;
            this.ShowCategoriesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowCategoriesCB.Location = new System.Drawing.Point(16, 42);
            this.ShowCategoriesCB.Name = "ShowCategoriesCB";
            this.ShowCategoriesCB.Size = new System.Drawing.Size(106, 17);
            this.ShowCategoriesCB.TabIndex = 46;
            this.ShowCategoriesCB.Text = "Show Categories";
            this.ShowCategoriesCB.UseVisualStyleBackColor = true;
            // 
            // ShowTablesCB
            // 
            this.ShowTablesCB.AutoSize = true;
            this.ShowTablesCB.Checked = true;
            this.ShowTablesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowTablesCB.Location = new System.Drawing.Point(16, 65);
            this.ShowTablesCB.Name = "ShowTablesCB";
            this.ShowTablesCB.Size = new System.Drawing.Size(88, 17);
            this.ShowTablesCB.TabIndex = 45;
            this.ShowTablesCB.Text = "Show Tables";
            this.ShowTablesCB.UseVisualStyleBackColor = true;
            // 
            // ShowPropertiesCB
            // 
            this.ShowPropertiesCB.AutoSize = true;
            this.ShowPropertiesCB.Checked = true;
            this.ShowPropertiesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowPropertiesCB.Location = new System.Drawing.Point(16, 88);
            this.ShowPropertiesCB.Name = "ShowPropertiesCB";
            this.ShowPropertiesCB.Size = new System.Drawing.Size(103, 17);
            this.ShowPropertiesCB.TabIndex = 44;
            this.ShowPropertiesCB.Text = "Show Properties";
            this.ShowPropertiesCB.UseVisualStyleBackColor = true;
            // 
            // ShowDevicesCB
            // 
            this.ShowDevicesCB.AutoSize = true;
            this.ShowDevicesCB.Checked = true;
            this.ShowDevicesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDevicesCB.Location = new System.Drawing.Point(16, 19);
            this.ShowDevicesCB.Name = "ShowDevicesCB";
            this.ShowDevicesCB.Size = new System.Drawing.Size(90, 17);
            this.ShowDevicesCB.TabIndex = 43;
            this.ShowDevicesCB.Text = "Show Device";
            this.ShowDevicesCB.UseVisualStyleBackColor = true;
            // 
            // AmiTab
            // 
            this.AmiTab.Controls.Add(this.EnableAMICB);
            this.AmiTab.Controls.Add(this.AMISettings);
            this.AmiTab.Location = new System.Drawing.Point(4, 22);
            this.AmiTab.Name = "AmiTab";
            this.AmiTab.Padding = new System.Windows.Forms.Padding(3);
            this.AmiTab.Size = new System.Drawing.Size(356, 227);
            this.AmiTab.TabIndex = 2;
            this.AmiTab.Text = "AMI";
            this.AmiTab.UseVisualStyleBackColor = true;
            // 
            // EnableAMICB
            // 
            this.EnableAMICB.AutoSize = true;
            this.EnableAMICB.Checked = true;
            this.EnableAMICB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableAMICB.Location = new System.Drawing.Point(16, 6);
            this.EnableAMICB.Name = "EnableAMICB";
            this.EnableAMICB.Size = new System.Drawing.Size(81, 17);
            this.EnableAMICB.TabIndex = 39;
            this.EnableAMICB.Text = "Enable AMI";
            this.EnableAMICB.UseVisualStyleBackColor = true;
            this.EnableAMICB.CheckedChanged += new System.EventHandler(this.EnableAMICB_CheckedChanged);
            // 
            // AMISettings
            // 
            this.AMISettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AMISettings.Controls.Add(this.TestBtn);
            this.AMISettings.Controls.Add(this.DatabaseSettingsBtn);
            this.AMISettings.Controls.Add(this.PortTB);
            this.AMISettings.Controls.Add(this.PortLbl);
            this.AMISettings.Controls.Add(this.HostTB);
            this.AMISettings.Controls.Add(this.HostLbl);
            this.AMISettings.Location = new System.Drawing.Point(7, 28);
            this.AMISettings.Name = "AMISettings";
            this.AMISettings.Size = new System.Drawing.Size(340, 188);
            this.AMISettings.TabIndex = 37;
            this.AMISettings.TabStop = false;
            this.AMISettings.Text = "AMI Settings";
            // 
            // TestBtn
            // 
            this.TestBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestBtn.Location = new System.Drawing.Point(208, 148);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(125, 23);
            this.TestBtn.TabIndex = 10;
            this.TestBtn.Text = "Validate settings...";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // DatabaseSettingsBtn
            // 
            this.DatabaseSettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DatabaseSettingsBtn.Location = new System.Drawing.Point(9, 148);
            this.DatabaseSettingsBtn.Name = "DatabaseSettingsBtn";
            this.DatabaseSettingsBtn.Size = new System.Drawing.Size(124, 23);
            this.DatabaseSettingsBtn.TabIndex = 38;
            this.DatabaseSettingsBtn.Text = "Database Settings...";
            this.DatabaseSettingsBtn.UseVisualStyleBackColor = true;
            this.DatabaseSettingsBtn.Click += new System.EventHandler(this.DatabaseSettingsBtn_Click);
            // 
            // PortTB
            // 
            this.PortTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PortTB.Location = new System.Drawing.Point(103, 45);
            this.PortTB.Name = "PortTB";
            this.PortTB.Size = new System.Drawing.Size(231, 20);
            this.PortTB.TabIndex = 34;
            // 
            // PortLbl
            // 
            this.PortLbl.Location = new System.Drawing.Point(6, 45);
            this.PortLbl.Name = "PortLbl";
            this.PortLbl.Size = new System.Drawing.Size(90, 16);
            this.PortLbl.TabIndex = 33;
            this.PortLbl.Text = "Port:";
            // 
            // HostTB
            // 
            this.HostTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HostTB.Location = new System.Drawing.Point(103, 19);
            this.HostTB.Name = "HostTB";
            this.HostTB.Size = new System.Drawing.Size(231, 20);
            this.HostTB.TabIndex = 32;
            // 
            // HostLbl
            // 
            this.HostLbl.Location = new System.Drawing.Point(6, 19);
            this.HostLbl.Name = "HostLbl";
            this.HostLbl.Size = new System.Drawing.Size(90, 16);
            this.HostLbl.TabIndex = 31;
            this.HostLbl.Text = "Host:";
            // 
            // Progress
            // 
            this.Progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Progress.Location = new System.Drawing.Point(5, 271);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(198, 8);
            this.Progress.TabIndex = 46;
            this.Progress.Visible = false;
            // 
            // GXOptionsForm
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(377, 297);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GXOptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gurux Device Suite Options";
            this.tabControl1.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.GeneralTab.PerformLayout();
            this.DirectorTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AmiTab.ResumeLayout(false);
            this.AmiTab.PerformLayout();
            this.AMISettings.ResumeLayout(false);
            this.AMISettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DeviceEditorTab;
        private System.Windows.Forms.TabPage DirectorTab;
        private System.Windows.Forms.TabPage AmiTab;
        private System.Windows.Forms.GroupBox AMISettings;
        private System.Windows.Forms.TextBox PortTB;
        private System.Windows.Forms.Label PortLbl;
        private System.Windows.Forms.TextBox HostTB;
        private System.Windows.Forms.Label HostLbl;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Button DatabaseSettingsBtn;
        private System.Windows.Forms.CheckBox EnableAMICB;
        private System.Windows.Forms.TabPage GeneralTab;
        private System.Windows.Forms.TextBox MaximimTraceCountTB;
        private System.Windows.Forms.Label MaximimTraceCountLbl;
        private System.Windows.Forms.TextBox MaximimErrorCountTB;
        private System.Windows.Forms.Label MaximimErrorCountLbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowTablesCB;
        private System.Windows.Forms.CheckBox ShowPropertiesCB;
        private System.Windows.Forms.CheckBox ShowDevicesCB;
        private System.Windows.Forms.CheckBox ShowCategoriesCB;
        private System.Windows.Forms.CheckBox ShowPropertyValueCB;
        private System.Windows.Forms.ProgressBar Progress;
    }
}