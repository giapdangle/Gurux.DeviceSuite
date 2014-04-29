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
using System.Drawing;
using System.Windows.Forms;
using Gurux.Device;
using GuruxAMI.Common;

namespace Gurux.DeviceSuite.Common
{
    public class CustomDeviceTypeListBox : System.Windows.Forms.ListBox
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomDeviceTypeListBox()
        {
            this.Sorted = true;
        }        
                
        /// <summary>
        /// Draw item type and protocol.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (this.Items.Count <= e.Index || e.Index == -1)
            {
                return;
            }
            // Draw the current item text based on the current Font and the custom brush settings.            
            Brush textBrush;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textBrush = SystemBrushes.HighlightText;
            }
            else
            {
                textBrush = SystemBrushes.WindowText;
            }
            Rectangle rc = e.Bounds;
            rc.Location = new Point(0, e.Bounds.Top);
            string str = string.Empty;
            GXDeviceProfile tp = this.Items[e.Index] as GXDeviceProfile;
            if (tp != null)
            {
                str = tp.Name;
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
                str = "[" + tp.Protocol + "]";
                SizeF size = e.Graphics.MeasureString(str, e.Font);
                rc.X += (int)(rc.Width - (size.Width));
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
            }
            else
            {
                GXAmiDeviceProfile dt = this.Items[e.Index] as GXAmiDeviceProfile;
                str = dt.Profile;
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
                str = "[" + dt.Protocol + "]";
                SizeF size = e.Graphics.MeasureString(str, e.Font);
                rc.X += (int)(rc.Width - (size.Width));
                e.Graphics.DrawString(str, e.Font, textBrush, rc, StringFormat.GenericDefault);
            }
        }
    }
}
