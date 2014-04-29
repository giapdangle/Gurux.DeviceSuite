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
using System.Windows.Forms;
using Gurux.Common;
using Gurux.Device.PresetDevices;
using Gurux.Device.Publisher;
using ServiceStack.ServiceClient.Web;

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXPublishing : Form, IGXWizardPage
    {
        GXPublisher Item;
        GXPublisherDlg ParentDlg;
        public GXPublishing(GXPublisherDlg parent, GXPublisher item)
        {
            Item = item;
            ParentDlg = parent;
            InitializeComponent();
        }

        #region IGXWizardPage Members

        bool IGXWizardPage.IsShown()
        {
            return true;
        }

        void IGXWizardPage.Next()
        {
        }

        void IGXWizardPage.Back()
        {
        }

        void IGXWizardPage.Finish()
        {
        }

        void IGXWizardPage.Cancel()
        {
        }

        void IGXWizardPage.Initialize()
        {
            try
            {
                ParentDlg.NextBtn.Enabled = false;
                timer1.Enabled = true;
                ParentDlg.Client.Post(Item);
                ParentDlg.NextBtn.Enabled = true;
                ParentDlg.NextBtn.PerformClick();
            }
            finally
            {
                timer1.Enabled = false;
            }
        }

        GXWizardButtons IGXWizardPage.EnabledButtons
        {
            get
            {
                return GXWizardButtons.Back | GXWizardButtons.Cancel | GXWizardButtons.Next;
            }
        }

        string IGXWizardPage.Caption
        {
            get 
            {
                return "Publishing device template.";
            }
        }

        string IGXWizardPage.Description
        {
            get
            {
                return "Device template Publishing is in progress.";
            }
        }

        object IGXWizardPage.Target
        {
            get;
            set;
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            progressBar1.PerformStep();
        }
    }
}
