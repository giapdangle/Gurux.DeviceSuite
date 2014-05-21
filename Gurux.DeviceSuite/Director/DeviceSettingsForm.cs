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
using Gurux.Device.Editor;
using Gurux.Communication;
using System.Globalization;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.Device.PresetDevices;
using System.Collections;
using GuruxAMI.Gateway;

namespace Gurux.DeviceSuite.Director
{
    public partial class DeviceSettingsForm : Form
    {
        System.Collections.Hashtable PresetItemToListViewItem = new System.Collections.Hashtable();
        private DisabledAction m_DisActions;
        Form PropertiesForm;
        Form SettingsForm;        
        internal Gurux.Common.IGXMedia SelectedMedia;

        GXDeviceManufacturerCollection Manufacturers;
        public GXDevice Device;
        public DeviceSettingsForm(GXDeviceManufacturerCollection manufacturers, GXDevice device)
        {
            Device = device;            
            Manufacturers = new GXDeviceManufacturerCollection();            
            InitializeComponent();
            AddManufacturers(manufacturers, Manufacturers);
            SettingsPanel.Dock = PropertyGrid.Dock = PresetList.Dock = CustomDeviceProfile.Dock = DockStyle.Fill;
            //Device type can not be changed after creation. This is for secure reasons.
            PresetCB.Enabled = CustomRB.Enabled = PresetList.Enabled = CustomDeviceProfile.Enabled = Device == null;                  
            CustomDeviceProfile.Visible = false;            
            if (Device != null)
            {
                NameTB.Text = Device.Name;
                RefreshRateTp.Value = new DateTime(((long)Device.UpdateInterval) * 10000000 + RefreshRateTp.MinDate.Ticks);
                UpdateResendCnt(Device.ResendCount);
                UpdateWaitTime(Device.WaitTime);  
            }
            else
            {
                RefreshRateTp.Value = new DateTime(((long)1) * 10000000 + RefreshRateTp.MinDate.Ticks);                
            }
            //Add disabled actions.
            m_DisActions = new DisabledAction(Device == null ? DisabledActions.None : Device.DisabledActions);
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

        /// <summary>
        /// Only manufacturers in both device templates are shown.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void AddManufacturers(GXDeviceManufacturerCollection from, GXDeviceManufacturerCollection to)
        {
            foreach (GXDeviceManufacturer man in from)
            {
                foreach (GXDeviceModel model in man.Models)
                {
                    foreach (GXDeviceVersion version in model.Versions)
                    {
                        if (version.Templates.Count != 0)
                        {
                            to.Add(man);
                        }
                    }
                }
            }
        }
       
        private void DeviceSettingsForm_Load(object sender, EventArgs e)
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
            if (GXDeviceList.GetDeviceTypes(true, null).Count == 0)
            {
                CustomRB.Checked = true;
                PresetCB.Enabled = false;
                return;
            }            
            bool find = Device != null && !string.IsNullOrEmpty(Device.Manufacturer);
            Dictionary<GXPublishedDeviceProfile, ListViewItem> items = new Dictionary<GXPublishedDeviceProfile, ListViewItem>();
            foreach (GXDeviceManufacturer man in Manufacturers)
            {
                foreach (GXDeviceModel model in man.Models)
                {
                    foreach (GXDeviceVersion dv in model.Versions)
                    {
                        foreach (GXPublishedDeviceProfile type in dv.Templates)
                        {
                            ListViewItem item = new ListViewItem(type.PresetName);
                            item.SubItems.Add(man.Name);
                            item.SubItems.Add(model.Name);
                            item.SubItems.Add(dv.Name);
                            item.Tag = type;
                            items.Add(type, item);
                        }
                    }
                }
            }
            PresetList.Items.AddRange(items.Values.ToArray());
            //Select found device.
            if (find)
            {
                GXDeviceManufacturer man = Manufacturers.Find(Device.Manufacturer);
                if (man == null)
                {
                    throw new Exception("Invalid Device Manufacturer: " + man.Name);
                }

                GXDeviceModel model = man.Models.Find(Device.Model);
                if (model == null)
                {
                    throw new Exception("Invalid Device Model: " + model.Name);
                } 
                GXDeviceVersion version = model.Versions.Find(Device.Version);
                if (version == null)
                {
                    throw new Exception("Invalid Device Version: " + version.Name);
                } 
                GXPublishedDeviceProfile type = version.Templates.Find(Device.PresetName);
                if (type == null)
                {
                    throw new Exception("Invalid Device Type: " + type.PresetName);
                }
                this.PresetList.SelectedIndexChanged -= new System.EventHandler(this.PresetList_SelectedIndexChanged);
                items[type].Selected = true;
                this.PresetList.SelectedIndexChanged += new System.EventHandler(this.PresetList_SelectedIndexChanged);
                UpdateMedias();
            }
        }

