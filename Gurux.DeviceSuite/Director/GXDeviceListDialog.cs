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
    public partial class GXDeviceListDialog : Form
    {
        DisabledAction m_DisActions;
        GXDeviceList m_List = null;

        /// <summary>
		/// Initializes a new instance of the DeviceListDialog class.
		/// </summary>
		/// <param name="resources">The resource manager object.</param>
		/// <param name="list">The device list object.</param>
        public GXDeviceListDialog(GXDeviceList list)
		{			
			m_List = list;
			InitializeComponent();

			CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
            OKBtn.Text = Gurux.DeviceSuite.Properties.Resources.OKTxt;
			label1.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
            this.Text = Gurux.DeviceSuite.Properties.Resources.DeviceListSettingsTxt;
			this.GeneralTab.Text = Gurux.DeviceSuite.Properties.Resources.GeneralTxt;
            NameTB.Text = list.Name;
			//Add disabled actions.
			m_DisActions = new DisabledAction(m_List.DisabledActions);
			tabControl1.TabPages.Add(m_DisActions.DisabledActionsTB);
		}

		/// <summary>
		/// Accepted changes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		private void OKBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (NameTB.Text.Trim().Length == 0)
				{
					throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrDevListNameEmptyTxt);
				}
				m_List.Name = NameTB.Text;
				m_List.DisabledActions = m_DisActions.DisabledActions;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(Ex);
				this.DialogResult = DialogResult.None;
			}
		}

		/// <summary>
		/// Show help not available message.
		/// </summary>
		/// <param name="hevent">A HelpEventArgs that contains the event data.</param>
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the control where the user clicked
			Control ctl = this.GetChildAtPoint(this.PointToClient(hevent.MousePos));
			string str = Gurux.DeviceSuite.Properties.Resources.HelpNotAvailable;
			// Show as a Help pop-up
			if (str != "")
			{
				Help.ShowPopup(ctl, str, hevent.MousePos);
			}
			// Set flag to show that the Help event as been handled
			hevent.Handled = true;
		}
	}
}
