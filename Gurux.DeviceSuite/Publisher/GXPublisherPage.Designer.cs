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
    partial class GXPublisherPage
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
            this.AnonymousCB = new System.Windows.Forms.CheckBox();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTb = new System.Windows.Forms.TextBox();
            this.NameLbl = new System.Windows.Forms.Label();
            this.RememberMeCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // AnonymousCB
            // 
            this.AnonymousCB.AutoSize = true;
            this.AnonymousCB.Location = new System.Drawing.Point(130, 82);
            this.AnonymousCB.Name = "AnonymousCB";
            this.AnonymousCB.Size = new System.Drawing.Size(125, 17);
            this.AnonymousCB.TabIndex = 27;
            this.AnonymousCB.Text = "Publish Anonymously";
            this.AnonymousCB.UseVisualStyleBackColor = true;
            // 
            // PasswordTB
            // 
            this.PasswordTB.Location = new System.Drawing.Point(130, 56);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = '*';
            this.PasswordTB.Size = new System.Drawing.Size(209, 20);
            this.PasswordTB.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Password:";
            // 
            // NameTb
            // 
            this.NameTb.Location = new System.Drawing.Point(130, 30);
            this.NameTb.Name = "NameTb";
            this.NameTb.Size = new System.Drawing.Size(209, 20);
            this.NameTb.TabIndex = 24;
            // 
            // NameLbl
            // 
            this.NameLbl.Location = new System.Drawing.Point(10, 30);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(90, 16);
            this.NameLbl.TabIndex = 23;
            this.NameLbl.Text = "Name:";
            // 
            // RememberMeCB
            // 
            this.RememberMeCB.AutoSize = true;
            this.RememberMeCB.Location = new System.Drawing.Point(130, 105);
            this.RememberMeCB.Name = "RememberMeCB";
            this.RememberMeCB.Size = new System.Drawing.Size(95, 17);
            this.RememberMeCB.TabIndex = 28;
            this.RememberMeCB.Text = "Remember Me";
            this.RememberMeCB.UseVisualStyleBackColor = true;
            // 
            // GXPublisherPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 216);
            this.Controls.Add(this.RememberMeCB);
            this.Controls.Add(this.AnonymousCB);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTb);
            this.Controls.Add(this.NameLbl);
            this.Name = "GXPublisherPage";
            this.Text = "GXPublisherPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AnonymousCB;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTb;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.CheckBox RememberMeCB;


    }
}