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
using System.Collections;
using Gurux.Common;
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Editor
{
    public partial class OpenDlg : Form
    {
        GXDeviceManufacturerCollection Manufacturers;
        public GXDeviceProfile Checked;
        GXDeviceProfile Target;
        System.Collections.Hashtable PresetItemToListViewItem = new System.Collections.Hashtable();

        public OpenDlg()
        {

        }

        /// <summary>
		/// Initializes a new instance of the OpenDlg class.
		/// </summary>
        public OpenDlg(GXDeviceManufacturerCollection manufacturers, GXDeviceProfile target)
		{			
			InitializeComponent();
            Target = target;
            Manufacturers = manufacturers;            
			UpdateResources();
            if (GXDeviceList.GetDeviceTypes(false, null).Count == 0 && GXDeviceList.GetDeviceTypes(true, null).Count == 0)
            {
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.NoDevicesPublishedTxt);
            }
            ShowPresetDevices();
			ShowCustomDeviceTypes();
		}

		private void UpdateResources()
		{
			RemoveMnu.Text = Gurux.DeviceSuite.Properties.Resources.RemoveTxt;
			OKBtn.Text = Gurux.DeviceSuite.Properties.Resources.OKTxt;
			CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
			this.Text = Gurux.DeviceSuite.Properties.Resources.OpenProjectTxt;
		}

        protected void RemoveMnu_Click(object sender, System.EventArgs e)
        {
            try
            {                
                GXDeviceProfile removed = GetChecked();
                if (removed != null)
                {
                    DialogResult dr = GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.DoYouWantToRemoveTxt + removed.ToString());
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                    if (removed is GXPublishedDeviceProfile)
                    {
                        GXPublishedDeviceProfile pd = removed as GXPublishedDeviceProfile;
                        string manufacturer, model, version, presetName;
                        pd.GetInfo(out manufacturer, out model, out version, out presetName);
                        GXDevice.Unregister(manufacturer, model, version, presetName);
                        PresetList.Items.Remove(PresetList.SelectedItems[0]);
                    }
                    else
                    {
                        GXDevice.Unregister(removed.Protocol, removed.Name);
                        CustomDeviceType.Items.Remove(CustomDeviceType.SelectedItems[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        /// <summary>
        /// Get the checked item.
        /// </summary>
        /// <returns>The checked item or null if nothing is checked.</returns>
        protected GXDeviceProfile GetChecked()
        {
            if (tabControl1.SelectedTab == PresetPage)
            {
                if (PresetList.SelectedItems.Count != 1)
                {
                    return null;
                }
                return PresetList.SelectedItems[0].Tag as GXDeviceProfile;
            }
            if (CustomDeviceType.SelectedItems.Count != 1)
            {
                return null;
            }
            return CustomDeviceType.SelectedItems[0] as GXDeviceProfile;
        }

        private void DeviceTypeLB_DoubleClick(object sender, System.EventArgs e)
        {
            OKBtn_Click(null, null);
        }

        protected virtual void OKBtn_Click(object sender, System.EventArgs e)
        {
            this.Checked = GetChecked();
            if (this.Checked == null)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        void ShowPresetDevices()
        {
            if (GXDeviceList.GetDeviceTypes(true, null).Count == 0)
            {
                tabControl1.TabPages.Remove(PresetPage);
                return;
            }
            //PresetList.Items
            List<ListViewItem> items = new List<ListViewItem>();
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
                            PresetItemToListViewItem[type] = item;
                            items.Add(item);
                        }
                    }
                }
            }
            PresetList.Items.AddRange(items.ToArray());

            ListViewItem target = null;
            if (Target != null)
            {
                target = PresetItemToListViewItem[Target] as ListViewItem;
            }
            if (target == null)
            {
                PresetList.SelectedIndices.Add(0);
            }
            else
            {
                PresetList.SelectedIndices.Add(target.Index);
            }
        }

        void ShowCustomDeviceTypes()
        {
            if (GXDeviceList.GetDeviceTypes(false, null).Count == 0)
            {
                tabControl1.TabPages.Remove(CustomPage);
                return;
            }
            SortedList sl = new SortedList();
            foreach (GXDeviceProfile type in GXDeviceList.GetDeviceTypes(false, null))
            {
                CustomDeviceType.Items.Add(type);
            }
        }

        protected void CancelBtn_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }             
    }
}
