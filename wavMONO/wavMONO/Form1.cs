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

                    byte[] bytes = new byte[wavBytes.Length/2];

                    for(int i = 0; i< wavBytes.Length/2; i++)
                    {
                        bytes[i] = wavBytes[wavBytes.Length/2 + i];
                    }

                    File.WriteAllBytes(outputName, bytes);
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
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
