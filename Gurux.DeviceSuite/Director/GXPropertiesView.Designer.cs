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

namespace Gurux.DeviceSuite.Director
{
    partial class GXPropertiesView
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
            this.PropertyList = new System.Windows.Forms.ListView();
            this.PropertyNameCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyValueCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyTypeCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyUnitCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyTimeCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyReadCountCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyWriteCountCH = new System.Windows.Forms.ColumnHeader();
            this.PropertyExecutionTimeCH = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // PropertyList
            // 
            this.PropertyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyNameCH,
            this.PropertyValueCH,
            this.PropertyTypeCH,
            this.PropertyUnitCH,
            this.PropertyTimeCH,
            this.PropertyReadCountCH,
            this.PropertyWriteCountCH,
            this.PropertyExecutionTimeCH});
            this.PropertyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyList.FullRowSelect = true;
            this.PropertyList.Location = new System.Drawing.Point(0, 0);
            this.PropertyList.Name = "PropertyList";
            this.PropertyList.Size = new System.Drawing.Size(284, 262);
            this.PropertyList.TabIndex = 10;
            this.PropertyList.UseCompatibleStateImageBehavior = false;
            this.PropertyList.View = System.Windows.Forms.View.Details;
            // 
            // PropertyNameCH
            // 
            this.PropertyNameCH.Text = "Name";
            // 
            // PropertyValueCH
            // 
            this.PropertyValueCH.Text = "Value";
            // 
            // PropertyTypeCH
            // 
            this.PropertyTypeCH.Text = "Type";
            // 
            // PropertyUnitCH
            // 
            this.PropertyUnitCH.Text = "Unit";
            // 
            // PropertyTimeCH
            // 
            this.PropertyTimeCH.Text = "Time";
            // 
            // PropertyReadCountCH
            // 
            this.PropertyReadCountCH.Text = "Read Count";
            // 
            // PropertyWriteCountCH
            // 
            this.PropertyWriteCountCH.Text = "Write Count";
            // 
            // PropertyExecutionTimeCH
            // 
            this.PropertyExecutionTimeCH.Text = "Execution Time";
            // 
            // GXPropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.PropertyList);
            this.Name = "GXPropertiesView";
            this.Text = "GXPropertiesView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PropertyList;
        private System.Windows.Forms.ColumnHeader PropertyNameCH;
        private System.Windows.Forms.ColumnHeader PropertyValueCH;
        private System.Windows.Forms.ColumnHeader PropertyTypeCH;
        private System.Windows.Forms.ColumnHeader PropertyUnitCH;
        private System.Windows.Forms.ColumnHeader PropertyTimeCH;
        private System.Windows.Forms.ColumnHeader PropertyReadCountCH;
        private System.Windows.Forms.ColumnHeader PropertyWriteCountCH;
        private System.Windows.Forms.ColumnHeader PropertyExecutionTimeCH;
    }
}