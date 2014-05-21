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
using Gurux.Device;
using Gurux.Common;
using System.IO;
using System.Threading;
using System.Xml;
using System.Runtime.Serialization;

namespace Gurux.DeviceSuite.Import
{
    public partial class GXImportDlg : Form
    {
        System.Diagnostics.TraceLevel TraceLevel;
        public bool m_IsClosing = false;
        Thread m_SearchThread = null;
        internal List<Control> m_ProtocolCustomPages = new List<Control>();
        GXProtocolAddIn m_GXAddIn = null;
        internal GXDevice m_GXDevice = null;
        private int m_CurrentPage = 0;
        int FirstPageIndex = 0;
        internal GXImportStartForm Start;
        GXImportSearchForm Trace;
        GXImportPropertiesForm Import;
        internal GXImportMediaForm MediaSettings;

        public GXImportDlg()
        {
            InitializeComponent();
            Bitmap bm = Gurux.DeviceSuite.Properties.Resources.leaf;
            bm.MakeTransparent();
            panel1.BackgroundImage = bm;
        }

        /// <summary>
        /// Initializes a new instance of the ImportDlg class.
        /// </summary>
        /// <param name="gxAddIn">The currently used protocol addin.</param>
        /// <param name="gxDevice">The target device.</param>
        public GXImportDlg(System.Diagnostics.TraceLevel traceLevel, GXProtocolAddIn addIn, GXDevice device, bool fromDataCollector)
        {
            try
            {
                InitializeComponent();
                Bitmap bm = Gurux.DeviceSuite.Properties.Resources.leaf;
                bm.MakeTransparent();
                panel1.BackgroundImage = bm;
                TraceLevel = traceLevel;
                GXCommon.Owner = this;
                Start = new GXImportStartForm(addIn);
                Trace = new GXImportSearchForm();
                Import = new GXImportPropertiesForm();
                MediaSettings = new GXImportMediaForm(fromDataCollector);
                m_GXAddIn = addIn;
                m_GXDevice = device;
                UpdateResources();
                //default settings								
                m_ProtocolCustomPages.Add(Start);
                m_ProtocolCustomPages.Add(MediaSettings);
                m_ProtocolCustomPages.Add(Trace);
                m_ProtocolCustomPages.Add(Import);
                addIn.ModifyWizardPages(device, GXPropertyPageType.Import, m_ProtocolCustomPages);
                m_GXAddIn.OnProgress += new Gurux.Device.Editor.GXProtocolAddIn.ProgressEventHandler(OnProgress);
                m_GXAddIn.OnTrace += new Gurux.Device.Editor.GXProtocolAddIn.TraceEventHandler(OnTraceEvent);
                ChangePage(0);               
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Gurux.DeviceSuite.ImportDlg"/> is reclaimed by garbage collection.
        /// </summary>
        ~GXImportDlg()
        {
            m_GXAddIn.OnProgress -= new Gurux.Device.Editor.GXProtocolAddIn.ProgressEventHandler(OnProgress);
            m_GXAddIn.OnTrace -= new Gurux.Device.Editor.GXProtocolAddIn.TraceEventHandler(OnTraceEvent);
        }

        void OnProgress(int value, int maximum)
        {
            if (this.Created)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Gurux.Device.Editor.GXProtocolAddIn.ProgressEventHandler(OnProgress), new object[] { value, maximum });
                    return;
                }
                ProgressBar1.Maximum = maximum;
                ProgressBar1.Value = value;
            }
        }

