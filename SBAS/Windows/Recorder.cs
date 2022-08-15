using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Gui;

namespace SBAS
{
    public partial class Recorder : Form
    {
        bool isRecording = false;
        MemoryStream RecordedFile;
        WaveFileWriter WfW;
        RawSourceWaveStream WfR;
        WaveIn CurrentDevice;
        WaveOut Speakers;

        public Recorder()
        {
            InitializeComponent();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                DeviceList.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }
            CurrentDevice = new WaveIn() { DeviceNumber = DeviceList.SelectedIndex, WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(DeviceList.SelectedIndex).Channels) };
            DeviceList.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Record
            if (RecordButton.Text == "Record")
            {
                CurrentDevice.Dispose();
                CurrentDevice = new WaveIn() { DeviceNumber = DeviceList.SelectedIndex, WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(DeviceList.SelectedIndex).Channels) };
                PlayButton.Enabled = false;
                DiscardButton.Enabled = false;
                SaveButton.Enabled = false;
                button1.Enabled = false;
                RecordButton.Text = "Stop";
                RecordedFile?.Dispose();
                RecordedFile = new MemoryStream();
                WfW = new WaveFileWriter(RecordedFile, CurrentDevice.WaveFormat);
                CurrentDevice.DataAvailable += CurrentDevice_DataAvailable;
                CurrentDevice.StartRecording();
            }
            //Stop
            else
            {
                PlayButton.Enabled = true;
                DiscardButton.Enabled = true;
                SaveButton.Enabled = true;
                button1.Enabled = true;
                RecordButton.Text = "Record";
                CurrentDevice.StopRecording();
                RecordedFile.Position = 0;
                WfR = new RawSourceWaveStream(RecordedFile, CurrentDevice.WaveFormat);
            }
        }

        private void CurrentDevice_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (WfW != null)
            {
                WfW.Write(e.Buffer, 0, e.BytesRecorded);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Play
            RecordedFile.Position = 0;
            Speakers = new WaveOut();
            Speakers.Init(WfR);
            Speakers.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Discard
            PlayButton.Enabled = false;
            DiscardButton.Enabled = false;
            SaveButton.Enabled = false;
            button1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Save
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                Filter = "Wave Files (*.wav)|*.wav",
                Title = "Save and Import File...",
                RestoreDirectory = true,
            };
            RecordedFile.Position = 0;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                WaveFileWriter.CreateWaveFile(saveDialog.FileName, WfR);
                Importer importer = new Importer(false, false, "", "", saveDialog.FileName);
                importer.ShowDialog();
            }
        }

        private void DeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentDevice.DeviceNumber = DeviceList.SelectedIndex;
            CurrentDevice.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(DeviceList.SelectedIndex).Channels);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Save
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                Filter = "Wave Files (*.wav)|*.wav",
                Title = "Save and Quick Import File...",
                RestoreDirectory = true,
            };
            RecordedFile.Position = 0;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                WaveFileWriter.CreateWaveFile(saveDialog.FileName, WfR);
                Importer importer = new Importer(false, false, "", "", saveDialog.FileName, true);
                importer.ShowDialog();
            }
        }
    }
}
