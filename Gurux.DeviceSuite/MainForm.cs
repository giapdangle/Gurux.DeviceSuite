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
using System.Windows.Forms;
using Gurux.Common;
using Gurux.Device;
using System.IO;
using Gurux.Device.Editor;
using System.Threading;
using Gurux.DeviceSuite.Director;
using Gurux.DeviceSuite.Editor;
using Gurux.DeviceSuite.Common;
using Gurux.DeviceSuite.Publisher;
using Gurux.Device.PresetDevices;
using System.Collections.Specialized;
using ServiceStack.ServiceClient.Web;
using Gurux.Device.Publisher;
using System.Text;
using Gurux.DeviceSuite.Ami;
using GuruxAMI.Client;

namespace Gurux.DeviceSuite
{    
	internal enum ScheduleImages
	{
		ScheduleList = 0,
		ScheduleItemStop = 1,
		ScheduleItemStart = 2,
		ScheduleItemExecute = 3,
	}

    public enum AppType
    {
        Editor,
        Director,
        Ami
    }

    public partial class MainForm : Form
    {
        AppType SelectedApplication;
        object TransactionObject;
        internal GXTransactionManager TransactionManager;
        ToolStripMenuItem SelectedTraceMenu;
        GXDirector Director;
        internal GXDeviceEditor Editor;
        GXAmi AMI;
        internal MRUManager MruManager;
        GXDeviceManufacturerCollection Published;
        internal bool m_ShowMediaTrace;        
        public MainForm()
        {            
            InitializeComponent();            
            ImportFromDataCollectorMenu.Enabled = ImportMenu.Enabled = false;
            CancelOperationMenu.ShortcutKeyDisplayString = "ESC";
            PropertiesMenu.ShortcutKeys = Keys.Alt | Keys.Enter;
            MruManager = new MRUManager(RecentItemsMenu);
            StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadyTxt;
            MruManager.OnOpenMRUFile += new OpenMRUFileEventHandler(OnOpenMRUFile);
            Director = new GXDirector(this);
            Editor = new GXDeviceEditor(this);
            AMI = new GXAmi(this);
            Director.OnItemActivated += new EventHandler(OnActivated);
            Editor.OnItemActivated += new EventHandler(OnActivated);
            AMI.OnItemActivated += new EventHandler(OnActivated);
            TransactionManager = new GXTransactionManager(OnAsyncStateChange);
            Director.m_DeviceList.OnUpdated += new ItemUpdatedEventHandler(OnUpdated);
            Director.m_DeviceList.OnSelectedItemChanged += new SelectedItemChangedEventHandler(m_DeviceList_OnSelectedItemChanged);
            Director.m_DeviceList.OnTransactionProgress += new TransactionProgressEventHandler(OnTransactionProgress);
        }

