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
using GuruxAMI.Client;
using GuruxAMI.Common;

namespace Gurux.DeviceSuite.Ami
{
    public partial class GXAmiImportForm : Gurux.DeviceSuite.Editor.OpenDlg
    {
        GXAmiClient Client;

        /// <summary>
		/// Initializes a new instance of the GXAmiImportForm class.
		/// </summary>
        public GXAmiImportForm(GXAmiClient client, GXDeviceManufacturerCollection manufacturers)
            : base(manufacturers, null)
		{
			InitializeComponent();
            Client = client;
		}

        protected override void OKBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Checked = GetChecked();                
                if (this.Checked == null)
                {
                    throw new Exception("Imported device template not selected.");
                }
                GXDevice device = null;
                if (this.Checked is GXPublishedDeviceProfile)
                {
                    string manufacturer, model, version, presetName;
                    (this.Checked as GXPublishedDeviceProfile).GetInfo(out manufacturer, out model, out version, out presetName);
                    device = GXDevice.Create(manufacturer, model, version, presetName, "import");
                }
                else
                {
                    device = GXDevice.Create(this.Checked.Protocol, this.Checked.Name, "import");
                }
                GXAmiDeviceProfile[] templates = Client.GetDeviceProfiles(true, false);
                foreach (GXAmiDeviceProfile it in templates)
                {
                    if (it.Guid == device.Guid)
                    {
                        if (GXCommon.ShowExclamation(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.DeviceProfileExistsTxt) != DialogResult.Yes)
                        {
                            DialogResult = DialogResult.None;
                            return;
                        }
                        break;
                    }
                }
                Client.AddDeviceProfile(Client.GetUserGroups(false), device);
                device.Dispose();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                DialogResult = DialogResult.None;
            }
        }        
    }
}
