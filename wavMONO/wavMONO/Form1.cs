using System;
using System.Windows.Forms;

namespace wavMONO
{
    public partial class Form1 : Form
    {
        private SaveAsWav saver;
        private ProcessHandler processing;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

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
                string outputName = saver.AppendMonoToFileName(inputName);

                wavBytes = processing.Process(inputName, outputName);

                saver.saveFile(outputName, wavBytes, processing.sampleRate);

                DateTime endTime = DateTime.Now;

                TimeSpan elapsedTime = endTime - startTime;

                label14.Text = $"{elapsedTime.TotalMilliseconds:F2} ms";
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            int count = checkedListBox1.Items.Count;

            for (int x = 0; x < count; x++)
            {
                if (index != x)
                {
                    checkedListBox1.SetItemChecked(x, false);
                }
            }
        }

    }
}
