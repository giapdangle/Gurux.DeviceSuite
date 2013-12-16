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
        Gurux.DeviceSuite.Director.GXAsyncWork TransactionWork;
        GXAmi AmiForm;
        public GXOptionsForm(AppType type, GXAmi ami)
        {
            InitializeComponent();
            //We do not need device editor tab at this moment.
            tabControl1.TabPages.Remove(DeviceEditorTab);
            AmiForm = ami;
            ShowDevicesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowDevices;
            ShowCategoriesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowCategories;
            ShowTablesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowTables;
            ShowPropertiesCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowProperties;
            ShowPropertyValueCB.Checked = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeShowPropertyValue;
            HostTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
            PortTB.Text = Gurux.DeviceSuite.Properties.Settings.Default.AmiPort;
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
                    Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = HostTB.Text;
                    Gurux.DeviceSuite.Properties.Settings.Default.AmiPort = PortTB.Text;
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
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = HostTB.Text;
                //Start AMI Server.
                AmiForm.Start(true);
            }
            string host = HostTB.Text;
            string baseUr = host;
            if (host == "*")
            {
                host = "localhost";
            }
            
            if (host.StartsWith("http://"))
            {
                baseUr = host;
            }
            else
            {
                baseUr = "http://" + host + ":" + PortTB.Text + "/";            
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
                        //Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName, Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword
                        dc = GXDBService.AddDataCollector(Db);
                    }
                    AmiForm.Start(true);
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
            string host = Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName;
            string port = Gurux.DeviceSuite.Properties.Settings.Default.AmiPort;
            string user = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName;
            string pw = Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword;
            try
            {
                if (string.IsNullOrEmpty(HostTB.Text))
                {
                    throw new Exception("GuruxAMI host name is invalid.");
                }
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = HostTB.Text;
                int value;
                if (!int.TryParse(PortTB.Text, out value) || value < 1)
                {
                    throw new Exception("GuruxAMI port is invalid.");
                }
                Gurux.DeviceSuite.Properties.Settings.Default.AmiPort = PortTB.Text;
                TransactionWork = new Gurux.DeviceSuite.Director.GXAsyncWork(this, OnAsyncStateChange, CreateDBAsync, "", null);
                TransactionWork.Start();
            }
            catch (Exception ex)
            {
                AmiForm.Start(true);
                GXCommon.ShowError(this, ex);
            }                
            finally //Restore values after test.
            {
                Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName = host;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiPort = port;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBUserName = user;
                Gurux.DeviceSuite.Properties.Settings.Default.AmiDBPassword = pw;
            }
        }

        void OnAsyncStateChange(System.Windows.Forms.Control sender, AsyncState state, string text)
        {
            tabControl1.Enabled = CancelBtn.Enabled = OkBtn.Enabled = state != AsyncState.Start;
        }

        delegate DialogResult ShowQuestionEventHandler(string message);

        DialogResult OnShowQuestion(string message)
        {
            return GXCommon.ShowQuestion(this, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, message);
        }

        DialogResult OnMessage(string message)
        {
            MessageBox.Show(this, message, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return DialogResult.OK;
        }

        void CreateDBAsync(object sender, object[] parameters)
        {
            GXAmiClient cl;
            if (IsDBCreated(out cl))
            {
                GXAmiDataCollector[] collectors = cl.GetDataCollectorsByMacAdderss(BitConverter.ToString(GuruxAMI.Client.GXAmiClient.GetMACAddress()).Replace('-', ':'));
                if (collectors.Length == 0)
                {
                    if ((DialogResult)this.Invoke(new ShowQuestionEventHandler(OnShowQuestion), Gurux.DeviceSuite.Properties.Resources.NoDataCollectorTxt) == DialogResult.Yes)
                    {
                        GXAmiDataCollector dc = cl.AddDataCollector(GXAmiClient.GetMACAddressAsString());
                        cl.AddDataCollector(dc, cl.GetUserGroups(false));
                        AmiForm.AddDataCollector(new GXAmiDataCollector[] { dc });
                    }
                }
                else
                {
                    this.Invoke(new ShowQuestionEventHandler(OnMessage), "GuruxAMI database is created and working.");
                }
            }
        }


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

        private void EnableAMICB_CheckedChanged(object sender, EventArgs e)
        {
            AMISettings.Enabled = EnableAMICB.Checked;
        }
    }
}
