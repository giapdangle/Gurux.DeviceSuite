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

    delegate void ShowDeviceTemplatesEventHandler(GXAmiDeviceTemplate[] templates);

    public partial class GXAmi : Form
    {
        //Swap object and tree node. This makes item search faster.
        System.Collections.Hashtable DCToListViewItem = new System.Collections.Hashtable();
        System.Collections.Hashtable UnassignedDCToListViewItem = new System.Collections.Hashtable();
        System.Collections.Hashtable DeviceToListItem = new System.Collections.Hashtable();
        System.Collections.Hashtable PropertyToListItem = new System.Collections.Hashtable();
        System.Collections.Hashtable ScheduleToListViewItem = new System.Collections.Hashtable();
        Dictionary<ulong, ListViewItem> TaskToListItem = new Dictionary<ulong, ListViewItem>();
        Dictionary<ulong, GXAmiDataTable> Tables = new Dictionary<ulong, GXAmiDataTable>();
#if DEBUG
        //This is used in debug purposes to insure that IDs are unigue.
        System.Collections.Hashtable m_ObjectIDs = new System.Collections.Hashtable();
#endif //DEBUG
        System.Collections.Hashtable m_IntervalCntSwapNode = new System.Collections.Hashtable();
        public List<GXAmiDataCollectorServer> DataCollectors = new List<GXAmiDataCollectorServer>();
        GXAmiClient Client;
        Thread ServerThread;
        public AutoResetEvent Started = new AutoResetEvent(false);
        public AutoResetEvent ClosingApplication = new AutoResetEvent(false);
        public AutoResetEvent ServerThreadClosed = new AutoResetEvent(false);        
        bool hex;
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
            //Remove schedules for now.
            TabControl1.TabPages.Remove(SchedulesTab);
            CountTB.Enabled = IndexTB.Enabled = false;
            TableCB_SelectedIndexChanged(null, null);
            ReadAllRB_CheckedChanged(null, null);
            //Remove Trace tab. It is added later when user selects trace level.
            TabControl1.TabPages.Remove(TraceTab);

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
            string host = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
            if (host == "*")
            {
                host = "localhost";
            }
            string baseUr;
            //If we are using web service.
            if (host.StartsWith("http://"))
            {
                baseUr = host;
            }
            else
            {
                baseUr = "http://" + host + ":" +
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiPort + "/";
            }            
            GXAmiDataCollectorServer collector = new GXAmiDataCollectorServer(baseUr, guid);
            DataCollectors.Add(collector);
            collector.OnTasksAdded += new TasksAddedEventHandler(DC_OnTasksAdded);
            collector.OnTasksClaimed += new TasksClaimedEventHandler(DC_OnTasksClaimed);
            collector.OnTasksRemoved += new TasksRemovedEventHandler(DC_OnTasksRemoved);
            collector.OnError += new Gurux.Common.ErrorEventHandler(DC_OnError);
            collector.OnAvailableSerialPorts += new AvailableSerialPortsEventHandler(DC_OnAvailableSerialPorts);
            collector.Init(null);
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
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Add(dc.Guid.ToString());
                dc.Description = dc.Name = "Default data collector ";                
                dc.Medias = Gurux.Communication.GXClient.GetAvailableMedias(true);
                dc.SerialPorts = Gurux.Serial.GXSerial.GetPortNames();
                GXAmiDataCollectorServer cl = StartDataCollector(dc.Guid);
                cl.Update(dc);
                
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
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} removed.", it.Id));
            }
        }

        /// <summary>
        /// Task is claimed to DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        static void DC_OnTasksClaimed(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} claimed.", it.Id));
            }
        }

        /// <summary>
        /// New task is added to DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tasks"></param>
        static void DC_OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Task {0} Added.", it.Id));
            }
        }

        delegate void DataCollectorsClearEventHandler(object sender);

        void OnDataCollectorsClear(object sender)
        {
            //Clear items...
            DCList.Items.Clear();
            DCToListViewItem.Clear();                
        }

        public void Start(bool waitStart)
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
                ServerThread = new Thread(new ThreadStart(StartServer));
                ServerThread.IsBackground = true;
                ServerThread.Start();
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

        void StartServer()
        {
            try
            {
                GXDBService Server = null;
                string baseUr = "http://" + Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName +
                    ":" + Gurux.DeviceSuite.Properties.Settings.Default.AmiPort + "/";
                if (string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName))
                {
                    Server = new GXDBService(baseUr, null, "");                    
                }
                else
                {
                    bool useServer = !Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName.StartsWith("http://");
                    if (useServer)
                    {
                        string connStr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName,
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword);
                        Server = new GXDBService(baseUr, new OrmLiteConnectionFactory(connStr, false, 
                            ServiceStack.OrmLite.MySql.MySqlDialectProvider.Instance), 
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix);
                        Server.Update();
                    }
                    else
                    {
                        baseUr = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
                    }

                    if (Client == null)
                    {
                        if (Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName == "*")
                        {
                            baseUr = "http://localhost:" + Gurux.DeviceSuite.Properties.Settings.Default.AmiPort + "/";
                        }                        
                        Client = new GXAmiClient(baseUr,
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName,
                            Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword);
                    }
                    if (Client.IsDatabaseCreated())
                    {
                        Client.OnDeviceTemplatesAdded += new DeviceTemplatesAddedEventHandler(Client_OnDeviceTemplatesAdded);
                        Client.OnDeviceTemplatesRemoved += new DeviceTemplatesRemovedEventHandler(Client_OnDeviceTemplatesRemoved);
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
                        Client.StartListenEvents();
                        GXAmiDataCollector[] collectors = Client.GetDataCollectors();
                        Client_OnDataCollectorsAdded(Client, collectors);
                        //Get all tasks and show them.
                        //Client_OnTasksAdded(Client, Client.GetTasks(TaskState.All));
                        //Get all device templates and show them
                        GXAmiDeviceTemplate[] templates = Client.GetDeviceTemplates(false);
                        Client_OnDeviceTemplatesAdded(Client, templates);
                        this.BeginInvoke(new ServerStateChanged(OnServerStateChanged), true);                        

                        //Start Data Collectors.
                        if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors != null)
                        {
                            List<string> tmp = new List<string>(Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Cast<string>());                            
                            foreach (string str in tmp)
                            {
                                bool found = false;
                                Guid guid = new Guid(str);
                                foreach (GXAmiDataCollector it in collectors)
                                {
                                    if (it.Guid == guid)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (found)
                                {
                                    StartDataCollector(guid);
                                }
                                else
                                {
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Remove(str);
                                }
                            }
                            if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Count == 0)
                            {
                                Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors = null;
                            }
                        }
                        if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors == null)
                        {
                            string mac = GXAmiClient.GetMACAddressAsString();
                            if (mac != null)
                            {
                                mac = mac.Replace(":", "");
                            }
                            foreach (GXAmiDataCollector it in collectors)
                            {
                                if (it.MAC == mac)
                                {
                                    if (Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors == null)
                                    {
                                        Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors = new System.Collections.Specialized.StringCollection();
                                    }
                                    Gurux.DeviceSuite.Properties.Settings.Default.AmiDataCollectors.Add(it.Guid.ToString());
                                    StartDataCollector(it.Guid);
                                }
                            }
                        }
                    }
                }
                Started.Set();
                ClosingApplication.WaitOne();
                if (Client != null)
                {
                    Client.Dispose();
                    Client = null;
                }
                foreach (GXAmiDataCollectorServer it in DataCollectors)
                {
                    it.Close();
                }
                if (Server != null)
                {
                    Server.Stop();
                    Server = null;
                }                
            }
            catch (Exception ex)
            {
                Started.Set();
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);                
            }
            ServerThreadClosed.Set();
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
        /// Remove selected device templates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="templates"></param>
        void OnDeviceTemplatesRemoved(object sender, GXAmiDeviceTemplate[] templates)
        {
            foreach(GXAmiDeviceTemplate dt in templates)
            {
                foreach (ListViewItem it in DeviceTemplateList.Items)
                {
                    if ((it.Tag as GXAmiDeviceTemplate).Id == dt.Id)
                    {
                        it.Remove();
                        break;
                    }
                }
            }
        }

        void Client_OnDeviceTemplatesRemoved(object sender, GXAmiDeviceTemplate[] templates)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DeviceTemplatesRemovedEventHandler(OnDeviceTemplatesRemoved), sender, templates);
            }
            else
            {
                OnDeviceTemplatesRemoved(sender, templates);
            } 
        }

        /// <summary>
        /// Show device templates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="templates"></param>
        void OnDeviceTemplatesAdded(object sender, GXAmiDeviceTemplate[] templates)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (GXAmiDeviceTemplate it in templates)
            {
                ListViewItem li = new ListViewItem(new string[]{it.Manufacturer, it.Model, 
                        it.Version, it.PresetName, it.Protocol, it.Template, 
                        it.TemplateVersion.ToString()});
                li.Tag = it;
                list.Add(li);
            }
            DeviceTemplateList.Items.AddRange(list.ToArray());
        }

        void Client_OnDeviceTemplatesAdded(object sender, GXAmiDeviceTemplate[] templates)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DeviceTemplatesAddedEventHandler(OnDeviceTemplatesAdded), sender, templates);
            }
            else
            {
                OnDeviceTemplatesAdded(sender, templates);
            } 
        }

        void OnDataCollectorStateChanged(object sender, GXAmiDataCollector[] collectors)
        {
            //TODO: Update DC image.
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

            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTemplateListColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.DeviceTemplateListColumns)
                {
                    DeviceTemplateList.Columns[pos].Width = int.Parse(it);
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

        string GetTaskStatus(GXAmiTask task)
        {
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
                        li.SubItems[0].Text = GetTaskStatus(task);
                        li.SubItems[3].Text = task.ClaimTime.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

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
                string target = "";//task.tar
                string claimTime = "";
                if (task.ClaimTime != null)
                {
                    claimTime = task.ClaimTime.Value.ToString();
                }
                ListViewItem li = new ListViewItem(new string[] { GetTaskStatus(task), target, task.CreationTime.ToString(), claimTime });
                li.Tag = task;
                TaskList.Items.Add(li);
                TaskToListItem.Add(task.Id, li);
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
                        info = it.Protocol + " " + it.Template;
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
                if (err.TargetDeviceID != 0)
                {
                    GuruxAMI.Common.GXAmiDevice device = Client.GetDevice(err.TargetDeviceID);
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
                            node.Text = it.Name;
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
            foreach (ColumnHeader it in DeviceTemplateList.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceTemplateListColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceTemplateListColumns.AddRange(columns.ToArray());
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
            ScheduleToListViewItem[item] = it;
        }              

        /// <summary>
        /// Add new device to the device collector.
        /// </summary>
        public void NewDevice()
        {
            try
            {
                if (Client.GetDeviceTemplates(false).Length == 0)
                {
                    throw new Exception("You must add device template to the data collector before you can create new device.");
                }
                List<GXAmiDataCollector> list = new List<GXAmiDataCollector>();
                if (DCList.SelectedItems.Count == 0)
                {
                    throw new Exception("Select atleast one Data Collector where you want to add device.");
                }
                foreach (ListViewItem it in DCList.SelectedItems)
                {
                    list.Add(DCList.SelectedItems[0].Tag as GXAmiDataCollector);
                }
                GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, null, list.ToArray());
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
                GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, null);
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
        public void ImportDeviceTemplate()
        {
            try
            {
                GXAmiImportForm dlg = new GXAmiImportForm(Client, ParentComponent.Editor.Manufacturers, null);
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }
      
        private void UpdateScheduleItem(object sender, GXItemEventArgs e)
        {
            try
            {
                GXSchedule item = e.Item as GXSchedule;
                ListViewItem it = (ListViewItem)ScheduleToListViewItem[item];
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
                if (DCList.SelectedItems.Count != 1)
                {
                    throw new Exception("Select data collector where schedule is added.");
                }

                GXAmiScheduleEditorDlg dlg = new GXAmiScheduleEditorDlg(Client, DCList.SelectedItems[0].Tag as GXAmiDataCollector, new GXAmiSchedule());
                dlg.ShowDialog(ParentComponent);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        public void DeleteDeviceTemplates()
        {
            try
            {
                List<GXAmiDeviceTemplate> items = new List<GXAmiDeviceTemplate>();
                foreach (ListViewItem it in DeviceTemplateList.SelectedItems)
                {
                    items.Add(it.Tag as GXAmiDeviceTemplate);
                }
                Client.RemoveDeviceTemplates(items.ToArray(), true);
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
                Client.StopSchedule(schedules.ToArray());
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
                Client.StopSchedule(schedules.ToArray());
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
                Client.RunSchedule(schedules.ToArray());
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        /// <summary>
        /// Show schedule options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SchedulePropertiesMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Schedules.SelectedItems.Count == 1)
                {
                    //GXScheduleEditorDlg dlg = new GXScheduleEditorDlg(Schedules.SelectedItems[0].Tag as GXSchedule, m_DeviceList);
                    //dlg.ShowDialog(ParentComponent);
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
                    GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, dc);
                    dlg.ShowDialog(ParentComponent);
                }
                else if (DevicesList.Focused)
                {
                    if (DevicesList.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one device.");
                    }
                    GuruxAMI.Common.GXAmiDevice device = DevicesList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDevice;
                    GuruxAMI.Common.GXAmiDataCollector dc = DCList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDataCollector;
                    GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, device, new GuruxAMI.Common.GXAmiDataCollector[]{dc});
                    dlg.ShowDialog(ParentComponent);
                }
                else if (DeviceTemplateList.Focused)
                {
                    if (DeviceTemplateList.SelectedItems.Count != 1)
                    {
                        throw new Exception("Select only one device template.");
                    }
                    GuruxAMI.Common.GXAmiDevice device = DeviceTemplateList.SelectedItems[0].Tag as GuruxAMI.Common.GXAmiDeviceTemplate;
                    GXAmiDeviceSettingsForm dlg = new GXAmiDeviceSettingsForm(Client, device, null);
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
                        return;
                    }
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
                else if (DeviceTemplateList.Focused)
                {
                    if (GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveDeviceTemplatesTxt) != DialogResult.Yes)
                    {
                        return;
                    }
                    DeleteDeviceTemplates();
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

        public void Read()
        {
            try
            {
                if (DCList.Focused)
                {
                    foreach (ListViewItem it in DCList.SelectedItems)
                    {
                        Client.Read(it.Tag as GuruxAMI.Common.GXAmiDataCollector);
                    }
                }
                else if (DevicesList.Focused)
                {
                    foreach (ListViewItem it in DevicesList.SelectedItems)
                    {
                        Client.Read(it.Tag as GuruxAMI.Common.GXAmiDevice);
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
                PropertyList.Items.Clear();
                PropertyToListItem.Clear();
                DevicesList.Items.Clear();
                DeviceToListItem.Clear();
                List<GuruxAMI.Common.GXAmiDevice> devices = new List<GuruxAMI.Common.GXAmiDevice>();
                foreach (ListViewItem it in DCList.SelectedItems)
                {
                    devices.AddRange(Client.GetDevices(it.Tag as GXAmiDataCollector, true, false));
                    if (devices.Count != 0)
                    {
                        Client_OnDevicesAdded(Client, it.Tag as GXAmiDataCollector, devices.ToArray());                        
                        devices.Clear();
                    }
                }
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
        private void NewDeviceTemplateMenu_Click(object sender, EventArgs e)
        {
            ImportDeviceTemplate();
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
                    GXAmiDataCollectorForm dlg = new GXAmiDataCollectorForm(Client, it.Tag as GXAmiDataCollector);
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
        private void DeviceTemplateMenu_Opening(object sender, CancelEventArgs e)
        {
            DeviceTemplateDelete.Enabled = DeviceTemplateList.SelectedIndices.Count != 0;
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
                    GXAMICommandPromptTab tmp = new GXAMICommandPromptTab(ParentComponent, Client, dc, dlg.SelectedMedia.MediaType, dlg.SelectedMedia.Settings);
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
    }
}
