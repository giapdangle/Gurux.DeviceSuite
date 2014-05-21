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

namespace Gurux.DeviceSuite.GXWizard
{
    public partial class GXWizardDlg : Form
    {
        GXWizardDeviceSettings DeviceSettings;
        int FirstVisiblePageIndex = 0;
        int PageIndex = 0;
        List<Control> Pages = new List<Control>();
        object TargetParent;
        public object Target;
        bool ReadOnlyTarget = false;
        GXProtocolAddIn AddIn;

        public GXWizardDlg()
        {
            InitializeComponent();
        }
	
        /// <summary>
		/// Initializes a new instance of the GXWizardDlg class.
		/// </summary>
        /// <param name="target">The type of the target is either GXDevice, GXCategory, GXTable or GXProperty.</param>
        public GXWizardDlg(object target, object parent, GXProtocolAddIn addIn)
        {            
            try
            {
                AddIn = addIn;
                TargetParent = parent;
                Target = target;
                InitializeComponent();
                UpdateResources();
                UpdatePages(AddIn, TargetParent);
                ChangePage(0);
                HidePageCB.Checked = !Gurux.DeviceSuite.Properties.Settings.Default.ShowWizardStartPage;
                this.Text = Gurux.DeviceSuite.Properties.Resources.DeviceProfileWizardTitle;
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }
		
		void UpdatePages(GXProtocolAddIn addIn, object parent)
		{
            if (Target == null || Target is GXDevice)
            {
                AddDevicePagesContent(parent as GXDeviceManufacturerCollection, Target as GXDevice, Pages);                
                this.Text = Gurux.DeviceSuite.Properties.Resources.DeviceWizardTxt;
            }
            else if (Target is GXCategory)
            {
                ReadOnlyTarget = !string.IsNullOrEmpty(((GXCategory)Target).Name);
                AddSettingsPage(addIn, parent, Pages);
                addIn.ModifyWizardPages(Target, GXPropertyPageType.Category, Pages);
                this.Text = Gurux.DeviceSuite.Properties.Resources.CategoryWizardTxt;
            }
            else if (Target is GXTable)
            {
                ReadOnlyTarget = !string.IsNullOrEmpty(((GXTable)Target).Name);
                AddSettingsPage(addIn, parent, Pages);
                addIn.ModifyWizardPages(Target, GXPropertyPageType.Table, Pages);
                this.Text = Gurux.DeviceSuite.Properties.Resources.TableWizardTxt;
            }
            else if (Target is GXProperty)
            {
                ReadOnlyTarget = !string.IsNullOrEmpty(((GXProperty)Target).Name);
                AddSettingsPage(addIn, parent, Pages);
                addIn.ModifyWizardPages(Target, GXPropertyPageType.Property, Pages);
                this.Text = Gurux.DeviceSuite.Properties.Resources.PropertyWizardTxt;
            }
            foreach (Control it in Pages)
            {
                if (!(it is IGXWizardPage))
                {
                    throw new Exception(it.GetType().Name + " is not derived from IGXWizardPage.");
                }
            }
		}
		
		private void UpdateResources()
		{
            BackBtn.Text = Gurux.DeviceSuite.Properties.Resources.BackTxt;			
            CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
		}

        private void AddSettingsPage(GXProtocolAddIn addIn, object parent, List<Control> pages)
		{
            pages.Add(new GXWizardPropertySettingsPage(addIn, parent));            
		}

        private void AddDevicePagesContent(GXDeviceManufacturerCollection manufacturers, GXDevice device, List<Control> pages)
		{
            //If new device is created.
            //If device is modified this is skipped.
            if (DeviceSettings == null)
            {
                pages.Add(new GXWizardStartPage());
                DeviceSettings = new GXWizardDeviceSettings();
                pages.Add(DeviceSettings);
            }            
            pages.Add(new GXWizardProtocolSettings());
            pages.Add(new GXWizardFinishPage());
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
                //Update custom pages.
                if (w is GXWizardDeviceSettings && Target is GXDevice && DeviceSettings != null)
                {
                    GXDevice dev = Target as GXDevice;
                    if (Pages[0] is GXWizardStartPage)
                    {
                        Pages.RemoveAt(0);
                        --PageIndex;
                    }
                    dev.AddIn.ModifyWizardPages(Target, GXPropertyPageType.Device, Pages);                   
                }
                if (PageIndex == Pages.Count - 1)
                {
                    Gurux.DeviceSuite.Properties.Settings.Default.ShowWizardStartPage = !HidePageCB.Checked;
                    foreach (IGXWizardPage it in Pages)
                    {
                        it.Finish();
                    }
                    if (Target is GXProperty)
                    {
                        GXProperty p = Target as GXProperty;
                        //If we are adding new item...
                        if (!ReadOnlyTarget)
                        {                            
                            GXPropertyCollection properties = ((GXCategory)TargetParent).Properties;                            
                            properties.Add(p);
                        }
                    }
                    else if (Target is GXTable)
                    {
                        GXTable t = Target as GXTable;
                        //If we are adding new item...
                        if (!ReadOnlyTarget)
                        {
                            ((GXDevice)TargetParent).Tables.Add(t);
                        }
                    }
                    else if (Target is GXCategory)
                    {
                        GXCategory c = Target as GXCategory;
                        //If we are adding new item...
                        if (!ReadOnlyTarget)
                        {
                            ((GXDevice)TargetParent).Categories.Add(c);
                        }
                    }                    
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
            bool show;
            do
            {
                PageIndex += add;
                w = Pages[PageIndex] as IGXWizardPage;
                w.Target = Target;
                show = w.IsShown();
                if (!show && add == 0)
                {
                    //If device settings are not show show custom settings.
                    //This is happening on edit.
                    if (w is GXWizardDeviceSettings && Target is GXDevice && DeviceSettings != null)
                    {
                        ChangePage(1);
                        return;
                    }
                    ++PageIndex;
                    FirstVisiblePageIndex = PageIndex;                    
                }
            }
            while (!show && PageIndex > -1 && PageIndex < Pages.Count);


            if (PageIndex > -1 && PageIndex < Pages.Count)
            {
                Frame.Tag = w;
                CaptionLbl.Text = w.Caption;
                DescriptionLbl.Text = w.Description;
                w.Target = Target;
                w.Initialize();
                while (Pages[PageIndex].Controls.Count != 0)
                {
                    Control ctrl = Pages[PageIndex].Controls[0];
                    Frame.Controls.Add(ctrl);
                    if (ctrl.TabIndex == 0)
                    {
                        ctrl.Select();
                    }
                }
            }
            //Reset device settings if coming back to first page.
            if (DeviceSettings != null && PageIndex == 0 && add == -1)
            {
                Pages.Clear();
                Pages.Add(DeviceSettings);
                UpdatePages(AddIn, TargetParent);                
            }
            HidePageCB.Visible = w is GXWizardStartPage;
            GXWizardButtons buttons = w.EnabledButtons;
            NextBtn.Enabled = ((buttons & GXWizardButtons.Next) != 0 || (buttons & GXWizardButtons.Finish) != 0);
            BackBtn.Enabled = (buttons & GXWizardButtons.Back) != 0 && FirstVisiblePageIndex < PageIndex;
            CancelBtn.Enabled = (buttons & GXWizardButtons.Cancel) != 0;
            NextBtn.Text = PageIndex < Pages.Count - 1 ? Gurux.DeviceSuite.Properties.Resources.NextTxt : Gurux.DeviceSuite.Properties.Resources.FinishTxt;
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
