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
    partial class GXWizardProtocolSettings
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
            this.WaitTimeLbl = new System.Windows.Forms.Label();
            this.ResendCountTb = new System.Windows.Forms.TextBox();
            this.ResendCountLbl = new System.Windows.Forms.Label();
            this.WaitTimeTb = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // WaitTimeLbl
            // 
            this.WaitTimeLbl.Location = new System.Drawing.Point(5, 44);
            this.WaitTimeLbl.Name = "WaitTimeLbl";
            this.WaitTimeLbl.Size = new System.Drawing.Size(120, 16);
            this.WaitTimeLbl.TabIndex = 10;
            this.WaitTimeLbl.Text = "Wait Time";
            // 
            // ResendCountTb
            // 
            this.ResendCountTb.Location = new System.Drawing.Point(125, 12);
            this.ResendCountTb.Name = "ResendCountTb";
            this.ResendCountTb.Size = new System.Drawing.Size(160, 20);
            this.ResendCountTb.TabIndex = 0;
            this.ResendCountTb.Text = "0";
            // 
            // ResendCountLbl
            // 
            this.ResendCountLbl.Location = new System.Drawing.Point(5, 12);
            this.ResendCountLbl.Name = "ResendCountLbl";
            this.ResendCountLbl.Size = new System.Drawing.Size(120, 16);
            this.ResendCountLbl.TabIndex = 8;
            this.ResendCountLbl.Text = "Resend Count";
            // 
            // WaitTimeTb
            // 
            this.WaitTimeTb.CustomFormat = "HH:mm:ss";
            this.WaitTimeTb.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.WaitTimeTb.Location = new System.Drawing.Point(125, 40);
            this.WaitTimeTb.Name = "WaitTimeTb";
            this.WaitTimeTb.ShowUpDown = true;
            this.WaitTimeTb.Size = new System.Drawing.Size(160, 20);
            this.WaitTimeTb.TabIndex = 1;
            // 
            // GXWizardProtocolSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 131);
            this.Controls.Add(this.WaitTimeTb);
            this.Controls.Add(this.WaitTimeLbl);
            this.Controls.Add(this.ResendCountTb);
            this.Controls.Add(this.ResendCountLbl);
            this.Name = "GXWizardProtocolSettings";
            this.Text = "GXWizardProtocolSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WaitTimeLbl;
        private System.Windows.Forms.TextBox ResendCountTb;
        private System.Windows.Forms.Label ResendCountLbl;
        private System.Windows.Forms.DateTimePicker WaitTimeTb;
    }
}