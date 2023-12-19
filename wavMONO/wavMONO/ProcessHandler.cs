using System;
using System.Windows.Forms;

namespace wavMONO
{
    internal class ProcessHandler
    {
        private FileReader reading;
        private ThreadHandler threadning;
        private byte[] wavBytes;
        private int bytesPerSample = 2;
        private int numChannels = 2;
        public int sampleRate { get; set; }

        public ProcessHandler()
        {
            sampleRate = 0;
            reading = new FileReader();
        }

        public byte[] Process(string inputName, string outputName, bool isASM, int threads)
        {
            try
            {
                int bytesPerFrame = bytesPerSample * numChannels;
                reading.ReadFileAsByteArray(inputName, bytesPerFrame);
                sampleRate = reading.sampleRate;

                wavBytes = new byte[reading.framesToRead * bytesPerSample];

                threadning = new ThreadHandler(threads);
                threadning.runConversion(reading.leftSample,reading.rightSample,reading.arraySize,isASM);

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
