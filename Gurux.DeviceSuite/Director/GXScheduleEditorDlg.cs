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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gurux.Device;
using Gurux.Common;
using System.Collections;
using System.Globalization;

namespace Gurux.DeviceSuite.Director
{
	public partial class GXScheduleEditorDlg : Form
	{
		public GXSchedule m_ScheduleItem = null;
		private GXDeviceList m_DeviceList = null;

		public GXScheduleEditorDlg()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Returns a localized string conversion of a ScheduleRepeat value.
		/// </summary>
		/// <param name="repeatMode">Enum value to convert to string.</param>
		/// <returns>Localized version of enum value as a string.</returns>
		public static string ScheduleRepeatToString(ScheduleRepeat repeatMode)
		{
			switch (repeatMode)
			{
				case ScheduleRepeat.Second:
					return Gurux.DeviceSuite.Properties.Resources.SecondTxt;
				case ScheduleRepeat.Minute:
					return Gurux.DeviceSuite.Properties.Resources.MinuteTxt;
				case ScheduleRepeat.Hour:
					return Gurux.DeviceSuite.Properties.Resources.HourTxt;
				case ScheduleRepeat.Day:
					return Gurux.DeviceSuite.Properties.Resources.DailyTxt;
				case ScheduleRepeat.Month:
					return Gurux.DeviceSuite.Properties.Resources.MonthlyTxt;
				case ScheduleRepeat.Once:
					return Gurux.DeviceSuite.Properties.Resources.OnceTxt;
				case ScheduleRepeat.Week:
					return Gurux.DeviceSuite.Properties.Resources.WeeklyTxt;
				default:
					throw new Exception("ScheduleRepeatModeToString failed: Unknown ScheduleRepeat " + repeatMode.ToString());
			}
		}

		/// <summary>
		/// Returns a localized string conversion of a ScheduleAction value.
		/// </summary>
		/// <param name="action">Enum value to convert to string.</param>
		/// <returns>Localized version of enum value as a string.</returns>
		public static string ScheduleActionToString(ScheduleAction action)
		{
			switch (action)
			{
				case ScheduleAction.Read:
					return Gurux.DeviceSuite.Properties.Resources.ReadTxt;
				case ScheduleAction.Write:
					return Gurux.DeviceSuite.Properties.Resources.WriteTxt;
				default:
					throw new Exception("GXActionToString failed: Unknown ScheduleAction " + action.ToString());
			}
		}

        /// <summary>
        /// Update selected target(s) from tree.
        /// </summary>
        /// <returns>Returns true if atleast one item is selected.</returns>
		private bool SetScheduleTarget(GXSchedule scheduleItem, TreeNode deviceListNode)
		{
            scheduleItem.Items.Clear();
            scheduleItem.ExcludedItems.Clear();
            //Get included and excluded items from the tree
			Queue q = new Queue();
			q.Enqueue(deviceListNode);
			while (q.Count > 0)
			{
				TreeNode Node = (TreeNode)q.Dequeue();
				if ((Node.Parent == null || !Node.Parent.Checked) && Node.Checked)
				{
                    scheduleItem.Items.Add(Node.Tag);
				}
				if (Node.Parent != null && Node.Parent.Checked && !Node.Checked)
				{
                    scheduleItem.ExcludedItems.Add(Node.Tag);
				}
				foreach (TreeNode ChildNode in Node.Nodes)
				{
					q.Enqueue(ChildNode);
				}
			}
            return (scheduleItem.Items.Count > 0);
		}

