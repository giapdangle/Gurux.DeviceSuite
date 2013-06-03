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

namespace Gurux.DeviceSuite.Common
{
    partial class GXPresetDevicesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXPresetDevicesForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PublisherImage = new System.Windows.Forms.Panel();
            this.ShowDisabledCB = new System.Windows.Forms.CheckBox();
            this.ShowPreviousVersionsCB = new System.Windows.Forms.CheckBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PresetPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PresetTree = new System.Windows.Forms.TreeView();
            this.PresetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PresetAddMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PresetDeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PresetPropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.PresetList = new System.Windows.Forms.ListView();
            this.TargetCH = new System.Windows.Forms.ColumnHeader();
            this.StateCH = new System.Windows.Forms.ColumnHeader();
            this.VersionCH = new System.Windows.Forms.ColumnHeader();
            this.LoadablePage = new System.Windows.Forms.TabPage();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.LoadableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LoadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EnableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PresetPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PresetMenu.SuspendLayout();
            this.LoadableMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PublisherImage);
            this.panel1.Controls.Add(this.ShowDisabledCB);
            this.panel1.Controls.Add(this.ShowPreviousVersionsCB);
            this.panel1.Controls.Add(this.OKBtn);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 46);
            this.panel1.TabIndex = 29;
            // 
            // PublisherImage
            // 
            this.PublisherImage.BackColor = System.Drawing.Color.Transparent;
            this.PublisherImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PublisherImage.BackgroundImage")));
            this.PublisherImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PublisherImage.Location = new System.Drawing.Point(6, 4);
            this.PublisherImage.Name = "PublisherImage";
            this.PublisherImage.Size = new System.Drawing.Size(40, 40);
            this.PublisherImage.TabIndex = 25;
            // 
            // ShowDisabledCB
            // 
            this.ShowDisabledCB.AutoSize = true;
            this.ShowDisabledCB.Location = new System.Drawing.Point(52, 26);
            this.ShowDisabledCB.Name = "ShowDisabledCB";
            this.ShowDisabledCB.Size = new System.Drawing.Size(97, 17);
            this.ShowDisabledCB.TabIndex = 5;
            this.ShowDisabledCB.Text = "Show Disabled";
            this.ShowDisabledCB.UseVisualStyleBackColor = true;
            this.ShowDisabledCB.CheckedChanged += new System.EventHandler(this.ShowDisabledCB_CheckedChanged);
            // 
            // ShowPreviousVersionsCB
            // 
            this.ShowPreviousVersionsCB.AutoSize = true;
            this.ShowPreviousVersionsCB.Location = new System.Drawing.Point(52, 6);
            this.ShowPreviousVersionsCB.Name = "ShowPreviousVersionsCB";
            this.ShowPreviousVersionsCB.Size = new System.Drawing.Size(140, 17);
            this.ShowPreviousVersionsCB.TabIndex = 4;
            this.ShowPreviousVersionsCB.Text = "Show Previous Versions";
            this.ShowPreviousVersionsCB.UseVisualStyleBackColor = true;
            this.ShowPreviousVersionsCB.CheckedChanged += new System.EventHandler(this.ShowPreviouslyVersionsCB_CheckedChanged);
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(272, 11);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "&OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(353, 11);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PresetPage);
            this.tabControl1.Controls.Add(this.LoadablePage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(440, 231);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // PresetPage
            // 
            this.PresetPage.Controls.Add(this.splitContainer1);
            this.PresetPage.Location = new System.Drawing.Point(4, 22);
            this.PresetPage.Name = "PresetPage";
            this.PresetPage.Padding = new System.Windows.Forms.Padding(3);
            this.PresetPage.Size = new System.Drawing.Size(432, 205);
            this.PresetPage.TabIndex = 0;
            this.PresetPage.Text = "Preset";
            this.PresetPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PresetTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PresetList);
            this.splitContainer1.Size = new System.Drawing.Size(426, 199);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 32;
            // 
            // PresetTree
            // 
            this.PresetTree.ContextMenuStrip = this.PresetMenu;
            this.PresetTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PresetTree.HideSelection = false;
            this.PresetTree.ImageIndex = 0;
            this.PresetTree.ImageList = this.imageList1;
            this.PresetTree.Location = new System.Drawing.Point(0, 0);
            this.PresetTree.Name = "PresetTree";
            this.PresetTree.SelectedImageIndex = 0;
            this.PresetTree.Size = new System.Drawing.Size(140, 199);
            this.PresetTree.TabIndex = 0;
            this.PresetTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PresetTree_AfterSelect);
            // 
            // PresetMenu
            // 
            this.PresetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PresetAddMenu,
            this.PresetDeleteMenu,
            this.PresetPropertiesMenu});
            this.PresetMenu.Name = "ScheduleMenu";
            this.PresetMenu.Size = new System.Drawing.Size(140, 70);
            this.PresetMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PresetMenu_Opening);
            // 
            // PresetAddMenu
            // 
            this.PresetAddMenu.Name = "PresetAddMenu";
            this.PresetAddMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.PresetAddMenu.Size = new System.Drawing.Size(139, 22);
            this.PresetAddMenu.Text = "Add";
            this.PresetAddMenu.Click += new System.EventHandler(this.PresetAddMenu_Click);
            // 
            // PresetDeleteMenu
            // 
            this.PresetDeleteMenu.Name = "PresetDeleteMenu";
            this.PresetDeleteMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.PresetDeleteMenu.Size = new System.Drawing.Size(139, 22);
            this.PresetDeleteMenu.Text = "Delete";
            this.PresetDeleteMenu.Click += new System.EventHandler(this.PresetDeleteMenu_Click);
            // 
            // PresetPropertiesMenu
            // 
            this.PresetPropertiesMenu.Name = "PresetPropertiesMenu";
            this.PresetPropertiesMenu.Size = new System.Drawing.Size(139, 22);
            this.PresetPropertiesMenu.Text = "Properties...";
            this.PresetPropertiesMenu.Click += new System.EventHandler(this.PresetPropertiesMenu_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "empty.bmp");
            this.imageList1.Images.SetKeyName(1, "deletemnu.bmp");
            this.imageList1.Images.SetKeyName(2, "installed.bmp");
            this.imageList1.Images.SetKeyName(3, "download.bmp");
            // 
            // PresetList
            // 
            this.PresetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TargetCH,
            this.StateCH,
            this.VersionCH});
            this.PresetList.ContextMenuStrip = this.PresetMenu;
            this.PresetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PresetList.FullRowSelect = true;
            this.PresetList.HideSelection = false;
            this.PresetList.Location = new System.Drawing.Point(0, 0);
            this.PresetList.Name = "PresetList";
            this.PresetList.Size = new System.Drawing.Size(282, 199);
            this.PresetList.TabIndex = 1;
            this.PresetList.UseCompatibleStateImageBehavior = false;
            this.PresetList.View = System.Windows.Forms.View.Details;
            this.PresetList.DoubleClick += new System.EventHandler(this.PresetList_DoubleClick);
            // 
            // TargetCH
            // 
            this.TargetCH.Text = "Target";
            this.TargetCH.Width = 111;
            // 
            // StateCH
            // 
            this.StateCH.Text = "State:";
            // 
            // VersionCH
            // 
            this.VersionCH.Text = "Version";
            this.VersionCH.Width = 98;
            // 
            // LoadablePage
            // 
            this.LoadablePage.Location = new System.Drawing.Point(4, 22);
            this.LoadablePage.Name = "LoadablePage";
            this.LoadablePage.Padding = new System.Windows.Forms.Padding(3);
            this.LoadablePage.Size = new System.Drawing.Size(432, 205);
            this.LoadablePage.TabIndex = 1;
            this.LoadablePage.Text = "Loadable";
            this.LoadablePage.UseVisualStyleBackColor = true;
            // 
            // LoadableMenu
            // 
            this.LoadableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadMenu,
            this.DisableMenu,
            this.EnableMenu});
            this.LoadableMenu.Name = "ScheduleMenu";
            this.LoadableMenu.Size = new System.Drawing.Size(141, 70);
            this.LoadableMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LoadableMenu_Opening);
            // 
            // LoadMenu
            // 
            this.LoadMenu.Name = "LoadMenu";
            this.LoadMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.LoadMenu.Size = new System.Drawing.Size(140, 22);
            this.LoadMenu.Text = "Load";
            this.LoadMenu.Click += new System.EventHandler(this.LoadMenu_Click);
            // 
            // DisableMenu
            // 
            this.DisableMenu.Name = "DisableMenu";
            this.DisableMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.DisableMenu.Size = new System.Drawing.Size(140, 22);
            this.DisableMenu.Text = "Disable";
            this.DisableMenu.Click += new System.EventHandler(this.DisableMenu_Click);
            // 
            // EnableMenu
            // 
            this.EnableMenu.Name = "EnableMenu";
            this.EnableMenu.Size = new System.Drawing.Size(140, 22);
            this.EnableMenu.Text = "Enable";
            this.EnableMenu.Click += new System.EventHandler(this.EnableMenu_Click);
            // 
            // GXPresetDevicesForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(440, 277);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GXPresetDevicesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preset and Downloadable Device Profiles";
            this.Load += new System.EventHandler(this.GXPresetDevicesForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.PresetPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.PresetMenu.ResumeLayout(false);
            this.LoadableMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PresetPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView PresetTree;
        private System.Windows.Forms.TabPage LoadablePage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView PresetList;
        private System.Windows.Forms.ColumnHeader TargetCH;
        private System.Windows.Forms.ContextMenuStrip PresetMenu;
        private System.Windows.Forms.ToolStripMenuItem PresetAddMenu;
        private System.Windows.Forms.ToolStripMenuItem PresetDeleteMenu;
        private System.Windows.Forms.ToolStripMenuItem PresetPropertiesMenu;
        private System.Windows.Forms.ContextMenuStrip LoadableMenu;
        private System.Windows.Forms.ToolStripMenuItem LoadMenu;
        private System.Windows.Forms.ToolStripMenuItem DisableMenu;
        private System.Windows.Forms.ColumnHeader VersionCH;
        private System.Windows.Forms.ColumnHeader StateCH;
        private System.Windows.Forms.CheckBox ShowPreviousVersionsCB;
        private System.Windows.Forms.CheckBox ShowDisabledCB;
        private System.Windows.Forms.ToolStripMenuItem EnableMenu;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel PublisherImage;
    }
}