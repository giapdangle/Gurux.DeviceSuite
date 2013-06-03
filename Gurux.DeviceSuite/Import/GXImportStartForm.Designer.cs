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

namespace Gurux.DeviceSuite.Import
{
    partial class GXImportStartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeviceRB = new System.Windows.Forms.RadioButton();
            this.CustomProtocolRB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // DeviceRB
            // 
            this.DeviceRB.Checked = true;
            this.DeviceRB.Location = new System.Drawing.Point(109, 155);
            this.DeviceRB.Name = "DeviceRB";
            this.DeviceRB.Size = new System.Drawing.Size(168, 24);
            this.DeviceRB.TabIndex = 3;
            this.DeviceRB.TabStop = true;
            this.DeviceRB.Text = "From Device";
            // 
            // CustomProtocolRB
            // 
            this.CustomProtocolRB.Location = new System.Drawing.Point(109, 179);
            this.CustomProtocolRB.Name = "CustomProtocolRB";
            this.CustomProtocolRB.Size = new System.Drawing.Size(168, 24);
            this.CustomProtocolRB.TabIndex = 4;
            this.CustomProtocolRB.Text = "Custom File Type";
            // 
            // ImportStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 358);
            this.Controls.Add(this.DeviceRB);
            this.Controls.Add(this.CustomProtocolRB);
            this.Name = "ImportStartForm";
            this.Text = "ImportStartForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RadioButton DeviceRB;
        private System.Windows.Forms.RadioButton CustomProtocolRB;
    }
}