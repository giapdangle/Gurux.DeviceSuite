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
using Gurux.Common;

namespace Gurux.DeviceSuite.Director
{
    public partial class GXDeviceGroupDialog : Form
    {
        private DisabledAction m_DisActions;
        GXDeviceGroup Target;
        GXDeviceGroupCollection ParentCollection;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="target"></param>
        public GXDeviceGroupDialog(GXDeviceGroup group, object target)
        {
            InitializeComponent();
            FindParentGroups(target);
            Target = group;
            NameTB.Text = group.Name;
            this.GeneralTab.Text = Gurux.DeviceSuite.Properties.Resources.GeneralTxt;
            this.Text = Gurux.DeviceSuite.Properties.Resources.DeviceGroupSettingsTxt;
            //Update help strings from the resource.
            this.helpProvider1.SetHelpString(this.NameTB, Gurux.DeviceSuite.Properties.Resources.DeviceGroupNameHelp);
            this.helpProvider1.SetHelpString(this.btnOK, Gurux.DeviceSuite.Properties.Resources.OKHelp);
            this.helpProvider1.SetHelpString(this.btnCancel, Gurux.DeviceSuite.Properties.Resources.CancelHelp);
            //Add disabled actions.
            m_DisActions = new DisabledAction(Target.DisabledActions);
            tabControl1.TabPages.Add(m_DisActions.DisabledActionsTB);
        }

        void FindParentGroups(object target)
        {
            //Find selected device groups			
            if (target is GXDeviceList)
            {
                ParentCollection = ((GXDeviceList)target).DeviceGroups;
            }
            else if (target is GXDeviceGroupCollection)
            {
                ParentCollection = (GXDeviceGroupCollection)target;
            }
            else if (target is GXDeviceGroup)
            {
                ParentCollection = ((GXDeviceGroup)target).DeviceGroups;
            }
            else if (target is GXDevice)
            {
                ParentCollection = ((GXDeviceGroup)((GXDevice)target).Parent.Parent).Parent;
            }
            else if (target is GXTable)
            {
                ParentCollection = ((GXDeviceGroup)((GXTable)target).Device.Parent.Parent).Parent;
            }
            else if (target is GXCategory)
            {
                ParentCollection = ((GXDeviceGroup)((GXCategory)target).Device.Parent.Parent).Parent;
            }
            else if (target is GXProperty)
            {
                ParentCollection = ((GXDeviceGroup)((GXProperty)target).Device.Parent.Parent).Parent;
            }
            else
            {
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrUnknownNodeTxt);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTB.Text.Trim().Length == 0)
                {
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
                }
                Target.Name = NameTB.Text.Trim();
                //Add new group.
                if (Target.Parent == null)
                {
                    ParentCollection.Add(Target);
                }
                Target.DisabledActions = m_DisActions.DisabledActions;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
