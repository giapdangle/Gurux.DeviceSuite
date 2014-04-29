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
using Gurux.Device.Editor;
using Gurux.Communication;
using System.Globalization;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.Device.PresetDevices;
using System.Collections;
using Gurux.Device;
using GuruxAMI.Client;
using GuruxAMI.Common;
using Gurux.Serial;
using System.IO;

namespace Gurux.DeviceSuite.Ami
{    
    public partial class GXAmiDeviceSettingsForm : Form
    {
        GXAmiDataCollector[] DataCollectors;
        List<string> AvailableMedias;
        GXAmiClient Client;
        System.Collections.Hashtable PresetItemToListViewItem = new System.Collections.Hashtable();
        private DisabledAction m_DisActions;
        Form PropertiesForm;
        Form SettingsForm;        
        internal Gurux.Common.IGXMedia SelectedMedia;
        Gurux.Device.GXDevice UIDevice;
        public GuruxAMI.Common.GXAmiDevice Device;
        List<GXAmiDeviceProfile> Templates = new List<GXAmiDeviceProfile>();
        GXParameters DeviceParameters = new GXParameters();
        List<GXAmiDeviceMedia> MediaConnections = new List<GXAmiDeviceMedia>();

        /// <summary>
        /// Get available medias from selected DC. If DC is not selected show all medias from all DCs.
        /// </summary>
        ulong GetAllAvailableMediasFromDCs()
        {
            AvailableMedias.Clear();
            ulong id = 0;
            //If device is edited.
            if (Device != null && Device.Medias[0].DataCollectorId != null)
            {
                id = Device.Medias[0].DataCollectorId.Value;
            }
            foreach (GXAmiDataCollector it in DataCollectors)
            {
                if (id == 0 || id == it.Id)
                {
                    foreach (string media in it.Medias)
                    {
                        if (!AvailableMedias.Contains(media))
                        {
                            AvailableMedias.Add(media);
                        }
                    }
                }
            }
            return id;
        }

