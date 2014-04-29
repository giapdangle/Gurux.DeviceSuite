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
using Gurux.Device.Publisher;

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXPublisherPage : Form, IGXWizardPage
    {
        /// <summary>
        /// Published item.
        /// </summary>
        GXPublisher Item;
        GXPublisherDlg ParentDlg;

        public GXPublisherPage(GXPublisherDlg parent, GXPublisher item)
        {
            ParentDlg = parent;
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
            if (NameTb.Text.Trim().Length == 0)
            {
                NameTb.Focus();
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
            }
            string pw;
            if (RememberMeCB.Checked)
            {
                Gurux.DeviceSuite.Properties.Settings.Default.UserName = NameTb.Text;
                //If password is changed count the new one.
                if (PasswordTB.Text != Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt)
                {
                    pw = CryptHelper.GetCryptedPassword(NameTb.Text, PasswordTB.Text);
                    Gurux.DeviceSuite.Properties.Settings.Default.Password = pw;
                }
                else
                {
                    pw = Gurux.DeviceSuite.Properties.Settings.Default.Password;
                }
            }
            else
            {
                pw = CryptHelper.GetCryptedPassword(NameTb.Text, PasswordTB.Text);
                Gurux.DeviceSuite.Properties.Settings.Default.Password = null;
                Gurux.DeviceSuite.Properties.Settings.Default.UserName = null;
            }

            ParentDlg.Client.SetCredentials(NameTb.Text, pw);
            Item.Anynomous = AnonymousCB.Checked;
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
            NameTb.Focus();
            string username = Gurux.DeviceSuite.Properties.Settings.Default.UserName;
            if (!string.IsNullOrEmpty(username))
            {
                NameTb.Text = username;
                PasswordTB.Text = Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt;
                RememberMeCB.Checked = true;
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
                return "Device template publisher information";
            }
        }

        public string Description
        {
            get
            {
                return "Fill your Gurux Community access name and password.";
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
