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
    public class GXTransactionManager
    {
        static List<AsyncStateChangeEventHandler> Events = new List<AsyncStateChangeEventHandler>();
        /// <summary>
        /// Constructor.
        /// </summary>
        public GXTransactionManager(AsyncStateChangeEventHandler e)
        {
            Events.Add(e);
        }

        GXAsyncWork TransactionWork;

        void OnAsyncStateChange(System.Windows.Forms.Control sender, AsyncState state)
        {
            foreach (AsyncStateChangeEventHandler e in Events)
            {
                e(sender, state);
            }
        }

        /// <summary>
        /// Executes reading of the item asynchronously.
        /// </summary>
        /// <param name="item">The item to be read.</param>
        public void Read(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, ReadAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Executes writing of the item asynchronously.
        /// </summary>
        /// <param name="item">The item to be written.</param>
        public void Write(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, WriteAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Executes connecting of the item or the parent GXDevice of it asynchronously.
        /// </summary>
        /// <param name="item">The item to be connected.</param>
        public void Connect(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, ConnectAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Executes disconnecting of the item or the parent GXDevice of it asynchronously.
        /// </summary>
        /// <param name="item">The item to be disconnected.</param>
        public void Disconnect(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, DisconnectAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Starts monitoring the specified item or its parent device.
        /// </summary>
        /// <param name="item">The item to be monitored.</param>
        public void StartMonitoring(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, StartMonitoringAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Stops monitoring the specified item or its parent device.
        /// </summary>
        /// <param name="item">An item to halt monitoring on.</param>
        public void StopMonitoring(System.Windows.Forms.Control sender, object item)
        {
            TransactionWork = new GXAsyncWork(sender, OnAsyncStateChange, StopMonitoringAsync, new object[] { item });
            TransactionWork.Start();
        }

        /// <summary>
        /// Connects the specified item or its parent device.
        /// </summary>
        /// <param name="item">An item to be connected.</param>
        void ConnectAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            //If device list is selected.
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).Connect();
            }
            //If devicegroup is selected.
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).Connect();
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                ((GXDevice)item).Connect();
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                ((GXCategory)item).Device.Connect();
            }
            //If table is selected.
            else if (item is GXTable)
            {
                ((GXTable)item).Device.Connect();
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                ((GXProperty)item).Device.Connect();
            }
        }

        /// <summary>
        /// Disconnects the specified item or its parent device.
        /// </summary>
        /// <param name="item">An item to be connected.</param>
        void DisconnectAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            //If device list is selected.
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).Disconnect();
            }
            //If devicegroup is selected.
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).Disconnect();
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                ((GXDevice)item).Disconnect();
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                ((GXCategory)item).Device.Disconnect();
            }
            //If table is selected.
            else if (item is GXTable)
            {
                ((GXTable)item).Device.Disconnect();
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                ((GXProperty)item).Device.Disconnect();
            }
        }

        /// <summary>
        /// Starts monitoring the specified item or its parent device.
        /// </summary>
        /// <param name="item">An item to be monitored.</param>
        void StartMonitoringAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).StartMonitoring();
            }
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).StartMonitoring();
            }
            else if (item is GXDevice)
            {
                ((GXDevice)item).StartMonitoring();
            }
            else if (item is GXCategory)
            {
                ((GXCategory)item).Device.StartMonitoring();
            }
            else if (item is GXTable)
            {
                ((GXTable)item).Device.StartMonitoring();
            }
            else if (item is GXProperty)
            {
                ((GXProperty)item).Device.StartMonitoring();
            }
        }

        /// <summary>
        /// Stops monitoring the specified item or its parent device.
        /// </summary>
        /// <param name="item">An item to halt monitoring on.</param>
        void StopMonitoringAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).StopMonitoring();
            }
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).StopMonitoring();
            }
            else if (item is GXDevice)
            {
                ((GXDevice)item).StopMonitoring();
            }
            else if (item is GXCategory)
            {
                ((GXCategory)item).Device.StopMonitoring();
            }
            else if (item is GXTable)
            {
                ((GXTable)item).Device.StopMonitoring();
            }
            else if (item is GXProperty)
            {
                ((GXProperty)item).Device.StopMonitoring();
            }
        }

        /// <summary>
        /// Reads the specified item.
        /// </summary>
        /// <param name="item">An item to be read</param>
        void ReadAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            //If device list is selected.
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).Read();
            }
            //If device group is selected.
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).Read();
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                ((GXDevice)item).Read();
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                ((GXCategory)item).Read();
            }
            //If table is selected.
            else if (item is GXTable)
            {
                ((GXTable)item).Read();
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                ((GXProperty)item).Read();
            }
            else if (item == null)
            {
                throw new Exception("GXTransactionManager.Read failed. Target is not set.");
            }
            else
            {
                throw new Exception("GXTransactionManager.Read failed: Unknown item type: " + item.GetType().ToString());
            }
        }

        /// <summary>
        /// Writes the specified item.
        /// </summary>
        /// <param name="item">An item to be written</param>
        public void WriteAsync(object sender, object[] parameters)
        {
            object item = parameters[0];
            //If device list is selected.
            if (item is GXDeviceList)
            {
                ((GXDeviceList)item).Write();
            }
            //If device group is selected.
            else if (item is GXDeviceGroup)
            {
                ((GXDeviceGroup)item).Write();
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                ((GXDevice)item).Write();
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                ((GXCategory)item).Write();
            }
            //If table is selected.
            else if (item is GXTable)
            {
                ((GXTable)item).Write();
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                ((GXProperty)item).Write();
            }
            else
            {
                throw new Exception("GXTransactionManager.Write failed: Unknown item type: " + item.GetType().ToString());
            }
        }

        /// <summary>
        /// A boolean value for determining the connection state of an item or its parent device.
        /// </summary>
        /// <param name="item">An item to be examined.</param>
        /// <returns>The connection state of an item.</returns>
        static public bool IsConnecting(object item)
        {
            //If device list is selected.
            if (item is GXDeviceList)
            {
                return false;
            }
            //If device group is selected.
            else if (item is GXDeviceGroup)
            {
                return false;
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                return (((GXDevice)item).Status & DeviceStates.Connecting) != 0;
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                return (((GXCategory)item).Device.Status & DeviceStates.Connecting) != 0;
            }
            //If table is selected.
            else if (item is GXTable)
            {
                return (((GXTable)item).Device.Status & DeviceStates.Connecting) != 0;
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                return (((GXProperty)item).Device.Status & DeviceStates.Connecting) != 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// A boolean value for determining the connection state of an item or its parent device.
        /// </summary>
        /// <param name="item">An item to be examined.</param>
        /// <returns>The connection state of an item.</returns>
        static public bool IsConnected(object item)
        {
            //If device list is selected.
            if (item is GXDeviceList)
            {
                return false;
            }
            //If device group is selected.
            else if (item is GXDeviceGroup)
            {
                return false;
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                return (((GXDevice)item).Status & (DeviceStates.Connected | DeviceStates.MediaConnected)) != 0;
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                return (((GXCategory)item).Device.Status & (DeviceStates.Connected | DeviceStates.MediaConnected)) != 0;
            }
            //If table is selected.
            else if (item is GXTable)
            {
                return (((GXTable)item).Device.Status & (DeviceStates.Connected | DeviceStates.MediaConnected)) != 0;
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                return (((GXProperty)item).Device.Status & (DeviceStates.Connected | DeviceStates.MediaConnected)) != 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A boolean value for determining the connection state of an item or its parent device.
        /// </summary>
        /// <param name="item">An item to be examined.</param>
        /// <returns>The monitoring state of an item.</returns>
        static public bool IsMonitoring(object item)
        {
            //If device list is selected.
            if (item is GXDeviceList)
            {
                return false;
            }
            //If device group is selected.
            else if (item is GXDeviceGroup)
            {
                return false;
            }
            //If device is selected.
            else if (item is GXDevice)
            {
                return (((GXDevice)item).Status & DeviceStates.Monitoring) != 0;
            }
            //If category is selected.
            else if (item is GXCategory)
            {
                return (((GXCategory)item).Device.Status & DeviceStates.Monitoring) != 0;
            }
            //If table is selected.
            else if (item is GXTable)
            {
                return (((GXTable)item).Device.Status & DeviceStates.Monitoring) != 0;
            }
            //If property is selected.
            else if (item is GXProperty)
            {
                return (((GXProperty)item).Device.Status & DeviceStates.Monitoring) != 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find item device.
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static GXDevice GetDevice(object item)
        {
            if (item is GXDeviceList)
            {
                return null;
            }
            if (item is GXDeviceGroup)
            {
                return null;
            }
            if (item is GXDevice)
            {
                return (GXDevice)item;
            }
            if (item is GXTable)
            {
                return ((GXTable)item).Device;
            }
            if (item is GXCategory)
            {
                return ((GXCategory)item).Device;
            }
            if (item is GXProperty)
            {
                return ((GXProperty)item).Device;
            }
            return null;
        }

        public static GXDeviceCollection GetDeviceCollection(object target)
        {
            if (target is GXDeviceCollection)
            {
                return target as GXDeviceCollection;
            }
            if (target is GXDeviceGroup)
            {
                return (target as GXDeviceGroup).Devices;
            }
            if (target is GXDevice)
            {
                return (target as GXDevice).Parent;
            }
            if (target is GXCategory)
            {
                return ((target as GXCategory).Parent.Parent as GXDevice).Parent;
            }
            if (target is GXTable)
            {
                return ((target as GXTable).Parent.Parent as GXDevice).Parent;
            }
            if (target is GXProperty)
            {
                return ((target as GXProperty).Parent.Parent as GXDevice).Parent;
            }
            throw new Exception("Invalid parent.");
        }
    }
}
