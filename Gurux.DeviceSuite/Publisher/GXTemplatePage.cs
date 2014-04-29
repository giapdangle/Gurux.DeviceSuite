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
using System.Windows.Forms;
using Gurux.Common;
using Gurux.Device;
using System.IO;
using Gurux.Device.Editor;
using Gurux.Device.PresetDevices;
using Gurux.Device.Publisher;
using System.Drawing;

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXTemplatePage : Form, IGXWizardPage
    {
        GXPublisher Item;

        public GXTemplatePage(GXPublisher item)
        {
            Item = item;
            InitializeComponent();
        }

        #region IGXWizardPage Members

        public bool IsShown()
        {
            return true;
        }

        public void Next()
        {
            if (DeviceProfilesCB.SelectedIndex == -1)
            {
                DeviceProfilesCB.Focus();
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
            }
            //Update selected manufacturer to target.            
            GXDeviceVersion ver = Target as GXDeviceVersion;
            GXPublishedDeviceProfile type = ver.Templates.Find(DeviceProfilesCB.SelectedItem.ToString());
            Target = type;
            Item.Manufacturers[0].Models[0].Versions[0].Templates.Clear();
            GXDevice dev = GXDevice.Load(type.Path);
            string name = Path.GetTempFileName();
            GXZip.Export(dev, name);
            using (Stream stream = File.OpenRead(name))
            {
                BinaryReader r = new BinaryReader(stream);
                Item.Data = r.ReadBytes((int)stream.Length);
            }
            Item.Manufacturers[0].Models[0].Versions[0].Templates.Add(type);
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
            if (Target is GXDeviceVersion)
            {
                GXDeviceVersion ver = Target as GXDeviceVersion;
                foreach (GXPublishedDeviceProfile it in ver.Templates)
                {
                    DeviceProfilesCB.Items.Add(it.PresetName);
                }
                DeviceProfilesCB.SelectedItem = DeviceProfilesCB.Items[0];
            }
        }

        public GXWizardButtons EnabledButtons
        {
            get
            {
                return GXWizardButtons.Back | GXWizardButtons.Cancel | GXWizardButtons.Next;
            }
        }

        public string Caption
        {
            get
            {
                return "Device template information";
            }
        }

        public string Description
        {
            get
            {
                return "Select device template that you want to publish.";
            }
        }

        public object Target
        {
            get;
            set;

        }

        #endregion      
    }
}
