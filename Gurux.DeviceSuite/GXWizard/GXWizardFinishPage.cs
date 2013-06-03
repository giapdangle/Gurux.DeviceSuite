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
using Gurux.Device;

namespace Gurux.DeviceSuite.GXWizard
{
    public partial class GXWizardFinishPage : Form, IGXWizardPage
    {
        private string m_Caption = string.Empty;
        private string m_Description = string.Empty;        

        public GXWizardFinishPage()
		{
			InitializeComponent();            
			UpdateResources();

//			this.TopLevel = false;
//			this.FormBorderStyle = FormBorderStyle.None;
		}

		private void UpdateResources()
		{
		}

		#region IGXWizardPage Members

		public void Back()
		{
		}

		public void Next()
		{
		}

		public string Description
		{
			get
			{
                return Gurux.DeviceSuite.Properties.Resources.DeviceProfileWizardTitle; ;
			}
		}

		public string Caption
		{
			get
			{
				return m_Caption;
			}
		}

		public GXWizardButtons EnabledButtons
		{
			get
			{
				return GXWizardButtons.All ^ GXWizardButtons.Next;
			}
		}

		public void Finish()
		{
		}

        public void Initialize()
		{
            if (Target is GXProperty)
            {
                label1.Text = Gurux.DeviceSuite.Properties.Resources.WizardFinishTitleTxt;                
                m_Description = Gurux.DeviceSuite.Properties.Resources.WizardFinishTxt;
                m_Caption = Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            }
            else if (Target is GXCategory)
            {
                label1.Text = Gurux.DeviceSuite.Properties.Resources.WizardFinishTitleTxt;                
                m_Description = Gurux.DeviceSuite.Properties.Resources.WizardFinishTxt;
                m_Caption = Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            }
            else if (Target is GXTable)
            {
                label1.Text = Gurux.DeviceSuite.Properties.Resources.WizardFinishTitleTxt;                
                m_Description = Gurux.DeviceSuite.Properties.Resources.WizardFinishTxt;
                m_Caption = Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            }
            else if (Target is GXDevice)
            {
                label1.Text = Gurux.DeviceSuite.Properties.Resources.WizardFinishTitleTxt;                
                m_Description = Gurux.DeviceSuite.Properties.Resources.WizardFinishTxt;
                m_Caption = Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            }
        }

		public void Cancel()
		{
		}

		public bool IsShown()
		{
			return true;
		}

        public object Target
        {
            get;
            set;
        }

        #endregion
    }
}
