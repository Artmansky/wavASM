using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;

namespace wavMONO
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavASM.dll")]
        static extern int MyProc1(int a, int b);

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        static string AppendMonoToFileName(string originalFileName)
        {
            string extension = System.IO.Path.GetExtension(originalFileName);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(originalFileName);

            string modifiedFileName = $"{fileNameWithoutExtension}_mono{extension}";

            string modifiedFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(originalFileName), modifiedFileName);

            return modifiedFilePath;
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            byte[] wavBytes;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV Files (*.wav)|*.wav";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string inputName = openFileDialog.FileName;
                string outputName = AppendMonoToFileName(inputName);
                try
                {
                    wavBytes = File.ReadAllBytes(inputName);

                    // Convert stereo WAV to mono by taking only the left channel
                    byte[] monoBytes = ConvertStereoToMono(wavBytes);

                    
                    SaveAsWav(outputName, monoBytes);






                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
        }

        static byte[] ConvertStereoToMono(byte[] data)
        {
            byte[] newData = new byte[data.Length / 2];

            for (int i = 0; i < data.Length / 4; ++i)
            {
                int HI = 1; int LO = 0;
                short left = (short)((data[i * 4 + HI] << 8) | (data[i * 4 + LO] & 0xff));
                short right = (short)((data[i * 4 + 2 + HI] << 8) | (data[i * 4 + 2 + LO] & 0xff));
                int avg = (left + right) / 2;

                newData[i * 2 + HI] = (byte)((avg >> 8) & 0xff);
                newData[i * 2 + LO] = (byte)((avg & 0xff));
            }

            return newData;
        }

        static void SaveAsWav(string filePath, byte[] audioData)
        {
            // Specify the audio format for the mono WAV file
            WaveFormat waveFormat = new WaveFormat(44100, 16, 1); // 44.1 kHz, 16-bit, mono

            // Create a WaveFileWriter to write the mono WAV file
            using (WaveFileWriter waveWriter = new WaveFileWriter(filePath, waveFormat))
            {
                waveWriter.Write(audioData, 0, audioData.Length);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index  = checkedListBox1.SelectedIndex;

            int count = checkedListBox1.Items.Count;

            for(int x = 0; x < count; x++)
            {
                if(index  != x)
                {
                    checkedListBox1.SetItemChecked(x, false);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