        void OnTraceEvent(string value)
        {
            if (this.Created)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Gurux.Device.Editor.GXProtocolAddIn.TraceEventHandler(OnTraceEvent), new object[] { value });
                }
                else
                {
                    Trace.TraceTB.Text += value;
                    Application.DoEvents();
                }
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            try
            {
                ulong tm = Convert.ToUInt64(ElapsedTime.Text) + 1;
                ElapsedTime.Text = tm.ToString();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
                this.DialogResult = DialogResult.None;
            }
        }       

        private void UpdateResources()
        {
            DefaultBtn.Text = Gurux.DeviceSuite.Properties.Resources.SaveAsDefaultSettingsTxt;
            btnBack.Text = Gurux.DeviceSuite.Properties.Resources.BackArrowTxt;
            btnNext.Text = Gurux.DeviceSuite.Properties.Resources.NextArrowTxt;
            CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
            DeviceListName.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
        }

        delegate void FinishedEventHandler(bool succeeded);

        /// <summary>
        /// Stop search
        /// </summary>
        /// <param name="ret"></param>
        public void SearchFinished(bool succeeded)
        {
            try
            {
                if (this.Created && this.InvokeRequired && !this.IsDisposed)
                {
                    Invoke(new FinishedEventHandler(SearchFinished), new object[] { succeeded });
                }
                else
                {
                    if (m_GXDevice != null)
                    {
                        m_GXDevice.Disconnect();
                    }
                    m_Timer.Stop();
                    btnNext.Enabled = succeeded;
                    ChangePage(succeeded ? 1 : 0);
                    btnNext.Select();
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
            try
            {
                MediaSettings.SelectedMedia.Close();
            }
            catch
            {
                //It's OK if close fails.
            }
        }

        delegate void ChangePageEventHandler(int add);

        void ChangePage(int add)
        {
            if (this.Created && this.InvokeRequired)
            {
                Invoke(new ChangePageEventHandler(ChangePage), new object[] { add });
                return;
            }
            if (add == 1 && m_CurrentPage == m_ProtocolCustomPages.Count - 1)
            {
                foreach (IGXWizardPage it in m_ProtocolCustomPages)
                {
                    it.Finish();
                }
                Close();
                DialogResult = DialogResult.OK;
                return;
            }

            IGXWizardPage w = m_ProtocolCustomPages[m_CurrentPage] as IGXWizardPage;
            if (add == 1)
            {
                w.Next();
            }
            else if (add == -1)
            {
                w.Back();
            }
            else if (add == 0)
            {
                FirstPageIndex = 0;
            }
            Control tmp = PropertiesPanel.Tag as Control;
            if (tmp != null)
            {
                while (PropertiesPanel.Controls.Count != 0)
                {
                    tmp.Controls.Add(PropertiesPanel.Controls[0]);
                }
            }
            bool show;
            do
            {
                m_CurrentPage += add;
                w = m_ProtocolCustomPages[m_CurrentPage] as IGXWizardPage;
                show = w.IsShown();
                //If first page is not shown.
                if (!show && add == 0)
                {
                    FirstPageIndex = ++m_CurrentPage;                    
                }
            }
            while (!show && m_CurrentPage > -1 && m_CurrentPage < m_ProtocolCustomPages.Count);
            if (m_CurrentPage > -1 && m_CurrentPage < m_ProtocolCustomPages.Count)
            {
                PropertiesPanel.Tag = w;
                CaptionLbl.Text = w.Caption;
                DescriptionLbl.Text = w.Description;
                w.Target = m_GXDevice;
                w.Initialize();
                //Default btn is visible only in media settings page.
                DefaultBtn.Visible = w is GXImportMediaForm;
                while (m_ProtocolCustomPages[m_CurrentPage].Controls.Count != 0)
                {
                    Control ctrl = m_ProtocolCustomPages[m_CurrentPage].Controls[0];
                    PropertiesPanel.Controls.Add(ctrl);
                    if (ctrl.TabIndex == 0)
                    {
                        ctrl.Focus();
                    }
                }
            }
            btnBack.Enabled = m_CurrentPage > FirstPageIndex;
            btnNext.Text = m_CurrentPage < m_ProtocolCustomPages.Count - 1 ? Gurux.DeviceSuite.Properties.Resources.NextArrowTxt : Gurux.DeviceSuite.Properties.Resources.FinishTxt;
            bool traceView = w == Trace && add == 1;
            ElapsedTime.Visible = ProgressBar1.Visible = traceView;
            if (traceView)
            {
                ElapsedTime.Text = "0";
                btnNext.Enabled = btnBack.Enabled = false;
                m_Timer.Start();
                GXThread th = new GXThread(this);
                th.GXAddIn = m_GXAddIn;
                th.Device = m_GXDevice;
                MediaSettings.SelectedMedia.Trace = TraceLevel;
                if (TraceLevel != System.Diagnostics.TraceLevel.Off)
                {
                    MediaSettings.SelectedMedia.OnTrace += new TraceEventHandler(OnTraceEvent);
                }
                m_SearchThread = new Thread(new ThreadStart(th.SearchThread));
                m_SearchThread.IsBackground = true;
                m_SearchThread.Start();
            }
        }
        public event TraceEventHandler OnTrace;

        void OnTraceEvent(object sender, TraceEventArgs e)
        {
            if (OnTrace != null)
            {
                OnTrace(sender, e);
            }
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (m_CurrentPage > 0)
                {
                    ChangePage(-1);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            try
            {
                ChangePage(1);
            }
            catch (Exception Ex)
            {
                if (!btnNext.Enabled)//If search is running.
                {
                    SearchFinished(false);
                }
                GXCommon.ShowError(this, Ex);
            }
        }

        private void CancelBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (m_SearchThread != null)
                {
                    if (m_SearchThread.IsAlive)
                    {
                        m_IsClosing = true;
                        MediaSettings.SelectedMedia.Close();
                        m_SearchThread = null;
                        this.DialogResult = DialogResult.None;                        
                    }
                }
                m_Timer.Stop();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
                this.DialogResult = DialogResult.None;
            }
        }

        private void DefaultBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                IGXPropertyPage p = MediaSettings.PropertiesForm as IGXPropertyPage;
                if (p != null)
                {
                    p.Apply();
                }
                //Update used media.
                bool found = false;
                if (Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia == null)
                {
                    Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia = new System.Collections.Specialized.StringCollection();
                }
                string newKey = m_GXDevice.ProtocolName + m_GXDevice.DeviceProfile;
                newKey = newKey.GetHashCode().ToString();
                int pos = -1;
                foreach(string it in Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia)
                {
                    ++pos;
                    string[] tmp = it.Split(new char[]{'='});
                    string key = tmp[0];
                    string value = tmp[1];
                    if (string.Compare(newKey, key) == 0)
                    {
                        Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia[pos] = newKey + "=" + MediaSettings.SelectedMedia.MediaType;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMedia.Add(newKey + "=" + MediaSettings.SelectedMedia.MediaType); 
                }
                //Update media settings.
                if (Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings == null)
                {
                    Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings = new System.Collections.Specialized.StringCollection();
                }
                found = false;
                newKey = m_GXDevice.ProtocolName + m_GXDevice.DeviceProfile + MediaSettings.SelectedMedia.MediaType;
                newKey = newKey.GetHashCode().ToString();
                pos = -1;
                foreach(string it in Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings)
                {
                    ++pos;
                    string[] tmp = it.Split(new char[]{'='});
                    string key = tmp[0];
                    string value = tmp[1];
                    if (string.Compare(newKey, key) == 0)
                    {
                        Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings[pos] = newKey + "=" + MediaSettings.SelectedMedia.Settings;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Gurux.DeviceSuite.Properties.Settings.Default.EditorSelectedMediaSettings.Add(newKey + "=" + MediaSettings.SelectedMedia.Settings); 
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this, Ex);
            }
        }
    }
}
