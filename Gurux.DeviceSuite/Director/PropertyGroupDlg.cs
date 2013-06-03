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

namespace Gurux.DeviceSuite.Director
{
    public partial class PropertyGroupDlg : Form
    {
        private DisabledAction m_DisActions;
        object Target;

        public PropertyGroupDlg(object group)
        {
            InitializeComponent();
            Target = group;
            if (Target is Gurux.Device.GXCategory)
            {
                NameTB.Text = ((Gurux.Device.GXCategory)Target).Name;
                //Add disabled actions.
                m_DisActions = new DisabledAction((Target as GXCategory).DisabledActions);
                this.Text = Gurux.DeviceSuite.Properties.Resources.CategoryPropertiesTxt;
            }
            else
            {
                NameTB.Text = (Target as GXTable).Name;
                //Add disabled actions.
                m_DisActions = new DisabledAction((Target as GXTable).DisabledActions);
                this.Text = Gurux.DeviceSuite.Properties.Resources.TablePropertiesTxt;
            }
            this.Text = Gurux.DeviceSuite.Properties.Resources.CategorySettingsTxt;
            this.GeneralTab.Text = Gurux.DeviceSuite.Properties.Resources.GeneralTxt;
            tabControl1.TabPages.Add(m_DisActions.DisabledActionsTB);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Target is GXCategory)
                {
                    GXCategory cat = Target as GXCategory;
                    cat.DisabledActions = m_DisActions.DisabledActions;
                }
                else
                {
                    GXTable table = Target as GXTable;
                    table.DisabledActions = m_DisActions.DisabledActions;
                }
                foreach (TabPage it in tabControl1.TabPages)
                {
                    if (it.Controls.Count > 0 && it.Controls[0] is Gurux.Common.IGXWizardPage)
                    {
                        ((Gurux.Common.IGXWizardPage)it.Controls[0]).Finish();
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}
