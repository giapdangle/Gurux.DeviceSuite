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
    partial class GXAmi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXAmi));
            this.AmiPanel = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.AssignedTab = new System.Windows.Forms.TabPage();
            this.DCList = new System.Windows.Forms.ListView();
            this.DcCH = new System.Windows.Forms.ColumnHeader();
            this.DataCollectorMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewDeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.PropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.AddCommandPromptMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UnAssignedTab = new System.Windows.Forms.TabPage();
            this.UnassignedDCList = new System.Windows.Forms.ListView();
            this.MACAddressLbl = new System.Windows.Forms.ColumnHeader();
            this.IPAddressCH = new System.Windows.Forms.ColumnHeader();
            this.UnAssignedMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AssignMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveUnAssignedMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DevicesList = new System.Windows.Forms.ListView();
            this.DeviceNameCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceStatusCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceTypeCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeviceNewDeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceDeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceReadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.DeviceStartMonitorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceStopMonitorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.DevicePropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TablePanel = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.ValuesTab = new System.Windows.Forms.TabPage();
            this.PropertyList = new System.Windows.Forms.ListView();
            this.PropertyNameCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyTypeCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyValueCH = new System.Windows.Forms.ColumnHeader();
            this.TimestampCH = new System.Windows.Forms.ColumnHeader();
            this.TableTab = new System.Windows.Forms.TabPage();
            this.TableData = new System.Windows.Forms.DataGridView();
            this.TableTabs = new System.Windows.Forms.TabControl();
            this.ViewTab = new System.Windows.Forms.TabPage();
            this.TableRowCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CountTB = new System.Windows.Forms.TextBox();
            this.IndexTB = new System.Windows.Forms.TextBox();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.ShowByIndexlCB = new System.Windows.Forms.RadioButton();
            this.ShowAllCB = new System.Windows.Forms.RadioButton();
            this.ReadTab = new System.Windows.Forms.TabPage();
            this.ReadBtn = new System.Windows.Forms.Button();
            this.LastReadValueTP = new System.Windows.Forms.TextBox();
            this.ReadNewValuesCB = new System.Windows.Forms.RadioButton();
            this.DaysLbl = new System.Windows.Forms.Label();
            this.ToLbl = new System.Windows.Forms.Label();
            this.ToPick = new System.Windows.Forms.DateTimePicker();
            this.StartPick = new System.Windows.Forms.DateTimePicker();
            this.ReadLastTB = new System.Windows.Forms.TextBox();
            this.ReadFromRB = new System.Windows.Forms.RadioButton();
            this.ReadLastRB = new System.Windows.Forms.RadioButton();
            this.ReadAllRB = new System.Windows.Forms.RadioButton();
            this.TableGB = new System.Windows.Forms.GroupBox();
            this.TableNameLbl = new System.Windows.Forms.Label();
            this.TableCB = new System.Windows.Forms.ComboBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.EventLogTab = new System.Windows.Forms.TabPage();
            this.EventsList = new System.Windows.Forms.ListView();
            this.EventTimeCH = new System.Windows.Forms.ColumnHeader();
            this.EventDeviceNameCH = new System.Windows.Forms.ColumnHeader();
            this.EventDescriptionCH = new System.Windows.Forms.ColumnHeader();
            this.EventMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EventPauseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EventCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EventFollowLastMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.EventClear = new System.Windows.Forms.ToolStripMenuItem();
            this.SchedulesTab = new System.Windows.Forms.TabPage();
            this.Schedules = new System.Windows.Forms.ListView();
            this.ScheduleNameCH = new System.Windows.Forms.ColumnHeader();
            this.ScheduleRepeatModeCH = new System.Windows.Forms.ColumnHeader();
            this.ScheduleNextRunTimeCH = new System.Windows.Forms.ColumnHeader();
            this.LastRunTimeCH = new System.Windows.Forms.ColumnHeader();
            this.ScheduleProgressCH = new System.Windows.Forms.ColumnHeader();
            this.ScheduleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ScheduleAddMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleDeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ScheduleStartMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleStartAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleStopMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleStopAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleRunMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SchedulePropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TasksTab = new System.Windows.Forms.TabPage();
            this.TaskList = new System.Windows.Forms.ListView();
            this.TaskDescriptionCH = new System.Windows.Forms.ColumnHeader();
            this.TaskTargetCH = new System.Windows.Forms.ColumnHeader();
            this.TaskCreationTimeCH = new System.Windows.Forms.ColumnHeader();
            this.TaskClaimTimeCH = new System.Windows.Forms.ColumnHeader();
            this.TasksMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TasksPauseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TasksFollowLastMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TasksRemoveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TasksClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TraceTab = new System.Windows.Forms.TabPage();
            this.TraceView = new System.Windows.Forms.ListView();
            this.TimeCH = new System.Windows.Forms.ColumnHeader();
            this.LevelCH = new System.Windows.Forms.ColumnHeader();
            this.DescriptionCH = new System.Windows.Forms.ColumnHeader();
            this.TraceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TraceCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.TracePauseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TraceFollowLastMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.TraceClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TraceHexMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceProfilesTab = new System.Windows.Forms.TabPage();
            this.DeviceTemplateList = new System.Windows.Forms.ListView();
            this.ManufacturerCH = new System.Windows.Forms.ColumnHeader();
            this.ModelCH = new System.Windows.Forms.ColumnHeader();
            this.VersionCH = new System.Windows.Forms.ColumnHeader();
            this.PresetNameCH = new System.Windows.Forms.ColumnHeader();
            this.ProtocolCH = new System.Windows.Forms.ColumnHeader();
            this.TemplateCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceTemplateVersionCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceTemplateMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeviceTemplateNew = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceTemplateDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.AmiPanel.Panel1.SuspendLayout();
            this.AmiPanel.Panel2.SuspendLayout();
            this.AmiPanel.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.AssignedTab.SuspendLayout();
            this.DataCollectorMenu.SuspendLayout();
            this.UnAssignedTab.SuspendLayout();
            this.UnAssignedMenu.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.DeviceMenu.SuspendLayout();
            this.TablePanel.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.ValuesTab.SuspendLayout();
            this.TableTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableData)).BeginInit();
            this.TableTabs.SuspendLayout();
            this.ViewTab.SuspendLayout();
            this.ReadTab.SuspendLayout();
            this.TableGB.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.EventLogTab.SuspendLayout();
            this.EventMenu.SuspendLayout();
            this.SchedulesTab.SuspendLayout();
            this.ScheduleMenu.SuspendLayout();
            this.TasksTab.SuspendLayout();
            this.TasksMenu.SuspendLayout();
            this.TraceTab.SuspendLayout();
            this.TraceMenu.SuspendLayout();
            this.DeviceProfilesTab.SuspendLayout();
            this.DeviceTemplateMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AmiPanel
            // 
            this.AmiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AmiPanel.Location = new System.Drawing.Point(0, 0);
            this.AmiPanel.Name = "AmiPanel";
            // 
            // AmiPanel.Panel1
            // 
            this.AmiPanel.Panel1.Controls.Add(this.tabControl2);
            // 
            // AmiPanel.Panel2
            // 
            this.AmiPanel.Panel2.Controls.Add(this.splitContainer2);
            this.AmiPanel.Size = new System.Drawing.Size(838, 710);
            this.AmiPanel.SplitterDistance = 127;
            this.AmiPanel.TabIndex = 10;
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl2.Controls.Add(this.AssignedTab);
            this.tabControl2.Controls.Add(this.UnAssignedTab);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(127, 710);
            this.tabControl2.TabIndex = 0;
            // 
            // AssignedTab
            // 
            this.AssignedTab.Controls.Add(this.DCList);
            this.AssignedTab.Location = new System.Drawing.Point(4, 4);
            this.AssignedTab.Name = "AssignedTab";
            this.AssignedTab.Padding = new System.Windows.Forms.Padding(3);
            this.AssignedTab.Size = new System.Drawing.Size(119, 684);
            this.AssignedTab.TabIndex = 0;
            this.AssignedTab.Text = "Data Collectors";
            this.AssignedTab.UseVisualStyleBackColor = true;
            // 
            // DCList
            // 
            this.DCList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DcCH});
            this.DCList.ContextMenuStrip = this.DataCollectorMenu;
            this.DCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DCList.FullRowSelect = true;
            this.DCList.HideSelection = false;
            this.DCList.Location = new System.Drawing.Point(3, 3);
            this.DCList.Name = "DCList";
            this.DCList.Size = new System.Drawing.Size(113, 678);
            this.DCList.TabIndex = 10;
            this.DCList.UseCompatibleStateImageBehavior = false;
            this.DCList.View = System.Windows.Forms.View.Details;
            this.DCList.SelectedIndexChanged += new System.EventHandler(this.DCList_SelectedIndexChanged);
            this.DCList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // DcCH
            // 
            this.DcCH.Text = "DC Name";
            this.DcCH.Width = 111;
            // 
            // DataCollectorMenu
            // 
            this.DataCollectorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDeviceMenu,
            this.DeleteMenu,
            this.toolStripMenuItem3,
            this.PropertiesMenu,
            this.toolStripMenuItem4,
            this.AddCommandPromptMenu});
            this.DataCollectorMenu.Name = "ScheduleMenu";
            this.DataCollectorMenu.Size = new System.Drawing.Size(209, 104);
            this.DataCollectorMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DataCollectorMenu_Opening);
            // 
            // NewDeviceMenu
            // 
            this.NewDeviceMenu.Name = "NewDeviceMenu";
            this.NewDeviceMenu.Size = new System.Drawing.Size(208, 22);
            this.NewDeviceMenu.Text = "New Device...";
            this.NewDeviceMenu.Click += new System.EventHandler(this.NewDeviceMenu_Click);
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Name = "DeleteMenu";
            this.DeleteMenu.Size = new System.Drawing.Size(208, 22);
            this.DeleteMenu.Text = "Delete";
            this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(205, 6);
            // 
            // PropertiesMenu
            // 
            this.PropertiesMenu.Name = "PropertiesMenu";
            this.PropertiesMenu.Size = new System.Drawing.Size(208, 22);
            this.PropertiesMenu.Text = "Properties...";
            this.PropertiesMenu.Click += new System.EventHandler(this.PropertiesMenu_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // AddCommandPromptMenu
            // 
            this.AddCommandPromptMenu.Name = "AddCommandPromptMenu";
            this.AddCommandPromptMenu.Size = new System.Drawing.Size(208, 22);
            this.AddCommandPromptMenu.Text = "Add Command Prompt...";
            this.AddCommandPromptMenu.Click += new System.EventHandler(this.AddCommandPromptMenu_Click);
            // 
            // UnAssignedTab
            // 
            this.UnAssignedTab.Controls.Add(this.UnassignedDCList);
            this.UnAssignedTab.Location = new System.Drawing.Point(4, 4);
            this.UnAssignedTab.Name = "UnAssignedTab";
            this.UnAssignedTab.Padding = new System.Windows.Forms.Padding(3);
            this.UnAssignedTab.Size = new System.Drawing.Size(119, 684);
            this.UnAssignedTab.TabIndex = 1;
            this.UnAssignedTab.Text = "Un Assigned";
            this.UnAssignedTab.UseVisualStyleBackColor = true;
            // 
            // UnassignedDCList
            // 
            this.UnassignedDCList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MACAddressLbl,
            this.IPAddressCH});
            this.UnassignedDCList.ContextMenuStrip = this.UnAssignedMenu;
            this.UnassignedDCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnassignedDCList.FullRowSelect = true;
            this.UnassignedDCList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.UnassignedDCList.HideSelection = false;
            this.UnassignedDCList.Location = new System.Drawing.Point(3, 3);
            this.UnassignedDCList.MultiSelect = false;
            this.UnassignedDCList.Name = "UnassignedDCList";
            this.UnassignedDCList.Size = new System.Drawing.Size(113, 678);
            this.UnassignedDCList.TabIndex = 7;
            this.UnassignedDCList.UseCompatibleStateImageBehavior = false;
            this.UnassignedDCList.View = System.Windows.Forms.View.Details;
            this.UnassignedDCList.SelectedIndexChanged += new System.EventHandler(this.UnassignedDCList_SelectedIndexChanged);
            this.UnassignedDCList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // MACAddressLbl
            // 
            this.MACAddressLbl.Text = "MAC Address:";
            // 
            // IPAddressCH
            // 
            this.IPAddressCH.Text = "IP Address";
            this.IPAddressCH.Width = 102;
            // 
            // UnAssignedMenu
            // 
            this.UnAssignedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AssignMenu,
            this.RemoveUnAssignedMenu});
            this.UnAssignedMenu.Name = "contextMenuStrip1";
            this.UnAssignedMenu.Size = new System.Drawing.Size(118, 48);
            this.UnAssignedMenu.Opening += new System.ComponentModel.CancelEventHandler(this.UnAssignedMenu_Opening);
            // 
            // AssignMenu
            // 
            this.AssignMenu.Name = "AssignMenu";
            this.AssignMenu.Size = new System.Drawing.Size(117, 22);
            this.AssignMenu.Text = "Assign";
            this.AssignMenu.Click += new System.EventHandler(this.AssignMenu_Click);
            // 
            // RemoveUnAssignedMenu
            // 
            this.RemoveUnAssignedMenu.Name = "RemoveUnAssignedMenu";
            this.RemoveUnAssignedMenu.Size = new System.Drawing.Size(117, 22);
            this.RemoveUnAssignedMenu.Text = "Remove";
            this.RemoveUnAssignedMenu.Click += new System.EventHandler(this.RemoveUnAssignedMenu_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.TabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(707, 710);
            this.splitContainer2.SplitterDistance = 468;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DevicesList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TablePanel);
            this.splitContainer1.Size = new System.Drawing.Size(707, 468);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 0;
            // 
            // DevicesList
            // 
            this.DevicesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DeviceNameCH,
            this.DeviceStatusCH,
            this.DeviceTypeCH});
            this.DevicesList.ContextMenuStrip = this.DeviceMenu;
            this.DevicesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevicesList.FullRowSelect = true;
            this.DevicesList.HideSelection = false;
            this.DevicesList.Location = new System.Drawing.Point(0, 0);
            this.DevicesList.Name = "DevicesList";
            this.DevicesList.Size = new System.Drawing.Size(235, 468);
            this.DevicesList.TabIndex = 9;
            this.DevicesList.UseCompatibleStateImageBehavior = false;
            this.DevicesList.View = System.Windows.Forms.View.Details;
            this.DevicesList.SelectedIndexChanged += new System.EventHandler(this.DevicesList_SelectedIndexChanged);
            this.DevicesList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // DeviceNameCH
            // 
            this.DeviceNameCH.Text = "Device Name";
            this.DeviceNameCH.Width = 111;
            // 
            // DeviceStatusCH
            // 
            this.DeviceStatusCH.Text = "Status";
            this.DeviceStatusCH.Width = 76;
            // 
            // DeviceTypeCH
            // 
            this.DeviceTypeCH.Text = "Type";
            this.DeviceTypeCH.Width = 69;
            // 
            // DeviceMenu
            // 
            this.DeviceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeviceNewDeviceMenu,
            this.DeviceDeleteMenu,
            this.DeviceReadMenu,
            this.toolStripSeparator13,
            this.DeviceStartMonitorMenu,
            this.DeviceStopMonitorMenu,
            this.toolStripSeparator14,
            this.DevicePropertiesMenu});
            this.DeviceMenu.Name = "ScheduleMenu";
            this.DeviceMenu.Size = new System.Drawing.Size(146, 148);
            this.DeviceMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DeviceMenu_Opening);
            // 
            // DeviceNewDeviceMenu
            // 
            this.DeviceNewDeviceMenu.Name = "DeviceNewDeviceMenu";
            this.DeviceNewDeviceMenu.Size = new System.Drawing.Size(145, 22);
            this.DeviceNewDeviceMenu.Text = "New Device...";
            this.DeviceNewDeviceMenu.Click += new System.EventHandler(this.NewDeviceMenu_Click);
            // 
            // DeviceDeleteMenu
            // 
            this.DeviceDeleteMenu.Name = "DeviceDeleteMenu";
            this.DeviceDeleteMenu.Size = new System.Drawing.Size(145, 22);
            this.DeviceDeleteMenu.Text = "Delete";
            this.DeviceDeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // DeviceReadMenu
            // 
            this.DeviceReadMenu.Name = "DeviceReadMenu";
            this.DeviceReadMenu.Size = new System.Drawing.Size(145, 22);
            this.DeviceReadMenu.Text = "Read";
            this.DeviceReadMenu.Click += new System.EventHandler(this.ReadMenu_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(142, 6);
            // 
            // DeviceStartMonitorMenu
            // 
            this.DeviceStartMonitorMenu.Name = "DeviceStartMonitorMenu";
            this.DeviceStartMonitorMenu.Size = new System.Drawing.Size(145, 22);
            this.DeviceStartMonitorMenu.Text = "Start Monitor";
            this.DeviceStartMonitorMenu.Click += new System.EventHandler(this.StartMonitorMenu_Click);
            // 
            // DeviceStopMonitorMenu
            // 
            this.DeviceStopMonitorMenu.Name = "DeviceStopMonitorMenu";
            this.DeviceStopMonitorMenu.Size = new System.Drawing.Size(145, 22);
            this.DeviceStopMonitorMenu.Text = "Stop Monitor";
            this.DeviceStopMonitorMenu.Click += new System.EventHandler(this.StopMonitorMnu_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(142, 6);
            // 
            // DevicePropertiesMenu
            // 
            this.DevicePropertiesMenu.Name = "DevicePropertiesMenu";
            this.DevicePropertiesMenu.Size = new System.Drawing.Size(145, 22);
            this.DevicePropertiesMenu.Text = "Properties...";
            this.DevicePropertiesMenu.Click += new System.EventHandler(this.PropertiesMenu_Click);
            // 
            // TablePanel
            // 
            this.TablePanel.Controls.Add(this.tabControl3);
            this.TablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablePanel.Location = new System.Drawing.Point(0, 0);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.Size = new System.Drawing.Size(468, 468);
            this.TablePanel.TabIndex = 12;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.ValuesTab);
            this.tabControl3.Controls.Add(this.TableTab);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(468, 468);
            this.tabControl3.TabIndex = 0;
            // 
            // ValuesTab
            // 
            this.ValuesTab.Controls.Add(this.PropertyList);
            this.ValuesTab.Location = new System.Drawing.Point(4, 22);
            this.ValuesTab.Name = "ValuesTab";
            this.ValuesTab.Padding = new System.Windows.Forms.Padding(3);
            this.ValuesTab.Size = new System.Drawing.Size(460, 442);
            this.ValuesTab.TabIndex = 0;
            this.ValuesTab.Text = "Values";
            this.ValuesTab.UseVisualStyleBackColor = true;
            // 
            // PropertyList
            // 
            this.PropertyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyNameCH,
            this.PropertyTypeCH,
            this.PropertyValueCH,
            this.TimestampCH});
            this.PropertyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyList.FullRowSelect = true;
            this.PropertyList.Location = new System.Drawing.Point(3, 3);
            this.PropertyList.Name = "PropertyList";
            this.PropertyList.Size = new System.Drawing.Size(454, 436);
            this.PropertyList.TabIndex = 19;
            this.PropertyList.UseCompatibleStateImageBehavior = false;
            this.PropertyList.View = System.Windows.Forms.View.Details;
            this.PropertyList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // PropertyNameCH
            // 
            this.PropertyNameCH.Text = "Property Name";
            this.PropertyNameCH.Width = 127;
            // 
            // PropertyTypeCH
            // 
            this.PropertyTypeCH.Text = "Type";
            this.PropertyTypeCH.Width = 85;
            // 
            // PropertyValueCH
            // 
            this.PropertyValueCH.Text = "Value";
            // 
            // TimestampCH
            // 
            this.TimestampCH.Text = "Timestamp";
            // 
            // TableTab
            // 
            this.TableTab.Controls.Add(this.TableData);
            this.TableTab.Controls.Add(this.TableTabs);
            this.TableTab.Controls.Add(this.TableGB);
            this.TableTab.Location = new System.Drawing.Point(4, 22);
            this.TableTab.Name = "TableTab";
            this.TableTab.Padding = new System.Windows.Forms.Padding(3);
            this.TableTab.Size = new System.Drawing.Size(460, 442);
            this.TableTab.TabIndex = 1;
            this.TableTab.Text = "Tables";
            this.TableTab.UseVisualStyleBackColor = true;
            // 
            // TableData
            // 
            this.TableData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableData.Location = new System.Drawing.Point(3, 193);
            this.TableData.Name = "TableData";
            this.TableData.Size = new System.Drawing.Size(454, 246);
            this.TableData.TabIndex = 27;
            // 
            // TableTabs
            // 
            this.TableTabs.Controls.Add(this.ViewTab);
            this.TableTabs.Controls.Add(this.ReadTab);
            this.TableTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableTabs.Location = new System.Drawing.Point(3, 56);
            this.TableTabs.Name = "TableTabs";
            this.TableTabs.SelectedIndex = 0;
            this.TableTabs.Size = new System.Drawing.Size(454, 137);
            this.TableTabs.TabIndex = 26;
            // 
            // ViewTab
            // 
            this.ViewTab.Controls.Add(this.TableRowCount);
            this.ViewTab.Controls.Add(this.label1);
            this.ViewTab.Controls.Add(this.CountTB);
            this.ViewTab.Controls.Add(this.IndexTB);
            this.ViewTab.Controls.Add(this.UpdateBtn);
            this.ViewTab.Controls.Add(this.ShowByIndexlCB);
            this.ViewTab.Controls.Add(this.ShowAllCB);
            this.ViewTab.Location = new System.Drawing.Point(4, 22);
            this.ViewTab.Name = "ViewTab";
            this.ViewTab.Padding = new System.Windows.Forms.Padding(3);
            this.ViewTab.Size = new System.Drawing.Size(446, 111);
            this.ViewTab.TabIndex = 0;
            this.ViewTab.Text = "View";
            this.ViewTab.UseVisualStyleBackColor = true;
            // 
            // TableRowCount
            // 
            this.TableRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TableRowCount.AutoSize = true;
            this.TableRowCount.Location = new System.Drawing.Point(425, 73);
            this.TableRowCount.Name = "TableRowCount";
            this.TableRowCount.Size = new System.Drawing.Size(13, 13);
            this.TableRowCount.TabIndex = 35;
            this.TableRowCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Count:";
            // 
            // CountTB
            // 
            this.CountTB.Location = new System.Drawing.Point(103, 69);
            this.CountTB.Name = "CountTB";
            this.CountTB.Size = new System.Drawing.Size(64, 20);
            this.CountTB.TabIndex = 31;
            this.CountTB.Text = "1000";
            // 
            // IndexTB
            // 
            this.IndexTB.Location = new System.Drawing.Point(103, 40);
            this.IndexTB.Name = "IndexTB";
            this.IndexTB.Size = new System.Drawing.Size(64, 20);
            this.IndexTB.TabIndex = 29;
            this.IndexTB.Text = "0";
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBtn.Location = new System.Drawing.Point(363, 16);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(75, 23);
            this.UpdateBtn.TabIndex = 23;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // ShowByIndexlCB
            // 
            this.ShowByIndexlCB.Location = new System.Drawing.Point(15, 40);
            this.ShowByIndexlCB.Name = "ShowByIndexlCB";
            this.ShowByIndexlCB.Size = new System.Drawing.Size(80, 16);
            this.ShowByIndexlCB.TabIndex = 28;
            this.ShowByIndexlCB.Text = "By Index:";
            this.ShowByIndexlCB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ShowAllCB
            // 
            this.ShowAllCB.Checked = true;
            this.ShowAllCB.Location = new System.Drawing.Point(15, 16);
            this.ShowAllCB.Name = "ShowAllCB";
            this.ShowAllCB.Size = new System.Drawing.Size(80, 16);
            this.ShowAllCB.TabIndex = 27;
            this.ShowAllCB.TabStop = true;
            this.ShowAllCB.Text = "Show All";
            // 
            // ReadTab
            // 
            this.ReadTab.Controls.Add(this.ReadBtn);
            this.ReadTab.Controls.Add(this.LastReadValueTP);
            this.ReadTab.Controls.Add(this.ReadNewValuesCB);
            this.ReadTab.Controls.Add(this.DaysLbl);
            this.ReadTab.Controls.Add(this.ToLbl);
            this.ReadTab.Controls.Add(this.ToPick);
            this.ReadTab.Controls.Add(this.StartPick);
            this.ReadTab.Controls.Add(this.ReadLastTB);
            this.ReadTab.Controls.Add(this.ReadFromRB);
            this.ReadTab.Controls.Add(this.ReadLastRB);
            this.ReadTab.Controls.Add(this.ReadAllRB);
            this.ReadTab.Location = new System.Drawing.Point(4, 22);
            this.ReadTab.Name = "ReadTab";
            this.ReadTab.Padding = new System.Windows.Forms.Padding(3);
            this.ReadTab.Size = new System.Drawing.Size(446, 111);
            this.ReadTab.TabIndex = 1;
            this.ReadTab.Text = "Read";
            this.ReadTab.UseVisualStyleBackColor = true;
            // 
            // ReadBtn
            // 
            this.ReadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadBtn.Location = new System.Drawing.Point(360, 20);
            this.ReadBtn.Name = "ReadBtn";
            this.ReadBtn.Size = new System.Drawing.Size(75, 23);
            this.ReadBtn.TabIndex = 34;
            this.ReadBtn.Text = "Read";
            this.ReadBtn.UseVisualStyleBackColor = true;
            this.ReadBtn.Click += new System.EventHandler(this.ReadBtn_Click);
            // 
            // LastReadValueTP
            // 
            this.LastReadValueTP.Location = new System.Drawing.Point(240, 81);
            this.LastReadValueTP.Name = "LastReadValueTP";
            this.LastReadValueTP.ReadOnly = true;
            this.LastReadValueTP.Size = new System.Drawing.Size(112, 20);
            this.LastReadValueTP.TabIndex = 32;
            // 
            // ReadNewValuesCB
            // 
            this.ReadNewValuesCB.Location = new System.Drawing.Point(8, 77);
            this.ReadNewValuesCB.Name = "ReadNewValuesCB";
            this.ReadNewValuesCB.Size = new System.Drawing.Size(200, 19);
            this.ReadNewValuesCB.TabIndex = 31;
            this.ReadNewValuesCB.Text = "Read New Values";
            this.ReadNewValuesCB.CheckedChanged += new System.EventHandler(this.ReadAllRB_CheckedChanged);
            // 
            // DaysLbl
            // 
            this.DaysLbl.Location = new System.Drawing.Point(176, 31);
            this.DaysLbl.Name = "DaysLbl";
            this.DaysLbl.Size = new System.Drawing.Size(72, 16);
            this.DaysLbl.TabIndex = 30;
            this.DaysLbl.Text = "Days";
            // 
            // ToLbl
            // 
            this.ToLbl.AutoSize = true;
            this.ToLbl.Location = new System.Drawing.Point(215, 60);
            this.ToLbl.Name = "ToLbl";
            this.ToLbl.Size = new System.Drawing.Size(20, 13);
            this.ToLbl.TabIndex = 29;
            this.ToLbl.Text = "To";
            // 
            // ToPick
            // 
            this.ToPick.Checked = false;
            this.ToPick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ToPick.Location = new System.Drawing.Point(240, 55);
            this.ToPick.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ToPick.Name = "ToPick";
            this.ToPick.ShowCheckBox = true;
            this.ToPick.Size = new System.Drawing.Size(112, 20);
            this.ToPick.TabIndex = 28;
            // 
            // StartPick
            // 
            this.StartPick.Checked = false;
            this.StartPick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartPick.Location = new System.Drawing.Point(96, 55);
            this.StartPick.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.StartPick.Name = "StartPick";
            this.StartPick.ShowCheckBox = true;
            this.StartPick.Size = new System.Drawing.Size(112, 20);
            this.StartPick.TabIndex = 27;
            // 
            // ReadLastTB
            // 
            this.ReadLastTB.Location = new System.Drawing.Point(96, 31);
            this.ReadLastTB.Name = "ReadLastTB";
            this.ReadLastTB.Size = new System.Drawing.Size(64, 20);
            this.ReadLastTB.TabIndex = 26;
            this.ReadLastTB.Text = "1";
            // 
            // ReadFromRB
            // 
            this.ReadFromRB.Location = new System.Drawing.Point(8, 55);
            this.ReadFromRB.Name = "ReadFromRB";
            this.ReadFromRB.Size = new System.Drawing.Size(80, 16);
            this.ReadFromRB.TabIndex = 25;
            this.ReadFromRB.Text = "Read From";
            this.ReadFromRB.CheckedChanged += new System.EventHandler(this.ReadAllRB_CheckedChanged);
            // 
            // ReadLastRB
            // 
            this.ReadLastRB.Location = new System.Drawing.Point(8, 31);
            this.ReadLastRB.Name = "ReadLastRB";
            this.ReadLastRB.Size = new System.Drawing.Size(80, 16);
            this.ReadLastRB.TabIndex = 24;
            this.ReadLastRB.Text = "Read last";
            this.ReadLastRB.CheckedChanged += new System.EventHandler(this.ReadAllRB_CheckedChanged);
            // 
            // ReadAllRB
            // 
            this.ReadAllRB.Checked = true;
            this.ReadAllRB.Location = new System.Drawing.Point(8, 7);
            this.ReadAllRB.Name = "ReadAllRB";
            this.ReadAllRB.Size = new System.Drawing.Size(80, 16);
            this.ReadAllRB.TabIndex = 23;
            this.ReadAllRB.TabStop = true;
            this.ReadAllRB.Text = "Read All";
            this.ReadAllRB.CheckedChanged += new System.EventHandler(this.ReadAllRB_CheckedChanged);
            // 
            // TableGB
            // 
            this.TableGB.Controls.Add(this.TableNameLbl);
            this.TableGB.Controls.Add(this.TableCB);
            this.TableGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableGB.Location = new System.Drawing.Point(3, 3);
            this.TableGB.Name = "TableGB";
            this.TableGB.Size = new System.Drawing.Size(454, 53);
            this.TableGB.TabIndex = 22;
            this.TableGB.TabStop = false;
            this.TableGB.Text = "Table:";
            this.TableGB.Enter += new System.EventHandler(this.OnEnter);
            // 
            // TableNameLbl
            // 
            this.TableNameLbl.AutoSize = true;
            this.TableNameLbl.Location = new System.Drawing.Point(6, 22);
            this.TableNameLbl.Name = "TableNameLbl";
            this.TableNameLbl.Size = new System.Drawing.Size(38, 13);
            this.TableNameLbl.TabIndex = 21;
            this.TableNameLbl.Text = "Name:";
            // 
            // TableCB
            // 
            this.TableCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TableCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableCB.FormattingEnabled = true;
            this.TableCB.Location = new System.Drawing.Point(50, 19);
            this.TableCB.Name = "TableCB";
            this.TableCB.Size = new System.Drawing.Size(300, 21);
            this.TableCB.TabIndex = 20;
            this.TableCB.SelectedIndexChanged += new System.EventHandler(this.TableCB_SelectedIndexChanged);
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.EventLogTab);
            this.TabControl1.Controls.Add(this.SchedulesTab);
            this.TabControl1.Controls.Add(this.TasksTab);
            this.TabControl1.Controls.Add(this.TraceTab);
            this.TabControl1.Controls.Add(this.DeviceProfilesTab);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.ImageList = this.imageList1;
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(707, 238);
            this.TabControl1.TabIndex = 40;
            this.TabControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseUp);
            // 
            // EventLogTab
            // 
            this.EventLogTab.Controls.Add(this.EventsList);
            this.EventLogTab.Location = new System.Drawing.Point(4, 23);
            this.EventLogTab.Name = "EventLogTab";
            this.EventLogTab.Padding = new System.Windows.Forms.Padding(3);
            this.EventLogTab.Size = new System.Drawing.Size(699, 211);
            this.EventLogTab.TabIndex = 0;
            this.EventLogTab.Text = "Event log";
            this.EventLogTab.UseVisualStyleBackColor = true;
            // 
            // EventsList
            // 
            this.EventsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EventTimeCH,
            this.EventDeviceNameCH,
            this.EventDescriptionCH});
            this.EventsList.ContextMenuStrip = this.EventMenu;
            this.EventsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventsList.FullRowSelect = true;
            this.EventsList.Location = new System.Drawing.Point(3, 3);
            this.EventsList.Name = "EventsList";
            this.EventsList.Size = new System.Drawing.Size(693, 205);
            this.EventsList.TabIndex = 3;
            this.EventsList.UseCompatibleStateImageBehavior = false;
            this.EventsList.View = System.Windows.Forms.View.Details;
            this.EventsList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // EventTimeCH
            // 
            this.EventTimeCH.Text = "Time";
            // 
            // EventDeviceNameCH
            // 
            this.EventDeviceNameCH.Text = "Name";
            // 
            // EventDescriptionCH
            // 
            this.EventDescriptionCH.Text = "Description";
            // 
            // EventMenu
            // 
            this.EventMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EventPauseMenu,
            this.EventCopyMenu,
            this.EventFollowLastMenu,
            this.toolStripMenuItem2,
            this.EventClear});
            this.EventMenu.Name = "contextMenuStrip1";
            this.EventMenu.Size = new System.Drawing.Size(134, 98);
            // 
            // EventPauseMenu
            // 
            this.EventPauseMenu.Name = "EventPauseMenu";
            this.EventPauseMenu.Size = new System.Drawing.Size(133, 22);
            this.EventPauseMenu.Text = "Pause";
            this.EventPauseMenu.Click += new System.EventHandler(this.EventPauseMenu_Click);
            // 
            // EventCopyMenu
            // 
            this.EventCopyMenu.Name = "EventCopyMenu";
            this.EventCopyMenu.Size = new System.Drawing.Size(133, 22);
            this.EventCopyMenu.Text = "Copy";
            this.EventCopyMenu.Click += new System.EventHandler(this.EventCopyMenu_Click);
            // 
            // EventFollowLastMenu
            // 
            this.EventFollowLastMenu.Checked = true;
            this.EventFollowLastMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EventFollowLastMenu.Name = "EventFollowLastMenu";
            this.EventFollowLastMenu.Size = new System.Drawing.Size(133, 22);
            this.EventFollowLastMenu.Text = "Follow Last";
            this.EventFollowLastMenu.Click += new System.EventHandler(this.EventFollowLastMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(130, 6);
            // 
            // EventClear
            // 
            this.EventClear.Name = "EventClear";
            this.EventClear.Size = new System.Drawing.Size(133, 22);
            this.EventClear.Text = "Clear";
            this.EventClear.Click += new System.EventHandler(this.EventClear_Click);
            // 
            // SchedulesTab
            // 
            this.SchedulesTab.Controls.Add(this.Schedules);
            this.SchedulesTab.Location = new System.Drawing.Point(4, 23);
            this.SchedulesTab.Name = "SchedulesTab";
            this.SchedulesTab.Padding = new System.Windows.Forms.Padding(3);
            this.SchedulesTab.Size = new System.Drawing.Size(699, 211);
            this.SchedulesTab.TabIndex = 1;
            this.SchedulesTab.Text = "Schedules";
            this.SchedulesTab.UseVisualStyleBackColor = true;
            // 
            // Schedules
            // 
            this.Schedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScheduleNameCH,
            this.ScheduleRepeatModeCH,
            this.ScheduleNextRunTimeCH,
            this.LastRunTimeCH,
            this.ScheduleProgressCH});
            this.Schedules.ContextMenuStrip = this.ScheduleMenu;
            this.Schedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Schedules.FullRowSelect = true;
            this.Schedules.HideSelection = false;
            this.Schedules.Location = new System.Drawing.Point(3, 3);
            this.Schedules.Name = "Schedules";
            this.Schedules.Size = new System.Drawing.Size(693, 205);
            this.Schedules.TabIndex = 4;
            this.Schedules.UseCompatibleStateImageBehavior = false;
            this.Schedules.View = System.Windows.Forms.View.Details;
            this.Schedules.SelectedIndexChanged += new System.EventHandler(this.Schedules_SelectedIndexChanged);
            this.Schedules.DoubleClick += new System.EventHandler(this.SchedulePropertiesMenu_Click);
            this.Schedules.Enter += new System.EventHandler(this.OnEnter);
            // 
            // ScheduleNameCH
            // 
            this.ScheduleNameCH.Text = "Name";
            // 
            // ScheduleRepeatModeCH
            // 
            this.ScheduleRepeatModeCH.Text = "Repeat Mode";
            this.ScheduleRepeatModeCH.Width = 113;
            // 
            // ScheduleNextRunTimeCH
            // 
            this.ScheduleNextRunTimeCH.Text = "Next Run Time";
            this.ScheduleNextRunTimeCH.Width = 114;
            // 
            // LastRunTimeCH
            // 
            this.LastRunTimeCH.Text = "Last Run Time";
            this.LastRunTimeCH.Width = 97;
            // 
            // ScheduleProgressCH
            // 
            this.ScheduleProgressCH.Text = "Progress";
            this.ScheduleProgressCH.Width = 75;
            // 
            // ScheduleMenu
            // 
            this.ScheduleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScheduleAddMenu,
            this.ScheduleDeleteMenu,
            this.toolStripSeparator1,
            this.ScheduleStartMenu,
            this.ScheduleStartAllMenu,
            this.ScheduleStopMenu,
            this.ScheduleStopAllMenu,
            this.ScheduleRunMenu,
            this.toolStripSeparator2,
            this.SchedulePropertiesMenu});
            this.ScheduleMenu.Name = "ScheduleMenu";
            this.ScheduleMenu.Size = new System.Drawing.Size(137, 192);
            this.ScheduleMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ScheduleMenu_Opening);
            // 
            // ScheduleAddMenu
            // 
            this.ScheduleAddMenu.Name = "ScheduleAddMenu";
            this.ScheduleAddMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleAddMenu.Text = "Add";
            this.ScheduleAddMenu.Click += new System.EventHandler(this.NewScheduleMenu_Click);
            // 
            // ScheduleDeleteMenu
            // 
            this.ScheduleDeleteMenu.Name = "ScheduleDeleteMenu";
            this.ScheduleDeleteMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ScheduleDeleteMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleDeleteMenu.Text = "Delete";
            this.ScheduleDeleteMenu.Click += new System.EventHandler(this.ScheduleDeleteMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // ScheduleStartMenu
            // 
            this.ScheduleStartMenu.Name = "ScheduleStartMenu";
            this.ScheduleStartMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleStartMenu.Text = "Start";
            this.ScheduleStartMenu.Click += new System.EventHandler(this.ScheduleStartMenu_Click);
            // 
            // ScheduleStartAllMenu
            // 
            this.ScheduleStartAllMenu.Name = "ScheduleStartAllMenu";
            this.ScheduleStartAllMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleStartAllMenu.Text = "Start All";
            this.ScheduleStartAllMenu.Click += new System.EventHandler(this.ScheduleStartAllMenu_Click);
            // 
            // ScheduleStopMenu
            // 
            this.ScheduleStopMenu.Name = "ScheduleStopMenu";
            this.ScheduleStopMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleStopMenu.Text = "Stop";
            this.ScheduleStopMenu.Click += new System.EventHandler(this.ScheduleStopMenu_Click);
            // 
            // ScheduleStopAllMenu
            // 
            this.ScheduleStopAllMenu.Name = "ScheduleStopAllMenu";
            this.ScheduleStopAllMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleStopAllMenu.Text = "Stop All";
            this.ScheduleStopAllMenu.Click += new System.EventHandler(this.ScheduleStopAllMenu_Click);
            // 
            // ScheduleRunMenu
            // 
            this.ScheduleRunMenu.Name = "ScheduleRunMenu";
            this.ScheduleRunMenu.Size = new System.Drawing.Size(136, 22);
            this.ScheduleRunMenu.Text = "Run";
            this.ScheduleRunMenu.Click += new System.EventHandler(this.ScheduleRunMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // SchedulePropertiesMenu
            // 
            this.SchedulePropertiesMenu.Name = "SchedulePropertiesMenu";
            this.SchedulePropertiesMenu.Size = new System.Drawing.Size(136, 22);
            this.SchedulePropertiesMenu.Text = "Properties...";
            this.SchedulePropertiesMenu.Click += new System.EventHandler(this.SchedulePropertiesMenu_Click);
            // 
            // TasksTab
            // 
            this.TasksTab.Controls.Add(this.TaskList);
            this.TasksTab.Location = new System.Drawing.Point(4, 23);
            this.TasksTab.Name = "TasksTab";
            this.TasksTab.Padding = new System.Windows.Forms.Padding(3);
            this.TasksTab.Size = new System.Drawing.Size(699, 211);
            this.TasksTab.TabIndex = 3;
            this.TasksTab.Text = "Tasks";
            this.TasksTab.UseVisualStyleBackColor = true;
            // 
            // TaskList
            // 
            this.TaskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskDescriptionCH,
            this.TaskTargetCH,
            this.TaskCreationTimeCH,
            this.TaskClaimTimeCH});
            this.TaskList.ContextMenuStrip = this.TasksMenu;
            this.TaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskList.FullRowSelect = true;
            this.TaskList.Location = new System.Drawing.Point(3, 3);
            this.TaskList.Name = "TaskList";
            this.TaskList.Size = new System.Drawing.Size(693, 205);
            this.TaskList.TabIndex = 4;
            this.TaskList.UseCompatibleStateImageBehavior = false;
            this.TaskList.View = System.Windows.Forms.View.Details;
            this.TaskList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // TaskDescriptionCH
            // 
            this.TaskDescriptionCH.Text = "Description";
            this.TaskDescriptionCH.Width = 149;
            // 
            // TaskTargetCH
            // 
            this.TaskTargetCH.Text = "Target";
            this.TaskTargetCH.Width = 105;
            // 
            // TaskCreationTimeCH
            // 
            this.TaskCreationTimeCH.Text = "Creation Time";
            this.TaskCreationTimeCH.Width = 98;
            // 
            // TaskClaimTimeCH
            // 
            this.TaskClaimTimeCH.Text = "Claim Time";
            // 
            // TasksMenu
            // 
            this.TasksMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TasksPauseMenu,
            this.TasksFollowLastMenu,
            this.TasksRemoveMenu,
            this.toolStripSeparator3,
            this.TasksClearMenu});
            this.TasksMenu.Name = "contextMenuStrip1";
            this.TasksMenu.Size = new System.Drawing.Size(134, 98);
            // 
            // TasksPauseMenu
            // 
            this.TasksPauseMenu.Name = "TasksPauseMenu";
            this.TasksPauseMenu.Size = new System.Drawing.Size(133, 22);
            this.TasksPauseMenu.Text = "Pause";
            this.TasksPauseMenu.Click += new System.EventHandler(this.TasksPauseMenu_Click);
            // 
            // TasksFollowLastMenu
            // 
            this.TasksFollowLastMenu.Checked = true;
            this.TasksFollowLastMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TasksFollowLastMenu.Name = "TasksFollowLastMenu";
            this.TasksFollowLastMenu.Size = new System.Drawing.Size(133, 22);
            this.TasksFollowLastMenu.Text = "Follow Last";
            this.TasksFollowLastMenu.Click += new System.EventHandler(this.TasksFollowLastMenu_Click);
            // 
            // TasksRemoveMenu
            // 
            this.TasksRemoveMenu.Name = "TasksRemoveMenu";
            this.TasksRemoveMenu.Size = new System.Drawing.Size(133, 22);
            this.TasksRemoveMenu.Text = "Remove";
            this.TasksRemoveMenu.Click += new System.EventHandler(this.TasksRemoveMenu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // TasksClearMenu
            // 
            this.TasksClearMenu.Name = "TasksClearMenu";
            this.TasksClearMenu.Size = new System.Drawing.Size(133, 22);
            this.TasksClearMenu.Text = "Clear";
            this.TasksClearMenu.Click += new System.EventHandler(this.TasksClearMenu_Click);
            // 
            // TraceTab
            // 
            this.TraceTab.Controls.Add(this.TraceView);
            this.TraceTab.Location = new System.Drawing.Point(4, 23);
            this.TraceTab.Name = "TraceTab";
            this.TraceTab.Padding = new System.Windows.Forms.Padding(3);
            this.TraceTab.Size = new System.Drawing.Size(699, 211);
            this.TraceTab.TabIndex = 2;
            this.TraceTab.Text = "Trace";
            this.TraceTab.UseVisualStyleBackColor = true;
            // 
            // TraceView
            // 
            this.TraceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeCH,
            this.LevelCH,
            this.DescriptionCH});
            this.TraceView.ContextMenuStrip = this.TraceMenu;
            this.TraceView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceView.FullRowSelect = true;
            this.TraceView.Location = new System.Drawing.Point(3, 3);
            this.TraceView.Name = "TraceView";
            this.TraceView.Size = new System.Drawing.Size(693, 205);
            this.TraceView.TabIndex = 3;
            this.TraceView.UseCompatibleStateImageBehavior = false;
            this.TraceView.View = System.Windows.Forms.View.Details;
            this.TraceView.VirtualMode = true;
            this.TraceView.Enter += new System.EventHandler(this.OnEnter);
            this.TraceView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.TraceView_RetrieveVirtualItem);
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
            this.toolStripMenuItem1,
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 6);
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
            // DeviceProfilesTab
            // 
            this.DeviceProfilesTab.Controls.Add(this.DeviceTemplateList);
            this.DeviceProfilesTab.Location = new System.Drawing.Point(4, 23);
            this.DeviceProfilesTab.Name = "DeviceProfilesTab";
            this.DeviceProfilesTab.Padding = new System.Windows.Forms.Padding(3);
            this.DeviceProfilesTab.Size = new System.Drawing.Size(699, 211);
            this.DeviceProfilesTab.TabIndex = 4;
            this.DeviceProfilesTab.Text = "Device Profiles";
            this.DeviceProfilesTab.UseVisualStyleBackColor = true;
            // 
            // DeviceTemplateList
            // 
            this.DeviceTemplateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ManufacturerCH,
            this.ModelCH,
            this.VersionCH,
            this.PresetNameCH,
            this.ProtocolCH,
            this.TemplateCH,
            this.DeviceTemplateVersionCH});
            this.DeviceTemplateList.ContextMenuStrip = this.DeviceTemplateMenu;
            this.DeviceTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceTemplateList.FullRowSelect = true;
            this.DeviceTemplateList.HideSelection = false;
            this.DeviceTemplateList.Location = new System.Drawing.Point(3, 3);
            this.DeviceTemplateList.Name = "DeviceTemplateList";
            this.DeviceTemplateList.Size = new System.Drawing.Size(693, 205);
            this.DeviceTemplateList.TabIndex = 5;
            this.DeviceTemplateList.UseCompatibleStateImageBehavior = false;
            this.DeviceTemplateList.View = System.Windows.Forms.View.Details;
            this.DeviceTemplateList.Enter += new System.EventHandler(this.OnEnter);
            // 
            // ManufacturerCH
            // 
            this.ManufacturerCH.Text = "Manufacturer";
            // 
            // ModelCH
            // 
            this.ModelCH.Text = "Model";
            this.ModelCH.Width = 113;
            // 
            // VersionCH
            // 
            this.VersionCH.Text = "Version";
            // 
            // PresetNameCH
            // 
            this.PresetNameCH.Text = "Preset Name";
            this.PresetNameCH.Width = 97;
            // 
            // ProtocolCH
            // 
            this.ProtocolCH.Text = "Protocol";
            this.ProtocolCH.Width = 75;
            // 
            // TemplateCH
            // 
            this.TemplateCH.Text = "Template";
            // 
            // DeviceTemplateVersionCH
            // 
            this.DeviceTemplateVersionCH.Text = "Device Template Version";
            this.DeviceTemplateVersionCH.Width = 101;
            // 
            // DeviceTemplateMenu
            // 
            this.DeviceTemplateMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeviceTemplateNew,
            this.DeviceTemplateDelete});
            this.DeviceTemplateMenu.Name = "ScheduleMenu";
            this.DeviceTemplateMenu.Size = new System.Drawing.Size(197, 48);
            this.DeviceTemplateMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DeviceTemplateMenu_Opening);
            // 
            // DeviceTemplateNew
            // 
            this.DeviceTemplateNew.Name = "DeviceTemplateNew";
            this.DeviceTemplateNew.Size = new System.Drawing.Size(196, 22);
            this.DeviceTemplateNew.Text = "Add Device Template...";
            this.DeviceTemplateNew.Click += new System.EventHandler(this.NewDeviceTemplateMenu_Click);
            // 
            // DeviceTemplateDelete
            // 
            this.DeviceTemplateDelete.Name = "DeviceTemplateDelete";
            this.DeviceTemplateDelete.Size = new System.Drawing.Size(196, 22);
            this.DeviceTemplateDelete.Text = "Delete";
            this.DeviceTemplateDelete.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "empty.bmp");
            this.imageList1.Images.SetKeyName(1, "deletemnu.bmp");
            // 
            // GXAmi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 710);
            this.Controls.Add(this.AmiPanel);
            this.Name = "GXAmi";
            this.Text = "GXDirector";
            this.AmiPanel.Panel1.ResumeLayout(false);
            this.AmiPanel.Panel2.ResumeLayout(false);
            this.AmiPanel.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.AssignedTab.ResumeLayout(false);
            this.DataCollectorMenu.ResumeLayout(false);
            this.UnAssignedTab.ResumeLayout(false);
            this.UnAssignedMenu.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.DeviceMenu.ResumeLayout(false);
            this.TablePanel.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.ValuesTab.ResumeLayout(false);
            this.TableTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableData)).EndInit();
            this.TableTabs.ResumeLayout(false);
            this.ViewTab.ResumeLayout(false);
            this.ViewTab.PerformLayout();
            this.ReadTab.ResumeLayout(false);
            this.ReadTab.PerformLayout();
            this.TableGB.ResumeLayout(false);
            this.TableGB.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.EventLogTab.ResumeLayout(false);
            this.EventMenu.ResumeLayout(false);
            this.SchedulesTab.ResumeLayout(false);
            this.ScheduleMenu.ResumeLayout(false);
            this.TasksTab.ResumeLayout(false);
            this.TasksMenu.ResumeLayout(false);
            this.TraceTab.ResumeLayout(false);
            this.TraceMenu.ResumeLayout(false);
            this.DeviceProfilesTab.ResumeLayout(false);
            this.DeviceTemplateMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer AmiPanel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip ScheduleMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleAddMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleDeleteMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStartMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStartAllMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStopMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStopAllMenu;
        private System.Windows.Forms.ToolStripMenuItem ScheduleRunMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem SchedulePropertiesMenu;
        private System.Windows.Forms.ContextMenuStrip DataCollectorMenu;
        private System.Windows.Forms.ToolStripMenuItem NewDeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
        private System.Windows.Forms.ToolStripMenuItem PropertiesMenu;
        private System.Windows.Forms.ContextMenuStrip TraceMenu;
        private System.Windows.Forms.TabPage EventLogTab;
        internal System.Windows.Forms.ListView EventsList;
        private System.Windows.Forms.ColumnHeader EventTimeCH;
        private System.Windows.Forms.ColumnHeader EventDeviceNameCH;
        private System.Windows.Forms.ColumnHeader EventDescriptionCH;
        private System.Windows.Forms.TabPage SchedulesTab;
        internal System.Windows.Forms.ListView Schedules;
        private System.Windows.Forms.ColumnHeader ScheduleNameCH;
        private System.Windows.Forms.ColumnHeader ScheduleRepeatModeCH;
        private System.Windows.Forms.ColumnHeader ScheduleNextRunTimeCH;
        private System.Windows.Forms.ColumnHeader LastRunTimeCH;
        private System.Windows.Forms.ColumnHeader ScheduleProgressCH;
        private System.Windows.Forms.ContextMenuStrip EventMenu;
        private System.Windows.Forms.ToolStripMenuItem EventCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem EventPauseMenu;
        private System.Windows.Forms.ToolStripMenuItem EventFollowLastMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem EventClear;
        private System.Windows.Forms.TabPage TraceTab;
        internal System.Windows.Forms.ListView TraceView;
        private System.Windows.Forms.ColumnHeader TimeCH;
        private System.Windows.Forms.ColumnHeader DescriptionCH;
        private System.Windows.Forms.ToolStripMenuItem TracePauseMenu;
        private System.Windows.Forms.ToolStripMenuItem TraceCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem TraceFollowLastMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem TraceClearMenu;
        private System.Windows.Forms.ColumnHeader LevelCH;
        private System.Windows.Forms.ToolStripMenuItem TraceHexMenu;
        public System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.ListView DevicesList;
        private System.Windows.Forms.ColumnHeader DeviceNameCH;
        private System.Windows.Forms.ColumnHeader DeviceStatusCH;
        private System.Windows.Forms.ColumnHeader DeviceTypeCH;
        private System.Windows.Forms.ContextMenuStrip DeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceNewDeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceDeleteMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceReadMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem DeviceStartMonitorMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceStopMonitorMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem DevicePropertiesMenu;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage AssignedTab;
        private System.Windows.Forms.TabPage UnAssignedTab;
        internal System.Windows.Forms.ListView UnassignedDCList;
        private System.Windows.Forms.ColumnHeader IPAddressCH;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.TabPage TasksTab;
        internal System.Windows.Forms.ListView TaskList;
        private System.Windows.Forms.ColumnHeader TaskCreationTimeCH;
        private System.Windows.Forms.ColumnHeader TaskTargetCH;
        private System.Windows.Forms.ColumnHeader TaskDescriptionCH;
        private System.Windows.Forms.ColumnHeader TaskClaimTimeCH;
        private System.Windows.Forms.ContextMenuStrip TasksMenu;
        private System.Windows.Forms.ToolStripMenuItem TasksPauseMenu;
        private System.Windows.Forms.ToolStripMenuItem TasksFollowLastMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TasksClearMenu;
        private System.Windows.Forms.ToolStripMenuItem TasksRemoveMenu;
        internal System.Windows.Forms.ListView DCList;
        private System.Windows.Forms.ColumnHeader DcCH;
        private System.Windows.Forms.ContextMenuStrip DeviceTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceTemplateNew;
        private System.Windows.Forms.ToolStripMenuItem DeviceTemplateDelete;
        private System.Windows.Forms.ContextMenuStrip UnAssignedMenu;
        private System.Windows.Forms.ToolStripMenuItem AssignMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveUnAssignedMenu;
        private System.Windows.Forms.ColumnHeader MACAddressLbl;
        private System.Windows.Forms.TabPage DeviceProfilesTab;
        internal System.Windows.Forms.ListView DeviceTemplateList;
        private System.Windows.Forms.ColumnHeader ManufacturerCH;
        private System.Windows.Forms.ColumnHeader ModelCH;
        private System.Windows.Forms.ColumnHeader VersionCH;
        private System.Windows.Forms.ColumnHeader PresetNameCH;
        private System.Windows.Forms.ColumnHeader ProtocolCH;
        private System.Windows.Forms.ColumnHeader TemplateCH;
        private System.Windows.Forms.ColumnHeader DeviceTemplateVersionCH;
        private System.Windows.Forms.Panel TablePanel;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage ValuesTab;
        internal System.Windows.Forms.ListView PropertyList;
        private System.Windows.Forms.ColumnHeader PropertyNameCH;
        private System.Windows.Forms.ColumnHeader PropertyTypeCH;
        private System.Windows.Forms.ColumnHeader PropertyValueCH;
        private System.Windows.Forms.TabPage TableTab;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.TabControl TableTabs;
        private System.Windows.Forms.TabPage ViewTab;
        private System.Windows.Forms.TabPage ReadTab;
        private System.Windows.Forms.TextBox IndexTB;
        private System.Windows.Forms.RadioButton ShowByIndexlCB;
        private System.Windows.Forms.RadioButton ShowAllCB;
        private System.Windows.Forms.Button ReadBtn;
        private System.Windows.Forms.TextBox LastReadValueTP;
        private System.Windows.Forms.RadioButton ReadNewValuesCB;
        private System.Windows.Forms.Label DaysLbl;
        private System.Windows.Forms.Label ToLbl;
        private System.Windows.Forms.DateTimePicker ToPick;
        private System.Windows.Forms.DateTimePicker StartPick;
        private System.Windows.Forms.TextBox ReadLastTB;
        private System.Windows.Forms.RadioButton ReadFromRB;
        private System.Windows.Forms.RadioButton ReadLastRB;
        private System.Windows.Forms.RadioButton ReadAllRB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CountTB;
        private System.Windows.Forms.DataGridView TableData;
        private System.Windows.Forms.Label TableRowCount;
        private System.Windows.Forms.GroupBox TableGB;
        private System.Windows.Forms.Label TableNameLbl;
        private System.Windows.Forms.ComboBox TableCB;
        private System.Windows.Forms.ColumnHeader TimestampCH;
        private System.Windows.Forms.ToolStripMenuItem AddCommandPromptMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ImageList imageList1;

    }
}