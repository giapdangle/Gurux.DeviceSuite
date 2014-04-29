using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuruxAMI.Common;
using Gurux.Communication;
using Gurux.Serial;
using Gurux.Common;

namespace Gurux.DeviceSuite.Ami
{
    public partial class RedundantForm : Form
    {
        GXAmiDataCollector[] Collectors;
        GXAmiMediaType[] Medias;
        GXAmiDeviceMedia SelectedMedia;
        Gurux.Common.IGXMedia Media;
        GXClient Client;
        Form PropertiesForm;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="collectors"></param>
        /// <param name="medias"></param>
        /// <param name="media"></param>
        public RedundantForm(GXClient client, GXAmiDataCollector[] collectors, GXAmiMediaType[] medias, GXAmiDeviceMedia media)
        {
            InitializeComponent();
            Client = client;
            Collectors = collectors;
            Medias = medias;
            SelectedMedia = media;
            UpdateCollectors();
            if (media.DataCollectorId == null)
            {
                UpdateMedias();
            }
            if (!string.IsNullOrEmpty(SelectedMedia.Settings))
            {
                Media.Settings = SelectedMedia.Settings;
                ((IGXPropertyPage)PropertiesForm).Initialize();
            }
        }

        void UpdateCollectors()
        {
            CollectorsCB.Items.Clear();
            CollectorsCB.Items.Add("");            
            foreach (GXAmiDataCollector c in Collectors)
            {
                int pos = CollectorsCB.Items.Add(c);
                if (SelectedMedia.DataCollectorId == c.Id)
                {
                    CollectorsCB.SelectedIndex = pos;
                }
            }
            if (CollectorsCB.SelectedIndex == -1)
            {
                CollectorsCB.SelectedIndex = 0;
            }
        }

        bool FindMediaFromCollector(string media, GXAmiDataCollector collector)
        {            
            if (collector == null)
            {
                return true;
            }
            foreach(string it in collector.Medias)
            {
                if (string.Compare(it, media, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        void UpdateMedias()
        {
            MediaCB.Items.Clear();
            GXAmiDataCollector collector = null;
            if (CollectorsCB.SelectedItem is GXAmiDataCollector)
            {
                ulong id = (CollectorsCB.SelectedItem as GXAmiDataCollector).Id;
                foreach (GXAmiDataCollector it in Collectors)
                {
                    if (it.Id == id)
                    {
                        collector = it;
                        break;
                    }
                }
            }
            foreach (GXAmiMediaType mt in Medias)
            {
                //If collector is selected show only medias that is supports.
                if (FindMediaFromCollector(mt.Name, collector))
                {
                    int pos = MediaCB.Items.Add(mt);
                    if (string.Compare(SelectedMedia.Name, mt.Name) == 0)
                    {
                        MediaCB.SelectedIndex = pos;
                    }
                }
            }
            //If new Connection is added.
            if (MediaCB.SelectedIndex == -1)
            {
                MediaCB.SelectedIndex = 0;
            }
        }

        private void CollectorsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMedias();
        }

        /// <summary>
        /// Show media settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MediaFrame.Controls.Clear();
                string mediaName = Convert.ToString(MediaCB.SelectedItem);
                if (!string.IsNullOrEmpty(mediaName))
                {
                    Media = Client.SelectMedia(mediaName);
                    if (Media == null)
                    {
                        throw new Exception(MediaCB.Text + " media not found.");
                    }
                    if (Media is GXSerial)
                    {
                        foreach (GXAmiDataCollector it in Collectors)
                        {
                            (Media as GXSerial).AvailablePorts = it.SerialPorts;
                        }
                    }
                    PropertiesForm = Media.PropertiesForm;
                    ((IGXPropertyPage)PropertiesForm).Initialize();
                    while (PropertiesForm.Controls.Count != 0)
                    {
                        Control ctr = PropertiesForm.Controls[0];
                        if (ctr is Panel)
                        {
                            if (!ctr.Enabled)
                            {
                                PropertiesForm.Controls.RemoveAt(0);
                                continue;
                            }
                        }
                        MediaFrame.Controls.Add(ctr);
                    }
                }
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate media settings.
                ((IGXPropertyPage)PropertiesForm).Apply();                
                Media.Validate();
                //Select data collector.
                if (CollectorsCB.SelectedItem is GXAmiDataCollector)
                {
                    SelectedMedia.DataCollectorId = (CollectorsCB.SelectedItem as GXAmiDataCollector).Id;
                }
                else
                {
                    SelectedMedia.DataCollectorId = null;
                }
                //Update Media settings.
                SelectedMedia.Name = Media.MediaType;
                SelectedMedia.Settings = Media.Settings;                
            }
            catch (Exception ex)
            {
                GXCommon.ShowError(this, ex);
                DialogResult = DialogResult.None;
            }
        }
    }
}
