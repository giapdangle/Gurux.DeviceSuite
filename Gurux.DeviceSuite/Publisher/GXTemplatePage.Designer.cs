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
            this.DeviceTemplateLbl = new System.Windows.Forms.Label();
            this.DeviceTemplateCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DeviceTemplateLbl
            // 
            this.DeviceTemplateLbl.AutoSize = true;
            this.DeviceTemplateLbl.Location = new System.Drawing.Point(10, 30);
            this.DeviceTemplateLbl.Name = "DeviceTemplateLbl";
            this.DeviceTemplateLbl.Size = new System.Drawing.Size(87, 13);
            this.DeviceTemplateLbl.TabIndex = 17;
            this.DeviceTemplateLbl.Text = "Device template:";
            // 
            // DeviceTemplateCB
            // 
            this.DeviceTemplateCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceTemplateCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceTemplateCB.FormattingEnabled = true;
            this.DeviceTemplateCB.Location = new System.Drawing.Point(130, 30);
            this.DeviceTemplateCB.Name = "DeviceTemplateCB";
            this.DeviceTemplateCB.Size = new System.Drawing.Size(157, 21);
            this.DeviceTemplateCB.Sorted = true;
            this.DeviceTemplateCB.TabIndex = 16;
            // 
            // GXTemplatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.DeviceTemplateCB);
            this.Controls.Add(this.DeviceTemplateLbl);
            this.Name = "GXTemplatePage";
            this.Text = "GXTemplatePage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DeviceTemplateLbl;
        private System.Windows.Forms.ComboBox DeviceTemplateCB;

    }
}