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
using Gurux.Device;
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Manufacturer
{
    public partial class GXDeviceProfilesForm : Form
    {
        public GXPublishedDeviceProfile Target;

        void UpdateTarget(GXPublishedDeviceProfile item)
        {
            SelectedTemplateTB.Text = item.Name;
            ProtocolTB.Text = item.Protocol;
        }

        public GXDeviceProfilesForm(GXPublishedDeviceProfile target)
        {
            InitializeComponent();
            Target = target;
            NameTB.Text = Target.PresetName;

            bool installed = target.Versions.Count != 0;
            NameTB.ReadOnly = installed;
            AddBtn.Enabled = OKBtn.Enabled = !installed;
            List<int> items = new List<int>(target.Versions.Count);
            //Show selected templates
            if(!string.IsNullOrEmpty(target.PresetName))
            {
                string str = target.Protocol + target.Name;
                UpdateTarget(target);
                items.Add(str.GetHashCode());
                AddBtn.Enabled = false;
            }
            //Show available templates.
            foreach (GXDeviceProfile it in GXDeviceList.GetDeviceTypes(false, null))
            {
                string str = it.Protocol + it.Name;
                if (!items.Contains(str.GetHashCode()))
                {
                    AvailableTemplates.Items.Add(it);
                }
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTB.Text.Trim().Length == 0)
                {
                    NameTB.Focus();
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
                }
                if (string.IsNullOrEmpty(SelectedTemplateTB.Text))
                {
                    throw new Exception("Select used template.");
                }                
                Target.PresetName = NameTB.Text;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                this.DialogResult = DialogResult.None;
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {                
                if (AddBtn.Enabled && AvailableTemplates.SelectedItems.Count != 0)
                {
                    if (Target != null)
                    {
                        AvailableTemplates.Items.Add(Target);
                    }
                    ListBox.SelectedObjectCollection items = AvailableTemplates.SelectedItems;
                    GXPublishedDeviceProfile item = new GXPublishedDeviceProfile(AvailableTemplates.SelectedItems[0] as GXDeviceProfile);
                    AvailableTemplates.Items.Remove(items[0]);                    
                    Target = item;
                    UpdateTarget(item);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }       
    }
}
