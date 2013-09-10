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
using Gurux.Device.Editor;
using Gurux.Common;
using System.IO;
using System.Xml;
using System.Collections;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.DeviceSuite.Common;
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Director
{
    public partial class GXDirector : Form
    {        
        bool hex;
        List<TraceEventArgs> TraceEvents = new List<TraceEventArgs>();
        List<TraceEventArgs> SelectedTraceEvents = new List<TraceEventArgs>();
        bool TracePause = false;
        GXDevice TracedDevice;
        bool EventFollowLast = true;
        bool TraceFollowLast = false;
        public event System.EventHandler OnItemActivated;
        public System.Diagnostics.TraceLevel m_TraceLevel = System.Diagnostics.TraceLevel.Off;
        MainForm ParentComponent;
        TreeNode m_ListNode = null;

        public System.Diagnostics.TraceLevel TraceLevel
        {
            get
            {
                return m_TraceLevel;
            }
            set
            {
                m_TraceLevel = value;
                if (m_TraceLevel == System.Diagnostics.TraceLevel.Off)
                {
                    if (TabControl1.TabPages.Contains(TracePage))
                    {
                        TabControl1.TabPages.Remove(TracePage);
                    }
                    if (TracedDevice != null)
                    {
                        TracedDevice.OnTrace -= new TraceEventHandler(OnTrace);
                    }
                    TracedDevice = null;                    
                }
                else if (TracedDevice == null)
                {
                    if (!TabControl1.TabPages.Contains(TracePage))
                    {
                        TabControl1.TabPages.Add(TracePage);
                    }
                    //If user changes device or device group.
                    TracedDevice = GXTransactionManager.GetDevice(m_DeviceList.SelectedItem);
                    if (TracedDevice != null)
                    {
                        TracedDevice.Trace = m_TraceLevel;
                        TracedDevice.OnTrace += new TraceEventHandler(OnTrace);
                    }
                }
                else
                {
                    TracedDevice.Trace = m_TraceLevel;
                }
            }
        }


 
        public event DirtyEventHandler OnDirty;

        GXProperty m_Property;
        bool UseCombobox = false;
        bool UseBitMask = false;
        public GXDeviceList m_DeviceList = null;
        public GXDeviceManufacturerCollection Manufacturers = new GXDeviceManufacturerCollection();
        //Swap object and tree node. This makes item search faster.
        System.Collections.Hashtable DeviceToTreeNode = new System.Collections.Hashtable();
        System.Collections.Hashtable DeviceToListItem = new System.Collections.Hashtable();
        System.Collections.Hashtable PropertyToListItem = new System.Collections.Hashtable();
        System.Collections.Hashtable ScheduleToTreeNode = new System.Collections.Hashtable();        
#if DEBUG
        //This is used in debug purposes to insure that IDs are unigue.
        System.Collections.Hashtable m_ObjectIDs = new System.Collections.Hashtable();
#endif //DEBUG
        System.Collections.Hashtable m_IntervalCntSwapNode = new System.Collections.Hashtable();

        /// <summary>
        /// Constructor.
        /// </summary>
        public GXDirector(MainForm parent)            
        {
            ParentComponent = parent;            
            InitializeComponent();
            Bitmap bm = DeviceImage.Image as Bitmap;
            bm.MakeTransparent();
            DeviceImage.Image = bm;
            this.TopLevel = false;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            TablePanel.Dock = DevicePanel.Dock = DevicesList.Dock = PropertyList.Dock = PropertyPanel.Dock = this.Dock = DockStyle.Fill;
            LoadDefaultImages();
            m_DeviceList = new GXDeviceList();
            m_DeviceList.OnDirty += new DirtyEventHandler(m_DeviceList_OnDirty);
            m_DeviceList.OnAdded += new ItemAddedEventHandler(this.OnAdded);
            m_DeviceList.OnRemoved += new ItemRemovedEventHandler(this.OnRemoved);
            m_DeviceList.OnUpdated += new ItemUpdatedEventHandler(OnUpdated);
            m_DeviceList.OnSelectedItemChanged += new SelectedItemChangedEventHandler(OnSelectedItemChanged);
            m_DeviceList.OnLocaleIdentifierChanged += new LocaleIdentifierChangedEventHandler(OnLocaleIdentifierChanged);
            m_DeviceList.OnLoadEnd += new LoadEndEventHandler(OnLoadEnd);
            m_DeviceList.OnClear += new ItemClearEventHandler(this.OnClear);
            m_DeviceList.OnUpdated += new ItemUpdatedEventHandler(OnTableChanged);
            m_DeviceList.OnUpdated += new ItemUpdatedEventHandler(OnPropertyChanged);
            m_DeviceList.OnDisplayTypeChanged += new DisplayTypeChangedEventHandler(OnDisplayTypeChanged);
            m_DeviceList.OnError += new Gurux.Common.ErrorEventHandler(m_DeviceList_OnError);
            DeviceListTree.AfterSelect += new TreeViewEventHandler(OnAfterSelect);
            ReadBtn.Text = Gurux.DeviceSuite.Properties.Resources.ReadTxt;
            WriteBtn.Text = Gurux.DeviceSuite.Properties.Resources.WriteTxt;
            ResetBtn.Text = Gurux.DeviceSuite.Properties.Resources.ResetTxt;
            TypeLbl.Text = Gurux.DeviceSuite.Properties.Resources.TypeTxt;
            UnitLbl.Text = Gurux.DeviceSuite.Properties.Resources.UnitTxt;
            NameLbl.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
            ValueLbl.Text = Gurux.DeviceSuite.Properties.Resources.ValueTxt;
            hex = true;
        }

        void AddError(object sender, Exception ex)
        {
            //If pause is checked or component has destroied.
            if (this.IsDisposed || EventPauseMenu.Checked)
            {
                return;
            }
            /*
			//If information messages are not shown...
			if (severity == -2 && !m_ShowInformationMessages)
			{
				return;
			}
             * */

            string DeviceName;
            if (sender is GXDevice)
            {
                DeviceName = ((GXDevice)sender).Name;
            }
            else if (sender is GXSchedule)
            {
                DeviceName = ((GXSchedule)sender).Name;
            }
            else if (sender is string)
            {
                DeviceName = (string)sender;
            }
            else
            {
                DeviceName = "Unknown type";
            }

            string errorInfo = ex.Message.Replace("\n", " ");
            //Remove first item if maximum item count is reached.
            if (Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount > 0 && 
                EventsList.Items.Count == Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount)
            {
                EventsList.Items[0].Remove();
            }
            ListViewItem it = new ListViewItem(System.DateTime.Now.ToString());
            it.SubItems.Add(DeviceName);
           // it.SubItems.Add(Gurux.DeviceSuite.Properties.Resources.InfoTxt);
            it.SubItems.Add(errorInfo);
            EventsList.Items.Add(it);
            if (EventFollowLast)
            {
                EventsList.EnsureVisible(it.Index);
            }
        }

        void m_DeviceList_OnError(object sender, Exception ex)
        {
            // Update UI if window has created.
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Gurux.Common.ErrorEventHandler(AddError), new object[] { sender, ex });
            }
            else
            {
                AddError(sender, ex);
            }
        }       

        /// <summary>
        /// Return device type.
        /// </summary>
        public string FileName
        {
            get
            {
                return m_DeviceList.FileName;
            }
        }

        /// <summary>
        /// Is device dirty.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return m_DeviceList.Dirty;
            }
        }

        void m_DeviceList_OnDirty(object sender, GXDirtyEventArgs e)
        {
            if (OnDirty != null)
            {
                OnDirty(this, e);
            }
        }

        /// <summary>
        /// Update texts.
        /// </summary>
        void UpdateText()
        {
            DeviceNameCH.Text = Gurux.DeviceSuite.Properties.Resources.DeviceNameTxt;
            DeviceTypeCH.Text = Gurux.DeviceSuite.Properties.Resources.DeviceTypeTxt;
            DeviceStatusCH.Text = Gurux.DeviceSuite.Properties.Resources.DeviceStatusTxt;
        }

        /// <summary>
        /// Load application settings.
        /// </summary>
        public void LoadSettings()
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.PropertyTreeWidth != -1)
            {
                DirectorPanel.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.PropertyTreeWidth;
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DirectorHeight != -1)
            {
                splitContainer2.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.DirectorHeight;
            }
            TraceFollowLastMenu.Checked = TraceFollowLast = Gurux.DeviceSuite.Properties.Settings.Default.TraceFollowLast;
            EventFollowLastMenu.Checked = EventFollowLast = Gurux.DeviceSuite.Properties.Settings.Default.EventFollowLast;
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DeviceListColumns)
                {
                    DevicesList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.EventColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.EventColumns)
                {
                    this.EventsList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.PropertyColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.PropertyColumns)
                {
                    PropertyList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.ScheduleColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.ScheduleColumns)
                {
                    Schedules.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DirectorTraceViewColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DirectorTraceViewColumns)
                {
                    TraceView.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
        }

        /// <summary>
        /// Save application settings.
        /// </summary>
        public void SaveSettings()
        {
            List<string> columns = new List<string>();
            ////////////////////////////////////////////////////////
            //Save widths of device list columns.
            foreach (ColumnHeader it in DevicesList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.TraceFollowLast = TraceFollowLast;
            Gurux.DeviceSuite.Properties.Settings.Default.EventFollowLast = EventFollowLast;
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceListColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of event list columns.
            foreach (ColumnHeader it in EventsList.Columns)
            {
                columns.Add(it.Width.ToString());
            }            
            Gurux.DeviceSuite.Properties.Settings.Default.EventColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.EventColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of property list columns.
            foreach (ColumnHeader it in PropertyList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.PropertyColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.PropertyColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of schedule list columns.
            foreach (ColumnHeader it in Schedules.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.ScheduleColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.ScheduleColumns.AddRange(columns.ToArray());

            Gurux.DeviceSuite.Properties.Settings.Default.DirectorHeight = splitContainer2.SplitterDistance;
            Gurux.DeviceSuite.Properties.Settings.Default.PropertyTreeWidth = DirectorPanel.SplitterDistance;
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of trace view list columns.
            foreach (ColumnHeader it in TraceView.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DirectorTraceViewColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DirectorTraceViewColumns.AddRange(columns.ToArray());
        }

        /// <summary>
        /// Load default images to the image list.
        /// </summary>
        public void LoadDefaultImages()
        {
            DevicesList.SmallImageList = DeviceListTree.ImageList = new ImageList();            
            //Load the image for the device list.
            System.Drawing.Bitmap bm = Gurux.DeviceSuite.Properties.Resources.DeviceList;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the error image for the device list.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceListError;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for the device group.
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceGroup;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the error image for the device group.
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceGroupError;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the disconnected image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceDisconnect;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the connect image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceConnected;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the monitoring image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceMonitor;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the error image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceError;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the dirty image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceDirty;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the reading image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceRead;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the writing image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceWrite;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);//Load the schedule image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemOn;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the executing schedule image for the device.			
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemExecute;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for device categories.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategories;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for the device category.			            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategory;
            bm.MakeTransparent(System.Drawing.Color.FromArgb(255, 0, 255));
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for device tables.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTables;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for the device table.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTable;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for device properties.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperties;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the image for the device property.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperty;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the error image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyError;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the changed image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyChanged2;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);
            //Load the notifications image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceNotifies;
            bm.MakeTransparent();
            DeviceListTree.ImageList.Images.Add(bm);

            //Load default schedule images.
            Schedules.SmallImageList = new ImageList();
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItems;
            bm.MakeTransparent();
            Schedules.SmallImageList.Images.Add(bm);
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemOff;
            bm.MakeTransparent();
            Schedules.SmallImageList.Images.Add(bm);
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemOn;
            bm.MakeTransparent();
            Schedules.SmallImageList.Images.Add(bm);
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemExecute;
            bm.MakeTransparent();
            Schedules.SmallImageList.Images.Add(bm);
        }

        /// <summary>
        /// Save file.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                if (!string.IsNullOrEmpty(m_DeviceList.FileName))
                {
                    m_DeviceList.Save(m_DeviceList.FileName);
                    ParentComponent.MruManager.Insert(0, m_DeviceList.FileName);
                    return true;
                }
                else
                {
                    return SaveFileAs();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);                
                return false;
            }
        }

        /// <summary>
        /// Save file as.
        /// </summary>
        /// <returns></returns>
        public bool SaveFileAs()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = Gurux.DeviceSuite.Properties.Resources.DeviceListFilterTxt;
                dlg.ValidateNames = true;
                dlg.DefaultExt = "gxd";
                if (!string.IsNullOrEmpty(m_DeviceList.FileName))
                {
                    dlg.FileName = Path.GetFileNameWithoutExtension(m_DeviceList.FileName);
                }
                else
                {
                    dlg.FileName = m_DeviceList.Name;
                }
                if (dlg.ShowDialog(ParentComponent) == DialogResult.OK)
                {
                    m_DeviceList.Save(dlg.FileName);
                    ParentComponent.MruManager.Insert(0, dlg.FileName);
                    return true;
                }
                return false;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
                return false;
            }
        }

        /// <summary>
        /// Is changes are made, ask are they saved.
        /// </summary>
        /// <returns></returns>
        private DialogResult AskSaveChanges()
        {
            //If device list is not dirty go back.
            if (m_DeviceList.Dirty == false)
            {
                return DialogResult.Yes;
            }
            //Ask should save changes.
            DialogResult ret = GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.SaveChangesTxt);
            if (ret == DialogResult.Yes)
            {
                //Save changes.
                if (Save())
                {
                    ret = DialogResult.OK;
                }
                else
                {
                    ret = DialogResult.Cancel;
                }
            }
            return ret;
        }

        /// <summary>
        /// Check are properies updated. If so, ask should changes write to the device.
        /// </summary>
        /// <returns></returns>
        public DialogResult CheckAndWritePropertyValues()
        {
            DialogResult ret = DialogResult.No;
            //Write changed propeerties.
            if (m_DeviceList.GetUserChangedItems().Count > 0)
            {
                //Ask should changed values write to the device.
                ret = GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.UpdateChangedValuesTxt);
            }
            //Write changed values to the device.
            if (ret == DialogResult.Yes)
            {
            }
            //save changes.
            if (ret != DialogResult.Cancel)
            {
                ret = AskSaveChanges();
            }
            return ret;

        }

        /// <summary>
        /// Delete selected device group.
        /// </summary>
        /// <param name="deviceGroup">The device group to be deleted.</param>
        public void DeleteDeviceGroup(GXDeviceGroup deviceGroup)
        {
            if (deviceGroup == null || deviceGroup.Parent == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            //Show default Gurux dialog.
            if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt) != DialogResult.Yes)
            {
                return;
            }

            Queue q = new Queue();
            q.Enqueue(deviceGroup);
            GXDeviceGroup grp = null;

            bool ChildDeviceConnected = false;
            while (q.Count > 0)
            {
                grp = (GXDeviceGroup)q.Dequeue();
                //Add child groups to queue
                foreach (GXDeviceGroup ChildGrp in grp.DeviceGroups)
                {
                    q.Enqueue(ChildGrp);
                }
                //Remove devices from schedules
                bool DeviceConnected = false;
                foreach (GXDevice dev in grp.Devices)
                {
                    if ((dev.Status & Gurux.Device.DeviceStates.Connected) != 0)
                    {
                        ChildDeviceConnected = DeviceConnected = true;
                    }
                }
                if (DeviceConnected)
                {
                    continue;
                }
            }
            if (!ChildDeviceConnected)
            {
                deviceGroup.Parent.Remove(deviceGroup);
            }
            else
            {
                throw new Exception("Could not remove device group, because of some connected device(s).");
            }
        }

        /// <summary>
        /// Delete selected device groups.
        /// </summary>
        /// <param name="deviceGroups">A collection of device groups to be deleted.</param>
        public void DeleteDeviceGroups(GXDeviceGroupCollection deviceGroups)
        {
            if (deviceGroups == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            //Show default Gurux dialog.
            if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceTxt) != DialogResult.Yes)
            {
                return;
            }
            deviceGroups.Clear();
        }

        /// <summary>
        /// Delete selected device.
        /// </summary>
        /// <param name="device">A device to be deleted.</param>
        public void DeleteDevice(GXDevice device)
        {
            if (device == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            //Show default Gurux dialog.			
            if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceTxt) != DialogResult.Yes)
            {
                return;
            }
            if ((device.Status & Gurux.Device.DeviceStates.Connected) == 0)
            {
                device.Parent.Remove(device);
                device.Dispose();
            }
            else
            {
                throw new Exception("Connected device cannot be removed.");
            }
        }

        /// <summary>
        /// Delete selected device.
        /// </summary>
        /// <param name="devices">A collection of devices to be deleted.</param>
        public void DeleteDevices(GXDeviceCollection devices)
        {
            if (devices == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            //Show default Gurux dialog.
            if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceTxt) != DialogResult.Yes)
            {
                return;
            }
            //Remove all itemss.
            foreach (GXDevice dev in devices)
            {
                if ((dev.Status & Gurux.Device.DeviceStates.Connected) == 0)
                {
                    dev.Parent.Remove(dev);
                }
                else
                {
                    throw new Exception("Connected device cannot be removed.");
                }
            }
        }

        /// <summary>
        /// Remove the specified item.
        /// </summary>
        /// <param name="target">An item to be removed.</param>
        public void DeleteItem(object target)
        {
            //If devicegroup is selected.
            if (target is GXDeviceGroup)
            {
                DeleteDeviceGroup((GXDeviceGroup)target);
            }
            //If device is selected.
            else if (target is GXDevice)
            {
                DeleteDevice((GXDevice)target);
            }
            else if (target is GXDeviceCollection)
            {
                DeleteDevices((GXDeviceCollection)target);
            }
            //If device list is selected.
            else if (target is GXDeviceList)
            {
                DeleteDeviceGroups(((GXDeviceList)target).DeviceGroups);
            }
            //If category is selected.
            else if (target is GXCategory)
            {
                throw new Exception("GXTransactionManager.Remove failed: GXCategory can not be removed.");
            }
            //If table is selected.
            else if (target is GXTable)
            {
                throw new Exception("GXTransactionManager.Remove failed: GXTable can not be removed.");
            }
            //If property is selected.
            else if (target is GXProperty)
            {
                throw new Exception("GXTransactionManager.Remove failed: GXProperty can not be removed.");
            }
        }

        private void AddScheduleItem(GXSchedule item)
        {
            ListViewItem it = new ListViewItem(item.Name);
            it.SubItems.Add(GXScheduleEditorDlg.ScheduleRepeatToString(item.RepeatMode));
            //Next run time
            DateTime NextDate = item.NextScheduledDate;
            if (NextDate > DateTime.MinValue)
            {
                it.SubItems.Add(NextDate.ToString());
            }
            else
            {
                it.SubItems.Add(string.Empty);
            }
            //Last run time
            if (item.Statistics.LastRunTime > DateTime.MinValue)
            {
                it.SubItems.Add(item.Statistics.LastRunTime.ToString());
            }
            else
            {
                it.SubItems.Add(string.Empty);
            }
            //Progress                
            it.SubItems.Add(string.Empty);
            it.Tag = item;
            it.ImageIndex = (int)((item.Status & ScheduleState.Run) != 0 ? ScheduleImages.ScheduleItemStart : ScheduleImages.ScheduleItemStop);
            Schedules.Items.Add(it);
            ScheduleToTreeNode[item] = it;
        }  

        /// <summary>
        /// Open selected file. Caller must take care error handling.
        /// </summary>
        /// <param name="FileName"></param>
        public void OpenFile(string fileName)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                TraceEvents.Clear();
                TraceView.VirtualListSize = 0;
                m_DeviceList.Load(fileName);
                DeviceListTree.SelectedNode = DeviceListTree.Nodes[0];
                ParentComponent.MruManager.Insert(0, fileName);                
                m_DeviceList.SelectedItem = m_DeviceList;
            }
            catch (Exception Ex)
            {
                //Create empty list if something went wrong.
                ParentComponent.MruManager.Remove(fileName);
                NewDeviceList();
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Open device list.
        /// </summary>
        public void Open()
        {
            try
            {
                if (CheckAndWritePropertyValues() != DialogResult.Cancel)
                {
                    OpenFileDialog gxDlg = new OpenFileDialog();
                    gxDlg.Multiselect = false;
                    gxDlg.Filter = Gurux.DeviceSuite.Properties.Resources.DeviceListFilterTxt;
                    gxDlg.DefaultExt = "gxd";
                    gxDlg.ValidateNames = true;
                    if (gxDlg.ShowDialog(ParentComponent) != DialogResult.OK)
                    {
                        return;
                    }
                    OpenFile(gxDlg.FileName);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }        

        public void NewDevice()
        {
            try
            {                
                object target = DeviceListTree.SelectedNode.Tag;
                DeviceSettingsForm dlg = new DeviceSettingsForm(Manufacturers, null);
                if (dlg.ShowDialog(ParentComponent) == DialogResult.OK)
                {
                    GXTransactionManager.GetDeviceCollection(target).Add(dlg.Device);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(ParentComponent, ex);
            }
        }

        public void NewDeviceGroup()
        {
            try
            {
                object target = DeviceListTree.SelectedNode.Tag;
                GXDeviceGroupDialog dlg = new GXDeviceGroupDialog(new GXDeviceGroup(), target);
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(ParentComponent, ex);
            }
        }

        /// <summary>
        /// Create new Device list.
        /// </summary>
        public void NewDeviceList()
        {
            try
            {
                if (CheckAndWritePropertyValues() != DialogResult.Cancel)
                {
                    //Don't clear errors on new list creation (and director opening)
                    m_DeviceList.CreateEmpty("GXDeviceList1");
                    //Create default device group where device are added.
                    GXDeviceGroup group = new GXDeviceGroup();
                    group.Name = "Devices";                    
                    m_DeviceList.DeviceGroups.Add(group);
                    m_DeviceList.SelectedItem = group;
                    m_DeviceList.Dirty = false;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(ParentComponent, ex);
            }
        }

        /// <summary>
        /// New device added to the device list.
        /// </summary>
        /// <param name="sender">A GXDevice that was added to the list.</param>
        /// <param name="index"></param>
        public virtual void OnAdded(object sender, GXItemEventArgs e)
        {
            try
            {
                if (!this.Created)
                {
                    return;
                }
                if (e.Item is GXProperty)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new ItemAddedEventHandler(PropertyAdded), new object[] { sender, e });
                    }
                    else
                    {
                        PropertyAdded(sender, e);
                    }
                    return;
                }
                else if (e.Item is GXCategory)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new ItemAddedEventHandler(CategoryAdded), new object[] { sender, e });
                    }
                    CategoryAdded(sender, e);
                    return;
                }
                else if (e.Item is GXTable)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new ItemAddedEventHandler(TableAdded), new object[] { sender, e });
                    }
                    TableAdded(sender, e);
                    return;
                }
                GXDevice device = e.Item as GXDevice;
                if (device != null)
                {
                    //If trace level is used update it.
                    if (TraceLevel != System.Diagnostics.TraceLevel.Off)
                    {
                        device.Trace = TraceLevel;
                    }
                    //Find parent node.
                    TreeNode parent = DeviceToTreeNode[device.Parent.Parent] as TreeNode;
                    System.Diagnostics.Debug.Assert(parent != null);
                    ShowAvailableDevice(device, parent);
                    return;
                }
                GXDeviceGroup deviceGroup = e.Item as GXDeviceGroup;
                if (deviceGroup != null)
                {
                    GXDeviceGroupCollection groups = deviceGroup.Parent;
                    TreeNode DeviceNode = (TreeNode)DeviceToTreeNode[groups.Parent];
                    TreeNodeCollection nodes = null;
                    if (DeviceNode != null)
                    {
                        nodes = DeviceNode.Nodes;
                    }
                    else
                    {
                        nodes = DeviceListTree.Nodes;
                    }
                    TreeNode node = ShowAvailableDeviceGroup(deviceGroup, nodes, false);
                    DeviceListTree.SelectedNode = node;
                    node.EnsureVisible();
                    return;
                }
                if (e.Item is GXSchedule)
                {
                    AddScheduleItem(e.Item as GXSchedule);                    
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        #region ShowAvailableItems

        private void ShowAvailableProperty(GXProperty prop, TreeNode root)
        {
#if DEBUG
            m_ObjectIDs.Add(prop.ID, prop);
#endif //DEBUG
            //If property already exists
            System.Diagnostics.Debug.Assert(DeviceToTreeNode[prop] == null);
            //If device ID is nill.
            ulong id = prop.ID;
            System.Diagnostics.Debug.Assert((id >> 16) != 0);
            //If property ID is nill.
            System.Diagnostics.Debug.Assert((id & 0xFFFF) != 0);
            TreeNode it;
            if (prop.Table == null)
            {
                it = root.Nodes.Add(prop.DisplayName + " " + prop.GetValue(true));
            }
            else
            {
                it = root.Nodes.Add(prop.DisplayName);
            }
            it.Tag = prop;
            DeviceToTreeNode[prop] = it;
            PropertyChanged(this, new GXPropertyEventArgs(prop, prop.Status));
        }

        private void ShowAvailableProperties(GXPropertyCollection props, TreeNode root)
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowProperties)
            {
                foreach (GXProperty prop in props)
                {
                    ShowAvailableProperty(prop, root);
                }
            }
        }

        private void ShowAvailableTable(GXTable table, TreeNode root)
        {
#if DEBUG
            m_ObjectIDs.Add(table.ID, table);
#endif //DEBUG
            System.Diagnostics.Debug.Assert(DeviceToTreeNode[table] == null);
            //If device ID is nill.
            ulong id = table.ID;
            System.Diagnostics.Debug.Assert((id >> 16) != 0);
            //If table ID is nill.
            System.Diagnostics.Debug.Assert((id & 0xFFFF) != 0);
            TreeNode node = root.Nodes.Add(table.DisplayName);
            node.Tag = table;
            node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DeviceTable;
            DeviceToTreeNode[table] = node;
        }

        private void ShowAvailableTables(GXTableCollection tables, TreeNode root)
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowTables)
            {
                foreach (GXTable table in tables)
                {
                    ShowAvailableTable(table, root);
                }
            }
        }

        private void ShowAvailableCategory(GXCategory cat, TreeNode root)
        {
#if DEBUG
            m_ObjectIDs.Add(cat.ID, cat);
#endif //DEBUG
            System.Diagnostics.Debug.Assert(DeviceToTreeNode[cat] == null);
            TreeNode node = root.Nodes.Add(cat.DisplayName);
            node.Tag = cat;
            //If device ID is nill.
            ulong id = cat.ID;
            System.Diagnostics.Debug.Assert((id >> 16) != 0);
            //If category ID is nill.
            System.Diagnostics.Debug.Assert((id & 0xFFFF) != 0);
            node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DeviceCategory;
            DeviceToTreeNode[cat] = node;
            ShowAvailableProperties(cat.Properties, node);
        }

        private void ShowAvailableCategories(GXCategoryCollection cats, TreeNode root)
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowCategories)
            {
            foreach (GXCategory cat in cats)
            {
                ShowAvailableCategory(cat, root);
            }
            }
            root.ExpandAll();
        }

        private TreeNode ShowAvailableDevice(GXDevice device, TreeNode root)
        {
#if DEBUG
            m_ObjectIDs.Add(device.ID, device);
#endif //DEBUG
            TreeNode it = root.Nodes.Add(device.Name);
            it.Tag = device;
            DeviceToTreeNode[device] = it;
            ShowAvailableCategories(device.Categories, it);
            ShowAvailableTables(device.Tables, it);

            if (device.Events.Categories.Count != 0 ||
                device.Events.Tables.Count != 0)
            {
                //Add notifies categories and tables.
                it = it.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.NotifyCategoriesTxt);
                it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceNotifies;
                it.Tag = device.Events;
                DeviceToTreeNode[device.Events] = it;
                ShowAvailableCategories(device.Events.Categories, it);
                ShowAvailableTables(device.Events.Tables, it);
            }
            //Update device icon.
            OnUpdated(this, new GXDeviceEventArgs(device, device.Status));
            return it;
        }

        private void ShowAvailableDevices(GXDeviceCollection devs, TreeNode root)
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowDevices)
            {
                foreach (GXDevice dev in devs)
                {
                    ShowAvailableDevice(dev, root);
                }
            }
        }

        private TreeNode ShowAvailableDeviceGroup(GXDeviceGroup group, TreeNodeCollection nodes, bool showDevices)
        {
#if DEBUG
            m_ObjectIDs.Add(group.ID, group);
#endif //DEBUG
            TreeNode node = nodes.Add(group.Name);
            node.Tag = group;
            node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DeviceGroup;
            DeviceToTreeNode[group] = node;
            if (showDevices)
            {
                ShowAvailableDevices(group.Devices, node);
            }
            ShowAvailableDeviceGroups(group.DeviceGroups, node.Nodes, showDevices);
            return node;
        }

        private void ShowAvailableDeviceGroups(GXDeviceGroupCollection groups, TreeNodeCollection nodes, bool showDevices)
        {
            foreach (GXDeviceGroup group in groups)
            {
                ShowAvailableDeviceGroup(group, nodes, showDevices);
            }
        }

        #endregion

        /// <summary>
        /// A device is removed from the device list.
        /// </summary>
        /// <param name="sender">A GXDevice that was removed from the list.</param>
        /// <param name="index"></param>
        public virtual void OnRemoved(object sender, GXItemEventArgs e)
        {
            try
            {
                GXDevice device = e.Item as GXDevice;
                if (device != null)
                {
#if DEBUG
                    m_ObjectIDs.Remove(device.ID);
#endif //DEBUG
                    TreeNode node = (TreeNode)DeviceToTreeNode[device];
                    if (node != null)
                    {
                        foreach (GXTable table in device.Tables)
                        {
                            TableRemoved(sender, new GXItemEventArgs(table));
                        }
                        foreach (GXCategory cat in device.Categories)
                        {
                            CategoryRemoved(sender, new GXItemEventArgs(cat));
                        }
                        DeviceToTreeNode.Remove(device);
                        node.Remove();
                    }                    
                }                
                else if (e.Item is GXDeviceGroup)
                {
                    GXDeviceGroup deviceGroup = e.Item as GXDeviceGroup;
#if DEBUG
                    m_ObjectIDs.Remove(deviceGroup.ID);
#endif //DEBUG
                    TreeNode node = (TreeNode)DeviceToTreeNode[deviceGroup];
                    if (node != null)
                    {
                        foreach (GXDevice dev in deviceGroup.Devices)
                        {
                            OnRemoved(sender, new GXItemEventArgs(dev));
                        }
                        DeviceToTreeNode.Remove(deviceGroup);
                        node.Remove();
                    }                    
                }
                else if (e.Item is GXProperty)
                {
                    this.BeginInvoke(new ItemRemovedEventHandler(PropertyRemoved), new object[] { sender, e });
                }
                else if (e.Item is GXCategory)
                {
                    this.BeginInvoke(new ItemRemovedEventHandler(CategoryRemoved), new object[] { sender, e });
                }
                else if (e.Item is GXTable)
                {
                    this.BeginInvoke(new ItemRemovedEventHandler(TableRemoved), new object[] { sender, e });                
                }
                else if (e.Item is GXSchedule)
                {
                    GXSchedule item = e.Item as GXSchedule;
                    ListViewItem it = ScheduleToTreeNode[item] as ListViewItem;
                    ScheduleToTreeNode.Remove(item);
                    m_IntervalCntSwapNode.Remove(item);
                    if (it != null)
                    {
                        it.Remove();
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void DeviceStateChange(object sender, GXItemEventArgs e)
        {
            TreeNode it = (TreeNode)DeviceToTreeNode[e.Item];
            ListViewItem item = DeviceToListItem[e.Item] as ListViewItem;
            if (it == null || item == null)
            {
                return;
            }
            GXDeviceEventArgs t = e as GXDeviceEventArgs;
            GXDevice device = e.Item as GXDevice;
            DeviceStates state = t.Status;
            // Devices name or type has changed.
            if (state == 0)
            {
                it.Text = device.Name;                
                item.Text = device.Name;
                return;
            }
            //Update device state.
            if ((device.Status & Gurux.Device.DeviceStates.Connected) != 0)
            {
                if ((device.Status & DeviceStates.Error) != 0)
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceError;
                }
                else if ((device.Status & DeviceStates.PropertyChanged) != 0)
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceChanged;
                }
                else if ((state & (DeviceStates.Reading | DeviceStates.ReadStart)) != 0)
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceReading;
                }
                else if ((state & DeviceStates.Writing) != 0)
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceWriting;
                }
                else if ((device.Status & DeviceStates.Monitoring) != 0)
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceMonitoring;
                }
                else
                {
                    item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceConnected;
                }
                //Do nothing if we are starting transaction.
                if ((state & (DeviceStates.ReadStart | DeviceStates.WriteStart)) != 0)
                {
                    return;
                }
            }
            else if ((device.Status & Gurux.Device.DeviceStates.MediaConnected) != 0)
            {
                item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.MediaConnected;
            }
            else //Device is disconnected.
            {
                item.ImageIndex = it.SelectedImageIndex = it.ImageIndex = (int)DeviceImageType.DeviceDisconnected;
            }
        }

        private void UpdateScheduleItem(object sender, GXItemEventArgs e)
        {
            try
            {
                GXSchedule item = e.Item as GXSchedule;
                ListViewItem it = (ListViewItem)ScheduleToTreeNode[item];
                if (it == null)
                {
                    return;
                }
                GXScheduleEventArgs t = e as GXScheduleEventArgs;
                //If user has updated schedule item.
                if (t.Status == ScheduleState.Updated)
                {
                    it.SubItems[0].Text = item.Name;
                    it.SubItems[1].Text = GXScheduleEditorDlg.ScheduleRepeatToString(item.RepeatMode);
                    DateTime NextDate = item.NextScheduledDate;
                    if (NextDate > DateTime.MinValue)
                    {
                        it.SubItems[2].Text = NextDate.ToString();
                    }
                    else
                    {
                        it.SubItems[2].Text = string.Empty;
                    }

                    if (item.Statistics.LastRunTime > DateTime.MinValue)
                    {
                        it.SubItems[3].Text = item.Statistics.LastRunTime.ToString();
                    }
                    else
                    {
                        it.SubItems[3].Text = string.Empty;
                    }
                    it.SubItems[4].Text = string.Empty;
                    it.ImageIndex = (int)((item.Status & ScheduleState.Run) != 0 ? ScheduleImages.ScheduleItemStart : ScheduleImages.ScheduleItemStop);
                }
                else
                {
                    switch (t.Status)
                    {
                        case ScheduleState.TaskStart:
                            it.ImageIndex = (int)ScheduleImages.ScheduleItemExecute;

                            try
                            {
                                m_IntervalCntSwapNode[item] = ((int)m_IntervalCntSwapNode[item]) + 1;
                            }
                            catch
                            {
                                if (!m_IntervalCntSwapNode.Contains(item))
                                {
                                    m_IntervalCntSwapNode[item] = item.Statistics.RunCount;
                                }
                                else
                                {
                                    throw;
                                }
                            }

                            if (item.TransactionCount == 0)
                            {
                                it.SubItems[4].Text = string.Format("{0}/-", (int)m_IntervalCntSwapNode[item]);
                            }
                            else
                            {
                                it.SubItems[4].Text = string.Format("{0}/{1}", (int)m_IntervalCntSwapNode[item], item.TransactionCount);
                            }
                            break;
                        case ScheduleState.TaskFinish:
                            it.ImageIndex = (int)ScheduleImages.ScheduleItemStart;
                            //Update next schedule date.
                            UpdateScheduleItem(this, new GXScheduleEventArgs(item, ScheduleState.Updated));
                            Schedules.Sort();
                            break;
                        case ScheduleState.Start:
                            it.ImageIndex = (int)ScheduleImages.ScheduleItemStart;
                            m_IntervalCntSwapNode[item] = 0;
                            DateTime NextDate = item.NextScheduledDate;
                            if (NextDate > DateTime.MinValue)
                            {
                                it.SubItems[2].Text = NextDate.ToString();
                            }
                            else
                            {
                                it.SubItems[2].Text = string.Empty;
                            }
                            break;
                        case ScheduleState.End:
                            it.ImageIndex = (int)ScheduleImages.ScheduleItemStop;
                            if (item.Statistics.LastRunTime > DateTime.MinValue)
                            {
                                it.SubItems[3].Text = item.Statistics.LastRunTime.ToString();
                            }
                            else
                            {
                                it.SubItems[3].Text = string.Empty;
                            }
                            break;
                        default:
                            //Unknown state.
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void PropertyChanged(object sender, GXItemEventArgs e)
        {
            try
            {
                GXPropertyEventArgs t = e as GXPropertyEventArgs;
                TreeNode node = (TreeNode)DeviceToTreeNode[t.Item];
                GXProperty p = e.Item as GXProperty;
                if (node != null)
                {                    
                    if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowPropertyValue && 
                        (t.Status & (PropertyStates.ValueChangedByDevice | PropertyStates.ValueChangedByUser)) != 0)
                    {
                        node.Text = p.DisplayName + " " + p.GetValue(true);
                    }
                    if ((t.Status & PropertyStates.ValueChangedByUser) != 0)
                    {
                        node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DevicePropertyChanged;
                    }
                    else if ((t.Status & PropertyStates.Error) != 0)
                    {
                        node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DevicePropertyError;
                    }
                    else
                    {
                        node.SelectedImageIndex = node.ImageIndex = (int)DeviceImageType.DeviceProperty;
                    }
                }
                //If property value has changed
                if ((t.Status & (Gurux.Device.PropertyStates.ValueReset | Gurux.Device.PropertyStates.ValueChangedByDevice | Gurux.Device.PropertyStates.ValueChangedByUser)) != 0)
                {                    
                    if (p.Parent.Parent == m_DeviceList.SelectedItem)
                    {
                        ListViewItem item = PropertyToListItem[p] as ListViewItem;
                        if (item == null) //It item not found.
                        {
                            return;
                        }
                        string type = p.ValueType == null ? "" : p.ValueType.Name;
                        string tm = p.ReadTime == DateTime.MinValue ? "" : p.ReadTime.ToString();
                        string unit = p.Unit == null ? "" : Convert.ToString(p.Unit);
                        item.SubItems[1].Text = Convert.ToString(p.GetValue(true));
                        item.SubItems[4].Text = tm;
                        item.SubItems[5].Text = string.Format("{0:#,0}/{1:#,0}", p.Statistics.ReadCount, p.Statistics.ReadFailCount);
                        item.SubItems[6].Text = string.Format("{0:#,0}/{1:#,0}", p.Statistics.WriteCount, p.Statistics.WriteFailCount);
                        item.SubItems[7].Text = string.Format("{0:#,0}/{1:#,0}", p.Statistics.ExecutionTime, p.Statistics.ExecutionAverage);
                    }
                    if (p == m_DeviceList.SelectedItem)
                    {
                        string tm = p.ReadTime == DateTime.MinValue ? "" : p.ReadTime.ToString();
                        TimeStampTB.Text = tm;
                        if ((t.Status & Gurux.Device.PropertyStates.ValueChanged) != 0)
                        {
                            string str = Convert.ToString(p.GetValue(true));
                            if (UseCombobox)
                            {
                                this.ValueCB.SelectedIndexChanged -= new System.EventHandler(this.ValueCB_SelectedIndexChanged);
                                ValueCB.Text = str;
                                this.ValueCB.SelectedIndexChanged += new System.EventHandler(this.ValueCB_SelectedIndexChanged);
                            }
                            else
                            {
                                ValueTB.Text = str;
                                if (UseBitMask)
                                {
                                    ulong val = Convert.ToUInt64(p.GetValue(true));
                                    this.ValueLB.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ValueLB_ItemCheck);
                                    foreach (ListViewItem it in ValueLB.Items)
                                    {
                                        it.Checked = (val & Convert.ToUInt64(it.Tag)) != 0;
                                    }
                                    this.ValueLB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ValueLB_ItemCheck);
                                }
                                if (UseBitMask)
                                {
                                    ValueLB.Text = str;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Content of the table is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatedTableData(object sender, GXItemEventArgs e)
        {
            if (e.Item is GXTable)
            {
                GXTableEventArgs t = e as GXTableEventArgs;
                if ((t.Status & TableStates.RowsAdded) != 0 && TableData.ColumnCount != 0)
                {
                    foreach (object[] it in t.Rows)
                    {
                        TableData.Rows.Add(it);
                    }
                    try
                    {
                        IGXPartialRead partialRead = e.Item as IGXPartialRead;
                        if (partialRead.Type != PartialReadType.New || partialRead.Start == null)
                        {
                            LastReadValueTP.Text = "";
                        }
                        else
                        {
                            LastReadValueTP.Text = Convert.ToDateTime(partialRead.Start).ToString();
                        }
                    }
                    catch
                    {
                        LastReadValueTP.Text = "";
                    }
                }
                else if ((t.Status & TableStates.RowsClear) != 0)
                {
                    TableData.Rows.Clear();
                }
                TableRowCount.Text = "Row count: " + (TableData.RowCount).ToString();
            }
        }

        private void Updated(object sender, GXItemEventArgs e)
        {
            if (e.Item is GXDeviceList)
            {
                GXDeviceList list = e.Item as GXDeviceList;
                TreeNode node = DeviceToTreeNode[list] as TreeNode;
                node.Text = list.Name;
                return;
            }
            if (e.Item is GXCategory)
            {
                GXCategory cat = e.Item as GXCategory;
                TreeNode node = DeviceToTreeNode[cat] as TreeNode;
                if (node != null)
                {
                    node.Text = cat.DisplayName;
                }
                GXCategoryEventArgs t = e as GXCategoryEventArgs;
                //Ignore if category read or write is started or ended.
                if ((t.Status & (CategoryStates.ReadStart | CategoryStates.ReadEnd | CategoryStates.WriteStart | CategoryStates.WriteEnd)) != 0)
                {
                    return;
                }
            }
            if (e.Item is GXTable)
            {
                GXTableEventArgs t = e as GXTableEventArgs;
                //Ignore if table read or write is started or ended.
                if ((t.Status & (TableStates.ReadStart | TableStates.ReadEnd | TableStates.WriteStart | TableStates.WriteEnd)) != 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(UpdatedTableData), new object[] { sender, e });
                }
                else
                {
                    UpdatedTableData(sender, e);
                }
                return;
            }
            if (e.Item is GXDeviceGroup)
            {
                GXDeviceGroup group = e.Item as GXDeviceGroup;
                TreeNode node = DeviceToTreeNode[group] as TreeNode;
                node.Text = group.Name;
                return;
            }
            if (e.Item is GXProperty)
            {
                GXPropertyEventArgs t = e as GXPropertyEventArgs;
                //Notify if property read or write is started or ended.
                if ((t.Status & (PropertyStates.ReadStart | PropertyStates.ReadEnd | PropertyStates.WriteStart | PropertyStates.WriteEnd)) == 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(PropertyChanged), new object[] { sender, e });
                }
                else
                {
                    PropertyChanged(sender, e);
                }
            }
            if (e.Item is GXDevice)
            {
                GXDeviceEventArgs t = e as GXDeviceEventArgs;
                if ((t.Status & (DeviceStates.Connecting | DeviceStates.Disconnecting | DeviceStates.Disconnecting | DeviceStates.ReadStart | DeviceStates.ReadEnd | DeviceStates.WriteStart | DeviceStates.WriteEnd)) != 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(DeviceStateChange), new object[] { sender, e });
                }
                else
                {
                    DeviceStateChange(sender, e);
                }
                return;
            }
            if (e.Item is GXSchedule)
            {
                UpdateScheduleItem(sender, e);
                return;
            }
        }

        private void OnUpdated(object sender, GXItemEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ItemUpdatedEventHandler(Updated), new object[] { sender, e });
            }
            else
            {
                Updated(sender, e);
            }
        }

        private void ValueLB_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                object tmp = m_Property.GetValue(true);
                if (tmp is Array)
                {

                }
                else
                {
                    ulong val = Convert.ToUInt64(tmp);
                    ulong mask = Convert.ToUInt64(ValueLB.Items[e.Index].Tag);
                    //Toggle bits...
                    val ^= mask;
                    m_Property.SetValue(val, true, Gurux.Device.PropertyStates.ValueChangedByUser);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        void AddDeviceToDeviceList(GXDevice device)
        {
            ListViewItem item = new ListViewItem(device.Name);            
            item.SubItems.AddRange(new string[] {"", device.DeviceType});
            item.Tag = device;
            item.ImageIndex = (int)((device.Status & DeviceStates.Connected) != 0 ? DeviceImageType.DeviceConnected : DeviceImageType.DeviceDisconnected);
            DevicesList.Items.Add(item);
            DeviceToListItem[device] = item;
            //Update device state.
            DeviceStateChange(device, new GXDeviceEventArgs(device, device.Status));
        }

        /// <summary>
        /// New device has selected. Select device but don't notify it.
        /// </summary>
        public virtual void OnSelectedItemChanged(object sender, GXSelectedItemEventArgs e)
        {
            try
            {
                DeviceListTree.AfterSelect -= new TreeViewEventHandler(OnAfterSelect);
                if (DeviceListTree.SelectedNode != e.NewItem)
                {
                    if (e.NewItem != null)
                    {
                        TreeNode node = (TreeNode)DeviceToTreeNode[e.NewItem];
                        DeviceListTree.SelectedNode = node;
                        if (node != null)
                        {
                            node.EnsureVisible();
                        }
                    }
                    else
                    {
                        DeviceListTree.SelectedNode = null;
                    }
                    bool showDeviceList = e.NewItem is GXDeviceList || e.NewItem is GXDeviceGroup;
                    bool showPropertyList = e.NewItem is GXCategory;
                    bool showPropertyInfo = e.NewItem is GXProperty;
                    GXDevice device = e.NewItem as GXDevice;
                    GXTable table = e.NewItem as GXTable;
                    if (showDeviceList)
                    {
                        DevicesList.Items.Clear();
                        if (e.NewItem is GXDeviceGroup)
                        {
                            GXDeviceGroup group = e.NewItem as GXDeviceGroup;
                            foreach(GXDevice it in group.Devices)
                            {
                                AddDeviceToDeviceList(it);
                            }
                        }
                        else 
                        {
                            GXDeviceList list = e.NewItem as GXDeviceList;
                            foreach (GXDevice it in list.DeviceGroups.GetDevicesRecursive())
                            {
                                AddDeviceToDeviceList(it);
                            }
                        }
                    }
                    else if (showPropertyList)
                    {
                        PropertyList.Items.Clear();
                        GXCategory cat = e.NewItem as GXCategory;
                        List<ListViewItem> items = new List<ListViewItem>();
                        foreach (GXProperty it in cat.Properties)
                        {
                            ListViewItem item = new ListViewItem(it.Name);
                            item.Tag = it;
                            string type = it.ValueType == null ? "" : it.ValueType.Name;
                            string tm = it.ReadTime == DateTime.MinValue ? "" : it.ReadTime.ToString();
                            string unit = it.Unit == null ? "" : Convert.ToString(it.Unit);
                            item.SubItems.AddRange(new string[]{Convert.ToString(it.GetValue(true)), 
                                            type, unit, tm, 
                                            string.Format("{0:#,0}/{1:#,0}", it.Statistics.ReadCount, it.Statistics.ReadFailCount),
                                            string.Format("{0:#,0}/{1:#,0}", it.Statistics.WriteCount, it.Statistics.WriteFailCount),
                                            string.Format("{0:#,0}/{1:#,0}", it.Statistics.ExecutionTime, it.Statistics.ExecutionAverage)});
                            items.Add(item);
                            PropertyToListItem[it] = item;
                        }
                        PropertyList.Items.AddRange(items.ToArray());
                    }
                    else if (showPropertyInfo)
                    {
                        m_Property = e.NewItem as GXProperty;
                        bool write = (m_Property.AccessMode & AccessMode.Write) != 0 && (m_Property.DisabledActions & DisabledActions.Write) == 0;
                        bool read = (m_Property.AccessMode & AccessMode.Read) != 0 && (m_Property.DisabledActions & DisabledActions.Read) == 0;
                        ResetBtn.Enabled = WriteBtn.Enabled = write;
                        ReadBtn.Enabled = read;
                        ValueTB.ReadOnly = !write;
                        NameTB.Text = m_Property.DisplayName;
                        UnitTB.Text = m_Property.Unit;
                        if (m_Property.ValueType != null)
                        {
                            TypeTB.Text = m_Property.ValueType.Name;
                        }
                        else
                        {
                            TypeTB.Text = "Unknown";
                        }
                        // Check type of the property to determine the type of the control used for value input.
                        UseBitMask = m_Property.BitMask;
                        UseCombobox = !UseBitMask && m_Property.Values.Count != 0;
                        ValueCB.Items.Clear();
                        ValueLB.Items.Clear();
                        Type type = m_Property.ValueType;
                        if (type == null)
                        {
                            type = typeof(string);
                        }
                        string str = Convert.ToString(m_Property.GetValue(true));
                        if (type == typeof(bool))
                        {
                            ValueCB.Items.Add(bool.FalseString);
                            ValueCB.Items.Add(bool.TrueString);
                            UseCombobox = true;
                        }
                        else if (UseCombobox)
                        {
                            foreach (GXValueItem it in m_Property.Values)
                            {
                                ValueCB.Items.Add(it.UIValue);
                            }
                        }
                        if (UseCombobox)
                        {
                            ValueCB.Visible = true;
                            ValueLB.Visible = ValueTB.Visible = false;
                            this.ValueCB.SelectedIndexChanged -= new System.EventHandler(this.ValueCB_SelectedIndexChanged);
                            ValueCB.SelectedIndex = ValueCB.FindStringExact(str);
                            this.ValueCB.SelectedIndexChanged += new System.EventHandler(this.ValueCB_SelectedIndexChanged);
                            if (m_Property.ForcePresetValues || type == typeof(bool))
                            {
                                ValueCB.DropDownStyle = ComboBoxStyle.DropDownList;
                            }
                            else
                            {
                                ValueCB.DropDownStyle = ComboBoxStyle.DropDown;
                                ValueCB.Text = str;
                            }
                        }
                        else
                        {
                            ValueLB.Visible = ValueCB.Visible = false;
                            if (UseBitMask)
                            {
                                ValueLB.Visible = true;
                                ulong val = Convert.ToUInt64(m_Property.GetValue(true));
                                this.ValueLB.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ValueLB_ItemCheck);
                                this.ValueLB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ValueLB_ItemCheck);
                            }
                            ValueTB.Visible = true;
                            ValueTB.Text = str;
                        }
                    }
                    else if (device != null)
                    {
                        SelectedDeviceLbl.Text = device.Name;
                        if (string.IsNullOrEmpty(device.Manufacturer))
                        {
                            DeviceTypeTB.Text = device.ProtocolName + " " + device.DeviceType;
                        }
                        else
                        {
                            DeviceTypeTB.Text = device.PresetName + " [" + device.Manufacturer + " " + device.Model + " " + device.Version + "]";
                        }
                        DeviceProperties.SelectedObject = device;
                        DeviceMediaFrame.Controls.Clear();
                        Form propertiesForm = device.GXClient.Media.PropertiesForm;
                        ((IGXPropertyPage)propertiesForm).Initialize();
                        while (propertiesForm.Controls.Count != 0)
                        {
                            Control ctr = propertiesForm.Controls[0];
                            if (ctr is Panel)
                            {
                                if (!ctr.Enabled)
                                {
                                    propertiesForm.Controls.RemoveAt(0);
                                    continue;
                                }
                            }
                            DeviceMediaFrame.Controls.Add(ctr);
                        }
                    }
                    else if (table != null)
                    {                        
                        TableData.AllowUserToAddRows = TableData.AllowUserToDeleteRows = TableData.ShowEditingIcon = (table.AccessMode & AccessMode.Write) != 0;
                        UpdatedTableData(this, new GXTableEventArgs(table, TableStates.RowsClear, 0, null));
                        TableData.Columns.Clear();
                        foreach (GXProperty it in table.Columns)
                        {
                            TableData.Columns.Add(it.Name, it.DisplayName);
                        }                        
                        List<object[]> rows = table.GetRows(0, -1, true);
                        OnUpdated(this, new GXTableEventArgs(table, TableStates.RowsAdded, 0, rows));
                        IGXPartialRead partialRead = table as IGXPartialRead;
                        if (table.Columns.Count != 0)
                        {
                            GXProperty prop = table.Columns[0];
                            if (prop.ValueType != typeof(DateTime))
                            {
                                ReadNewValuesCB.Enabled = ReadFromRB.Enabled = ReadLastRB.Enabled = false;
                                partialRead.Type = PartialReadType.All;
                            }
                            else
                            {
                                ReadNewValuesCB.Enabled = ReadFromRB.Enabled = ReadLastRB.Enabled = true;
                            }
                        }
                        ReadingGB.Visible = partialRead != null;
                        if (partialRead != null)
                        {
                            switch (partialRead.Type)
                            {
                                case PartialReadType.New://Read New values.
                                    ReadNewValuesCB.Checked = true;
                                    try
                                    {
                                        if (partialRead.Type != PartialReadType.New || partialRead.Start == null)
                                        {
                                            LastReadValueTP.Text = "";
                                        }
                                        else
                                        {
                                            LastReadValueTP.Text = Convert.ToDateTime(partialRead.Start).ToString();
                                        }
                                    }
                                    catch
                                    {
                                        LastReadValueTP.Text = "";
                                    }
                                    break;
                                case PartialReadType.All://Read all values.
                                    ReadAllRB.Checked = true;
                                    break;
                                case PartialReadType.Last: //Read last n. Days.
                                    ReadLastRB.Checked = true;
                                    ReadLastTB.Text = partialRead.Start.ToString();
                                    break;
                                case PartialReadType.Range://Read between days
                                    ReadFromRB.Checked = true;
                                    if (partialRead.Start != null)
                                    {
                                        StartPick.Value = Convert.ToDateTime(partialRead.Start);
                                        StartPick.Checked = true;
                                    }
                                    else
                                    {
                                        StartPick.Checked = false;
                                    }
                                    if (partialRead.End != null)
                                    {
                                        ToPick.Value = Convert.ToDateTime(partialRead.End);
                                        ToPick.Checked = true;
                                    }
                                    else
                                    {
                                        ToPick.Checked = false;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    TablePanel.Visible = table != null;
                    DevicePanel.Visible = device != null;
                    DevicesList.Visible = showDeviceList;
                    PropertyList.Visible = showPropertyList;
                    PropertyPanel.Visible = showPropertyInfo;
                    //Show diagnostics trace.
                    if (TraceLevel != System.Diagnostics.TraceLevel.Off)
                    {
                        if (!TabControl1.TabPages.Contains(TracePage))
                        {
                            TabControl1.TabPages.Add(TracePage);
                        }
                        //If user changes device or device group.
                        device = GXTransactionManager.GetDevice(e.NewItem);
                        if (device == null || device != TracedDevice)
                        {
                            lock (TraceEvents)
                            {
                                TraceEvents.Clear();
                                TraceView.VirtualListSize = 0;
                            }
                            if (TracedDevice != null)
                            {
                                TracedDevice.OnTrace -= new TraceEventHandler(OnTrace);
                            }
                            TracedDevice = null;
                        }
                        //If new device is selected.
                        if (device != null && device != TracedDevice)
                        {
                            TracedDevice = device;
                            TracedDevice.OnTrace += new TraceEventHandler(OnTrace);
                        }
                    }
                    else if (TabControl1.TabPages.Contains(TracePage))
                    {
                        TabControl1.TabPages.Remove(TracePage);
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
            finally
            {
                DeviceListTree.AfterSelect += new TreeViewEventHandler(OnAfterSelect);
            }
        }

        void OnTrace(object sender, TraceEventArgs e)
        {
            if (TracePause)
            {
                return;
            }
            //Ignore client send and receive trace because we want to show media traces.
            if ((e.Type & (TraceTypes.Sent | TraceTypes.Received)) != 0 && sender is Gurux.Communication.GXClient)
            {
                return;
            }
            if (InvokeRequired)
            {
                this.BeginInvoke(new TraceEventHandler(OnTrace), new object[] { sender, e });
                return;
            }
            lock (TraceEvents)
            {
                //Remove first item if maximum item count is reached.
                if (Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount > 0 && 
                    TraceEvents.Count == Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount)
                {
                    TraceEvents.RemoveAt(0);
                }
                TraceEvents.Add(e);
                TraceView.VirtualListSize = TraceEvents.Count;
                if (TraceFollowLast)
                {                    
                    TraceView.EnsureVisible(TraceEvents.Count - 1);
                }
            }           
        }

        /// <summary>
        /// Select new item from the list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void OnLocaleIdentifierChanged(System.Globalization.CultureInfo newLocaleIdentifier)
        {
            try
            {
                /*
                NewDeviceMenu.Text = Gurux.DeviceSuite.Properties.Resources.AddDeviceTxt;
                AddDeviceGroupMnu.Text = Gurux.DeviceSuite.Properties.Resources.AddDeviceGroupTxt;
                ReadMenu.Text = Gurux.DeviceSuite.Properties.Resources.ReadTxt;
                WriteMenu.Text = Gurux.DeviceSuite.Properties.Resources.WriteTxt;
                OptionsMnu.Text = Gurux.DeviceSuite.Properties.Resources.OptionsTxt;
                DeleteMnu.Text = Gurux.DeviceSuite.Properties.Resources.DeleteTxt;
                StartMonitorMenu.Text = Gurux.DeviceSuite.Properties.Resources.StartMonitorTxt;
                StopMonitorMnu.Text = Gurux.DeviceSuite.Properties.Resources.StopMonitorTxt;
                ResetPropertiesMnu.Text = Gurux.DeviceSuite.Properties.Resources.ResetPropertiesTxt;
                ClearErrorsMenu.Text = Gurux.DeviceSuite.Properties.Resources.ResetErrorsTxt;
                ResetDeviceMenu.Text = Gurux.DeviceSuite.Properties.Resources.ResetDeviceTxt;
                CancelTransactionMnu.Text = Gurux.DeviceSuite.Properties.Resources.CancelTransactionTxt;
                CloneMnu.Text = Gurux.DeviceSuite.Properties.Resources.CloneTxt;
                ConnectMenu.Text = Gurux.DeviceSuite.Properties.Resources.ConnectTxt;
                DisconnectMenu.Text = Gurux.DeviceSuite.Properties.Resources.DisconnectTxt;
                ExpandAllMnu.Text = Gurux.DeviceSuite.Properties.Resources.ExpandAllTxt;
                CollapseAllMnu.Text = Gurux.DeviceSuite.Properties.Resources.CollapseAllTxt;
                ResetCountersMnu.Text = Gurux.DeviceSuite.Properties.Resources.ResetCountersTxt;
                //Build tree again because localized names might change.
                 */
                UpdateTree(true);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void GetExpandedItems(TreeNodeCollection nodes, System.Collections.ArrayList list)
        {
            foreach (TreeNode it in nodes)
            {
                //Save expanded item to the list.
                if (it.IsExpanded)
                {
                    list.Add(it.Tag);
                    GetExpandedItems(it.Nodes, list);
                }
            }
        }

        /// <summary>
        /// Updates tree view.
        /// </summary>
        protected void UpdateTree(bool notifySelect)
        {
            try
            {
#if DEBUG
                m_ObjectIDs.Clear();
#endif //DEBUG
                DeviceToTreeNode.Clear();
                // Suppress repainting the TreeView until all the objects have been created.
                DeviceListTree.BeginUpdate();
                if (notifySelect)
                {
                    DeviceListTree.AfterSelect -= new TreeViewEventHandler(OnAfterSelect);
                }
                //Save devicegroup expand state.
                System.Collections.ArrayList ExpandedList = new System.Collections.ArrayList();
                //Get root node.
                TreeNode node = (TreeNode)DeviceToTreeNode[DevicesList];
                TreeNodeCollection nodes = null;
                //If root node is used.
                if (node != null)
                {
                    //Save expanded item to the list.
                    if (node.IsExpanded)
                    {
                        ExpandedList.Add(node.Tag);
                    }
                    nodes = node.Nodes;
                }
                else //Root node is hidden.
                {
                    nodes = DeviceListTree.Nodes;
                }
                GetExpandedItems(nodes, ExpandedList);
                DeviceListTree.Nodes.Clear();
                m_ListNode = new TreeNode(DevicesList.Name, (int)DeviceImageType.DeviceList, (int)DeviceImageType.DeviceList);
                m_ListNode.Tag = m_DeviceList;
                DeviceToTreeNode[m_DeviceList] = m_ListNode;
                ShowAvailableDeviceGroups(m_DeviceList.DeviceGroups, m_ListNode.Nodes, true);
                DeviceListTree.Nodes.Add(m_ListNode);
                m_ListNode.Expand();
                /////////////////////////////////////////////////////////////////////
                //Restore expanded items again.
                foreach (object it in ExpandedList)
                {
                    node = (TreeNode)DeviceToTreeNode[it];
                    //Node might be null if its hidden.
                    if (node != null)
                    {
                        node.Expand();
                    }
                }
                //show selected item again.
                if (m_DeviceList.SelectedItem != null)
                {
                    node = (TreeNode)DeviceToTreeNode[m_DeviceList.SelectedItem];
                    if (node != null)
                    {
                        DeviceListTree.SelectedNode = node;
                        node.EnsureVisible();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("UpdateTree failed. " + Ex.Message);
            }
            finally
            {
                DeviceListTree.EndUpdate();
                if (notifySelect)
                {
                    DeviceListTree.AfterSelect += new TreeViewEventHandler(OnAfterSelect);
                }
            }
        }

        private void OnClear(object sender, GXItemEventArgs e)
        {
            try
            {
                if (e.Item is GXDeviceList)
                {
                    UpdateTree(true);
                    return;
                }
                GXDeviceCollection devices = e.Item as GXDeviceCollection;
                if (devices != null)
                {
                    foreach (GXDevice it in devices)
                    {
                        OnRemoved(e.Item, new GXItemEventArgs(it));
                    }
                    return;
                }

                GXDeviceGroupCollection deviceGroups = e.Item as GXDeviceGroupCollection;
                if (deviceGroups != null)
                {
                    foreach (GXDeviceGroup it in deviceGroups)
                    {
                        this.OnRemoved(e.Item, new GXItemEventArgs(it));
                    }
                    return;
                }
                GXTableCollection tables = e.Item as GXTableCollection;
                if (tables != null)
                {
                    foreach (GXTable it in tables)
                    {
                        this.OnRemoved(e.Item, new GXItemEventArgs(it));
                    }
                    return;
                }

                GXCategoryCollection categories = e.Item as GXCategoryCollection;
                if (categories != null)
                {
                    foreach (GXCategory it in categories)
                    {
                        this.OnRemoved(e.Item, new GXItemEventArgs(it));
                    }
                    return;
                }

                GXPropertyCollection properties = e.Item as GXPropertyCollection;
                if (properties != null)
                {
                    if (categories != null)
                    {
                        foreach (GXProperty it in properties)
                        {
                            this.OnRemoved(e.Item, new GXItemEventArgs(it));
                        }
                    }
                    return;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void UpdateLoadEnd(GXDeviceList deviceList)
        {
            UpdateTree(true);
            Schedules.Items.Clear();
            ScheduleToTreeNode.Clear();
            foreach (GXSchedule it in m_DeviceList.Schedules)
            {
                AddScheduleItem(it);
            }
        }

        private void OnLoadEnd(GXDeviceList deviceList)
        {
            //If trace level is used update it to all devices.
            if (TraceLevel != System.Diagnostics.TraceLevel.Off)
            {
                foreach (GXDevice it in m_DeviceList.DeviceGroups.GetDevicesRecursive())
                {
                    it.Trace = TraceLevel;
                }
            }
            if (InvokeRequired)
            {
                this.BeginInvoke(new Gurux.Device.LoadEndEventHandler(UpdateLoadEnd), new object[] { deviceList });
            }
            else
            {
                UpdateLoadEnd(deviceList);
            }
        }        

        private void OnPropertyChanged(object sender, GXItemEventArgs e)
        {
            try
            {
                if (e.Item is GXProperty)
                {
                    GXPropertyEventArgs t = e as GXPropertyEventArgs;
                    //Ignore transaction start and end notify.
                    if ((t.Status & (PropertyStates.ReadEnd | PropertyStates.ReadStart | PropertyStates.WriteStart | PropertyStates.WriteEnd)) != 0)
                    {
                        return;
                    }
                    // Update UI if window has created.
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new Gurux.Device.ItemUpdatedEventHandler(PropertyChanged), sender, e);
                    }
                    else
                    {
                        PropertyChanged(sender, e);
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Select new device in the device list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A TreeViewEventArgs that contains the event data.</param>
        private void OnAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                if (m_DeviceList.SelectedItem != e.Node.Tag)
                {
                    m_DeviceList.SelectedItem = e.Node.Tag;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void OnDisplayTypeChanged(DisplayTypes displayType)
        {
            UpdateTree(true);
        }

        /// <summary>
        /// New property has added to the table or category.
        /// </summary>
        /// <param name="sender">A GXProperty that was added to the tree view.</param>
        /// <param name="index">The zero-based index at which the property was inserted.</param>
        private void PropertyAdded(object sender, GXItemEventArgs e)
        {
            try
            {
                GXProperty property = e.Item as GXProperty;
                TreeNode Parent = (TreeNode)DeviceToTreeNode[property.Parent.Parent];
                ShowAvailableProperty(property, Parent);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Property has removed from the table or category.
        /// </summary>
        /// <param name="property">A GXProperty that was removed from the tree view.</param>
        /// <param name="index">The zero-based index from which the property was removed.</param>
        private void PropertyRemoved(object sender, GXItemEventArgs e)
        {
            try
            {
                GXProperty property = e.Item as GXProperty;
#if DEBUG
                m_ObjectIDs.Remove(property.ID);
#endif //DEBUG
                TreeNode Parent = (TreeNode)DeviceToTreeNode[property];
                if (Parent != null)
                {
                    Parent.Remove();
                    DeviceToTreeNode.Remove(property);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }


        private void CategoryAdded(object sender, GXItemEventArgs e)
        {
            try
            {
                GXCategory category = e.Item as GXCategory;
                TreeNode Parent = (TreeNode)DeviceToTreeNode[category.Device];
                ShowAvailableCategory(category, Parent);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void CategoryRemoved(object sender, GXItemEventArgs e)
        {
            try
            {
                GXCategory category = e.Item as GXCategory;
#if DEBUG
                m_ObjectIDs.Remove(category.ID);
#endif //DEBUG
                TreeNode Parent = (TreeNode)DeviceToTreeNode[category];
                if (Parent != null)
                {
                    foreach (GXProperty prop in category.Properties)
                    {
                        PropertyRemoved(sender, new GXItemEventArgs(prop));
                    }
                    Parent.Remove();
                    DeviceToTreeNode.Remove(category);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void TableAdded(object sender, GXItemEventArgs e)
        {
            try
            {
                GXTable table = e.Item as GXTable;
                TreeNode Parent = (TreeNode)DeviceToTreeNode[table.Device];
                ShowAvailableTable(table, Parent);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void OnTableChanged(object sender, GXItemEventArgs e)
        {
            if (e.Item is GXTable)
            {
                GXTableEventArgs t = e as GXTableEventArgs;
                // Update UI if window has created.
                if (this.IsHandleCreated && (t.Status & TableStates.Updated) != 0)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(TableChanged), new object[] { sender, e });
                }
            }
        }
        private void TableChanged(object sender, GXItemEventArgs e)
        {
            try
            {
                GXTable table = e.Item as GXTable;
                TreeNode node = (TreeNode)DeviceToTreeNode[table];
                if (node != null)
                {
                    node.Text = table.DisplayName;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void TableRemoved(object sender, GXItemEventArgs e)
        {
            try
            {
                GXTable table = e.Item as GXTable;
#if DEBUG
                m_ObjectIDs.Remove(table.ID);
#endif //DEBUG
                TreeNode Parent = (TreeNode)DeviceToTreeNode[table];
                if (Parent != null)
                {
                    Parent.Remove();
                    DeviceToTreeNode.Remove(table);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Disable unused items.
        /// </summary>
        private void ScheduleMenu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                ScheduleAddMenu.Enabled = m_DeviceList != null;
                if (Schedules.SelectedItems.Count == 0)
                {
                    ScheduleOptionsMenu.Enabled = ScheduleRunMenu.Enabled = ScheduleStopAllMenu.Enabled = ScheduleStopMenu.Enabled = ScheduleStartAllMenu.Enabled = ScheduleStartMenu.Enabled = ScheduleDeleteMenu.Enabled = false;
                }
                else
                {
                    ScheduleOptionsMenu.Enabled = true;
                    bool anyStarted = false; //Is any item started.
                    bool anySelectedStarted = false; //Is any selected item started.
                    bool allSelectedStarted = true; //Are all selected items started.
                    bool allStarted = true; //Are all items started.					
                    foreach (ListViewItem it in Schedules.SelectedItems)
                    {
                        GXSchedule item = (GXSchedule)it.Tag;
                        if (!anySelectedStarted && (item.Status & ScheduleState.Run) != 0)
                        {
                            anySelectedStarted = true;
                        }
                        if (allSelectedStarted && (item.Status & ScheduleState.Run) == 0)
                        {
                            allSelectedStarted = false;
                        }
                    }
                    foreach (ListViewItem it in Schedules.Items)
                    {
                        GXSchedule item = (GXSchedule)it.Tag;
                        if (!anyStarted && (item.Status & ScheduleState.Run) != 0)
                        {
                            anyStarted = true;
                        }
                        if (allStarted && (item.Status & ScheduleState.Run) == 0)
                        {
                            allStarted = false;
                        }
                    }
                    ScheduleOptionsMenu.Enabled = Schedules.SelectedItems.Count == 1;
                    ScheduleDeleteMenu.Enabled = !anySelectedStarted;
                    ScheduleStopAllMenu.Enabled = anyStarted;
                    ScheduleStopMenu.Enabled = anySelectedStarted;
                    ScheduleStartAllMenu.Enabled = !allStarted;
                    ScheduleStartMenu.Enabled = !allSelectedStarted;
                    ScheduleRunMenu.Enabled = !anyStarted;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Add new Schedule.
        /// </summary>
        private void NewScheduleMenu_Click(object sender, EventArgs e)
        {
            NewSchedule();
        }

        /// <summary>
        /// Add new Schedule.
        /// </summary>
        public void NewSchedule()
        {
            try
            {
                GXScheduleEditorDlg dlg = new GXScheduleEditorDlg(null, m_DeviceList);
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }



        /// <summary>
        /// Delete selected schedule items.
        /// </summary>
        public void DeleteScheduleItems()
        {
            try
            {
                if (Schedules.SelectedItems.Count != 0)
                {
                    if (GXCommon.ShowQuestion(Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    List<GXSchedule> items = new List<GXSchedule>();
                    foreach (ListViewItem it in Schedules.SelectedItems)
                    {
                        GXSchedule item = it.Tag as GXSchedule;
                        //Don't remove started items.
                        if ((item.Status & ScheduleState.Run) == 0)
                        {
                            item.Parent.Remove(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Delete selected schedule items.
        /// </summary>
        private void ScheduleDeleteMenu_Click(object sender, EventArgs e)
        {
            DeleteScheduleItems();
        }

        /// <summary>
        /// Start selected schedule items.
        /// </summary>
        private void ScheduleStartMenu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    (it.Tag as GXSchedule).Start();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Start all schedule items.
        /// </summary>
        private void ScheduleStartAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GXSchedule it in m_DeviceList.Schedules)
                {
                    if ((it.Status & Gurux.Device.ScheduleState.Start) == 0)
                    {
                        it.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Stop selected schedule items.
        /// </summary>
        private void ScheduleStopMenu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    (it.Tag as GXSchedule).Stop();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Stop all schedule items.
        /// </summary>
        private void ScheduleStopAllMenu_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (GXSchedule it in m_DeviceList.Schedules)
                {
                    it.Stop();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Run selected schedule items.
        /// </summary>
        private void ScheduleRunMenu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    (it.Tag as GXSchedule).Run();
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Show schedule options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScheduleOptionsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Schedules.SelectedItems.Count == 1)
                {
                    GXScheduleEditorDlg dlg = new GXScheduleEditorDlg(Schedules.SelectedItems[0].Tag as GXSchedule, m_DeviceList);
                    dlg.ShowDialog(ParentComponent);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        private void PropertyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PropertyList.SelectedItems.Count == 1)
            {
                TreeNode item = DeviceToTreeNode[PropertyList.SelectedItems[0].Tag] as TreeNode;
                if (item != null)
                {
                    DeviceListTree.SelectedNode = item;
                }
            }
        }

        /// <summary>
        /// Clear occurred errors.
        /// </summary>
        public void ClearErrors()
        {
            try
            {
                EventsList.Items.Clear();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
            
        }

        private void DeviceTreeMenu_Opening(object sender, CancelEventArgs e)
        {
            if (DeviceListTree.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
            object selItem = DeviceListTree.SelectedNode.Tag;
            AddDeviceGroupMenu.Enabled = selItem != null;
            //Is device list selected.
            bool deviceListSelected = selItem != null && (selItem is GXDeviceList);
            NewDeviceMenu.Visible = (!deviceListSelected && selItem != null);
            //Is device selected.			
            bool deviceSelected = (selItem != null && (selItem is GXDevice || selItem is GXCategory || selItem is GXTable || selItem is GXProperty));
            //Is device group selected.
            bool deviceGroupSelected = (selItem != null && selItem is GXDeviceGroup);
            bool activeDevicesSelected = (selItem != null && selItem is GXDeviceCollection);
            //Is notfies selected
            bool notifiesSelected = (selItem != null && selItem is GXEvents);
            //SeparatorMnu3.Visible = SeparatorMnu.Visible = !notifiesSelected;
            DeleteMenu.Enabled = deviceSelected || deviceGroupSelected;
            CollapseAllMenu.Enabled = ExpandAllMenu.Enabled = (deviceListSelected || deviceGroupSelected || (selItem is GXDevice) || notifiesSelected) && !activeDevicesSelected;
            AddDeviceGroupMenu.Enabled = deviceListSelected || deviceGroupSelected;
            NewDeviceMenu.Enabled = deviceGroupSelected || activeDevicesSelected || (selItem is GXDevice);
            bool Connecting = GXTransactionManager.IsConnecting(selItem);
            if (deviceSelected)
            {
                bool open = GXTransactionManager.IsConnected(selItem);
                ConnectMenu.Visible = !Connecting && !open;
                ReadMenu.Enabled = WriteMenu.Enabled = DisconnectMenu.Visible = open;
                CancelTransactionMenu.Enabled = ConnectMenu.Enabled = DisconnectMenu.Enabled = ResetPropertiesMenu.Enabled = ClearErrorsMenu.Enabled = PropertiesMenu.Enabled = StopMonitorMnu.Enabled = StartMonitorMenu.Enabled = true;
                //If device is monitoring.
                bool monitoring = GXTransactionManager.IsMonitoring(selItem);
                DeleteMenu.Enabled = !Connecting && !monitoring;
                if (selItem is GXProperty && ((GXProperty)selItem).Table != null)
                {
                    PropertiesMenu.Enabled = false;
                }
            }
            else if (deviceGroupSelected)
            {
                CancelTransactionMenu.Enabled = ResetPropertiesMenu.Enabled = ClearErrorsMenu.Enabled = DisconnectMenu.Visible = ConnectMenu.Visible = ReadMenu.Enabled = WriteMenu.Enabled = PropertiesMenu.Enabled = StopMonitorMnu.Enabled = StartMonitorMenu.Enabled = true;
                StopMonitorMnu.Visible = true;                
            }
            else if (activeDevicesSelected)
            {
                //DisconnectMenu.Visible = ConnectMenu.Visible = DisconnectMenu.Visible = 
                DeleteMenu.Visible = toolStripSeparator6.Visible = toolStripSeparator7.Visible = toolStripSeparator5.Visible = toolStripSeparator4.Visible = toolStripSeparator8.Visible = false;
                PropertiesMenu.Visible = CollapseAllMenu.Visible = ExpandAllMenu.Visible = ReadMenu.Visible = WriteMenu.Visible = false;
                CancelTransactionMenu.Visible = ResetPropertiesMenu.Visible = ClearErrorsMenu.Visible = StopMonitorMnu.Visible = StartMonitorMenu.Visible = false;
                AddDeviceGroupMenu.Visible = CancelTransactionMenu.Visible = false;
            }
            else if (notifiesSelected)
            {
                DisconnectMenu.Visible = ConnectMenu.Visible = StopMonitorMnu.Visible = StartMonitorMenu.Visible = false;
                CancelTransactionMenu.Enabled = PropertiesMenu.Enabled = ResetPropertiesMenu.Enabled = ClearErrorsMenu.Enabled = DisconnectMenu.Enabled = ConnectMenu.Enabled = ReadMenu.Enabled = WriteMenu.Enabled = StopMonitorMnu.Enabled = StartMonitorMenu.Enabled = false;
            }
            else //If other component is selected.
            {
                DisconnectMenu.Visible = ConnectMenu.Visible = StopMonitorMnu.Visible = StartMonitorMenu.Visible = true;
                ResetPropertiesMenu.Enabled = ClearErrorsMenu.Enabled = DisconnectMenu.Enabled = ConnectMenu.Enabled = ReadMenu.Enabled = WriteMenu.Enabled = StopMonitorMnu.Enabled = StartMonitorMenu.Enabled = selItem != null;
                CancelTransactionMenu.Enabled = false;
            }
        }

        public void ShowProperties()
        {
            try
            {
                if (DeviceListTree.Focused)
                {
                    object target = DeviceListTree.SelectedNode.Tag;
                    //If device list is selected.
                    if (target is GXDeviceList)
                    {
                        GXDeviceListDialog dlg = new GXDeviceListDialog(target as GXDeviceList);
                        dlg.ShowDialog(ParentComponent);
                    }
                    //If device is selected.
                    else if (target is GXDevice)
                    {
                        DeviceSettingsForm dlg = new DeviceSettingsForm(Manufacturers, target as GXDevice);
                        dlg.ShowDialog(ParentComponent);
                    }
                    //If deviceGroup is selected.
                    else if (target is GXDeviceGroup)
                    {
                        GXDeviceGroup deviceGroup = target as GXDeviceGroup;
                        GXDeviceGroupDialog dlg = new GXDeviceGroupDialog(deviceGroup, deviceGroup.Parent);
                        dlg.ShowDialog(ParentComponent);

                    }
                    //If Property is selected.
                    else if (target is GXProperty)
                    {
                        ModifyValueDialog dlg = new ModifyValueDialog(target as GXProperty, true, true);
                        dlg.ShowDialog(ParentComponent);
                    }
                    //If Category is selected.
                    else if (target is GXCategory)
                    {
                        PropertyGroupDlg dlg = new PropertyGroupDlg(target);
                        dlg.ShowDialog(ParentComponent);
                    }
                    //If Table is selected.
                    else if (target is GXTable)
                    {
                        PropertyGroupDlg dlg = new PropertyGroupDlg(target);
                        dlg.ShowDialog(ParentComponent);
                    }
                    else
                    {
                        throw new Exception("Unknown type: " + target.GetType().ToString());
                    }
                }
                else if (Schedules.Focused)
                {
                    ScheduleOptionsMenu_Click(null, null);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Show Options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesMenu_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        private void NewDeviceMenu_Click(object sender, EventArgs e)
        {
            NewDevice();
        }

        private void AddDeviceGroupMenu_Click(object sender, EventArgs e)
        {
            NewDeviceGroup();
        }

        public void Delete()
        {
            try
            {
                if (DeviceListTree.Focused)
                {
                    if (DeviceListTree.SelectedNode != null)
                    {
                        DeleteItem(DeviceListTree.SelectedNode.Tag);
                        return;
                    }
                }
                else if (Schedules.Focused)
                {
                    DeleteScheduleItems();
                    return;
                }
                //Delete trace items.
                else if (TraceView.Focused)
                {
                    TraceEvents.Clear();
                    TraceView.VirtualListSize = 0;
                    return;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Delete active item(s).
        /// </summary>
        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Called when user selects new item.
        /// Menu items are enabled/disabled debending what is selected.
        /// </summary>
        private void PropertyGrid_Enter(object sender, EventArgs e)
        {
            if (OnItemActivated != null)
            {
                OnItemActivated(sender, e);
            }
        }

        /// <summary>
        /// Is events listening paused.
        /// </summary>
        private void EventPauseMenu_Click(object sender, EventArgs e)
        {
            try
            {
                EventPauseMenu.Checked = !EventPauseMenu.Checked;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Copy selected items from the event list to the clipboard.
        /// </summary>
        private void EventCopyMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ClipboardCopy.CopyDataToClipboard(EventsList);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Is last event item followed.
        /// </summary>
        private void EventFollowLastMenu_Click(object sender, EventArgs e)
        {
            try
            {
                EventFollowLast = EventFollowLastMenu.Checked = !EventFollowLast;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Clear events.
        /// </summary>
        private void EventClear_Click(object sender, EventArgs e)
        {
            try
            {
                EventsList.Items.Clear();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Pause trace.
        /// </summary>
        private void TracePauseMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TracePause = TracePauseMenu.Checked = !TracePauseMenu.Checked;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Copy selected trace items to the clipboard.
        /// </summary>
        private void TraceCopyMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ClipboardCopy.CopyDataToClipboard(TraceView);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Is last trace item followed.
        /// </summary>
        private void TraceFollowLastMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TraceFollowLast = TraceFollowLastMenu.Checked = !TraceFollowLast;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Clear trace view.
        /// </summary>
        private void TraceClearMenu_Click(object sender, EventArgs e)
        {
            try
            {
                lock (TraceEvents)
                {
                    TraceEvents.Clear();
                    TraceView.VirtualListSize = 0;
                }                 
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Read from property form.
        /// </summary>
        private void ReadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GXProperty prop = m_DeviceList.SelectedItem as GXProperty;
                if (prop != null)
                {
                    ParentComponent.TransactionManager.Read(this, prop);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Write from property form.
        /// </summary>
        private void WriteBtn_Click(object sender, EventArgs e)
        {
            try
            {                
                GXProperty prop = m_DeviceList.SelectedItem as GXProperty;
                if (prop != null)
                {
                    ParentComponent.TransactionManager.Write(this, prop);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }

        }

        /// <summary>
        /// Reset from property form.
        /// </summary>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GXProperty prop = m_DeviceList.SelectedItem as GXProperty;
                if (prop != null)
                {
                    prop.Reset(ResetTypes.Values);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }

        }

        /// <summary>
        /// Connect device.
        /// </summary>
        private void ConnectMenu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsConnectMenu_Click(null, null);
        }

        /// <summary>
        /// Disconnect device.
        /// </summary>
        private void DisconnectMenu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsDisconnectMenu_Click(null, null);
        }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        private void StartMonitorMenu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsMonitorMenu_Click(null, null);
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        private void StopMonitorMnu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsStopMonitoringMenu_Click(null, null);
        }

        private void ExpandAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceListTree.SelectedNode.ExpandAll();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void CollapseAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceListTree.SelectedNode.Collapse();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        /// <summary>
        /// Get trace item to draw.
        /// </summary>
        private void TraceView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            lock (TraceEvents)
            {
                if (e.ItemIndex < TraceEvents.Count)
                {
                    TraceEventArgs ei = TraceEvents[e.ItemIndex];
                    ListViewItem item = new ListViewItem(ei.Timestamp.ToString("hh:mm:ss.fff"));
                    item.SubItems.AddRange(new string[] { ei.Type.ToString(), ei.DataToString(!hex)});
                    e.Item = item;
                }
            }
        }

        private void TraceHexMenu_Click(object sender, EventArgs e)
        {
            try
            {
                hex = TraceHexMenu.Checked = !TraceHexMenu.Checked;
                TraceView.Refresh();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void SelectAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TraceView.SelectedIndices.Clear();
                for (int pos = 0; pos != TraceEvents.Count; ++pos)
                {
                    TraceView.SelectedIndices.Add(pos);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void OnReadingChanged(object sender, EventArgs e)
        {
            try
            {                
                IGXPartialRead partialRead = m_DeviceList.SelectedItem as IGXPartialRead;
                if (partialRead != null)
                {
                    ReadLastTB.Enabled = ReadLastRB.Checked;
                    StartPick.Enabled = ToPick.Enabled = ReadFromRB.Checked;
                    PartialReadType oldType = partialRead.Type;
                    if (ReadAllRB.Checked)//Read all values.
                    {
                        partialRead.Type = PartialReadType.All;
                        if (oldType != partialRead.Type)
                        {
                            partialRead.Start = partialRead.End = null;
                        }
                    }
                    else if (ReadLastRB.Checked)//Read last n. Days.
                    {
                        partialRead.Type = PartialReadType.Last;
                        partialRead.Start = DateTime.Now.Date.AddDays(-Convert.ToInt32(ReadLastTB.Text));
                        partialRead.End = DateTime.MaxValue;                            
                    }
                    else if (ReadFromRB.Checked)//Read between days
                    {
                        partialRead.Type = PartialReadType.Range;
                        if (StartPick.Checked)
                        {
                            partialRead.Start = StartPick.Value.Date;
                        }
                        else
                        {
                            partialRead.Start = null;
                        }
                        if (ToPick.Checked)
                        {
                            partialRead.End = ToPick.Value.Date + new TimeSpan(23, 59, 59);
                        }
                        else
                        {
                            partialRead.End = null;
                        }
                    }
                    else if (ReadNewValuesCB.Checked)//Read New values.
                    {
                        partialRead.Type = PartialReadType.New;
                        if (oldType != partialRead.Type)
                        {
                            partialRead.Start = partialRead.End = null;
                        }
                    }   
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }

        private void WriteMenu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsWriteMenu_Click(sender, e);
        }

        private void ReadMenu_Click(object sender, EventArgs e)
        {
            ParentComponent.ToolsReadMenu_Click(sender, e);
        }

        private void CancelTransactionMenu_Click(object sender, EventArgs e)
        {
            //TODO:
        }
    }
}
