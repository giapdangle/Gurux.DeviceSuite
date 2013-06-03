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
using Gurux.Device.Editor;
using Gurux.Device;
using Gurux.Common;

namespace Gurux.DeviceSuite.Import
{
    /// <summary>
	/// Implements threading functionality for GXAddIn search functions.
	/// </summary>
    internal class GXThread
    {
        public GXImportDlg m_Parent = null;
        public GXProtocolAddIn GXAddIn = null;
        public GXDevice Device = null;
        public List<string> FileNames = null;

        public GXThread(GXImportDlg parent)
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Search available devices.
        /// </summary>
        public void SearchThread()
        {

            try
            {
                if (m_Parent.Start.DeviceRB.Checked)
                {
                    Device.PacketHandler = null;
                    Device.GXClient.PacketParser = null;
                    GXAddIn.ImportFromDevice(m_Parent.m_ProtocolCustomPages.ToArray(), Device, m_Parent.MediaSettings.SelectedMedia);
                }
                else
                {
                    GXAddIn.ImportFromFile(Device, FileNames);
                }
                m_Parent.SearchFinished(true);
            }
            catch (Exception Ex)
            {
                m_Parent.SearchFinished(false);
                //Don't show errors if thread is closed.
                if (!m_Parent.m_IsClosing)
                {
                    GXCommon.ShowError(Ex);
                }
            }
        }
    }
}
