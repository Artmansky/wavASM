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
using System.IO;
using System.Windows.Forms;

namespace wavMONO
{
    internal class FileReader
    {
        public int arraySize {  get; set; }
        public int[] leftSample {  get; set; }
        public int[] rightSample {  get; set; }
        public int sampleRate { get; set; }
        public int framesToRead { get; set; }

        public FileReader()
        {
            arraySize = 0;
        }
        public void ReadFileAsByteArray(string filePath, int bytesPerFrame)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        byte[] header = binaryReader.ReadBytes(44);
                        sampleRate = BitConverter.ToInt32(header, 24);

                        int remainingBytes = (int)(fileStream.Length - header.Length);
                        framesToRead = remainingBytes / bytesPerFrame; 

                        leftSample = new int[framesToRead];
                        rightSample = new int[framesToRead];

                        int size = 0;

                        for (int i = 0; i < framesToRead; i++)
                        {
                            leftSample[i] = binaryReader.ReadInt16();
                            rightSample[i] = binaryReader.ReadInt16();
                            size++;
                        }

                        arraySize = size;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
}
