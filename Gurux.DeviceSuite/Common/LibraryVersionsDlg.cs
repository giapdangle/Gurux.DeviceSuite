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
using System.Diagnostics;
using System.Collections;
using Gurux.Common;
using System.IO;
using System.Threading;

namespace Gurux.DeviceSuite.Common
{
    public partial class LibraryVersionsDlg : Form
    {
        /// <summary>
        /// Initializes a new instance of the LibraryVersionsDlg class.
        /// </summary>
        public LibraryVersionsDlg()
        {
            InitializeComponent();
            UpdateResources();
        }

        private void UpdateResources()
        {
            try
            {
                this.CancelBtn.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
                this.NameHeader.Text = Gurux.DeviceSuite.Properties.Resources.NameTxt;
                this.VersionHeader.Text = Gurux.DeviceSuite.Properties.Resources.VersionTxt;
                this.CopyBtn.Text = Gurux.DeviceSuite.Properties.Resources.CopyTxt;
                this.Text = Gurux.DeviceSuite.Properties.Resources.LibraryVersionsTxt;
                //Update help strings from the resource.
                this.helpProvider1.SetHelpString(this.listView1, Gurux.DeviceSuite.Properties.Resources.LibraryListHelp);
                this.helpProvider1.SetHelpString(this.CopyBtn, Gurux.DeviceSuite.Properties.Resources.LibraryCopyHelp);
                this.helpProvider1.SetHelpString(this.CancelBtn, Gurux.DeviceSuite.Properties.Resources.CancelHelp);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void LibraryVersionsDlg_Load(object sender, System.EventArgs e)
        {
            Process CurrentProcess = Process.GetCurrentProcess();
            ArrayList gxModules = new ArrayList();
            ArrayList msModules = new ArrayList();
            ArrayList otherModules = new ArrayList();
            foreach (ProcessModule Mod in CurrentProcess.Modules)
            {
                //Do not show Mono debug info.
                if (Path.GetExtension(Mod.FileName) == ".mdb")
                {
                    continue;
                }
                string Name = Path.GetFileName(Mod.FileName).ToLower();
                //In Mono Comapny name might be null.
                string company = Mod.FileVersionInfo.CompanyName;
                if (company == null)
                {
                    company = "";
                }
                else
                {
                    company = company.ToLower();
                }
                if (company.StartsWith("gurux") ||
                    Name.StartsWith("gx") ||
                    Name.StartsWith("gurux") ||
                    Name.StartsWith("interop") ||
                    Name.StartsWith("interop.gurux"))
                {
                    gxModules.Add(Mod);
                }
                else if (company.StartsWith("microsoft"))
                {
                    msModules.Add(Mod);
                }
                else
                {
                    otherModules.Add(Mod);
                }
            }
            ListViewItem it = listView1.Items.Add("---GURUX---");
            it.SubItems.Add("-----");
            foreach (ProcessModule Mod in gxModules)
            {
                string Name = Path.GetFileName(Mod.FileName);
                it = listView1.Items.Add(Name);
                it.SubItems.Add(Mod.FileVersionInfo.FileVersion);
                FileInfo fi = new FileInfo(Mod.FileName);
                it.SubItems.Add(fi.FullName.ToString());
            }
            if (msModules.Count != 0)
            {
                it = listView1.Items.Add("---MICROSOFT---");
                it.SubItems.Add("-----");
                foreach (ProcessModule Mod in msModules)
                {
                    string Name = Path.GetFileName(Mod.FileName);
                    it = listView1.Items.Add(Name);
                    it.SubItems.Add(Mod.FileVersionInfo.FileVersion);
                    it.SubItems.Add(Mod.FileName);
                }
            }
            it = listView1.Items.Add("---OTHERS---");
            it.SubItems.Add("-----");
            foreach (ProcessModule Mod in otherModules)
            {
                string Name = Path.GetFileName(Mod.FileName);
                it = listView1.Items.Add(Name);
                it.SubItems.Add(Mod.FileVersionInfo.FileVersion);
                it.SubItems.Add(Mod.FileName);
            }
        }

        private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopyText();
            }
        }

        private void CopyText()
        {
            try
            {
                string clip = string.Empty;
                foreach (ListViewItem it in listView1.Items)
                {
                    clip += it.Text + " " + it.SubItems[1].Text + Environment.NewLine;
                }
                ClipboardCopy.CopyDataToClipboard(clip);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CopyBtn_Click(object sender, System.EventArgs e)
        {
            CopyText();
        }

        /// <summary>
        /// Show help not available message.
        /// </summary>
        /// <param name="hevent">A HelpEventArgs that contains the event data.</param>
        protected override void OnHelpRequested(HelpEventArgs hevent)
        {
            // Get the control where the user clicked
            Control ctl = this.GetChildAtPoint(this.PointToClient(hevent.MousePos));
            string str = Gurux.DeviceSuite.Properties.Resources.HelpNotAvailable;
            // Show as a Help pop-up
            if (str != "")
            {
                Help.ShowPopup(ctl, str, hevent.MousePos);
            }
            // Set flag to show that the Help event as been handled
            hevent.Handled = true;
        }
    }  
}
