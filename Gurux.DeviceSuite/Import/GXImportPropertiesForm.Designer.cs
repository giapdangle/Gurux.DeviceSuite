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
    partial class GXImportPropertiesForm
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
            this.DeselectAllBtn = new System.Windows.Forms.Button();
            this.NameClm = new System.Windows.Forms.ColumnHeader();
            this.SelectAllBtn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // DeselectAllBtn
            // 
            this.DeselectAllBtn.Location = new System.Drawing.Point(349, 35);
            this.DeselectAllBtn.Name = "DeselectAllBtn";
            this.DeselectAllBtn.Size = new System.Drawing.Size(96, 23);
            this.DeselectAllBtn.TabIndex = 20;
            this.DeselectAllBtn.Text = "Deselect All";
            this.DeselectAllBtn.Click += new System.EventHandler(this.DeselectAllBtn_Click);
            // 
            // NameClm
            // 
            this.NameClm.Text = "Name";
            this.NameClm.Width = 372;
            // 
            // SelectAllBtn
            // 
            this.SelectAllBtn.Location = new System.Drawing.Point(347, 6);
            this.SelectAllBtn.Name = "SelectAllBtn";
            this.SelectAllBtn.Size = new System.Drawing.Size(96, 23);
            this.SelectAllBtn.TabIndex = 19;
            this.SelectAllBtn.Text = "Select All";
            this.SelectAllBtn.Click += new System.EventHandler(this.SelectAllBtn_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameClm});
            this.listView1.Location = new System.Drawing.Point(4, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(339, 296);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // GXImportPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 307);
            this.Controls.Add(this.DeselectAllBtn);
            this.Controls.Add(this.SelectAllBtn);
            this.Controls.Add(this.listView1);
            this.Name = "GXImportPropertiesForm";
            this.Text = "ImportPropertiesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DeselectAllBtn;
        private System.Windows.Forms.ColumnHeader NameClm;
        private System.Windows.Forms.Button SelectAllBtn;
        internal System.Windows.Forms.ListView listView1;
    }
}