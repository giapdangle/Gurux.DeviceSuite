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
using Gurux.Device.Editor;
using Gurux.Common;
using Gurux.Device;
using System.Globalization;

namespace Gurux.DeviceSuite.GXWizard
{
    public partial class GXWizardPropertySettingsPage : Form, IGXWizardPage
    {
        bool ReadOnlyTarget = false;
        GXProtocolAddIn AddIn;
        object TargetParent;        

        public GXWizardPropertySettingsPage(GXProtocolAddIn addIn, object parent)
        {
            InitializeComponent();
            TargetParent = parent;
            AddIn = addIn;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            TypeDdl.DrawMode = DrawMode.OwnerDrawFixed;
            TypeDdl.DrawItem += new DrawItemEventHandler(TypeDdl_DrawItem);
            UpdateResources();
        }    

		private void UpdateResources()
		{
            TypeLbl.Text = Gurux.DeviceSuite.Properties.Resources.TypeTxt;
            NameLbl.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
            DescriptionLbl.Text = Gurux.DeviceSuite.Properties.Resources.DescriptionTxt;
		}
		
		#region IGXWizardPage Members

		public void Back()
		{
		}

		public void Next()
		{
			double noOutput = 0;
			if (NameTb.Text.Trim().Length == 0 || double.TryParse(NameTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out noOutput))
			{
                throw new Exception(Gurux.DeviceSuite.Properties.Resources.ErrNameEmptyTxt);
			}
            if (!ReadOnlyTarget)
            {
                Target = TypeDdl.SelectedItem;
            }
		}
       
		public string Description
		{
			get
			{
                return "";
			}
		}

		public string Caption
		{
			get
			{
                return "";
			}
		}

		public GXWizardButtons EnabledButtons
		{
			get
			{
				return GXWizardButtons.All ^ GXWizardButtons.Back;
			}
		}

		public void Finish()
		{
			if (Target is GXProperty)
			{
                GXProperty p = Target as GXProperty;                
				p.Name = NameTb.Text;
				p.Description = DescriptionTb.Text;
                if (!ReadOnlyTarget)
                {
                    p.Initialize(null);
                }
			}
			else if (Target is GXTable)
			{
                GXTable t = Target as GXTable;
				t.Name = NameTb.Text;
				t.Description = DescriptionTb.Text;
                if (!ReadOnlyTarget)
                {
                    t.Initialize();
                }
            }
			else if (Target is GXCategory)
			{
                GXCategory c = Target as GXCategory;
                c.Name = NameTb.Text;
				c.Description = DescriptionTb.Text;
                if (!ReadOnlyTarget)
                {
                    c.Initialize(null);
                }
            }
			else
			{
				throw new Exception("Could not set Name and Description.");
			}
		}        

        public object Target
        {
            get;
            set;
        }

        public void Initialize()
		{
            TypeDdl.Items.Clear();
            int index = 0;
            if (Target is GXProperty)
            {
                GXProperty prop = Target as GXProperty;
                NameTb.Text = prop.Name;
                DescriptionTb.Text = prop.Description;
                foreach (GXProperty it in this.AddIn.GetProperties(TargetParent))
                {
                    if (it.ToString() == null)
                    {
                        throw new Exception("Property name can't be null.");
                    }
                    //If modified.
                    if (it.GetType() == Target.GetType())
                    {
                        index = TypeDdl.Items.Add(prop);
                    }
                    else
                    {
                        TypeDdl.Items.Add(it);
                    }                    
                }
            }
            else if (Target is GXCategory)
            {
                GXCategory cat = Target as GXCategory;
                NameTb.Text = cat.Name;
                DescriptionTb.Text = cat.Description;
                foreach (GXCategory it in this.AddIn.GetCategories(TargetParent))
                {
                    if (it.ToString() == null)
                    {
                        throw new Exception("Category name can't be null.");
                    }
                    //If modified.
                    if (it.GetType() == Target.GetType())
                    {
                        index = TypeDdl.Items.Add(cat);
                    }
                    else
                    {
                        TypeDdl.Items.Add(it);
                    }
                }
            }
            else if (Target is GXTable)
            {
                GXTable table = Target as GXTable;
                NameTb.Text = table.Name;
                DescriptionTb.Text = table.Description;
                foreach (GXTable it in this.AddIn.GetTables(TargetParent))
                {
                    if (it.ToString() == null)
                    {
                        throw new Exception("Table name can't be null.");
                    }
                    //If modified.
                    if (it.GetType() == Target.GetType())
                    {
                        index = TypeDdl.Items.Add(table);
                    }
                    else
                    {
                        TypeDdl.Items.Add(it);
                    }                    
                }
            }
            ReadOnlyTarget = !string.IsNullOrEmpty(NameTb.Text);
            TypeDdl.Enabled = TypeDdl.Items.Count != 1;
            if (!ReadOnlyTarget)
            {
                this.TypeDdl.SelectedIndexChanged += new System.EventHandler(this.TypeDdl_SelectedIndexChanged);
            }
            else
            {
                this.TypeDdl.Enabled = false;
            }
            TypeDdl.SelectedIndex = index;
		}

		public void Cancel()
		{
		}

		public bool IsShown()
		{
			return true;
		}
		#endregion

        private void TypeDdl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            // If the index is invalid then simply exit.
            if (e.Index == -1)
            {
                return;
            }
            string str = TypeDdl.Items[e.Index].GetType().Name;
            using (Brush b = new SolidBrush(e.ForeColor))
            {                
                e.Graphics.DrawString(str, e.Font, b, e.Bounds);
            }
        }

        /// <summary>
        /// Get new name and release old name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!ReadOnlyTarget)
                {
                    Target = TypeDdl.SelectedItem;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }
    }
}
