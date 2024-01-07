/*
Project title: wavASM
Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
The result is then saved back to the left channel.
The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.

Time: 1 term, 3 years
Author: Tomasz Artmanski
Version: 1.0
*/

using System;
using System.Media;
using System.Windows.Forms;

namespace wavMONO
{
    public partial class Form1 : Form
    {
        private SaveAsWav saver;
        private ProcessHandler processing;
        private string outputName;
        private bool isPlaying;
        private int selectedValue;

        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            PlaySound.Enabled = false;
            ASMCheck.Checked = true;
            isPlaying = false;

            selectedValue = Environment.ProcessorCount;
            trackBar1.Value = selectedValue;
            label17.Text = selectedValue.ToString();

            processing = new ProcessHandler();
            saver = new SaveAsWav();
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            byte[] wavBytes;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV Files (*.wav)|*.wav";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startTime = DateTime.Now;
                string inputName = openFileDialog.FileName;
                outputName = saver.AppendMonoToFileName(inputName);

                wavBytes = processing.Process(inputName, ASMCheck.Checked, selectedValue);

                saver.saveFile(outputName, wavBytes, processing.sampleRate);

                DateTime endTime = DateTime.Now;

                TimeSpan elapsedTime = endTime - startTime;

                if (ASMCheck.Checked)
                {
                    label14.Text = $"{elapsedTime.TotalMilliseconds:F2} ms";
                }
                else
                {
                    label15.Text = $"{elapsedTime.TotalMilliseconds:F2} ms";
                }

                PlaySound.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SoundPlayer player = new SoundPlayer(outputName))
                if (isPlaying)
                {
                    PlaySound.Text = "Play MONO";
                    player.Stop();
                    isPlaying = false;
                }
                else
                {
                    PlaySound.Text = "Stop";
                    player.Play();
                    isPlaying = true;
                }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void ASMCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ASMCheck.Checked == true)
            {
                CPPCheck.Checked = false;
            } else if (ASMCheck.Checked == false && CPPCheck.Checked == false)
            {
                CPPCheck.Checked = true;
            }
        }

        private void CPPCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CPPCheck.Checked == true)
            {
                ASMCheck.Checked = false;
            }
            else if (ASMCheck.Checked == false && CPPCheck.Checked == false)
            {
                ASMCheck.Checked = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            selectedValue = trackBar1.Value;
            label17.Text = selectedValue.ToString();
        }
    }
}
