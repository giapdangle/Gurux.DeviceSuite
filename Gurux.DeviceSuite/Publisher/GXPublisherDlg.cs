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
using Gurux.Device.Editor;
using Gurux.Device;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.Device.PresetDevices;
using Gurux.DeviceSuite.Common;
using Gurux.Device.Publisher;
using ServiceStack.ServiceClient.Web;

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXPublisherDlg : Form
    {
        GXDeviceManufacturerCollection Manufacturers;
        object Target;
        int PageIndex = 0;
        List<Control> Pages = new List<Control>();
        GXPublisher Item;
        internal JsonServiceClient Client;

        public GXPublisherDlg(GXDeviceManufacturerCollection manufacturers)
        {
            Client = new JsonServiceClient(Gurux.DeviceSuite.Properties.Settings.Default.UpdateServer);
            Client.AlwaysSendBasicAuthHeader = true;
            Manufacturers = manufacturers;
            Target = manufacturers;
            InitializeComponent();
            Bitmap bm = PublisherImage.BackgroundImage as Bitmap;
            bm.MakeTransparent();
            PublisherImage.BackgroundImage = bm;

            Item = new GXPublisher();
            Pages.Add(new GXPublishStartPage());
            Pages.Add(new GXPublisherPage(this, Item));
            Pages.Add(new GXManufacturerPage(Item));
            Pages.Add(new GXModelPage(Item));
            Pages.Add(new GXVersionPage(Item));
            Pages.Add(new GXTemplatePage(Item));
            Pages.Add(new GXPublishing(this, Item));            
            Pages.Add(new GXPublishFinishPage());
            ChangePage(0);
        }
			
		private void UpdateResources()
		{
            BackBtn.Text = Gurux.DeviceSuite.Properties.Resources.BackTxt;			
            CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
		}

		private void CancelBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
                foreach (IGXWizardPage it in Pages)
                {
                    it.Cancel();
                }
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(this, Ex);
			}
			this.Close();
		}

        void ChangePage(int add)
        {            
            IGXWizardPage w = Pages[PageIndex] as IGXWizardPage;
            if (add == 1)
            {
                w.Next();
                Target = w.Target;
                if (PageIndex == Pages.Count - 1)
                {
                    foreach (IGXWizardPage it in Pages)
                    {
                        it.Finish();
                    }
                    GXDeviceManufacturer man = Manufacturers[0];
                    GXDeviceModel model = man.Models[0];
                    GXDeviceVersion dv = model.Versions[0];
                    GXDeviceProfile dt = dv.Templates[0];
                    GXPublishedDeviceProfile type = Manufacturers.Find(man.Name, model.Name, dv.Name, dt.PresetName) as GXPublishedDeviceProfile; 
                    Close();
                    DialogResult = DialogResult.OK;
                    return;
                }
            }
            else if (add == -1)
            {
                w.Back();
            }
            Control tmp = Frame.Tag as Control;
            if (tmp != null)
            {
                while (Frame.Controls.Count != 0)
                {
                    tmp.Controls.Add(Frame.Controls[0]);
                }
            }
            do
            {
                PageIndex += add;
                w = Pages[PageIndex] as IGXWizardPage;
            }
            while (!w.IsShown() && PageIndex > -1 && PageIndex < Pages.Count );
            if (PageIndex > -1 && PageIndex < Pages.Count)
            {
                Frame.Tag = w;
                CaptionLbl.Text = w.Caption;
                DescriptionLbl.Text = w.Description;
                w.Target = Target;
                while (Pages[PageIndex].Controls.Count != 0)
                {
                    Frame.Controls.Add(Pages[PageIndex].Controls[0]);
                }
                Frame.Controls.Add(PublisherImage);
                PublisherImage.Visible = true;
            }            
            GXWizardButtons buttons = w.EnabledButtons;
            NextBtn.Enabled = ((buttons & GXWizardButtons.Next) != 0 || (buttons & GXWizardButtons.Finish) != 0);
            BackBtn.Enabled = (buttons & GXWizardButtons.Back) != 0 && PageIndex > 0;
            CancelBtn.Enabled = (buttons & GXWizardButtons.Cancel) != 0;
            NextBtn.Text = PageIndex < Pages.Count - 1 ? Gurux.DeviceSuite.Properties.Resources.NextTxt : Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            if (PageIndex > -1 && PageIndex < Pages.Count)
            {
                w.Initialize();
            }

        }

		private void NextBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
                ChangePage(1);			
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(this, Ex);
                if (Pages[PageIndex] is GXPublishing)
                {
                    PageIndex = 0;
                    ChangePage(1);
                }
			}
		}

		private void BackBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
                ChangePage(-1);
			}
			catch (Exception Ex)
			{
				GXCommon.ShowError(this, Ex);
			}
		}
    }
}
