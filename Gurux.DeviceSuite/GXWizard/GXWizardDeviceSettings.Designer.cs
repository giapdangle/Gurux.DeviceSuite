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

namespace Gurux.DeviceSuite.GXWizard
{
    partial class GXWizardDeviceSettings
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
            this.NameLbl = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.ProtocolCB = new System.Windows.Forms.ComboBox();
            this.ProtocolLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(11, 18);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(38, 13);
            this.NameLbl.TabIndex = 45;
            this.NameLbl.Text = "Name:";
            // 
            // NameTB
            // 
            this.NameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTB.Location = new System.Drawing.Point(98, 15);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(247, 20);
            this.NameTB.TabIndex = 0;
            // 
            // ProtocolCB
            // 
            this.ProtocolCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProtocolCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProtocolCB.FormattingEnabled = true;
            this.ProtocolCB.Location = new System.Drawing.Point(98, 41);
            this.ProtocolCB.Name = "ProtocolCB";
            this.ProtocolCB.Size = new System.Drawing.Size(247, 21);
            this.ProtocolCB.Sorted = true;
            this.ProtocolCB.TabIndex = 1;
            // 
            // ProtocolLbl
            // 
            this.ProtocolLbl.AutoSize = true;
            this.ProtocolLbl.Location = new System.Drawing.Point(11, 44);
            this.ProtocolLbl.Name = "ProtocolLbl";
            this.ProtocolLbl.Size = new System.Drawing.Size(49, 13);
            this.ProtocolLbl.TabIndex = 43;
            this.ProtocolLbl.Text = "Protocol:";
            // 
            // GXWizardDeviceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 205);
            this.Controls.Add(this.NameLbl);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.ProtocolCB);
            this.Controls.Add(this.ProtocolLbl);
            this.Name = "GXWizardDeviceSettings";
            this.Text = "GXWizardDeviceSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.ComboBox ProtocolCB;
        private System.Windows.Forms.Label ProtocolLbl;

    }
}