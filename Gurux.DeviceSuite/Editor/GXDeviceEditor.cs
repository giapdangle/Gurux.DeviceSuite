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
using Gurux.DeviceSuite.GXWizard;
using Gurux.Device.Editor;
using System.IO;
using Gurux.DeviceSuite.Import;
using Gurux.DeviceSuite.Manufacturer;
using Gurux.DeviceSuite.Common;
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Editor
{
    public partial class GXDeviceEditor : Form
    {
        bool hex = true;
        List<TraceEventArgs> TraceEvents = new List<TraceEventArgs>();
        bool TracePause = false;
        bool TraceFollowLast = true;
        public System.Diagnostics.TraceLevel TraceLevel = System.Diagnostics.TraceLevel.Off;
        public event DirtyEventHandler OnDirty;
        public event System.EventHandler OnItemActivated;
        System.Collections.Hashtable ObjectToTreeNode = new System.Collections.Hashtable();
        public GXDeviceManufacturerCollection Manufacturers = new GXDeviceManufacturerCollection();        
        MainForm ParentComponent;
        GXDevice Device;
        public GXDeviceEditor(MainForm parent)
        {            
            InitializeComponent();
            ParentComponent = parent;
            this.TopLevel = false;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            LoadDefaultImages();
        }

        /// <summary>
        /// Is Device Profile support import.
        /// </summary>
        public bool IsImportSupported()
        {
            if (Device != null)
            {
                if (!Device.ImportFromDeviceEnabled)
                {
                    return false;
                }
                return Device.AddIn.ImportFromDeviceEnabled;
            }
            return false;
        }

        /// <summary>
        /// Return device type.
        /// </summary>
        public string FileName
        {
            get
            {
                if (Device == null)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(Device.PresetName))
                {
                    return Device.Name + " [" + Device.DeviceProfile + "]";
                }                
                return Device.PresetName + " [" + Device.Manufacturer + " " + Device.Model + "]";
            }
        }

        public void CloseFile()
        {
            try
            {
                Clear();
                if (Device != null)
                {
                    Device.Dispose();
                    Device = null;
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Is device dirty.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (Device == null)
                {
                    return false;
                }
                return Device.Dirty;
            }
        }

        /// <summary>
        /// Load default images to the image list.
        /// </summary>
        public void LoadDefaultImages()
        {
            PropertyTree.ImageList = new ImageList();
            //Load the image for the device list.
            System.Drawing.Bitmap bm = Gurux.DeviceSuite.Properties.Resources.DeviceConnected;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for device tables.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTables;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for the device table.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceTable;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for device categories.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategories;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for the device category.			            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceCategory;
            bm.MakeTransparent(System.Drawing.Color.FromArgb(255, 0, 255));
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for device properties.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperties;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the image for the device property.			
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceProperty;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the error image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyError;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the changed image for the device property.            
            bm = Gurux.DeviceSuite.Properties.Resources.DevicePropertyChanged2;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
            //Load the notifications image for the device.            
            bm = Gurux.DeviceSuite.Properties.Resources.DeviceNotifies;
            bm.MakeTransparent();
            PropertyTree.ImageList.Images.Add(bm);
        }

        public void Delete()
        {
            //TODO: ObjectToTreeNode poista
            try
            {
                //Delete trace items.
                if (TraceView.Focused)
                {
                    TraceEvents.Clear();
                    TraceView.VirtualListSize = 0;
                    return;
                }
                if (PropertyTree.Focused)
                {
                    if (PropertyTree.SelectedNode == null ||
                        PropertyTree.SelectedNode.Tag == null)
                    {
                        return;
                    }
                    object source = PropertyTree.SelectedNode.Tag;
                    //Remove Category
                    if (source is GXCategory)
                    {
                        GXCategory cat = source as GXCategory;
                        string txt1 = string.Format(Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt, source.ToString());
                        DialogResult res = GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, txt1);
                        if (res != DialogResult.Yes)
                        {
                            return;
                        }
                        Device.Categories.Remove(cat);
                        NotifyDirty();
                    }
                    //Remove Table
                    else if (source is GXTable)
                    {
                        GXTable table = source as GXTable;
                        string txt1 = string.Format(Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt, table.Name);
                        DialogResult res = GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, txt1);
                        if (res != DialogResult.Yes)
                        {
                            return;
                        }
                        Device.Tables.Remove(table);
                        NotifyDirty();
                    }
                    //Remove Property		
                    else if (source is GXProperty)
                    {
                        string txt1 = string.Format(Gurux.DeviceSuite.Properties.Resources.RemoveItemTxt, source.ToString());
                        DialogResult res = GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, txt1);
                        if (res != DialogResult.Yes)
                        {
                            return;
                        }
                        GXProperty prop = (GXProperty)source;
                        if (prop.Category != null)
                        {
                            prop.Category.Properties.Remove(prop);
                        }
                        NotifyDirty();
                    }
                    else
                    {
                        if (PropertyTree.SelectedNode.Nodes.Count != 0)
                        {
                            DialogResult res = DialogResult.None;
                            GXCategoryCollection cats = null;
                            GXTableCollection tables = null;
                            if (source is GXCategoryCollection)
                            {
                                res = GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveAllCategoriesTxt);
                                cats = source as GXCategoryCollection;
                            }
                            else
                            {
                                res = GXCommon.ShowExclamation(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.RemoveAllTablesTxt);
                                tables = source as GXTableCollection;
                            }
                            if (res != DialogResult.Yes)
                            {
                                return;
                            }
                            while (PropertyTree.SelectedNode.Nodes.Count != 0)
                            {
                                TreeNode node = PropertyTree.SelectedNode.Nodes[0];
                                object item = node.Tag;
                                if (tables != null)
                                {
                                    tables.Remove(item);
                                }
                                else if (cats != null)
                                {
                                    cats.Remove(item);
                                }
                                node.Remove();
                            }
                            NotifyDirty();
                        }
                        return;
                    }
                    PropertyTree.SelectedNode.Remove();                    
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Save device template.
        /// </summary>
        public void Save()
        {
            try
            {
                Device.Save(Device.ProfilePath);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        public void NewObject()
        {
            try
            {
                if (PropertyTree.SelectedNode == null ||
                    PropertyTree.SelectedNode.Tag == null)
                {
                    return;
                }
                GXProtocolAddIn AddIn = Device.AddIn;
                TreeNode parent = null;
                object source = PropertyTree.SelectedNode.Tag;
                //Add Category
                if (source is GXCategoryCollection)
                {
                    GXCategoryCollection cats = source as GXCategoryCollection;
                    GXCategory[] itemArray = AddIn.GetCategories(cats);
                    if (itemArray.Length == 0)
                    {
                        throw new Exception("No category types found.");
                    }
                    GXCategory cat = itemArray[0];
                    parent = PropertyTree.SelectedNode;
                    GXWizardDlg dlg = new GXWizardDlg(cat, cats.Parent, AddIn);
                    if (dlg.ShowDialog(ParentComponent) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.ShowProperties(Device);
                    NotifyDirty();
                }
                //Add Table
                else if (source is GXTableCollection)
                {
                    GXTableCollection tables = source as GXTableCollection;
                    GXTable[] itemArray = AddIn.GetTables(tables);
                    if (itemArray.Length == 0)
                    {
                        throw new Exception("No table types found.");
                    }
                    GXTable table = itemArray[0];
                    parent = PropertyTree.SelectedNode;
                    GXWizardDlg dlg = new GXWizardDlg(table, tables.Parent, AddIn);
                    if (dlg.ShowDialog(ParentComponent) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.ShowProperties(Device);
                    NotifyDirty();
                }
                //Add Property
                else if (source is GXProperty || source is GXCategory || source is GXTable)
                {
                    GXCategory cat = null;
                    GXTable table = null;
                    object propertyParent = null;
                    if (source is GXProperty)
                    {
                        table = ((GXProperty)source).Table;
                        cat = ((GXProperty)source).Category;
                        if (table != null)
                        {
                            propertyParent = table;
                            parent = ObjectToTreeNode[table] as TreeNode;
                        }
                        else if (cat != null)
                        {
                            propertyParent = cat;
                            parent = ObjectToTreeNode[cat] as TreeNode;
                        }
                    }
                    else if (source is GXCategory)
                    {
                        propertyParent = cat = (GXCategory)source;
                        parent = ObjectToTreeNode[cat] as TreeNode;
                    }
                    else if (source is GXTable)
                    {
                        propertyParent = table = (GXTable)source;
                        parent = ObjectToTreeNode[table] as TreeNode;
                    }
                    else
                    {
                        //Shouldn't get here
                        throw new Exception("Unknown error while adding property.");
                    }
                    GXProperty[] itemArray = AddIn.GetProperties(source);
                    if (itemArray.Length == 0)
                    {
                        throw new Exception("No property types found.");
                    }
                    GXProperty prop = itemArray[0];
                    GXWizardDlg dlg = new GXWizardDlg(prop, propertyParent, AddIn);
                    if (dlg.ShowDialog(ParentComponent) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.ShowProperties(Device);
                    NotifyDirty();
                }
                else
                {
                    throw new Exception("Unknown type to add.");
                }
                parent.Expand();
                ValidateTasks();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Create new device template.
        /// </summary>
        public void NewTemplate()
        {
            try
            {
                //Ask save settings if device template is dirty.
                if (Device != null && Device.Dirty)
                {
                    DialogResult ret = MessageBox.Show(ParentComponent, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.YesNoCancel);
                    if (ret == DialogResult.Cancel)
                    {
                        return;
                    }
                    else if (ret == DialogResult.Yes)
                    {
                        Save();
                    }
                }
                GXWizardDlg dlg = new GXWizardDlg(null, Manufacturers, null);
                if (dlg.ShowDialog(ParentComponent) == DialogResult.Cancel)
                {
                    return;
                }                
                Device = dlg.Target as GXDevice;
                Device.Save(Device.ProfilePath);                
                Device.Register();                
                ShowProperties(Device);                
                ParentComponent.CloseMenu.Enabled = true;
                ParentComponent.ExportMenu.Enabled = !System.Diagnostics.Debugger.IsAttached;
                ParentComponent.ImportMenu.Enabled = Device.AddIn.ImportFromDeviceEnabled;
                PropertyTree.SelectedNode = PropertyTree.Nodes[0];
                PropertyTree.Focus();                
                ValidateTasks();                
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void Open()
        {
            //Ask save settings if device template is dirty.
            if (Device != null && Device.Dirty)
            {
                DialogResult ret = MessageBox.Show(ParentComponent, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.YesNoCancel);
                if (ret == DialogResult.Cancel)
                {
                    return;
                }
                else if (ret == DialogResult.Yes)
                {
                    Save();
                }
            }
            OpenDlg dlg = new OpenDlg(Manufacturers, null);
            if (dlg.ShowDialog(ParentComponent) == DialogResult.OK)
            {                
                LoadDevice(dlg.Checked);
            }
        }

        void Clear()
        {
            TraceEvents.Clear();
            TraceView.VirtualListSize = 0;
            PropertyGrid.SelectedObjects = null;
            TaskView.Items.Clear();            
            ObjectToTreeNode.Clear();
            PropertyTree.Nodes.Clear();
        }

        void ValidateTasks()
        {
            TaskView.Items.Clear();
            //Validate tasks.
            GXTaskCollection tasks = new GXTaskCollection();
            Device.Validate(true, tasks);
            foreach (GXCategory cat in Device.Categories)
            {
                cat.Validate(true, tasks);
            }
            foreach (GXTable table in Device.Tables)
            {
                table.Validate(true, tasks);
            }
            foreach (GXTask it in tasks)
            {
                ListViewItem li = TaskView.Items.Add(it.Source.ToString());
                li.SubItems.Add(it.Description);
                li.Tag = it;
            }
        }

        private void LoadDevice(GXDeviceProfile deviceType)
        {
            try
            {
                if (deviceType == null)
                {
                    throw new Exception("LoadDevice failed: DeviceType is null");
                }
                this.Cursor = Cursors.WaitCursor;
                Clear();
                Device = GXDevice.Load(deviceType.Path);
                ShowProperties(Device);
                ParentComponent.CloseMenu.Enabled = true;                
                ParentComponent.ImportMenu.Enabled = Device.AddIn.ImportFromDeviceEnabled;
                Device.Dirty = false;
                ValidateTasks();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void ShowProperties(GXDevice device)
        {
            PropertyTree.Nodes.Clear();
            GXProtocolAddIn AddIn = device.AddIn;
            GXProtocolAddIn.VisibilityItems func = AddIn.ItemVisibility;
            //Show device properties
            TreeNode DevNode = PropertyTree.Nodes.Add(device.DeviceProfile);
            DevNode.SelectedImageIndex = DevNode.ImageIndex = 0;
            DevNode.Tag = device;
            ObjectToTreeNode[device] = DevNode;
            if ((func & GXProtocolAddIn.VisibilityItems.Tables) != 0)
            {
                //Add tables
                TreeNode TablesNode = DevNode.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.TablesTxt);
                TablesNode.Tag = device.Tables;
                ObjectToTreeNode[device.Tables] = TablesNode;
                TablesNode.SelectedImageIndex = TablesNode.ImageIndex = 1;
                foreach (GXTable table in device.Tables)
                {
                    TreeNode TableNode = TablesNode.Nodes.Add(table.Name);
                    TableNode.SelectedImageIndex = TableNode.ImageIndex = 2;
                    TableNode.Tag = table;
                    ObjectToTreeNode[table] = TableNode;
                }
            }

            //Add categories
            if ((func & GXProtocolAddIn.VisibilityItems.Categories) != 0)
            {
                TreeNode CatsNode = DevNode.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.CategoriesTxt);
                CatsNode.Tag = device.Categories;
                ObjectToTreeNode[device.Categories] = CatsNode;
                CatsNode.SelectedImageIndex = CatsNode.ImageIndex = 3;
                foreach (GXCategory cat in device.Categories)
                {
                    TreeNode CatNode = CatsNode.Nodes.Add(cat.Name);
                    CatNode.SelectedImageIndex = CatNode.ImageIndex = 4;
                    CatNode.Tag = cat;
                    ObjectToTreeNode[cat] = CatNode;
                    //Add properties					
                    foreach (GXProperty Prop in cat.Properties)
                    {
                        TreeNode PropNode = CatNode.Nodes.Add(Prop.Name);
                        PropNode.SelectedImageIndex = PropNode.ImageIndex = -1;
                        PropNode.Tag = Prop;
                        ObjectToTreeNode[Prop] = PropNode;
                    }
                }
            }
            //Add notifies
            if ((func & GXProtocolAddIn.VisibilityItems.Events) != 0)
            {
                TreeNode NotifiesNode = DevNode.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.NotifiesTxt);
                NotifiesNode.SelectedImageIndex = NotifiesNode.ImageIndex = 5;
                NotifiesNode.Tag = device.Events;
                ObjectToTreeNode[device.Events] = NotifiesNode;
                //New way.
                if ((func & GXProtocolAddIn.VisibilityItems.Tables) != 0)
                {
                    //Add tables
                    TreeNode TablesNode = NotifiesNode.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.TablesTxt);
                    TablesNode.Tag = device.Events.Tables;
                    ObjectToTreeNode[device.Events.Tables] = TablesNode;
                    TablesNode.SelectedImageIndex = TablesNode.ImageIndex = 1;
                    foreach (GXTable table in device.Events.Tables)
                    {
                        TreeNode TableNode = TablesNode.Nodes.Add(table.Name);
                        TableNode.SelectedImageIndex = TableNode.ImageIndex = 2;
                        TableNode.Tag = table;
                        ObjectToTreeNode[table] = TableNode;
                    }
                }

                //Add categories
                if ((func & GXProtocolAddIn.VisibilityItems.Categories) != 0)
                {
                    TreeNode CatsNode = NotifiesNode.Nodes.Add(Gurux.DeviceSuite.Properties.Resources.CategoriesTxt);
                    CatsNode.Tag = device.Events.Categories;
                    ObjectToTreeNode[device.Events.Categories] = CatsNode;
                    CatsNode.SelectedImageIndex = CatsNode.ImageIndex = 3;
                    foreach (GXCategory cat in device.Events.Categories)
                    {
                        TreeNode CatNode = CatsNode.Nodes.Add(cat.Name);
                        CatNode.SelectedImageIndex = CatNode.ImageIndex = 4;
                        CatNode.Tag = cat;
                        ObjectToTreeNode[cat] = CatNode;
                        //Add properties					
                        foreach (GXProperty Prop in cat.Properties)
                        {
                            TreeNode PropNode = CatNode.Nodes.Add(Prop.Name);
                            PropNode.SelectedImageIndex = PropNode.ImageIndex = -1;
                            PropNode.Tag = Prop;
                            ObjectToTreeNode[Prop] = PropNode;
                        }
                    }
                }
            }
            if (PropertyTree.Nodes.Count > 0)
            {
                PropertyTree.Nodes[0].ExpandAll();
            }
        }

        /// <summary>
        /// Load application settings.
        /// </summary>
        public void LoadSettings()
        {
            if (Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeWidth != -1)
            {
                EditorPanel.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeWidth;
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.EditorHeight != -1)
            {
                splitContainer1.SplitterDistance = Gurux.DeviceSuite.Properties.Settings.Default.EditorHeight;
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.TraceViewWidth != -1)
            {
                TraceView.Width = Gurux.DeviceSuite.Properties.Settings.Default.TraceViewWidth;
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.TaskViewColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.TaskViewColumns)
                {
                    TaskView.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
            if (Gurux.DeviceSuite.Properties.Settings.Default.TraceViewColumns != null)
            {
                int pos = 0;
                foreach (string it in Gurux.DeviceSuite.Properties.Settings.Default.TraceViewColumns)
                {
                    TraceView.Columns[pos].Width = int.Parse(it);
                    ++pos;
                }
            }
        }

        /// <summary>
        /// Save application settings.
        /// </summary>
        public void SaveSettings()
        {
            Gurux.DeviceSuite.Properties.Settings.Default.EditorHeight = splitContainer1.SplitterDistance;
            Gurux.DeviceSuite.Properties.Settings.Default.DeviceTreeWidth = EditorPanel.SplitterDistance;
            Gurux.DeviceSuite.Properties.Settings.Default.TraceViewWidth = TraceView.Width;

            List<string> columns = new List<string>();
            ////////////////////////////////////////////////////////
            //Save widths of task view list columns.
            foreach (ColumnHeader it in TaskView.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.TaskViewColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.TaskViewColumns.AddRange(columns.ToArray());
            columns.Clear();
            ////////////////////////////////////////////////////////
            //Save widths of trace view list columns.
            foreach (ColumnHeader it in TraceView.Columns)
            {
                columns.Add(it.Width.ToString());
            }
            Gurux.DeviceSuite.Properties.Settings.Default.TraceViewColumns = new System.Collections.Specialized.StringCollection();
            Gurux.DeviceSuite.Properties.Settings.Default.TraceViewColumns.AddRange(columns.ToArray());            
        }

        /// <summary>
        /// Create new object.
        /// </summary>
        private void NewMnu_Click(object sender, EventArgs e)
        {
            NewObject();
        }

        void EditObject(object target)
        {
            try
            {                                
                GXWizardDlg dlg = new GXWizardDlg(target, null, Device.AddIn);             
                if (dlg.ShowDialog(ParentComponent) == DialogResult.Cancel)
                {
                    return;
                }
                this.ShowProperties(Device);
                NotifyDirty();            
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Edit selected object.
        /// </summary>
        private void PropertiesMenu_Click(object sender, EventArgs e)
        {
            EditObject(PropertyTree.SelectedNode.Tag);
        }

        /// <summary>
        /// Delete selected object and it's child objects.
        /// </summary>
        private void RemoveMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Expand selected object.
        /// </summary>
        private void ExpandMenu_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyTree.SelectedNode.Expand();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Collapse selected object.
        /// </summary>
        private void CollapseMenu_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyTree.SelectedNode.Collapse();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Disable edit, remove, expand and collapse if node is not selected.
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode node = PropertyTree.SelectedNode;
            if (node != null)
            {
                object target = node.Tag;                
                bool device = target is GXDevice;
                if (device)
                {
                    NewMnu.Enabled = RemoveMenu.Enabled = false;
                    PropertiesMenu.Enabled = true;
                }
                else 
                {
                    Gurux.Device.Editor.GXProtocolAddIn.Functionalities func = Device.AddIn.GetFunctionalities(target);
                    NewMnu.Enabled = (func & GXProtocolAddIn.Functionalities.Add) != 0;
                    PropertiesMenu.Enabled = (func & GXProtocolAddIn.Functionalities.Edit) != 0;
                    RemoveMenu.Enabled = (func & GXProtocolAddIn.Functionalities.Remove) != 0;
                }                
                ExpandMenu.Enabled = CollapseMenu.Enabled = !(node.Tag is GXProperty);
            }
            else
            {
                NewMnu.Enabled = PropertiesMenu.Enabled = RemoveMenu.Enabled = ExpandMenu.Enabled = CollapseMenu.Enabled = false;
            }
        }

        /// <summary>
        /// Show properties of selected object in the property grid.
        /// </summary>        
        private void PropertyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                PropertyGrid.SelectedObject = PropertyTree.SelectedNode.Tag;
                if (PropertyTree.SelectedNode.Tag is GXDevice)
                {
                    ParentComponent.OnActivated(null, null);
                }
                else
                {
                    ParentComponent.OnActivated(PropertyTree, null);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        void NotifyDirty()
        {
            if (!Device.Dirty)
            {
                Device.Dirty = true;
                if (OnDirty != null)
                {
                    OnDirty(this, new GXDirtyEventArgs(this, true));
                }
            }
        }

        /// <summary>
        /// User has change value.
        /// </summary>
        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                NotifyDirty();
                ValidateTasks();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        //Find the GridItem with the given label.
        GridItem FindGridItem(GridItem node, string label)
        {
            if (string.Compare(node.Label, label, true) == 0)
            {
                return node;
            }
            foreach (GridItem it in node.GridItems)
            {
                if ((node = FindGridItem(it, label)) != null)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Export device template.
        /// </summary>
        public void Export()
        {
            try
            {
                //Ask save settings if device template is dirty.
                if (Device != null && Device.Dirty)
                {
                    DialogResult ret = GXCommon.ShowQuestion(ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt);
                    if (ret == DialogResult.Cancel)
                    {
                        return;
                    }
                    else if (ret == DialogResult.Yes)
                    {
                        Save();
                    }
                }
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = Gurux.DeviceSuite.Properties.Resources.ExportedFilesFileTxt + Gurux.DeviceSuite.Properties.Resources.AllFilesTxt;
                dlg.ValidateNames = true;
                dlg.DefaultExt = "gxz";
                dlg.FileName = Device.DeviceProfile;
                if (dlg.ShowDialog(ParentComponent) == DialogResult.OK)
                {
                    string Path = dlg.FileName;
                    GXZip.Export(Device, Path);
                    MessageBox.Show("\"" + dlg.FileName + "\" " + Gurux.DeviceSuite.Properties.Resources.IsExportedTxt, Gurux.DeviceSuite.Properties.Resources.DoneTxt);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Open exported device template.
        /// </summary>
        public void OpenExported()
        {
            try
            {
                //Ask save settings if device template is dirty.
                if (Device != null && Device.Dirty)
                {
                    DialogResult ret = MessageBox.Show(ParentComponent, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.YesNoCancel);
                    if (ret == DialogResult.Cancel)
                    {
                        return;
                    }
                    else if (ret == DialogResult.Yes)
                    {
                        Save();
                    }
                }
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt;
                dlg.Multiselect = false;
                dlg.AddExtension = dlg.CheckFileExists = dlg.CheckPathExists = true;
                dlg.Filter = Gurux.DeviceSuite.Properties.Resources.ExportedFilesFileTxt + Gurux.DeviceSuite.Properties.Resources.AllFilesTxt;
                if (dlg.ShowDialog(ParentComponent) != DialogResult.OK)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                GXDeviceProfile gxtype = GXZip.Import(this, dlg.FileName);
                if (gxtype == null)
                {
                    MessageBox.Show(this, Gurux.DeviceSuite.Properties.Resources.RestartToUpdateAddins, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.OK);
                    if (!System.Diagnostics.Debugger.IsAttached)
                    {
                        Application.Restart();
                    }
                    return;
                }
                GXDeviceList.Update();
                LoadDevice(gxtype);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void Import(bool fromDataCollector)
        {
            try
            {
                //Ask save settings if device template is dirty.
                if (Device != null && Device.Dirty)
                {
                    DialogResult ret = MessageBox.Show(ParentComponent, Gurux.DeviceSuite.Properties.Resources.SaveChangesQuestionTxt, Gurux.DeviceSuite.Properties.Resources.DeviceEditorTxt, MessageBoxButtons.YesNoCancel);
                    if (ret == DialogResult.Cancel)
                    {
                        return;
                    }
                    else if (ret == DialogResult.Yes)
                    {
                        Save();
                    }
                }
                GXDevice tmpDev = Device.Clone();
                tmpDev.Categories.Clear();
                tmpDev.Tables.Clear();
                GXImportDlg dlg = new GXImportDlg(TraceLevel, tmpDev.AddIn, tmpDev, fromDataCollector);
                dlg.OnTrace += new TraceEventHandler(OnTrace);
                if (dlg.ShowDialog(ParentComponent) != DialogResult.OK)
                {
                    dlg.OnTrace -= new TraceEventHandler(OnTrace);
                    tmpDev.Dispose();
                    return;
                }                
                //Copy device properties from temp device to original device because they might change on import.
    			PropertyDescriptorCollection Props = TypeDescriptor.GetProperties(Device);
		        foreach (PropertyDescriptor it in Props)
		        {
                    try
                    {
                        if (!it.IsReadOnly && it.SerializationVisibility != DesignerSerializationVisibility.Hidden)
                        {
                            object value = it.GetValue(tmpDev);
                            object orig = it.GetValue(Device);
                            if (!object.Equals(value, orig))
                            {
                                it.SetValue(Device, value);
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
		        }
                Device.Keepalive.Ignore = tmpDev.Keepalive.Ignore;
                Device.Keepalive.Interval = tmpDev.Keepalive.Interval;
                Device.Keepalive.Target = tmpDev.Keepalive.Target;
                dlg.OnTrace -= new TraceEventHandler(OnTrace);
                //Add device description
                Device.Dirty = true;
                Device.Description = tmpDev.Description;
                //Add new categories
                foreach (GXCategory cat in tmpDev.Categories)
                {
                    if (Device.Categories.Find(cat.Name) == null)
                    {
                        Device.Categories.Add(cat);
                    }
                    else
                    {
                        GXCategory TargetCat = (GXCategory)Device.Categories.Find(cat.Name);
                        foreach (GXProperty prop in cat.Properties)
                        {
                            if (TargetCat.Properties.Find(prop.Name, null) == null)
                            {
                                TargetCat.Properties.Add(prop);
                            }
                        }
                    }
                }
                //Add new notify categories
                if (tmpDev.Events != null)
                {
                    foreach (GXCategory cat in tmpDev.Events.Categories)
                    {
                        if (Device.Events.Categories.Find(cat.Name) == null)
                        {
                            Device.Events.Categories.Add(cat);
                        }
                        else
                        {
                            GXCategory TargetCat = (GXCategory)Device.Events.Categories.Find(cat.Name);
                            foreach (GXProperty prop in cat.Properties)
                            {
                                if (TargetCat.Properties.Find(prop.Name, null) == null)
                                {
                                    TargetCat.Properties.Add(prop);
                                }
                            }
                        }
                    }
                }
                //Add new tables
                foreach (GXTable table in tmpDev.Tables)
                {
                    if (Device.Tables.Find(table.Name) == null)
                    {
                        Device.Tables.Add(table);
                    }                   
                }
                if (tmpDev.Events != null)
                {
                    //Add new notify tables
                    foreach (GXTable table in tmpDev.Events.Tables)
                    {
                        if (Device.Events.Tables.Find(table.Name) == null)
                        {
                            Device.Events.Tables.Add(table);
                        }
                    }
                }
                //Update view.
                ShowProperties(Device);
                ValidateTasks();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        void OnTrace(object sender, TraceEventArgs e)
        {
            //Are media sent and received bytes shown.
            if (!ParentComponent.m_ShowMediaTrace && sender is IGXMedia)
            {                
                if ((e.Type & (TraceTypes.Sent | TraceTypes.Received)) != 0)
                {
                    return;
                }
            }
            if (InvokeRequired)
            {               
                this.BeginInvoke(new TraceEventHandler(OnTrace), new object[] { sender, e });
                return;
            }
            lock (TraceEvents)
            {
                TraceEvents.Add(e);
                TraceView.VirtualListSize = TraceEvents.Count;
                if (TraceFollowLast)
                {                    
                    TraceView.EnsureVisible(TraceEvents.Count - 1);
                }
            }
        }

        /// <summary>
        /// Show selected issue on the property grid.
        /// </summary>
        private void TaskView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (TaskView.SelectedItems.Count == 1)
                {
                    GXTask it = (GXTask)TaskView.SelectedItems[0].Tag;
                    PropertyTree.SelectedNode = ObjectToTreeNode[it.Source] as TreeNode;
                    GridItem node = FindGridItem(PropertyGrid.SelectedGridItem.Parent, it.FailedProperty);
                    if (node != null)
                    {
                        PropertyGrid.SelectedGridItem = node;
                        if (node.Expandable)
                        {
                            node.Expanded = true;
                        }
                        PropertyGrid.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }

        }

        private void TraceMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = TraceView.SelectedItems.Count == 0;
        }

        /// <summary>
        /// Called when user selects new item.
        /// Menu items are enabled/disabled debending what is selected.
        /// </summary>
        private void PropertyGrid_Enter(object sender, EventArgs e)
        {
            if (OnItemActivated != null)
            {
                OnItemActivated(sender, e);
            }
        }

        /// <summary>
        /// Pause trace.
        /// </summary>
        private void TracePauseMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TracePause = TracePauseMenu.Checked = !TracePauseMenu.Checked;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Copy selected trace items to the clipboard.
        /// </summary>
        private void TraceCopyMenu_Click(object sender, EventArgs e)
        {
            try
            {                
                ClipboardCopy.CopyDataToClipboard(TraceView);
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Is last trace item followed.
        /// </summary>
        private void TraceFollowLastMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TraceFollowLast = TraceFollowLastMenu.Checked = !TraceFollowLastMenu.Checked;
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Clear trace view.
        /// </summary>
        private void TraceClearMenu_Click(object sender, EventArgs e)
        {
            try
            {
                lock (TraceEvents)
                {
                    TraceEvents.Clear();
                    TraceView.VirtualListSize = 0;
                }                
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(ParentComponent, Ex);
            }
        }

        /// <summary>
        /// Get trace item to draw.
        /// </summary>
        private void TraceView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            lock (TraceEvents)
            {
                if (e.ItemIndex < TraceEvents.Count)
                {
                    TraceEventArgs ei = TraceEvents[e.ItemIndex];
                    ListViewItem item = new ListViewItem(ei.Timestamp.ToString("hh:mm:ss.fff"));
                    item.SubItems.AddRange(new string[] { ei.Type.ToString(), ei.DataToString(!hex) });
                    e.Item = item;
                }
            }
        }

        private void TaskView_Enter(object sender, EventArgs e)
        {
            if (OnItemActivated != null)
            {
                OnItemActivated(sender, e);
            }
        }

        private void TraceView_Enter(object sender, EventArgs e)
        {
            if (OnItemActivated != null)
            {
                OnItemActivated(sender, e);
            }
        }

        private void TraceHexMenu_Click(object sender, EventArgs e)
        {
            try
            {
                hex = TraceHexMenu.Checked = !TraceHexMenu.Checked;
                TraceView.Refresh();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }            
        }

        private void SelectAllMenu_Click(object sender, EventArgs e)
        {
            try
            {
                TraceView.SelectedIndices.Clear();
                for (int pos = 0; pos != TraceEvents.Count; ++pos)
                {
                    TraceView.SelectedIndices.Add(pos);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(this.ParentComponent, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, Ex);
            }
        }
    }
}
