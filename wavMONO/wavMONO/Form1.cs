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

        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            PlaySound.Enabled = false;
            ASMCheck.Checked = true;
            isPlaying = false;

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

                wavBytes = processing.Process(inputName, outputName, ASMCheck.Checked);

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
                    player.Stop();
                    isPlaying = false;
                }
                else
                {
                    player.Play();
                    isPlaying = true;
                }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void ASMCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ASMCheck.Checked)
            {
                CPPCheck.Checked = false;
            }
        }

        private void CPPCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CPPCheck.Checked)
            {
                ASMCheck.Checked = false;
            }
        }
    }
}
