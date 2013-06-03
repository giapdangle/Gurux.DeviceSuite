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
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Manufacturer
{
    public partial class GXDeviceVersionForm : Form
    {
        GXDeviceVersion Version;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version"></param>
        public GXDeviceVersionForm(GXDeviceVersion version)
        {
            Version = version;
            InitializeComponent();
            NameTB.Text = Version.Name;
            DescriptionTB.Text = Version.Description;
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTB.Text.Trim().Length == 0)
                {
                    NameTB.Focus();
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
                }
                Version.Name = NameTB.Text;
                Version.Description = DescriptionTB.Text;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
