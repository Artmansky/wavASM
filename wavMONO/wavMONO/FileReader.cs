using System;
using System.IO;
using System.Windows.Forms;

namespace wavMONO
{
    internal class FileReader
    {
        public int arraySize {  get; set; }
        public short[] leftSample {  get; set; }
        public short[] rightSample {  get; set; }
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

                        leftSample = new short[framesToRead];
                        rightSample = new short[framesToRead];

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
