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

namespace Gurux.DeviceSuite
{
    public partial class GXAmiDBSettingsForm : Form
    {
        public GXAmiDBSettingsForm()
        {
            InitializeComponent();
            DBHostTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName;
            DBNameTb.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName;
            TablePrefixTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix;
            DBUserNameTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName;
            DBPasswordTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(DBHostTB.Text))
                {
                    throw new Exception("Database host name is invalid.");
                }                
                if (string.IsNullOrEmpty(DBNameTb.Text))
                {
                    throw new Exception("Database name is invalid.");
                }
                if (string.IsNullOrEmpty(DBUserNameTB.Text))
                {
                    throw new Exception("Database user name is invalid.");
                }
                if (string.IsNullOrEmpty(DBPasswordTB.Text))
                {
                    throw new Exception("Database password is invalid.");
                }
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName = DBHostTB.Text;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName = DBNameTb.Text;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseTablePrefix = TablePrefixTB.Text;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName = DBUserNameTB.Text;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword = DBPasswordTB.Text;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                DialogResult = DialogResult.None;
            }  
        }
    }
}
