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
    partial class GXPublisherDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GXPublisherDlg));
            this.panel3 = new System.Windows.Forms.Panel();
            this.Frame = new System.Windows.Forms.Panel();
            this.PublisherImage = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CaptionLbl = new System.Windows.Forms.Label();
            this.DescriptionLbl = new System.Windows.Forms.Label();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.BackBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Frame.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Gurux.DeviceSuite.Properties.Resources.factory;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(395, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(90, 65);
            this.panel3.TabIndex = 13;
            // 
            // Frame
            // 
            this.Frame.Controls.Add(this.PublisherImage);
            this.Frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Frame.Location = new System.Drawing.Point(0, 66);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(485, 323);
            this.Frame.TabIndex = 12;
            // 
            // PublisherImage
            // 
            this.PublisherImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PublisherImage.BackColor = System.Drawing.Color.Transparent;
            this.PublisherImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PublisherImage.BackgroundImage")));
            this.PublisherImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PublisherImage.Location = new System.Drawing.Point(13, 207);
            this.PublisherImage.Name = "PublisherImage";
            this.PublisherImage.Size = new System.Drawing.Size(122, 116);
            this.PublisherImage.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Gurux.DeviceSuite.Properties.Resources.leaf;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(47, 66);
            this.panel2.TabIndex = 12;
            // 
            // CaptionLbl
            // 
            this.CaptionLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CaptionLbl.BackColor = System.Drawing.Color.Transparent;
            this.CaptionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionLbl.Location = new System.Drawing.Point(46, 4);
            this.CaptionLbl.Name = "CaptionLbl";
            this.CaptionLbl.Size = new System.Drawing.Size(343, 24);
            this.CaptionLbl.TabIndex = 11;
            this.CaptionLbl.Text = "Caption";
            // 
            // DescriptionLbl
            // 
            this.DescriptionLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionLbl.BackColor = System.Drawing.Color.Transparent;
            this.DescriptionLbl.Location = new System.Drawing.Point(47, 29);
            this.DescriptionLbl.Name = "DescriptionLbl";
            this.DescriptionLbl.Size = new System.Drawing.Size(342, 32);
            this.DescriptionLbl.TabIndex = 10;
            this.DescriptionLbl.Text = "Description";
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.CancelBtn);
            this.BottomPanel.Controls.Add(this.NextBtn);
            this.BottomPanel.Controls.Add(this.BackBtn);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 389);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(485, 50);
            this.BottomPanel.TabIndex = 11;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(395, 11);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 24);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextBtn.Location = new System.Drawing.Point(291, 11);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 24);
            this.NextBtn.TabIndex = 0;
            this.NextBtn.Text = "Next";
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BackBtn.Location = new System.Drawing.Point(211, 11);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(75, 24);
            this.BackBtn.TabIndex = 1;
            this.BackBtn.Text = "Back";
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::Gurux.DeviceSuite.Properties.Resources.middle;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.CaptionLbl);
            this.panel1.Controls.Add(this.DescriptionLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 66);
            this.panel1.TabIndex = 10;
            // 
            // GXPublisherDlg
            // 
            this.AcceptButton = this.NextBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(485, 439);
            this.Controls.Add(this.Frame);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GXPublisherDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gurux Device Publisiher Wizard";
            this.Frame.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel Frame;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label CaptionLbl;
        private System.Windows.Forms.Label DescriptionLbl;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Panel PublisherImage;
    }
}