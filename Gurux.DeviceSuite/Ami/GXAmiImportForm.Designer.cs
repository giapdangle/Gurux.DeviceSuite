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
    partial class GXAmiImportForm
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
            this.components = new System.ComponentModel.Container();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PresetPage = new System.Windows.Forms.TabPage();
            this.PresetList = new System.Windows.Forms.ListView();
            this.PresetNameCH = new System.Windows.Forms.ColumnHeader();
            this.ManufacturerCH = new System.Windows.Forms.ColumnHeader();
            this.ModelCH = new System.Windows.Forms.ColumnHeader();
            this.VersionCH = new System.Windows.Forms.ColumnHeader();
            this.CustomPage = new System.Windows.Forms.TabPage();
            this.CustomDeviceType = new Gurux.DeviceSuite.Common.CustomDeviceTypeListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.PresetPage.SuspendLayout();
            this.CustomPage.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.Location = new System.Drawing.Point(194, 227);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(80, 24);
            this.OKBtn.TabIndex = 3;
            this.OKBtn.Text = "OK";
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(282, 227);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(80, 24);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "Cancel";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.PresetPage);
            this.tabControl1.Controls.Add(this.CustomPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 221);
            this.tabControl1.TabIndex = 6;
            // 
            // PresetPage
            // 
            this.PresetPage.Controls.Add(this.PresetList);
            this.PresetPage.Location = new System.Drawing.Point(4, 22);
            this.PresetPage.Name = "PresetPage";
            this.PresetPage.Padding = new System.Windows.Forms.Padding(3);
            this.PresetPage.Size = new System.Drawing.Size(362, 195);
            this.PresetPage.TabIndex = 0;
            this.PresetPage.Text = "Preset";
            this.PresetPage.UseVisualStyleBackColor = true;
            // 
            // PresetList
            // 
            this.PresetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PresetNameCH,
            this.ManufacturerCH,
            this.ModelCH,
            this.VersionCH});
            this.PresetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PresetList.FullRowSelect = true;
            this.PresetList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PresetList.HideSelection = false;
            this.PresetList.Location = new System.Drawing.Point(3, 3);
            this.PresetList.MultiSelect = false;
            this.PresetList.Name = "PresetList";
            this.PresetList.Size = new System.Drawing.Size(356, 189);
            this.PresetList.TabIndex = 2;
            this.PresetList.UseCompatibleStateImageBehavior = false;
            this.PresetList.View = System.Windows.Forms.View.Details;
            this.PresetList.DoubleClick += new System.EventHandler(this.OKBtn_Click);
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
            // CustomPage
            // 
            this.CustomPage.Controls.Add(this.CustomDeviceType);
            this.CustomPage.Location = new System.Drawing.Point(4, 22);
            this.CustomPage.Name = "CustomPage";
            this.CustomPage.Padding = new System.Windows.Forms.Padding(3);
            this.CustomPage.Size = new System.Drawing.Size(362, 195);
            this.CustomPage.TabIndex = 1;
            this.CustomPage.Text = "Custom";
            this.CustomPage.UseVisualStyleBackColor = true;
            // 
            // CustomDeviceType
            // 
            this.CustomDeviceType.ContextMenuStrip = this.contextMenuStrip1;
            this.CustomDeviceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomDeviceType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CustomDeviceType.FormattingEnabled = true;
            this.CustomDeviceType.Location = new System.Drawing.Point(3, 3);
            this.CustomDeviceType.Name = "CustomDeviceType";
            this.CustomDeviceType.Size = new System.Drawing.Size(356, 186);
            this.CustomDeviceType.Sorted = true;
            this.CustomDeviceType.TabIndex = 0;
            this.CustomDeviceType.DoubleClick += new System.EventHandler(this.OKBtn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveMnu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // RemoveMnu
            // 
            this.RemoveMnu.Name = "RemoveMnu";
            this.RemoveMnu.Size = new System.Drawing.Size(117, 22);
            this.RemoveMnu.Text = "Remove";
            // 
            // GXAmiImportForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(368, 262);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.CancelBtn);
            this.Name = "GXAmiImportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Device Template";
            this.tabControl1.ResumeLayout(false);
            this.PresetPage.ResumeLayout(false);
            this.CustomPage.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PresetPage;
        private System.Windows.Forms.TabPage CustomPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RemoveMnu;
        private System.Windows.Forms.ListView PresetList;
        private System.Windows.Forms.ColumnHeader PresetNameCH;
        private System.Windows.Forms.ColumnHeader ManufacturerCH;
        private System.Windows.Forms.ColumnHeader ModelCH;
        private System.Windows.Forms.ColumnHeader VersionCH;
        private Gurux.DeviceSuite.Common.CustomDeviceTypeListBox CustomDeviceType;
    }
}