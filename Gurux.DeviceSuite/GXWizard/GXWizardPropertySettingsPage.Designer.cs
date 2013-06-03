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
    partial class GXWizardPropertySettingsPage
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
            this.TypeDdl = new System.Windows.Forms.ComboBox();
            this.TypeLbl = new System.Windows.Forms.Label();
            this.DescriptionTb = new System.Windows.Forms.TextBox();
            this.DescriptionLbl = new System.Windows.Forms.Label();
            this.NameTb = new System.Windows.Forms.TextBox();
            this.NameLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TypeDdl
            // 
            this.TypeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeDdl.FormattingEnabled = true;
            this.TypeDdl.Location = new System.Drawing.Point(128, 8);
            this.TypeDdl.Name = "TypeDdl";
            this.TypeDdl.Size = new System.Drawing.Size(284, 21);
            this.TypeDdl.TabIndex = 11;
            // 
            // TypeLbl
            // 
            this.TypeLbl.Location = new System.Drawing.Point(8, 8);
            this.TypeLbl.Name = "TypeLbl";
            this.TypeLbl.Size = new System.Drawing.Size(112, 16);
            this.TypeLbl.TabIndex = 10;
            this.TypeLbl.Text = "Type";
            // 
            // DescriptionTb
            // 
            this.DescriptionTb.Location = new System.Drawing.Point(128, 61);
            this.DescriptionTb.Multiline = true;
            this.DescriptionTb.Name = "DescriptionTb";
            this.DescriptionTb.Size = new System.Drawing.Size(284, 136);
            this.DescriptionTb.TabIndex = 9;
            // 
            // DescriptionLbl
            // 
            this.DescriptionLbl.Location = new System.Drawing.Point(8, 61);
            this.DescriptionLbl.Name = "DescriptionLbl";
            this.DescriptionLbl.Size = new System.Drawing.Size(112, 16);
            this.DescriptionLbl.TabIndex = 8;
            this.DescriptionLbl.Text = "Description";
            // 
            // NameTb
            // 
            this.NameTb.Location = new System.Drawing.Point(128, 35);
            this.NameTb.Name = "NameTb";
            this.NameTb.Size = new System.Drawing.Size(284, 20);
            this.NameTb.TabIndex = 7;
            // 
            // NameLbl
            // 
            this.NameLbl.Location = new System.Drawing.Point(8, 35);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(112, 16);
            this.NameLbl.TabIndex = 6;
            this.NameLbl.Text = "Name";
            // 
            // GXWizardPropertySettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 243);
            this.Controls.Add(this.TypeDdl);
            this.Controls.Add(this.TypeLbl);
            this.Controls.Add(this.DescriptionTb);
            this.Controls.Add(this.DescriptionLbl);
            this.Controls.Add(this.NameTb);
            this.Controls.Add(this.NameLbl);
            this.Name = "GXWizardPropertySettingsPage";
            this.Text = "GXWizardPropertySettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TypeDdl;
        private System.Windows.Forms.Label TypeLbl;
        private System.Windows.Forms.TextBox DescriptionTb;
        private System.Windows.Forms.Label DescriptionLbl;
        private System.Windows.Forms.TextBox NameTb;
        private System.Windows.Forms.Label NameLbl;
    }
}