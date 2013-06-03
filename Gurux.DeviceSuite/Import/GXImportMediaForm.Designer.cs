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
    partial class GXImportMediaForm
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
            this.MediasCB = new System.Windows.Forms.ComboBox();
            this.MediaPanel = new System.Windows.Forms.Panel();
            this.MediaLbl = new System.Windows.Forms.Label();
            this.MediaFrame = new System.Windows.Forms.Panel();
            this.MediaPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MediasCB
            // 
            this.MediasCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MediasCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediasCB.Location = new System.Drawing.Point(80, 9);
            this.MediasCB.Name = "MediasCB";
            this.MediasCB.Size = new System.Drawing.Size(200, 21);
            this.MediasCB.Sorted = true;
            this.MediasCB.TabIndex = 4;
            this.MediasCB.SelectedIndexChanged += new System.EventHandler(this.MediasCB_SelectedIndexChanged);
            // 
            // MediaPanel
            // 
            this.MediaPanel.Controls.Add(this.MediaLbl);
            this.MediaPanel.Controls.Add(this.MediasCB);
            this.MediaPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MediaPanel.Location = new System.Drawing.Point(0, 0);
            this.MediaPanel.Name = "MediaPanel";
            this.MediaPanel.Size = new System.Drawing.Size(419, 39);
            this.MediaPanel.TabIndex = 6;
            // 
            // MediaLbl
            // 
            this.MediaLbl.AutoSize = true;
            this.MediaLbl.Location = new System.Drawing.Point(12, 12);
            this.MediaLbl.Name = "MediaLbl";
            this.MediaLbl.Size = new System.Drawing.Size(39, 13);
            this.MediaLbl.TabIndex = 5;
            this.MediaLbl.Text = "Media:";
            // 
            // MediaFrame
            // 
            this.MediaFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaFrame.Location = new System.Drawing.Point(0, 39);
            this.MediaFrame.Name = "MediaFrame";
            this.MediaFrame.Size = new System.Drawing.Size(419, 240);
            this.MediaFrame.TabIndex = 7;
            // 
            // GXImportMediaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 279);
            this.Controls.Add(this.MediaFrame);
            this.Controls.Add(this.MediaPanel);
            this.Name = "GXImportMediaForm";
            this.Text = "ImportMediaForm";
            this.MediaPanel.ResumeLayout(false);
            this.MediaPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox MediasCB;
        private System.Windows.Forms.Panel MediaPanel;
        private System.Windows.Forms.Label MediaLbl;
        private System.Windows.Forms.Panel MediaFrame;
    }
}