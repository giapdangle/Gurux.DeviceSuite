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
using GuruxAMI.Common;
using GuruxAMI.Client;
using System.Net;

namespace Gurux.DeviceSuite.Ami
{
    /// <summary>
    /// What action we are doing.
    /// </summary>
    internal enum DataCollectorActionType
    {        
        Add,
        Edit,
        Bind,
        Unbind
    }

    partial class GXAmiDataCollectorForm : Form
    {
        GXAmiDataCollector Collector;
        GXAmiClient Client;
        GXAsyncWork TransactionWork;
        DataCollectorActionType Action;
        public GXAmiDataCollectorForm(GXAmiClient client, GXAmiDataCollector collector, DataCollectorActionType action)
        {
            Action = action;
            Client = client;
            InitializeComponent();
            Collector = collector;
            RefreshBtn.Text = Gurux.DeviceSuite.Properties.Resources.RefreshTxt;
            RefreshBtn.Enabled = action != DataCollectorActionType.Add;
            if (collector != null)
            {
                this.NameTB.Text = Collector.Name;
                this.IPAddressTB.Text = Collector.IP;
                this.DescriptionTB.Text = Collector.Description;
                if (Collector.Guid != Guid.Empty)
                {
                    this.GuidTB.Text = Collector.Guid.ToString();
                }
                if (Collector.LastRequestTimeStamp.HasValue)
                {
                    LastConnectedTB.Text = Collector.LastRequestTimeStamp.Value.ToString();
                }
                InternalCB.Checked = collector.Internal;
            }            
        }

        /// <summary>
        /// Accpept changes for DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTB.Text.Trim().Length == 0)
                {
                    NameTB.Focus();
                    throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
                }               
                Collector.Name = this.NameTB.Text;
                Collector.Description = this.DescriptionTB.Text;
                Collector.Internal = InternalCB.Checked;
                if (Action == DataCollectorActionType.Add)
                {
                    Client.AddDataCollector(Collector, Client.GetUserGroups(false));
                }
                else if (Action == DataCollectorActionType.Edit)
                {
                    Client.Update(Collector);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Change Refresh button text when read is started.
        /// </summary>
        /// <param name="work"></param>
        /// <param name="sender"></param>
        /// <param name="parameters"></param>
        /// <param name="state"></param>
        /// <param name="text"></param>
        void OnAsyncStateChange(object sender, GXAsyncWork work, object[] parameters, AsyncState state, string text)
        {
            if (state == AsyncState.Start)
            {
                RefreshBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
            }
            else
            {
                RefreshBtn.Text = Gurux.DeviceSuite.Properties.Resources.RefreshTxt;
                DateTime? tm = work.Result as DateTime?;
                Collector.LastRequestTimeStamp = tm;
                if (tm.HasValue)
                {
                    LastConnectedTB.Text = tm.Value.ToString();
                }
                else
                {
                    LastConnectedTB.Text = "";
                }                
            }
        }

        void GetLastRequestTimeStampAsync(object sender, GXAsyncWork work, object[] parameters)
        {
            GXAmiClient cl = parameters[0] as GXAmiClient;
            GXAmiDataCollector dc = parameters[1] as GXAmiDataCollector;
            work.Result = cl.GetDataCollectorByGuid(dc.Guid).LastRequestTimeStamp;            
        }

        /// <summary>
        /// Refresh last access time of DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionWork == null)
                {
                    TransactionWork = new GXAsyncWork(this, OnAsyncStateChange, GetLastRequestTimeStampAsync, null, "", new object[] { Client, Collector });
                }
                //Is work running.
                if (!TransactionWork.IsRunning)
                {
                    TransactionWork.Start();
                }
                else //Wait until work ends.
                {
                    TransactionWork.Wait(0);                    
                }
            }             
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);             
            }
        }
    }
}
