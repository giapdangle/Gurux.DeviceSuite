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

namespace Gurux.DeviceSuite.Director
{
    public partial class ModifyValueDialog : Form
    {
        private DisabledAction m_DisActions;
        private bool m_UseCombobox = false;
        private bool m_UseBitMask = false;
        bool m_UIValue = false;
        Gurux.Device.GXProperty m_TmpProperty = null;
        private Gurux.Device.GXProperty m_Property;

        public ModifyValueDialog(Gurux.Device.GXProperty property, bool editable, bool uiValue)
        {
            try
            {
                Type type = property.ValueType;
                m_TmpProperty = property.Clone();
                m_UIValue = uiValue;
                InitializeComponent();

                btnCancel.Text = Gurux.DeviceSuite.Properties.Resources.CancelTxt;
                btnOK.Text = Gurux.DeviceSuite.Properties.Resources.OKTxt;
                lblValue.Text = Gurux.DeviceSuite.Properties.Resources.ValueTxt;
                this.GeneralTab.Text = Gurux.DeviceSuite.Properties.Resources.GeneralTxt;
                this.Text = Gurux.DeviceSuite.Properties.Resources.EditValueTxt + " - " + property.Name;

                m_Property = property;
                // Check th eproperty type, i.e. do we use the textbox or dropdown list.
                m_UseBitMask = m_Property.BitMask;
                m_UseCombobox = !m_UseBitMask;
                if (!m_UseBitMask && type == typeof(bool) || property.Values.Count != 0)
                {
                    m_UseCombobox = true;
                }

                //Disable OK Btn if property is not settable.
                Gurux.Device.AccessMode Mode = property.AccessMode;
                bool readOnly = (property.Device.Status & Gurux.Device.DeviceStates.Connected) == 0 || (Mode & Gurux.Device.AccessMode.Write) == 0;
                txtValue.ReadOnly = readOnly;
                ValueLB.Enabled = cbValue.Enabled = !readOnly;
                ValueLB.Visible = m_UseBitMask;

                if (m_UseCombobox)
                {
                    if (type == typeof(bool))
                    {
                        cbValue.Items.Add(bool.FalseString);
                        cbValue.Items.Add(bool.TrueString);
                    }
                    else
                    {
                        foreach (object it in m_Property.Values)
                        {
                            cbValue.Items.Add(it.ToString());
                        }
                    }
                    string str = Convert.ToString(property.GetValue(m_UIValue));
                    cbValue.SelectedIndex = cbValue.FindStringExact(str);
                    if (m_Property.ForcePresetValues || type == typeof(bool))
                    {
                        cbValue.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        cbValue.DropDownStyle = ComboBoxStyle.DropDown;
                        cbValue.Text = Convert.ToString(property.GetValue(m_UIValue));
                    }
                }
                else
                {
                    if (m_UseBitMask)
                    {
                        txtValue.ReadOnly = true;
                        /* TODO:
						long val = Convert.ToInt64(m_Property.GetValue(true));
						foreach (Gurux.Device.GXValue it in m_Property.LocalizedValueItems)
						{
							long tmp = Convert.ToInt64(it.DeviceValue);
							ListViewItem item = ValueLB.Items.Add(it.UIValue.ToString());
							item.Checked = (val & tmp) != 0;
							item.Tag = tmp;
						}
                         * */
                    }
                    //If data type is string show multiline textbox.
                    txtValue.Multiline = type == typeof(string);
                    HexCB.Checked = (m_Property.DisplayType != DisplayTypes.None &&
                                type != typeof(string)) ||
                                type.IsArray;
                    txtValue.Text = HexCB.Checked ? property.GetValueAsHex(m_UIValue) : Convert.ToString(property.GetValue(m_UIValue));
                }
                // Hide the textbox and show the combo box.
                cbValue.Visible = m_UseCombobox;
                ValueLB.Visible = m_UseBitMask;
                txtValue.Visible = !m_UseCombobox;
                HexCB.Visible = !m_UseBitMask && !m_UseCombobox;//TODO: && m_Property.IsTypeNumeric;
                //Add disabled actions.
                //TODO: if (!m_Property.IsNotify)
                {
                    m_DisActions = new DisabledAction(property.DisabledActions);
                    tabControl1.TabPages.Add(m_DisActions.DisabledActionsTB);
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Update the value.
                if (m_UseCombobox)
                {
                    m_Property.SetValue(cbValue.Text, m_UIValue, Gurux.Device.PropertyStates.ValueChangedByUser);
                }
                else if (m_UseBitMask)
                {
                    m_Property.SetValue(Convert.ToInt64(txtValue.Text, 2), m_UIValue, Gurux.Device.PropertyStates.ValueChangedByUser);
                }
                else
                {
                    if (HexCB.Checked)
                    {
                        m_Property.SetValueAsHex(txtValue.Text, m_UIValue, Gurux.Device.PropertyStates.ValueChangedByUser);
                    }
                    else
                    {
                        m_Property.SetValue(txtValue.Text, m_UIValue, Gurux.Device.PropertyStates.ValueChangedByUser);
                    }

                }
                if (m_DisActions != null)
                {
                    m_Property.DisabledActions = m_DisActions.DisabledActions;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
                this.DialogResult = DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ModifyValueDialog_Load(object sender, System.EventArgs e)
        {

        }

        private void HexCB_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (m_TmpProperty.ValueType == typeof(string))
                {
                    if (HexCB.Checked)
                    {
                        m_TmpProperty.SetValueAsHex(txtValue.Text, true, 0);
                    }
                    else
                    {
                        m_TmpProperty.SetValue(txtValue.Text, true, 0);
                    }
                    txtValue.Text = HexCB.Checked ? m_TmpProperty.GetValueAsHex(true) : Convert.ToString(m_TmpProperty.GetValue(true));
                }
                else
                {
                    if (HexCB.Checked)
                    {
                        m_TmpProperty.SetValueAsHex(txtValue.Text, true, 0);
                    }
                    else
                    {
                        m_TmpProperty.SetValue(txtValue.Text, true, 0);
                    }

                    txtValue.Text = HexCB.Checked ? m_TmpProperty.GetValueAsHex(true) : Convert.ToString(m_TmpProperty.GetValue(true));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Shows 'help not available' message.
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

        private void ValueLB_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                object tmp = m_TmpProperty.GetValue(true);
                if (tmp is Array)
                {

                }
                else
                {
                    ulong val = Convert.ToUInt64(tmp);
                    ulong mask = Convert.ToUInt64(ValueLB.Items[e.Index].Tag);
                    //Toggle bits...
                    val ^= mask;
                    m_TmpProperty.SetValue(val, true, Gurux.Device.PropertyStates.ValueChangedByUser);
                    txtValue.Text = Convert.ToString(m_TmpProperty.GetValue(true));
                }
            }
            catch (Exception Ex)
            {
                GXCommon.ShowError(Ex);
            }
        }
    }
}
