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
	partial class GXScheduleEditorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXScheduleEditorDlg));
            this.DayOfMonthTb = new System.Windows.Forms.TextBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.ErrorWaitTimeTp = new System.Windows.Forms.DateTimePicker();
            this.TransactionStartTimeLbl = new System.Windows.Forms.Label();
            this.UpdateFrequencyLbl = new System.Windows.Forms.Label();
            this.ErrorTryCountLbl = new System.Windows.Forms.Label();
            this.ActionCb = new System.Windows.Forms.ComboBox();
            this.ErrorTryCountTb = new System.Windows.Forms.TextBox();
            this.ErrorWaitTimeLbl = new System.Windows.Forms.Label();
            this.TransactionEndTimeLbl = new System.Windows.Forms.Label();
            this.TransactionCountLbl = new System.Windows.Forms.Label();
            this.TransactionCountTb = new System.Windows.Forms.TextBox();
            this.DayMonCb = new System.Windows.Forms.CheckBox();
            this.TransactionStartTimeTp = new System.Windows.Forms.DateTimePicker();
            this.IntervalLbl = new System.Windows.Forms.Label();
            this.DayOfWeekLbl = new System.Windows.Forms.Label();
            this.ScheduleStartDateTp = new System.Windows.Forms.DateTimePicker();
            this.ActionLbl = new System.Windows.Forms.Label();
            this.TransactionEndTimeTp = new System.Windows.Forms.DateTimePicker();
            this.ScheduleStartTimeLbl = new System.Windows.Forms.Label();
            this.NameTb = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ScheduleTab = new System.Windows.Forms.TabPage();
            this.ScheduleEndTimeLbl = new System.Windows.Forms.Label();
            this.RepeatModeCb = new System.Windows.Forms.ComboBox();
            this.ScheduleEndDateTp = new System.Windows.Forms.DateTimePicker();
            this.NameLbl = new System.Windows.Forms.Label();
            this.RepeatModeLbl = new System.Windows.Forms.Label();
            this.DaySunCb = new System.Windows.Forms.CheckBox();
            this.DaySatCb = new System.Windows.Forms.CheckBox();
            this.DayFriCb = new System.Windows.Forms.CheckBox();
            this.DayThuCb = new System.Windows.Forms.CheckBox();
            this.DayWedCb = new System.Windows.Forms.CheckBox();
            this.DayTueCb = new System.Windows.Forms.CheckBox();
            this.DayOfMonthLbl = new System.Windows.Forms.Label();
            this.IntervalTb = new System.Windows.Forms.TextBox();
            this.TransactionTab = new System.Windows.Forms.TabPage();
            this.UpdateFrequencyTp = new System.Windows.Forms.DateTimePicker();
            this.ConnectionDelayTimeTp = new System.Windows.Forms.DateTimePicker();
            this.ConnectionDelayTimeLbl = new System.Windows.Forms.Label();
            this.ConnectionFailWaitTimeTp = new System.Windows.Forms.DateTimePicker();
            this.ConnectionFailTryCountTb = new System.Windows.Forms.TextBox();
            this.ConnectionFailTryCountLbl = new System.Windows.Forms.Label();
            this.ConnectionFailWaitTimeLbl = new System.Windows.Forms.Label();
            this.MaxThreadCountTb = new System.Windows.Forms.TextBox();
            this.MaxThreadCountLbl = new System.Windows.Forms.Label();
            this.TransactionCountCb = new System.Windows.Forms.CheckBox();
            this.TargetTab = new System.Windows.Forms.TabPage();
            this.TargetCheckTree = new System.Windows.Forms.TreeView();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tabControl1.SuspendLayout();
            this.ScheduleTab.SuspendLayout();
            this.TransactionTab.SuspendLayout();
            this.TargetTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // DayOfMonthTb
            // 
            this.DayOfMonthTb.Location = new System.Drawing.Point(176, 208);
            this.DayOfMonthTb.Name = "DayOfMonthTb";
            this.helpProvider1.SetShowHelp(this.DayOfMonthTb, true);
            this.DayOfMonthTb.Size = new System.Drawing.Size(168, 20);
            this.DayOfMonthTb.TabIndex = 8;
            this.DayOfMonthTb.Text = "DayOfMonthTb";
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(225, 335);
            this.OKBtn.Name = "OKBtn";
            this.helpProvider1.SetShowHelp(this.OKBtn, true);
            this.OKBtn.Size = new System.Drawing.Size(64, 24);
            this.OKBtn.TabIndex = 104;
            this.OKBtn.Text = "OK";
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // ErrorWaitTimeTp
            // 
            this.ErrorWaitTimeTp.CustomFormat = "HH:mm:ss";
            this.ErrorWaitTimeTp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ErrorWaitTimeTp.Location = new System.Drawing.Point(176, 152);
            this.ErrorWaitTimeTp.Name = "ErrorWaitTimeTp";
            this.helpProvider1.SetShowHelp(this.ErrorWaitTimeTp, true);
            this.ErrorWaitTimeTp.ShowUpDown = true;
            this.ErrorWaitTimeTp.Size = new System.Drawing.Size(168, 20);
            this.ErrorWaitTimeTp.TabIndex = 5;
            // 
            // TransactionStartTimeLbl
            // 
            this.TransactionStartTimeLbl.Location = new System.Drawing.Point(12, 100);
            this.TransactionStartTimeLbl.Name = "TransactionStartTimeLbl";
            this.TransactionStartTimeLbl.Size = new System.Drawing.Size(156, 16);
            this.TransactionStartTimeLbl.TabIndex = 15;
            this.TransactionStartTimeLbl.Text = "TransactionStartTimeLbl";
            // 
            // UpdateFrequencyLbl
            // 
            this.UpdateFrequencyLbl.Location = new System.Drawing.Point(12, 44);
            this.UpdateFrequencyLbl.Name = "UpdateFrequencyLbl";
            this.UpdateFrequencyLbl.Size = new System.Drawing.Size(156, 16);
            this.UpdateFrequencyLbl.TabIndex = 19;
            this.UpdateFrequencyLbl.Text = "FrequencyLbl";
            // 
            // ErrorTryCountLbl
            // 
            this.ErrorTryCountLbl.Location = new System.Drawing.Point(12, 128);
            this.ErrorTryCountLbl.Name = "ErrorTryCountLbl";
            this.ErrorTryCountLbl.Size = new System.Drawing.Size(164, 16);
            this.ErrorTryCountLbl.TabIndex = 29;
            this.ErrorTryCountLbl.Text = "ErrorTryCountLbl";
            // 
            // ActionCb
            // 
            this.ActionCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionCb.ItemHeight = 13;
            this.ActionCb.Location = new System.Drawing.Point(176, 12);
            this.ActionCb.Name = "ActionCb";
            this.helpProvider1.SetShowHelp(this.ActionCb, true);
            this.ActionCb.Size = new System.Drawing.Size(168, 21);
            this.ActionCb.TabIndex = 0;
            // 
            // ErrorTryCountTb
            // 
            this.ErrorTryCountTb.Location = new System.Drawing.Point(176, 124);
            this.ErrorTryCountTb.Name = "ErrorTryCountTb";
            this.helpProvider1.SetShowHelp(this.ErrorTryCountTb, true);
            this.ErrorTryCountTb.Size = new System.Drawing.Size(168, 20);
            this.ErrorTryCountTb.TabIndex = 4;
            this.ErrorTryCountTb.Text = "ErrorTryCountTb";
            // 
            // ErrorWaitTimeLbl
            // 
            this.ErrorWaitTimeLbl.Location = new System.Drawing.Point(12, 156);
            this.ErrorWaitTimeLbl.Name = "ErrorWaitTimeLbl";
            this.ErrorWaitTimeLbl.Size = new System.Drawing.Size(156, 16);
            this.ErrorWaitTimeLbl.TabIndex = 28;
            this.ErrorWaitTimeLbl.Text = "ErrorWaitTimeLbl";
            // 
            // TransactionEndTimeLbl
            // 
            this.TransactionEndTimeLbl.Location = new System.Drawing.Point(12, 128);
            this.TransactionEndTimeLbl.Name = "TransactionEndTimeLbl";
            this.TransactionEndTimeLbl.Size = new System.Drawing.Size(156, 16);
            this.TransactionEndTimeLbl.TabIndex = 17;
            this.TransactionEndTimeLbl.Text = "TransactionEndTimeLbl";
            // 
            // TransactionCountLbl
            // 
            this.TransactionCountLbl.Location = new System.Drawing.Point(12, 72);
            this.TransactionCountLbl.Name = "TransactionCountLbl";
            this.TransactionCountLbl.Size = new System.Drawing.Size(156, 16);
            this.TransactionCountLbl.TabIndex = 18;
            this.TransactionCountLbl.Text = "TransactionCountLbl";
            // 
            // TransactionCountTb
            // 
            this.TransactionCountTb.Enabled = false;
            this.TransactionCountTb.Location = new System.Drawing.Point(192, 68);
            this.TransactionCountTb.Name = "TransactionCountTb";
            this.helpProvider1.SetShowHelp(this.TransactionCountTb, true);
            this.TransactionCountTb.Size = new System.Drawing.Size(152, 20);
            this.TransactionCountTb.TabIndex = 3;
            this.TransactionCountTb.Text = "TransactionCountTb";
            // 
            // DayMonCb
            // 
            this.DayMonCb.Location = new System.Drawing.Point(176, 208);
            this.DayMonCb.Name = "DayMonCb";
            this.DayMonCb.Size = new System.Drawing.Size(48, 20);
            this.DayMonCb.TabIndex = 9;
            // 
            // TransactionStartTimeTp
            // 
            this.TransactionStartTimeTp.CustomFormat = "";
            this.TransactionStartTimeTp.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TransactionStartTimeTp.Location = new System.Drawing.Point(176, 96);
            this.TransactionStartTimeTp.Name = "TransactionStartTimeTp";
            this.TransactionStartTimeTp.ShowCheckBox = true;
            this.helpProvider1.SetShowHelp(this.TransactionStartTimeTp, true);
            this.TransactionStartTimeTp.ShowUpDown = true;
            this.TransactionStartTimeTp.Size = new System.Drawing.Size(168, 20);
            this.TransactionStartTimeTp.TabIndex = 4;
            this.TransactionStartTimeTp.Value = new System.DateTime(2006, 3, 17, 16, 4, 2, 0);
            // 
            // IntervalLbl
            // 
            this.IntervalLbl.Location = new System.Drawing.Point(12, 184);
            this.IntervalLbl.Name = "IntervalLbl";
            this.IntervalLbl.Size = new System.Drawing.Size(156, 16);
            this.IntervalLbl.TabIndex = 23;
            this.IntervalLbl.Text = "IntervalLbl";
            // 
            // DayOfWeekLbl
            // 
            this.DayOfWeekLbl.Location = new System.Drawing.Point(12, 212);
            this.DayOfWeekLbl.Name = "DayOfWeekLbl";
            this.DayOfWeekLbl.Size = new System.Drawing.Size(156, 16);
            this.DayOfWeekLbl.TabIndex = 21;
            this.DayOfWeekLbl.Text = "DayOfWeekLbl";
            // 
            // ScheduleStartDateTp
            // 
            this.ScheduleStartDateTp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ScheduleStartDateTp.Location = new System.Drawing.Point(176, 68);
            this.ScheduleStartDateTp.Name = "ScheduleStartDateTp";
            this.helpProvider1.SetShowHelp(this.ScheduleStartDateTp, true);
            this.ScheduleStartDateTp.Size = new System.Drawing.Size(168, 20);
            this.ScheduleStartDateTp.TabIndex = 2;
            this.ScheduleStartDateTp.Value = new System.DateTime(2006, 12, 22, 16, 4, 0, 0);
            // 
            // ActionLbl
            // 
            this.ActionLbl.Location = new System.Drawing.Point(12, 16);
            this.ActionLbl.Name = "ActionLbl";
            this.ActionLbl.Size = new System.Drawing.Size(156, 16);
            this.ActionLbl.TabIndex = 24;
            this.ActionLbl.Text = "ActionLbl";
            // 
            // TransactionEndTimeTp
            // 
            this.TransactionEndTimeTp.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TransactionEndTimeTp.Location = new System.Drawing.Point(176, 124);
            this.TransactionEndTimeTp.Name = "TransactionEndTimeTp";
            this.TransactionEndTimeTp.ShowCheckBox = true;
            this.helpProvider1.SetShowHelp(this.TransactionEndTimeTp, true);
            this.TransactionEndTimeTp.ShowUpDown = true;
            this.TransactionEndTimeTp.Size = new System.Drawing.Size(168, 20);
            this.TransactionEndTimeTp.TabIndex = 5;
            this.TransactionEndTimeTp.Value = new System.DateTime(2006, 3, 17, 16, 4, 2, 0);
            // 
            // ScheduleStartTimeLbl
            // 
            this.ScheduleStartTimeLbl.Location = new System.Drawing.Point(12, 72);
            this.ScheduleStartTimeLbl.Name = "ScheduleStartTimeLbl";
            this.ScheduleStartTimeLbl.Size = new System.Drawing.Size(156, 16);
            this.ScheduleStartTimeLbl.TabIndex = 26;
            this.ScheduleStartTimeLbl.Text = "ScheduleStartTimeLbl";
            // 
            // NameTb
            // 
            this.NameTb.Location = new System.Drawing.Point(176, 12);
            this.NameTb.Name = "NameTb";
            this.helpProvider1.SetShowHelp(this.NameTb, true);
            this.NameTb.Size = new System.Drawing.Size(168, 20);
            this.NameTb.TabIndex = 0;
            this.NameTb.Text = "NameTb";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ScheduleTab);
            this.tabControl1.Controls.Add(this.TransactionTab);
            this.tabControl1.Controls.Add(this.TargetTab);
            this.tabControl1.ItemSize = new System.Drawing.Size(76, 18);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 322);
            this.tabControl1.TabIndex = 103;
            // 
            // ScheduleTab
            // 
            this.ScheduleTab.Controls.Add(this.ErrorWaitTimeTp);
            this.ScheduleTab.Controls.Add(this.DayOfMonthTb);
            this.ScheduleTab.Controls.Add(this.ErrorTryCountLbl);
            this.ScheduleTab.Controls.Add(this.ErrorTryCountTb);
            this.ScheduleTab.Controls.Add(this.ErrorWaitTimeLbl);
            this.ScheduleTab.Controls.Add(this.DayMonCb);
            this.ScheduleTab.Controls.Add(this.IntervalLbl);
            this.ScheduleTab.Controls.Add(this.DayOfWeekLbl);
            this.ScheduleTab.Controls.Add(this.ScheduleStartDateTp);
            this.ScheduleTab.Controls.Add(this.ScheduleStartTimeLbl);
            this.ScheduleTab.Controls.Add(this.NameTb);
            this.ScheduleTab.Controls.Add(this.ScheduleEndTimeLbl);
            this.ScheduleTab.Controls.Add(this.RepeatModeCb);
            this.ScheduleTab.Controls.Add(this.ScheduleEndDateTp);
            this.ScheduleTab.Controls.Add(this.NameLbl);
            this.ScheduleTab.Controls.Add(this.RepeatModeLbl);
            this.ScheduleTab.Controls.Add(this.DaySunCb);
            this.ScheduleTab.Controls.Add(this.DaySatCb);
            this.ScheduleTab.Controls.Add(this.DayFriCb);
            this.ScheduleTab.Controls.Add(this.DayThuCb);
            this.ScheduleTab.Controls.Add(this.DayWedCb);
            this.ScheduleTab.Controls.Add(this.DayTueCb);
            this.ScheduleTab.Controls.Add(this.DayOfMonthLbl);
            this.ScheduleTab.Controls.Add(this.IntervalTb);
            this.ScheduleTab.Location = new System.Drawing.Point(4, 22);
            this.ScheduleTab.Name = "ScheduleTab";
            this.ScheduleTab.Size = new System.Drawing.Size(352, 296);
            this.ScheduleTab.TabIndex = 1;
            this.ScheduleTab.Text = "ScheduleTab";
            // 
            // ScheduleEndTimeLbl
            // 
            this.ScheduleEndTimeLbl.Location = new System.Drawing.Point(12, 100);
            this.ScheduleEndTimeLbl.Name = "ScheduleEndTimeLbl";
            this.ScheduleEndTimeLbl.Size = new System.Drawing.Size(156, 16);
            this.ScheduleEndTimeLbl.TabIndex = 27;
            this.ScheduleEndTimeLbl.Text = "ScheduleEndTimeLbl";
            // 
            // RepeatModeCb
            // 
            this.RepeatModeCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RepeatModeCb.ItemHeight = 13;
            this.RepeatModeCb.Location = new System.Drawing.Point(176, 40);
            this.RepeatModeCb.Name = "RepeatModeCb";
            this.helpProvider1.SetShowHelp(this.RepeatModeCb, true);
            this.RepeatModeCb.Size = new System.Drawing.Size(168, 21);
            this.RepeatModeCb.TabIndex = 1;
            this.RepeatModeCb.SelectedIndexChanged += new System.EventHandler(this.RepeatModeCb_SelectedIndexChanged);
            // 
            // ScheduleEndDateTp
            // 
            this.ScheduleEndDateTp.Checked = false;
            this.ScheduleEndDateTp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ScheduleEndDateTp.Location = new System.Drawing.Point(176, 96);
            this.ScheduleEndDateTp.Name = "ScheduleEndDateTp";
            this.ScheduleEndDateTp.ShowCheckBox = true;
            this.helpProvider1.SetShowHelp(this.ScheduleEndDateTp, true);
            this.ScheduleEndDateTp.Size = new System.Drawing.Size(168, 20);
            this.ScheduleEndDateTp.TabIndex = 3;
            this.ScheduleEndDateTp.Value = new System.DateTime(2006, 12, 22, 16, 4, 0, 0);
            // 
            // NameLbl
            // 
            this.NameLbl.Location = new System.Drawing.Point(12, 16);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(156, 16);
            this.NameLbl.TabIndex = 16;
            this.NameLbl.Text = "NameLbl";
            // 
            // RepeatModeLbl
            // 
            this.RepeatModeLbl.Location = new System.Drawing.Point(12, 44);
            this.RepeatModeLbl.Name = "RepeatModeLbl";
            this.RepeatModeLbl.Size = new System.Drawing.Size(156, 16);
            this.RepeatModeLbl.TabIndex = 20;
            this.RepeatModeLbl.Text = "RepeatModeLbl";
            // 
            // DaySunCb
            // 
            this.DaySunCb.Location = new System.Drawing.Point(292, 232);
            this.DaySunCb.Name = "DaySunCb";
            this.helpProvider1.SetShowHelp(this.DaySunCb, true);
            this.DaySunCb.Size = new System.Drawing.Size(48, 20);
            this.DaySunCb.TabIndex = 15;
            // 
            // DaySatCb
            // 
            this.DaySatCb.Location = new System.Drawing.Point(232, 232);
            this.DaySatCb.Name = "DaySatCb";
            this.helpProvider1.SetShowHelp(this.DaySatCb, true);
            this.DaySatCb.Size = new System.Drawing.Size(48, 20);
            this.DaySatCb.TabIndex = 14;
            // 
            // DayFriCb
            // 
            this.DayFriCb.Location = new System.Drawing.Point(176, 232);
            this.DayFriCb.Name = "DayFriCb";
            this.helpProvider1.SetShowHelp(this.DayFriCb, true);
            this.DayFriCb.Size = new System.Drawing.Size(48, 20);
            this.DayFriCb.TabIndex = 13;
            // 
            // DayThuCb
            // 
            this.DayThuCb.Location = new System.Drawing.Point(124, 232);
            this.DayThuCb.Name = "DayThuCb";
            this.helpProvider1.SetShowHelp(this.DayThuCb, true);
            this.DayThuCb.Size = new System.Drawing.Size(48, 20);
            this.DayThuCb.TabIndex = 12;
            // 
            // DayWedCb
            // 
            this.DayWedCb.Location = new System.Drawing.Point(292, 208);
            this.DayWedCb.Name = "DayWedCb";
            this.DayWedCb.Size = new System.Drawing.Size(48, 20);
            this.DayWedCb.TabIndex = 11;
            // 
            // DayTueCb
            // 
            this.DayTueCb.Location = new System.Drawing.Point(232, 208);
            this.DayTueCb.Name = "DayTueCb";
            this.DayTueCb.Size = new System.Drawing.Size(48, 20);
            this.DayTueCb.TabIndex = 10;
            // 
            // DayOfMonthLbl
            // 
            this.DayOfMonthLbl.Location = new System.Drawing.Point(12, 212);
            this.DayOfMonthLbl.Name = "DayOfMonthLbl";
            this.DayOfMonthLbl.Size = new System.Drawing.Size(156, 16);
            this.DayOfMonthLbl.TabIndex = 22;
            this.DayOfMonthLbl.Text = "DayOfMonthLbl";
            // 
            // IntervalTb
            // 
            this.IntervalTb.Location = new System.Drawing.Point(176, 180);
            this.IntervalTb.Name = "IntervalTb";
            this.helpProvider1.SetShowHelp(this.IntervalTb, true);
            this.IntervalTb.Size = new System.Drawing.Size(168, 20);
            this.IntervalTb.TabIndex = 7;
            this.IntervalTb.Text = "IntervalTb";
            // 
            // TransactionTab
            // 
            this.TransactionTab.Controls.Add(this.UpdateFrequencyTp);
            this.TransactionTab.Controls.Add(this.ConnectionDelayTimeTp);
            this.TransactionTab.Controls.Add(this.ConnectionDelayTimeLbl);
            this.TransactionTab.Controls.Add(this.ConnectionFailWaitTimeTp);
            this.TransactionTab.Controls.Add(this.ConnectionFailTryCountTb);
            this.TransactionTab.Controls.Add(this.ConnectionFailTryCountLbl);
            this.TransactionTab.Controls.Add(this.ConnectionFailWaitTimeLbl);
            this.TransactionTab.Controls.Add(this.MaxThreadCountTb);
            this.TransactionTab.Controls.Add(this.MaxThreadCountLbl);
            this.TransactionTab.Controls.Add(this.TransactionCountCb);
            this.TransactionTab.Controls.Add(this.ActionLbl);
            this.TransactionTab.Controls.Add(this.TransactionEndTimeTp);
            this.TransactionTab.Controls.Add(this.TransactionStartTimeTp);
            this.TransactionTab.Controls.Add(this.ActionCb);
            this.TransactionTab.Controls.Add(this.UpdateFrequencyLbl);
            this.TransactionTab.Controls.Add(this.TransactionStartTimeLbl);
            this.TransactionTab.Controls.Add(this.TransactionEndTimeLbl);
            this.TransactionTab.Controls.Add(this.TransactionCountLbl);
            this.TransactionTab.Controls.Add(this.TransactionCountTb);
            this.TransactionTab.Location = new System.Drawing.Point(4, 22);
            this.TransactionTab.Name = "TransactionTab";
            this.TransactionTab.Size = new System.Drawing.Size(352, 296);
            this.TransactionTab.TabIndex = 0;
            this.TransactionTab.Text = "TransactionTab";
            // 
            // UpdateFrequencyTp
            // 
            this.UpdateFrequencyTp.CustomFormat = "HH:mm:ss";
            this.UpdateFrequencyTp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.UpdateFrequencyTp.Location = new System.Drawing.Point(176, 40);
            this.UpdateFrequencyTp.Name = "UpdateFrequencyTp";
            this.helpProvider1.SetShowHelp(this.UpdateFrequencyTp, true);
            this.UpdateFrequencyTp.ShowUpDown = true;
            this.UpdateFrequencyTp.Size = new System.Drawing.Size(168, 20);
            this.UpdateFrequencyTp.TabIndex = 40;
            this.UpdateFrequencyTp.Value = new System.DateTime(2006, 3, 17, 16, 4, 2, 0);
            // 
            // ConnectionDelayTimeTp
            // 
            this.ConnectionDelayTimeTp.CustomFormat = "HH:mm:ss";
            this.ConnectionDelayTimeTp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ConnectionDelayTimeTp.Location = new System.Drawing.Point(176, 152);
            this.ConnectionDelayTimeTp.Name = "ConnectionDelayTimeTp";
            this.helpProvider1.SetShowHelp(this.ConnectionDelayTimeTp, true);
            this.ConnectionDelayTimeTp.ShowUpDown = true;
            this.ConnectionDelayTimeTp.Size = new System.Drawing.Size(168, 20);
            this.ConnectionDelayTimeTp.TabIndex = 6;
            // 
            // ConnectionDelayTimeLbl
            // 
            this.ConnectionDelayTimeLbl.Location = new System.Drawing.Point(12, 156);
            this.ConnectionDelayTimeLbl.Name = "ConnectionDelayTimeLbl";
            this.ConnectionDelayTimeLbl.Size = new System.Drawing.Size(164, 16);
            this.ConnectionDelayTimeLbl.TabIndex = 39;
            this.ConnectionDelayTimeLbl.Text = "ConnectionDelayTimeLbl";
            // 
            // ConnectionFailWaitTimeTp
            // 
            this.ConnectionFailWaitTimeTp.CustomFormat = "HH:mm:ss";
            this.ConnectionFailWaitTimeTp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ConnectionFailWaitTimeTp.Location = new System.Drawing.Point(176, 252);
            this.ConnectionFailWaitTimeTp.Name = "ConnectionFailWaitTimeTp";
            this.helpProvider1.SetShowHelp(this.ConnectionFailWaitTimeTp, true);
            this.ConnectionFailWaitTimeTp.ShowUpDown = true;
            this.ConnectionFailWaitTimeTp.Size = new System.Drawing.Size(168, 20);
            this.ConnectionFailWaitTimeTp.TabIndex = 9;
            // 
            // ConnectionFailTryCountTb
            // 
            this.ConnectionFailTryCountTb.Location = new System.Drawing.Point(176, 216);
            this.ConnectionFailTryCountTb.Name = "ConnectionFailTryCountTb";
            this.helpProvider1.SetShowHelp(this.ConnectionFailTryCountTb, true);
            this.ConnectionFailTryCountTb.Size = new System.Drawing.Size(168, 20);
            this.ConnectionFailTryCountTb.TabIndex = 8;
            this.ConnectionFailTryCountTb.Text = "ConnectionFailTryCountTb";
            // 
            // ConnectionFailTryCountLbl
            // 
            this.ConnectionFailTryCountLbl.Location = new System.Drawing.Point(12, 220);
            this.ConnectionFailTryCountLbl.Name = "ConnectionFailTryCountLbl";
            this.ConnectionFailTryCountLbl.Size = new System.Drawing.Size(156, 28);
            this.ConnectionFailTryCountLbl.TabIndex = 37;
            this.ConnectionFailTryCountLbl.Text = "ConnectionFailTryCountLbl";
            // 
            // ConnectionFailWaitTimeLbl
            // 
            this.ConnectionFailWaitTimeLbl.Location = new System.Drawing.Point(12, 256);
            this.ConnectionFailWaitTimeLbl.Name = "ConnectionFailWaitTimeLbl";
            this.ConnectionFailWaitTimeLbl.Size = new System.Drawing.Size(156, 28);
            this.ConnectionFailWaitTimeLbl.TabIndex = 35;
            this.ConnectionFailWaitTimeLbl.Text = "ConnectionFailWaitTimeLbl";
            // 
            // MaxThreadCountTb
            // 
            this.MaxThreadCountTb.Location = new System.Drawing.Point(176, 180);
            this.MaxThreadCountTb.Name = "MaxThreadCountTb";
            this.helpProvider1.SetShowHelp(this.MaxThreadCountTb, true);
            this.MaxThreadCountTb.Size = new System.Drawing.Size(168, 20);
            this.MaxThreadCountTb.TabIndex = 7;
            // 
            // MaxThreadCountLbl
            // 
            this.MaxThreadCountLbl.Location = new System.Drawing.Point(12, 184);
            this.MaxThreadCountLbl.Name = "MaxThreadCountLbl";
            this.MaxThreadCountLbl.Size = new System.Drawing.Size(156, 28);
            this.MaxThreadCountLbl.TabIndex = 33;
            this.MaxThreadCountLbl.Text = "MaxThreadCountLbl";
            // 
            // TransactionCountCb
            // 
            this.TransactionCountCb.Location = new System.Drawing.Point(176, 68);
            this.TransactionCountCb.Name = "TransactionCountCb";
            this.helpProvider1.SetShowHelp(this.TransactionCountCb, true);
            this.TransactionCountCb.Size = new System.Drawing.Size(12, 20);
            this.TransactionCountCb.TabIndex = 2;
            this.TransactionCountCb.CheckStateChanged += new System.EventHandler(this.TransactionCountCb_CheckedChanged);
            // 
            // TargetTab
            // 
            this.TargetTab.Controls.Add(this.TargetCheckTree);
            this.TargetTab.Location = new System.Drawing.Point(4, 22);
            this.TargetTab.Name = "TargetTab";
            this.TargetTab.Size = new System.Drawing.Size(352, 296);
            this.TargetTab.TabIndex = 2;
            this.TargetTab.Text = "TargetTab";
            // 
            // TargetCheckTree
            // 
            this.TargetCheckTree.CheckBoxes = true;
            this.TargetCheckTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TargetCheckTree.Location = new System.Drawing.Point(0, 0);
            this.TargetCheckTree.Name = "TargetCheckTree";
            this.helpProvider1.SetShowHelp(this.TargetCheckTree, true);
            this.TargetCheckTree.Size = new System.Drawing.Size(352, 296);
            this.TargetCheckTree.TabIndex = 0;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(297, 335);
            this.CancelBtn.Name = "CancelBtn";
            this.helpProvider1.SetShowHelp(this.CancelBtn, true);
            this.CancelBtn.Size = new System.Drawing.Size(64, 24);
            this.CancelBtn.TabIndex = 105;
            this.CancelBtn.Text = "Cancel";
            // 
            // GXScheduleEditorDlg
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(370, 365);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GXScheduleEditorDlg";
            this.helpProvider1.SetShowHelp(this, false);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GXScheduleEditorDlg";
            this.Load += new System.EventHandler(this.GXScheduleEditorDlg_Load);
            this.tabControl1.ResumeLayout(false);
            this.ScheduleTab.ResumeLayout(false);
            this.ScheduleTab.PerformLayout();
            this.TransactionTab.ResumeLayout(false);
            this.TransactionTab.PerformLayout();
            this.TargetTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox DayOfMonthTb;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.Button OKBtn;
		private System.Windows.Forms.DateTimePicker ErrorWaitTimeTp;
		private System.Windows.Forms.Label TransactionStartTimeLbl;
		private System.Windows.Forms.Label UpdateFrequencyLbl;
		private System.Windows.Forms.Label ErrorTryCountLbl;
		private System.Windows.Forms.ComboBox ActionCb;
		private System.Windows.Forms.TextBox ErrorTryCountTb;
		private System.Windows.Forms.Label ErrorWaitTimeLbl;
		private System.Windows.Forms.Label TransactionEndTimeLbl;
		private System.Windows.Forms.Label TransactionCountLbl;
		private System.Windows.Forms.TextBox TransactionCountTb;
		private System.Windows.Forms.CheckBox DayMonCb;
		private System.Windows.Forms.DateTimePicker TransactionStartTimeTp;
		private System.Windows.Forms.Label IntervalLbl;
		private System.Windows.Forms.Label DayOfWeekLbl;
		private System.Windows.Forms.DateTimePicker ScheduleStartDateTp;
		private System.Windows.Forms.Label ActionLbl;
		private System.Windows.Forms.DateTimePicker TransactionEndTimeTp;
		private System.Windows.Forms.Label ScheduleStartTimeLbl;
		private System.Windows.Forms.TextBox NameTb;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage ScheduleTab;
		private System.Windows.Forms.Label ScheduleEndTimeLbl;
		private System.Windows.Forms.ComboBox RepeatModeCb;
		private System.Windows.Forms.DateTimePicker ScheduleEndDateTp;
		private System.Windows.Forms.Label NameLbl;
		private System.Windows.Forms.Label RepeatModeLbl;
		private System.Windows.Forms.CheckBox DaySunCb;
		private System.Windows.Forms.CheckBox DaySatCb;
		private System.Windows.Forms.CheckBox DayFriCb;
		private System.Windows.Forms.CheckBox DayThuCb;
		private System.Windows.Forms.CheckBox DayWedCb;
		private System.Windows.Forms.CheckBox DayTueCb;
		private System.Windows.Forms.Label DayOfMonthLbl;
		private System.Windows.Forms.TextBox IntervalTb;
		private System.Windows.Forms.TabPage TransactionTab;
		private System.Windows.Forms.DateTimePicker UpdateFrequencyTp;
		private System.Windows.Forms.DateTimePicker ConnectionDelayTimeTp;
		private System.Windows.Forms.Label ConnectionDelayTimeLbl;
		private System.Windows.Forms.DateTimePicker ConnectionFailWaitTimeTp;
		private System.Windows.Forms.TextBox ConnectionFailTryCountTb;
		private System.Windows.Forms.Label ConnectionFailTryCountLbl;
		private System.Windows.Forms.Label ConnectionFailWaitTimeLbl;
		private System.Windows.Forms.TextBox MaxThreadCountTb;
		private System.Windows.Forms.Label MaxThreadCountLbl;
		private System.Windows.Forms.CheckBox TransactionCountCb;
		private System.Windows.Forms.TabPage TargetTab;
		private System.Windows.Forms.TreeView TargetCheckTree;
		private System.Windows.Forms.Button CancelBtn;

	}
}