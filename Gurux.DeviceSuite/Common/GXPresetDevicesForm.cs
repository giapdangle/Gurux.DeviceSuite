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
using Gurux.Device.PresetDevices;
using Gurux.Common;
using Gurux.Device;
using Gurux.DeviceSuite.Manufacturer;
using System.IO;
using Gurux.Device.Editor;
using ServiceStack.ServiceClient.Web;
using Gurux.Device.Publisher;

namespace Gurux.DeviceSuite.Common
{
    public partial class GXPresetDevicesForm : Form
    {
        bool InitializePreset;
        GXDeviceManufacturerCollection PresetManufacturers;
        GXDeviceManufacturerCollection Downloadable = new GXDeviceManufacturerCollection();
        TreeNode ManufacturersNode;
        System.Collections.Hashtable ItemToTreeNode = new System.Collections.Hashtable();
        System.Collections.Hashtable ItemToListItem = new System.Collections.Hashtable();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="manufacturers"></param>
        public GXPresetDevicesForm(bool initializePreset, GXDeviceManufacturerCollection manufacturers, GXDeviceManufacturerCollection downloadable)
        {
            InitializeComponent();
            Bitmap bm = PublisherImage.BackgroundImage as Bitmap;
            bm.MakeTransparent();
            PublisherImage.BackgroundImage = bm;
            PresetPropertiesMenu.ShortcutKeys = Keys.Alt | Keys.Enter;
            PresetManufacturers = manufacturers;
            ManufacturersNode = PresetTree.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.ManufacturersTxt);
            Downloadable = downloadable;
            InitializePreset = initializePreset;
        }

        void UpdateView(bool preset)
        {
            ShowDevices(preset);
            PresetTree.Select();
            PresetTree.Focus();
        }

