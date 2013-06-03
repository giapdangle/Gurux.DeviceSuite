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

namespace Gurux.DeviceSuite.Import
{
    public partial class GXImportPropertiesForm : Form, IGXWizardPage
    {
        GXDevice Device;
        public GXImportPropertiesForm()
        {
            InitializeComponent();
            DeselectAllBtn.Text = Gurux.DeviceSuite.Properties.Resources.DeselectAllTxt;
            SelectAllBtn.Text = Gurux.DeviceSuite.Properties.Resources.SelectAllTxt;
            NameClm.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
        }

        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem it in listView1.Items)
                {
                    it.Checked = true;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        private void DeselectAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem it in listView1.Items)
                {
                    it.Checked = false;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        #region IGXWizardPage Members

        bool IGXWizardPage.IsShown()
        {
            return true;
        }

        void IGXWizardPage.Next()
        {
        }

        void IGXWizardPage.Back()
        {
        }

        void IGXWizardPage.Finish()
        {
        }

        void IGXWizardPage.Cancel()
        {
        }

        object IGXWizardPage.Target
        {
            get;
            set;
        }

        void IGXWizardPage.Initialize()
        {
            Device = ((IGXWizardPage)this).Target as GXDevice;
            listView1.Items.Clear();
            foreach (GXCategory cat in Device.Categories)
            {
                foreach (GXProperty it in cat.Properties)
                {
                    ListViewItem item = listView1.Items.Add(it.Name);
                    item.Tag = it;
                    item.Checked = true;
                }
            }
            foreach (GXTable table in Device.Tables)
            {
                ListViewItem item = listView1.Items.Add(table.Name);
                item.Tag = table;
                item.Checked = true;
            }
        }

        string IGXWizardPage.Caption
        {
            get
            {
                return Gurux.DeviceSuite.Properties.Resources.DeviceImportTxt;
            }
        }

        string IGXWizardPage.Description
        {
            get
            {
                return Gurux.DeviceSuite.Properties.Resources.ChoosePropertiesTxt;
            }
        }

        GXWizardButtons IGXWizardPage.EnabledButtons
        {
            get
            {
                return GXWizardButtons.All;
            }
        }

        #endregion
    }
}
