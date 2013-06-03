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

namespace Gurux.DeviceSuite.Director
{
    public partial class DisabledAction : Form
    {
        /// <summary>
        /// Disabled actions.
        /// </summary>
        public DisabledActions DisabledActions;

        /// <summary>
        /// Initializes a new instance of the DisabledAction class.
        /// </summary>
        /// <param name="disActions">DisabledActions bitmask value.</param>
        public DisabledAction(DisabledActions disabledActions)
        {
            InitializeComponent();
            ReadCb.Text = Gurux.DeviceSuite.Properties.Resources.ReadTxt;
            WriteCb.Text = Gurux.DeviceSuite.Properties.Resources.WriteTxt;
            MonitorCB.Text = Gurux.DeviceSuite.Properties.Resources.MonitorTxt;
            ScheduleCB.Text = Gurux.DeviceSuite.Properties.Resources.ScheduleTxt;
            DisabledActionsTB.Text = Gurux.DeviceSuite.Properties.Resources.AdvancedTxt;
            //Update helps from the resources.
            this.helpProvider1.SetHelpString(this.ReadCb, Gurux.DeviceSuite.Properties.Resources.DisableReadText);
            this.helpProvider1.SetHelpString(this.WriteCb, Gurux.DeviceSuite.Properties.Resources.DisableWriteText);
            this.helpProvider1.SetHelpString(this.MonitorCB, Gurux.DeviceSuite.Properties.Resources.DisableMonitorText);
            this.helpProvider1.SetHelpString(this.ScheduleCB, Gurux.DeviceSuite.Properties.Resources.DisableScheduleText);
            //Notifications will update this.
            DisabledActions = 0;
            ReadCb.Checked = (disabledActions & DisabledActions.Read) != 0;
            WriteCb.Checked = (disabledActions & DisabledActions.Write) != 0;
            MonitorCB.Checked = (disabledActions & DisabledActions.Monitor) != 0;
            ScheduleCB.Checked = (disabledActions & DisabledActions.Schedule) != 0;
        }

        /// <summary>
        /// Toggle value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void ReadCb_CheckedChanged(object sender, EventArgs e)
        {
            DisabledActions ^= DisabledActions.Read;
        }

        /// <summary>
        /// Toggle value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void WriteCb_CheckedChanged(object sender, EventArgs e)
        {
            DisabledActions ^= DisabledActions.Write;
        }

        /// <summary>
        /// Toggle value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void MonitorCB_CheckedChanged(object sender, EventArgs e)
        {
            DisabledActions ^= DisabledActions.Monitor;
        }

        /// <summary>
        /// Toggle value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void ScheduleCB_CheckedChanged(object sender, EventArgs e)
        {
            DisabledActions ^= DisabledActions.Schedule;
        }
    }
}
