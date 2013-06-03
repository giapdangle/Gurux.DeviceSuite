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
using System.Globalization;

namespace Gurux.DeviceSuite.GXWizard
{
    public partial class GXWizardProtocolSettings : Form, IGXWizardPage
    {
        private GXDevice m_Device;

        public GXWizardProtocolSettings()
		{
			InitializeComponent();						
			UpdateResources();
		}

		private void UpdateResources()
		{
			ResendCountLbl.Text = Gurux.DeviceSuite.Properties.Resources.ResendCountTxt;
            WaitTimeLbl.Text = Gurux.DeviceSuite.Properties.Resources.WaitTimeTxt;
		}

		#region IGXWizardPage Members

		public void Back()
		{
		}

		public void Next()
		{
			double tmpResult = 0;
			if (!double.TryParse(ResendCountTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out tmpResult) || (tmpResult < 0 && tmpResult != -3))
			{
				throw new Exception("Invalid resend count");
			}
		}

		public string Description
		{
			get
			{
                return Gurux.DeviceSuite.Properties.Resources.ProtocolSettingsTxt;
			}
		}

		public string Caption
		{
			get
			{
                return Gurux.DeviceSuite.Properties.Resources.DeviceProfileWizardTitle;
			}
		}

		public GXWizardButtons EnabledButtons
		{
			get
			{
				return GXWizardButtons.All;
			}
		}

		public void Finish()
		{			
            m_Device.WaitTime = (int)((WaitTimeTb.Value.Ticks - WaitTimeTb.MinDate.Ticks) / 10000);
            double tmpResult = 0; 
            if (!double.TryParse(ResendCountTb.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out tmpResult) || (tmpResult < 0 && tmpResult != -3))
			{
				throw new Exception("Invalid resend count");
			}
			else
			{
				m_Device.ResendCount = (int)tmpResult;
			}
		}

        public void Initialize()
		{
            m_Device = Target as GXDevice;
            WaitTimeTb.Value = new DateTime(WaitTimeTb.MinDate.Ticks + (m_Device.WaitTime * 10000));
            ResendCountTb.Text = m_Device.ResendCount.ToString();
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