		private void FillEnums()
		{
			RepeatModeCb.Items.Clear();
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Once));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Second));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Minute));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Hour));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Week));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Day));
			RepeatModeCb.Items.Add(ScheduleRepeatToString(ScheduleRepeat.Month));
			ActionCb.Items.Clear();
			ActionCb.Items.Add(ScheduleActionToString(ScheduleAction.Read));
			ActionCb.Items.Add(ScheduleActionToString(ScheduleAction.Write));
		}

		public GXScheduleEditorDlg(GXSchedule scheduleItem, GXDeviceList deviceList)
		{
			InitializeComponent();
			try
			{
				if (scheduleItem == null)
				{
					scheduleItem = new GXSchedule();
					scheduleItem.ScheduleStartTime = DateTime.Now;
					scheduleItem.ScheduleEndTime = DateTime.MaxValue;
					scheduleItem.TransactionStartTime = DateTime.MinValue;
                    scheduleItem.TransactionEndTime = DateTime.MaxValue;
				}
				m_DeviceList = deviceList;
				m_ScheduleItem = scheduleItem;
				if (System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek == DayOfWeek.Sunday)
				{
					Point TmpLoc = new Point(DaySunCb.Location.X, DaySunCb.Location.Y);
					DaySunCb.Location = DayMonCb.Location;
					DayMonCb.Location = DayTueCb.Location;
					DayTueCb.Location = DayWedCb.Location;
					DayWedCb.Location = DayThuCb.Location;
					DayThuCb.Location = DayFriCb.Location;
					DayFriCb.Location = DaySatCb.Location;
					DaySatCb.Location = TmpLoc;
				}
				FillEnums();                
				UpdateResouces();				

				NameTb.Text = m_ScheduleItem.Name;
                if (m_ScheduleItem.TransactionStartTime != DateTime.MinValue)
                {
                    TransactionStartTimeTp.Value = m_ScheduleItem.TransactionStartTime;
                }
                else
                {
                    TransactionStartTimeTp.Checked = false;
                }
                if (m_ScheduleItem.TransactionEndTime != DateTime.MaxValue)
                {
                    TransactionEndTimeTp.Value = m_ScheduleItem.TransactionEndTime;
                }
                //Has to be done to uncheck, known issue in .NET
				TransactionEndTimeTp.Checked = true; //Has to be done to uncheck, known issue in .NET
				TransactionEndTimeTp.Checked = m_ScheduleItem.TransactionEndTime != DateTime.MaxValue;

                if (m_ScheduleItem.TransactionEndTime != DateTime.MaxValue)
                {
                    TransactionEndTimeTp.Value = m_ScheduleItem.TransactionEndTime;
                }
                if (m_ScheduleItem.TransactionCount == 0)
				{
					TransactionCountCb.Checked = false;
					TransactionCountTb.Text = "";
				}
				else
				{
					TransactionCountCb.Checked = true;
					TransactionCountTb.Text = m_ScheduleItem.TransactionCount.ToString();
				}
				UpdateFrequencyTp.Value = new DateTime(((long)m_ScheduleItem.UpdateInterval) * 10000000 + UpdateFrequencyTp.MinDate.Ticks);
				RepeatModeCb.SelectedItem = ScheduleRepeatToString(m_ScheduleItem.RepeatMode);
				DayOfMonthTb.Text = m_ScheduleItem.DayOfMonth.ToString();
				IntervalTb.Text = m_ScheduleItem.Interval.ToString();
				ActionCb.SelectedItem = ScheduleActionToString(m_ScheduleItem.Action);
                if (m_ScheduleItem.ScheduleStartTime != DateTime.MinValue)
                {
                    ScheduleStartDateTp.Value = m_ScheduleItem.ScheduleStartTime;
                }
                if (m_ScheduleItem.ScheduleEndTime != DateTime.MaxValue)
                {
                    ScheduleEndDateTp.Value = m_ScheduleItem.ScheduleEndTime;
                }
                ScheduleEndDateTp.Checked = true; //Has to be done to uncheck, known issue in .NET
                ScheduleEndDateTp.Checked = m_ScheduleItem.ScheduleEndTime != DateTime.MaxValue;
				UpdateTargetCheckTree();

				ErrorWaitTimeTp.Value = new DateTime(ErrorWaitTimeTp.MinDate.Ticks + ((long)m_ScheduleItem.FailWaitTime) * 10000);
				//ErrorWaitTimeTp.Value. = m_ScheduleItem.FailWaitTime/1000.0;
				ErrorTryCountTb.Text = m_ScheduleItem.FailTryCount.ToString();
				ConnectionDelayTimeTp.Value = new DateTime(ConnectionDelayTimeTp.MinDate.Ticks + ((long)m_ScheduleItem.ConnectionDelayTime) * 10000);
				MaxThreadCountTb.Text = m_ScheduleItem.MaxThreadCount.ToString();
				//ConnectionFailWaitTimeTb.Text = Convert.ToString(m_ScheduleItem.ConnectionFailWaitTime/1000.0);
				ConnectionFailWaitTimeTp.Value = new DateTime(ConnectionFailWaitTimeTp.MinDate.Ticks + ((long)m_ScheduleItem.ConnectionFailWaitTime) * 10000);
				ConnectionFailTryCountTb.Text = m_ScheduleItem.ConnectionFailTryCount.ToString();

                if (m_ScheduleItem.DayOfWeeks == null)
                {
                    DayMonCb.Checked = DayTueCb.Checked = DayWedCb.Checked = DayThuCb.Checked = DayFriCb.Checked = DaySatCb.Checked = DaySunCb.Checked = false;
                }
                else
                {
                    System.Collections.Generic.List<DayOfWeek> list = new System.Collections.Generic.List<DayOfWeek>();
                    list.AddRange(m_ScheduleItem.DayOfWeeks);
                    DayMonCb.Checked = list.Contains(DayOfWeek.Monday);
                    DayTueCb.Checked = list.Contains(DayOfWeek.Tuesday);
                    DayWedCb.Checked = list.Contains(DayOfWeek.Wednesday);
                    DayThuCb.Checked = list.Contains(DayOfWeek.Thursday);
                    DayFriCb.Checked = list.Contains(DayOfWeek.Friday);
                    DaySatCb.Checked = list.Contains(DayOfWeek.Saturday);
                    DaySunCb.Checked = list.Contains(DayOfWeek.Sunday);
                }
                OKBtn.Enabled = (m_ScheduleItem.Status & ScheduleState.Run) == 0;
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(this, Ex);
			}
		}

		//Recursive
		private void CreateDeviceGroupTree(GXDeviceGroupCollection deviceGroups, TreeNode rootNode)
		{
			foreach (GXDeviceGroup DeviceGroup in deviceGroups)
			{
				TreeNode GroupNode = new TreeNode(DeviceGroup.Name);
				GroupNode.Tag = DeviceGroup;
				GroupNode.SelectedImageIndex = GroupNode.ImageIndex = 1;
				rootNode.Nodes.Add(GroupNode);
				//Add devices
				foreach (GXDevice Device in DeviceGroup.Devices)
				{
					TreeNode DeviceNode = new TreeNode(Device.Name);
					DeviceNode.Tag = Device;
					DeviceNode.SelectedImageIndex = DeviceNode.ImageIndex = 2;
					GroupNode.Nodes.Add(DeviceNode);
					//Add tables
					foreach (GXTable table in Device.Tables)
					{
						TreeNode TableNode = new TreeNode(table.Name);
						TableNode.Tag = table;
						TableNode.SelectedImageIndex = TableNode.ImageIndex = 3;
						DeviceNode.Nodes.Add(TableNode);

						//Table properties cannot be selected.
					}
					//Add categories
					foreach (GXCategory cat in Device.Categories)
					{
						TreeNode CategoryNode = new TreeNode(cat.Name);
						CategoryNode.Tag = cat;
						CategoryNode.SelectedImageIndex = CategoryNode.ImageIndex = 4;
						DeviceNode.Nodes.Add(CategoryNode);
						//Add category properties
						foreach (GXProperty prop in cat.Properties)
						{
							TreeNode PropNode = new TreeNode(prop.Name);
							PropNode.Tag = prop;
							PropNode.SelectedImageIndex = PropNode.ImageIndex = 5;
							CategoryNode.Nodes.Add(PropNode);
						}
					}
				}
				//Recursion
				CreateDeviceGroupTree(DeviceGroup.DeviceGroups, GroupNode);
			}
		}

		private void LoadImages(ImageList treeImageList)
		{
			treeImageList.Images.Clear();
			//Load the image for the device list image
			System.Drawing.Bitmap bm = Gurux.DeviceSuite.Properties.Resources.DeviceList;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);

			//Load the image for the device group image
			bm = Gurux.DeviceSuite.Properties.Resources.DeviceGroup;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);

			//Load the image for the device connect image
			bm = Gurux.DeviceSuite.Properties.Resources.DeviceConnected;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);

			//Load the image for the device table image
			bm = Gurux.DeviceSuite.Properties.Resources.DeviceTable;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);

			//Load the image for the device category image
			bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategory;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);
			//Load the image for the device property image
			bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperty;
			bm.MakeTransparent();
			treeImageList.Images.Add(bm);
		}

		private void UpdateTargetCheckTree()
		{
			TargetCheckTree.ImageList = new ImageList();
			LoadImages(TargetCheckTree.ImageList);
			TreeNode Node = new TreeNode(m_DeviceList.Name);
			Node.Tag = m_DeviceList;
			Node.SelectedImageIndex = Node.ImageIndex = 0;
			TargetCheckTree.Nodes.Add(Node);
			CreateDeviceGroupTree(m_DeviceList.DeviceGroups, Node);
			SetTargetCheckBoxes();
			TargetCheckTree.ExpandAll();
		}

		private void SetTargetCheckBoxes()
		{
			if (m_ScheduleItem.Items.Contains(this.m_DeviceList))
			{
				TargetCheckTree.Nodes[0].Checked = true;
			}
			Queue q = new Queue();
			q.Enqueue(TargetCheckTree.Nodes[0]);
			while (q.Count > 0)
			{
				TreeNode Node = (TreeNode)q.Dequeue();
				bool Included = false;
				bool Excluded = false;
				object Target = Node.Tag;
				if (Target == null)
				{
					//Do nothing
				}
				if (m_ScheduleItem.Items.Contains(Target))
				{
					Included = true;
				}
				else if (m_ScheduleItem.ExcludedItems.Contains(Target))
				{
					Excluded = true;
				}
				if (Excluded)
				{
					Node.Checked = false;
				}
				else if (Included)
				{
					Node.Checked = true;
				}
				else if (Node.Parent == null)
				{
					Node.Checked = false;
				}
				else
				{
					Node.Checked = Node.Parent.Checked;
				}

				foreach (TreeNode ChildNode in Node.Nodes)
				{
					q.Enqueue(ChildNode);
				}
			}
		}

		private void UpdateResouces()
		{
			DateTime KnownMonday = new DateTime(2006, 3, 13);
			DayMonCb.Text = KnownMonday.ToString("ddd");
			DayTueCb.Text = KnownMonday.AddDays(1).ToString("ddd");
			DayWedCb.Text = KnownMonday.AddDays(2).ToString("ddd");
			DayThuCb.Text = KnownMonday.AddDays(3).ToString("ddd");
			DayFriCb.Text = KnownMonday.AddDays(4).ToString("ddd");
			DaySatCb.Text = KnownMonday.AddDays(5).ToString("ddd");
			DaySunCb.Text = KnownMonday.AddDays(6).ToString("ddd");

			TransactionStartTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.TransactionStartTimeTxt;
			NameLbl.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
			TransactionEndTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.TransactionEndTimeTxt;
			TransactionCountLbl.Text = Gurux.DeviceSuite.Properties.Resources.TransactionCountTxt;
			UpdateFrequencyLbl.Text = Gurux.DeviceSuite.Properties.Resources.UpdateFrequencyTxt;
			RepeatModeLbl.Text = Gurux.DeviceSuite.Properties.Resources.RepeatModeTxt;
			ScheduleEndTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.ScheduleEndTimeTxt;
			ScheduleStartTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.ScheduleStartTimeTxt;
			ActionLbl.Text = Gurux.DeviceSuite.Properties.Resources.ActionTxt;
			IntervalLbl.Text = Gurux.DeviceSuite.Properties.Resources.IntervalTxt;
			DayOfMonthLbl.Text = Gurux.DeviceSuite.Properties.Resources.DayOfMonthTxt;
			DayOfWeekLbl.Text = Gurux.DeviceSuite.Properties.Resources.DayOfWeekTxt;
			TargetTab.Text = Gurux.DeviceSuite.Properties.Resources.TargetTxt;
			OKBtn.Text = Gurux.DeviceSuite.Properties.Resources.OKTxt;
			CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
			ErrorTryCountLbl.Text = Gurux.DeviceSuite.Properties.Resources.ErrorTryCountTxt;
			ErrorWaitTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.ErrorWaitTimeTxt;
			ConnectionDelayTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.ConnectionDelayTimeTxt;
			MaxThreadCountLbl.Text = Gurux.DeviceSuite.Properties.Resources.MaxThreadCountTxt;
			ConnectionFailWaitTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.ConnectionFailWaitTime;
			ConnectionFailTryCountLbl.Text = Gurux.DeviceSuite.Properties.Resources.ConnectionFailTryCountTxt;

			ScheduleTab.Text = Gurux.DeviceSuite.Properties.Resources.ScheduleTxt;
			TransactionTab.Text = Gurux.DeviceSuite.Properties.Resources.TransactionTxt;

			this.Text = Gurux.DeviceSuite.Properties.Resources.ScheduleEditorTxt;

			//Update help resources...
			this.helpProvider1.SetHelpString(this.NameTb, Gurux.DeviceSuite.Properties.Resources.ScheduleNameHelp);
			this.helpProvider1.SetHelpString(this.RepeatModeCb, Gurux.DeviceSuite.Properties.Resources.ScheduleRepeatModeHelp);
			this.helpProvider1.SetHelpString(this.ScheduleStartDateTp, Gurux.DeviceSuite.Properties.Resources.ScheduleStartDateHelp);
			this.helpProvider1.SetHelpString(this.ScheduleEndDateTp, Gurux.DeviceSuite.Properties.Resources.ScheduleEndDateHelp);
			this.helpProvider1.SetHelpString(this.ErrorWaitTimeTp, Gurux.DeviceSuite.Properties.Resources.ScheduleErrorWaitTimeHelp);
			this.helpProvider1.SetHelpString(this.ErrorTryCountTb, Gurux.DeviceSuite.Properties.Resources.ScheduleErrorTryCountHelp);
			this.helpProvider1.SetHelpString(this.ConnectionDelayTimeTp, Gurux.DeviceSuite.Properties.Resources.ScheduleConnectionDelayTimeHelp);
			this.helpProvider1.SetHelpString(this.IntervalTb, Gurux.DeviceSuite.Properties.Resources.ScheduleIntervalHelp);
			this.helpProvider1.SetHelpString(this.DayOfMonthTb, Gurux.DeviceSuite.Properties.Resources.ScheduleDayOfMonthHelp);
			this.helpProvider1.SetHelpString(this.DaySunCb, Gurux.DeviceSuite.Properties.Resources.ScheduleSunHelp);
			this.helpProvider1.SetHelpString(this.DayMonCb, Gurux.DeviceSuite.Properties.Resources.ScheduleMonHelp);
			this.helpProvider1.SetHelpString(this.DayTueCb, Gurux.DeviceSuite.Properties.Resources.ScheduleTueHelp);
			this.helpProvider1.SetHelpString(this.DayWedCb, Gurux.DeviceSuite.Properties.Resources.ScheduleWedHelp);
			this.helpProvider1.SetHelpString(this.DayThuCb, Gurux.DeviceSuite.Properties.Resources.ScheduleThuHelp);
			this.helpProvider1.SetHelpString(this.DayFriCb, Gurux.DeviceSuite.Properties.Resources.ScheduleFriHelp);
			this.helpProvider1.SetHelpString(this.DaySatCb, Gurux.DeviceSuite.Properties.Resources.ScheduleSatHelp);
			this.helpProvider1.SetHelpString(this.ActionCb, Gurux.DeviceSuite.Properties.Resources.ScheduleActionHelp);
			this.helpProvider1.SetHelpString(this.UpdateFrequencyTp, Gurux.DeviceSuite.Properties.Resources.ScheduleUpdateFrequencyHelp);
			this.helpProvider1.SetHelpString(this.TransactionCountCb, Gurux.DeviceSuite.Properties.Resources.ScheduleTransactionCountHelp);
			this.helpProvider1.SetHelpString(this.TransactionCountTb, Gurux.DeviceSuite.Properties.Resources.ScheduleTransactionCountHelp);
			this.helpProvider1.SetHelpString(this.TransactionStartTimeTp, Gurux.DeviceSuite.Properties.Resources.ScheduleTransactionStartTimeHelp);
			this.helpProvider1.SetHelpString(this.TransactionEndTimeTp, Gurux.DeviceSuite.Properties.Resources.ScheduleTransactionEndTimeHelp);
			this.helpProvider1.SetHelpString(this.MaxThreadCountTb, Gurux.DeviceSuite.Properties.Resources.ScheduleMaxThreadCountHelp);
			this.helpProvider1.SetHelpString(this.TargetCheckTree, Gurux.DeviceSuite.Properties.Resources.ScheduleTargetHelp);
			this.helpProvider1.SetHelpString(this.OKBtn, Gurux.DeviceSuite.Properties.Resources.OKHelp);
			this.helpProvider1.SetHelpString(this.CancelBtn, Gurux.DeviceSuite.Properties.Resources.CancelHelp);
		}

		/// <summary>
		/// Converts a string to ScheduleDay. Evaluation uses localized versions of the string.
		/// </summary>
		/// <param name="repeatMode">A string to convert.</param>
		/// <returns>ScheduleAction enum item.</returns>
		public static ScheduleRepeat StringToScheduleRepeat(string repeatMode)
		{
			if (repeatMode == Gurux.DeviceSuite.Properties.Resources.DailyTxt)
			{
				return ScheduleRepeat.Day;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.MonthlyTxt)
			{
				return ScheduleRepeat.Month;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.OnceTxt)
			{
				return ScheduleRepeat.Once;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.WeeklyTxt)
			{
				return ScheduleRepeat.Week;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.SecondTxt)
			{
				return ScheduleRepeat.Second;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.MinuteTxt)
			{
				return ScheduleRepeat.Minute;
			}
			else if (repeatMode == Gurux.DeviceSuite.Properties.Resources.HourTxt)
			{
				return ScheduleRepeat.Hour;
			}
			else
			{
				throw new Exception("StringToGXRepeatMode failed: Unknown string " + repeatMode.ToString());
			}
		}

		/// <summary>
		/// Converts a string to ScheduleAction. Evaluation uses localized versions of the string.
		/// </summary>
		/// <param name="action">A string to convert.</param>
		/// <returns>ScheduleAction enum item.</returns>
		public static ScheduleAction StringToScheduleAction(string action)
		{
			if (action == Gurux.DeviceSuite.Properties.Resources.WriteTxt)
			{
				return ScheduleAction.Write;
			}
			else if (action == Gurux.DeviceSuite.Properties.Resources.ReadTxt)
			{
				return ScheduleAction.Read;
			}
			else
			{
				throw new Exception("StringToGXAction failed: Unknown string " + action.ToString());
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (NameTb.Text.Trim().Length == 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
				}
				bool Monthly = StringToScheduleRepeat(RepeatModeCb.SelectedItem.ToString()) == ScheduleRepeat.Month;
				if ((Convert.ToInt32(DayOfMonthTb.Text) < 1 || Convert.ToInt32(DayOfMonthTb.Text) > 31) && Monthly)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.InvalidDayOfMonthTxt);
				}
				bool MoreThanOnce = StringToScheduleRepeat(RepeatModeCb.SelectedItem.ToString()) != ScheduleRepeat.Once;
				if (int.Parse(IntervalTb.Text) < 1 && MoreThanOnce)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.InvalidIntervalTxt);
				}
				if (!TransactionCountCb.Checked && !TransactionEndTimeTp.Checked)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.SetTransactionCountOrEndTxt);
				}
				if (TransactionCountCb.Checked && int.Parse(TransactionCountTb.Text) < 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.InvalidTransactionCountTxt);
				}
				bool EmptyTarget = !SetScheduleTarget(m_ScheduleItem, TargetCheckTree.Nodes[0]);
				if (EmptyTarget)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.TargetMustBeSetTxt);
				}
				if (TransactionEndTimeTp.Checked && TransactionStartTimeTp.Value > TransactionEndTimeTp.Value)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.TransactionEndBeforeStartTxt);
				}
				if (ScheduleEndDateTp.Checked && ScheduleStartDateTp.Value > ScheduleEndDateTp.Value)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ScheduleEndBeforeStartTxt);
				}
				bool Weekly = StringToScheduleRepeat(RepeatModeCb.SelectedItem.ToString()) == ScheduleRepeat.Week;
				if (Weekly && !(DayMonCb.Checked || DayTueCb.Checked || DayWedCb.Checked || DayThuCb.Checked || DayFriCb.Checked || DaySatCb.Checked || DaySunCb.Checked))
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ScheduleWeekdaysNotSetTxt);
				}
				double tmpResult = -1;

				if (!double.TryParse(ErrorTryCountTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out tmpResult) || tmpResult < 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrorTryCountIncorrectTxt);
				}
				if (!double.TryParse(MaxThreadCountTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out tmpResult) || tmpResult < 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.MaxThreadCountIncorrectTxt);
				}

				if (!double.TryParse(ConnectionFailTryCountTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out tmpResult) || tmpResult < 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ConnectionFailTryCountIncorrectTxt);
				}


				m_ScheduleItem.Name = NameTb.Text;

				m_ScheduleItem.FailWaitTime = (int)((ErrorWaitTimeTp.Value.Ticks - ErrorWaitTimeTp.MinDate.Ticks) / 10000);
				m_ScheduleItem.ConnectionFailWaitTime = (int)((ConnectionFailWaitTimeTp.Value.Ticks - ConnectionFailWaitTimeTp.MinDate.Ticks) / 10000);
				m_ScheduleItem.FailTryCount = Convert.ToInt32(ErrorTryCountTb.Text);
				m_ScheduleItem.ConnectionFailTryCount = Convert.ToInt32(ConnectionFailTryCountTb.Text);
				m_ScheduleItem.MaxThreadCount = Convert.ToInt32(MaxThreadCountTb.Text);
				m_ScheduleItem.ConnectionDelayTime = (int)((ConnectionDelayTimeTp.Value.Ticks - ConnectionDelayTimeTp.MinDate.Ticks) / 10000);
				if (TransactionStartTimeTp.Checked)
				{
					m_ScheduleItem.TransactionStartTime = TransactionStartTimeTp.Value;
				}
				else
				{
					m_ScheduleItem.TransactionStartTime = DateTime.MinValue;
				}

				if (TransactionEndTimeTp.Checked)
				{
					m_ScheduleItem.TransactionEndTime = TransactionEndTimeTp.Value;
				}
				else
				{
					m_ScheduleItem.TransactionEndTime = DateTime.MaxValue;
				}
				if (TransactionCountCb.Checked)
				{
					m_ScheduleItem.TransactionCount = int.Parse(TransactionCountTb.Text);
				}
				else
				{
					m_ScheduleItem.TransactionCount = 0;
				}
				m_ScheduleItem.UpdateInterval = UpdateFrequencyTp.Value.Second + UpdateFrequencyTp.Value.Minute * 60 + UpdateFrequencyTp.Value.Hour * 3600;
				m_ScheduleItem.RepeatMode = StringToScheduleRepeat(RepeatModeCb.SelectedItem.ToString());
				if (Monthly)
				{
					m_ScheduleItem.DayOfMonth = int.Parse(DayOfMonthTb.Text);
				}
				if (MoreThanOnce)
				{
					m_ScheduleItem.Interval = int.Parse(IntervalTb.Text);
				}
				m_ScheduleItem.Action = StringToScheduleAction(ActionCb.SelectedItem.ToString());
				m_ScheduleItem.ScheduleStartTime = ScheduleStartDateTp.Value;
				if (ScheduleEndDateTp.Checked)
				{
					m_ScheduleItem.ScheduleEndTime = ScheduleEndDateTp.Value;
				}
				else
				{
					m_ScheduleItem.ScheduleEndTime = DateTime.MaxValue;
				}

				System.Collections.Generic.List<DayOfWeek> list = new System.Collections.Generic.List<DayOfWeek>();
				if (DayMonCb.Checked)
				{
					list.Add(DayOfWeek.Monday);
				}
				if (DayTueCb.Checked)
				{
					list.Add(DayOfWeek.Tuesday);
				}
				if (DayWedCb.Checked)
				{
					list.Add(DayOfWeek.Wednesday);
				}
				if (DayThuCb.Checked)
				{
					list.Add(DayOfWeek.Thursday);
				}
				if (DayFriCb.Checked)
				{
					list.Add(DayOfWeek.Friday);
				}
				if (DaySatCb.Checked)
				{
					list.Add(DayOfWeek.Saturday);
				}
				if (DaySunCb.Checked)
				{
					list.Add(DayOfWeek.Sunday);
				}
				m_ScheduleItem.DayOfWeeks = list.ToArray();
				foreach (TabPage it in tabControl1.TabPages)
				{
					if (it.Controls.Count > 0 && it.Controls[0] is IGXWizardPage)
					{
						((IGXWizardPage)it.Controls[0]).Finish();
					}
				}

				//Create new item.
				if (m_ScheduleItem.Parent == null)
				{
					m_DeviceList.Schedules.Add(m_ScheduleItem);
				}
				this.Close();
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(this, Ex);
				this.DialogResult = DialogResult.None;
			}
		}

		private void GXScheduleEditorDlg_Load(object sender, EventArgs e)
		{
			NameTb.Select();
		}

		private void RepeatModeCb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool ShowDays = RepeatModeCb.SelectedItem.ToString() == ScheduleRepeatToString(ScheduleRepeat.Week);
			DayOfWeekLbl.Visible = DayMonCb.Visible = DayTueCb.Visible = DayWedCb.Visible = DayThuCb.Visible = DayFriCb.Visible = DaySatCb.Visible = DaySunCb.Visible = ShowDays;
			DayMonCb.Checked = DayTueCb.Checked = DayWedCb.Checked = DayThuCb.Checked = DayFriCb.Checked = DaySatCb.Checked = DaySunCb.Checked = ShowDays;

			bool ShowDayOfMonth = RepeatModeCb.SelectedItem.ToString() == ScheduleRepeatToString(ScheduleRepeat.Month);
			DayOfMonthLbl.Visible = DayOfMonthTb.Visible = ShowDayOfMonth;

			IntervalLbl.Visible = IntervalTb.Visible = RepeatModeCb.SelectedItem.ToString() != ScheduleRepeatToString(ScheduleRepeat.Once);
		}

		private void TransactionCountCb_CheckedChanged(object sender, System.EventArgs e)
		{
			TransactionCountTb.Enabled = TransactionCountCb.Checked;
		}

        /// <summary>
        /// Select or unselect all child items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TargetCheckTree_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.TargetCheckTree.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.TargetCheckTree_AfterCheck);

			try
			{
				if (e.Node.Parent != null && e.Node.Checked && !e.Node.Parent.Checked)
				{
					Queue q = new Queue();
					q.Enqueue(e.Node.Parent);
					bool NoParentsChecked = true;
					while (q.Count > 0)
					{
						TreeNode n = (TreeNode)q.Dequeue();
						if (n.Checked)
						{
							NoParentsChecked = false;
							break;
						}
						if (n.Parent != null)
						{
							q.Enqueue(n.Parent);
						}
					}
					if (!NoParentsChecked)
					{
						e.Node.Checked = false;
						return;
					}
				}

				foreach (TreeNode Node in e.Node.Nodes)
				{
					if (Node.Checked != e.Node.Checked)
					{
						Node.Checked = e.Node.Checked;
						TargetCheckTree_AfterCheck(null, new TreeViewEventArgs(Node, TreeViewAction.Unknown));
					}
				}
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(Ex);
			}
			finally
			{
				this.TargetCheckTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TargetCheckTree_AfterCheck);
			}
		}

		/// <summary>
		/// Shows 'help not available' message.
		/// </summary>
		/// <param name="hevent">A HelpEventArgs that contains the event data.</param>
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the control where the user clicked
			Control ctl = this.GetChildAtPoint(this.PointToClient(hevent.MousePos));
			string str = Gurux.DeviceSuite.Properties.Resources.HelpNotAvailable;
			// Show as a Help pop-up
			if (str != "")
			{
				Help.ShowPopup(ctl, str, hevent.MousePos);
			}
			// Set flag to show that the Help event as been handled
			hevent.Handled = true;
		}
	}
}
