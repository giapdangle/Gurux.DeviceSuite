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
using ServiceStack.OrmLite;
using GuruxAMI.Server;
using Gurux.DeviceSuite.Ami;
using GuruxAMI.Common;
using GuruxAMI.Client;
using Gurux.DeviceSuite.Director;

namespace Gurux.DeviceSuite
{
    public partial class GXOptionsForm : Form
    {
        GXAsyncWork TransactionWork;
        GXAmi AmiForm;
        public GXOptionsForm(AppType type, GXAmi ami)
        {
            InitializeComponent();
            //We do not need device editor tab at this moment.
            tabControl1.TabPages.Remove(DeviceEditorTab);
            AmiForm = ami;
            GetDevicesAutomaticallyCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.GetDevicesAutomatically;
            GetDataCollectorsAutomaticallyCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.GetDataCollectorsAutomatically;
            ShowDevicesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowDevices;
            ShowCategoriesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowCategories;
            ShowTablesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowTables;
            ShowPropertiesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowProperties;
            ShowPropertyValueCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowPropertyValue;
            AddressTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName.Replace("http://", "");            
            this.MaximimErrorCountTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount.ToString();
            this.MaximimTraceCountTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount.ToString();
            EnableAMICB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled;
            switch (type)
            {
                case AppType.Ami:
                    tabControl1.SelectedTab = AmiTab;
                break;
                case AppType.Director:
                tabControl1.SelectedTab = DirectorTab;
                break;
                case AppType.Editor:
                tabControl1.SelectedTab = DeviceEditorTab;
                break;
            }            
        }        

        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowDevices = ShowDevicesCB.Checked;
                Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowCategories = ShowCategoriesCB.Checked;
                Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowTables = ShowTablesCB.Checked;
                Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowProperties = ShowPropertiesCB.Checked;
                Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowPropertyValue = ShowPropertyValueCB.Checked;
                Gurux.DeviceSuite.Properties.Settings.Default.ErrorMaximumCount = Convert.ToInt32(this.MaximimErrorCountTB.Text);
                Gurux.DeviceSuite.Properties.Settings.Default.TraceMaximumCount = Convert.ToInt32(this.MaximimTraceCountTB.Text);
                Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled = EnableAMICB.Checked;
                if (Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled)
                {
                    //Check is DB created when host name is updated first time.
                    if (string.IsNullOrEmpty(Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName))
                    {
                        GuruxAMI.Client.GXAmiClient cl;
                        IsDBCreated(out cl);
                    }
                    Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = "http://" + AddressTB.Text;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        DialogResult OnShowQuestion2(string text)
        {
            GXAmiDBSettingsForm dlg = new GXAmiDBSettingsForm();
            return dlg.ShowDialog(this);
        }

        bool IsDBCreated(out GuruxAMI.Client.GXAmiClient cl)
        {
            if (!Gurux.DeviceSuite.Properties.Settings.Default.AmiEnabled)
            {
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = "http://" + AddressTB.Text;
                //Start AMI Server.
                AmiForm.Start(true, true);
            }
            string baseUr = "http://" + AddressTB.Text;
            if (baseUr.Contains('*'))
            {
                baseUr = baseUr.Replace("*", "localhost");
            }
            cl = new GuruxAMI.Client.GXAmiClient(baseUr, 
                Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName, 
                Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword);
            bool created = false;
            try
            {
                created = cl.IsDatabaseCreated();
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Status == System.Net.WebExceptionStatus.ConnectFailure ||
                    ex.Status == System.Net.WebExceptionStatus.ConnectionClosed)
                {
                    created = false;
                }
                else
                {
                    throw ex;
                }
            }
            if (!created)
            {
                if ((DialogResult)this.Invoke(new ShowQuestionEventHandler(OnShowQuestion), "GuruxAMI Database is not created. Do you want to create it?") != DialogResult.Yes)
                {
                    return false;
                }
                if ((DialogResult)this.Invoke(new ShowQuestionEventHandler(OnShowQuestion2), "") == DialogResult.OK)
                {
                    //Close AMI Server and start again.
                    AmiForm.ClosingApplication.Set();
                    AmiForm.ServerThreadClosed.WaitOne();

                    string connStr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseHostName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDatabaseName,
                        Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword);
                    GXAmiDataCollector dc;
                    using (IDbConnection Db = new OrmLiteConnectionFactory(connStr, true, ServiceStack.OrmLite.MySql.MySqlDialectProvider.Instance).OpenDbConnection())
                    {
                        GXDBService.CreateTables(Db, OnProgress, "gurux", "gurux");
                        dc = GXDBService.AddDataCollector(Db);
                    }
                    dc.Internal = true;
                    AmiForm.Start(true, false);
                    AmiForm.AddDataCollector(new GXAmiDataCollector[] {dc});
                    if (cl.IsDatabaseCreated())
                    {
                        this.Invoke(new ShowQuestionEventHandler(OnMessage), "Database created successfully.");
                    }
                }
            }
            return created;
        }

        void OnProgress(int index, int count)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ProgressEventHandler(OnProgress), index, count);
            }
            else
            {
                Progress.Value = index;
                if (index == 1)
                {
                    Progress.Maximum = count;
                    Progress.Visible = true;
                }
                else if (!Progress.Visible)
                {
                    Progress.Visible = true;
                }
                else if (index == 0)
                {
                    Progress.Visible = false;
                }
            }
        }

        /// <summary>
        /// Test DB connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AddressTB.Text))
                {
                    throw new Exception("GuruxAMI host name is invalid.");
                }
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = "http://" + AddressTB.Text;
                TransactionWork = new GXAsyncWork(this, OnAsyncStateChange, CreateDBAsync, null, "", null);
                TransactionWork.Start();
            }
            catch (Exception ex)
            {
                AmiForm.Start(true, true);
                GXCommon.ShowError(this, ex);
            }                
        }

        void OnAsyncStateChange(object sender, GXAsyncWork work, object[] parameters, AsyncState state, string text)
        {
            tabControl1.Enabled = CancelBtn.Enabled = OkBtn.Enabled = state != AsyncState.Start;
        }

        delegate DialogResult ShowQuestionEventHandler(string message);

        DialogResult OnShowQuestion(string message)
        {
            return GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, message);
        }

        /// <summary>
        /// Show working thread message.
        /// </summary>
        DialogResult OnMessage(string message)
        {
            MessageBox.Show(this, message, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return DialogResult.OK;
        }

        /// <summary>
        /// Create DB asynchronously.
        /// </summary>
        void CreateDBAsync(object sender, GXAsyncWork work, object[] parameters)
        {
            GXAmiClient cl;
            if (IsDBCreated(out cl))
            {
                this.Invoke(new ShowQuestionEventHandler(OnMessage), "GuruxAMI database is created and working.");
            }            
        }

        /// <summary>
        /// Show database settings.
        /// </summary>
        private void DatabaseSettingsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                GXAmiDBSettingsForm dlg = new GXAmiDBSettingsForm();
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            } 
        }

        /// <summary>
        /// Is GuruxAMI on use.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableAMICB_CheckedChanged(object sender, EventArgs e)
        {
            AMISettings.Enabled = EnableAMICB.Checked;
        }

        /// <summary>
        /// Show server settings.
        /// </summary>
        private void AddressEditBtn_Click(object sender, EventArgs e)
        {
            string address = GuruxAMI.Client.GXAmiClient.ShowServerSettings(this, AddressTB.Text);
            if (address != null)
            {
                AddressTB.Text = address.Replace("http://", "");
            }
        }
    }
}