        void ShowCustomDeviceTypes()
        {
            if (GXDeviceList.GetDeviceTypes(false, null).Count == 0)
            {
                PresetCB.Checked = true;
                CustomRB.Enabled = false;                
                return;
            }
            bool find = Device != null && string.IsNullOrEmpty(Device.Manufacturer);
            foreach (GXDeviceProfile type in GXDeviceList.GetDeviceTypes(false, null))
            {
                int pos = CustomDeviceProfile.Items.Add(type);
                if (find && Device.ProtocolName == type.Protocol && type.Name == Device.DeviceProfile)
                {
                    this.CustomDeviceProfile.SelectedIndexChanged -= new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    CustomDeviceProfile.SelectedIndices.Add(pos);
                    this.CustomDeviceProfile.SelectedIndexChanged += new System.EventHandler(this.CustomDeviceType_SelectedIndexChanged);
                    UpdateMedias();
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
                    GXPublishedDeviceProfile type = PresetList.SelectedItems[0].Tag as GXPublishedDeviceProfile;
                    GXDeviceVersion ver = type.Parent.Parent;
                    GXDeviceModel model = ver.Parent.Parent;
                    GXDeviceManufacturer man = model.Parent.Parent; ;
                    if (Device != null)
                    {
                        Device.Dispose();
                    }
                    Device = GXDevice.Create(man.Name, model.Name, ver.Name, type.PresetName, "");
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
                if (CustomDeviceProfile.SelectedItems.Count == 1)
                {
                    PresetList.SelectedItems.Clear();
                    GXDeviceProfile type = CustomDeviceProfile.SelectedItems[0] as GXDeviceProfile;
                    if (Device == null || 
                        Device.ProtocolName != type.Protocol || 
                        Device.DeviceProfile != type.Name)
                    {
                        if (Device != null)
                        {
                            Device.Dispose();
                        }
                        Device = GXDevice.Create(type.Protocol, type.Name, "");
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
            List<string> medias = new List<string>();
            if (Device.AllowedMediaTypes.Count == 0)
            {
                medias.AddRange(Gurux.Communication.GXClient.GetAvailableMedias());
            }
            else
            {
                List<string> availableMedias = new List<string>(Gurux.Communication.GXClient.GetAvailableMedias());
                string gw = new GXAmiGateway().MediaType;
                foreach (GXMediaType it in Device.AllowedMediaTypes)
                {
                    if (availableMedias.Contains(it.Name))
                    {
                        medias.Add(it.Name);
                    }
                }
                //Add Gateway media.
                if (!medias.Contains(gw))
                {
                    medias.Add(gw);
                }
            }
            string selectedMediaType = Device.GXClient.MediaType;
            foreach (string it in medias)
            {
                int pos = MediaCB.Items.Add(it);
                if (Device != null && selectedMediaType == it)
                {
                    MediaCB.SelectedIndex = pos;
                }
            }
            if (MediaCB.SelectedIndex == -1)
            {
                MediaCB.SelectedIndex = 0;
            }
            PropertyGrid.SelectedObject = Device;
            SettingsForm = Device.AddIn.GetCustomUI(Device);
            if (SettingsForm != null)
            {
                SettingsForm.Location = SettingsPanel.Location;
                SettingsForm.Width = SettingsPanel.Width;
                SettingsForm.Height = SettingsPanel.Height;
                SettingsPanel.Visible = true;
                PropertyGrid.Visible = false;
                if (SettingsForm as IGXPropertyPage == null)
                {
                    throw new Exception("Custom Device UI do not implement IGXPropertyPage interface.");
                }
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
                ((IGXPropertyPage)SettingsForm).Initialize();
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
                //If new media is selected or media is changed.
                if (Device.GXClient.Media == null || Device.GXClient.Media.MediaType != MediaCB.Text)
                {
                    SelectedMedia = Device.GXClient.SelectMedia(MediaCB.Text);
                    Gurux.Device.GXMediaTypeCollection mediatypes = Device.GetAllowedMediaTypes();
                    Gurux.Device.GXMediaType tp = mediatypes[SelectedMedia.MediaType];
                    if (tp != null)
                    {
                        SelectedMedia.Settings = tp.DefaultMediaSettings;
                    }
                    if (SelectedMedia is GXAmiGateway)
                    {
                        GXAmiGateway gw = SelectedMedia as GXAmiGateway;
                        gw.Host = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
                        gw.UserName = Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName;
                        gw.Password = Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword;
                        gw.GXClient = Device.GXClient;
                    }
                }
                else
                {
                    SelectedMedia = Device.GXClient.Media;
                }
                if (SelectedMedia == null)
                {
                    throw new Exception(MediaCB.Text + " media not found.");
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
                ((IGXPropertyPage)PropertiesForm).Apply();
                //Validate device settings.
                GXTaskCollection tasks = new GXTaskCollection();
                Device.Validate(false, tasks);
                if (tasks.Count != 0)
                {
                    throw new Exception(tasks[0].Description);
                }
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
                Device.UpdateInterval = RefreshRateTp.Value.Second + RefreshRateTp.Value.Minute * 60 + RefreshRateTp.Value.Hour * 3600;
                Device.WaitTime = waitTime;
                if (SelectedMedia is GXAmiGateway)
                {
                    (SelectedMedia as GXAmiGateway).WaitTime = waitTime;
                }
                Device.ResendCount = resendCount;
                Device.Name = NameTB.Text;                
                Device.GXClient.AssignMedia(SelectedMedia);
                //Add disabled actions.
                Device.DisabledActions = m_DisActions.DisabledActions;
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
