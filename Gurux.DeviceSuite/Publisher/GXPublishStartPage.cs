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

namespace Gurux.DeviceSuite.Publisher
{
    public partial class GXPublishStartPage : Form, IGXWizardPage
    {
        public GXPublishStartPage()
        {
            InitializeComponent();
            CaptionLbl.Text = Gurux.DeviceSuite.Properties.Resources.PublisherInfoTitle;
            DescriptionLbl.Text = Gurux.DeviceSuite.Properties.Resources.PublisherInfoDescription;
        }

        #region IGXWizardPage Members

        public bool IsShown()
        {
            return true;
        }

        public void Next()
        {
            
        }

        public void Back()
        {
           
        }

        public void Finish()
        {
            
        }

        public void Cancel()
        {
            
        }

        public void Initialize()
        {
           
        }

        public GXWizardButtons EnabledButtons
        {
            get
            {
                return GXWizardButtons.Cancel | GXWizardButtons.Next;
            }
        }

        public string Caption
        {
            get
            {
                return "Device template publisher";
            }
        }

        public string Description
        {
            get
            {
                return Gurux.DeviceSuite.Properties.Resources.PublisherInfoTitle;
            }
        }

        public object Target
        {
            get;
            set;

        }

        #endregion

    }
}
