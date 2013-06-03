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
using Gurux.DeviceSuite.Manufacturer;
using Gurux.Device.PresetDevices;
using Gurux.DeviceSuite.Common;
using Gurux.Device.Publisher;

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXVersionPage : Form, IGXWizardPage
    {
        GXPublisher Item;
        GXDeviceModel SelectedModel;
        GXDeviceVersion SelectedVersion;

        public GXVersionPage(GXPublisher item)
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
            if (VersionCB.SelectedIndex == -1)
            {
                VersionCB.Focus();
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
            }
            if ((VersionCB.SelectedItem as GXDeviceVersion).Templates.Count == 0)
            {
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.PublishFailedNotEnoughtData);
            }
            //Update selected version to target.
            Target = SelectedVersion = VersionCB.SelectedItem as GXDeviceVersion;
            Item.Manufacturers[0].Models[0].Versions.Clear();
            Item.Manufacturers[0].Models[0].Versions.Add(SelectedVersion);
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
            GXDeviceModel model = Target as GXDeviceModel;
            if (model != null)
            {
                SelectedModel = model;
                VersionCB.Items.Clear();
                foreach (GXDeviceVersion it in model.Versions)
                {
                    VersionCB.Items.Add(it);
                }
                if (VersionCB.Items.Count != 0)
                {
                    VersionCB.SelectedIndex = 0;
                }
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
                return "Device version information";
            }
        }

        public string Description
        {
            get
            {
                return "Select device version or create new one.";
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
