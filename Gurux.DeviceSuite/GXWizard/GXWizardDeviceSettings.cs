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
using Gurux.Device.Editor;
using Gurux.Communication;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.Device.PresetDevices;
using System.IO;

namespace Gurux.DeviceSuite.GXWizard
{
    public partial class GXWizardDeviceSettings : Form, IGXWizardPage
    {       
        GXDevice Device;

        public GXWizardDeviceSettings()
        {
            InitializeComponent();
        }       
              
        #region IGXWizardPage Members

        public bool IsShown()
        {
            return true;
        }

        public void Next()
        {
            if (NameTB.Text.Trim().Length == 0)
            {
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
            }
            GXProtocolAddIn addIn = Gurux.Device.GXDeviceList.Protocols[ProtocolCB.Text];
            Device = GXDevice.CreateDeviceProfiles(addIn, NameTB.Text);
            if (Device.IsRegistered())
            {
                if (GXCommon.ShowQuestion(Gurux.DeviceSuite.Properties.Resources.OverregisterTxt) != DialogResult.Yes)
                {
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.CancelTxt);
                }
            }
            GXCommunicationAttribute att = TypeDescriptor.GetAttributes(Device.AddIn)[typeof(GXCommunicationAttribute)] as GXCommunicationAttribute;
            if (att != null && att.PacketParserType != null)
            {
                Device.GXClient.PacketParser = Activator.CreateInstance(att.PacketParserType) as IGXPacketParser;
            }
        
        }

        public void Back()
        {         
        }

        public void Finish()
        {            
        }

        public void Cancel()
        {         
        }

        public void Initialize()
        {
            ProtocolCB.Items.Clear();
            string[] disabledProtocols;
            if (Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols == null)
            {
                disabledProtocols = new string[0];
            }
            else
            {
                disabledProtocols = new string[Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols.Count];
                Gurux.DeviceSuite.Properties.Settings.Default.DisabledProtocols.CopyTo(disabledProtocols, 0);
            }
            foreach (string it in GXDeviceList.Protocols.Keys)
            {
                if (!disabledProtocols.Contains(it))
                {
                    ProtocolCB.Items.Add(it);
                }
            }
            ProtocolCB.SelectedIndex = 0;

        }

        public Gurux.Common.GXWizardButtons EnabledButtons
        {
            get 
            {
                return GXWizardButtons.All ^ GXWizardButtons.Back;                
            }
        }

        public string Caption
        {
            get 
            {
                return Gurux.DeviceSuite.Properties.Resources.DeviceProfileWizardTitle;
            }
        }

        public string Description
        {
            get 
            {
                return Gurux.DeviceSuite.Properties.Resources.DeviceSettingsWizardDescription;
            }
        }

        public object Target
        {
            get
            {
                return Device;
            }
            set
            {
                Device = value as GXDevice;
            }
        }

        #endregion       
    }
}
