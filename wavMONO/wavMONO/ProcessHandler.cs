using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wavMONO
{
    internal class ProcessHandler
    {
        private FileReader reading;
        private byte[] wavBytes;
        private int bytesPerSample = 2;
        private int numChannels = 2;
        public int sampleRate { get; set; }

        public ProcessHandler()
        {
            sampleRate = 0;
            reading = new FileReader();
        }

        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavASM.dll")]
        static extern short MyProc1(short rightChannel, short leftChannel);
        public byte[] Process(string inputName, string outputName)
        {
            try
            {
                int bytesPerFrame = bytesPerSample * numChannels;
                reading.ReadFileAsByteArray(inputName, bytesPerFrame);
                sampleRate = reading.sampleRate;

                wavBytes = new byte[bytesPerFrame * (reading.framesToRead * bytesPerSample)];

                for (int i = 0; i < reading.arraySize; i++)
                {
                    short monoSample = (short)((reading.leftSample[i] + reading.rightSample[i]) / 2);

                    BitConverter.GetBytes(monoSample).CopyTo(wavBytes, i * bytesPerSample);
                }

                return wavBytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
