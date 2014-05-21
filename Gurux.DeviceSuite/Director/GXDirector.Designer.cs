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

namespace Gurux.DeviceSuite.Director
{
    partial class GXDirector
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
            this.DirectorPanel = new System.Windows.Forms.SplitContainer();
            this.DeviceListTree = new System.Windows.Forms.TreeView();
            this.DeviceTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddDeviceGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewDeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ConnectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ReadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.WriteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ResetPropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearErrorsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelTransactionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.StartMonitorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StopMonitorMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ExpandAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.PropertiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TablePanel = new System.Windows.Forms.Panel();
            this.TableData = new System.Windows.Forms.DataGridView();
            this.ReadingGB = new System.Windows.Forms.GroupBox();
            this.ReadLastTB = new System.Windows.Forms.NumericUpDown();
            this.TableRowCount = new System.Windows.Forms.Label();
            this.LastReadValueTP = new System.Windows.Forms.TextBox();
            this.ReadNewValuesCB = new System.Windows.Forms.RadioButton();
            this.DaysLbl = new System.Windows.Forms.Label();
            this.ToLbl = new System.Windows.Forms.Label();
            this.ToPick = new System.Windows.Forms.DateTimePicker();
            this.StartPick = new System.Windows.Forms.DateTimePicker();
            this.ReadFromRB = new System.Windows.Forms.RadioButton();
            this.ReadLastRB = new System.Windows.Forms.RadioButton();
            this.ReadAllRB = new System.Windows.Forms.RadioButton();
            this.DevicePanel = new System.Windows.Forms.Panel();
            this.DeviceProperties = new System.Windows.Forms.PropertyGrid();
            this.DeviceMediaFrame = new System.Windows.Forms.Panel();
            this.DeviceTypeTB = new System.Windows.Forms.TextBox();
            this.DeviceTypeLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeviceImage = new System.Windows.Forms.PictureBox();
            this.SelectedDeviceLbl = new System.Windows.Forms.Label();
            this.PropertyPanel = new System.Windows.Forms.Panel();
            this.ValueTB = new System.Windows.Forms.TextBox();
            this.TimeStampTB = new System.Windows.Forms.TextBox();
            this.UnitTB = new System.Windows.Forms.TextBox();
            this.TypeTB = new System.Windows.Forms.TextBox();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.TypeLbl = new System.Windows.Forms.Label();
            this.TimeStampLbl = new System.Windows.Forms.Label();
            this.UnitLbl = new System.Windows.Forms.Label();
            this.ValueLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.ValueLB = new System.Windows.Forms.ListView();
            this.ValueCB = new System.Windows.Forms.ComboBox();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.WriteBtn = new System.Windows.Forms.Button();
            this.ReadBtn = new System.Windows.Forms.Button();
            this.PropertyList = new System.Windows.Forms.ListView();
            this.PropertyNameCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyValueCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyTypeCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyUnitCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyTimeCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyReadCountCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyWriteCountCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyExecutionTimeCH = new System.Windows.Forms.ColumnHeader();
            this.DevicesList = new System.Windows.Forms.ListView();
            this.DeviceNameCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceStatusCH = new System.Windows.Forms.ColumnHeader();
            this.DeviceTypeCH = new System.Windows.Forms.ColumnHeader();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.EventLogPage = new System.Windows.Forms.TabPage();
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
            this.ScheduleOptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TracePage = new System.Windows.Forms.TabPage();
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
            this.DirectorPanel.Panel1.SuspendLayout();
            this.DirectorPanel.Panel2.SuspendLayout();
            this.DirectorPanel.SuspendLayout();
            this.DeviceTreeMenu.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.TablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableData)).BeginInit();
            this.ReadingGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadLastTB)).BeginInit();
            this.DevicePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceImage)).BeginInit();
            this.PropertyPanel.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.EventLogPage.SuspendLayout();
            this.EventMenu.SuspendLayout();
            this.SchedulesTab.SuspendLayout();
            this.ScheduleMenu.SuspendLayout();
            this.TracePage.SuspendLayout();
            this.TraceMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DirectorPanel
            // 
            this.DirectorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DirectorPanel.Location = new System.Drawing.Point(0, 0);
            this.DirectorPanel.Name = "DirectorPanel";
            // 
            // DirectorPanel.Panel1
            // 
            this.DirectorPanel.Panel1.Controls.Add(this.DeviceListTree);
            // 
            // DirectorPanel.Panel2
            // 
            this.DirectorPanel.Panel2.Controls.Add(this.splitContainer2);
            this.DirectorPanel.Size = new System.Drawing.Size(987, 710);
            this.DirectorPanel.SplitterDistance = 150;
            this.DirectorPanel.TabIndex = 10;
            // 
            // DeviceListTree
            // 
            this.DeviceListTree.ContextMenuStrip = this.DeviceTreeMenu;
            this.DeviceListTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceListTree.FullRowSelect = true;
            this.DeviceListTree.Location = new System.Drawing.Point(0, 0);
            this.DeviceListTree.Name = "DeviceListTree";
            this.DeviceListTree.Size = new System.Drawing.Size(150, 710);
            this.DeviceListTree.TabIndex = 10;
            this.DeviceListTree.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // DeviceTreeMenu
            // 
            this.DeviceTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddDeviceGroupMenu,
            this.NewDeviceMenu,
            this.DeleteMenu,
            this.toolStripSeparator3,
            this.ConnectMenu,
            this.DisconnectMenu,
            this.toolStripSeparator4,
            this.ReadMenu,
            this.WriteMenu,
            this.toolStripSeparator5,
            this.ResetPropertiesMenu,
            this.ClearErrorsMenu,
            this.CancelTransactionMenu,
            this.toolStripSeparator6,
            this.StartMonitorMenu,
            this.StopMonitorMnu,
            this.toolStripSeparator7,
            this.ExpandAllMenu,
            this.CollapseAllMenu,
            this.toolStripSeparator8,
            this.PropertiesMenu});
            this.DeviceTreeMenu.Name = "ScheduleMenu";
            this.DeviceTreeMenu.Size = new System.Drawing.Size(193, 370);
            this.DeviceTreeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DeviceTreeMenu_Opening);
            // 
            // AddDeviceGroupMenu
            // 
            this.AddDeviceGroupMenu.Name = "AddDeviceGroupMenu";
            this.AddDeviceGroupMenu.Size = new System.Drawing.Size(192, 22);
            this.AddDeviceGroupMenu.Text = "New Device Group...";
            this.AddDeviceGroupMenu.Click += new System.EventHandler(this.AddDeviceGroupMenu_Click);
            // 
            // NewDeviceMenu
            // 
            this.NewDeviceMenu.Name = "NewDeviceMenu";
            this.NewDeviceMenu.Size = new System.Drawing.Size(192, 22);
            this.NewDeviceMenu.Text = "New Device...";
            this.NewDeviceMenu.Click += new System.EventHandler(this.NewDeviceMenu_Click);
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Name = "DeleteMenu";
            this.DeleteMenu.Size = new System.Drawing.Size(192, 22);
            this.DeleteMenu.Text = "Delete";
            this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            // 
            // ConnectMenu
            // 
            this.ConnectMenu.Name = "ConnectMenu";
            this.ConnectMenu.Size = new System.Drawing.Size(192, 22);
            this.ConnectMenu.Text = "Connect";
            this.ConnectMenu.Click += new System.EventHandler(this.ConnectMenu_Click);
            // 
            // DisconnectMenu
            // 
            this.DisconnectMenu.Name = "DisconnectMenu";
            this.DisconnectMenu.Size = new System.Drawing.Size(192, 22);
            this.DisconnectMenu.Text = "Disconnect";
            this.DisconnectMenu.Click += new System.EventHandler(this.DisconnectMenu_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(189, 6);
            // 
            // ReadMenu
            // 
            this.ReadMenu.Name = "ReadMenu";
            this.ReadMenu.Size = new System.Drawing.Size(192, 22);
            this.ReadMenu.Text = "Read";
            this.ReadMenu.Click += new System.EventHandler(this.ReadMenu_Click);
            // 
            // WriteMenu
            // 
            this.WriteMenu.Name = "WriteMenu";
            this.WriteMenu.Size = new System.Drawing.Size(192, 22);
            this.WriteMenu.Text = "Write";
            this.WriteMenu.Click += new System.EventHandler(this.WriteMenu_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(189, 6);
            // 
            // ResetPropertiesMenu
            // 
            this.ResetPropertiesMenu.Name = "ResetPropertiesMenu";
            this.ResetPropertiesMenu.Size = new System.Drawing.Size(192, 22);
            this.ResetPropertiesMenu.Text = "Reset to default values";
            this.ResetPropertiesMenu.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // ClearErrorsMenu
            // 
            this.ClearErrorsMenu.Name = "ClearErrorsMenu";
            this.ClearErrorsMenu.Size = new System.Drawing.Size(192, 22);
            this.ClearErrorsMenu.Text = "Clear Errors";
            // 
            // CancelTransactionMenu
            // 
            this.CancelTransactionMenu.Name = "CancelTransactionMenu";
            this.CancelTransactionMenu.Size = new System.Drawing.Size(192, 22);
            this.CancelTransactionMenu.Text = "Cancel Transaction";
            this.CancelTransactionMenu.Click += new System.EventHandler(this.CancelTransactionMenu_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(189, 6);
            // 
            // StartMonitorMenu
            // 
            this.StartMonitorMenu.Name = "StartMonitorMenu";
            this.StartMonitorMenu.Size = new System.Drawing.Size(192, 22);
            this.StartMonitorMenu.Text = "Start Monitor";
            this.StartMonitorMenu.Click += new System.EventHandler(this.StartMonitorMenu_Click);
            // 
            // StopMonitorMnu
            // 
            this.StopMonitorMnu.Name = "StopMonitorMnu";
            this.StopMonitorMnu.Size = new System.Drawing.Size(192, 22);
            this.StopMonitorMnu.Text = "Stop Monitor";
            this.StopMonitorMnu.Click += new System.EventHandler(this.StopMonitorMnu_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(189, 6);
            // 
            // ExpandAllMenu
            // 
            this.ExpandAllMenu.Name = "ExpandAllMenu";
            this.ExpandAllMenu.Size = new System.Drawing.Size(192, 22);
            this.ExpandAllMenu.Text = "Expand All";
            this.ExpandAllMenu.Click += new System.EventHandler(this.ExpandAllMenu_Click);
            // 
            // CollapseAllMenu
            // 
            this.CollapseAllMenu.Name = "CollapseAllMenu";
            this.CollapseAllMenu.Size = new System.Drawing.Size(192, 22);
            this.CollapseAllMenu.Text = "Collapse All";
            this.CollapseAllMenu.Click += new System.EventHandler(this.CollapseAllMenu_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(189, 6);
            // 
            // PropertiesMenu
            // 
            this.PropertiesMenu.Name = "PropertiesMenu";
            this.PropertiesMenu.Size = new System.Drawing.Size(192, 22);
            this.PropertiesMenu.Text = "Properties...";
            this.PropertiesMenu.Click += new System.EventHandler(this.PropertiesMenu_Click);
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
            this.splitContainer2.Panel1.Controls.Add(this.TablePanel);
            this.splitContainer2.Panel1.Controls.Add(this.DevicePanel);
            this.splitContainer2.Panel1.Controls.Add(this.PropertyPanel);
            this.splitContainer2.Panel1.Controls.Add(this.PropertyList);
            this.splitContainer2.Panel1.Controls.Add(this.DevicesList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.TabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(833, 710);
            this.splitContainer2.SplitterDistance = 468;
            this.splitContainer2.TabIndex = 0;
            // 
            // TablePanel
            // 
            this.TablePanel.Controls.Add(this.TableData);
            this.TablePanel.Controls.Add(this.ReadingGB);
            this.TablePanel.Location = new System.Drawing.Point(7, 12);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.Size = new System.Drawing.Size(391, 250);
            this.TablePanel.TabIndex = 13;
            // 
            // TableData
            // 
            this.TableData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableData.Location = new System.Drawing.Point(0, 131);
            this.TableData.Name = "TableData";
            this.TableData.Size = new System.Drawing.Size(391, 119);
            this.TableData.TabIndex = 15;
            // 
            // ReadingGB
            // 
            this.ReadingGB.Controls.Add(this.ReadLastTB);
            this.ReadingGB.Controls.Add(this.TableRowCount);
            this.ReadingGB.Controls.Add(this.LastReadValueTP);
            this.ReadingGB.Controls.Add(this.ReadNewValuesCB);
            this.ReadingGB.Controls.Add(this.DaysLbl);
            this.ReadingGB.Controls.Add(this.ToLbl);
            this.ReadingGB.Controls.Add(this.ToPick);
            this.ReadingGB.Controls.Add(this.StartPick);
            this.ReadingGB.Controls.Add(this.ReadFromRB);
            this.ReadingGB.Controls.Add(this.ReadLastRB);
            this.ReadingGB.Controls.Add(this.ReadAllRB);
            this.ReadingGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReadingGB.Location = new System.Drawing.Point(0, 0);
            this.ReadingGB.Name = "ReadingGB";
            this.ReadingGB.Size = new System.Drawing.Size(391, 131);
            this.ReadingGB.TabIndex = 13;
            this.ReadingGB.TabStop = false;
            this.ReadingGB.Text = "Reading";
            // 
            // ReadLastTB
            // 
            this.ReadLastTB.Location = new System.Drawing.Point(94, 39);
            this.ReadLastTB.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ReadLastTB.Name = "ReadLastTB";
            this.ReadLastTB.Size = new System.Drawing.Size(74, 20);
            this.ReadLastTB.TabIndex = 20;            
            this.ReadLastTB.ValueChanged += new System.EventHandler(this.OnReadingChanged);
            this.ReadLastTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReadLastTB_KeyUp);
            // 
            // TableRowCount
            // 
            this.TableRowCount.Location = new System.Drawing.Point(22, 111);
            this.TableRowCount.Name = "TableRowCount";
            this.TableRowCount.Size = new System.Drawing.Size(72, 16);
            this.TableRowCount.TabIndex = 19;
            this.TableRowCount.Text = "RownLbl";
            // 
            // LastReadValueTP
            // 
            this.LastReadValueTP.Location = new System.Drawing.Point(238, 93);
            this.LastReadValueTP.Name = "LastReadValueTP";
            this.LastReadValueTP.ReadOnly = true;
            this.LastReadValueTP.Size = new System.Drawing.Size(112, 20);
            this.LastReadValueTP.TabIndex = 18;
            // 
            // ReadNewValuesCB
            // 
            this.ReadNewValuesCB.Location = new System.Drawing.Point(6, 89);
            this.ReadNewValuesCB.Name = "ReadNewValuesCB";
            this.ReadNewValuesCB.Size = new System.Drawing.Size(200, 19);
            this.ReadNewValuesCB.TabIndex = 17;
            this.ReadNewValuesCB.Text = "Read New Values";
            this.ReadNewValuesCB.CheckedChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // DaysLbl
            // 
            this.DaysLbl.Location = new System.Drawing.Point(174, 43);
            this.DaysLbl.Name = "DaysLbl";
            this.DaysLbl.Size = new System.Drawing.Size(72, 16);
            this.DaysLbl.TabIndex = 16;
            this.DaysLbl.Text = "Days";
            // 
            // ToLbl
            // 
            this.ToLbl.AutoSize = true;
            this.ToLbl.Location = new System.Drawing.Point(214, 70);
            this.ToLbl.Name = "ToLbl";
            this.ToLbl.Size = new System.Drawing.Size(20, 13);
            this.ToLbl.TabIndex = 15;
            this.ToLbl.Text = "To";
            // 
            // ToPick
            // 
            this.ToPick.Checked = false;
            this.ToPick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ToPick.Location = new System.Drawing.Point(238, 67);
            this.ToPick.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ToPick.Name = "ToPick";
            this.ToPick.ShowCheckBox = true;
            this.ToPick.Size = new System.Drawing.Size(112, 20);
            this.ToPick.TabIndex = 14;
            this.ToPick.ValueChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // StartPick
            // 
            this.StartPick.Checked = false;
            this.StartPick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartPick.Location = new System.Drawing.Point(94, 67);
            this.StartPick.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.StartPick.Name = "StartPick";
            this.StartPick.ShowCheckBox = true;
            this.StartPick.Size = new System.Drawing.Size(112, 20);
            this.StartPick.TabIndex = 13;
            this.StartPick.ValueChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // ReadFromRB
            // 
            this.ReadFromRB.Location = new System.Drawing.Point(6, 67);
            this.ReadFromRB.Name = "ReadFromRB";
            this.ReadFromRB.Size = new System.Drawing.Size(80, 16);
            this.ReadFromRB.TabIndex = 11;
            this.ReadFromRB.Text = "Read From";
            this.ReadFromRB.CheckedChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // ReadLastRB
            // 
            this.ReadLastRB.Location = new System.Drawing.Point(6, 43);
            this.ReadLastRB.Name = "ReadLastRB";
            this.ReadLastRB.Size = new System.Drawing.Size(80, 16);
            this.ReadLastRB.TabIndex = 10;
            this.ReadLastRB.Text = "Read last";
            this.ReadLastRB.CheckedChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // ReadAllRB
            // 
            this.ReadAllRB.Checked = true;
            this.ReadAllRB.Location = new System.Drawing.Point(6, 19);
            this.ReadAllRB.Name = "ReadAllRB";
            this.ReadAllRB.Size = new System.Drawing.Size(80, 16);
            this.ReadAllRB.TabIndex = 9;
            this.ReadAllRB.TabStop = true;
            this.ReadAllRB.Text = "Read All";
            this.ReadAllRB.CheckedChanged += new System.EventHandler(this.OnReadingChanged);
            // 
            // DevicePanel
            // 
            this.DevicePanel.Controls.Add(this.DeviceProperties);
            this.DevicePanel.Controls.Add(this.DeviceMediaFrame);
            this.DevicePanel.Controls.Add(this.DeviceTypeTB);
            this.DevicePanel.Controls.Add(this.DeviceTypeLbl);
            this.DevicePanel.Controls.Add(this.panel1);
            this.DevicePanel.Location = new System.Drawing.Point(17, 77);
            this.DevicePanel.Name = "DevicePanel";
            this.DevicePanel.Size = new System.Drawing.Size(290, 350);
            this.DevicePanel.TabIndex = 12;
            // 
            // DeviceProperties
            // 
            this.DeviceProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceProperties.Enabled = false;
            this.DeviceProperties.HelpVisible = false;
            this.DeviceProperties.Location = new System.Drawing.Point(8, 243);
            this.DeviceProperties.Name = "DeviceProperties";
            this.DeviceProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.DeviceProperties.Size = new System.Drawing.Size(279, 104);
            this.DeviceProperties.TabIndex = 53;
            this.DeviceProperties.ToolbarVisible = false;
            // 
            // DeviceMediaFrame
            // 
            this.DeviceMediaFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceMediaFrame.Enabled = false;
            this.DeviceMediaFrame.Location = new System.Drawing.Point(8, 77);
            this.DeviceMediaFrame.Name = "DeviceMediaFrame";
            this.DeviceMediaFrame.Size = new System.Drawing.Size(275, 160);
            this.DeviceMediaFrame.TabIndex = 52;
            // 
            // DeviceTypeTB
            // 
            this.DeviceTypeTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceTypeTB.Location = new System.Drawing.Point(80, 51);
            this.DeviceTypeTB.Name = "DeviceTypeTB";
            this.DeviceTypeTB.ReadOnly = true;
            this.DeviceTypeTB.Size = new System.Drawing.Size(207, 20);
            this.DeviceTypeTB.TabIndex = 49;
            // 
            // DeviceTypeLbl
            // 
            this.DeviceTypeLbl.AutoSize = true;
            this.DeviceTypeLbl.Location = new System.Drawing.Point(12, 54);
            this.DeviceTypeLbl.Name = "DeviceTypeLbl";
            this.DeviceTypeLbl.Size = new System.Drawing.Size(34, 13);
            this.DeviceTypeLbl.TabIndex = 47;
            this.DeviceTypeLbl.Text = "Type:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.DeviceImage);
            this.panel1.Controls.Add(this.SelectedDeviceLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 48);
            this.panel1.TabIndex = 9;
            // 
            // DeviceImage
            // 
            this.DeviceImage.Image = global::Gurux.DeviceSuite.Properties.Resources.Device;
            this.DeviceImage.Location = new System.Drawing.Point(8, 8);
            this.DeviceImage.Name = "DeviceImage";
            this.DeviceImage.Size = new System.Drawing.Size(16, 16);
            this.DeviceImage.TabIndex = 1;
            this.DeviceImage.TabStop = false;
            // 
            // SelectedDeviceLbl
            // 
            this.SelectedDeviceLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedDeviceLbl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SelectedDeviceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedDeviceLbl.Location = new System.Drawing.Point(32, 8);
            this.SelectedDeviceLbl.Name = "SelectedDeviceLbl";
            this.SelectedDeviceLbl.Size = new System.Drawing.Size(241, 24);
            this.SelectedDeviceLbl.TabIndex = 0;
            // 
            // PropertyPanel
            // 
            this.PropertyPanel.Controls.Add(this.ValueTB);
            this.PropertyPanel.Controls.Add(this.TimeStampTB);
            this.PropertyPanel.Controls.Add(this.UnitTB);
            this.PropertyPanel.Controls.Add(this.TypeTB);
            this.PropertyPanel.Controls.Add(this.NameTB);
            this.PropertyPanel.Controls.Add(this.TypeLbl);
            this.PropertyPanel.Controls.Add(this.TimeStampLbl);
            this.PropertyPanel.Controls.Add(this.UnitLbl);
            this.PropertyPanel.Controls.Add(this.ValueLbl);
            this.PropertyPanel.Controls.Add(this.NameLbl);
            this.PropertyPanel.Controls.Add(this.ValueLB);
            this.PropertyPanel.Controls.Add(this.ValueCB);
            this.PropertyPanel.Controls.Add(this.ResetBtn);
            this.PropertyPanel.Controls.Add(this.WriteBtn);
            this.PropertyPanel.Controls.Add(this.ReadBtn);
            this.PropertyPanel.Location = new System.Drawing.Point(339, 65);
            this.PropertyPanel.Name = "PropertyPanel";
            this.PropertyPanel.Size = new System.Drawing.Size(370, 298);
            this.PropertyPanel.TabIndex = 11;
            this.PropertyPanel.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // ValueTB
            // 
            this.ValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTB.Location = new System.Drawing.Point(116, 111);
            this.ValueTB.Name = "ValueTB";
            this.ValueTB.Size = new System.Drawing.Size(232, 20);
            this.ValueTB.TabIndex = 48;
            // 
            // TimeStampTB
            // 
            this.TimeStampTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeStampTB.Location = new System.Drawing.Point(114, 83);
            this.TimeStampTB.Name = "TimeStampTB";
            this.TimeStampTB.ReadOnly = true;
            this.TimeStampTB.Size = new System.Drawing.Size(232, 20);
            this.TimeStampTB.TabIndex = 47;
            // 
            // UnitTB
            // 
            this.UnitTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UnitTB.Location = new System.Drawing.Point(114, 58);
            this.UnitTB.Name = "UnitTB";
            this.UnitTB.ReadOnly = true;
            this.UnitTB.Size = new System.Drawing.Size(232, 20);
            this.UnitTB.TabIndex = 46;
            // 
            // TypeTB
            // 
            this.TypeTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeTB.Location = new System.Drawing.Point(114, 35);
            this.TypeTB.Name = "TypeTB";
            this.TypeTB.ReadOnly = true;
            this.TypeTB.Size = new System.Drawing.Size(232, 20);
            this.TypeTB.TabIndex = 45;
            // 
            // NameTB
            // 
            this.NameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTB.Location = new System.Drawing.Point(114, 12);
            this.NameTB.Name = "NameTB";
            this.NameTB.ReadOnly = true;
            this.NameTB.Size = new System.Drawing.Size(232, 20);
            this.NameTB.TabIndex = 44;
            // 
            // TypeLbl
            // 
            this.TypeLbl.AutoSize = true;
            this.TypeLbl.Location = new System.Drawing.Point(10, 38);
            this.TypeLbl.Name = "TypeLbl";
            this.TypeLbl.Size = new System.Drawing.Size(45, 13);
            this.TypeLbl.TabIndex = 43;
            this.TypeLbl.Text = "TypeLbl";
            // 
            // TimeStampLbl
            // 
            this.TimeStampLbl.AutoSize = true;
            this.TimeStampLbl.Location = new System.Drawing.Point(10, 86);
            this.TimeStampLbl.Name = "TimeStampLbl";
            this.TimeStampLbl.Size = new System.Drawing.Size(63, 13);
            this.TimeStampLbl.TabIndex = 41;
            this.TimeStampLbl.Text = "Time Stamp";
            // 
            // UnitLbl
            // 
            this.UnitLbl.AutoSize = true;
            this.UnitLbl.Location = new System.Drawing.Point(10, 62);
            this.UnitLbl.Name = "UnitLbl";
            this.UnitLbl.Size = new System.Drawing.Size(40, 13);
            this.UnitLbl.TabIndex = 35;
            this.UnitLbl.Text = "UnitLbl";
            // 
            // ValueLbl
            // 
            this.ValueLbl.AutoSize = true;
            this.ValueLbl.Location = new System.Drawing.Point(10, 110);
            this.ValueLbl.Name = "ValueLbl";
            this.ValueLbl.Size = new System.Drawing.Size(48, 13);
            this.ValueLbl.TabIndex = 34;
            this.ValueLbl.Text = "ValueLbl";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(10, 14);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(49, 13);
            this.NameLbl.TabIndex = 33;
            this.NameLbl.Text = "NameLbl";
            // 
            // ValueLB
            // 
            this.ValueLB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueLB.AutoArrange = false;
            this.ValueLB.CheckBoxes = true;
            this.ValueLB.FullRowSelect = true;
            this.ValueLB.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ValueLB.Location = new System.Drawing.Point(114, 134);
            this.ValueLB.MultiSelect = false;
            this.ValueLB.Name = "ValueLB";
            this.ValueLB.Size = new System.Drawing.Size(234, 128);
            this.ValueLB.TabIndex = 42;
            this.ValueLB.UseCompatibleStateImageBehavior = false;
            this.ValueLB.View = System.Windows.Forms.View.List;
            // 
            // ValueCB
            // 
            this.ValueCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueCB.ItemHeight = 13;
            this.ValueCB.Location = new System.Drawing.Point(114, 110);
            this.ValueCB.Name = "ValueCB";
            this.ValueCB.Size = new System.Drawing.Size(234, 21);
            this.ValueCB.TabIndex = 39;
            // 
            // ResetBtn
            // 
            this.ResetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ResetBtn.Location = new System.Drawing.Point(273, 268);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(75, 23);
            this.ResetBtn.TabIndex = 38;
            this.ResetBtn.Text = "ResetBtn";
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // WriteBtn
            // 
            this.WriteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WriteBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.WriteBtn.Location = new System.Drawing.Point(193, 268);
            this.WriteBtn.Name = "WriteBtn";
            this.WriteBtn.Size = new System.Drawing.Size(75, 23);
            this.WriteBtn.TabIndex = 37;
            this.WriteBtn.Text = "WriteBtn";
            this.WriteBtn.Click += new System.EventHandler(this.WriteBtn_Click);
            // 
            // ReadBtn
            // 
            this.ReadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ReadBtn.Location = new System.Drawing.Point(113, 268);
            this.ReadBtn.Name = "ReadBtn";
            this.ReadBtn.Size = new System.Drawing.Size(75, 23);
            this.ReadBtn.TabIndex = 36;
            this.ReadBtn.Text = "ReadBtn";
            this.ReadBtn.Click += new System.EventHandler(this.ReadBtn_Click);
            // 
            // PropertyList
            // 
            this.PropertyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyNameCH,
            this.PropertyValueCH,
            this.PropertyTypeCH,
            this.PropertyUnitCH,
            this.PropertyTimeCH,
            this.PropertyReadCountCH,
            this.PropertyWriteCountCH,
            this.PropertyExecutionTimeCH});
            this.PropertyList.FullRowSelect = true;
            this.PropertyList.Location = new System.Drawing.Point(472, 376);
            this.PropertyList.Name = "PropertyList";
            this.PropertyList.Size = new System.Drawing.Size(187, 48);
            this.PropertyList.TabIndex = 9;
            this.PropertyList.UseCompatibleStateImageBehavior = false;
            this.PropertyList.View = System.Windows.Forms.View.Details;
            this.PropertyList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PropertyList_MouseDoubleClick);
            this.PropertyList.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // PropertyNameCH
            // 
            this.PropertyNameCH.Text = "Name";
            // 
            // PropertyValueCH
            // 
            this.PropertyValueCH.Text = "Value";
            // 
            // PropertyTypeCH
            // 
            this.PropertyTypeCH.Text = "Type";
            // 
            // PropertyUnitCH
            // 
            this.PropertyUnitCH.Text = "Unit";
            // 
            // PropertyTimeCH
            // 
            this.PropertyTimeCH.Text = "Time";
            // 
            // PropertyReadCountCH
            // 
            this.PropertyReadCountCH.Text = "Read Count";
            // 
            // PropertyWriteCountCH
            // 
            this.PropertyWriteCountCH.Text = "Write Count";
            // 
            // PropertyExecutionTimeCH
            // 
            this.PropertyExecutionTimeCH.Text = "Execution Time";
            // 
            // DevicesList
            // 
            this.DevicesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DeviceNameCH,
            this.DeviceStatusCH,
            this.DeviceTypeCH});
            this.DevicesList.FullRowSelect = true;
            this.DevicesList.Location = new System.Drawing.Point(415, 5);
            this.DevicesList.Name = "DevicesList";
            this.DevicesList.Size = new System.Drawing.Size(232, 54);
            this.DevicesList.TabIndex = 8;
            this.DevicesList.UseCompatibleStateImageBehavior = false;
            this.DevicesList.View = System.Windows.Forms.View.Details;
            this.DevicesList.Enter += new System.EventHandler(this.PropertyGrid_Enter);
            // 
            // DeviceNameCH
            // 
            this.DeviceNameCH.Text = "Device Name";
            this.DeviceNameCH.Width = 85;
            // 
            // DeviceStatusCH
            // 
            this.DeviceStatusCH.Text = "Status";
            // 
            // DeviceTypeCH
            // 
            this.DeviceTypeCH.Text = "Type";
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.EventLogPage);
            this.TabControl1.Controls.Add(this.SchedulesTab);
            this.TabControl1.Controls.Add(this.TracePage);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(833, 238);
            this.TabControl1.TabIndex = 40;
            // 
            // EventLogPage
            // 
            this.EventLogPage.Controls.Add(this.EventsList);
            this.EventLogPage.Location = new System.Drawing.Point(4, 22);
            this.EventLogPage.Name = "EventLogPage";
            this.EventLogPage.Padding = new System.Windows.Forms.Padding(3);
            this.EventLogPage.Size = new System.Drawing.Size(825, 212);
            this.EventLogPage.TabIndex = 0;
            this.EventLogPage.Text = "Event log";
            this.EventLogPage.UseVisualStyleBackColor = true;
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
            this.EventsList.Size = new System.Drawing.Size(819, 206);
            this.EventsList.TabIndex = 3;
            this.EventsList.UseCompatibleStateImageBehavior = false;
            this.EventsList.View = System.Windows.Forms.View.Details;
            this.EventsList.Enter += new System.EventHandler(this.PropertyGrid_Enter);
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
            this.SchedulesTab.Location = new System.Drawing.Point(4, 22);
            this.SchedulesTab.Name = "SchedulesTab";
            this.SchedulesTab.Padding = new System.Windows.Forms.Padding(3);
            this.SchedulesTab.Size = new System.Drawing.Size(825, 212);
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
            this.Schedules.Location = new System.Drawing.Point(3, 3);
            this.Schedules.Name = "Schedules";
            this.Schedules.Size = new System.Drawing.Size(819, 206);
            this.Schedules.TabIndex = 4;
            this.Schedules.UseCompatibleStateImageBehavior = false;
            this.Schedules.View = System.Windows.Forms.View.Details;
            this.Schedules.DoubleClick += new System.EventHandler(this.ScheduleOptionsMenu_Click);
            this.Schedules.Enter += new System.EventHandler(this.PropertyGrid_Enter);
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
            this.ScheduleOptionsMenu});
            this.ScheduleMenu.Name = "ScheduleMenu";
            this.ScheduleMenu.Size = new System.Drawing.Size(132, 192);
            this.ScheduleMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ScheduleMenu_Opening);
            // 
            // ScheduleAddMenu
            // 
            this.ScheduleAddMenu.Name = "ScheduleAddMenu";
            this.ScheduleAddMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleAddMenu.Text = "Add";
            this.ScheduleAddMenu.Click += new System.EventHandler(this.NewScheduleMenu_Click);
            // 
            // ScheduleDeleteMenu
            // 
            this.ScheduleDeleteMenu.Name = "ScheduleDeleteMenu";
            this.ScheduleDeleteMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ScheduleDeleteMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleDeleteMenu.Text = "Delete";
            this.ScheduleDeleteMenu.Click += new System.EventHandler(this.ScheduleDeleteMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // ScheduleStartMenu
            // 
            this.ScheduleStartMenu.Name = "ScheduleStartMenu";
            this.ScheduleStartMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleStartMenu.Text = "Start";
            this.ScheduleStartMenu.Click += new System.EventHandler(this.ScheduleStartMenu_Click);
            // 
            // ScheduleStartAllMenu
            // 
            this.ScheduleStartAllMenu.Name = "ScheduleStartAllMenu";
            this.ScheduleStartAllMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleStartAllMenu.Text = "Start All";
            this.ScheduleStartAllMenu.Click += new System.EventHandler(this.ScheduleStartAllMenu_Click);
            // 
            // ScheduleStopMenu
            // 
            this.ScheduleStopMenu.Name = "ScheduleStopMenu";
            this.ScheduleStopMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleStopMenu.Text = "Stop";
            this.ScheduleStopMenu.Click += new System.EventHandler(this.ScheduleStopMenu_Click);
            // 
            // ScheduleStopAllMenu
            // 
            this.ScheduleStopAllMenu.Name = "ScheduleStopAllMenu";
            this.ScheduleStopAllMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleStopAllMenu.Text = "Stop All";
            this.ScheduleStopAllMenu.Click += new System.EventHandler(this.ScheduleStopAllMenu_Click);
            // 
            // ScheduleRunMenu
            // 
            this.ScheduleRunMenu.Name = "ScheduleRunMenu";
            this.ScheduleRunMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleRunMenu.Text = "Run";
            this.ScheduleRunMenu.Click += new System.EventHandler(this.ScheduleRunMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // ScheduleOptionsMenu
            // 
            this.ScheduleOptionsMenu.Name = "ScheduleOptionsMenu";
            this.ScheduleOptionsMenu.Size = new System.Drawing.Size(131, 22);
            this.ScheduleOptionsMenu.Text = "Options";
            this.ScheduleOptionsMenu.Click += new System.EventHandler(this.ScheduleOptionsMenu_Click);
            // 
            // TracePage
            // 
            this.TracePage.Controls.Add(this.TraceView);
            this.TracePage.Location = new System.Drawing.Point(4, 22);
            this.TracePage.Name = "TracePage";
            this.TracePage.Padding = new System.Windows.Forms.Padding(3);
            this.TracePage.Size = new System.Drawing.Size(825, 212);
            this.TracePage.TabIndex = 2;
            this.TracePage.Text = "Trace";
            this.TracePage.UseVisualStyleBackColor = true;
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
            this.TraceView.Size = new System.Drawing.Size(819, 206);
            this.TraceView.TabIndex = 3;
            this.TraceView.UseCompatibleStateImageBehavior = false;
            this.TraceView.View = System.Windows.Forms.View.Details;
            this.TraceView.VirtualMode = true;
            this.TraceView.Enter += new System.EventHandler(this.PropertyGrid_Enter);
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
            // GXDirector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 710);
            this.Controls.Add(this.DirectorPanel);
            this.Name = "GXDirector";
            this.Text = "GXDirector";
            this.DirectorPanel.Panel1.ResumeLayout(false);
            this.DirectorPanel.Panel2.ResumeLayout(false);
            this.DirectorPanel.ResumeLayout(false);
            this.DeviceTreeMenu.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.TablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableData)).EndInit();
            this.ReadingGB.ResumeLayout(false);
            this.ReadingGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReadLastTB)).EndInit();
            this.DevicePanel.ResumeLayout(false);
            this.DevicePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceImage)).EndInit();
            this.PropertyPanel.ResumeLayout(false);
            this.PropertyPanel.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.EventLogPage.ResumeLayout(false);
            this.EventMenu.ResumeLayout(false);
            this.SchedulesTab.ResumeLayout(false);
            this.ScheduleMenu.ResumeLayout(false);
            this.TracePage.ResumeLayout(false);
            this.TraceMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer DirectorPanel;
        public System.Windows.Forms.TreeView DeviceListTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel PropertyPanel;
        private System.Windows.Forms.TextBox TimeStampTB;
        private System.Windows.Forms.TextBox UnitTB;
        private System.Windows.Forms.TextBox TypeTB;
        private System.Windows.Forms.Label TypeLbl;
        private System.Windows.Forms.Label TimeStampLbl;
        private System.Windows.Forms.Label UnitLbl;
        private System.Windows.Forms.Label ValueLbl;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.ListView ValueLB;
        private System.Windows.Forms.ComboBox ValueCB;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.Button WriteBtn;
        private System.Windows.Forms.Button ReadBtn;
        private System.Windows.Forms.ListView PropertyList;
        private System.Windows.Forms.ColumnHeader PropertyNameCH;
        private System.Windows.Forms.ColumnHeader PropertyValueCH;
        private System.Windows.Forms.ColumnHeader PropertyTypeCH;
        private System.Windows.Forms.ColumnHeader PropertyUnitCH;
        private System.Windows.Forms.ColumnHeader PropertyTimeCH;
        private System.Windows.Forms.ColumnHeader PropertyReadCountCH;
        private System.Windows.Forms.ColumnHeader PropertyWriteCountCH;
        private System.Windows.Forms.ColumnHeader PropertyExecutionTimeCH;
        private System.Windows.Forms.ListView DevicesList;
        private System.Windows.Forms.ColumnHeader DeviceNameCH;
        private System.Windows.Forms.ColumnHeader DeviceStatusCH;
        private System.Windows.Forms.ColumnHeader DeviceTypeCH;
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
        private System.Windows.Forms.ToolStripMenuItem ScheduleOptionsMenu;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.TextBox ValueTB;
        private System.Windows.Forms.ContextMenuStrip DeviceTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem AddDeviceGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem NewDeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ConnectMenu;
        private System.Windows.Forms.ToolStripMenuItem DisconnectMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ReadMenu;
        private System.Windows.Forms.ToolStripMenuItem WriteMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ResetPropertiesMenu;
        private System.Windows.Forms.ToolStripMenuItem ClearErrorsMenu;
        private System.Windows.Forms.ToolStripMenuItem CancelTransactionMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem StartMonitorMenu;
        private System.Windows.Forms.ToolStripMenuItem StopMonitorMnu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem ExpandAllMenu;
        private System.Windows.Forms.ToolStripMenuItem CollapseAllMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem PropertiesMenu;
        private System.Windows.Forms.ContextMenuStrip TraceMenu;
        private System.Windows.Forms.Panel DevicePanel;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox DeviceImage;
        private System.Windows.Forms.Label SelectedDeviceLbl;
        private System.Windows.Forms.TabPage EventLogPage;
        private System.Windows.Forms.ListView EventsList;
        private System.Windows.Forms.ColumnHeader EventTimeCH;
        private System.Windows.Forms.ColumnHeader EventDeviceNameCH;
        private System.Windows.Forms.ColumnHeader EventDescriptionCH;
        private System.Windows.Forms.TabPage SchedulesTab;
        public System.Windows.Forms.ListView Schedules;
        private System.Windows.Forms.ColumnHeader ScheduleNameCH;
        private System.Windows.Forms.ColumnHeader ScheduleRepeatModeCH;
        private System.Windows.Forms.ColumnHeader ScheduleNextRunTimeCH;
        private System.Windows.Forms.ColumnHeader LastRunTimeCH;
        private System.Windows.Forms.ColumnHeader ScheduleProgressCH;
        private System.Windows.Forms.TextBox DeviceTypeTB;
        private System.Windows.Forms.Label DeviceTypeLbl;
        private System.Windows.Forms.Panel DeviceMediaFrame;
        private System.Windows.Forms.PropertyGrid DeviceProperties;
        private System.Windows.Forms.ContextMenuStrip EventMenu;
        private System.Windows.Forms.ToolStripMenuItem EventCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem EventPauseMenu;
        private System.Windows.Forms.ToolStripMenuItem EventFollowLastMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem EventClear;
        private System.Windows.Forms.Panel TablePanel;
        private System.Windows.Forms.GroupBox ReadingGB;
        private System.Windows.Forms.TextBox LastReadValueTP;
        private System.Windows.Forms.RadioButton ReadNewValuesCB;
        private System.Windows.Forms.Label DaysLbl;
        private System.Windows.Forms.Label ToLbl;
        private System.Windows.Forms.DateTimePicker ToPick;
        private System.Windows.Forms.DateTimePicker StartPick;
        private System.Windows.Forms.RadioButton ReadFromRB;
        private System.Windows.Forms.RadioButton ReadLastRB;
        private System.Windows.Forms.RadioButton ReadAllRB;
        private System.Windows.Forms.DataGridView TableData;
        private System.Windows.Forms.Label TableRowCount;
        private System.Windows.Forms.TabPage TracePage;
        public System.Windows.Forms.ListView TraceView;
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
        private System.Windows.Forms.NumericUpDown ReadLastTB;

    }
}