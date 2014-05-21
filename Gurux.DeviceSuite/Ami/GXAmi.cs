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
using Gurux.Common;
using System.IO;
using System.Xml;
using System.Collections;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.DeviceSuite.Common;
using Gurux.Device.PresetDevices;
using Gurux.DeviceSuite.Director;
using GuruxAMI.Common;
using GuruxAMI.Server;
using GuruxAMI.Client;
using ServiceStack.OrmLite;
using Gurux.Device;
using System.Threading;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Gurux.DeviceSuite.Ami
{
    /// <summary>
    /// Server is closed or started.
    /// </summary>
    /// <param name="started"></param>
    delegate void ServerStateChanged(bool started);

    delegate void ShowDeviceProfilesEventHandler(GXAmiDeviceProfile[] profiles);

    public partial class GXAmi : Form
    {
        internal GXAmiSchedulerServer SchedulerServer;
        //Swap object and tree node. This makes item search faster.
        System.Collections.Hashtable DCToListViewItem = new System.Collections.Hashtable();
        System.Collections.Hashtable UnassignedDCToListViewItem = new System.Collections.Hashtable();
        System.Collections.Hashtable DeviceToListItem = new System.Collections.Hashtable();
        System.Collections.Hashtable PropertyToListItem = new System.Collections.Hashtable();        
        Dictionary<ulong, ListViewItem> ScheduleToListViewItem = new Dictionary<ulong, ListViewItem>();
        Dictionary<ulong, ListViewItem> TaskToListItem = new Dictionary<ulong, ListViewItem>();
        Dictionary<ulong, GXAmiDataTable> Tables = new Dictionary<ulong, GXAmiDataTable>();
        List<GXAmiDataCollectorServer> InternalDCs = new List<GXAmiDataCollectorServer>();
        internal GXAsyncWork TransactionWork;
#if DEBUG
        //This is used in debug purposes to insure that IDs are unigue.
        System.Collections.Hashtable m_ObjectIDs = new System.Collections.Hashtable();
#endif //DEBUG
        System.Collections.Hashtable m_IntervalCntSwapNode = new System.Collections.Hashtable();
        public List<GXAmiDataCollectorServer> DataCollectors = new List<GXAmiDataCollectorServer>();
        internal GXAmiClient Client;
        Thread ServerThread;
        public AutoResetEvent Started = new AutoResetEvent(false);
        public AutoResetEvent ClosingApplication = new AutoResetEvent(false);
        public AutoResetEvent ServerThreadClosed = new AutoResetEvent(false);        
        bool hex;
        List<GXAmiTaskLog> LogTasks = new List<GXAmiTaskLog>();
        List<GXAmiTrace> TraceEvents = new List<GXAmiTrace>();
        List<GXAmiTrace> SelectedTraceEvents = new List<GXAmiTrace>();
        bool TracePause = false;
        bool TasksFollowLast = true;
        bool EventFollowLast = true;
        bool TraceFollowLast = false;
        public event System.EventHandler OnItemActivated;
        public System.Diagnostics.TraceLevel m_TraceLevel = System.Diagnostics.TraceLevel.Off; 
        MainForm ParentComponent;

        public System.Diagnostics.TraceLevel TraceLevel
        {
            get
            {
                return m_TraceLevel;
            }
            set
            {
                if (m_TraceLevel != value)
                {
                    if (DCList.Focused)
                    {
                        if (DCList.SelectedItems.Count == 0)
                        {
                            throw new Exception("Select Data Collector to trace.");
                        }
                        GXAmiDataCollector dc = DCList.SelectedItems[0].Tag as GXAmiDataCollector;
                        dc.TraceLevel = value;
                        Client.SetTraceLevel(dc, value);
                        Client_OnTraceAdded(null, Client.GetTraces(dc));
                    }
                    else if (DevicesList.Focused)
                    {
                        if (DevicesList.SelectedItems.Count == 0)
                        {
                            throw new Exception("Select device to trace.");
                        }
                        GuruxAMI.Common.GXAmiDevice device = DevicesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDevice;
                        device.TraceLevel = value;
                        Client.SetTraceLevel(device, value);
                        Client_OnTraceAdded(null, Client.GetTraces(device));
                    }
                    else
                    {
                        throw new Exception("Select Data Collector or Device to trace.");
                    }                    
                    m_TraceLevel = value;
                    if (m_TraceLevel == System.Diagnostics.TraceLevel.Off)
                    {
                        if (TabControl1.TabPages.Contains(TraceTab))
                        {
                            TabControl1.TabPages.Remove(TraceTab);
                        }
                    }
                    else
                    {
                        if (!TabControl1.TabPages.Contains(TraceTab))
                        {
                            TabControl1.TabPages.Add(TraceTab);
                        }
                    }                    
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GXAmi(MainForm parent)            
        {
            ParentComponent = parent;            
            InitializeComponent();            
            this.TopLevel = false;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            TableCB.Enabled = false;
            LoadDefaultImages();
            OnServerStateChanged(false);
            CountTB.Enabled = IndexTB.Enabled = false;
            TableCB_SelectedIndexChanged(null, null);
            ReadAllRB_CheckedChanged(null, null);
            //Remove task log tab for now.
            TabControl1.TabPages.Remove(TaskLogTab);

            //Remove Trace tab. It is added later when user selects trace level.
            TabControl1.TabPages.Remove(TraceTab);

            //Add search DC text box.
            Bitmap bm = Gurux.DeviceSuite.Properties.Resources.Search;
            bm.MakeTransparent();
            PictureBox pic = new PictureBox();
            pic.BackgroundImage = bm;
            pic.Width = pic.BackgroundImage.Width;
            pic.Height = pic.BackgroundImage.Height;
            pic.Dock = DockStyle.Right;
            pic.Cursor = Cursors.Arrow;
            pic.Click += new System.EventHandler(delegate(object sender, EventArgs e)
            {
                SearchDC();
            });
            DCSearchBtn.Controls.Add(pic);

            //Add search device text box.
            pic = new PictureBox();
            pic.BackgroundImage = bm;
            pic.Width = pic.BackgroundImage.Width;
            pic.Height = pic.BackgroundImage.Height;
            pic.Dock = DockStyle.Right;
            pic.Cursor = Cursors.Arrow;
            pic.Click += new System.EventHandler(delegate(object sender, EventArgs e)
            {
                SearchDevice();
            });
            DeviceSearchBtn.Controls.Add(pic);

            //TODO: Table tab is removed for now.
            TableTabs.TabPages.Remove(ReadTab);
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

        void OnServerStateChanged(bool started)
        {
            this.AmiPanel.Enabled = started;
        }

        GXAmiDataCollectorServer StartDataCollector(Guid guid)
        {
            string baseUr = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
            if (baseUr.Contains("*"))
            {
                baseUr = baseUr.Replace("*",  "localhost");
            }
            GXAmiDataCollectorServer collector = new GXAmiDataCollectorServer(baseUr, guid);
            DataCollectors.Add(collector);
            collector.OnTasksAdded += new TasksAddedEventHandler(DC_OnTasksAdded);
            collector.OnTasksClaimed += new TasksClaimedEventHandler(DC_OnTasksClaimed);
            collector.OnTasksRemoved += new TasksRemovedEventHandler(DC_OnTasksRemoved);
            collector.OnError += new Gurux.Common.ErrorEventHandler(DC_OnError);
            collector.OnAvailableSerialPorts += new AvailableSerialPortsEventHandler(DC_OnAvailableSerialPorts);
            collector.Init(null);
            if (!Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Contains(guid.ToString()))
            {
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Add(guid.ToString());
            }
            InternalDCs.Add(collector);
            return collector;
        }

        /// <summary>
        /// Add new Data collector. This is called when database is created and we want to add new DC.
        /// </summary>
        /// <param name="dc"></param>
        public void AddDataCollector(GXAmiDataCollector[] collectors)
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors == null)
            {
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors = new System.Collections.Specialized.StringCollection();
            }
            foreach (GXAmiDataCollector dc in collectors)
            {
                if (dc.Internal)
                {
                    if (string.IsNullOrEmpty(dc.Name))
                    {
                        dc.Description = dc.Name = "Default data collector ";
                    }
                    dc.Medias = Gurux.Communication.GXClient.GetAvailableMedias(true);
                    dc.SerialPorts = Gurux.Serial.GXSerial.GetPortNames();
                    dc.MAC = GXAmiClient.GetMACAddressAsString();
                    GXAmiDataCollectorServer cl = StartDataCollector(dc.Guid);
                    cl.Update(dc);
                    dc.Internal = true;                    
                }
            }
        }

        /// <summary>
        /// Get info from available serial ports.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void DC_OnAvailableSerialPorts(object sender, GXSerialPortInfo e)
        {
            e.SerialPorts = Gurux.Serial.GXSerial.GetPortNames();
        }

        /// <summary>
        /// Error has occurred.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        static void DC_OnError(object sender, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        /// <summary>
        /// Task is removed from DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        static void DC_OnTasksRemoved(object sender, GXAmiTask[] tasks)
        {
            /*
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} removed.", it.Id));
            }
             * */
        }

        /// <summary>
        /// Task is claimed to DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        static void DC_OnTasksClaimed(object sender, GXAmiTask[] tasks)
        {
            /*
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} claimed.", it.Id));
            }
             * */
        }

        /// <summary>
        /// New task is added to DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        static void DC_OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            /*
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} Added.", it.Id));
            }
             * */
        }

        delegate void DataCollectorsClearEventHandler(object sender);

        void OnDataCollectorsClear(object sender)
        {
            //Clear items...
            DCList.Items.Clear();
            DCToListViewItem.Clear();                
        }

        public void Start(bool waitStart, bool initialize)
        {
            //if DB is configured.
            if (!string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName) && 
                (ServerThread == null || !ServerThread.IsAlive))
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new DataCollectorsClearEventHandler(OnDataCollectorsClear), this);
                }
                else
                {
                    OnDataCollectorsClear(this);
                }
                ServerThread = new Thread(new ParameterizedThreadStart(StartServer));
                ServerThread.IsBackground = true;
                ServerThread.Start(initialize);
                if (waitStart)
                {
                    Started.WaitOne();
                }
            }
        }

        public void CloseServer()
        {
            if (ServerThread != null)
            {
                ClosingApplication.Set();
                //Wait until server is closed.
                ServerThreadClosed.WaitOne(10000);
            }
        }

        void StartServer(object param)
        {
            bool initialize = (bool) param;
            GXDBService Server = null;
            try
            {
                this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), 0, "Starting GuruxAMI...");
                string baseUr = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
                bool useServer = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName.Contains("localhost") || Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName.Contains("*");
                if (useServer)
                {
                    string connStr = null;
                    //If DB is set
                    if (!string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName))
                    {
                        connStr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                         Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName,
                         Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword);
                    }
                    try
                    {
                        OrmLiteConnectionFactory f = new OrmLiteConnectionFactory(connStr, false, ServiceStack.OrmLite.MySql.MySqlDialectProvider.Instance);
                        f.AutoDisposeConnection = true;
                        Server = new GXDBService(baseUr, f,
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix);
                        if (Server.IsDatabaseCreated())
                        {
                            Server.Update();
                        }
                    }
                    catch (System.Net.HttpListenerException)
                    {
                        //If GuruxAMI service is already started.
                        Server = null;
                    }
                }
                else
                {
                    baseUr = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
                }

                if (Client == null)
                {
                    if (baseUr.Contains("*"))
                    {
                        baseUr = baseUr.Replace("*", "localhost");
                    }
                    Client = new GXAmiClient(baseUr,
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName,
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword);
                }

                //Create schedule server.
                if (SchedulerServer == null)
                {
                    this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), -1, "Starting GuruxAMI schedule server...");
                    SchedulerServer = new GXAmiSchedulerServer(baseUr,
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName,
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword);                    
                }
                if (Client.IsDatabaseCreated())
                {                    
                    Client.OnDeviceProfilesAdded += new DeviceProfilesAddedEventHandler(Client_OnDeviceProfilesAdded);
                    Client.OnDeviceProfilesRemoved += new DeviceProfilesRemovedEventHandler(Client_OnDeviceProfilesRemoved);
                    Client.OnDataCollectorsAdded += new DataCollectorsAddedEventHandler(Client_OnDataCollectorsAdded);
                    Client.OnDataCollectorsRemoved += new DataCollectorsRemovedEventHandler(Client_OnDataCollectorsRemoved);
                    Client.OnDataCollectorsUpdated += new DataCollectorsUpdatedEventHandler(Client_OnDataCollectorsUpdated);
                    Client.OnDeviceErrorsAdded += new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded);
                    Client.OnDeviceErrorsRemoved += new DeviceErrorsRemovedEventHandler(Client_OnDeviceErrorsRemoved);
                    Client.OnDeviceGroupsAdded += new DeviceGroupsAddedEventHandler(Client_OnDeviceGroupsAdded);
                    Client.OnDeviceGroupsRemoved += new DeviceGroupsRemovedEventHandler(Client_OnDeviceGroupsRemoved);
                    Client.OnDeviceGroupsUpdated += new DeviceGroupsUpdatedEventHandler(Client_OnDeviceGroupsUpdated);
                    Client.OnDevicesAdded += new DevicesAddedEventHandler(Client_OnDevicesAdded);
                    Client.OnDevicesRemoved += new DevicesRemovedEventHandler(Client_OnDevicesRemoved);
                    Client.OnDevicesUpdated += new DevicesUpdatedEventHandler(Client_OnDevicesUpdated);
                    Client.OnSystemErrorsAdded += new SystemErrorsAddedEventHandler(Client_OnSystemErrorsAdded);
                    Client.OnSystemErrorsRemoved += new SystemErrorsRemovedEventHandler(Client_OnSystemErrorsRemoved);
                    Client.OnTasksAdded += new TasksAddedEventHandler(Client_OnTasksAdded);
                    Client.OnTasksClaimed += new TasksClaimedEventHandler(Client_OnTasksClaimed);
                    Client.OnTasksRemoved += new TasksRemovedEventHandler(Client_OnTasksRemoved);
                    Client.OnValueUpdated += new ValueUpdatedEventHandler(Client_OnValueUpdated);
                    Client.OnDeviceStateChanged += new DeviceStateChangedEventHandler(Client_OnDeviceStateChanged);
                    Client.OnDataCollectorStateChanged += new DataCollectorsStateChangedEventHandler(Client_OnDataCollectorStateChanged);
                    Client.OnTraceStateChanged += new TraceStateChangedEventHandler(Client_OnTraceStateChanged);
                    Client.OnTraceAdded += new TraceAddedEventHandler(Client_OnTraceAdded);
                    Client.OnTraceClear += new TraceClearEventHandler(Client_OnTraceClear);
                    Client.OnSchedulesAdded += new SchedulesAddedEventHandler(Client_OnSchedulesAdded);
                    Client.OnSchedulesRemoved += new SchedulesRemovedEventHandler(Client_OnSchedulesRemoved);
                    Client.OnSchedulesUpdated += new SchedulesUpdatedEventHandler(Client_OnSchedulesUpdated);
                    Client.OnSchedulesStateChanged += new SchedulesStateChangedEventHandler(Client_OnSchedulesStateChanged);
                    Client.StartListenEvents();
                    SchedulerServer.Start();
                    //Get all DC automatically.
                    GXAmiDataCollector[] collectors = new GXAmiDataCollector[0];
                    if (Gurux.DeviceSuite.Properties.Settings.Default.GetDataCollectorsAutomatically)
                    {
                        this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), -1, "Starting GuruxAMI data collectors...");
                        collectors = Client.GetDataCollectors();
                        Client_OnDataCollectorsAdded(Client, collectors);
                    }
                    if (Gurux.DeviceSuite.Properties.Settings.Default.GetDevicesAutomatically)
                    {
                        this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), -1, "Starting GuruxAMI devices...");
                        DateTime start = DateTime.Now;
                        GXAmiDevice[] devices = Client.GetDevices(false, DeviceContentType.Main);                        
                        Client_OnDevicesAdded(Client, null, devices);
                        System.Diagnostics.Debug.WriteLine("Getting devices: " + (DateTime.Now - start).TotalSeconds.ToString());                      
                    }
                    //Get all tasks and show them.
                    Client_OnTasksAdded(Client, Client.GetTasks(TaskState.All, false));
                    //Get all schedules and show them.
                    Client_OnSchedulesAdded(Client, Client.GetSchedules());

                    //Get all device profiles and show them
                    GXAmiDeviceProfile[] profiles = Client.GetDeviceProfiles(true, false);
                    Client_OnDeviceProfilesAdded(Client, profiles);
                    this.BeginInvoke(new ServerStateChanged(OnServerStateChanged), true);

                    //Start Data Collectors.
                    if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors != null)
                    {
                        List<string> tmp = new List<string>(Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Cast<string>());
                        foreach (string str in tmp)
                        {
                            Guid guid = new Guid(str);
                            try
                            {
                                GXAmiDataCollector dc = null;
                                foreach (GXAmiDataCollector it2 in collectors)
                                {
                                    if (it2.Guid == guid)
                                    {
                                        dc = it2;
                                        break;
                                    }
                                }
                                if (dc == null)
                                {
                                    dc = Client.GetDataCollectorByGuid(guid);
                                    //DC might be null if DC not found from the DB.
                                    if (dc != null)
                                    {
                                        Client_OnDataCollectorsAdded(Client, new GXAmiDataCollector[] { dc });
                                    }
                                    else
                                    {
                                        Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Remove(str);
                                    }
                                }
                                if (dc != null)
                                {
                                    dc.Internal = true;
                                    StartDataCollector(guid);
                                }
                            }
                            //If DC is removed.
                            catch (UnauthorizedAccessException)
                            {
                                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Remove(str);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("GuruxAMI database not created.");
                }
                Started.Set();
                this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), 1, Gurux.DeviceSuite.Properties.Resources.ReadyTxt);
            }
            catch (Exception ex)
            {
                if (initialize)
                {
                    this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), 1, Gurux.DeviceSuite.Properties.Resources.ReadyTxt);
                }
                else
                {
                    this.BeginInvoke(new UpdateStatusEventHandler(OnUpdateStatusEventHandler), 2, "GuruxAMI start failed: " + ex.Message);
                }
                Started.Set();
                if (Client != null)
                {
                    Client.Dispose();
                    Client = null;
                }
            }
            ClosingApplication.WaitOne();

            if (SchedulerServer != null)
            {
                SchedulerServer.Stop();
                SchedulerServer = null;
            }
            if (Server != null)
            {
                Server.Stop();
                Server = null;
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            foreach (GXAmiDataCollectorServer it in DataCollectors)
            {
                it.Close();
            }
            ServerThreadClosed.Set();
        }

        void UpdateScheduleImage(ListViewItem li, ScheduleState status)
        {
            switch (status)
            {
                case ScheduleState.TaskStart:
                case ScheduleState.TaskRun:
                    li.ImageIndex = (int)ScheduleImages.ScheduleItemExecute;
                    break;
                case ScheduleState.TaskFinish:
                    li.ImageIndex = (int)ScheduleImages.ScheduleItemStart;
                    //Update next schedule date.
                    //                                Schedules.Sort();
                    break;
                case ScheduleState.Start:
                case ScheduleState.Run:
                    li.ImageIndex = (int)ScheduleImages.ScheduleItemStart;
                    break;
                case ScheduleState.End:
                case ScheduleState.None:
                    li.ImageIndex = (int)ScheduleImages.ScheduleItemStop;
                    break;
                default:
                    //Unknown state.
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            (li.Tag as GXAmiSchedule).Status = status;
        }

        void OnSchedulesStateChanged(object sender, GXAmiSchedule[] schedules)
        {
            try
            {
                lock (ScheduleToListViewItem)
                {
                    foreach (GXAmiSchedule it in schedules)
                    {
                        ListViewItem li = ScheduleToListViewItem[it.Id];
                        UpdateScheduleImage(li, it.Status);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnSchedulesStateChanged(object sender, GXAmiSchedule[] schedules)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SchedulesStateChangedEventHandler(OnSchedulesStateChanged), sender, schedules);
            }
            else
            {
                OnSchedulesStateChanged(sender, schedules);
            }
        }

        void OnSchedulesUpdated(object sender, GXAmiSchedule[] schedules)
        {
            try
            {
                lock (ScheduleToListViewItem)
                {
                    foreach (GXAmiSchedule it in schedules)
                    {
                        ListViewItem li = ScheduleToListViewItem[it.Id];                        
                        li.Tag = it;

                        li.SubItems[0].Text = it.Name;
                        li.SubItems[1].Text = GXAmiScheduleEditorDlg.ScheduleRepeatToString(it.RepeatMode);
                        //Next run time
                        if (it.NextRunTine > DateTime.MinValue)
                        {
                            li.SubItems[2].Text = it.NextRunTine.ToString();
                        }
                        else
                        {
                            li.SubItems[2].Text = string.Empty;
                        }
                        //Last run time
                        if (it.LastRunTime > DateTime.MinValue)
                        {
                            li.SubItems[3].Text = it.LastRunTime.ToString();
                        }
                        else
                        {
                            li.SubItems[3].Text = string.Empty;
                        }
                        //Progress                
                        li.SubItems[3].Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnSchedulesUpdated(object sender, GXAmiSchedule[] schedules)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SchedulesUpdatedEventHandler(OnSchedulesUpdated), sender, schedules);
            }
            else
            {
                OnSchedulesUpdated(sender, schedules);
            }
        }

        void OnSchedulesRemoved(object sender, ulong[] schedules)
        {
            try
            {
                lock (ScheduleToListViewItem)
                {
                    foreach (ulong id in schedules)
                    {
                        ListViewItem li = ScheduleToListViewItem[id];
                        ScheduleToListViewItem.Remove(id);
                        li.Remove();
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnSchedulesRemoved(object sender, ulong[] schedules)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SchedulesRemovedEventHandler(OnSchedulesRemoved), sender, schedules);
            }
            else
            {
                OnSchedulesRemoved(sender, schedules);
            }
        }


        void OnSchedulesAdded(object sender, GXAmiSchedule[] schedules)
        {
            try
            {
                lock (ScheduleToListViewItem)
                {
                    foreach (GXAmiSchedule it in schedules)
                    {
                        AddScheduleItem(it);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnSchedulesAdded(object sender, GXAmiSchedule[] schedules)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SchedulesAddedEventHandler(OnSchedulesAdded), sender, schedules);
            }
            else
            {
                OnSchedulesAdded(sender, schedules);
            }
        }

        delegate void UpdateStatusEventHandler(int status, string text);

        void OnUpdateStatusEventHandler(int status, string text)
        {
            //If started
            if (status == 0)
            {
                this.ParentComponent.Leaf.Image = Gurux.DeviceSuite.Properties.Resources.leafanim;
            }
            else if (status == 1) //If ended
            {
                this.ParentComponent.Leaf.Image = Gurux.DeviceSuite.Properties.Resources.leaf;
            }
            else if (status == 2) //If error
            {
                this.ParentComponent.Leaf.Image = Gurux.DeviceSuite.Properties.Resources.leaferror;
            }
            else if (status == -1) //If additional info.
            {
                //Do not update image.
            }
            this.ParentComponent.StatusLbl.Text = text;
        }

        delegate void ShowOptionsEventHandler();

        void ShowOptions()
        {
            GXOptionsForm dlg = new GXOptionsForm(AppType.Ami, this);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(Gurux.DeviceSuite.Properties.Resources.RestartTxt, Gurux.DeviceSuite.Properties.Resources.AmiTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Update trace level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataCollector"></param>
        /// <param name="deviceId"></param>
        /// <param name="level"></param>
        void Client_OnTraceStateChanged(object sender, Guid dataCollector, ulong deviceId, System.Diagnostics.TraceLevel level)
        {
            ListViewItem node = DCToListViewItem[dataCollector] as ListViewItem;
            if (node != null)
            {
                (node.Tag as GXAmiDataCollector).TraceLevel = level;
            }
        }

        void Client_OnTraceClear(object sender, Guid dataCollector, ulong deviceId)
        {
            throw new NotImplementedException();
        }

        void AddTrace(GXAmiTrace e)
        {
            //Remove first item if maximum item count is reached.
            if (Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount > 0 && TraceEvents.Count == Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount)
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

        void Client_OnTraceAdded(object sender, GXAmiTrace[] e)
        {
            if (TracePause)
            {
                return;
            }
            if (InvokeRequired)
            {
                this.BeginInvoke(new TraceAddedEventHandler(Client_OnTraceAdded), sender, e);
                return;
            }
            lock (TraceEvents)
            {
                foreach (GXAmiTrace it in e)
                {
                    AddTrace(it);
                }
            }
        }

        /// <summary>
        /// Remove selected device profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="profiles"></param>
        void OnDeviceProfilesRemoved(object sender, GXAmiDeviceProfile[] profiles)
        {
            foreach(GXAmiDeviceProfile dt in profiles)
            {
                foreach (ListViewItem it in DeviceProfilesList.Items)
                {
                    if ((it.Tag as GXAmiDeviceProfile).Id == dt.Id)
                    {
                        it.Remove();
                        break;
                    }
                }
            }
        }

        void Client_OnDeviceProfilesRemoved(object sender, GXAmiDeviceProfile[] profiles)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DeviceProfilesRemovedEventHandler(OnDeviceProfilesRemoved), sender, profiles);
            }
            else
            {
                OnDeviceProfilesRemoved(sender, profiles);
            } 
        }

        /// <summary>
        /// Show device profiles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="profiles"></param>
        void OnDeviceProfilesAdded(object sender, GXAmiDeviceProfile[] profiles)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (GXAmiDeviceProfile it in profiles)
            {
                ListViewItem li = new ListViewItem(new string[]{it.Manufacturer, it.Model, 
                        it.Version, it.PresetName, it.Protocol, it.Profile, 
                        it.ProfileVersion.ToString()});
                li.Tag = it;
                list.Add(li);
            }
            DeviceProfilesList.Items.AddRange(list.ToArray());
        }

        void Client_OnDeviceProfilesAdded(object sender, GXAmiDeviceProfile[] profiles)
        {
            if (profiles != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new DeviceProfilesAddedEventHandler(OnDeviceProfilesAdded), sender, profiles);
                }
                else
                {
                    OnDeviceProfilesAdded(sender, profiles);
                }
            }
        }

        void UpdateDCImage(ListViewItem li, GXAmiDataCollector dc)
        {
            if (dc.State == DeviceStates.Connected)
            {
                li.ImageIndex = 5;
            }
            else
            {
                li.ImageIndex = 4;
            }
        }

        void OnDataCollectorStateChanged(object sender, GXAmiDataCollector[] collectors)
        {
            foreach (GXAmiDataCollector it in collectors)
            {
                ListViewItem node = DCToListViewItem[it.Guid] as ListViewItem;
                if (node != null)
                {
                    UpdateDCImage(node, it);
                }
            }
        }

        void Client_OnDataCollectorStateChanged(object sender, GXAmiDataCollector[] collectors)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DataCollectorsStateChangedEventHandler(OnDataCollectorStateChanged), sender, collectors);
            }
            else
            {
                OnDataCollectorStateChanged(sender, collectors);
            } 
        }


        void OnDeviceStateChanged(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            try
            {
                foreach (GuruxAMI.Common.GXAmiDevice it in devices)
                {
                    ListViewItem node = DeviceToListItem[it.Id] as ListViewItem;
                    if (node != null)
                    {
                        node.SubItems[1].Text = it.State.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnDeviceStateChanged(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DeviceStateChangedEventHandler(OnDeviceStateChanged), sender, devices);
            }
            else
            {
                OnDeviceStateChanged(sender, devices);
            }  
        }

        /// <summary>
        /// Load application settings.
        /// </summary>
        public void LoadSettings()
        {            
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCTreeWidth != -1)
            {
                AmiPanel.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.DCTreeWidth;
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCDevicesHeight != -1)
            {
                splitContainer2.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.DCDevicesHeight;
            }
            TraceFollowLastMenu.Checked = TraceFollowLast = Gurux.DeviceSuite.Properties.Settings.Default.TraceFollowLast;
            EventFollowLastMenu.Checked = EventFollowLast = Gurux.DeviceSuite.Properties.Settings.Default.EventFollowLast;

            if (Gurux.DeviceSuite.Properties.Settings.Default.DataCollectorListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DataCollectorListColumns)
                {
                    DCList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }

            if (Gurux.DeviceSuite.Properties.Settings.Default.UnAssignedListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.UnAssignedListColumns)
                {
                    UnassignedDCList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }

            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceProfilesListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DeviceProfilesListColumns)
                {
                    DeviceProfilesList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }

            if (Gurux.DeviceSuite.Properties.Settings.Default.TaskListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.TaskListColumns)
                {
                    TaskList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }

            if (Gurux.DeviceSuite.Properties.Settings.Default.DCDeviceListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DCDeviceListColumns)
                {
                    DevicesList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCEventColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DCEventColumns)
                {
                    this.EventsList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCPropertyColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DCPropertyColumns)
                {
                    PropertyList.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCScheduleColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DCScheduleColumns)
                {
                    Schedules.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.DCTraceViewColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DCTraceViewColumns)
                {
                    TraceView.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }            
        }

        void OnValueUpdated(object sender, GXAmiDataValue[] values)
        {
            foreach (GXAmiDataValue it in values)
            {
                ListViewItem item = PropertyToListItem[it.PropertyID] as ListViewItem;
                if (item != null)
                {
                    item.SubItems[2].Text = it.UIValue.ToString();
                    item.SubItems[3].Text = it.TimeStamp.ToString();
                }
            }
        }

        void Client_OnValueUpdated(object sender, GXAmiDataValue[] values)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ValueUpdatedEventHandler(OnValueUpdated), sender, values);
            }
            else
            {
                OnValueUpdated(sender, values);
            }        
        }

        /// <summary>
        /// Remove task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void OnTasksRemoved(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask task in tasks)
            {
                if (TaskToListItem.ContainsKey(task.Id))
                {
                    ListViewItem li = TaskToListItem[task.Id] as ListViewItem;
                    if (li != null)
                    {
                        TaskList.Items.Remove(li);
                        TaskToListItem.Remove(task.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Remove task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void Client_OnTasksRemoved(object sender, GXAmiTask[] tasks)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new TasksRemovedEventHandler(OnTasksRemoved), sender, tasks);
            }
            else
            {
                OnTasksRemoved(sender, tasks);
            }        
        }

        string GetStateAsString(TaskState state)
        {
            string str;
            switch (state)
            {
                case TaskState.Pending:
                   str = "Send";
                break;
                default:
                   str = state.ToString();
                break;
            }
            return str;
        }

        string GetTaskStatus(GXAmiTask task)
        {
            if (task.TargetType == TargetType.Media)
            {
                if (task.TaskType == TaskType.MediaState)
                {
                    if (task.State == TaskState.Pending)
                    {
                        string[] tmp = task.Data.Split(Environment.NewLine.ToCharArray());
                        MediaState state = (MediaState)int.Parse(tmp[tmp.Length - 1]);
                        return task.TaskType.ToString() + " Changing: " + state.ToString();
                    }
                    if (task.State == TaskState.Succeeded)
                    {
                        return task.TaskType.ToString() + "Changed.";
                    }
                    return task.TaskType.ToString();
                }
                if (task.TaskType == TaskType.MediaWrite)
                {
                    if (task.State == TaskState.Pending)
                    {
                        string[] tmp = task.Data.Split(Environment.NewLine.ToCharArray());
                        return "-> " + GetStateAsString(task.State) + " " + tmp[tmp.Length - 1];
                    }
                    return task.TaskType.ToString() + " " + task.State.ToString() + " " + task.Data;
                }
                return task.TaskType.ToString() + " " + GetStateAsString(task.State);
            }
            return task.TargetType.ToString() + " " + task.TaskType.ToString() + " " + task.State.ToString();
        }

        /// <summary>
        /// Task is claimed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void OnTasksClaimed(object sender, GXAmiTask[] tasks)
        {
            try
            {
                foreach (GXAmiTask task in tasks)
                {
                    if (!TaskToListItem.ContainsKey(task.Id))
                    {
                        OnTasksAdded(sender, new GXAmiTask[] { task});
                    }
                    ListViewItem li = TaskToListItem[task.Id] as ListViewItem;
                    if (li != null)
                    {
#if DEBUG
                        li.SubItems[0].Text = task.Id.ToString() + " " + GetTaskStatus(task);
#else
                        li.SubItems[0].Text = GetTaskStatus(task);
#endif
                        li.SubItems[3].Text = task.ClaimTime.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Task is claimed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void Client_OnTasksClaimed(object sender, GXAmiTask[] tasks)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new TasksClaimedEventHandler(OnTasksClaimed), sender, tasks);
            }
            else
            {
                OnTasksClaimed(sender, tasks);
            }
        }

        /// <summary>
        /// New task is added for the DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask task in tasks)
            {
                System.Diagnostics.Debug.Assert(task.Id != 0);               
                string claimTime = "";
                if (task.ClaimTime != null)
                {
                    claimTime = task.ClaimTime.Value.ToString();
                }
                //Show task ID when debugging.
#if DEBUG
                ListViewItem li = new ListViewItem(new string[] { task.Id.ToString() + " " + GetTaskStatus(task), task.SenderAsString, task.TargetAsString, task.CreationTime.ToString(), claimTime });
#else
                ListViewItem li = new ListViewItem(new string[] { GetTaskStatus(task), task.SenderAsString, task.TargetAsString, task.CreationTime.ToString(), claimTime });
#endif
                li.Tag = task;
                TaskList.Items.Add(li);
                TaskToListItem.Add(task.Id, li);
                //LogTasks.Add(new GXAmiTaskLog(task));
                TaskLogList.VirtualListSize = LogTasks.Count;
            }
        }

        /// <summary>
        /// New task is added for the DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        void Client_OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new TasksAddedEventHandler(OnTasksAdded), sender, tasks);
            }
            else
            {
                OnTasksAdded(sender, tasks);
            }                        
        }

        void Client_OnSystemErrorsRemoved(object sender, GXAmiSystemError[] errors)
        {
            throw new NotImplementedException();
        }

        void OnSystemErrorsAdded(object sender, GXAmiSystemError[] errors)
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
            foreach (GXAmiSystemError err in errors)
            {
                //Remove first item if maximum item count is reached.
                if (Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount > 0 && 
                    EventsList.Items.Count == Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount)
                {
                    EventsList.Items[0].Remove();
                }
                ListViewItem it = new ListViewItem(new string[] { err.TimeStamp.ToString(), "", err.Message });
                EventsList.Items.Add(it);
                if (EventFollowLast)
                {
                    EventsList.EnsureVisible(it.Index);
                }
            }
        }

        void Client_OnSystemErrorsAdded(object sender, GXAmiSystemError[] errors)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SystemErrorsAddedEventHandler(OnSystemErrorsAdded), sender, errors);
            }
            else
            {
                OnSystemErrorsAdded(sender, errors);
            }                     
        }

        void OnDevicesUpdated(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            try
            {
                foreach (GuruxAMI.Common.GXAmiDevice it in devices)
                {
                    ListViewItem node = DeviceToListItem[it.Id] as ListViewItem;
                    if (node != null)
                    {
                        node.Text = it.Name;
                    }
                }                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnDevicesUpdated(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DevicesUpdatedEventHandler(OnDevicesUpdated), sender, devices);
            }
            else
            {
                OnDevicesUpdated(sender, devices);
            }  
        }

        void OnDevicesRemoved(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            try
            {
                foreach (GuruxAMI.Common.GXAmiDevice it in devices)
                {
                    DevicesList.Items.Remove(DeviceToListItem[it.Id] as ListViewItem);
                    DeviceToListItem.Remove(it);                    
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }
        void Client_OnDevicesRemoved(object sender, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DevicesRemovedEventHandler(OnDevicesRemoved), sender, devices);
            }
            else
            {
                OnDevicesRemoved(sender, devices);
            }  
        }

        void OnDevicesAdded(object sender, object target, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            try
            {
                List<ListViewItem> list = new List<ListViewItem>();
                foreach (GuruxAMI.Common.GXAmiDevice it in devices)
                {
                    string info;
                    if (string.IsNullOrEmpty(it.Manufacturer))
                    {
                        info = it.Protocol + " " + it.Profile;
                    }
                    else
                    {
                        info = it.Manufacturer + " " + it.Model + " " + it.Version + " " + it.PresetName;
                    }
                    ListViewItem node = new ListViewItem(new string[] { it.Name, it.State.ToString(), info});
                    DeviceToListItem.Add(it.Id, node);
                    list.Add(node);
                    node.Tag = it;
                }
                DevicesList.Items.AddRange(list.ToArray());                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        void Client_OnDevicesAdded(object sender, object target, GuruxAMI.Common.GXAmiDevice[] devices)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DevicesAddedEventHandler(OnDevicesAdded), sender, target, devices);
            }
            else
            {
                OnDevicesAdded(sender, target, devices);
            }
        }


        void Client_OnDeviceGroupsUpdated(object sender, GXAmiDeviceGroup[] deviceGrops)
        {
            throw new NotImplementedException();
        }

        void Client_OnDeviceGroupsRemoved(object sender, GXAmiDeviceGroup[] deviceGrops)
        {
            throw new NotImplementedException();
        }

        void Client_OnDeviceGroupsAdded(object sender, GXAmiDeviceGroup[] deviceGrops, GXAmiDeviceGroup group)
        {
            throw new NotImplementedException();
        }

        void Client_OnDeviceErrorsRemoved(object sender, GXAmiDeviceError[] errors)
        {
            throw new NotImplementedException();
        }

        void OnDeviceErrorsAdded(object sender, GXAmiDeviceError[] errors)
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

            //string errorInfo = ex.Message.Replace("\n", " ");
            foreach (GXAmiDeviceError err in errors)
            {
                //Remove first item if maximum item count is reached.
                if (Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount > 0 &&
                    EventsList.Items.Count == Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount)
                {
                    EventsList.Items[0].Remove();
                }
                string name = "";
                if (err.TargetDeviceID != null)
                {
                    GuruxAMI.Common.GXAmiDevice device = Client.GetDevice(err.TargetDeviceID.Value);
                    name = device.Name;
                }
                ListViewItem it = new ListViewItem(new string[]{err.TimeStamp.ToString(), name, err.Message});
                EventsList.Items.Add(it);
                if (EventFollowLast)
                {
                    EventsList.EnsureVisible(it.Index);
                }
            }
        }

        void Client_OnDeviceErrorsAdded(object sender, GXAmiDeviceError[] errors)
        {
            // Update UI if window has created.
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DeviceErrorsAddedEventHandler(OnDeviceErrorsAdded), sender, errors);
            }
            else
            {
                OnDeviceErrorsAdded(sender, errors);
            }
        }

        /// <summary>
        /// Update DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void OnDataCollectorsUpdated(object sender, GXAmiDataCollector[] collectors)
        {
            try
            {
                foreach (GXAmiDataCollector it in collectors)
                {
                    if (it.UnAssigned)
                    {
                        ListViewItem node = UnassignedDCToListViewItem[it.Guid] as ListViewItem;
                        if (node != null)
                        {
                            node.Text = it.MAC;
                        }
                    }
                    else
                    {
                        ListViewItem node = DCToListViewItem[it.Guid] as ListViewItem;
                        if (node != null)
                        {
                            //Update data.
                            bool Internal = (node.Tag as GXAmiDataCollector).Internal;
                            node.Tag = it;
                            it.Internal = Internal;
                            if (Internal)
                            {                                
                                if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors == null)
                                {
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors = new StringCollection();
                                }
                                if (!Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Contains(it.Guid.ToString()))
                                {
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Add(it.Guid.ToString());
                                    StartDataCollector(it.Guid);
                                }                                
                            }
                            else if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors != null)
                            {
                                int pos = Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.IndexOf(it.Guid.ToString());
                                if (pos != -1)
                                {
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.RemoveAt(pos);
                                    foreach (GXAmiDataCollectorServer it2 in InternalDCs)
                                    {
                                        if (it2.Guid == it.Guid)
                                        {
                                            it2.Close();
                                            InternalDCs.Remove(it2);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (it.Internal)
                            {
                                node.Text = it.Name + " [Internal]";
                            }
                            else
                            {
                                node.Text = it.Name;
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

         /// <summary>
        /// Update DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void Client_OnDataCollectorsUpdated(object sender, GXAmiDataCollector[] collectors)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DataCollectorsUpdatedEventHandler(OnDataCollectorsUpdated), sender, collectors);
            }
            else
            {
                OnDataCollectorsUpdated(sender, collectors);
            }            
        }
                       
        /// <summary>
        /// Remove DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void OnDataCollectorsRemoved(object sender, GXAmiDataCollector[] collectors)
        {
            try
            {
                foreach (GXAmiDataCollector it in collectors)
                {
                    if (it.UnAssigned)
                    {
                        UnassignedDCList.Items.Remove(UnassignedDCToListViewItem[it.Guid] as ListViewItem);
                        UnassignedDCToListViewItem.Remove(it.Guid);
                    }
                    else
                    {
                        DCList.Items.Remove(DCToListViewItem[it.Guid] as ListViewItem);
                        DCToListViewItem.Remove(it);
                        if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors != null)
                        {
                            foreach (string str in Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors)
                            {
                                if (it.Guid.ToString().Equals(str))
                                {
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Remove(str);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Remove DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void Client_OnDataCollectorsRemoved(object sender, GXAmiDataCollector[] collectors)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new DataCollectorsRemovedEventHandler(OnDataCollectorsRemoved), sender, collectors);
            }
            else
            {
                OnDataCollectorsRemoved(sender, collectors);
            }          
        }

        /// <summary>
        /// Add new DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void OnDataCollectorsAdded(object sender, GXAmiDataCollector[] collectors)
        {
            try
            {
                List<ListViewItem> assigned = new List<ListViewItem>();
                List<ListViewItem> unAssigned = new List<ListViewItem>();
                foreach (GXAmiDataCollector it in collectors)
                {
                    if (it.UnAssigned)
                    {
                        ListViewItem node = new ListViewItem(new string[]{it.MAC, it.IP});
                        UnassignedDCToListViewItem.Add(it.Guid, node);
                        unAssigned.Add(node);
                        node.Tag = it;
                    }
                    else
                    {
                        ListViewItem node = new ListViewItem(it.Name);
                        UpdateDCImage(node, it);
                        DCToListViewItem.Add(it.Guid, node);
                        assigned.Add(node);
                        node.Tag = it;
                    }
                }
                if (assigned.Count != 0)
                {
                    DCList.Items.AddRange(assigned.ToArray());
                }
                if (unAssigned.Count != 0)
                {
                    UnassignedDCList.Items.AddRange(unAssigned.ToArray());
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Add new DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collectors"></param>
        void Client_OnDataCollectorsAdded(object sender, GXAmiDataCollector[] collectors)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new DataCollectorsAddedEventHandler(OnDataCollectorsAdded), sender, collectors);
            }
            else
            {
                OnDataCollectorsAdded(sender, collectors);
            }
        }

        /// <summary>
        /// Save application settings.
        /// </summary>
        public void SaveSettings()
        {            
            List<string> columns = new List<string>();
            Gurux.DeviceSuite.Properties.Settings.Default.TraceFollowLast = TraceFollowLast;
            Gurux.DeviceSuite.Properties.Settings.Default.EventFollowLast = EventFollowLast;
            
            ////////////////////////////////////////////////////////
            //Save widths of data collector list columns.
            foreach (ColumnHeader it in DCList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DataCollectorListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DataCollectorListColumns.AddRange(columns.ToArray());
            columns.Clear();

            ////////////////////////////////////////////////////////
            //Save widths of unassigned data collector list columns.
            foreach (ColumnHeader it in UnassignedDCList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.UnAssignedListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.UnAssignedListColumns.AddRange(columns.ToArray());
            columns.Clear();

            ////////////////////////////////////////////////////////
            //Save widths of device template list columns.
            foreach (ColumnHeader it in DeviceProfilesList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceProfilesListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceProfilesListColumns.AddRange(columns.ToArray());
            columns.Clear();

            ////////////////////////////////////////////////////////
            //Save widths task list columns.
            foreach (ColumnHeader it in TaskList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.TaskListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.TaskListColumns.AddRange(columns.ToArray());
            columns.Clear();

            ////////////////////////////////////////////////////////
            //Save widths of device list columns.
            foreach (ColumnHeader it in DevicesList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DCDeviceListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DCDeviceListColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of event list columns.
            foreach (ColumnHeader it in EventsList.Columns)
            {
                columns.Add(it.Width.ToString());
            }            
            Gurux.DeviceSuite.Properties.Settings.Default.DCEventColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DCEventColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of property list columns.
            foreach (ColumnHeader it in PropertyList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DCPropertyColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DCPropertyColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of schedule list columns.
            foreach (ColumnHeader it in Schedules.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DCScheduleColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DCScheduleColumns.AddRange(columns.ToArray());

            Gurux.DeviceSuite.Properties.Settings.Default.DCDevicesHeight = splitContainer2.SplitterDistance;
            Gurux.DeviceSuite.Properties.Settings.Default.DCTreeWidth = AmiPanel.SplitterDistance;
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of trace view list columns.
            foreach (ColumnHeader it in TraceView.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DCTraceViewColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DCTraceViewColumns.AddRange(columns.ToArray());             
        }

        /// <summary>
        /// Load default images to the image list.
        /// </summary>
        public void LoadDefaultImages()
        {
            DevicesList.SmallImageList = DCList.SmallImageList = new ImageList();            
            //Load the image for the device list.
            System.Drawing.Bitmap bm = Gurux.DeviceSuite.Properties.Resources.DeviceList;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the error image for the device list.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceListError;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for the device group.
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceGroup;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the error image for the device group.
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceGroupError;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the disconnected image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceDisconnect;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the connect image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceConnected;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the monitoring image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceMonitor;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the error image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceError;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the dirty image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceDirty;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the reading image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceRead;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the writing image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceWrite;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);//Load the schedule image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemOn;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the executing schedule image for the device.			
            bm = Gurux.DeviceSuite.Properties.Resources.ScheduleItemExecute;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for device categories.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategories;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for the device category.			            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategory;
            bm.MakeTransparent(System.Drawing.Color.FromArgb(255, 0, 255));
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for device tables.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTables;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for the device table.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTable;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for device properties.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperties;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the image for the device property.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperty;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the error image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyError;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the changed image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyChanged2;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);
            //Load the notifications image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceNotifies;
            bm.MakeTransparent();
            DCList.SmallImageList.Images.Add(bm);

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
        /// Remove the specified item.
        /// </summary>
        /// <param name="target">An item to be removed.</param>
        public void DeleteItem(object target)
        {            
            //If device is selected.
            if (target is GuruxAMI.Common.GXAmiDevice)
            {
                Client.RemoveDevice(target as GuruxAMI.Common.GXAmiDevice, true);
            }
            else if (target is GXAmiDataCollector)
            {
                Client.RemoveDataCollectors(new GXAmiDataCollector[]{target as GXAmiDataCollector}, true);
            }            
            else
            {
                throw new Exception("Delete failed. Unknown target.");
            }
        }

        private void AddScheduleItem(GXAmiSchedule item)
        {
            ListViewItem it = new ListViewItem(item.Name);
            it.SubItems.Add(GXAmiScheduleEditorDlg.ScheduleRepeatToString(item.RepeatMode));
            //Next run time
            if (item.NextRunTine > DateTime.MinValue)
            {
                it.SubItems.Add(item.NextRunTine.ToString());
            }
            else
            {
                it.SubItems.Add(string.Empty);
            }
            //Last run time
            if (item.LastRunTime > DateTime.MinValue)
            {
                it.SubItems.Add(item.LastRunTime.ToString());
            }
            else
            {
                it.SubItems.Add(string.Empty);
            }
            //Progress                
            it.SubItems.Add(string.Empty);
            it.Tag = item;
            UpdateScheduleImage(it, item.Status);
            Schedules.Items.Add(it);
            ScheduleToListViewItem[item.Id] = it;            
        }              

        /// <summary>
        /// Add new device to the device collector.
        /// </summary>
        public void NewDevice()
        {
            try
            {
                if (Client.GetDeviceProfiles(false, false).Length == 0)
                {
                    throw new Exception("You must add device profile to the data collector before you can create new device.");
                }
                GXAmiDataCollector[] list = Client.GetDataCollectors();
                if (list.Length == 0)
                {
                    throw new Exception("You must create one data collector first.");
                }
                GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, null, list);
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Add new data collector.
        /// </summary>
        public void NewDataCollector()
        {
            try
            {
                GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, null, DataCollectorActionType.Add);
                dlg.ShowDialog(ParentComponent);                 
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Import device template to AMI.
        /// </summary>
        public void ImportDeviceProfiles()
        {
            try
            {
                GXAmiImportForm dlg = new GXAmiImportForm(Client, ParentComponent.Editor.Manufacturers);
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Disable unused items.
        /// </summary>
        private void ScheduleMenu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (Schedules.SelectedItems.Count == 0)
                {
                    SchedulePropertiesMenu.Enabled = ScheduleRunMenu.Enabled = ScheduleStopAllMenu.Enabled = ScheduleStopMenu.Enabled = ScheduleStartAllMenu.Enabled = ScheduleStartMenu.Enabled = ScheduleDeleteMenu.Enabled = false;
                }
                else
                {
                    SchedulePropertiesMenu.Enabled = true;
                    bool anyStarted = false; //Is any item started.
                    bool anySelectedStarted = false; //Is any selected item started.
                    bool allSelectedStarted = true; //Are all selected items started.
                    bool allStarted = true; //Are all items started.					
                    foreach (ListViewItem it in Schedules.SelectedItems)
                    {
                        GXAmiSchedule item = (GXAmiSchedule)it.Tag;
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
                        GXAmiSchedule item = (GXAmiSchedule)it.Tag;
                        if (!anyStarted && (item.Status & ScheduleState.Run) != 0)
                        {
                            anyStarted = true;
                        }
                        if (allStarted && (item.Status & ScheduleState.Run) == 0)
                        {
                            allStarted = false;
                        }
                    }
                    SchedulePropertiesMenu.Enabled = Schedules.SelectedItems.Count == 1;
                    ScheduleDeleteMenu.Enabled = !anySelectedStarted;
                    ScheduleStopAllMenu.Enabled = anyStarted;
                    ScheduleStopMenu.Enabled = anySelectedStarted;
                    ScheduleStartAllMenu.Enabled = !allStarted;
                    ScheduleStartMenu.Enabled = !allSelectedStarted;
                    ScheduleRunMenu.Enabled = !anyStarted;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
                GXAmiSchedule schedule = new GXAmiSchedule();
                schedule.ScheduleStartTime = DateTime.Now;
                GXAmiScheduleEditorDlg dlg = new GXAmiScheduleEditorDlg(Client, schedule);
                dlg.ShowDialog(ParentComponent);                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        public void DeleteDeviceProfiles()
        {
            try
            {
                List<GXAmiDeviceProfile> items = new List<GXAmiDeviceProfile>();
                foreach (ListViewItem it in DeviceProfilesList.SelectedItems)
                {
                    items.Add(it.Tag as GXAmiDeviceProfile);
                }
                Client.RemoveDeviceProfile(items.ToArray(), true);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
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
                    List<GXAmiSchedule> items = new List<GXAmiSchedule>();
                    foreach (ListViewItem it in Schedules.SelectedItems)
                    {
                        GXAmiSchedule item = it.Tag as GXAmiSchedule;
                        //Don't remove started items.
                        if ((item.Status & ScheduleState.Run) == 0)
                        {
                            items.Add(item);
                        }
                    }
                    Client.RemoveSchedules(items.ToArray(), true);
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
                List<GXAmiSchedule> schedules = new List<GXAmiSchedule>();
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    schedules.Add(it.Tag as GXAmiSchedule);
                }
                Client.StartSchedules(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Start all schedule items.
        /// </summary>
        private void ScheduleStartAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiSchedule> schedules = new List<GXAmiSchedule>();
                foreach (ListViewItem it in Schedules.Items)
                {
                    schedules.Add(it.Tag as GXAmiSchedule);
                }
                Client.StartSchedules(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Stop selected schedule items.
        /// </summary>
        private void ScheduleStopMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiSchedule> schedules = new List<GXAmiSchedule>();
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    schedules.Add(it.Tag as GXAmiSchedule);
                }
                Client.StopSchedules(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Stop all schedule items.
        /// </summary>
        private void ScheduleStopAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiSchedule> schedules = new List<GXAmiSchedule>();
                foreach (ListViewItem it in Schedules.Items)
                {
                    schedules.Add(it.Tag as GXAmiSchedule);
                }
                Client.StopSchedules(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Run selected schedule items.
        /// </summary>
        private void ScheduleRunMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiSchedule> schedules = new List<GXAmiSchedule>();                
                foreach (ListViewItem it in Schedules.SelectedItems)
                {
                    schedules.Add(it.Tag as GXAmiSchedule);
                }
                Client.RunSchedules(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void PropertyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PropertyList.SelectedItems.Count == 1)
            {
                ListViewItem item = DCToListViewItem[PropertyList.SelectedItems[0].Tag] as ListViewItem;
                if (item != null)
                {
                    item.Selected = true;
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
            
        }

        private void DataCollectorMenu_Opening(object sender, CancelEventArgs e)
        {
            DeleteMenu.Enabled = PropertiesMenu.Enabled = DCList.SelectedIndices.Count != 0;
        }

        public void ShowProperties()
        {
            try
            {
                if (DCList.Focused)
                {
                    if (DCList.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one Data Collector.");
                    }
                    GXAmiDataCollector dc = DCList.SelectedItems[0].Tag as GXAmiDataCollector;
                    GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, dc, DataCollectorActionType.Edit);
                    dlg.ShowDialog(ParentComponent);
                }
                else if (DevicesList.Focused)
                {
                    if (DevicesList.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one device.");
                    }
                    GuruxAMI.Common.GXAmiDevice device = DevicesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDevice;
                    GuruxAMI.Common.GXAmiDataCollector[] dcs = Client.GetDataCollectors(device);
                    GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, device, dcs);
                    dlg.ShowDialog(ParentComponent);
                }
                else if (DeviceProfilesList.Focused)
                {
                    if (DeviceProfilesList.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one device template.");
                    }
                    GuruxAMI.Common.GXAmiDevice device = DeviceProfilesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDeviceProfile;
                    GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, device, null);
                    dlg.ShowDialog(ParentComponent);
                }   
                else if (Schedules.Focused)
                {
                    if (Schedules.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one schedule.");
                    }
                    GXAmiSchedule schedule = Schedules.SelectedItems[0].Tag as GXAmiSchedule;                    
                    GXAmiScheduleEditorDlg dlg = new GXAmiScheduleEditorDlg(Client, schedule);
                    dlg.ShowDialog(ParentComponent);
                }  
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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

        /// <summary>
        /// Delete selected items.
        /// </summary>
        public void Delete()
        {
            try
            {
                if (DCList.Focused)
                {                 
                    if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDataCollectorTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    foreach(ListViewItem it in DCList.SelectedItems)
                    {
                        DeleteItem(it.Tag);
                        return;
                    }
                }
                if (UnassignedDCList.Focused)
                {                 
                    if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDataCollectorTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    foreach (ListViewItem it in UnassignedDCList.SelectedItems)
                    {
                        DeleteItem(it.Tag);
                        return;
                    }
                }                
                if (DevicesList.Focused)
                {                    
                    if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    foreach (ListViewItem it in DevicesList.SelectedItems)
                    {
                        DeleteItem(it.Tag);
                    }
                    return;
                }
                else if (Schedules.Focused)
                {
                    if (GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    DeleteScheduleItems();
                    return;
                }
                else if (DeviceProfilesList.Focused)
                {
                    if (GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceProfilesTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    DeleteDeviceProfiles();
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Called when user selects new item.
        /// Menu items are enabled/disabled debending what is selected.
        /// </summary>
        private void OnEnter(object sender, EventArgs e)
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
                    Client.ClearTraces(TraceEvents.ToArray());
                    TraceEvents.Clear();
                    TraceView.VirtualListSize = 0;
                }                 
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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

        /// <summary>
        /// Get trace item to draw.
        /// </summary>
        private void TraceView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            lock (TraceEvents)
            {
                if (e.ItemIndex < TraceEvents.Count)
                {
                    GXAmiTrace ei = TraceEvents[e.ItemIndex];
                    ListViewItem item = new ListViewItem(ei.Timestamp.ToString("hh:mm:ss.fff"));
                    string data = "";
                    if (ei.Data is byte[])
                    {
                        data = BitConverter.ToString(ei.Data as byte[]);
                    }
                    else if (ei.Data != null)
                    {
                        data = ei.Data.ToString();
                    }
                    item.SubItems.AddRange(new string[] { ei.Type.ToString(), data});
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void OnReadingChanged(object sender, EventArgs e)
        {

        }

        public void StartMonitoring()
        {
            try
            {                
                if (DCList.Focused)
                {
                    foreach (ListViewItem it in DCList.SelectedItems)
                    {
                        Client.StartMonitoring(it.Tag as GuruxAMI.Common.GXAmiDataCollector);
                    }
                }
                else if (DevicesList.Focused)
                {
                    foreach (ListViewItem it in DevicesList.SelectedItems)
                    {
                        Client.StartMonitoring(it.Tag as GuruxAMI.Common.GXAmiDevice);
                    }
                }
                else
                {
                    throw new Exception("Unknown target.");
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        public void StopMonitoring()
        {
            try
            {
                if (DCList.Focused)
                {
                    foreach (ListViewItem it in DCList.SelectedItems)
                    {
                        Client.StopMonitoring(it.Tag as GuruxAMI.Common.GXAmiDataCollector);
                    }
                }
                else if (DevicesList.Focused)
                {
                    foreach (ListViewItem it in DevicesList.SelectedItems)
                    {
                        Client.StopMonitoring(it.Tag as GuruxAMI.Common.GXAmiDevice);
                    }
                }               
                else
                {
                    throw new Exception("Unknown target.");
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
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
                ParentComponent.Progress.Visible = ParentComponent.CancelOperationMenu.Enabled = state == AsyncState.Start;
                if (state == AsyncState.Start)
                {                    
                    ParentComponent.StatusLbl.Text = text;
                    GXAmiDevice[] devices = parameters[1] as GXAmiDevice[];
                    ParentComponent.Progress.Value = 0;
                    ParentComponent.Progress.Maximum = devices.Length;                    
                }
                else
                {
                    ParentComponent.StatusLbl.Text = Gurux.DeviceSuite.Properties.Resources.ReadyTxt;                
                }
            }
        }

        delegate void UpdateProgressEventHandler(int index);
        
        void OnUpdateProgress(int index)
        {
            ParentComponent.Progress.Value = index;
        }

        void ReadAsync(object sender, GXAsyncWork work, object[] parameters)
        {
            GXAmiClient client = (parameters[0] as GXAmiClient);
            GXAmiDevice[] devices = parameters[1] as GXAmiDevice[];
            int pos = 0;
            foreach (GXAmiDevice it in devices)
            {
                if (work.IsCanceled)
                {
                    break;
                }
                BeginInvoke(new UpdateProgressEventHandler(this.OnUpdateProgress), ++pos);
                client.Read(it);
                Application.DoEvents();
            }            
        }

        /// <summary>
        /// Read selected DC or device.
        /// </summary>
        public void Read()
        {
            try
            {
                if (DCList.Focused)
                {
                    foreach (ListViewItem it in DCList.SelectedItems)
                    {
                        Client.Read(it.Tag as GXAmiDataCollector);
                    }
                }
                else if (DevicesList.Focused)
                {
                    if (TransactionWork != null && TransactionWork.IsRunning)
                    {
                        throw new Exception("Read Failed. Transcaction already in progress.");
                    }
                    List<GXAmiDevice> devices = new List<GXAmiDevice>();
                    foreach (ListViewItem it in DevicesList.SelectedItems)
                    {
                        devices.Add(it.Tag as GXAmiDevice);                        
                    }
                    TransactionWork = new GXAsyncWork(this, OnAsyncStateChange, ReadAsync, null, Gurux.DeviceSuite.Properties.Resources.ReadingTxt, new object[] { Client, devices.ToArray() });
                    TransactionWork.Start();
                }                
                else
                {
                    throw new Exception("Unknown target.");
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void ReadMenu_Click(object sender, EventArgs e)
        {
            Read();
        }

        /// <summary>
        /// Is tasks listening paused.
        /// </summary>
        private void TasksPauseMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TasksPauseMenu.Checked = !TasksPauseMenu.Checked;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Is last task item followed.
        /// </summary>
        private void TasksFollowLastMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TasksFollowLast = TasksFollowLastMenu.Checked = !TasksFollowLast;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Clear tasks view.
        /// </summary>
        private void TasksClearMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiTask> tasks = new List<GXAmiTask>();
                foreach(ListViewItem it in TaskToListItem.Values)
                {
                    tasks.Add(it.Tag as GXAmiTask);
                }
                Client.RemoveTask(tasks.ToArray(), true);                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }
       
        /// <summary>
        /// Remove selected tasks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TasksRemoveMenu_Click(object sender, EventArgs e)
        {
            try
            {
                List<GXAmiTask> tasks = new List<GXAmiTask>();
                foreach (ListViewItem it in TaskList.SelectedItems)
                {
                    tasks.Add(it.Tag as GXAmiTask);
                }
                Client.RemoveTask(tasks.ToArray(), true);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Find devices of selected DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (OnItemActivated != null)
                {
                    OnItemActivated(sender, e);
                }
                AddCommandPromptMenu.Enabled = DCList.SelectedItems.Count == 1;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void DevicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyList.Items.Clear();
                PropertyToListItem.Clear();
                Tables.Clear();
                TableCB.Items.Clear();
                TableCB.Enabled = DevicesList.SelectedItems.Count == 1;                
                if (DevicesList.SelectedItems.Count == 1)
                {
                    GuruxAMI.Common.GXAmiDevice device = DevicesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDevice;
                    //If content is not get yet.
                    if (device.Categories == null)
                    {
                        device = Client.GetDevice(device.Id);
                        DevicesList.SelectedItems[0].Tag = device;
                    }
                    foreach (GXAmiCategory cat in device.Categories)
                    {
                        foreach (GXAmiProperty p in cat.Properties)
                        {
                            ListViewItem li = new ListViewItem(new string[]{p.Name, p.Unit, "", ""});
                            PropertyList.Items.Add(li);
                            PropertyToListItem.Add(p.Id, li);
                        }
                    }                   
                    foreach (GXAmiDataTable table in device.Tables)
                    {
                        Tables.Add(table.Id, table);
                        TableCB.Items.Add(table);
                    }
                    if (device.Tables.Length != 0)
                    {
                        if (!tabControl3.TabPages.Contains(TableTab))
                        {
                            tabControl3.TabPages.Add(TableTab);
                        }                        
                        TableCB.SelectedIndex = 0;
                    }
                    else
                    {                        
                        if (tabControl3.TabPages.Contains(TableTab))
                        {
                            tabControl3.TabPages.Remove(TableTab);
                        }                        
                    }
                    GXAmiDataValue[] values = Client.GetLatestValues(device);
                    if (values.Length != 0)
                    {
                        Client_OnValueUpdated(sender, values);
                    }
                }                                 
                if (OnItemActivated != null)
                {
                    OnItemActivated(sender, e);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }            
        }

        /// <summary>
        /// Add new device template to the selectd DCs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDeviceProfilesMenu_Click(object sender, EventArgs e)
        {
            ImportDeviceProfiles();
        }

        /// <summary>
        /// Assign DC so users can see it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignMenu_Click(object sender, EventArgs e)
        {
            try
            {
                GXAmiUserGroup[] ugs = Client.GetUserGroups(false);
                foreach (ListViewItem it in UnassignedDCList.SelectedItems)
                {
                    GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, it.Tag as GXAmiDataCollector, DataCollectorActionType.Edit);
                    if (dlg.ShowDialog(this.ParentComponent) != DialogResult.OK)
                    {
                        break;
                    }
                    Client.AddDataCollector(it.Tag as GXAmiDataCollector, ugs);
                }               
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }  
        }

        /// <summary>
        /// Remove DC from unassigned DC list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveUnAssignedMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDataCollectorTxt) != DialogResult.Yes)
                {
                    return;
                }
                List<GXAmiDataCollector> collectors = new List<GXAmiDataCollector>();
                foreach (ListViewItem it in UnassignedDCList.SelectedItems)
                {
                    collectors.Add(it.Tag as GXAmiDataCollector);
                }
                Client.RemoveDataCollectors(collectors.ToArray(), true);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Update menus when selected item changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnItemActivated != null)
            {
                OnItemActivated(sender, e);
            }
        }

        /// <summary>
        /// Clear device and property lists when unassigned DC is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnassignedDCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyList.Items.Clear();
            PropertyToListItem.Clear();
            DevicesList.Items.Clear();
            DeviceToListItem.Clear();
        }

        private void UnAssignedMenu_Opening(object sender, CancelEventArgs e)
        {
            RemoveUnAssignedMenu.Enabled = AssignMenu.Enabled = UnassignedDCList.SelectedItems.Count != 0;
        }

        /// <summary>
        /// Enable or disable device menu items if item is selected.
        /// </summary>
        private void DeviceMenu_Opening(object sender, CancelEventArgs e)
        {
            DeviceDeleteMenu.Enabled = DeviceReadMenu.Enabled = DeviceStartMonitorMenu.Enabled = 
                    DeviceStopMonitorMenu.Enabled = DevicePropertiesMenu.Enabled = 
                    DevicesList.SelectedItems.Count != 0;
        }

        /// <summary>
        /// Enable or disable device template menu items if item is selected.
        /// </summary>
        private void DeviceProfilesMenu_Opening(object sender, CancelEventArgs e)
        {
            DeviceProfilesDelete.Enabled = DeviceProfilesList.SelectedIndices.Count != 0;
        }
        
        /// <summary>
        /// Get values from the DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {                
                TableData.Columns.Clear();
                GXAmiDataTable table = TableCB.SelectedItem as GXAmiDataTable;
                object[][] rows;
                if (ShowAllCB.Checked)
                {
                    rows = Client.GetTableRows(table, 0, 0);
                }
                else
                {
                    rows = Client.GetTableRows(table, Convert.ToInt32(IndexTB.Text), Convert.ToInt32(CountTB.Text));
                }
                TableRowCount.Text = rows.Length.ToString();
                DataTable dt = new DataTable();
                int pos = 1;
                foreach (GXAmiProperty it in table.Columns)
                {
                    dt.Columns.Add(pos.ToString() + " " + it.Name);
                    ++pos;
                }
                foreach(object[] row in rows)
                {
                    dt.Rows.Add(row);
                }
                TableData.DataSource = dt;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Read table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GXAmiDataTable table = TableCB.SelectedItem as GXAmiDataTable;
                if (ReadAllRB.Checked)//Read all.
                {
                    Client.ReadTable(table, 0, 0);
                }
                else if (ReadLastRB.Checked) //Read n. last days.
                {
                    int cnt = int.Parse(ReadLastTB.Text);
                    Client.ReadTable(table, DateTime.Now.AddDays(-cnt).Date, DateTime.MaxValue);
                }
                else if (ReadFromRB.Checked)//Read between.
                {
                    Client.ReadTable(table, StartPick.Value, ToPick.Value);
                }
                else if (ReadNewValuesCB.Checked) //Read new values.
                {
                    Client.Read(table);
                }                                               
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CountTB.Enabled = IndexTB.Enabled = ShowByIndexlCB.Checked;
        }

        /// <summary>
        /// Disable read and update buttons if no table is selected.
        /// </summary>
        private void TableCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAllCB.Enabled = ShowByIndexlCB.Enabled = IndexTB.Enabled = CountTB.Enabled = 
                ReadAllRB.Enabled = ReadLastRB.Enabled = ReadLastTB.Enabled = 
                ReadFromRB.Enabled = ReadNewValuesCB.Enabled = UpdateBtn.Enabled = 
                ReadBtn.Enabled = TableCB.SelectedIndex != -1;            
        }

        private void ReadAllRB_CheckedChanged(object sender, EventArgs e)
        {
            ReadLastTB.Enabled = ReadLastRB.Checked;
            StartPick.Enabled = ToPick.Enabled = ReadFromRB.Checked;
        }

        private void AddCommandPromptMenu_Click(object sender, EventArgs e)
        {
            try
            {
                GXAmiDataCollector dc = DCList.SelectedItems[0].Tag as GXAmiDataCollector;
                GXAmiCommandPromptForm dlg = new GXAmiCommandPromptForm(Client, dc);
                if (dlg.ShowDialog(this.ParentComponent) == DialogResult.OK)
                {
                    TabPage page = new TabPage("Command Prompt");
                    GXAMICommandPromptTab tmp = new GXAMICommandPromptTab(ParentComponent, Client, dc, dlg.SelectedMedia.MediaType, dlg.SelectedMedia.Name, dlg.SelectedMedia.Settings);
                    while (tmp.Controls.Count != 0)
                    {
                        page.Controls.Add(tmp.Controls[0]);
                    }
                    page.ImageIndex = 1;
                    page.Tag = tmp;
                    TabControl1.TabPages.Add(page);
                    TabControl1.SelectedTab = page;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void TabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            int w = imageList1.Images[0].Width;
            int h = imageList1.Images[0].Height;
            for (int i = 0; i < TabControl1.TabCount; i++)
            {
                TabPage it = TabControl1.TabPages[i];
                if (it.ImageIndex == 1)
                {
                    Rectangle r = TabControl1.GetTabRect(i);
                    r.Offset(3, 3);
                    r.Width = w;
                    r.Height = h;
                    if (r.Contains(e.Location))
                    {
                        (it.Tag as GXAMICommandPromptTab).CloseConnection();
                        TabControl1.TabPages.Remove(it);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Search DC.
        /// </summary>
        void SearchDC()
        {
            try
            {
                DCSearchBtn.Enabled = false;
                object[] tmp = Client.Search(new string[] { DCSearchBtn.Text }, ActionTargets.DataCollector, SearchType.All);
                //Remove exists DCs.
                DCToListViewItem.Clear();
                DCList.Items.Clear();               
                GXAmiDataCollector[] dcs = new GXAmiDataCollector[tmp.Length];
                Array.Copy(tmp, dcs, tmp.Length);
                OnDataCollectorsAdded(null, dcs);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
            DCSearchBtn.Enabled = true;
            DCSearchBtn.Focus();
        }

        /// <summary>
        /// Search device.
        /// </summary>
        void SearchDevice()
        {
            try
            {
                DeviceSearchBtn.Enabled = false;
                object[] tmp = Client.Search(new string[] { DeviceSearchBtn.Text }, ActionTargets.Device, SearchType.All);
                //Remove exists devices.
                PropertyList.Items.Clear();
                PropertyToListItem.Clear();
                DevicesList.Items.Clear();
                DeviceToListItem.Clear();       
                GXAmiDevice[] dcs = new GXAmiDevice[tmp.Length];
                Array.Copy(tmp, dcs, tmp.Length);
                OnDevicesAdded(null, null, dcs);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
            DeviceSearchBtn.Enabled = true;
            DeviceSearchBtn.Focus();
        }

        /// <summary>
        /// Search DC when enter is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DCSearchBtn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {                
                SearchDC();                
            }
        }

        /// <summary>
        /// Search devices if enter is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceSearchBtn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {                
                SearchDevice();                
            }
        }

        /// <summary>
        /// Add new DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDataCollectorMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GXAmiUserGroup[] ugs = Client.GetUserGroups(false);
                GXAmiDataCollector dc = new GXAmiDataCollector();
                GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, dc, DataCollectorActionType.Add);
                if (dlg.ShowDialog(this.ParentComponent) == System.Windows.Forms.DialogResult.OK)
                {
                    AddDataCollector(new GXAmiDataCollector[]{dc});
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }  
        }       

        /// <summary>
        /// Show Tasklog item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskLogList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                lock (LogTasks)
                {
                    if (e.ItemIndex < LogTasks.Count)
                    {
                        GXAmiTaskLog task = LogTasks[e.ItemIndex];
                        string finishTime = "", claimTime = "";
                        if (task.ClaimTime != null)
                        {
                            claimTime = task.ClaimTime.Value.ToString();
                        }
                        if (task.FinishTime != null)
                        {
                            finishTime = task.FinishTime.Value.ToString();
                        }
                        //Show task ID when debugging.
#if DEBUG
                        ListViewItem item = new ListViewItem(new string[] { task.Id.ToString() + " " + GetTaskStatus(task), task.SenderAsString, task.TargetAsString, task.CreationTime.ToString(), claimTime, finishTime });
#else
                        ListViewItem item = new ListViewItem(new string[] { GetTaskStatus(task), task.SenderAsString, task.TargetAsString, task.CreationTime.ToString(), claimTime });
#endif
                        e.Item = item;                       
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Show devices that DC can access.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDCDevicesMenu_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyList.Items.Clear();
                PropertyToListItem.Clear();
                DevicesList.Items.Clear();
                DeviceToListItem.Clear();                                
                List<GuruxAMI.Common.GXAmiDevice> devices = new List<GuruxAMI.Common.GXAmiDevice>();
                //If no DC is selected show all devices.
                if (DCList.SelectedItems.Count == 0)
                {
                    devices.AddRange(Client.GetDevices(false, DeviceContentType.Main));
                    if (devices.Count != 0)
                    {
                        Client_OnDevicesAdded(Client, null, devices.ToArray());
                        devices.Clear();
                    }
                }
                else
                {
                    foreach (ListViewItem it in DCList.SelectedItems)
                    {
                        devices.AddRange(Client.GetDevices(it.Tag as GXAmiDataCollector, false));
                        if (devices.Count != 0)
                        {
                            Client_OnDevicesAdded(Client, it.Tag as GXAmiDataCollector, devices.ToArray());
                            devices.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }
    }
}