        int GetImageIndex(DownloadStates status)
        {
            if ((status & DownloadStates.Remove) != 0)
            {
                return 1;
            }
            else if ((status & DownloadStates.Installed) != 0)
            {
                return 2;
            }
            else if ((status & DownloadStates.Add) != 0)
            {
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// Show preset devices or downloadable devices.
        /// </summary>
        /// <param name="preset"></param>
        void ShowDevices(bool preset)
        {
            ShowDisabledCB.Visible = ShowPreviousVersionsCB.Visible = !preset;
            //Preset do not have images.
            if (preset)
            {
                PresetTree.ImageList = null;
            }
            else
            {
                PresetTree.ImageList = imageList1;
            }
            ManufacturersNode.Nodes.Clear();
            PresetList.Items.Clear();
            ItemToListItem.Clear();
            GXDeviceManufacturerCollection manufacturers;
            if (preset)
            {
                manufacturers = PresetManufacturers;
            }
            else
            {
                manufacturers = Downloadable;
            }
            bool showDisabled = !preset && ShowDisabledCB.Checked;
            bool showHistory = !preset && ShowPreviousVersionsCB.Checked;
            ManufacturersNode.Tag = manufacturers;
            foreach (GXDeviceManufacturer man in manufacturers)
            {
                if ((!showDisabled && (man.Status & DownloadStates.Remove) != 0) ||
                    (!showHistory && (man.Status & DownloadStates.Installed) != 0))
                {
                    continue;
                }
                TreeNode manNode = new TreeNode(man.Name);
                manNode.Tag = man;
                manNode.ImageIndex = manNode.SelectedImageIndex = GetImageIndex(man.Status);
                foreach (GXDeviceModel model in man.Models)
                {
                    if ((!showDisabled && (model.Status & DownloadStates.Remove) != 0) ||
                        (!showHistory && (model.Status & DownloadStates.Installed) != 0))
                    {
                        continue;
                    }
                    TreeNode modelNode = new TreeNode(model.Name);
                    modelNode.Tag = model;
                    modelNode.ImageIndex = modelNode.SelectedImageIndex = GetImageIndex(model.Status);
                    foreach (GXDeviceVersion dv in model.Versions)
                    {
                        if ((!showDisabled && (dv.Status & DownloadStates.Remove) != 0) ||
                            (!showHistory && (dv.Status & DownloadStates.Installed) != 0))
                        {
                            continue;
                        }
                        TreeNode dvNode = new TreeNode(dv.Name);
                        dvNode.Tag = dv;
                        dvNode.ImageIndex = dvNode.SelectedImageIndex = GetImageIndex(dv.Status);
                        bool show = preset;
                        if (!preset)
                        {
                            foreach (GXPublishedDeviceType dt in dv.Templates)
                            {
                                bool installed = false;
                                //Find installed or latest version.
                                foreach (GXTemplateVersion tv in dt.Versions)
                                {
                                    //If new version.
                                    if (installed)
                                    {
                                        if ((!showDisabled && (tv.Status & DownloadStates.Remove) != 0))
                                        {
                                            continue;
                                        }
                                        show = true;
                                        break;
                                    }
                                    //Find latest version.
                                    if ((tv.Status & DownloadStates.Installed) != 0)
                                    {
                                        installed = true;
                                    }
                                    if ((!showDisabled && (tv.Status & DownloadStates.Remove) != 0) ||
                                        (!showHistory && (tv.Status == 0 || (tv.Status & DownloadStates.Installed) != 0)))
                                        //(!showHistory && (tv.Status & DownloadStates.Installed) != 0))
                                    {
                                        continue;
                                    }
                                    show = true;
                                    break;
                                }
                                if (!installed)
                                {
                                    show = true;
                                }
                                if (show)
                                {
                                    break;
                                }
                            }
                        }
                        /*
                        if (!preset)
                        {
                            foreach (GXPublishedDeviceType dt in dv.Templates)
                            {
                                if ((!showDisabled && (dt.Status & DownloadStates.Remove) != 0) ||
                                    (!showHistory && (dt.Status & DownloadStates.Installed) != 0))
                                {
                                    continue;
                                }
                                TreeNode dtNode = dvNode.Nodes.Add(dt.PresetName);
                                dtNode.Tag = dt;
                                ItemToTreeNode[dt] = dtNode;
                                dtNode.ImageIndex = dtNode.SelectedImageIndex = GetImageIndex(dt.Status);                                
                            }
                        }
                         * */
                        //Do not add empty downloadable node.
                        //if (preset) || dvNode.Nodes.Count != 0)
                        if (show)
                        {
                            modelNode.Nodes.Add(dvNode);
                            ItemToTreeNode[dv] = dvNode;
                        }
                    }
                    //Do not add empty downloadable node.
                    if (preset || modelNode.Nodes.Count != 0)
                    {
                        manNode.Nodes.Add(modelNode);
                        ItemToTreeNode[model] = modelNode;
                    }
                }
                //Do not add empty downloadable node.
                if (preset || manNode.Nodes.Count != 0)
                {
                    ManufacturersNode.Nodes.Add(manNode);
                    ItemToTreeNode[man] = manNode;
                }
            }
            PresetTree.SelectedNode = null;
            PresetTree.SelectedNode = ManufacturersNode;
            PresetTree.Focus();
            PresetTree.ExpandAll();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == PresetPage)
                {
                    while (LoadablePage.Controls.Count != 0)
                    {
                        PresetPage.Controls.Add(LoadablePage.Controls[0]);
                    }
                    PresetTree.ContextMenuStrip = PresetList.ContextMenuStrip = PresetMenu;
                    ShowDevices(true);
                }
                else
                {
                    while (PresetPage.Controls.Count != 0)
                    {
                        LoadablePage.Controls.Add(PresetPage.Controls[0]);
                    }
                    PresetTree.ContextMenuStrip = PresetList.ContextMenuStrip = LoadableMenu;
                    ShowDevices(false);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        GXTemplateVersion FindInstalledVersion(GXTemplateVersionCollection versions)
        {
            GXTemplateVersion latest = null;
            foreach (GXTemplateVersion it in versions)
            {
                if (it.Status == DownloadStates.Installed)
                {
                    return it;
                }
                if (it.Status == DownloadStates.None)
                {
                    latest = it;
                }
            }            
            return latest;
        }

        private void PresetTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                bool preset = tabControl1.SelectedTab == PresetPage;
                ItemToListItem.Clear();
                PresetList.Items.Clear();
                TreeNode node = PresetTree.SelectedNode;
                if (node.Tag is GXDeviceVersion || node.Tag is GXPublishedDeviceType)
                {
                    if (!PresetList.Columns.Contains(VersionCH))
                    {
                        PresetList.Columns.Add(VersionCH);
                    }
                }
                else
                {
                    if (PresetList.Columns.Contains(VersionCH))
                    {
                        PresetList.Columns.Remove(VersionCH);
                    }
                }
                bool showDisabled = !preset && ShowDisabledCB.Checked;
                ListViewItem li;
                if (node.Tag is GXDeviceManufacturerCollection)
                {
                    TargetCH.Text = "Manufacturers";
                    GXDeviceManufacturerCollection items = node.Tag as GXDeviceManufacturerCollection;
                    foreach (GXDeviceManufacturer it in items)
                    {
                        if (!showDisabled && (it.Status & DownloadStates.Remove) != 0)
                        {
                            continue;
                        }
                        //If device is in tree
                        if (ItemToTreeNode[it] != null)
                        {
                            li = PresetList.Items.Add(it.Name);
                            li.SubItems.Add(GetStatusText(preset, it.Status));
                            li.Tag = it;
                            ItemToListItem[it] = li;
                        }
                    }
                }
                else if (node.Tag is GXDeviceManufacturer)
                {
                    TargetCH.Text = "Models";
                    GXDeviceManufacturer man = node.Tag as GXDeviceManufacturer;
                    foreach (GXDeviceModel it in man.Models)
                    {
                        if (!showDisabled && (it.Status & DownloadStates.Remove) != 0)
                        {
                            continue;
                        }
                        li = PresetList.Items.Add(it.Name);
                        li.SubItems.Add(GetStatusText(preset, it.Status));
                        li.Tag = it;
                        ItemToListItem[it] = li;
                    }
                }
                else if (node.Tag is GXDeviceModel)
                {
                    TargetCH.Text = "Versions";
                    GXDeviceModel model = node.Tag as GXDeviceModel;
                    foreach (GXDeviceVersion it in model.Versions)
                    {
                        if (!showDisabled && (it.Status & DownloadStates.Remove) != 0)
                        {
                            continue;
                        }
                        li = PresetList.Items.Add(it.Name);
                        li.SubItems.Add(GetStatusText(preset, it.Status));
                        li.Tag = it;
                        ItemToListItem[it] = li;
                    }
                }
                else if (node.Tag is GXDeviceVersion)
                {
                    TargetCH.Text = Gurux.DeviceSuite.Properties.Resources.DeviceProfilesTxt;
                    GXDeviceVersion version = node.Tag as GXDeviceVersion;
                    foreach (GXPublishedDeviceType dt in version.Templates)
                    {
                        if (!showDisabled && (dt.Status & DownloadStates.Remove) != 0)
                        {
                            continue;
                        }
                        if (preset || !ShowPreviousVersionsCB.Checked)
                        {                            
                            //Show last device template version number.
                            if (dt.Versions.Count != 0)
                            {
                                if (preset)
                                {
                                    li = PresetList.Items.Add(dt.PresetName);
                                    GXTemplateVersion ver = FindInstalledVersion(dt.Versions);
                                    li.SubItems.Add(GetStatusText(preset, ver.Status));
                                    li.SubItems.Add(ver.ToString());
                                    li.Tag = dt;
                                    ItemToListItem[dt] = li;
                                }
                                else
                                {
                                    //Show new versions.
                                    bool installed = false;
                                    foreach (GXTemplateVersion it in dt.Versions)
                                    {
                                        if (it.Status == DownloadStates.Installed)
                                        {
                                            installed = true;
                                            continue;
                                        }
                                        if (it.Status == DownloadStates.Remove)
                                        {
                                            installed = true;
                                            continue;
                                        }
                                        if (installed && (showDisabled || (it.Status & DownloadStates.Remove) == 0))
                                        {
                                            li = PresetList.Items.Add(dt.PresetName);
                                            li.SubItems.Add(GetStatusText(preset, it.Status));
                                            li.SubItems.Add(it.ToString());
                                            li.Tag = it;
                                            ItemToListItem[it] = li;                                            
                                        }
                                    }
                                    //If new item show latest version.
                                    if (!installed)
                                    {
                                        li = PresetList.Items.Add(dt.PresetName);
                                        GXTemplateVersion it = FindInstalledVersion(dt.Versions);
                                        li.SubItems.Add(GetStatusText(preset, it.Status));
                                        li.SubItems.Add(it.ToString());
                                        li.Tag = dt;
                                        ItemToListItem[dt] = li;
                                    }
                                }
                            }
                            else
                            {
                                li = PresetList.Items.Add(dt.PresetName);
                                li.SubItems.Add(GetStatusText(preset, dt.Status));
                                li.Tag = dt;
                                ItemToListItem[dt] = li;
                            }
                        }
                        else
                        {
                            foreach (GXTemplateVersion tv in dt.Versions)
                            {
                                if (!showDisabled && (tv.Status & DownloadStates.Remove) != 0)
                                {
                                    continue;
                                }
                                li = PresetList.Items.Add(dt.PresetName);
                                li.SubItems.Add(GetStatusText(preset, tv.Status));
                                li.SubItems.Add(tv.ToString());
                                li.Tag = tv;
                                ItemToListItem[tv] = li;
                            }
                        }
                    }
                }
                else if (node.Tag is GXPublishedDeviceType)
                {
                    TargetCH.Text = "Device templates";
                    GXPublishedDeviceType dt = node.Tag as GXPublishedDeviceType;
                    if (preset || !ShowPreviousVersionsCB.Checked)
                    {
                        li = PresetList.Items.Add(dt.PresetName);
                        //Show last device template version number.
                        if (dt.Versions.Count != 0)
                        {
                            GXTemplateVersion ver = FindInstalledVersion(dt.Versions);
                            li.SubItems.Add(GetStatusText(preset, ver.Status));
                            li.SubItems.Add(ver.ToString());
                        }
                        else
                        {
                            li.SubItems.Add(GetStatusText(preset, dt.Status));
                        }                        
                        li.Tag = dt;
                        ItemToListItem[dt] = li;
                    }
                    else
                    {
                        foreach (GXTemplateVersion tv in dt.Versions)
                        {
                            if (!showDisabled && (tv.Status & DownloadStates.Remove) != 0)
                            {
                                continue;
                            }
                            li = PresetList.Items.Insert(0, dt.PresetName);
                            li.SubItems.Add(GetStatusText(preset, tv.Status));
                            li.SubItems.Add(tv.ToString());
                            li.Tag = tv;
                            ItemToListItem[tv] = li;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        //Preset None, Add, Remove, (Oma, installoitu.
        //Update Add (Available), Remove (Disable), None (Installed)
        string GetStatusText(bool preset, DownloadStates status)
        {
            //If latest version is installed.
            if (status == DownloadStates.None)
            {
                if (preset)
                {
                    return "None";
                }
                //If new version is available.
                return "Available";
            }
            if ((status & DownloadStates.Remove) != 0)
            {
                if (preset)
                {
                    return "Remove";
                }
                return "Disable";
            }
            //Item is marked as downloaded.
            if ((status & DownloadStates.Add) != 0)
            {
                if (preset)
                {
                    return "Add";
                }
                return "Download";
            }
            /*
            //If new version is available.
            if ((status & DownloadStates.Available) != 0)
            {
                return "Available";
            }
             * */
            if ((status & DownloadStates.Installed) != 0)
            {
                return "Installed";
            }
            throw new Exception("Invalid status.");
        }

        /// <summary>
        /// Add new item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetAddMenu_Click(object sender, EventArgs e)
        {
            AddObject(PresetTree.SelectedNode.Tag);
        }

        void AddObject(object target)
        {
            if (target is GXDeviceManufacturerCollection)
            {
                AddManufacturer();
            }
            if (target is GXDeviceManufacturer)
            {
                AddModel(target as GXDeviceManufacturer);
            }
            else if (target is GXDeviceModel)
            {
                AddVersion(target as GXDeviceModel);
            }
            else if (target is GXDeviceVersion)
            {
                AddTemplate(target as GXDeviceVersion);
            }
        }

        void RemoveObject(object target)
        {
            if (target is GXDeviceManufacturer)
            {
                GXDeviceManufacturer item = target as GXDeviceManufacturer;
                //If item is new we can remove it right a way.
                if (item.Status == DownloadStates.Add)
                {
                    item.Parent.Remove(item);
                }
                else
                {
                    item.Status = DownloadStates.Remove;
                }
            }
            else if (target is GXDeviceModel)
            {
                GXDeviceModel item = target as GXDeviceModel;
                //If item is new we can remove it right a way.
                if (item.Status == DownloadStates.Add)
                {
                    item.Parent.Remove(item);
                }
                else
                {
                    item.Status = DownloadStates.Remove;
                }
            }
            else if (target is GXDeviceVersion)
            {
                GXDeviceVersion item = target as GXDeviceVersion;
                //If item is new we can remove it right a way.
                if (item.Status == DownloadStates.Add)
                {
                    item.Parent.Remove(item);
                }
                else
                {
                    item.Status = DownloadStates.Remove;
                }
            }
            else if (target is GXPublishedDeviceType)
            {
                GXPublishedDeviceType item = target as GXPublishedDeviceType;
                //If item is new we can remove it right a way.
                if (item.Status == DownloadStates.Add)
                {
                    item.Parent.Remove(item);
                }
                else
                {
                    item.Status = DownloadStates.Remove;
                }
            }
            else
            {
                throw new Exception("Failed to remove selected target.");
            }
            ItemToTreeNode.Remove(target);
            ItemToListItem.Remove(target);
        }

        /// <summary>
        /// Remove selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetDeleteMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if (PresetTree.Focused)
                    {
                        RemoveObject(PresetTree.SelectedNode.Tag);
                        PresetTree.SelectedNode.Remove();
                    }
                    else if (PresetList.Focused)
                    {
                        while (PresetList.SelectedItems.Count != 0)
                        {
                            RemoveObject(PresetList.SelectedItems[0].Tag);
                            PresetList.SelectedItems[0].Remove();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Edit selected manufacturer.
        /// </summary>
        private void EditManufacturer(GXDeviceManufacturer manufacturer)
        {
            GXManufacturerForm dlg = new GXManufacturerForm(manufacturer);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TreeNode manNode = ItemToTreeNode[manufacturer] as TreeNode;
                manNode.Text = manufacturer.Name;
            }
        }

        /// <summary>
        /// Edit selected model.
        /// </summary>
        private void EditModel(GXDeviceModel model)
        {
            GXDeviceModelForm dlg = new GXDeviceModelForm(model);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TreeNode node = ItemToTreeNode[model] as TreeNode;
                node.Text = model.Name;
            }
        }

        /// <summary>
        /// Edit selected version.
        /// </summary>
        private void EditVersion(GXDeviceVersion version)
        {
            try
            {
                GXDeviceVersionForm dlg = new GXDeviceVersionForm(version);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TreeNode node = ItemToTreeNode[version] as TreeNode;
                    node.Text = version.Name;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        /// <summary>
        /// Edit selected device template.
        /// </summary>
        private void EditTemplate(GXPublishedDeviceType type)
        {
            try
            {
                GXDeviceTemplateForm dlg = new GXDeviceTemplateForm(type);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TreeNode node = ItemToTreeNode[type] as TreeNode;
                    if (node != null)
                    {
                        node.Text = type.PresetName;
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        void ObjectProperties(object target)
        {
            if (target is GXDeviceManufacturer)
            {
                EditManufacturer(target as GXDeviceManufacturer);
            }
            else if (target is GXDeviceModel)
            {
                EditModel(target as GXDeviceModel);
            }
            else if (target is GXDeviceVersion)
            {
                EditVersion(target as GXDeviceVersion);
            }
            else if (target is GXPublishedDeviceType)
            {
                EditTemplate(target as GXPublishedDeviceType);
            }
            else
            {
                throw new Exception("Invalid target.");
            }
        }

        /// <summary>
        /// Show properties of selected object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetPropertiesMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (PresetTree.Focused)
                {
                    ObjectProperties(PresetTree.SelectedNode.Tag);
                }
                else if (PresetList.Focused)
                {
                    if (PresetList.SelectedItems.Count == 1)
                    {
                        ObjectProperties(PresetList.SelectedItems[0].Tag);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        /// <summary>
        /// Hide delete and options if no item is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetMenu_Opening(object sender, CancelEventArgs e)
        {
            bool visible = false;
            if (PresetTree.Focused)
            {
                visible = !(PresetTree.SelectedNode.Tag is GXDeviceManufacturerCollection);
            }
            else if (PresetList.Focused)
            {
                visible = PresetList.SelectedItems.Count != 0;
            }
            PresetDeleteMenu.Visible = PresetPropertiesMenu.Visible = visible;
        }

        /// <summary>
        /// Do not show menu if there is nothing selected to load or remove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadableMenu_Opening(object sender, CancelEventArgs e)
        {
            if (PresetTree.Focused)
            {
                e.Cancel = (PresetTree.SelectedNode.Tag is GXDeviceManufacturerCollection);
                ToggleDisableMenu(PresetTree.SelectedNode.Tag);
            }
            else if (PresetList.Focused)
            {
                e.Cancel = PresetList.SelectedItems.Count == 0;
                if (e.Cancel)
                {
                    return;
                }
                if (PresetList.SelectedItems.Count == 1)
                {                    
                    ToggleDisableMenu(PresetList.SelectedItems[0].Tag);
                }
                else
                {
                    DisableMenu.Visible = EnableMenu.Visible = false;
                }
            }
        }

        void ToggleDisableMenu(object target)
        {
            bool disabled = false;
            if (target is GXDeviceManufacturer)
            {
                disabled = ((target as GXDeviceManufacturer).Status & DownloadStates.Remove) != 0;
            }
            else if (target is GXDeviceModel)
            {
                disabled = ((target as GXDeviceModel).Status & DownloadStates.Remove) != 0;
            }
            else if (target is GXDeviceVersion)
            {
                disabled = ((target as GXDeviceVersion).Status & DownloadStates.Remove) != 0;
            }
            else if (target is GXPublishedDeviceType)
            {
                disabled = ((target as GXPublishedDeviceType).Status & DownloadStates.Remove) != 0;
            }
            else if (target is GXTemplateVersion)
            {
                disabled = ((target as GXTemplateVersion).Status & DownloadStates.Remove) != 0;
            }
            else
            {
                DisableMenu.Visible = EnableMenu.Visible = false;
                return;
            }
            DisableMenu.Visible = !disabled;
            EnableMenu.Visible = disabled;
        }

        /// <summary>
        /// Add new manufacturer.
        /// </summary>
        private void AddManufacturer()
        {
            GXDeviceManufacturer manufacturer = new GXDeviceManufacturer();
            manufacturer.Status = DownloadStates.Add;
            GXManufacturerForm dlg = new GXManufacturerForm(manufacturer);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PresetManufacturers.Add(manufacturer);
                ListViewItem li = PresetList.Items.Add(manufacturer.Name);
                li.Tag = manufacturer;
                TreeNode manNode = ManufacturersNode.Nodes.Add(manufacturer.Name);
                manNode.Tag = manufacturer;
                ItemToListItem[manufacturer] = li;
                ItemToTreeNode[manufacturer] = manNode;
                PresetTree.SelectedNode = manNode;
            }
        }

        /// <summary>
        /// Add new model.
        /// </summary>
        private void AddModel(GXDeviceManufacturer man)
        {            
            GXDeviceModel model = new GXDeviceModel();
            model.Status = DownloadStates.Add;
            GXDeviceModelForm dlg = new GXDeviceModelForm(model);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                man.Models.Add(model);
                //Add tree item.
                TreeNode parentNode = ItemToTreeNode[man] as TreeNode;
                TreeNode node = parentNode.Nodes.Add(model.Name);
                node.Tag = model;
                ItemToTreeNode[model] = node;
                //Add list item.
                ListViewItem li = PresetList.Items.Add(model.Name);
                li.Tag = model;
                ItemToListItem[model] = li;
                //Select new item from the tree.
                PresetTree.SelectedNode = node;
            }
        }

        /// <summary>
        /// Add new version and select it.
        /// </summary>
        private void AddVersion(GXDeviceModel model)
        {
            try
            {
                GXDeviceVersion version = new GXDeviceVersion();
                version.Status = DownloadStates.Add;
                GXDeviceVersionForm dlg = new GXDeviceVersionForm(version);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    model.Versions.Add(version);
                    //Add tree item.
                    TreeNode parentNode = ItemToTreeNode[model] as TreeNode;
                    TreeNode node = parentNode.Nodes.Add(version.Name);
                    node.Tag = version;
                    ItemToTreeNode[version] = node;
                    //Add list item.
                    ListViewItem li = PresetList.Items.Add(version.Name);
                    li.Tag = version;
                    ItemToListItem[version] = li;
                    //Select new item from the tree.
                    PresetTree.SelectedNode = node;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }
        
        /// <summary>
        /// Add new device template and select it.
        /// </summary>
        private void AddTemplate(GXDeviceVersion version)
        {
            try
            {
                GXPublishedDeviceType type = new GXPublishedDeviceType();
                type.Status = DownloadStates.Add;
                GXDeviceTemplateForm dlg = new GXDeviceTemplateForm(type);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    type = dlg.Target;
                    type.Status = DownloadStates.Add;
                    version.Templates.Add(type);
                    //Add list item.
                    ListViewItem li = PresetList.Items.Add(type.PresetName);
                    li.Tag = type;
                    ItemToListItem[type] = li;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        /// <summary>
        /// Download all items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Update preset items.                
                for (int manPos = 0; manPos != PresetManufacturers.Count; ++manPos)
                {
                    GXDeviceManufacturer man = PresetManufacturers[manPos];
                    bool remove = man.Status == DownloadStates.Remove;
                    if (remove && man.Parent != null)
                    {
                        man.Parent.Remove(man);
                        --manPos;
                    }
                    //Item is added earlier. Just remove state.
                    if (man.Status == DownloadStates.Add)
                    {
                        man.Status = DownloadStates.None;
                    }
                    for (int modelPos = 0; modelPos != man.Models.Count; ++modelPos)
                    {
                        GXDeviceModel model = man.Models[modelPos];
                        remove |= model.Status == DownloadStates.Remove;
                        if (remove && model.Parent != null)
                        {
                            model.Parent.Remove(model);
                            --modelPos;
                        }
                        //Item is added earlier. Just remove state.
                        if (model.Status == DownloadStates.Add)
                        {
                            model.Status = DownloadStates.None;
                        }
                        for (int dvPos = 0; dvPos != model.Versions.Count; ++dvPos)
                        {
                            GXDeviceVersion dv = model.Versions[dvPos];
                            remove |= dv.Status == DownloadStates.Remove;
                            if (remove && dv.Parent != null)
                            {
                                dv.Parent.Remove(dv);
                                --dvPos;
                            }
                            //Item is added earlier. Just remove state.
                            if (dv.Status == DownloadStates.Add)
                            {
                                dv.Status = DownloadStates.None;
                            }
                            for (int dtPos = 0; dtPos != dv.Templates.Count; ++dtPos)
                            {
                                GXPublishedDeviceType dt = dv.Templates[dtPos] as GXPublishedDeviceType;
                                if (dt.Status == DownloadStates.Add)
                                {                                    
                                    string customPath = GXDevice.GetDeviceTemplatePath(dt.Protocol, dt.Name);
                                    //Update device guid.
                                    string protocol = null, deviceType = null;
                                    Guid deviceGuid;
                                    bool preset;
                                    GXDevice.GetProtocolInfo(customPath, out preset, out protocol, out deviceType, out deviceGuid);
                                    dt.DeviceGuid = deviceGuid;
                                    string presetPath = GXDevice.GetDeviceTemplatePath(deviceGuid);
                                    string dir = Path.GetDirectoryName(presetPath);
                                    if (!Directory.Exists(dir))
                                    {
                                        Directory.CreateDirectory(dir);
                                    }
                                    File.Copy(customPath, presetPath, true);
                                    GXDevice dev = GXDevice.Load(presetPath);
                                    dev.PresetName = dt.PresetName;
                                    dev.Manufacturer = man.Name;
                                    dev.Model = model.Name;
                                    dev.Version = dv.Name;
                                    dev.Save(presetPath);
                                    dt.Status = DownloadStates.None;
                                }
                                else
                                {
                                    remove |= dt.Status == DownloadStates.Remove;
                                    if (remove)
                                    {
                                        dt.Parent.Remove(dt);
                                        --dtPos;
                                    }
                                    else
                                    {
                                        foreach (GXTemplateVersion it in dt.Versions)
                                        {
                                            if (remove || it.Status == DownloadStates.Remove)
                                            {
                                                //TODO: Remove version from the HD.
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Download new items.
                foreach (GXDeviceManufacturer man in Downloadable)
                {
                    if ((man.Status & DownloadStates.Add) != 0)
                    {
                        Download(man);                        
                        continue;
                    }
                    foreach (GXDeviceModel model in man.Models)
                    {
                        if ((model.Status & DownloadStates.Add) != 0)
                        {
                            Download(model);
                            continue;
                        }
                        foreach (GXDeviceVersion dv in model.Versions)
                        {
                            if ((dv.Status & DownloadStates.Add) != 0)
                            {
                                Download(dv);
                                continue;
                            }
                            foreach (GXPublishedDeviceType dt2 in dv.Templates)
                            {
                                //If latest device type is marked as download.
                                if ((dt2.Status & DownloadStates.Add) != 0)
                                {
                                    Download(dt2);
                                    continue;
                                }
                                //If version is marked as download.
                                foreach (GXTemplateVersion it in dt2.Versions)
                                {
                                    if ((it.Status & DownloadStates.Add) != 0)
                                    {
                                        Download(it);
                                        //Mark old versions as available not installed.
                                        foreach (GXTemplateVersion tv in dt2.Versions)
                                        {
                                            //If item is installed.
                                            if (it != tv && (tv.Status & DownloadStates.Installed) != 0)
                                            {
                                                UpdateState(false, tv, tv.Status & ~DownloadStates.Installed);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Downloads selected device, model, version or template from Gurux www-server.
        /// </summary>
        public Guid Download(object target)
        {
            if (target is GXDeviceManufacturer)
            {
                foreach (GXDeviceModel it in (target as GXDeviceManufacturer).Models)
                {
                    Download(it);                    
                }
                (target as GXDeviceManufacturer).Status = DownloadStates.None;
            }
            else if (target is GXDeviceModel)
            {
                foreach (GXDeviceVersion it in (target as GXDeviceModel).Versions)
                {
                    Download(it);                    
                }
                (target as GXDeviceModel).Status = DownloadStates.None;
            }
            else if (target is GXDeviceVersion)
            {
                foreach (GXPublishedDeviceType it in (target as GXDeviceVersion).Templates)
                {
                    Download(it);                    
                }
                (target as GXDeviceVersion).Status = DownloadStates.None;
            }
            else if (target is GXPublishedDeviceType)
            {
                GXTemplateVersionCollection versions = (target as GXPublishedDeviceType).Versions;
                (target as GXPublishedDeviceType).DeviceGuid = Download(versions[versions.Count - 1]);
                (target as GXPublishedDeviceType).Status = DownloadStates.None;                
            }
            else if (target is GXTemplateVersion)
            {
                JsonServiceClient Client = new JsonServiceClient(Gurux.DeviceSuite.Properties.Settings.Default.UpdateServer);
                GXDownloadRequest download = new GXDownloadRequest();
                download.Template = target as GXTemplateVersion;
                GXDownloadResponse ret = Client.Get(download);
                GXPublishedDeviceType type = UpdatePublishedDeviceType(download.Template, ret.Data);
                (target as GXTemplateVersion).Status = DownloadStates.Installed;
                return type.Guid;
            }
            else
            {
                throw new Exception("Invalid target.");
            }
            return Guid.Empty;
        }

        GXPublishedDeviceType UpdatePublishedDeviceType(GXTemplateVersion tv, byte[] data)
        {
            GXPublishedDeviceType dt = tv.Parent.Parent;
            GXDeviceVersion dv = dt.Parent.Parent;
            GXDeviceModel model = dv.Parent.Parent;
            GXDeviceManufacturer man = model.Parent.Parent;
            GXPublishedDeviceType type = GXZip.Import(this, data, null) as GXPublishedDeviceType;
            GXDeviceManufacturer ma;
            dt.DeviceGuid = type.DeviceGuid;
            if ((ma = PresetManufacturers.Find(man)) == null)
            {
                GXDeviceManufacturer man2 = new GXDeviceManufacturer(man);
                GXPublishedDeviceTypeCollection templates = man2.Models.Find(model).Versions.Find(dv).Templates;
                templates.Clear();
                templates.Add(dt);
                PresetManufacturers.Add(man2);
            }
            else
            {
                GXDeviceModel mo = ma.Models.Find(model);
                if (mo == null)
                {
                    ma.Models.Add(new GXDeviceModel(model));
                }
                else
                {
                    GXDeviceVersion ve = mo.Versions.Find(dv);
                    if (ve == null)
                    {
                        mo.Versions.Add(new GXDeviceVersion(dv));
                    }
                    else
                    {
                        GXPublishedDeviceType dt3 = ve.Templates.Find(dt.PresetName);
                        if (dt3 == null)
                        {
                            ve.Templates.Add(new GXPublishedDeviceType(dt));
                        }
                        else
                        {
                            dt3.Versions.Clear();
                            dt3.Versions.Add(tv);
                        }
                    }
                }
            }
            return type;
        }

        void UpdateState(bool preset, object target, DownloadStates state)
        {
            TreeNode node = ItemToTreeNode[target] as TreeNode;
            ListViewItem li = ItemToListItem[target] as ListViewItem;
            if (target is GXDeviceManufacturer)
            {
                GXDeviceManufacturer item = target as GXDeviceManufacturer;
                item.Status = state;
                if (li != null)
                {
                    li.SubItems[1].Text = GetStatusText(preset, item.Status);
                }
                if (node != null)
                {
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(item.Status);
                }
                foreach (GXDeviceModel it in item.Models)
                {
                    UpdateState(preset, it, state);
                }
            }
            else if (target is GXDeviceModel)
            {
                GXDeviceModel item = target as GXDeviceModel;
                item.Status = state;
                if (li != null)
                {
                    li.SubItems[1].Text = GetStatusText(preset, item.Status);
                }
                if (node != null)
                {
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(item.Status);
                }
                foreach (GXDeviceVersion it in item.Versions)
                {
                    UpdateState(preset, it, state);
                }
            }
            else if (target is GXDeviceVersion)
            {
                GXDeviceVersion item = target as GXDeviceVersion;
                item.Status = state;
                if (li != null)
                {
                    li.SubItems[1].Text = GetStatusText(preset, item.Status);
                }
                if (node != null)
                {
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(item.Status);
                }
                foreach (GXPublishedDeviceType it in item.Templates)
                {
                    UpdateState(preset, it, state);
                }
            }
            else if (target is GXPublishedDeviceType)
            {
                GXPublishedDeviceType item = target as GXPublishedDeviceType;
                item.Status = state;
                if (li != null)
                {
                    li.SubItems[1].Text = GetStatusText(preset, item.Status);
                }
                if (node != null)
                {
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(item.Status);
                }
            }
            else if (target is GXTemplateVersion)
            {
                GXTemplateVersion item = target as GXTemplateVersion;
                if (item.Status == DownloadStates.Installed && state == DownloadStates.Remove)
                {
                    throw new Exception("Can't disable installed template.");
                }
                item.Status = state;
                if (li != null)
                {
                    li.SubItems[1].Text = GetStatusText(preset, item.Status);
                }
            }
            else
            {
                throw new Exception("Unknown target type.");
            }
        }

        /// <summary>
        /// Mark item as loadable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (PresetTree.Focused)
                {
                    UpdateState(false, PresetTree.SelectedNode.Tag, DownloadStates.Add);
                }
                else if (PresetList.Focused)
                {
                    if (PresetList.SelectedItems.Count != 0)
                    {
                        GXTemplateVersion item = PresetList.SelectedItems[0].Tag as GXTemplateVersion;
                        if (item != null)
                        {
                            foreach (GXTemplateVersion it in item.Parent)
                            {
                                UpdateState(false, it, it.Status & ~DownloadStates.Add);
                            }
                        }
                    }
                    foreach (ListViewItem it in PresetList.SelectedItems)
                    {
                        UpdateState(false, it.Tag, DownloadStates.Add);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        /// <summary>
        /// Disable loadable item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisableMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (PresetTree.Focused)
                {
                    UpdateState(false, PresetTree.SelectedNode.Tag, DownloadStates.Remove);                                        
                }
                else if (PresetList.Focused)
                {
                    foreach (ListViewItem it in PresetList.SelectedItems)
                    {
                        UpdateState(false, it.Tag, DownloadStates.Remove);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        /// <summary>
        /// Enable loadable item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (PresetTree.Focused)
                {
                    UpdateState(false, PresetTree.SelectedNode.Tag, DownloadStates.None);
                }
                else if (PresetList.Focused)
                {
                    foreach (ListViewItem it in PresetList.SelectedItems)
                    {
                        UpdateState(false, it.Tag, DownloadStates.None);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        DownloadStates ToggleState(bool preset, DownloadStates status)
        {
            //If latest version is installed.
            if (status == DownloadStates.None)
            {
                return DownloadStates.Add;
            }
            //Remove disabled status.
            if ((status & DownloadStates.Remove) != 0)
            {
                return status & ~DownloadStates.Remove;
            }
            //Remove download status.
            if ((status & DownloadStates.Add) != 0)
            {
                return DownloadStates.Remove;
            }
            //Do nothing if version is installed.
            if ((status & DownloadStates.Installed) != 0)
            {
                return DownloadStates.Installed;
            }
            throw new Exception("Invalid status.");

        }

        void ToggleState(bool preset, object target)
        {
            ListViewItem li = ItemToListItem[target] as ListViewItem;
            if (target is GXDeviceManufacturer)
            {
                GXDeviceManufacturer item = target as GXDeviceManufacturer;
                item.Status = ToggleState(preset, item.Status);
                UpdateState(preset, target, item.Status);
            }
            else if (target is GXDeviceModel)
            {
                GXDeviceModel item = target as GXDeviceModel;
                item.Status = ToggleState(preset, item.Status);
                UpdateState(preset, target, item.Status);
            }
            else if (target is GXDeviceVersion)
            {
                GXDeviceVersion item = target as GXDeviceVersion;
                item.Status = ToggleState(preset, item.Status);
                UpdateState(preset, target, item.Status);
            }
            else if (target is GXPublishedDeviceType)
            {
                GXPublishedDeviceType item = target as GXPublishedDeviceType;
                item.Status = ToggleState(preset, item.Status);
                UpdateState(preset, target, item.Status);
            }
            else if (target is GXTemplateVersion)
            {
                GXTemplateVersion item = target as GXTemplateVersion;
                item.Status = ToggleState(preset, item.Status);
                UpdateState(preset, target, item.Status);
            }
            else
            {
                throw new Exception("Unknown target type.");
            }
        }

        /// <summary>
        /// Toggle state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == LoadablePage)
                {
                    foreach (ListViewItem it in PresetList.SelectedItems)
                    {
                        ToggleState(false, it.Tag);
                    }
                }
                else //If preset page.
                {
                    PresetPropertiesMenu_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        object GetSelectedNode()
        {
            TreeNode node = PresetTree.SelectedNode;
            if (node != null)
            {
                return node.Tag;
            }
            return null;
        }

        /// <summary>
        /// Show previous versions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPreviouslyVersionsCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                object selectedItem = GetSelectedNode();
                UpdateView(tabControl1.SelectedTab == PresetPage);
                if (selectedItem != null)
                {
                    PresetTree.SelectedNode = ItemToTreeNode[selectedItem] as TreeNode;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }            
        }

        /// <summary>
        /// Show disabled versions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDisabledCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                object selectedItem = GetSelectedNode();
                UpdateView(tabControl1.SelectedTab == PresetPage);
                if (selectedItem != null)
                {
                    PresetTree.SelectedNode = ItemToTreeNode[selectedItem] as TreeNode;
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this.Parent, ex);
            }
        }

        private void GXPresetDevicesForm_Load(object sender, EventArgs e)
        {
            if (!InitializePreset)
            {
                tabControl1.SelectedTab = LoadablePage;
            }
            else
            {
                UpdateView(InitializePreset);
            }
        }        
    }
}
