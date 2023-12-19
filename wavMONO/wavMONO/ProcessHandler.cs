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
        static extern void ASMtoMONO(short[] rightChannel, short[] leftChannel, int size);

        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavCPP.dll")]
        static extern void stereoToMono(short[] leftChannel, short[] rightChannel, int size);
        public byte[] Process(string inputName, string outputName, bool isASM)
        {
            try
            {
                int bytesPerFrame = bytesPerSample * numChannels;
                reading.ReadFileAsByteArray(inputName, bytesPerFrame);
                sampleRate = reading.sampleRate;

                wavBytes = new byte[reading.framesToRead * bytesPerSample];

                if (isASM)
                {
                    ASMtoMONO(reading.leftSample, reading.rightSample, reading.arraySize);
                }
                else
                {
                    stereoToMono(reading.leftSample,reading.rightSample, reading.arraySize);
                }

                for (int i = 0; i < reading.arraySize; i++)
                {
                    BitConverter.GetBytes(reading.leftSample[i]).CopyTo(wavBytes, i * bytesPerSample);
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
