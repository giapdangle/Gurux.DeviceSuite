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
        List<GXAmiDeviceTemplate> Templates = new List<GXAmiDeviceTemplate>();
        GXParameters DeviceParameters = new GXParameters();

        public GXAmiDeviceSettingsForm(GXAmiClient client, GuruxAMI.Common.GXAmiDevice device, GXAmiDataCollector[] dcs)
        {
            DataCollectors = dcs;
            AvailableMedias = new List<string>();
            foreach(GXAmiDataCollector it in DataCollectors)
            {
                foreach(string media in it.Medias)
                {
                    if (!AvailableMedias.Contains(media))
                    {
                        AvailableMedias.Add(media);
                    }
                }
            }
            Client = client;
            Device = device;            
            InitializeComponent();            
            SettingsPanel.Dock = PropertyGrid.Dock = PresetList.Dock = CustomDeviceType.Dock = DockStyle.Fill;
            //GuruxAMI.Common.Device type can not be changed after creation. This is for secure reasons.
            PresetCB.Enabled = CustomRB.Enabled = PresetList.Enabled = CustomDeviceType.Enabled = Device == null;                  
            CustomDeviceType.Visible = false;            
            if (Device != null)
            {                                
                NameTB.Text = Device.Name;
                RefreshRateTp.Value = new DateTime(((long)Device.UpdateInterval) * 10000000 + RefreshRateTp.MinDate.Ticks);
                UpdateResendCnt(Device.ResendCount);
                UpdateWaitTime(Device.WaitTime);
                //Create UI Device so all assemblys are loaded.
                if (string.IsNullOrEmpty(Device.PresetName))
                {
                    UIDevice = GXDevice.Create(Device.Protocol, Device.Template, "");
                }
                else
                {
                    UIDevice = GXDevice.Create(Device.Manufacturer, Device.Model,Device.Version, Device.PresetName, "");
                }
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
            CustomDeviceType.Visible = !preset;
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
                    else if (CustomDeviceType.Items.Count != 0)
                    {
                        CustomDeviceType.SelectedIndex = 0;
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
            GXAmiDeviceTemplate[] templates = Client.GetDeviceTypes(true, null, false);
            if (templates.Length == 0)
            {
                CustomRB.Checked = true;
                PresetCB.Enabled = false;
                return;
            }
            Templates.AddRange(templates);
            GXAmiDeviceTemplate deviceTemplate = null;            
            if (Device != null)
            {
                deviceTemplate = FindDeviceTemplate(Device.TemplateId, templates);                
            }
            Dictionary<GXAmiDeviceTemplate, ListViewItem> items = new Dictionary<GXAmiDeviceTemplate, ListViewItem>();
            foreach (GXAmiDeviceTemplate it in templates)
            {
                ListViewItem item = new ListViewItem(new string[]{it.Manufacturer, it.Model, 
                                it.Version, it.PresetName, it.TemplateVersion.ToString()});
                item.Tag = it;
                items.Add(it, item);               
            }
            PresetList.Items.AddRange(items.Values.ToArray());
            //Select found device.
            if (deviceTemplate != null)
            {               
                this.PresetList.SelectedIndexChanged -= new System.EventHandler(this.PresetList_SelectedIndexChanged);
                items[deviceTemplate].Selected = true;
                this.PresetList.SelectedIndexChanged += new System.EventHandler(this.PresetList_SelectedIndexChanged);          
                UpdateMedias();
            }             
        }

        GXAmiDeviceTemplate FindDeviceTemplate(ulong id, GXAmiDeviceTemplate[] templates)
        {
            if (id == 0)
            {
                return null;
            }
            foreach (GXAmiDeviceTemplate it in templates)
            {
                if (it.Id == Device.TemplateId)
                {
                    return it;
                }
            }
            return null;
        }

        void ShowCustomDeviceTypes()
        {
            GXAmiDeviceTemplate[] templates = Client.GetDeviceTypes(false, null, false);
            if (templates.Length == 0)
            {
                PresetCB.Checked = true;
                CustomRB.Enabled = false;                
                return;
            }
            Templates.AddRange(templates);            
            GXAmiDeviceTemplate deviceTemplate = null;
            if (Device != null)
            {
                deviceTemplate = FindDeviceTemplate(Device.TemplateId, templates);                
            }
            foreach (GXAmiDeviceTemplate type in templates)
            {
                int pos = CustomDeviceType.Items.Add(type);
                if (deviceTemplate != null && deviceTemplate.Protocol == type.Protocol && type.Template == deviceTemplate.Template)
                {
                    this.CustomDeviceType.SelectedIndexChanged -= new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    CustomDeviceType.SelectedIndices.Add(pos);
                    this.CustomDeviceType.SelectedIndexChanged += new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    UpdateMedias();
                    break;
                }
            }
        }

        private void CustomDeviceType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (CustomDeviceType.Items.Count <= e.Index || e.Index == -1)
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
            GXDeviceType tp = CustomDeviceType.Items[e.Index] as GXDeviceType;
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
                    CustomDeviceType.SelectedItems.Clear();
                    GXAmiDeviceTemplate template = PresetList.SelectedItems[0].Tag as GXAmiDeviceTemplate;
                    Device = Client.CreateDevice(template);
                    UIDevice = GXDevice.Create(Device.Manufacturer, Device.Model, Device.Version, Device.PresetName, "");
                    UpdateMedias();
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
                if (CustomDeviceType.SelectedItems.Count == 1)
                {
                    PresetList.SelectedItems.Clear();
                    GXAmiDeviceTemplate type = CustomDeviceType.SelectedItems[0] as GXAmiDeviceTemplate;
                    GXAmiDeviceTemplate deviceTemplate = null;
                    if (Device != null)
                    {
                        deviceTemplate = FindDeviceTemplate(Device.TemplateId, Templates.ToArray());            
                    }
                    if (deviceTemplate == null || 
                        deviceTemplate.Protocol != type.Protocol ||
                        deviceTemplate.Template != type.Name)
                    {
                        Device = Client.CreateDevice(type);
                        UIDevice = GXDevice.Create(Device.Protocol, Device.Template, "");
                        UpdateMedias();
                    }                     
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        void UpdateMedias()
        {
            MediaCB.Items.Clear();
            GXAmiDeviceTemplate template = FindDeviceTemplate(Device.TemplateId, Templates.ToArray());
            foreach (GXAmiMediaType mt in template.AllowedMediaTypes)
            {
                if (AvailableMedias.Contains(mt.Name))
                {
                    int pos = MediaCB.Items.Add(mt);
                    if (Device != null && string.Compare(Device.MediaName, mt.Name) == 0)
                    {
                        MediaCB.SelectedIndex = pos;
                    }
                }
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
                Device.MediaSettings = SelectedMedia.Settings;
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
                if (string.IsNullOrEmpty(Device.MediaName) || Device.MediaName != MediaCB.Text)
                {
                    GXAmiMediaType mt = MediaCB.SelectedItem as GXAmiMediaType;
                    SelectedMedia = UIDevice.GXClient.SelectMedia(mt.Name);
                    SelectedMedia.Settings = mt.Settings;                   
                }
                else
                {
                    SelectedMedia = UIDevice.GXClient.SelectMedia(Device.MediaName);
                    SelectedMedia.Settings = Device.MediaSettings;
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
                    foreach (GXAmiParameter it in Device.Parameters)
                    {
                        properties[it.Name].SetValue(UIDevice, it.Value);
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
                Device.MediaName = SelectedMedia.MediaType;
                Device.MediaSettings = SelectedMedia.Settings;
                //Add disabled actions.
                Device.DisabledActions = m_DisActions.DisabledActions;
                if (Device.Id == 0)
                {
                    Client.AddDevice(Device, Client.GetDeviceGroups(false));
                    foreach (GXAmiDataCollector it in DataCollectors)
                    {
                        Client.AddDataCollector(it, new GuruxAMI.Common.GXAmiDevice[] { Device });
                    }
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
    }  
}
