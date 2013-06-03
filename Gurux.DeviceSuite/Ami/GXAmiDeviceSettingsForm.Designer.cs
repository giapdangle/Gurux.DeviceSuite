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

namespace Gurux.DeviceSuite.Ami
{
    partial class GXAmiDeviceSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXAmiDeviceSettingsForm));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.WaitTimeTb = new System.Windows.Forms.DateTimePicker();
            this.RefreshRateTp = new System.Windows.Forms.DateTimePicker();
            this.WaitTimeLbl = new System.Windows.Forms.Label();
            this.ResendCountTb = new System.Windows.Forms.TextBox();
            this.ResendCountLbl = new System.Windows.Forms.Label();
            this.RefreshRateLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.CustomDeviceType = new Gurux.DeviceSuite.Common.CustomDeviceTypeListBox();
            this.PresetList = new System.Windows.Forms.ListView();
            this.PresetNameCH = new System.Windows.Forms.ColumnHeader();
            this.ManufacturerCH = new System.Windows.Forms.ColumnHeader();
            this.ModelCH = new System.Windows.Forms.ColumnHeader();
            this.VersionCH = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CustomRB = new System.Windows.Forms.RadioButton();
            this.PresetCB = new System.Windows.Forms.RadioButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MediaFrame = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MediaCB = new System.Windows.Forms.ComboBox();
            this.MediaLbl = new System.Windows.Forms.Label();
            this.DeviceTemplateVersionCH = new System.Windows.Forms.ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(437, 435);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 13;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.Location = new System.Drawing.Point(356, 435);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 12;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.GeneralTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 429);
            this.tabControl1.TabIndex = 0;
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.splitContainer1);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Size = new System.Drawing.Size(504, 403);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "GeneralTab";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(504, 403);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.WaitTimeTb);
            this.splitContainer3.Panel1.Controls.Add(this.RefreshRateTp);
            this.splitContainer3.Panel1.Controls.Add(this.WaitTimeLbl);
            this.splitContainer3.Panel1.Controls.Add(this.ResendCountTb);
            this.splitContainer3.Panel1.Controls.Add(this.ResendCountLbl);
            this.splitContainer3.Panel1.Controls.Add(this.RefreshRateLbl);
            this.splitContainer3.Panel1.Controls.Add(this.NameLbl);
            this.splitContainer3.Panel1.Controls.Add(this.NameTB);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.CustomDeviceType);
            this.splitContainer3.Panel2.Controls.Add(this.PresetList);
            this.splitContainer3.Panel2.Controls.Add(this.panel1);
            this.splitContainer3.Size = new System.Drawing.Size(504, 168);
            this.splitContainer3.SplitterDistance = 70;
            this.splitContainer3.TabIndex = 0;
            // 
            // WaitTimeTb
            // 
            this.WaitTimeTb.CustomFormat = "HH:mm:ss";
            this.WaitTimeTb.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.WaitTimeTb.Location = new System.Drawing.Point(236, 38);
            this.WaitTimeTb.Name = "WaitTimeTb";
            this.WaitTimeTb.ShowUpDown = true;
            this.WaitTimeTb.Size = new System.Drawing.Size(74, 20);
            this.WaitTimeTb.TabIndex = 3;
            // 
            // RefreshRateTp
            // 
            this.RefreshRateTp.CustomFormat = "HH:mm:ss";
            this.RefreshRateTp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RefreshRateTp.Location = new System.Drawing.Point(405, 15);
            this.RefreshRateTp.Name = "RefreshRateTp";
            this.RefreshRateTp.ShowUpDown = true;
            this.RefreshRateTp.Size = new System.Drawing.Size(81, 20);
            this.RefreshRateTp.TabIndex = 1;
            // 
            // WaitTimeLbl
            // 
            this.WaitTimeLbl.Location = new System.Drawing.Point(151, 40);
            this.WaitTimeLbl.Name = "WaitTimeLbl";
            this.WaitTimeLbl.Size = new System.Drawing.Size(79, 16);
            this.WaitTimeLbl.TabIndex = 137;
            this.WaitTimeLbl.Text = "Wait Time:";
            // 
            // ResendCountTb
            // 
            this.ResendCountTb.Location = new System.Drawing.Point(88, 38);
            this.ResendCountTb.Name = "ResendCountTb";
            this.ResendCountTb.Size = new System.Drawing.Size(58, 20);
            this.ResendCountTb.TabIndex = 2;
            // 
            // ResendCountLbl
            // 
            this.ResendCountLbl.Location = new System.Drawing.Point(7, 40);
            this.ResendCountLbl.Name = "ResendCountLbl";
            this.ResendCountLbl.Size = new System.Drawing.Size(80, 16);
            this.ResendCountLbl.TabIndex = 136;
            this.ResendCountLbl.Text = "Resend Count:";
            // 
            // RefreshRateLbl
            // 
            this.RefreshRateLbl.Location = new System.Drawing.Point(322, 17);
            this.RefreshRateLbl.Name = "RefreshRateLbl";
            this.RefreshRateLbl.Size = new System.Drawing.Size(78, 15);
            this.RefreshRateLbl.TabIndex = 135;
            this.RefreshRateLbl.Text = "Refresh rate:";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(7, 15);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(38, 13);
            this.NameLbl.TabIndex = 129;
            this.NameLbl.Text = "Name:";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(88, 12);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(222, 20);
            this.NameTB.TabIndex = 0;
            // 
            // CustomDeviceType
            // 
            this.CustomDeviceType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CustomDeviceType.FormattingEnabled = true;
            this.CustomDeviceType.Location = new System.Drawing.Point(297, 3);
            this.CustomDeviceType.Name = "CustomDeviceType";
            this.CustomDeviceType.Size = new System.Drawing.Size(204, 69);
            this.CustomDeviceType.Sorted = true;
            this.CustomDeviceType.TabIndex = 7;
            this.CustomDeviceType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CustomDeviceType_DrawItem);
            this.CustomDeviceType.SelectedIndexChanged += new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
            // 
            // PresetList
            // 
            this.PresetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ManufacturerCH,
            this.ModelCH,
            this.VersionCH,
            this.PresetNameCH,
            this.DeviceTemplateVersionCH});
            this.PresetList.FullRowSelect = true;
            this.PresetList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PresetList.HideSelection = false;
            this.PresetList.Location = new System.Drawing.Point(93, 3);
            this.PresetList.MultiSelect = false;
            this.PresetList.Name = "PresetList";
            this.PresetList.Size = new System.Drawing.Size(198, 67);
            this.PresetList.TabIndex = 6;
            this.PresetList.UseCompatibleStateImageBehavior = false;
            this.PresetList.View = System.Windows.Forms.View.Details;
            this.PresetList.SelectedIndexChanged += new System.EventHandler(this.PresetList_SelectedIndexChanged);
            // 
            // PresetNameCH
            // 
            this.PresetNameCH.Text = "Preset Name";
            this.PresetNameCH.Width = 102;
            // 
            // ManufacturerCH
            // 
            this.ManufacturerCH.Text = "Manufacturer";
            this.ManufacturerCH.Width = 85;
            // 
            // ModelCH
            // 
            this.ModelCH.Text = "Model";
            this.ModelCH.Width = 98;
            // 
            // VersionCH
            // 
            this.VersionCH.Text = "Version";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CustomRB);
            this.panel1.Controls.Add(this.PresetCB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 94);
            this.panel1.TabIndex = 0;
            // 
            // CustomRB
            // 
            this.CustomRB.AutoSize = true;
            this.CustomRB.Location = new System.Drawing.Point(10, 41);
            this.CustomRB.Name = "CustomRB";
            this.CustomRB.Size = new System.Drawing.Size(60, 17);
            this.CustomRB.TabIndex = 5;
            this.CustomRB.Text = "Custom";
            this.CustomRB.UseVisualStyleBackColor = true;
            // 
            // PresetCB
            // 
            this.PresetCB.AutoSize = true;
            this.PresetCB.Checked = true;
            this.PresetCB.Location = new System.Drawing.Point(10, 18);
            this.PresetCB.Name = "PresetCB";
            this.PresetCB.Size = new System.Drawing.Size(58, 17);
            this.PresetCB.TabIndex = 4;
            this.PresetCB.TabStop = true;
            this.PresetCB.Text = "Preset ";
            this.PresetCB.UseVisualStyleBackColor = true;
            this.PresetCB.CheckedChanged += new System.EventHandler(this.PresetCB_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.SettingsPanel);
            this.splitContainer2.Panel1.Controls.Add(this.PropertyGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.MediaFrame);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(504, 231);
            this.splitContainer2.SplitterDistance = 214;
            this.splitContainer2.TabIndex = 0;
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Location = new System.Drawing.Point(0, 116);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(211, 112);
            this.SettingsPanel.TabIndex = 11;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.HelpVisible = false;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.PropertyGrid.Size = new System.Drawing.Size(214, 110);
            this.PropertyGrid.TabIndex = 8;
            this.PropertyGrid.ToolbarVisible = false;
            // 
            // MediaFrame
            // 
            this.MediaFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaFrame.Location = new System.Drawing.Point(0, 32);
            this.MediaFrame.Name = "MediaFrame";
            this.MediaFrame.Size = new System.Drawing.Size(286, 199);
            this.MediaFrame.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MediaCB);
            this.panel2.Controls.Add(this.MediaLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 32);
            this.panel2.TabIndex = 9;
            // 
            // MediaCB
            // 
            this.MediaCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MediaCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaCB.FormattingEnabled = true;
            this.MediaCB.Location = new System.Drawing.Point(79, 4);
            this.MediaCB.Name = "MediaCB";
            this.MediaCB.Size = new System.Drawing.Size(130, 21);
            this.MediaCB.TabIndex = 9;
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
            // DeviceTemplateVersionCH
            // 
            this.DeviceTemplateVersionCH.Text = "Version";
            // 
            // GXAmiDeviceSettingsForm
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(524, 470);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GXAmiDeviceSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Device Settings";
            this.Load += new System.EventHandler(this.GXAmiDeviceSettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage GeneralTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DateTimePicker WaitTimeTb;
        private System.Windows.Forms.DateTimePicker RefreshRateTp;
        private System.Windows.Forms.Label WaitTimeLbl;
        private System.Windows.Forms.TextBox ResendCountTb;
        private System.Windows.Forms.Label ResendCountLbl;
        private System.Windows.Forms.Label RefreshRateLbl;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton CustomRB;
        private System.Windows.Forms.RadioButton PresetCB;
        private System.Windows.Forms.ListView PresetList;
        private System.Windows.Forms.ColumnHeader PresetNameCH;
        private System.Windows.Forms.ColumnHeader ManufacturerCH;
        private System.Windows.Forms.ColumnHeader ModelCH;
        private System.Windows.Forms.ColumnHeader VersionCH;
        private Gurux.DeviceSuite.Common.CustomDeviceTypeListBox CustomDeviceType;
        private System.Windows.Forms.Panel MediaFrame;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox MediaCB;
        private System.Windows.Forms.Label MediaLbl;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.ColumnHeader DeviceTemplateVersionCH;
    }
}