        void UpdateUIState(object newObject)
        {
            //Enable menus.
            int devCnt = Director.m_DeviceList.DeviceGroups.GetDevicesRecursive().Count;
            bool isDeviceGroupExists = Director.m_DeviceList.DeviceGroups.Count > 0;
            bool IsDeviceListSelected = newObject is GXDeviceList;
            bool IsDeviceGroupSelected = newObject is GXDeviceGroup || newObject is GXDeviceCollection;
            bool Connecting = GXTransactionManager.IsConnecting(newObject);
            bool IsConnected = IsDeviceListSelected || IsDeviceGroupSelected || GXTransactionManager.IsConnected(newObject);
            bool IsMonitoring = IsDeviceListSelected || IsDeviceGroupSelected || GXTransactionManager.IsMonitoring(newObject);
            NewDeviceMenu.Enabled = isDeviceGroupExists && !IsDeviceListSelected;
            //Connect is enabled when device list or device group is selected or device is selected and device is disconnected.			
            ToolsConnectMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || !IsConnected);
            ToolsDisconnectMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || IsConnected);
            ToolsMonitorMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || !IsMonitoring);
            ToolsStopMonitoringMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || IsMonitoring);
            DeleteToolStripButton.Enabled = DeleteMenu.Enabled = (isDeviceGroupExists && (!IsMonitoring || IsDeviceGroupSelected)) && (IsDeviceGroupSelected || newObject is GXDevice);
            WriteToolStripButton.Enabled = ReadToolStripButton.Enabled = ToolsWriteMenu.Enabled = ToolsReadMenu.Enabled = !Connecting && IsConnected && isDeviceGroupExists && devCnt > 0;
            MonitorToolStripButton.Enabled = ConnectToolStripButton.Enabled = !Connecting && devCnt > 0;
            //ConnectToolStripButton.ImageIndex = TransactionManager.IsConnectedMedia(newObject) ? 10 : 4;
            ConnectToolStripButton.Checked = !Connecting && !(IsDeviceListSelected || IsDeviceGroupSelected) && IsConnected;
            MonitorToolStripButton.Checked = !Connecting && ConnectToolStripButton.Checked && IsMonitoring;
        }

        private void DeviceStateChanged(object sender, GXItemEventArgs e)
        {
            GXDevice device = e.Item as GXDevice;
            GXDeviceEventArgs t = e as GXDeviceEventArgs;
            DeviceStates state = t.Status;
            GXDevice SelDevice = Director.m_DeviceList.SelectedItem is GXDevice ? (GXDevice)Director.m_DeviceList.SelectedItem : null;
            if (SelDevice == null)
            {
                SelDevice = Director.m_DeviceList.SelectedItem is GXProperty ? ((GXProperty)Director.m_DeviceList.SelectedItem).Device : null;
            }
            if (SelDevice == null)
            {
                SelDevice = Director.m_DeviceList.SelectedItem is GXCategory ? ((GXCategory)Director.m_DeviceList.SelectedItem).Device : null;
            }
            if (SelDevice == null)
            {
                SelDevice = Director.m_DeviceList.SelectedItem is GXTable ? (GXDevice)((GXTable)Director.m_DeviceList.SelectedItem).Device : null;
            }
            if (SelDevice == null)
            {
                SelDevice = Director.m_DeviceList.SelectedItem is GXTableCollection ? (GXDevice)((GXTableCollection)Director.m_DeviceList.SelectedItem).Parent : null;
            }
            if (SelDevice == null)
            {
                SelDevice = Director.m_DeviceList.SelectedItem is GXCategoryCollection ? (GXDevice)((GXCategoryCollection)Director.m_DeviceList.SelectedItem).Parent : null;
            }
            if (SelDevice == device)
            {
                if ((state & DeviceStates.Connected | DeviceStates.Disconnected) != 0)
                {
                    UpdateUIState(Director.m_DeviceList.SelectedItem);
                }
            }
        }        

        /// <summary>
        /// Update memus when device state changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnUpdated(object sender, GXItemEventArgs e)
        {
            if (e.Item is GXDevice)
            {
                GXDeviceEventArgs t = e as GXDeviceEventArgs;
                if ((t.Status & (DeviceStates.Connected | DeviceStates.Disconnected)) != 0)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new Gurux.Device.ItemUpdatedEventHandler(DeviceStateChanged), new object[] { sender, e });
                    }
                    else
                    {
                        DeviceStateChanged(sender, e);
                    }
                }                
            }            
        }

        void UpdateTraceLevel(System.Diagnostics.TraceLevel level)
        {
            switch (level)
            {
                case System.Diagnostics.TraceLevel.Error:
                    SelectedTraceMenu = TraceErrorMenu;
                    break;
                case System.Diagnostics.TraceLevel.Warning:
                    SelectedTraceMenu = TraceWarningMenu;
                    break;
                case System.Diagnostics.TraceLevel.Info:
                    SelectedTraceMenu = TraceInfoMenu;
                    break;
                case System.Diagnostics.TraceLevel.Verbose:
                    SelectedTraceMenu = TraceVerboseMenu;
                    break;
                default:
                    SelectedTraceMenu = TraceOffMenu;
                    break;
            }
            OnTraceChanged(SelectedTraceMenu, null);
        }

        internal void OnActivated(object sender, EventArgs e)
        {
            bool enable;
            if (sender is ListView)
            {
                ListView lv = sender as ListView;
                if (lv.VirtualMode)
                {
                    enable = lv.VirtualListSize != 0;
                }
                else
                {
                    enable = lv.SelectedItems.Count != 0;
                }
                ToolsReadMenu.Enabled = ToolsStopMonitoringMenu.Enabled = ToolsMonitorMenu.Enabled = false;
                TraceMenu.Enabled = true;
                if (SelectedApplication == AppType.Ami)                
                {
                    TraceMenu.Enabled = false;
                    if (lv == AMI.DCList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = lv.SelectedIndices.Count != 0;
                        TraceMenu.Enabled = lv.SelectedIndices.Count == 1;
                        if (lv.SelectedItems.Count == 1)
                        {
                            UpdateTraceLevel((lv.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDataCollector).TraceLevel);
                        }
                    }
                    else if (lv == AMI.EventsList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.Schedules)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.TraceView)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.DevicesList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = true;
                        ToolsReadMenu.Enabled = ToolsStopMonitoringMenu.Enabled = 
                            ToolsMonitorMenu.Enabled = AMI.DevicesList.SelectedItems.Count != 0;
                        TraceMenu.Enabled = AMI.DevicesList.SelectedItems.Count == 1;
                        if (AMI.DevicesList.SelectedItems.Count == 1)
                        {
                            UpdateTraceLevel((AMI.DevicesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDevice).TraceLevel);
                        }
                    }
                    else if (lv == AMI.PropertyList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.UnassignedDCList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.TaskList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                    else if (lv == AMI.DeviceProfilesList)
                    {
                        newToolStripButton.Enabled = NewDeviceMenu.Enabled = false;
                    }
                }
            }
            else if (sender is TreeView)
            {
                enable = (sender as TreeView).SelectedNode != null;
            }
            else
            {
                enable = false;
            }
            PropertiesToolStripButton.Enabled = PropertiesMenu.Enabled = CutMenu.Enabled = cutToolStripButton.Enabled = enable;
            CopyMenu.Enabled = copyToolStripButton.Enabled = DeleteToolStripButton.Enabled = DeleteMenu.Enabled = enable;
            selectAllToolStripMenuItem.Enabled = sender is ListView;
        }

        void OnDirty(object sender, GXDirtyEventArgs e)
        {
            UpdateTitle();
        }

        /// <summary>
        /// Update Windows title.
        /// </summary>
        void UpdateTitle()
        {
            string txt;
            if (SelectedApplication == AppType.Director)
            {
                txt = Gurux.DeviceSuite.Properties.Resources.DirectorTxt;
                txt += " " + Director.FileName;
                if (Director.IsDirty)
                {
                    txt += " *";
                }
                this.Text = txt;
            }
            else if (SelectedApplication == AppType.Editor)
            {
                txt = Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt;
                txt += " " + Editor.FileName;
                if (Editor.IsDirty)
                {
                    txt += " *";
                }
                this.Text = txt;
            }
            else if (SelectedApplication == AppType.Ami)
            {
                this.Text = Gurux.DeviceSuite.Properties.Resources.AmiTxt;
            }
        }

        /// <summary>
        /// Open MRU file.
        /// </summary>
        /// <param name="fileName"></param>
        void OnOpenMRUFile(string fileName)
        {
            try
            {
                Director.ClearErrors();
                if (Director.CheckAndWritePropertyValues() != DialogResult.Cancel)
                {
                    Director.OpenFile(fileName);
                }
                UpdateTitle();
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        void m_DeviceList_OnSelectedItemChanged(object sender, GXSelectedItemEventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                //Enable menus.
                int devCnt = Director.m_DeviceList.DeviceGroups.GetDevicesRecursive().Count;
                bool isDeviceGroupExists = Director.m_DeviceList.DeviceGroups.Count > 0;
                bool IsDeviceListSelected = e.NewItem is GXDeviceList;
                bool IsDeviceGroupSelected = e.NewItem is GXDeviceGroup || e.NewItem is GXDeviceCollection;
                bool Connecting = GXTransactionManager.IsConnecting(e.NewItem);
                bool IsConnected = IsDeviceListSelected || IsDeviceGroupSelected || GXTransactionManager.IsConnected(e.NewItem);
                bool IsMonitoring = IsDeviceListSelected || IsDeviceGroupSelected || GXTransactionManager.IsMonitoring(e.NewItem);
                NewDeviceMenu.Enabled = isDeviceGroupExists && !IsDeviceListSelected;
                //Connect is enabled when device list or device group is selected or device is selected and device is disconnected.			
                ToolsConnectMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || !IsConnected);
                ToolsDisconnectMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || IsConnected);
                ToolsMonitorMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || !IsMonitoring);
                ToolsStopMonitoringMenu.Enabled = !Connecting && devCnt > 0 && ((IsDeviceListSelected || IsDeviceGroupSelected) || IsMonitoring);
                DeleteToolStripButton.Enabled = DeleteMenu.Enabled = (isDeviceGroupExists && (!IsMonitoring || IsDeviceGroupSelected)) && (IsDeviceGroupSelected || e.NewItem is GXDevice);
                WriteToolStripButton.Enabled = ReadToolStripButton.Enabled = ToolsWriteMenu.Enabled = ToolsReadMenu.Enabled = !Connecting && IsConnected && isDeviceGroupExists && devCnt > 0;
                MonitorToolStripButton.Enabled = ConnectToolStripButton.Enabled = !Connecting && devCnt > 0;
                //ConnectToolStripButton.ImageIndex = TransactionManager.IsConnectedMedia(newObject) ? 10 : 4;
                ConnectToolStripButton.Checked = !Connecting && !(IsDeviceListSelected || IsDeviceGroupSelected) && IsConnected;
                MonitorToolStripButton.Checked = !Connecting && ConnectToolStripButton.Checked && IsMonitoring;
            }
        }

        /// <summary>
        /// Show active item transaction progress in status bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransactionProgress(object sender, GXTransactionProgressEventArgs e)
        {
            try
            {
                DeviceStates status = e.Status;
                //If user has change item OnSelectedObjectChanged will call this.
                GXDevice device = GXTransactionManager.GetDevice(e.Item);
                GXDevice activeDevice = GXTransactionManager.GetDevice(Director.m_DeviceList.SelectedItem);
                if (device == activeDevice)
                {
                    /* TODO:
                    if ((status & DeviceStates.ReadStart) != 0)
                    {
                        TransactionObject = e.Item;
                        StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadingTxt;
                    }
                    else if ((status & DeviceStates.WriteStart) != 0)
                    {
                        TransactionObject = e.Item;
                        StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.WritingTxt;
                    }
                    else if ((status & (DeviceStates.WriteEnd | DeviceStates.ReadEnd)) != 0)
                    {
                        StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadyTxt;
                    }
                     * */

                    if (status == DeviceStates.None)
                    {
                        Progress.Value = 0;
                        if (device == null)
                        {                            
                            return;
                        }
                        e = device.GetTransactionProgress();
                    }
                    if ((status & (DeviceStates.ReadStart | DeviceStates.WriteStart)) != 0)
                    {                        
                        try
                        {
                            Progress.Maximum = e.Maximum;
                            Progress.Value = e.Current;
                        }
                        catch(Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                        if (TransactionObject == null || TransactionObject == e.Item)
                        {
                            TransactionObject = e.Item;
                            Progress.Visible = true;
                            if ((status & DeviceStates.ReadStart) != 0)
                            {
                                StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadingTxt;
                            }
                            else if ((status & DeviceStates.WriteStart) != 0)
                            {
                                StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.WritingTxt;
                            }
                        }
                    }
                    else if ((status & (DeviceStates.ReadEnd | DeviceStates.WriteEnd)) != 0)
                    {                        
                        if (TransactionObject == e.Item && e.Current == e.Maximum)
                        {
                            TransactionObject = null;
                            Progress.Value = 0;
                            Progress.Visible = false;
                            StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadyTxt;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void OnTransactionProgress(object sender, GXTransactionProgressEventArgs e)
        {
            if (e.Current > e.Maximum)
            {
                System.Diagnostics.Debug.WriteLine("OnTransactionProgress failed. Current value is higger than maximum.");
            }
            else if (this.InvokeRequired)
            {
                this.BeginInvoke(new Gurux.Device.TransactionProgressEventHandler(TransactionProgress), new object[] { sender, e });
            }            
        }

        /// <summary>
        /// Create new device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNewDeviceMenu_Click(object sender, EventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                Director.NewDevice();
            }
            else 
            {
                AMI.NewDevice();
            }
        }

        /// <summary>
        /// Create new device template.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNewTemplateMenu_Click(object sender, EventArgs e)
        {
            Editor.NewTemplate();
        }

        /// <summary>
        /// Load UI settings.
        /// </summary>
        void LoadUISettings()
        {
            AppType type = (AppType) Gurux.DeviceSuite.Properties.Settings.Default.ApplicationType;
            if (type == AppType.Editor)
            {             
                EditorMenu_Click(null, null);
            }
            else if (type == AppType.Director)
            {
                DirectorMenu_Click(null, null);
            }
            else
            {
                ViewAmiMenu_Click(null, null);
            }

            if (!Gurux.DeviceSuite.Properties.Settings.Default.ShowToolBar && !ToolBarMenu.Checked)
            {                
                ToolBarMenu_Click(null, null);
            }
            if (!Gurux.DeviceSuite.Properties.Settings.Default.ShowStatusBar && !StatusBarMnu.Checked)
            {
                StatusBarMnu_Click(null, null);
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.Usermode != (int) UserLevelType.Beginner)
            {
                UsermodeMenu_Click(null, null);
            }
            Director.LoadSettings();
            Editor.LoadSettings();
            AMI.LoadSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Update previous installed settings.
                if (Properties.Settings.Default.UpdateSettings)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.UpdateSettings = false;
                    Properties.Settings.Default.Save();
                }

                Published = new GXDeviceManufacturerCollection();
                GXDeviceManufacturerCollection.Load(Published, GXDeviceManufacturerCollection.PublishedPath);                
                WorkArea.Controls.Add(Editor.EditorPanel);
                Editor.EditorPanel.Visible = false;
                WorkArea.Controls.Add(Director.DirectorPanel);
                Director.DirectorPanel.Visible = false;
                WorkArea.Controls.Add(AMI.AmiPanel);
                AMI.AmiPanel.Visible = false;
                if (!string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.Bounds))
                {
                    try
                    {
                        System.Drawing.Rectangle rc = (System.Drawing.Rectangle)new System.Drawing.RectangleConverter().ConvertFromString(Gurux.DeviceSuite.Properties.Settings.Default.Bounds);
                        if (rc.X > -1 && rc.Y > 0)
                        {
                            this.Bounds = rc;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

                if (Gurux.DeviceSuite.Properties.Settings.Default.MruFiles != null)
                {
                    foreach(string it in Gurux.DeviceSuite.Properties.Settings.Default.MruFiles)
                    {
                        MruManager.Insert(-1, it);
                    }
                }
                m_ShowMediaTrace = ShowMediaTrace.Checked = Gurux.DeviceSuite.Properties.Settings.Default.ShowMediaTrace;
                Editor.Visible = true;
                Editor.Visible = false;
                Director.Visible = true;
                Director.Visible = false;
                AMI.Visible = true;
                AMI.Visible = false;
                LoadUISettings();
                NewDeviceListMenu_Click(null, null);
                //AMI trace level is updated when user selects new DC or device.
                if (SelectedApplication != AppType.Ami)
                {
                    UpdateTraceLevel((System.Diagnostics.TraceLevel)Gurux.DeviceSuite.Properties.Settings.Default.TraceLevel);
                }
                else//Disable trace.
                {
                    TraceMenu.Enabled = false;
                }
                Director.OnDirty += new DirtyEventHandler(this.OnDirty);
                Editor.OnDirty += new DirtyEventHandler(this.OnDirty);                                
                //Do not check updates while debugging.
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    //Check protocol and application updates.                
                    Gurux.Common.CheckUpdatesEventHandler p = (Gurux.Common.CheckUpdatesEventHandler)this.OnCheckUpdatesEnabled;
                    ThreadPool.QueueUserWorkItem(Gurux.Common.GXUpdateChecker.CheckUpdates, p);
                }                
                GXDeviceManufacturerCollection.Load(Editor.Manufacturers);
                PublishMenu.Enabled = Editor.Manufacturers.IsPresetDevices();
                Director.Manufacturers = Editor.Manufacturers;
                //Check new preset device templates from Gurux.
                ThreadPool.QueueUserWorkItem(new WaitCallback(CheckUpdates), this);
                //If AMI is enabled.
                if (Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled)
                {
                    try
                    {
                        AMI.Start(false, false);
                    }
                    catch (Exception ex)
                    {
                        GXCommon.ShowError(this, ex);
                    }
                }

            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Is this first run.
        /// </summary>
        /// <returns></returns>
        public static bool IsFirstRun()
        {
            string path = Path.GetDirectoryName(GXDeviceManufacturerCollection.PublishedPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                GXFileSystemSecurity.UpdateDirectorySecurity(path);
            }
            path = GXDeviceManufacturerCollection.PublishedPath;
            if (!System.IO.File.Exists(path) || new FileInfo(path).Length < 10)
            {
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// Check if there are any updates available in Gurux www server.
        /// </summary>
        /// <returns>Returns true if there are any updates available.</returns>
        bool IsUpdatesAvailable(DateTime lastUpdated, bool forse)
        {
            try
            {
                //Do not check updates while debugging.
                if (!forse && System.Diagnostics.Debugger.IsAttached)
                {
                    return false;
                }
                string path = GXDeviceManufacturerCollection.PublishedPath;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                try
                {
                    GXDeviceManufacturerCollection.Save(Published, GXDeviceManufacturerCollection.PublishedPath);
                    JsonServiceClient Client = new JsonServiceClient(Gurux.DeviceSuite.Properties.Settings.Default.UpdateServer);
                    GXUpdater updates = new GXUpdater();
                    updates.LastUpdated = Published.LastUpdated;
                    GXGXUpdaterResponse ret = Client.Post(updates);                    
                    if (Published.LastUpdated == ret.LastUpdated)
                    {
                        return false;
                    }
                    Published.LastUpdated = ret.LastUpdated;
                    bool update = false;
                    foreach (GXDeviceManufacturer man in ret.Manufacturers)
                    {                        
                        GXDeviceManufacturer man2 = Published.Find(man);
                        if (man2 == null)
                        {
                            Published.Add(man);
                            update = true;
                            continue;
                        }                        
                        foreach (GXDeviceModel model in man.Models)
                        {
                            GXDeviceModel model2 = man2.Models.Find(model);
                            if (model2 == null)
                            {
                                man2.Models.Add(model);
                                //If not interested from the manufacturer updates.
                                if ((man2.Status & DownloadStates.Remove) == 0)
                                {
                                    update = true;
                                }
                                continue;
                            }
                            foreach (GXDeviceVersion dv in model.Versions)
                            {
                                GXDeviceVersion dv2 = model2.Versions.Find(dv);
                                if (dv2 == null)
                                {
                                    man.Models.Add(dv);
                                    //If not interested from the model updates.
                                    if ((model2.Status & DownloadStates.Remove) == 0)
                                    {
                                        update = true;
                                    }
                                    continue;
                                }                                
                                foreach (GXPublishedDeviceProfile dt in dv.Templates)
                                {
                                    GXPublishedDeviceProfile dt2 = dv2.Templates.Find(dt);
                                    if (dt2 == null)
                                    {
                                        dv2.Templates.Add(dt);
                                        //If not interested from the device type updates.
                                        if ((dv2.Status & DownloadStates.Remove) == 0)
                                        {
                                            update = true;
                                        }
                                        continue;
                                    }
                                    foreach (GXDeviceProfileVersion version in dt.Versions)
                                    {
                                        GXDeviceProfileVersion version2 = dt2.Versions.Find(version);
                                        if (version2 == null)
                                        {
                                            //New item is available.
                                            if (version.Status != DownloadStates.Remove)
                                            {
                                                dt2.Versions.Add(version);
                                                //If not interested from the device type updates.
                                                if ((dt2.Status & DownloadStates.Remove) == 0)
                                                {
                                                    update = true;
                                                }
                                            }
                                        }
                                        else if (version.Status == DownloadStates.Remove)
                                        {
                                            dt2.Versions.Remove(version2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GXDeviceManufacturerCollection.Save(Published, GXDeviceManufacturerCollection.PublishedPath);
                    return update;
                }
                catch (Exception ex)
                {
                    GXDeviceManufacturerCollection.Load(Published, GXDeviceManufacturerCollection.PublishedPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        void CheckUpdates(object data)
        {
            try
            {
                DateTime LastUpdateCheck = DateTime.MinValue;
                MainForm main = (MainForm)data;
                //Wait for a while before check updates.
                //Check new updates once a day.
                while (true)
                {
                    LastUpdateCheck = DateTime.Now;
                    bool isConnected = true;
                    if (Environment.OSVersion.Platform != PlatformID.Unix)
                    {
                        Gurux.DeviceSuite.Common.Win32.InternetConnectionState flags = Gurux.DeviceSuite.Common.Win32.InternetConnectionState.INTERNET_CONNECTION_LAN | Gurux.DeviceSuite.Common.Win32.InternetConnectionState.INTERNET_CONNECTION_CONFIGURED;
                        isConnected = Gurux.DeviceSuite.Common.Win32.InternetGetConnectedState(ref flags, 0);
                    }
                    //If there are updates available.
                    if (IsUpdatesAvailable(Published.LastUpdated, false))
                    {
                        main.BeginInvoke(new CheckUpdatesEventHandler(OnNewManufacturers));
                        break;
                    }
                    //Wait for a day before next check.
                    //System.Threading.Thread.Sleep(DateTime.Now.AddDays(1) - DateTime.Now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch
            {
                //It's OK if this fails.
            }
        }

        void OnNewManufacturers()
        {
            updateManufactureSettingsToolStripMenuItem.Visible = true;
        }

        /// <summary>
        /// Enable update button if updates are available.
        /// </summary>
        void OnCheckUpdatesEnabled()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Gurux.Common.CheckUpdatesEventHandler(OnCheckUpdatesEnabled), null);
            }
            else
            {
                this.UpdateProtocolsMenu.Visible = true;
            }
        }
        
        void UpdateMenus(AppType type)
        {
            AddDeviceProfileMenu.Visible = type == AppType.Ami;
            toolStripMenuItem7.Visible = type == AppType.Director;
            SelectedApplication = type;
            Gurux.DeviceSuite.Properties.Settings.Default.ApplicationType = (int) type;
            GXObjectTypeConverter.DesignMode = type == AppType.Editor;
            SaveMenu.Visible = OpenMenu.Visible = openToolStripButton.Enabled = ViewAmiMenu.Visible = type != AppType.Ami;            
            PropertiesToolStripButton.Visible = PropertiesMenu.Visible = ViewDeviceEditorMenu.Visible = type != AppType.Editor;
            ViewDirectorMenu.Visible = type != AppType.Director;            
            NewCategoryMenu.Visible = NewTableMenu.Visible = NewPropertyMenu.Visible = type == AppType.Editor;
            toolStripMenuItem5.Visible = NewDeviceMenu.Visible = type != AppType.Editor;
            ImportFromDataCollectorMenu.Visible = ImportSeparator.Visible = ImportMenu.Visible = ExportMenu.Visible = OpenExportedMenu.Visible = EditorNewMenu.Visible = type == AppType.Editor;
            NewScheduleMenu.Visible = toolStripMenuItem6.Visible = RecentItemsMenu.Visible = NewDeviceGroupMenu.Visible = NewDeviceListMenu.Visible = SaveAsMenu.Visible = type == AppType.Director;
            ToolsWriteMenu.Visible = ToolsDisconnectMenu.Visible = ToolsConnectMenu.Visible = type == AppType.Director;
            ToolsReadMenu.Visible = ToolsStopMonitoringMenu.Visible = ToolsMonitorMenu.Visible = type != AppType.Editor;
            if (type == AppType.Ami)
            {
                ToolsMonitorMenu.Enabled = ToolsStopMonitoringMenu.Enabled = ReadToolStripButton.Enabled = ToolsReadMenu.Enabled = true;
            }
            toolStripSeparator3.Visible = ConnectToolStripButton.Visible = MonitorToolStripButton.Visible = ReadToolStripButton.Visible = WriteToolStripButton.Visible = type == AppType.Director;
            toolStripMenuItem10.Visible = PresetDeviceProfilesMenu.Visible = ProtocolAddinsMenu.Visible = PublishMenu.Visible = type == AppType.Editor;
            UserModMenu.Visible = type == AppType.Editor;
            CloseMenu.Visible = type != AppType.Ami;
            if (type == AppType.Editor)
            {                
                this.NewDeviceMenu.ShortcutKeys = System.Windows.Forms.Keys.None;
                this.EditorNewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
                OpenMenu.Enabled = openToolStripButton.Enabled = GXDeviceList.GetDeviceTypes(false, null).Count != 0 || GXDeviceList.GetDeviceTypes(true, null).Count != 0;
                newToolStripButton.Enabled = EditorNewMenu.Enabled = GXDeviceList.Protocols.Count != 0;
                //Save is disabled if template is not open.
                bool templateOpen = !string.IsNullOrEmpty(Editor.FileName);
                NewCategoryMenu.Visible = NewTableMenu.Visible = NewPropertyMenu.Visible = templateOpen;
                CopyMenu.Enabled = copyToolStripButton.Enabled = DeleteToolStripButton.Enabled = DeleteMenu.Enabled = CloseMenu.Enabled = SaveMenu.Enabled = saveToolStripButton.Enabled = templateOpen;
                ExportMenu.Enabled = templateOpen;
            }
            else
            {
                this.EditorNewMenu.ShortcutKeys = System.Windows.Forms.Keys.None;
                this.NewDeviceMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
                //Save is always enabled.
                SaveMenu.Enabled = saveToolStripButton.Enabled = type != AppType.Ami;
            }
            if (type == AppType.Editor)
            {
                Progress.Visible = false;
            }
            UpdateTitle();             
        }

        /// <summary>
        /// Enable editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DirectorSave())
                {
                    return;
                }
                //If no protocol is not installed yet.
                if (GXDeviceList.Protocols.Count == 0)
                {
                    MessageBox.Show(this, Gurux.DeviceSuite.Properties.Resources.AddInsNotInstalled,
                        Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProtocolAddinsMenu_Click(null, null);
                    if (GXDeviceList.Protocols.Count == 0)
                    {
                        DirectorMenu_Click(null, null);
                        return;
                    }
                }
                AMI.AmiPanel.Visible = false;
                Director.DirectorPanel.Visible = false;
                Editor.LoadSettings();
                Editor.EditorPanel.Visible = true;
                UpdateMenus(AppType.Editor);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(Parent, ex);
            }
        }

        /// <summary>
        /// Enable Director.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectorMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EditorSave())
                {
                    return;
                }
                AMI.AmiPanel.Visible = false;
                Editor.EditorPanel.Visible = false;
                Director.LoadSettings();
                Director.DirectorPanel.Visible = true;
                UpdateMenus(AppType.Director);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(Parent, ex);
            }
        }

        private void ViewAmiMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EditorSave())
                {
                    return;
                }
              
                //If database is not create yet.
                if (!Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled)                
                {
                    MessageBox.Show(this, Gurux.DeviceSuite.Properties.Resources.GuruxAMINotInitialized, 
                            Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GXOptionsForm dlg = new GXOptionsForm(AppType.Ami, AMI);
                    if (dlg.ShowDialog(this) != DialogResult.OK || !Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled)
                    {
                        return;
                    }
                }
                Director.DirectorPanel.Visible = false;
                Editor.EditorPanel.Visible = false;
                AMI.LoadSettings();
                AMI.AmiPanel.Visible = true;
                UpdateMenus(AppType.Ami);
            }
            catch (Exception ex)
            {
                ViewAmiMenu.Enabled = false;
                GXCommon.ShowError(Parent, ex);
            }
        }

        private void ExitMnu_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Save editor settings if needed.
        /// </summary>
        /// <returns></returns>
        bool EditorSave()
        {
            if (Editor.IsDirty)
            {
                DialogResult ret = GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt);
                if (ret == DialogResult.Cancel)
                {
                    return false;
                }
                else if (ret == DialogResult.Yes)
                {
                    Editor.Save();
                }
            }
            return true;
        }

        /// <summary>
        /// Save director settings if needed.
        /// </summary>
        /// <returns></returns>
        bool DirectorSave()
        {
            if (Director.IsDirty)
            {
                DialogResult ret = GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt);
                if (ret == DialogResult.Cancel)
                {
                    return false;
                }
                else if (ret == DialogResult.Yes)
                {
                    Director.Save();
                }
            }
            return true;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Director.m_DeviceList.Schedules.GetActiveSchedules().Count != 0)
                {
                    DialogResult retval = GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.CloseAndKillSchedulesTxt);
                    if (retval == DialogResult.Cancel || retval == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                if (!EditorSave() || !DirectorSave())
                {
                    e.Cancel = true;
                    return;
                }
                //Close Data collectors.
                foreach (GXAmiDataCollectorServer it in AMI.DataCollectors)
                {
                    it.Dispose();
                }
                if (AMI.SchedulerServer != null)
                {
                    AMI.SchedulerServer.Dispose();
                    AMI.SchedulerServer = null;
                }
                if (AMI.Client != null)
                {
                    AMI.Client.Dispose();
                    AMI.Client = null;
                }
                Gurux.DeviceSuite.Properties.Settings.Default.Bounds = new System.Drawing.RectangleConverter().ConvertToString(this.Bounds);
                Gurux.DeviceSuite.Properties.Settings.Default.TraceLevel = (int)Director.TraceLevel;
                List<string> columns = new List<string>();
                Gurux.DeviceSuite.Properties.Settings.Default.ShowToolBar = toolStrip1.Visible;
                Gurux.DeviceSuite.Properties.Settings.Default.ShowStatusBar = statusStrip1.Visible;
                Gurux.DeviceSuite.Properties.Settings.Default.ShowMediaTrace = ShowMediaTrace.Checked;
                Director.SaveSettings();
                Editor.SaveSettings();
                AMI.SaveSettings();
                Gurux.DeviceSuite.Properties.Settings.Default.MruFiles = new System.Collections.Specialized.StringCollection();
                Gurux.DeviceSuite.Properties.Settings.Default.MruFiles.AddRange(MruManager.GetNames());
                Gurux.DeviceSuite.Properties.Settings.Default.Save();
                Editor.Manufacturers.Save();
                /*
                Director.m_DeviceList.StopMonitoring();
                try
				{
                    foreach (GXSchedule schedule in Director.m_DeviceList.Schedules)
					{
						schedule.Stop();
					}
				}
				catch
				{
					//Do nothing.
				}
                Director.m_DeviceList.CloseSchedules();
                 * */
                Director.m_DeviceList.Dispose();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Parent, Ex);
            }
        }

        /// <summary>
        /// Toggle Tool Bar.
        /// </summary>
        private void ToolBarMenu_Click(object sender, EventArgs e)
        {
            ToolBarMenu.Checked = !ToolBarMenu.Checked;
            toolStrip1.Visible = ToolBarMenu.Checked;          
        }

        /// <summary>
        /// Toggle Status Bar.
        /// </summary>
        private void StatusBarMnu_Click(object sender, EventArgs e)
        {
            StatusBarMnu.Checked = !StatusBarMnu.Checked;
            statusStrip1.Visible = StatusBarMnu.Checked;
        }

        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedApplication == AppType.Director)
                {
                    Director.Delete();
                }
                else if (SelectedApplication == AppType.Editor)
                {
                    Editor.Delete();
                    UpdateTitle();
                }
                else if (SelectedApplication == AppType.Ami)
                {
                    AMI.Delete();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Parent, Ex);
            }
        }

        private void CutMenu_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Find control with focus.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Control FindFocusedControl(Control container)
        {
            foreach (Control it in container.Controls)
            {
                if (it.Focused)
                {
                    return it;
                }
            }
            foreach (Control it in container.Controls)
            {
                Control item = FindFocusedControl(it);
                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }

        private void CopyMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Control item = FindFocusedControl(this);                
                if (item != null)
                {
                    ClipboardCopy.CopyDataToClipboard(item);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void PasteMenu_Click(object sender, EventArgs e)
        {

        }		

		/// <summary>
		/// Create new device group.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewDeviceGroupMenu_Click(object sender, EventArgs e)
		{
            Director.NewDeviceGroup();
		}

        /// <summary>
        /// Create new Device list.
        /// </summary>
        public void NewDeviceListMenu_Click(object sender, EventArgs e)
        {
            Director.NewDeviceList();
        }
        
        /// <summary>
        /// Open device list.
        /// </summary>
        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                Director.Open();
                UpdateMenus(AppType.Director);
            }
            else
            {
                Editor.Open();
                ImportFromDataCollectorMenu.Enabled = ImportMenu.Enabled = Editor.IsImportSupported();
                UpdateMenus(AppType.Editor);
            }            
        }

        /// <summary>
        /// Add new Schedule.
        /// </summary>
        private void NewScheduleMenu_Click(object sender, EventArgs e)
        {
            Director.NewSchedule();
            UpdateTitle();
        }

        private void PropertyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        /// <summary>
        /// Create new device list of template.
        /// </summary>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (SelectedApplication == AppType.Director)
            {
                Director.NewDeviceList();
                UpdateMenus(AppType.Director);
            }
            else if (SelectedApplication == AppType.Editor)
            {
                Editor.NewTemplate();
                UpdateMenus(AppType.Editor);
            }
            else if (SelectedApplication == AppType.Ami)
            {
                AMI.NewDevice();
                UpdateMenus(AppType.Ami);
            }
        }

        /// <summary>
        /// Get selected device.
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        private GXDevice GetSelectedDevice(object selectedItem)
        {
            if (selectedItem is GXDevice)
            {
                return selectedItem as GXDevice;
            }
            else if (selectedItem is GXTable)
            {
                return (selectedItem as GXTable).Device;
            }
            else if (selectedItem is GXCategory)
            {
                return (selectedItem as GXCategory).Device;
            }
            else if (selectedItem is GXProperty)
            {
                return (selectedItem as GXProperty).Device;
            }
            else
            {
                return null;
            }
        }
        #region IMRUClient Members

        void OpenMRUFile(string fileName)
        {
            try
            {
                Director.ClearErrors();
                if (Director.CheckAndWritePropertyValues() != DialogResult.Cancel)
                {
                    Director.OpenFile(fileName);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        #endregion

        /// <summary>
        /// Start monitoring.
        /// </summary>
        internal void ToolsMonitorMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (AMI.AmiPanel.Visible)
                {
                    AMI.StartMonitoring();
                }
                else
                {
                    if (GXTransactionManager.IsMonitoring(Director.m_DeviceList.SelectedItem))
                    {
                        ToolsStopMonitoringMenu_Click(null, null);
                    }
                    else
                    {
                        TransactionManager.StartMonitoring(this, Director.m_DeviceList.SelectedItem);
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        void OnAsyncStateChange(object sender, GXAsyncWork work, object[] parameters, AsyncState state, string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AsyncStateChangeEventHandler(this.OnAsyncStateChange), sender, work, state, text);
            }
            else
            {
                CancelOperationMenu.Enabled = state == AsyncState.Start;
                if (state == AsyncState.Start)
                {
                    StatusLbl.Text = text;
                }
                else if (state == AsyncState.Cancel)
                {
                    object target = parameters[0];
                    GXDevice device = GXTransactionManager.GetDevice(target);
                    if (device != null)
                    {
                        device.Cancel();
                    }
                    //ToolsDisconnectMenu_Click(this, null);
                }
                else if (state == AsyncState.Finish)
                {
                    StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadyTxt;
                }
            }
        }

        /// <summary>
        /// Connect device.
        /// </summary>
        internal void ToolsConnectMenu_Click(object sender, EventArgs e)
        {
            if (GXTransactionManager.IsConnected(Director.m_DeviceList.SelectedItem))
            {
                ToolsDisconnectMenu_Click(null, null);
            }
            else
            {
                StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ConnectingTxt;
                TransactionManager.Connect(this, Director.m_DeviceList.SelectedItem);
            }   
        }

        /// <summary>
        /// Disconnect device.
        /// </summary>
        internal void ToolsDisconnectMenu_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.DisconnectingTxt;
                TransactionManager.Disconnect(this, Director.m_DeviceList.SelectedItem);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        internal void ToolsStopMonitoringMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (AMI.AmiPanel.Visible)
                {
                    AMI.StopMonitoring();
                }
                else
                {
                    TransactionManager.StopMonitoring(this, Director.m_DeviceList.SelectedItem);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Read seected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ToolsReadMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (AMI.AmiPanel.Visible)
                {
                    AMI.Read();
                }
                else
                {
                    TransactionManager.Read(this, Director.m_DeviceList.SelectedItem);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }	
        }

        internal void ToolsWriteMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TransactionManager.Write(this, Director.m_DeviceList.SelectedItem);

            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void PublishMenu_Click(object sender, EventArgs e)
        {
            try
            {
                GXPublisherDlg dlg = new GXPublisherDlg(Editor.Manufacturers);
                dlg.ShowDialog(this);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
            
        }

        /// <summary>
        /// Show available protocols.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProtocolAddinsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                string[] disabledItems;
                ProtocolUpdateStatus status = Gurux.Common.GXUpdateChecker.ShowUpdates(this, true, false, out disabledItems);
                Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols = new StringCollection();
                Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols.AddRange(disabledItems);
                UpdateProtocolsMenu.Visible = false;
                newToolStripButton.Enabled = EditorNewMenu.Enabled = GXDeviceList.Protocols.Count != 0;
                if ((status & ProtocolUpdateStatus.Restart) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.RestartTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if ((status & ProtocolUpdateStatus.Changed) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.ProtocolsUpdatedTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GXDeviceList.Update();
                    newToolStripButton.Enabled = EditorNewMenu.Enabled = GXDeviceList.Protocols.Count != 0;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                Director.Save();
            }
            else
            {
                Editor.Save();
            }
            UpdateTitle();
        }

        private void SaveAsMenu_Click(object sender, EventArgs e)
        {
            Director.SaveFileAs();
        }

        /// <summary>
        /// Show Properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesMenu_Click(object sender, EventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                Director.ShowProperties();
            }
            else if (AMI.AmiPanel.Visible)
            {
                AMI.ShowProperties();
            }
        }

        private void EditorNewMenu_Click(object sender, EventArgs e)
        {
            Editor.NewTemplate();
            ImportFromDataCollectorMenu.Enabled = ImportMenu.Enabled = Editor.IsImportSupported();
            UpdateMenus(AppType.Editor);
        }

        void ShowPresetSettings(bool InitializePreset)
        {
            try
            {
                Editor.Manufacturers.Save();
                GXDeviceManufacturerCollection.Save(Published, GXDeviceManufacturerCollection.PublishedPath);
                GXPresetDevicesForm dlg = new GXPresetDevicesForm(InitializePreset, Editor.Manufacturers, Published);
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    GXDeviceManufacturerCollection.Load(Published, GXDeviceManufacturerCollection.PublishedPath);
                    GXDeviceManufacturerCollection.Load(Editor.Manufacturers);
                }
                else
                {
                    GXDeviceManufacturerCollection.Save(Published, GXDeviceManufacturerCollection.PublishedPath);
                    PublishMenu.Enabled = Editor.Manufacturers.IsPresetDevices();                    
                    Editor.Manufacturers.Save();
                    GXDeviceList.Update();
                }
            }
            catch (Exception ex)
            {
                GXDeviceManufacturerCollection.Load(Published, GXDeviceManufacturerCollection.PublishedPath);
                GXDeviceManufacturerCollection.Load(Editor.Manufacturers);
                GXDeviceList.Update();
                GXCommon.ShowError(this, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show manufacturer settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManufacturersMenu_Click(object sender, EventArgs e)
        {
            ShowPresetSettings(true);
        }

        private void updateManufactureSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                updateManufactureSettingsToolStripMenuItem.Visible = false;
                ShowPresetSettings(false);                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Create new category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCategoryMenu_Click(object sender, EventArgs e)
        {
            Editor.NewObject();
            UpdateTitle();
        }

        private void NewTableMenu_Click(object sender, EventArgs e)
        {
            Editor.NewObject();
            UpdateTitle();
        }

        private void NewPropertyMenu_Click(object sender, EventArgs e)
        {
            Editor.NewObject();
            UpdateTitle();
        }

        private void ExportMenu_Click(object sender, EventArgs e)
        {
            Editor.Export();
            UpdateTitle();
        }

        /// <summary>
        /// Import registers from the device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportMenu_Click(object sender, EventArgs e)
        {
            Editor.Import(false);
            UpdateTitle();
        }

        private void ImportFromDataCollectorMenu_Click(object sender, EventArgs e)
        {
            Editor.Import(true);
            UpdateTitle();
        }

        private void OnTraceChanged(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                SelectedTraceMenu.Checked = false;
                SelectedTraceMenu = item;
                item.Checked = true;
                Editor.TraceView.Visible = SelectedTraceMenu != TraceOffMenu;
                if (SelectedTraceMenu == TraceOffMenu)
                {
                    Director.TraceLevel = System.Diagnostics.TraceLevel.Off;
                }
                else if (SelectedTraceMenu == TraceErrorMenu)
                {
                    Director.TraceLevel = System.Diagnostics.TraceLevel.Error;
                }
                else if (SelectedTraceMenu == TraceWarningMenu)
                {
                    Director.TraceLevel = System.Diagnostics.TraceLevel.Warning;
                }
                else if (SelectedTraceMenu == TraceInfoMenu)
                {
                    Director.TraceLevel = System.Diagnostics.TraceLevel.Info;
                }
                else if (SelectedTraceMenu == TraceVerboseMenu)
                {
                    Director.TraceLevel = System.Diagnostics.TraceLevel.Verbose;
                }
                ShowMediaTrace.Enabled = SelectedTraceMenu == TraceVerboseMenu;
                Editor.TraceLevel = Director.TraceLevel;
                if (SelectedApplication == AppType.Ami)
                {
                    AMI.TraceLevel = Director.TraceLevel;
                }
                foreach (GXDevice it in Director.m_DeviceList.DeviceGroups.GetDevicesRecursive())
                {
                    it.Trace = Director.TraceLevel;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }
      
        /// <summary>
        /// Close 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenu_Click(object sender, EventArgs e)
        {
            if (Director.DirectorPanel.Visible)
            {
                Director.NewDeviceList();
                UpdateMenus(AppType.Editor);
            }
            else
            {
                ImportFromDataCollectorMenu.Enabled = ImportMenu.Enabled = false;
                Editor.CloseFile();
                UpdateMenus(AppType.Editor);
            }            
        }

        private void ConnectToolStripButton_Click(object sender, EventArgs e)
        {
            if (GXTransactionManager.IsConnected(Director.m_DeviceList.SelectedItem))
            {
                ToolsDisconnectMenu_Click(null, null);
            }
            else
            {
                ToolsConnectMenu_Click(null, null);
            }
        }       

        private void ContentsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                string HelpPath = string.Empty;
#if DEBUG
                HelpPath = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
#else
				HelpPath = Application.StartupPath;
#endif //DEBUG
                HelpPath += "\\GuruxDeviceSuite.chm";
                bool exists = File.Exists(HelpPath);
                if (exists)
                {
                    Help.ShowHelp(null, HelpPath);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.gurux.org/index.php?q=GuruxDeviceSuiteHelp");
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Show library versions.
        /// </summary>
        private void LibraryVersionsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                LibraryVersionsDlg dlg = new LibraryVersionsDlg();
                dlg.ShowDialog(this);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Check new updates.
        /// </summary>
        private void CheckForUpdatesMenu_Click(object sender, EventArgs e)
        {
            try
            {                           
                ProtocolUpdateStatus status = Gurux.Common.GXUpdateChecker.ShowUpdates(this, true, true);
                UpdateProtocolsMenu.Visible = false;
                openToolStripButton.Enabled = newToolStripButton.Enabled = OpenMenu.Enabled = FileNewMnu.Enabled = GXDeviceList.Protocols.Count != 0;
                if ((status & ProtocolUpdateStatus.Restart) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.RestartTxt, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if ((status & ProtocolUpdateStatus.Changed) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.ProtocolsUpdatedTxt, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openToolStripButton.Enabled = newToolStripButton.Enabled = OpenMenu.Enabled = FileNewMnu.Enabled = GXDeviceList.Protocols.Count != 0;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }        
        }

        /// <summary>
        /// About dialog.
        /// </summary>
        private void AboutMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Gurux.Common.GXAboutForm lic = new Gurux.Common.GXAboutForm();
                lic.Title = Gurux.DeviceSuite.Properties.Resources.AboutGuruxDeviceSuiteTxt;
                lic.Application = Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt;
                lic.AboutText = Gurux.DeviceSuite.Properties.Resources.ForMoreInfoTxt;
                lic.CopyrightText = Gurux.DeviceSuite.Properties.Resources.CopyrightTxt;
                //Get version info
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(ass.Location);
                lic.ShowAbout(this, info.FileVersion);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Restore default layout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetWindowLayoutMenu_Click(object sender, EventArgs e)
        {
            try
            {
                StringCollection collectors = Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors;
                int maxerr = Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount;
                int maxTrace = Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount;
                bool amiEnabled = Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled;
                string amiHost = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
                string amiUser = Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName;
                string amiPW = Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword;
                string amiDbHost = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName;
                string amiDb = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName;
                string amiDbPrefix = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix;
                string amiDbUser = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName;
                string amiDbPw = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword;
                StringCollection mru = Gurux.DeviceSuite.Properties.Settings.Default.MruFiles;
                StringCollection disabled = Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols;
                Gurux.DeviceSuite.Properties.Settings.Default.Reset();
                Gurux.DeviceSuite.Properties.Settings.Default.MruFiles = mru;
                Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols = disabled;
                Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount = maxerr;
                Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount = maxTrace;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled = amiEnabled;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = amiHost;                
                Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName = amiUser;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword = amiPW;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName = amiDbHost;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName = amiDb;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix = amiDbPrefix;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName = amiDbUser;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword = amiDbPw;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors = collectors;
                LoadUISettings();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void OpenExportedMenu_Click(object sender, EventArgs e)
        {
            Editor.OpenExported();
        }

        /// <summary>
        /// Update new protocol AddIns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProtocolsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ProtocolUpdateStatus status = Gurux.Common.GXUpdateChecker.ShowUpdates(this, true, true);
                UpdateProtocolsMenu.Visible = false;
                newToolStripButton.Enabled = EditorNewMenu.Enabled = GXDeviceList.Protocols.Count != 0;
                if ((status & ProtocolUpdateStatus.Restart) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.RestartTxt, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if ((status & ProtocolUpdateStatus.Changed) != 0)
                {
                    MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.ProtocolsUpdatedTxt, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GXDeviceList.Update();
                    newToolStripButton.Enabled = EditorNewMenu.Enabled = GXDeviceList.Protocols.Count != 0;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        /// <summary>
        /// User mode has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsermodeMenu_Click(object sender, EventArgs e)
        {
            bool normal = NormalMenu.Checked;
            NormalMenu.Checked = !normal;
            ExperiencedMenu.Checked = normal;
            GXDesigner.Visibility = !normal ? UserLevelType.Beginner : UserLevelType.Experienced;
            Gurux.DeviceSuite.Properties.Settings.Default.Usermode = (int)GXDesigner.Visibility;                
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control item = FindFocusedControl(this);
                if (item is ListView)
                {
                    ListView lv = item as ListView;
                    lv.SelectedIndices.Clear();
                    for (int pos = 0; pos != lv.Items.Count; ++pos)
                    {
                        lv.SelectedIndices.Add(pos);
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        /// <summary>
        /// Import device template to AMI.
        /// </summary>
        private void AddDeviceProfilesMenu_Click(object sender, EventArgs e)
        {
            AMI.ImportDeviceProfiles();
        }

        /// <summary>
        /// Show options...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolsOptionsMenu_Click(object sender, EventArgs e)
        {
            GXOptionsForm dlg = new GXOptionsForm(SelectedApplication, AMI);
            dlg.ShowDialog(this);
        }

        /// <summary>
        /// Report new buf to bugtracker.
        /// </summary>
        private void ReportABugMenu_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.gurux.fi/bugtracker/bugtracker.php");
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void ShowMediaTrace_Click(object sender, EventArgs e)
        {
            m_ShowMediaTrace = !m_ShowMediaTrace;
            ShowMediaTrace.Checked = m_ShowMediaTrace;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                CancelOperationMenu_Click(this, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Cancel current transaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOperationMenu_Click(object sender, EventArgs e)
        {
            DialogResult ret = GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, "Are you sure that you want to cancel current transacion?");
            if (ret == DialogResult.Yes)
            {
                if (this.AMI.TransactionWork != null)
                {
                    this.AMI.TransactionWork.Cancel();
                }
                this.TransactionManager.Cancel();
            }            
        }      
    }
}
