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

namespace Gurux.DeviceSuite
{
    partial class GXAmiDBSettingsForm
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
            this.DatabaseSettingsGB = new System.Windows.Forms.GroupBox();
            this.DBPasswordTB = new System.Windows.Forms.TextBox();
            this.DBPasswordLbl = new System.Windows.Forms.Label();
            this.DBUserNameTB = new System.Windows.Forms.TextBox();
            this.DBBUserNameLbl = new System.Windows.Forms.Label();
            this.TablePrefixTB = new System.Windows.Forms.TextBox();
            this.TablePrefixLbl = new System.Windows.Forms.Label();
            this.DBNameTb = new System.Windows.Forms.TextBox();
            this.DBNameLbl = new System.Windows.Forms.Label();
            this.DBHostTB = new System.Windows.Forms.TextBox();
            this.DBHostLbl = new System.Windows.Forms.Label();
            this.DatabaseSettingsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.Location = new System.Drawing.Point(223, 182);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 9;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(304, 182);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 10;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // DatabaseSettingsGB
            // 
            this.DatabaseSettingsGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DatabaseSettingsGB.Controls.Add(this.DBPasswordTB);
            this.DatabaseSettingsGB.Controls.Add(this.DBPasswordLbl);
            this.DatabaseSettingsGB.Controls.Add(this.DBUserNameTB);
            this.DatabaseSettingsGB.Controls.Add(this.DBBUserNameLbl);
            this.DatabaseSettingsGB.Controls.Add(this.TablePrefixTB);
            this.DatabaseSettingsGB.Controls.Add(this.TablePrefixLbl);
            this.DatabaseSettingsGB.Controls.Add(this.DBNameTb);
            this.DatabaseSettingsGB.Controls.Add(this.DBNameLbl);
            this.DatabaseSettingsGB.Controls.Add(this.DBHostTB);
            this.DatabaseSettingsGB.Controls.Add(this.DBHostLbl);
            this.DatabaseSettingsGB.Location = new System.Drawing.Point(12, 12);
            this.DatabaseSettingsGB.Name = "DatabaseSettingsGB";
            this.DatabaseSettingsGB.Size = new System.Drawing.Size(367, 161);
            this.DatabaseSettingsGB.TabIndex = 28;
            this.DatabaseSettingsGB.TabStop = false;
            this.DatabaseSettingsGB.Text = "Database Settings";
            // 
            // DBPasswordTB
            // 
            this.DBPasswordTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBPasswordTB.Location = new System.Drawing.Point(103, 129);
            this.DBPasswordTB.Name = "DBPasswordTB";
            this.DBPasswordTB.PasswordChar = '*';
            this.DBPasswordTB.Size = new System.Drawing.Size(258, 20);
            this.DBPasswordTB.TabIndex = 36;
            // 
            // DBPasswordLbl
            // 
            this.DBPasswordLbl.Location = new System.Drawing.Point(6, 129);
            this.DBPasswordLbl.Name = "DBPasswordLbl";
            this.DBPasswordLbl.Size = new System.Drawing.Size(90, 16);
            this.DBPasswordLbl.TabIndex = 35;
            this.DBPasswordLbl.Text = "Password:";
            // 
            // DBUserNameTB
            // 
            this.DBUserNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBUserNameTB.Location = new System.Drawing.Point(103, 103);
            this.DBUserNameTB.Name = "DBUserNameTB";
            this.DBUserNameTB.Size = new System.Drawing.Size(258, 20);
            this.DBUserNameTB.TabIndex = 34;
            // 
            // DBBUserNameLbl
            // 
            this.DBBUserNameLbl.Location = new System.Drawing.Point(6, 103);
            this.DBBUserNameLbl.Name = "DBBUserNameLbl";
            this.DBBUserNameLbl.Size = new System.Drawing.Size(90, 16);
            this.DBBUserNameLbl.TabIndex = 33;
            this.DBBUserNameLbl.Text = "User Name:";
            // 
            // TablePrefixTB
            // 
            this.TablePrefixTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablePrefixTB.Location = new System.Drawing.Point(103, 77);
            this.TablePrefixTB.MaxLength = 3;
            this.TablePrefixTB.Name = "TablePrefixTB";
            this.TablePrefixTB.Size = new System.Drawing.Size(258, 20);
            this.TablePrefixTB.TabIndex = 32;
            // 
            // TablePrefixLbl
            // 
            this.TablePrefixLbl.Location = new System.Drawing.Point(6, 77);
            this.TablePrefixLbl.Name = "TablePrefixLbl";
            this.TablePrefixLbl.Size = new System.Drawing.Size(90, 16);
            this.TablePrefixLbl.TabIndex = 31;
            this.TablePrefixLbl.Text = "Table Prefix:";
            // 
            // DBNameTb
            // 
            this.DBNameTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBNameTb.Location = new System.Drawing.Point(103, 51);
            this.DBNameTb.Name = "DBNameTb";
            this.DBNameTb.Size = new System.Drawing.Size(258, 20);
            this.DBNameTb.TabIndex = 30;
            // 
            // DBNameLbl
            // 
            this.DBNameLbl.Location = new System.Drawing.Point(6, 51);
            this.DBNameLbl.Name = "DBNameLbl";
            this.DBNameLbl.Size = new System.Drawing.Size(90, 16);
            this.DBNameLbl.TabIndex = 29;
            this.DBNameLbl.Text = "DB Name:";
            // 
            // DBHostTB
            // 
            this.DBHostTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBHostTB.Location = new System.Drawing.Point(103, 25);
            this.DBHostTB.Name = "DBHostTB";
            this.DBHostTB.Size = new System.Drawing.Size(258, 20);
            this.DBHostTB.TabIndex = 28;
            // 
            // DBHostLbl
            // 
            this.DBHostLbl.Location = new System.Drawing.Point(6, 25);
            this.DBHostLbl.Name = "DBHostLbl";
            this.DBHostLbl.Size = new System.Drawing.Size(90, 16);
            this.DBHostLbl.TabIndex = 27;
            this.DBHostLbl.Text = "DB Host:";
            // 
            // GXAmiDBSettingsForm
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(391, 217);
            this.Controls.Add(this.DatabaseSettingsGB);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GXAmiDBSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GuruxAMI Database Settings";
            this.DatabaseSettingsGB.ResumeLayout(false);
            this.DatabaseSettingsGB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.GroupBox DatabaseSettingsGB;
        private System.Windows.Forms.TextBox DBPasswordTB;
        private System.Windows.Forms.Label DBPasswordLbl;
        private System.Windows.Forms.TextBox DBUserNameTB;
        private System.Windows.Forms.Label DBBUserNameLbl;
        private System.Windows.Forms.TextBox TablePrefixTB;
        private System.Windows.Forms.Label TablePrefixLbl;
        private System.Windows.Forms.TextBox DBNameTb;
        private System.Windows.Forms.Label DBNameLbl;
        private System.Windows.Forms.TextBox DBHostTB;
        private System.Windows.Forms.Label DBHostLbl;
    }
}