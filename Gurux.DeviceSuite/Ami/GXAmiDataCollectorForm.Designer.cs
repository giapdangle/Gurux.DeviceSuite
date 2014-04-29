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

namespace Gurux.DeviceSuite.Ami
{
    partial class GXAmiDataCollectorForm
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
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.NameLbl = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.IPAddressLbl = new System.Windows.Forms.Label();
            this.IPAddressTB = new System.Windows.Forms.TextBox();
            this.DescriptionLbl = new System.Windows.Forms.Label();
            this.DescriptionTB = new System.Windows.Forms.TextBox();
            this.GuidLbl = new System.Windows.Forms.Label();
            this.GuidTB = new System.Windows.Forms.TextBox();
            this.LastConnectedLbl = new System.Windows.Forms.Label();
            this.LastConnectedTB = new System.Windows.Forms.TextBox();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.InternalCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.Location = new System.Drawing.Point(156, 185);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 7;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(237, 185);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(9, 15);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(38, 13);
            this.NameLbl.TabIndex = 131;
            this.NameLbl.Text = "Name:";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(90, 12);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(222, 20);
            this.NameTB.TabIndex = 0;
            // 
            // IPAddressLbl
            // 
            this.IPAddressLbl.AutoSize = true;
            this.IPAddressLbl.Location = new System.Drawing.Point(9, 41);
            this.IPAddressLbl.Name = "IPAddressLbl";
            this.IPAddressLbl.Size = new System.Drawing.Size(61, 13);
            this.IPAddressLbl.TabIndex = 133;
            this.IPAddressLbl.Text = "IP Address:";
            // 
            // IPAddressTB
            // 
            this.IPAddressTB.Location = new System.Drawing.Point(90, 38);
            this.IPAddressTB.Name = "IPAddressTB";
            this.IPAddressTB.ReadOnly = true;
            this.IPAddressTB.Size = new System.Drawing.Size(222, 20);
            this.IPAddressTB.TabIndex = 1;
            // 
            // DescriptionLbl
            // 
            this.DescriptionLbl.AutoSize = true;
            this.DescriptionLbl.Location = new System.Drawing.Point(9, 67);
            this.DescriptionLbl.Name = "DescriptionLbl";
            this.DescriptionLbl.Size = new System.Drawing.Size(63, 13);
            this.DescriptionLbl.TabIndex = 137;
            this.DescriptionLbl.Text = "Description:";
            // 
            // DescriptionTB
            // 
            this.DescriptionTB.Location = new System.Drawing.Point(90, 64);
            this.DescriptionTB.Name = "DescriptionTB";
            this.DescriptionTB.Size = new System.Drawing.Size(222, 20);
            this.DescriptionTB.TabIndex = 2;
            // 
            // GuidLbl
            // 
            this.GuidLbl.AutoSize = true;
            this.GuidLbl.Location = new System.Drawing.Point(9, 93);
            this.GuidLbl.Name = "GuidLbl";
            this.GuidLbl.Size = new System.Drawing.Size(32, 13);
            this.GuidLbl.TabIndex = 139;
            this.GuidLbl.Text = "Guid:";
            // 
            // GuidTB
            // 
            this.GuidTB.Location = new System.Drawing.Point(90, 90);
            this.GuidTB.Name = "GuidTB";
            this.GuidTB.ReadOnly = true;
            this.GuidTB.Size = new System.Drawing.Size(222, 20);
            this.GuidTB.TabIndex = 3;
            // 
            // LastConnectedLbl
            // 
            this.LastConnectedLbl.AutoSize = true;
            this.LastConnectedLbl.Location = new System.Drawing.Point(9, 143);
            this.LastConnectedLbl.Name = "LastConnectedLbl";
            this.LastConnectedLbl.Size = new System.Drawing.Size(82, 13);
            this.LastConnectedLbl.TabIndex = 141;
            this.LastConnectedLbl.Text = "Last Connected";
            // 
            // LastConnectedTB
            // 
            this.LastConnectedTB.Location = new System.Drawing.Point(90, 140);
            this.LastConnectedTB.Name = "LastConnectedTB";
            this.LastConnectedTB.ReadOnly = true;
            this.LastConnectedTB.Size = new System.Drawing.Size(131, 20);
            this.LastConnectedTB.TabIndex = 5;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshBtn.Location = new System.Drawing.Point(237, 137);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(75, 23);
            this.RefreshBtn.TabIndex = 6;
            this.RefreshBtn.Text = "Refresh...";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // InternalCB
            // 
            this.InternalCB.AutoSize = true;
            this.InternalCB.Location = new System.Drawing.Point(11, 119);
            this.InternalCB.Name = "InternalCB";
            this.InternalCB.Size = new System.Drawing.Size(61, 17);
            this.InternalCB.TabIndex = 4;
            this.InternalCB.Text = "Internal";
            this.InternalCB.UseVisualStyleBackColor = true;
            // 
            // GXAmiDataCollectorForm
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(324, 220);
            this.Controls.Add(this.InternalCB);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.LastConnectedLbl);
            this.Controls.Add(this.LastConnectedTB);
            this.Controls.Add(this.GuidLbl);
            this.Controls.Add(this.GuidTB);
            this.Controls.Add(this.DescriptionLbl);
            this.Controls.Add(this.DescriptionTB);
            this.Controls.Add(this.IPAddressLbl);
            this.Controls.Add(this.IPAddressTB);
            this.Controls.Add(this.NameLbl);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GXAmiDataCollectorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Collector Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Label IPAddressLbl;
        private System.Windows.Forms.TextBox IPAddressTB;
        private System.Windows.Forms.Label DescriptionLbl;
        private System.Windows.Forms.TextBox DescriptionTB;
        private System.Windows.Forms.Label GuidLbl;
        private System.Windows.Forms.TextBox GuidTB;
        private System.Windows.Forms.Label LastConnectedLbl;
        private System.Windows.Forms.TextBox LastConnectedTB;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.CheckBox InternalCB;
    }
}