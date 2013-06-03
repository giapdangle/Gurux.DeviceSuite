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
    partial class ModifyValueDialog
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
            this.ValueLB = new System.Windows.Forms.ListView();
            this.HexCB = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbValue = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueLB
            // 
            this.ValueLB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueLB.AutoArrange = false;
            this.ValueLB.CheckBoxes = true;
            this.ValueLB.FullRowSelect = true;
            this.ValueLB.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ValueLB.Location = new System.Drawing.Point(95, 30);
            this.ValueLB.MultiSelect = false;
            this.ValueLB.Name = "ValueLB";
            this.ValueLB.Size = new System.Drawing.Size(410, 123);
            this.ValueLB.TabIndex = 21;
            this.ValueLB.UseCompatibleStateImageBehavior = false;
            this.ValueLB.View = System.Windows.Forms.View.List;
            // 
            // HexCB
            // 
            this.HexCB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HexCB.Location = new System.Drawing.Point(7, 38);
            this.HexCB.Name = "HexCB";
            this.HexCB.Size = new System.Drawing.Size(48, 24);
            this.HexCB.TabIndex = 20;
            this.HexCB.Text = "&Hex";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(214, 171);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblValue
            // 
            this.lblValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblValue.Location = new System.Drawing.Point(7, 8);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(48, 16);
            this.lblValue.TabIndex = 17;
            this.lblValue.Text = "Value:";
            // 
            // cbValue
            // 
            this.cbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValue.ItemHeight = 13;
            this.cbValue.Location = new System.Drawing.Point(95, 6);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(410, 21);
            this.cbValue.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.GeneralTab);
            this.tabControl1.Location = new System.Drawing.Point(9, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(362, 154);
            this.tabControl1.TabIndex = 20;
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.ValueLB);
            this.GeneralTab.Controls.Add(this.HexCB);
            this.GeneralTab.Controls.Add(this.lblValue);
            this.GeneralTab.Controls.Add(this.cbValue);
            this.GeneralTab.Controls.Add(this.txtValue);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Size = new System.Drawing.Size(354, 128);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "GeneralTab";
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(95, 6);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(184, 20);
            this.txtValue.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(295, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "&Cancel";
            // 
            // ModifyValueDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(381, 204);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Name = "ModifyValueDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModifyValueDialog";
            this.tabControl1.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.GeneralTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ValueLB;
        private System.Windows.Forms.CheckBox HexCB;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage GeneralTab;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnCancel;
    }
}