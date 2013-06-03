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

namespace Gurux.DeviceSuite.Manufacturer
{
    partial class GXDeviceTemplateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXDeviceTemplateForm));
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.NameLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.AvailableTemplates = new Gurux.DeviceSuite.Common.CustomDeviceTypeListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.AddBtn = new System.Windows.Forms.Button();
            this.ProtocolTB = new System.Windows.Forms.TextBox();
            this.ProtocolLbl = new System.Windows.Forms.Label();
            this.SelectedTemplateTB = new System.Windows.Forms.TextBox();
            this.SelectedTemplateLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(278, 11);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 5;
            this.OKBtn.Text = "&OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(359, 11);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // NameTB
            // 
            this.NameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTB.Location = new System.Drawing.Point(9, 28);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(431, 20);
            this.NameTB.TabIndex = 0;
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(9, 12);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(71, 13);
            this.NameLbl.TabIndex = 30;
            this.NameLbl.Text = "Preset Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Controls.Add(this.OKBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 46);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NameTB);
            this.panel2.Controls.Add(this.NameLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(443, 56);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 56);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.AvailableTemplates);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(443, 166);
            this.splitContainer2.SplitterDistance = 128;
            this.splitContainer2.TabIndex = 39;
            // 
            // AvailableTemplates
            // 
            this.AvailableTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableTemplates.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.AvailableTemplates.FormattingEnabled = true;
            this.AvailableTemplates.Location = new System.Drawing.Point(0, 0);
            this.AvailableTemplates.Name = "AvailableTemplates";
            this.AvailableTemplates.Size = new System.Drawing.Size(128, 160);
            this.AvailableTemplates.Sorted = true;
            this.AvailableTemplates.TabIndex = 1;
            this.AvailableTemplates.DoubleClick += new System.EventHandler(this.AddBtn_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.AddBtn);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ProtocolTB);
            this.splitContainer3.Panel2.Controls.Add(this.ProtocolLbl);
            this.splitContainer3.Panel2.Controls.Add(this.SelectedTemplateTB);
            this.splitContainer3.Panel2.Controls.Add(this.SelectedTemplateLbl);
            this.splitContainer3.Size = new System.Drawing.Size(311, 166);
            this.splitContainer3.SplitterDistance = 89;
            this.splitContainer3.TabIndex = 0;
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(11, 44);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(65, 23);
            this.AddBtn.TabIndex = 2;
            this.AddBtn.Text = ">>";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // ProtocolTB
            // 
            this.ProtocolTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProtocolTB.Location = new System.Drawing.Point(3, 65);
            this.ProtocolTB.Name = "ProtocolTB";
            this.ProtocolTB.ReadOnly = true;
            this.ProtocolTB.Size = new System.Drawing.Size(206, 20);
            this.ProtocolTB.TabIndex = 33;
            // 
            // ProtocolLbl
            // 
            this.ProtocolLbl.AutoSize = true;
            this.ProtocolLbl.Location = new System.Drawing.Point(3, 49);
            this.ProtocolLbl.Name = "ProtocolLbl";
            this.ProtocolLbl.Size = new System.Drawing.Size(49, 13);
            this.ProtocolLbl.TabIndex = 34;
            this.ProtocolLbl.Text = "Protocol:";
            // 
            // SelectedTemplateTB
            // 
            this.SelectedTemplateTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedTemplateTB.Location = new System.Drawing.Point(3, 19);
            this.SelectedTemplateTB.Name = "SelectedTemplateTB";
            this.SelectedTemplateTB.ReadOnly = true;
            this.SelectedTemplateTB.Size = new System.Drawing.Size(206, 20);
            this.SelectedTemplateTB.TabIndex = 31;
            // 
            // SelectedTemplateLbl
            // 
            this.SelectedTemplateLbl.AutoSize = true;
            this.SelectedTemplateLbl.Location = new System.Drawing.Point(3, 3);
            this.SelectedTemplateLbl.Name = "SelectedTemplateLbl";
            this.SelectedTemplateLbl.Size = new System.Drawing.Size(121, 13);
            this.SelectedTemplateLbl.TabIndex = 32;
            this.SelectedTemplateLbl.Text = "Selected Device Profile:";
            // 
            // GXDeviceTemplateForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(443, 268);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GXDeviceTemplateForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preset Device Profile";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox ProtocolTB;
        private System.Windows.Forms.Label ProtocolLbl;
        private System.Windows.Forms.TextBox SelectedTemplateTB;
        private System.Windows.Forms.Label SelectedTemplateLbl;
        private Gurux.DeviceSuite.Common.CustomDeviceTypeListBox AvailableTemplates;
    }
}