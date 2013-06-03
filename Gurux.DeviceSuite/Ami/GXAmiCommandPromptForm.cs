﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuruxAMI.Common;
using Gurux.Communication;
using Gurux.Common;
using Gurux.Serial;
using GuruxAMI.Client;
using System.Threading;
using Gurux.DeviceSuite.Director;

namespace Gurux.DeviceSuite.Ami
{
    public partial class GXAmiCommandPromptForm : Form
    {
        GXAsyncWork TransactionWork;
        AutoResetEvent Connected = new AutoResetEvent(false);
        GXAmiTask ExecutedTask;
        Form PropertiesForm;
        public IGXMedia SelectedMedia;
        GXAmiClient Client;
        GXAmiDataCollector DataCollector;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="collector"></param>
        public GXAmiCommandPromptForm(GXAmiClient client, GXAmiDataCollector collector)
        {
            Client = client;
            DataCollector = collector;
            InitializeComponent();
            ConnectingPanel.Dock = DockStyle.Fill;
            string selected = null;
            if (!string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.CommandPrompSettings))
            {
                List<string> arr = new List<string>(Gurux.DeviceSuite.Properties.Settings.Default.CommandPrompSettings.Split(new char[]{';'}));
                if (arr.Count > 1)
                {
                    selected = arr[0];
                }
            }

            foreach (string media in collector.Medias)
            {
                int pos = MediaCB.Items.Add(media);
                if (selected != null && string.Compare(selected, media) == 0)
                {
                    MediaCB.SelectedIndex = pos;
                }
            }
            if (MediaCB.SelectedIndex == -1)
            {
                MediaCB.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Show settings of selected media.
        /// </summary>
        private void MediaCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MediaFrame.Controls.Clear();
                GXClient cl = new GXClient();
                SelectedMedia = cl.SelectMedia(MediaCB.Text);
                if (SelectedMedia == null)
                {
                    throw new Exception(MediaCB.Text + " media not found.");
                }
                if (!string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.CommandPrompSettings))
                {
                    List<string> arr = new List<string>(Gurux.DeviceSuite.Properties.Settings.Default.CommandPrompSettings.Split(new char[]{';'}));
                    if (arr.Count > 1)
                    {
                        arr.RemoveAt(0);
                        SelectedMedia.Settings = string.Join(";", arr.ToArray());
                    }
                }

                if (SelectedMedia is GXSerial)
                {
                    (SelectedMedia as GXSerial).AvailablePorts = DataCollector.SerialPorts;
                }
                PropertiesForm = SelectedMedia.PropertiesForm;
                ((IGXPropertyPage)PropertiesForm).Initialize();
                while (PropertiesForm.Controls.Count != 0)
                {
                    Control ctr = PropertiesForm.Controls[0];
                    if (ctr is Panel)
                    {
                        if (!ctr.Enabled)
                        {
                            PropertiesForm.Controls.RemoveAt(0);
                            continue;
                        }
                    }
                    MediaFrame.Controls.Add(ctr);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        void OnAsyncStateChange(System.Windows.Forms.Control sender, AsyncState state)
        {
            panel1.Visible = panel2.Visible = MediaFrame.Visible = state != AsyncState.Start;
            ConnectingPanel.Visible = state == AsyncState.Start;
            if (state == AsyncState.Start)
            {
                Client.OnTasksAdded += new TasksAddedEventHandler(Client_OnTasksAdded);
                Client.OnDeviceErrorsAdded += new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded);
            }
            else
            {
                Client.OnDeviceErrorsAdded -= new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded);
                Client.OnTasksAdded -= new TasksAddedEventHandler(Client_OnTasksAdded);
                if (ExecutedTask != null && state == AsyncState.Cancel)
                {
                    Client.RemoveTask(ExecutedTask, true);
                    ExecutedTask = null;
                    this.DialogResult = DialogResult.None;
                }
                else if (ExecutedTask != null && state == AsyncState.Finish)
                {
                    ExecutedTask = null;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        void Client_OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask it in tasks)
            {
                if (it.TaskType == TaskType.MediaState)
                {
                    string[] arr = it.Data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (SelectedMedia.MediaType == arr[0] &&
                        SelectedMedia.Settings.Replace(Environment.NewLine, "") == arr[1] &&
                        ((MediaState)Convert.ToInt32(arr[2])) == MediaState.Open)
                    {
                        Connected.Set();
                        Client.RemoveTask(it, true);
                    }
                }
            }
        }

        void ConnectAsync(object sender, object[] parameters)
        {
            ExecutedTask = Client.MediaOpen((Guid)parameters[0], (string)parameters[1], (string)parameters[2]);
            Connected.WaitOne();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Connected.Reset();
                //Apply media dialog settings.
                if (PropertiesForm != null)
                {
                    ((IGXPropertyPage)PropertiesForm).Apply();
                }
                SelectedMedia.Validate();
                TransactionWork = new GXAsyncWork(this, OnAsyncStateChange, ConnectAsync, new object[] { DataCollector.Guid, SelectedMedia.MediaType, SelectedMedia.Settings });
                TransactionWork.Start();
            }            
            catch (Exception ex)
            {                
                GXCommon.ShowError(this, ex);
            }
        }

        delegate void ShowErrorEventHandler(string message);

        void ShowError(string message)
        {
            OnAsyncStateChange(this, AsyncState.Cancel);
            MessageBox.Show(this, message);
        }

        /// <summary>
        /// If task is failed. If task failed dialog is not closed.
        /// </summary>
        void Client_OnDeviceErrorsAdded(object sender, GXAmiDeviceError[] errors)
        {
            foreach(GXAmiDeviceError it in errors)
            {
                if (it.TaskID == ExecutedTask.Id)
                {
                    DialogResult = DialogResult.None;
                    this.BeginInvoke(new ShowErrorEventHandler(ShowError), it.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// Save settings as a default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Apply media dialog settings.
                ((IGXPropertyPage)PropertiesForm).Apply();
                SelectedMedia.Validate();
                Gurux.DeviceSuite.Properties.Settings.Default.CommandPrompSettings = SelectedMedia.MediaType + ";" + SelectedMedia.Settings;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        private void ConnectCancelBtn_Click(object sender, EventArgs e)
        {
            TransactionWork.Cancel();
        }
    }
}
