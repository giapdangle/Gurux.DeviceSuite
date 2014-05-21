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

namespace Gurux.DeviceSuite.Editor
{
    partial class GXDeviceEditor
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
            this.EditorPanel = new System.Windows.Forms.SplitContainer();
            this.PropertyTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExpandMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.TaskView = new System.Windows.Forms.ListView();
            this.TargetHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TraceView = new System.Windows.Forms.ListView();
            this.TimeCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LevelCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TraceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TraceCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.TracePauseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TraceFollowLastMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.TraceClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TraceHexMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorPanel.Panel1.SuspendLayout();
            this.EditorPanel.Panel2.SuspendLayout();
            this.EditorPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TraceMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorPanel
            // 
            this.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorPanel.Location = new System.Drawing.Point(0, 0);
            this.EditorPanel.Name = "EditorPanel";
            // 
            // EditorPanel.Panel1
            // 
            this.EditorPanel.Panel1.Controls.Add(this.PropertyTree);
            // 
            // EditorPanel.Panel2
            // 
            this.EditorPanel.Panel2.Controls.Add(this.splitContainer1);
            this.EditorPanel.Size = new System.Drawing.Size(690, 533);
            this.EditorPanel.SplitterDistance = 224;
            this.EditorPanel.TabIndex = 4;
            // 
            // PropertyTree
            // 
            this.PropertyTree.ContextMenuStrip = this.contextMenuStrip1;
            this.PropertyTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyTree.HideSelection = false;
            this.PropertyTree.Location = new System.Drawing.Point(0, 0);
            this.PropertyTree.Name = "PropertyTree";
            this.PropertyTree.Size = new System.Drawing.Size(224, 533);
            this.PropertyTree.TabIndex = 2;
            this.PropertyTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PropertyTree_AfterSelect);
            this.PropertyTree.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMnu,
            this.PropertiesMenu,
            this.RemoveMenu,
            this.toolStripMenuItem1,
            this.ExpandMenu,
            this.CollapseMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 120);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // NewMnu
            // 
            this.NewMnu.Name = "NewMnu";
            this.NewMnu.Size = new System.Drawing.Size(136, 22);
            this.NewMnu.Text = "New...";
            this.NewMnu.Click += new System.EventHandler(this.NewMnu_Click);
            // 
            // PropertiesMenu
            // 
            this.PropertiesMenu.Name = "PropertiesMenu";
            this.PropertiesMenu.Size = new System.Drawing.Size(136, 22);
            this.PropertiesMenu.Text = "Properties...";
            this.PropertiesMenu.Click += new System.EventHandler(this.PropertiesMenu_Click);
            // 
            // RemoveMenu
            // 
            this.RemoveMenu.Name = "RemoveMenu";
            this.RemoveMenu.Size = new System.Drawing.Size(136, 22);
            this.RemoveMenu.Text = "Remove";
            this.RemoveMenu.Click += new System.EventHandler(this.RemoveMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // ExpandMenu
            // 
            this.ExpandMenu.Name = "ExpandMenu";
            this.ExpandMenu.Size = new System.Drawing.Size(136, 22);
            this.ExpandMenu.Text = "Expand";
            this.ExpandMenu.Click += new System.EventHandler(this.ExpandMenu_Click);
            // 
            // CollapseMenu
            // 
            this.CollapseMenu.Name = "CollapseMenu";
            this.CollapseMenu.Size = new System.Drawing.Size(136, 22);
            this.CollapseMenu.Text = "Collapse";
            this.CollapseMenu.Click += new System.EventHandler(this.CollapseMenu_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.PropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TaskView);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Panel2.Controls.Add(this.TraceView);
            this.splitContainer1.Size = new System.Drawing.Size(462, 533);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 0;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(462, 147);
            this.PropertyGrid.TabIndex = 15;
            this.PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            this.PropertyGrid.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // TaskView
            // 
            this.TaskView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TargetHeader,
            this.DescriptionHeader});
            this.TaskView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskView.FullRowSelect = true;
            this.TaskView.Location = new System.Drawing.Point(0, 0);
            this.TaskView.MultiSelect = false;
            this.TaskView.Name = "TaskView";
            this.TaskView.Size = new System.Drawing.Size(229, 382);
            this.TaskView.TabIndex = 5;
            this.TaskView.UseCompatibleStateImageBehavior = false;
            this.TaskView.View = System.Windows.Forms.View.Details;
            this.TaskView.DoubleClick += new System.EventHandler(this.TaskView_DoubleClick);
            this.TaskView.Enter += new System.EventHandler(this.TaskView_Enter);
            // 
            // TargetHeader
            // 
            this.TargetHeader.Text = "Target";
            this.TargetHeader.Width = 87;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 138;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(229, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 382);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // TraceView
            // 
            this.TraceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeCH,
            this.LevelCH,
            this.DescriptionCH});
            this.TraceView.ContextMenuStrip = this.TraceMenu;
            this.TraceView.Dock = System.Windows.Forms.DockStyle.Right;
            this.TraceView.FullRowSelect = true;
            this.TraceView.Location = new System.Drawing.Point(232, 0);
            this.TraceView.Name = "TraceView";
            this.TraceView.Size = new System.Drawing.Size(230, 382);
            this.TraceView.TabIndex = 3;
            this.TraceView.UseCompatibleStateImageBehavior = false;
            this.TraceView.View = System.Windows.Forms.View.Details;
            this.TraceView.VirtualMode = true;
            this.TraceView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.TraceView_RetrieveVirtualItem);
            this.TraceView.Enter += new System.EventHandler(this.TraceView_Enter);
            // 
            // TimeCH
            // 
            this.TimeCH.Text = "Time";
            // 
            // LevelCH
            // 
            this.LevelCH.Text = "Level";
            // 
            // DescriptionCH
            // 
            this.DescriptionCH.Text = "Description";
            this.DescriptionCH.Width = 214;
            // 
            // TraceMenu
            // 
            this.TraceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TraceCopyMenu,
            this.SelectAllMenu,
            this.toolStripMenuItem2,
            this.TracePauseMenu,
            this.TraceFollowLastMenu,
            this.toolStripSeparator9,
            this.TraceClearMenu,
            this.TraceHexMenu});
            this.TraceMenu.Name = "contextMenuStrip1";
            this.TraceMenu.Size = new System.Drawing.Size(138, 148);
            // 
            // TraceCopyMenu
            // 
            this.TraceCopyMenu.Name = "TraceCopyMenu";
            this.TraceCopyMenu.Size = new System.Drawing.Size(137, 22);
            this.TraceCopyMenu.Text = "Copy";
            this.TraceCopyMenu.Click += new System.EventHandler(this.TraceCopyMenu_Click);
            // 
            // SelectAllMenu
            // 
            this.SelectAllMenu.Name = "SelectAllMenu";
            this.SelectAllMenu.Size = new System.Drawing.Size(137, 22);
            this.SelectAllMenu.Text = "Select All";
            this.SelectAllMenu.Click += new System.EventHandler(this.SelectAllMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(134, 6);
            // 
            // TracePauseMenu
            // 
            this.TracePauseMenu.Name = "TracePauseMenu";
            this.TracePauseMenu.Size = new System.Drawing.Size(137, 22);
            this.TracePauseMenu.Text = "Pause";
            this.TracePauseMenu.Click += new System.EventHandler(this.TracePauseMenu_Click);
            // 
            // TraceFollowLastMenu
            // 
            this.TraceFollowLastMenu.Checked = true;
            this.TraceFollowLastMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TraceFollowLastMenu.Name = "TraceFollowLastMenu";
            this.TraceFollowLastMenu.Size = new System.Drawing.Size(137, 22);
            this.TraceFollowLastMenu.Text = "Follow Last";
            this.TraceFollowLastMenu.Click += new System.EventHandler(this.TraceFollowLastMenu_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(134, 6);
            // 
            // TraceClearMenu
            // 
            this.TraceClearMenu.Name = "TraceClearMenu";
            this.TraceClearMenu.Size = new System.Drawing.Size(137, 22);
            this.TraceClearMenu.Text = "Clear";
            this.TraceClearMenu.Click += new System.EventHandler(this.TraceClearMenu_Click);
            // 
            // TraceHexMenu
            // 
            this.TraceHexMenu.Checked = true;
            this.TraceHexMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TraceHexMenu.Name = "TraceHexMenu";
            this.TraceHexMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.TraceHexMenu.Size = new System.Drawing.Size(137, 22);
            this.TraceHexMenu.Text = "&Hex";
            this.TraceHexMenu.Click += new System.EventHandler(this.TraceHexMenu_Click);
            // 
            // GXDeviceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 533);
            this.ControlBox = false;
            this.Controls.Add(this.EditorPanel);
            this.Name = "GXDeviceEditor";
            this.Text = "GXDeviceEditor";
            this.EditorPanel.Panel1.ResumeLayout(false);
            this.EditorPanel.Panel2.ResumeLayout(false);
            this.EditorPanel.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.TraceMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer EditorPanel;
        private System.Windows.Forms.TreeView PropertyTree;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewMnu;
        private System.Windows.Forms.ToolStripMenuItem RemoveMenu;
        private System.Windows.Forms.ToolStripMenuItem PropertiesMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExpandMenu;
        private System.Windows.Forms.ToolStripMenuItem CollapseMenu;
        private System.Windows.Forms.ContextMenuStrip TraceMenu;
        private System.Windows.Forms.ToolStripMenuItem TracePauseMenu;
        private System.Windows.Forms.ToolStripMenuItem TraceCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem TraceFollowLastMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem TraceClearMenu;
        private System.Windows.Forms.Splitter splitter1;
        public System.Windows.Forms.ListView TraceView;
        private System.Windows.Forms.ColumnHeader TimeCH;
        private System.Windows.Forms.ColumnHeader LevelCH;
        private System.Windows.Forms.ColumnHeader DescriptionCH;
        private System.Windows.Forms.ListView TaskView;
        private System.Windows.Forms.ColumnHeader TargetHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ToolStripMenuItem TraceHexMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}