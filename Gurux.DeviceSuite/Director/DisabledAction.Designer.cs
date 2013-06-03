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
    partial class DisabledAction
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
            this.ReadCb = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DisabledActionsTB = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.WriteCb = new System.Windows.Forms.CheckBox();
            this.ScheduleCB = new System.Windows.Forms.CheckBox();
            this.MonitorCB = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tabControl1.SuspendLayout();
            this.DisabledActionsTB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadCb
            // 
            this.ReadCb.Location = new System.Drawing.Point(6, 19);
            this.ReadCb.Name = "ReadCb";
            this.ReadCb.Size = new System.Drawing.Size(104, 16);
            this.ReadCb.TabIndex = 17;
            this.ReadCb.Text = "ReadCb";
            this.ReadCb.CheckedChanged += new System.EventHandler(this.ReadCb_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.DisabledActionsTB);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(235, 173);
            this.tabControl1.TabIndex = 16;
            // 
            // DisabledActionsTB
            // 
            this.DisabledActionsTB.Controls.Add(this.groupBox2);
            this.DisabledActionsTB.Location = new System.Drawing.Point(4, 22);
            this.DisabledActionsTB.Name = "DisabledActionsTB";
            this.DisabledActionsTB.Size = new System.Drawing.Size(227, 147);
            this.DisabledActionsTB.TabIndex = 0;
            this.DisabledActionsTB.Text = "DisabledActionsTB";
            this.DisabledActionsTB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ReadCb);
            this.groupBox2.Controls.Add(this.WriteCb);
            this.groupBox2.Controls.Add(this.ScheduleCB);
            this.groupBox2.Controls.Add(this.MonitorCB);
            this.groupBox2.Location = new System.Drawing.Point(11, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 111);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Disabled Actions";
            // 
            // WriteCb
            // 
            this.WriteCb.Location = new System.Drawing.Point(6, 41);
            this.WriteCb.Name = "WriteCb";
            this.WriteCb.Size = new System.Drawing.Size(104, 16);
            this.WriteCb.TabIndex = 18;
            this.WriteCb.Text = "WriteCb";
            this.WriteCb.CheckedChanged += new System.EventHandler(this.WriteCb_CheckedChanged);
            // 
            // ScheduleCB
            // 
            this.ScheduleCB.Location = new System.Drawing.Point(6, 85);
            this.ScheduleCB.Name = "ScheduleCB";
            this.ScheduleCB.Size = new System.Drawing.Size(104, 16);
            this.ScheduleCB.TabIndex = 20;
            this.ScheduleCB.Text = "ScheduleCB";
            this.ScheduleCB.CheckedChanged += new System.EventHandler(this.ScheduleCB_CheckedChanged);
            // 
            // MonitorCB
            // 
            this.MonitorCB.Location = new System.Drawing.Point(6, 63);
            this.MonitorCB.Name = "MonitorCB";
            this.MonitorCB.Size = new System.Drawing.Size(104, 16);
            this.MonitorCB.TabIndex = 19;
            this.MonitorCB.Text = "MonitorCB";
            this.MonitorCB.CheckedChanged += new System.EventHandler(this.MonitorCB_CheckedChanged);
            // 
            // DisabledAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 408);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DisabledAction";
            this.ShowInTaskbar = false;
            this.Text = "DisabledAction";
            this.tabControl1.ResumeLayout(false);
            this.DisabledActionsTB.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ReadCb;
        private System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage DisabledActionsTB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox WriteCb;
        private System.Windows.Forms.CheckBox ScheduleCB;
        private System.Windows.Forms.CheckBox MonitorCB;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}