        public GXAmiDeviceSettingsForm(GXAmiClient client, GuruxAMI.Common.GXAmiDevice device, GXAmiDataCollector[] dcs)
        {
            InitializeComponent();            
            DataCollectors = dcs;
            AvailableMedias = new List<string>();                    
            Client = client;
            Device = device;
            ulong id = GetAllAvailableMediasFromDCs();
            if (Device != null)
            {
                MediaConnections.AddRange(Device.Medias);
            }
            this.CollectorsCB.Items.Add("");
            foreach (GXAmiDataCollector it in DataCollectors)
            {
                int pos2 = this.CollectorsCB.Items.Add(it);                
                if (id == it.Id)             
                {
                    this.CollectorsCB.SelectedIndex = pos2;
                }
            }
            if (this.CollectorsCB.SelectedIndex == -1)
            {
                this.CollectorsCB.SelectedIndex = 0;
            }
            SettingsPanel.Dock = PropertyGrid.Dock = PresetList.Dock = CustomDeviceProfile.Dock = DockStyle.Fill;
            //GuruxAMI.Common.Device type can not be changed after creation. This is for secure reasons.
            PresetCB.Enabled = CustomRB.Enabled = PresetList.Enabled = CustomDeviceProfile.Enabled = Device == null;                  
            CustomDeviceProfile.Visible = false;            
            if (Device != null)
            {
                //Add redundant conections.
                for (int pos = 1; pos < Device.Medias.Length; ++pos)
                {
                    AddConnection(Device.Medias[pos]);
                }            
                NameTB.Text = Device.Name;
                RefreshRateTp.Value = new DateTime(((long)Device.UpdateInterval) * 10000000 + RefreshRateTp.MinDate.Ticks);
                UpdateResendCnt(Device.ResendCount);
                UpdateWaitTime(Device.WaitTime);
                //Create UI Device so all assemblys are loaded.
                string path = Path.Combine(Gurux.Common.GXCommon.ApplicationDataPath, "Gurux");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Gurux.Common.GXFileSystemSecurity.UpdateDirectorySecurity(path);
                }
                path = Path.Combine(path, "Gurux.DeviceSuite");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Gurux.Common.GXFileSystemSecurity.UpdateDirectorySecurity(path);
                }
                path = Path.Combine(path, "DeviceProfiles");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Gurux.Common.GXFileSystemSecurity.UpdateDirectorySecurity(path);
                }
                path = Path.Combine(path, Device.ProfileGuid.ToString());
                //Load Device template if not loaded yet.                                 
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Gurux.Common.GXFileSystemSecurity.UpdateDirectorySecurity(path);
                    byte[] data = Client.GetDeviceProfilesData(Device.ProfileGuid);
                    GXZip.Import(this, data, path + "\\");
                }
                path = Path.Combine(path, Device.ProfileGuid.ToString() + ".gxp");
                UIDevice = GXDevice.Load(path);                
            }
            else
            {
                RefreshRateTp.Value = new DateTime(((long)1) * 10000000 + RefreshRateTp.MinDate.Ticks);                
            }
            //Add disabled actions.
            m_DisActions = new DisabledAction(Device == null ? Gurux.Device.DisabledActions.None : (Gurux.Device.DisabledActions)Device.DisabledActions);
            tabControl1.TabPages.Add(m_DisActions.DisabledActionsTB);
            this.Text = Gurux.DeviceSuite.Properties.Resources.DeviceSettingsTxt;
            this.GeneralTab.Text = Gurux.DeviceSuite.Properties.Resources.GeneralTxt;
            //Update helps from the resources.
            this.helpProvider1.SetHelpString(this.NameTB, Gurux.DeviceSuite.Properties.Resources.DeviceNameHelp);
            this.helpProvider1.SetHelpString(this.MediaCB, Gurux.DeviceSuite.Properties.Resources.MediaListHelp);
            this.helpProvider1.SetHelpString(this.RefreshRateTp, Gurux.DeviceSuite.Properties.Resources.RefreshRateHelp);
            this.helpProvider1.SetHelpString(this.OkBtn, Gurux.DeviceSuite.Properties.Resources.OKHelp);
            this.helpProvider1.SetHelpString(this.CancelBtn, Gurux.DeviceSuite.Properties.Resources.CancelHelp);
        }

        private void PresetCB_CheckedChanged(object sender, EventArgs e)
        {
            bool preset = PresetCB.Checked;
            PresetList.Visible = preset;
            CustomDeviceProfile.Visible = !preset;
        }

        private void UpdateResendCnt(int resendCnt)
        {
            bool enable = resendCnt != -3;
            ResendCountTb.ReadOnly = !enable;
            ResendCountLbl.Enabled = enable;
            ResendCountTb.Text = enable ? resendCnt.ToString() : "Protocol";
        }

        private void UpdateWaitTime(int wt)
        {
            bool enable = wt != -3;
            WaitTimeLbl.Enabled = WaitTimeTb.Enabled = enable;
            if (enable)
            {
                WaitTimeTb.Value = DateTime.Now.Date.AddMilliseconds(wt);
            }
        }
       
        private void GXAmiDeviceSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                ShowCustomDeviceTypes();
                ShowPresetDevices();
                if (Device == null)
                {
                    if (PresetList.Items.Count != 0)
                    {
                        PresetList.Items[0].Selected = true;
                    }
                    else if (CustomDeviceProfile.Items.Count != 0)
                    {
                        CustomDeviceProfile.SelectedIndex = 0;
                    }
                    else // If no device templates are installed.
                    {
                        OkBtn.Enabled = false;
                    }
                }                
                NameTB.Select();
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        void ShowPresetDevices()
        {
            GXAmiDeviceProfile[] templates = Client.GetDeviceProfiles(true, null, false, false);
            if (templates.Length == 0)
            {
                CustomRB.Checked = true;
                PresetCB.Enabled = false;
                return;
            }
            Templates.AddRange(templates);
            GXAmiDeviceProfile DeviceProfiles = null;            
            if (Device != null)
            {
                DeviceProfiles = FindDeviceProfiles(Device.ProfileId, templates);                
            }
            Dictionary<GXAmiDeviceProfile, ListViewItem> items = new Dictionary<GXAmiDeviceProfile, ListViewItem>();
            foreach (GXAmiDeviceProfile it in templates)
            {
                ListViewItem item = new ListViewItem(new string[]{it.Manufacturer, it.Model, 
                                it.Version, it.PresetName, it.ProfileVersion.ToString()});
                item.Tag = it;
                items.Add(it, item);               
            }
            PresetList.Items.AddRange(items.Values.ToArray());
            //Select found device.
            if (DeviceProfiles != null)
            {               
                this.PresetList.SelectedIndexChanged -= new System.EventHandler(this.PresetList_SelectedIndexChanged);
                items[DeviceProfiles].Selected = true;
                this.PresetList.SelectedIndexChanged += new System.EventHandler(this.PresetList_SelectedIndexChanged);          
                UpdateMedias(true);
            }             
        }

        GXAmiDeviceProfile FindDeviceProfiles(ulong id, GXAmiDeviceProfile[] templates)
        {
            if (id == 0)
            {
                return null;
            }
            foreach (GXAmiDeviceProfile it in templates)
            {
                if (it.Id == Device.ProfileId)
                {
                    return it;
                }
            }
            return null;
        }

        void ShowCustomDeviceTypes()
        {
            GXAmiDeviceProfile[] templates = Client.GetDeviceProfiles(false, null, false, false);
            if (templates.Length == 0)
            {
                PresetCB.Checked = true;
                CustomRB.Enabled = false;                
                return;
            }
            Templates.AddRange(templates);            
            GXAmiDeviceProfile DeviceProfiles = null;
            if (Device != null)
            {
                DeviceProfiles = FindDeviceProfiles(Device.ProfileId, templates);                
            }
            foreach (GXAmiDeviceProfile type in templates)
            {
                int pos = CustomDeviceProfile.Items.Add(type);
                if (DeviceProfiles != null && DeviceProfiles.Protocol == type.Protocol && type.Profile == DeviceProfiles.Profile)
                {
                    this.CustomDeviceProfile.SelectedIndexChanged -= new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    CustomDeviceProfile.SelectedIndices.Add(pos);
                    this.CustomDeviceProfile.SelectedIndexChanged += new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    UpdateMedias(true);
                    break;
                }
            }
        }

        private void CustomDeviceType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (CustomDeviceProfile.Items.Count <= e.Index || e.Index == -1)
            {
                return;
            }
            // Draw the current item text based on the current Font and the custom brush settings.            
            Brush textBrush;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textBrush = SystemBrushes.HighlightText;
            }
            else
            {
                textBrush = SystemBrushes.WindowText;
            }
            Rectangle rc = e.Bounds;
            rc.Location = new Point(0, e.Bounds.Top);
            string str = string.Empty;
            GXDeviceProfile tp = CustomDeviceProfile.Items[e.Index] as GXDeviceProfile;
            str = tp.Name;
            e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
            str = "[" + tp.Protocol + "]";
            SizeF size = e.Graphics.MeasureString(str, e.Font);
            rc.X += (int)(rc.Width - (size.Width));
            e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
        }        

        /// <summary>
        /// New preset device is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PresetList.SelectedIndices.Count == 1)
                {
                    CustomDeviceProfile.SelectedItems.Clear();
                    GXAmiDeviceProfile template = PresetList.SelectedItems[0].Tag as GXAmiDeviceProfile;
                    Device = Client.CreateDevice(template);
                    UIDevice = GXDevice.Create(Device.Manufacturer, Device.Model, Device.Version, Device.PresetName, "");
                    UpdateMedias(false);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }         
        }

        /// <summary>
        /// New custom device type is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CustomDeviceProfile.SelectedItems.Count == 1)
                {
                    PresetList.SelectedItems.Clear();
                    GXAmiDeviceProfile type = CustomDeviceProfile.SelectedItems[0] as GXAmiDeviceProfile;
                    GXAmiDeviceProfile DeviceProfiles = null;
                    if (Device != null)
                    {
                        DeviceProfiles = FindDeviceProfiles(Device.ProfileId, Templates.ToArray());            
                    }
                    if (DeviceProfiles == null || 
                        DeviceProfiles.Protocol != type.Protocol ||
                        DeviceProfiles.Profile != type.Name)
                    {
                        Device = Client.CreateDevice(type);
                        UIDevice = GXDevice.Create(Device.Protocol, Device.Profile, "");
                        UpdateMedias(false);
                    }                     
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        void UpdateMedias(bool loading)
        {
            GXAmiDeviceMedia selectedmedia = null;
            MediaCB.Items.Clear();            
            GXAmiDeviceProfile template = FindDeviceProfiles(Device.ProfileId, Templates.ToArray());
            foreach (GXAmiMediaType mt in template.AllowedMediaTypes)
            {
                if (AvailableMedias.Contains(mt.Name))
                {
                    int pos = MediaCB.Items.Add(mt);
                    if (selectedmedia == null)
                    {
                        foreach (GXAmiDeviceMedia media in MediaConnections)
                        {
                            if (string.Compare(media.Name, mt.Name) == 0)
                            {
                                selectedmedia = media;
                                MediaCB.SelectedIndex = pos;
                                break;
                            }
                        }
                    }
                }
            }
            if (MediaCB.Items.Count == 0)
            {
                MediaCB.Enabled = false;
                return;
            }
            if (MediaCB.SelectedIndex == -1)
            {                
                MediaCB.SelectedIndex = 0;
            }
            DeviceParameters.Clear();
            DeviceParameters.AddRange(Device.Parameters);
            PropertyGrid.SelectedObject = DeviceParameters;
            if (!string.IsNullOrEmpty(Device.ProtocolAddInType))
            {
                GXProtocolAddIn AddIn = Activator.CreateInstance(Type.GetType(Device.ProtocolAddInType)) as GXProtocolAddIn;
                //Update settings
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(UIDevice);
                foreach (GXAmiParameter it in Device.Parameters)
                {
                    if (it.Type != null && it.Value != null)
                    {
                        if (it.Type.IsEnum)
                        {
                            it.Value = Enum.Parse(it.Type, it.Value.ToString());
                        }
                        else if (it.Type.GetType() != typeof(object))
                        {
                            it.Value = Convert.ChangeType(it.Value, it.Type);
                        }
                    }
                    properties[it.Name].SetValue(UIDevice, it.Value);
                }
                SettingsForm = AddIn.GetCustomUI(UIDevice);
            }
            else if(UIDevice != null)
            {
                SettingsForm = UIDevice.AddIn.GetCustomUI(UIDevice);
            }
            if (SettingsForm != null)
            {
                SettingsForm.Location = SettingsPanel.Location;
                SettingsForm.Width = SettingsPanel.Width;
                SettingsForm.Height = SettingsPanel.Height;
                SettingsPanel.Visible = true;
                PropertyGrid.Visible = false;
                if (SettingsForm as IGXPropertyPage == null)
                {
                    throw new Exception("Custom GuruxAMI.Common.Device UI do not implement IGXPropertyPage interface.");
                }
                ((IGXPropertyPage)SettingsForm).Initialize();
                while (SettingsForm.Controls.Count != 0)
                {
                    Control ctr = SettingsForm.Controls[0];
                    if (ctr is Panel)
                    {
                        if (!ctr.Enabled)
                        {
                            SettingsForm.Controls.RemoveAt(0);
                            continue;
                        }
                    }
                    SettingsPanel.Controls.Add(ctr);
                }
                if (MediaConnections.Count != 0)
                {
                    if (!loading)
                    {
                        GXAmiDeviceMedia m = MediaConnections[0];
                        m.Name = SelectedMedia.Name;
                        m.Settings = SelectedMedia.Settings;
                    }
                    else
                    {
                        SelectedMedia.Settings = MediaConnections[0].Settings;
                        ((IGXPropertyPage)PropertiesForm).Initialize();
                    }
                }
            }
            else
            {
                SettingsPanel.Visible = false;
                PropertyGrid.Visible = true;
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
                string mediaName = null;
                if (MediaConnections.Count != 0)
                {
                    mediaName = MediaConnections[0].Name;
                }
                if (string.IsNullOrEmpty(mediaName) || mediaName != MediaCB.Text)
                {
                    GXAmiMediaType mt = MediaCB.SelectedItem as GXAmiMediaType;
                    SelectedMedia = UIDevice.GXClient.SelectMedia(mt.Name);
                    SelectedMedia.Settings = mt.Settings;                   
                }
                else
                {
                    SelectedMedia = UIDevice.GXClient.SelectMedia(mediaName);
                    SelectedMedia.Settings = MediaConnections[0].Settings;
                }
                if (SelectedMedia == null)
                {
                    throw new Exception(MediaCB.Text + " media not found.");
                }
                if (UIDevice.GXClient.PacketParser != null)
                {
                    UIDevice.GXClient.PacketParser.InitializeMedia(UIDevice.GXClient, SelectedMedia);
                }

                if (SelectedMedia is GXSerial)
                {
                    foreach (GXAmiDataCollector it in DataCollectors)
                    {
                        (SelectedMedia as GXSerial).AvailablePorts = it.SerialPorts;
                    }
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
                UpdateResendCnt(Device.ResendCount);
                UpdateWaitTime(Device.WaitTime);
            }
            catch (Exception ex)
            {                
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTB.Text.Trim().Length == 0)
                {
                    NameTB.Focus();
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
                }
                
                //Apply media dialog settings.
                if (SettingsForm != null)
                {
                    ((IGXPropertyPage)SettingsForm).Apply();
                }

                foreach(var it in DeviceParameters)
                {
                    //UIDevice
                }
                ((IGXPropertyPage)PropertiesForm).Apply();                
                //Validate media settings.
                SelectedMedia.Validate();
                //If we are adding new device.
                if (MediaConnections.Count == 0)
                {
                    MediaConnections.Add(new GXAmiDeviceMedia());
                }
                MediaConnections[0].Name = SelectedMedia.MediaType;
                MediaConnections[0].Settings = SelectedMedia.Settings;
                int resendCount = -3;
                double NoOutput;
                if (!ResendCountTb.ReadOnly)
                {
                    string tmpStr = ResendCountTb.Text.Trim();
                    if (tmpStr.Length == 0)
                    {
                        throw new Exception(Gurux.DeviceSuite.Properties.Resources.IncorrectResendCountTxt);
                    }
                    else if (!double.TryParse(tmpStr, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out NoOutput))
                    {
                        throw new Exception(Gurux.DeviceSuite.Properties.Resources.IncorrectResendCountTxt);
                    }
                    resendCount = (int)NoOutput;
                }
                int waitTime = -3; ;
                if (WaitTimeTb.Enabled)
                {
                    waitTime = (WaitTimeTb.Value.Hour * 3600 + WaitTimeTb.Value.Minute * 60 + WaitTimeTb.Value.Second) * 1000;
                }

                //Update settings
                if (UIDevice != null)
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(UIDevice);
                    object tmp;
                    foreach (GXAmiParameter it in Device.Parameters)
                    {
                        if (it.Type.IsEnum)
                        {
                            tmp = Enum.Parse(properties[it.Name].PropertyType, it.Value.ToString());
                        }
                        else
                        {
                            tmp = Convert.ChangeType(it.Value, properties[it.Name].PropertyType);
                        }                        
                        properties[it.Name].SetValue(UIDevice, tmp);
                    }
                    //Validate device settings.
                    GXTaskCollection tasks = new GXTaskCollection();
                    UIDevice.Validate(false, tasks);
                    if (tasks.Count != 0)
                    {
                        throw new Exception(tasks[0].Description);
                    }
                }
                
                Device.UpdateInterval = RefreshRateTp.Value.Second + RefreshRateTp.Value.Minute * 60 + RefreshRateTp.Value.Hour * 3600;
                Device.WaitTime = waitTime;
                Device.ResendCount = resendCount;
                Device.Name = NameTB.Text;
                int index = -1;
                foreach (var it in MediaConnections)
                {
                    it.Index = ++index;
                }
                Device.Medias = MediaConnections.ToArray();
                //Add disabled actions.
                Device.DisabledActions = m_DisActions.DisabledActions;
                if (Device.Id == 0)
                {
                    Client.AddDevice(Device, Client.GetDeviceGroups(false));                   
                }
                else
                {
                    Client.Update(Device);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Show help not available message.
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

        void AddConnection(GXAmiDeviceMedia m)
        {
            //Find name of DC.
            string dc = "";
            foreach (GXAmiDataCollector it in DataCollectors)
            {
                if (it.Id == m.DataCollectorId)
                {
                    dc = it.Name;
                    break;
                }
            }
            ListViewItem li = RedundantConnectionsList.Items.Add(m.Name);
            li.SubItems.AddRange(new string[] {m.Settings, dc });
            li.Tag = m;
        }

        private void AddMenu_Click(object sender, EventArgs e)
        {
            GXAmiDeviceMedia m = new GXAmiDeviceMedia();
            GXAmiDeviceProfile template = FindDeviceProfiles(Device.ProfileId, Templates.ToArray());
            RedundantForm dlg = new RedundantForm(UIDevice.GXClient, DataCollectors, template.AllowedMediaTypes, m);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                MediaConnections.Add(m);
                AddConnection(m);
            }
        }

        private void EditMenu_Click(object sender, EventArgs e)
        {
            ListViewItem li = RedundantConnectionsList.SelectedItems[0];
            GXAmiDeviceMedia m = li.Tag as GXAmiDeviceMedia;
            GXAmiDeviceProfile template = FindDeviceProfiles(Device.ProfileId, Templates.ToArray());
            RedundantForm dlg = new RedundantForm(UIDevice.GXClient, DataCollectors, template.AllowedMediaTypes, m);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                //Find name of DC.
                string dc = "";
                foreach (GXAmiDataCollector it in DataCollectors)
                {
                    if (it.Id == m.DataCollectorId)
                    {
                        dc = it.Name;
                        break;
                    }
                }                
                li.SubItems[0].Text = m.Name;
                li.SubItems[1].Text = m.Settings;
                li.SubItems[2].Text = dc;
            }
        }       

        private void RemoveMenu_Click(object sender, EventArgs e)
        {
            if (GXCommon.ShowQuestion(Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt) != DialogResult.Yes)
            {
                return;
            }
            ListViewItem it = RedundantConnectionsList.SelectedItems[0];
            it.Remove();
            MediaConnections.Remove(it.Tag as GXAmiDeviceMedia);
        }

        /// <summary>
        /// Move redundant connection Up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUpMenu_Click(object sender, EventArgs e)
        {
            ListViewItem it = RedundantConnectionsList.SelectedItems[0];            
            int index = it.Index;
            RedundantConnectionsList.Items.RemoveAt(index);
            RedundantConnectionsList.Items.Insert(index - 1, it);
            index = MediaConnections.IndexOf(it.Tag as GXAmiDeviceMedia) - 1;
            MediaConnections.Remove(it.Tag as GXAmiDeviceMedia);
            MediaConnections.Insert(index, it.Tag as GXAmiDeviceMedia);
        }

        /// <summary>
        /// Move redundant connection Down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDownMenu_Click(object sender, EventArgs e)
        {
            ListViewItem it = RedundantConnectionsList.SelectedItems[0];
            int index = it.Index;
            RedundantConnectionsList.Items.RemoveAt(index);
            RedundantConnectionsList.Items.Insert(index + 1, it);
            index = MediaConnections.IndexOf(it.Tag as GXAmiDeviceMedia) + 1;
            MediaConnections.Remove(it.Tag as GXAmiDeviceMedia);
            MediaConnections.Insert(index, it.Tag as GXAmiDeviceMedia);
        }

        private void RedundantMenu_Opening(object sender, CancelEventArgs e)
        {
            bool Selected = RedundantConnectionsList.SelectedItems.Count != 0;
            EditMenu.Enabled = RemoveMenu.Enabled = Selected;
            MoveUpMenu.Enabled = Selected && RedundantConnectionsList.SelectedItems[0].Index != 0;
            MoveDownMenu.Enabled = Selected && RedundantConnectionsList.SelectedItems[0].Index != RedundantConnectionsList.Items.Count - 1;
        }

        /// <summary>
        /// Show available medias when DC change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectorsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //This is not do on load.
                if (SelectedMedia != null)
                {
                    //If new device
                    if (Device.Medias == null)
                    {
                        Device.Medias = new GXAmiDeviceMedia[] { new GXAmiDeviceMedia() };
                        MediaConnections.AddRange(Device.Medias);
                    }
                    if ((CollectorsCB.SelectedItem is string))
                    {
                        MediaConnections[0].DataCollectorId = null;
                    }
                    else
                    {
                        MediaConnections[0].DataCollectorId = ((GXAmiDataCollector)CollectorsCB.SelectedItem).Id;                        
                    }
                    GetAllAvailableMediasFromDCs();
                    ((IGXPropertyPage)PropertiesForm).Apply();                    
                    MediaConnections[0].Name = SelectedMedia.MediaType;
                    if (AvailableMedias.Contains(SelectedMedia.MediaType))
                    {
                        MediaConnections[0].Settings = SelectedMedia.Settings;
                    }
                    else
                    {
                        MediaConnections[0].Settings = "";
                    }
                    UpdateMedias(true);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);                
            }
        }

    }  
}
