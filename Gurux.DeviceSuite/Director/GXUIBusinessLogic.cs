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
using System.Linq;
using System.Text;
using Gurux.Device;

namespace Gurux.DeviceSuite.Director
{
    class GXUIBusinessLogic : IGXDataHandler
    {
        GXDeviceList m_DeviceList;

        #region IGXDataHandler Members

        void IGXDataHandler.Load(GXDeviceList list)
        {
            m_DeviceList = list;
            m_DeviceList.OnUpdated += new ItemUpdatedEventHandler(OnUpdated);
        }

        void IGXDataHandler.Unload(GXDeviceList list)
        {
            m_DeviceList = null;
        }

        string IGXDataHandler.DisplayName
        {
            get
            {
                return "Default";
            }            
        }

        void IGXDataHandler.Changing(object target)
        {
            throw new NotImplementedException();
        }

        void IGXDataHandler.Changed(object target)
        {
            throw new NotImplementedException();
        }

        private void Updated(object sender, GXItemEventArgs e)
        {
            if (e.Item is GXDeviceList)
            {
                GXDeviceList list = e.Item as GXDeviceList;
                TreeNode node = DeviceToTreeNode[list] as TreeNode;
                node.Text = list.Name;
                return;
            }
            if (e.Item is GXCategory)
            {
                GXCategory cat = e.Item as GXCategory;
                TreeNode node = DeviceToTreeNode[cat] as TreeNode;
                if (node != null)
                {
                    node.Text = cat.DisplayName;
                }
                GXCategoryEventArgs t = e as GXCategoryEventArgs;
                //Ignore if category read or write is started or ended.
                if ((t.Status & (CategoryStates.ReadStart | CategoryStates.ReadEnd | CategoryStates.WriteStart | CategoryStates.WriteEnd)) != 0)
                {
                    return;
                }
            }
            if (e.Item is GXTable)
            {
                GXTableEventArgs t = e as GXTableEventArgs;
                //Ignore if table read or write is started or ended.
                if ((t.Status & (TableStates.ReadStart | TableStates.ReadEnd | TableStates.WriteStart | TableStates.WriteEnd)) != 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(UpdatedTableData), new object[] { sender, e });
                }
                else
                {
                    UpdatedTableData(sender, e);
                }
                return;
            }
            if (e.Item is GXDeviceGroup)
            {
                GXDeviceGroup group = e.Item as GXDeviceGroup;
                TreeNode node = DeviceToTreeNode[group] as TreeNode;
                node.Text = group.Name;
                return;
            }
            if (e.Item is GXProperty)
            {
                GXPropertyEventArgs t = e as GXPropertyEventArgs;
                //Notify if property read or write is started or ended.
                if ((t.Status & (PropertyStates.ReadStart | PropertyStates.ReadEnd | PropertyStates.WriteStart | PropertyStates.WriteEnd)) == 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(PropertyChanged), new object[] { sender, e });
                }
                else
                {
                    PropertyChanged(sender, e);
                }
            }
            if (e.Item is GXDevice)
            {
                GXDeviceEventArgs t = e as GXDeviceEventArgs;
                if ((t.Status & (DeviceStates.Connecting | DeviceStates.Disconnecting | DeviceStates.Disconnecting | DeviceStates.ReadStart | DeviceStates.ReadEnd | DeviceStates.WriteStart | DeviceStates.WriteEnd)) != 0)
                {
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new ItemUpdatedEventHandler(DeviceStateChange), new object[] { sender, e });
                }
                else
                {
                    DeviceStateChange(sender, e);
                }
                return;
            }
            if (e.Item is GXSchedule)
            {
                UpdateScheduleItem(sender, e);
                return;
            }
        }

        private void OnUpdated(object sender, GXItemEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ItemUpdatedEventHandler(Updated), new object[] { sender, e });
            }
            else
            {
                Updated(sender, e);
            }
        }
        #endregion
    }
}
