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

namespace Gurux.DeviceSuite.Publisher
{
    partial class GXTemplatePage
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
            this.DeviceProfilesLbl = new System.Windows.Forms.Label();
            this.DeviceProfilesCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DeviceProfilesLbl
            // 
            this.DeviceProfilesLbl.AutoSize = true;
            this.DeviceProfilesLbl.Location = new System.Drawing.Point(10, 30);
            this.DeviceProfilesLbl.Name = "DeviceProfilesLbl";
            this.DeviceProfilesLbl.Size = new System.Drawing.Size(87, 13);
            this.DeviceProfilesLbl.TabIndex = 17;
            this.DeviceProfilesLbl.Text = "Device template:";
            // 
            // DeviceProfilesCB
            // 
            this.DeviceProfilesCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceProfilesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceProfilesCB.FormattingEnabled = true;
            this.DeviceProfilesCB.Location = new System.Drawing.Point(130, 30);
            this.DeviceProfilesCB.Name = "DeviceProfilesCB";
            this.DeviceProfilesCB.Size = new System.Drawing.Size(157, 21);
            this.DeviceProfilesCB.Sorted = true;
            this.DeviceProfilesCB.TabIndex = 16;
            // 
            // GXTemplatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.DeviceProfilesCB);
            this.Controls.Add(this.DeviceProfilesLbl);
            this.Name = "GXTemplatePage";
            this.Text = "GXTemplatePage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DeviceProfilesLbl;
        private System.Windows.Forms.ComboBox DeviceProfilesCB;

    }
}