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

namespace Gurux.DeviceSuite.Import
{
    public partial class GXImportMediaForm : Form, IGXWizardPage
    {
        bool FromDataCollector;
        Dictionary<string, IGXMedia> AvailableMedias = new Dictionary<string, IGXMedia>();
        internal Form PropertiesForm;
        internal Gurux.Common.IGXMedia SelectedMedia;
        Gurux.Device.GXDevice m_GXDevice;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public GXImportMediaForm(bool fromDataCollector)
        {
            FromDataCollector = fromDataCollector;
            InitializeComponent();
            if (FromDataCollector)
            {
                MediaPanel.Visible = false;
            }
        }

        /// <summary>
        /// New media is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediasCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (MediasCB.SelectedIndex == -1)
                {
                    return;
                }
                MediaFrame.Controls.Clear();                
                string mediaName = MediasCB.Items[MediasCB.SelectedIndex].ToString();
                if (!AvailableMedias.ContainsKey(mediaName))
                {
                    SelectedMedia = m_GXDevice.GXClient.SelectMedia(mediaName);
                    if (SelectedMedia == null)
                    {
                        throw new Exception(mediaName + " media not found.");
                    }
                }
                else
                {
                    SelectedMedia = AvailableMedias[mediaName];
                    SelectedMedia.Close();
                }
                if (m_GXDevice.GXClient.PacketParser != null)
                {
                    m_GXDevice.GXClient.PacketParser.InitializeMedia(m_GXDevice.GXClient, SelectedMedia);
                }
                PropertiesForm = SelectedMedia.PropertiesForm;
                Gurux.Device.GXMediaTypeCollection mediatypes = m_GXDevice.AllowedMediaTypes;                
                Gurux.Device.GXMediaType tp = mediatypes[SelectedMedia.MediaType];
                try
                {
                    string settings = null;
                    //Find used media.
                    if (Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia != null &&
                        Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings != null)
                    {
                        string newKey = m_GXDevice.ProtocolName + m_GXDevice.DeviceProfile;
                        newKey = newKey.GetHashCode().ToString();
                        foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia)
                        {
                            string[] tmp = it.Split(new char[] { '=' });
                            string key = tmp[0];                            
                            if (string.Compare(newKey, key) == 0)
                            {
                                newKey = m_GXDevice.ProtocolName + m_GXDevice.DeviceProfile + SelectedMedia.MediaType;
                                newKey = newKey.GetHashCode().ToString();
                                //Update media settings.
                                foreach (string it2 in Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings)
                                {
                                    tmp = it2.Split(new char[] { '=' });
                                    key = tmp[0];
                                    if (string.Compare(newKey, key) == 0)
                                    {
                                        settings = tmp[1];
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    if (settings == null && tp != null)
                    {
                        settings = tp.DefaultMediaSettings;
                    }
                    //Set default settings if set.
                    if (settings != null)
                    {
                        SelectedMedia.Settings = settings;
                    }                   
                }
                catch
                {
                    //It's OK if this fails.
                }
                ((IGXPropertyPage)PropertiesForm).Initialize();
                while (PropertiesForm.Controls.Count != 0)
                {
                    Control ctr = PropertiesForm.Controls[0];
                    if (ctr is Panel)
                    {
                        if (!ctr.Enabled)
                        {
                            PropertiesForm.Controls.RemoveAt(0);
                            continue;
                        }
                    }
                    MediaFrame.Controls.Add(ctr);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.Parent, Ex);
            }
        }

        #region IGXWizardPage Members

        bool IGXWizardPage.IsShown()
        {
            return true;
        }

        void IGXWizardPage.Next()
        {           
            IGXPropertyPage p = PropertiesForm as IGXPropertyPage;
            p.Apply();
            SelectedMedia.Validate();
            m_GXDevice.GXClient.AssignMedia(SelectedMedia);
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
            MediasCB.Items.Clear();
            m_GXDevice = ((IGXWizardPage) this).Target as GXDevice;
            //Medias must load always or gateway can't find medias.
            GXMediaTypeCollection types = m_GXDevice.GetAllowedMediaTypes();
            if (FromDataCollector)
            {
                GuruxAMI.Gateway.GXAmiGateway gw = new GuruxAMI.Gateway.GXAmiGateway(Gurux.DeviceSuite.Properties.Settings.Default.AmiHostName,                                
                                Gurux.DeviceSuite.Properties.Settings.Default.AmiUserName,
                                Gurux.DeviceSuite.Properties.Settings.Default.AmiPassword,
                                m_GXDevice.GXClient);
                gw.WaitTime = m_GXDevice.WaitTime; 
                if (!AvailableMedias.ContainsKey(gw.MediaType))
                {
                    AvailableMedias.Add(gw.MediaType, gw);
                }
                MediasCB.Items.Add(gw.MediaType);
            }
            else
            {
                foreach (GXMediaType type in types)
                {
                    MediasCB.Items.Add(type.Name);
                }
            }
            if (m_GXDevice.GXClient.Media == null)
            {
                //Find used media.
                if (Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia != null)
                {
                    string newKey = m_GXDevice.ProtocolName + m_GXDevice.DeviceProfile;
                    newKey = newKey.GetHashCode().ToString();
                    foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia)
                    {
                        string[] tmp = it.Split(new char[] { '=' });
                        string key = tmp[0];
                        string value = tmp[1];
                        if (string.Compare(newKey, key) == 0)
                        {
                            MediasCB.SelectedItem = value;
                            break;
                        }
                    }
                }
                if (MediasCB.SelectedIndex == -1 && MediasCB.Items.Count != 0)
                {
                    MediasCB.SelectedItem = MediasCB.Items[0];
                }
            }
            else
            {
                MediasCB.SelectedItem = m_GXDevice.GXClient.Media.MediaType;
            }
        }

        string IGXWizardPage.Caption
        {
            get
            {
                return Gurux.DeviceSuite.Properties.Resources.DeviceImportTxt;
            }
        }

        string IGXWizardPage.Description
        {
            get
            {
                return Gurux.DeviceSuite.Properties.Resources.SetMediaSettingsTxt;
            }
        }

        GXWizardButtons IGXWizardPage.EnabledButtons
        {
            get
            {
                return GXWizardButtons.All;
            }
        }

        object IGXWizardPage.Target
        {
            get;
            set;
        }

        #endregion
    }
}
