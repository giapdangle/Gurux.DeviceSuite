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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Gurux.Device;
using Gurux.Device.PresetDevices;

namespace Gurux.DeviceSuite.Editor
{
    public class RadioListBox : System.Windows.Forms.ListBox
    {
        /// <summary>
        /// Selected index.
        /// </summary>
        int m_CheckedIndex = -1;

        /// <summary>
        /// Initializes a new instance of the RadioListBox class.
        /// </summary>
        public RadioListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        
        public int CheckedIndex
        {
            get
            {
                return m_CheckedIndex;
            }
            set
            {
                if (m_CheckedIndex > 0 && m_CheckedIndex < this.Items.Count - 1)
                {
                    this.Invalidate(this.GetItemRectangle(m_CheckedIndex));
                }
                m_CheckedIndex = value;
                SelectedIndex = value;
            }
        }

        /// <summary>
        /// Hide focus from selected item.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void OnLeave(object sender, System.EventArgs e)
        {
            if (this.SelectedIndex != -1)
            {
                this.Invalidate(this.GetItemRectangle(this.SelectedIndex));
            }
        }

        void SelectNewItem(int index)
        {
            if (index == -1 || index == m_CheckedIndex)
            {
                return;
            }
            if (m_CheckedIndex != -1 && m_CheckedIndex < this.Items.Count)
            {
                this.Invalidate(this.GetItemRectangle(m_CheckedIndex));
            }
            m_CheckedIndex = index;
            this.Invalidate(this.GetItemRectangle(m_CheckedIndex));
        }

        /// <summary>
        /// Draw selected item.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectNewItem(this.IndexFromPoint(e.X, e.Y));
            }
        }

        /// <summary>
        /// Draw selected item.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            SelectNewItem(this.IndexFromPoint(e.X, e.Y));
        }

        /// <summary>
        /// Select new item if user press Space.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.SelectedIndex != -1 &&
                (e.KeyValue == (int)Keys.Up ||
                e.KeyValue == (int)Keys.Down ||
                e.KeyValue == (int)Keys.Left ||
                e.KeyValue == (int)Keys.Right ||
                e.KeyValue == (int)Keys.PageDown ||
                e.KeyValue == (int)Keys.PageUp))
            {
                this.Invalidate(this.GetItemRectangle(this.SelectedIndex));
            }

            if (this.SelectedIndex != -1 && e.KeyValue == (int)Keys.Space)
            {
                SelectNewItem(this.SelectedIndex);
            }
        }

        /// <summary>
        /// Draw item.
        /// </summary>
        /// <param name="e">A DrawItemEventArgs that contains the event data.</param>
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (this.Items.Count <= e.Index || e.Index == -1)
            {
                return;
            }
            ButtonState bs = ButtonState.Normal;
            //Should it be grayed out?
            if (!this.Enabled)
            {
                bs |= ButtonState.Inactive;
            }

            //Should it be selected?
            if (m_CheckedIndex == e.Index)
            {
                bs |= ButtonState.Checked;
            }

            // Draw the current item text based on the current Font and the custom brush settings.
            Brush textBrush = new SolidBrush(this.ForeColor);
            int sz = e.Bounds.Height;
            Rectangle rc = e.Bounds;
            rc.Location = new Point(sz + 2, e.Bounds.Top);

            string str = string.Empty;
            GXPublishedDeviceProfile type = this.Items[e.Index] as GXPublishedDeviceProfile;
            if (type != null)
            {
                GXDeviceVersion ver = type.Parent.Parent as GXDeviceVersion;
                string manufacturer = ver.Parent.Parent.Parent.Parent.Name;
                string model = ver.Parent.Parent.Name;
                string v = ver.Name;
                str = type.PresetName;
                ControlPaint.DrawRadioButton(e.Graphics, 1, e.Bounds.Top, sz, sz, bs);
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
                str = "[" + manufacturer + " " + model + " " + v + "]";
                SizeF size = e.Graphics.MeasureString(str, e.Font);
                rc.X += (int)(rc.Width - (size.Width + 20));
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);

            }
            else
            {
                GXDeviceProfile tp = this.Items[e.Index] as GXDeviceProfile;
                if (tp != null)
                {
                    str = tp.Name;
                    ControlPaint.DrawRadioButton(e.Graphics, 1, e.Bounds.Top, sz, sz, bs);
                    e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);

                    str = "[" + tp.Protocol + "]";
                    SizeF size = e.Graphics.MeasureString(str, e.Font);
                    rc.X += (int)(rc.Width - (size.Width + 20));
                    e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
                }
                else
                {
                    str = this.Items[e.Index].ToString();
                    ControlPaint.DrawRadioButton(e.Graphics, 1, e.Bounds.Top, sz, sz, bs);
                    e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
                }
            }

            //Draw focus rectangle (dotted line)
            if (this.SelectedIndex == e.Index && this.Focused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
            }

        }
    }
}
