using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuruxAMI.Common;
using GuruxAMI.Client;
using Gurux.Common;
using System.Runtime.InteropServices;
using System.Threading;
using Gurux.DeviceSuite.Director;

namespace Gurux.DeviceSuite.Ami
{
    public partial class GXAMICommandPromptTab : Form
    {
        string Media;
        string Settings;
        GXAmiClient Client;
        GXAmiDataCollector Collector;
        Control ParentDlg;
        GXAsyncWork TransactionWork;
        AutoResetEvent Connected = new AutoResetEvent(false);
        GXAmiTask ExecutedTask;

        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        public GXAMICommandPromptTab(Control parentForm, GXAmiClient client, GXAmiDataCollector collector, string media, string settings)
        {
            ParentDlg = parentForm;
            Media = media;
            Settings = settings.Replace(Environment.NewLine, "");
            Client = client;
            Client.OnDeviceErrorsAdded += new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded);
            Client.OnTasksAdded += new TasksAddedEventHandler(Client_OnTasksAdded);
            Collector = collector;
            InitializeComponent();
            CancelBtn.Dock = CommandPromptTB.Dock = DockStyle.Fill;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    CreateCaret(CommandPromptTB.Handle, IntPtr.Zero, 10, CommandPromptTB.Font.Height);
                    ShowCaret(CommandPromptTB.Handle);
                }
                catch
                {
                    //It's OK if this fails.
                }
            }
        }        

        public void CloseConnection()
        {
            try
            {
                Client.MediaClose(Collector.Guid, Media, Settings);
                Connected.WaitOne();
                Client.OnDeviceErrorsAdded -= new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded);
                Client.OnTasksAdded -= new TasksAddedEventHandler(Client_OnTasksAdded);
            }
            catch
            {
                //It's OK if this fails.
            }
        }

        delegate void UpdateDataEventHandler(byte[] data);

        void UpdateData(byte[] data)
        {
            if (HexCB.Checked)
            {
                CommandPromptTB.AppendText(GXCommon.ToHex(data, true) + Environment.NewLine);
            }
            else
            {
                CommandPromptTB.AppendText(ASCIIEncoding.ASCII.GetString(data) + Environment.NewLine);
            }
            CommandPromptTB.SelectionStart = CommandPromptTB.Text.Length;
            CommandPromptTB.ScrollToCaret();
        }

        /// <summary>
        /// Show reconnect text if connection is closed.
        /// Otherwice show clear text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMediaStateChange(object sender, MediaStateEventArgs e)
        {
            this.CommandPromptTB.Enabled = e.State == MediaState.Open;
            if (e.State == MediaState.Open)
            {
                CommandPromptClearBtn.Text = "Clear";
            }
            else
            {
                CommandPromptClearBtn.Text = "Reconnect";
            }
        }

        /// <summary>
        /// Show occurred errors on command prompth.
        /// </summary>
        void OnDeviceErrorsAdded(object sender, GXAmiDeviceError[] errors)
        {
            foreach (GXAmiDeviceError it in errors)
            {
                CommandPromptTB.AppendText(it.Message + Environment.NewLine);
                CommandPromptTB.SelectionStart = CommandPromptTB.Text.Length;
                CommandPromptTB.ScrollToCaret();
            }
        }

        void Client_OnDeviceErrorsAdded(object sender, GXAmiDeviceError[] errors)
        {
            if (ParentDlg.InvokeRequired)
            {
                this.BeginInvoke(new DeviceErrorsAddedEventHandler(Client_OnDeviceErrorsAdded), sender, errors);
            }
            else 
            {
                Client_OnDeviceErrorsAdded(sender, errors);
            }
        }

        void Client_OnTasksAdded(object sender, GXAmiTask[] tasks)
        {
            foreach (GXAmiTask it in tasks)
            {
                System.Diagnostics.Debug.Assert(it.SenderDataCollectorGuid == Collector.Guid);
                if (it.TaskType == TaskType.MediaState)
                {
                    Connected.Set();
                    string[] tmp = it.Data.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
                    string media = tmp[0];
                    string settings = tmp[1];
                    if (Media == media && Settings == settings)
                    {
                        if (ParentDlg.InvokeRequired)
                        {
                            ParentDlg.BeginInvoke(new MediaStateChangeEventHandler(OnMediaStateChange), sender, new MediaStateEventArgs((MediaState)Convert.ToInt32(tmp[2])));
                        }
                        else
                        {
                            OnMediaStateChange(sender, new MediaStateEventArgs((MediaState)Convert.ToInt32(tmp[2])));
                        }
                        Client.RemoveTask(it);
                    }
                }
                if (it.TaskType == TaskType.MediaWrite)
                {
                    string[] tmp = it.Data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string media = tmp[0];
                    string settings = tmp[1];
                    if (Media == media && Settings == settings)
                    {
                        ParentDlg.BeginInvoke(new UpdateDataEventHandler(UpdateData), Gurux.Common.GXCommon.HexToBytes(tmp[2], false));
                        Client.RemoveTask(it);
                    }
                }
            }
        }

        /// <summary>
        /// Handle user back keys so user can't delete previous lines;
        /// </summary>
        private void CommandPromptTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                int len = CommandPromptTB.Text.Length;
                if (len > 2)
                {
                    if (CommandPromptTB.Text.Substring(len - 2, 2) == Environment.NewLine)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// If user press Delete all data is cleared.
        /// If user press Enter data is sent to the DC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandPromptTB_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
                    e.KeyData == Keys.Home || e.KeyData == Keys.End)
                {
                    e.Handled = true;
                }
                else if (e.KeyData == Keys.Delete)
                {
                    CommandPromptTB.Text = string.Empty;
                }
                else if (e.KeyData == Keys.Enter)
                {
                    byte[] data;
                    string[] lines = CommandPromptTB.Lines;
                    string str = lines[lines.Length - 2];
                    if (str.Length == 0)
                    {
                        return;
                    }
                    if (HexCB.Checked)
                    {
                        string[] values = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        data = new byte[values.Length];
                        for (int pos = 0; pos < values.Length; ++pos)
                        {
                            int a = int.Parse(str[2 * pos].ToString(), System.Globalization.NumberStyles.HexNumber);
                            int b = int.Parse(str[(2 * pos) + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                            data[pos] = (byte)((a << 4) + b);
                        }
                    }
                    else
                    {
                        if (AddNewLineCB.Checked)
                        {
                            str += Environment.NewLine;
                        }
                        data = ASCIIEncoding.ASCII.GetBytes(str);
                    }
                    Client.Write(Collector.Guid, Media, Settings, data, 0, null);
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(ParentDlg, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void CommandPromptTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
                    e.KeyData == Keys.Home || e.KeyData == Keys.End)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void CommandPromptTB_MouseCaptureChanged(object sender, EventArgs e)
        {
            return;            
        }

        private void CommandPromptTB_MouseUp(object sender, MouseEventArgs e)
        {
            return;
        }

        private void CommandPromptTB_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void ConnectAsync(object sender, GXAsyncWork work, object[] parameters)
        {
            ExecutedTask = Client.MediaOpen((Guid)parameters[0], (string)parameters[1], (string)parameters[2]);
            Connected.WaitOne();            
        }

        void OnAsyncStateChange(object sender, GXAsyncWork work, object[] parameters, AsyncState state, string text)
        {
            CommandPromptTB.Visible = CommandPromptClearBtn.Visible = state != AsyncState.Start;
            CancelBtn.Visible = state == AsyncState.Start;            
        }

        /// <summary>
        /// Clear text if connected. Otherwice reconnect.
        /// </summary>
        private void CommandPromptClearBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommandPromptTB.Enabled)
                {
                    CommandPromptTB.Text = string.Empty;
                }
                else
                {
                    TransactionWork = new GXAsyncWork(this, OnAsyncStateChange, ConnectAsync, null, Gurux.DeviceSuite.Properties.Resources.ConnectingTxt, new object[] { Collector.Guid, Media, Settings });
                    TransactionWork.Start();
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(ParentDlg, Gurux.DeviceSuite.Properties.Resources.GuruxDeviceSuiteTxt, ex);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            TransactionWork.Cancel();
        }       
    }
}
