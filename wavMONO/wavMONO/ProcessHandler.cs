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

        public byte[] Process(string inputName, bool isASM, int threads)
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
                    BitConverter.GetBytes((short)reading.leftSample[i]).CopyTo(wavBytes, i * bytesPerSample);
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
