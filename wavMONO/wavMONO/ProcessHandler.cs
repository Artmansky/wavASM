using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wavMONO
{
    internal class ProcessHandler
    {
        public int sampleRate { get; set; }

        public ProcessHandler() {
            sampleRate = 0;    
        }
        public byte[] Process(string inputName, string outputName)
        {
            byte[] wavBytes;
            try
            {
                using (FileStream fileStream = new FileStream(inputName, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    var header = reader.ReadBytes(44);

                    sampleRate = BitConverter.ToInt32(header, 24);

                    int bytesPerSample = 2;
                    int numChannels = 2;
                    int bytesPerFrame = bytesPerSample * numChannels;

                    int remainingBytes = (int)(fileStream.Length - header.Length);
                    int framesToRead = remainingBytes / bytesPerFrame;
                    wavBytes = new byte[framesToRead * bytesPerSample];

                    for (int i = 0; i < framesToRead; i++)
                    {
                        short leftSample = reader.ReadInt16();
                        short rightSample = reader.ReadInt16();

                        short monoSample = (short)((leftSample + rightSample) / 2);

                        BitConverter.GetBytes(monoSample).CopyTo(wavBytes, i * bytesPerSample);
                    }

                    return wavBytes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
