/*
Project title: wavASM
Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
The result is then saved back to the left channel.
The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.

Time: 1 term, 3 years
Author: Tomasz Artmanski
Version: 1.0
*/

using NAudio.Wave;

namespace wavMONO
{
    internal class SaveAsWav
    {
        public void saveFile(string filePath, byte[] audioData, int sampleRate)
        {
            WaveFormat waveFormat = new WaveFormat(sampleRate, 16, 1);

            using (WaveFileWriter waveWriter = new WaveFileWriter(filePath, waveFormat))
            {
                waveWriter.Write(audioData, 0, audioData.Length);
            }
        }

        public string AppendMonoToFileName(string originalFileName)
        {
            string extension = System.IO.Path.GetExtension(originalFileName);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(originalFileName);

            string modifiedFileName = $"{fileNameWithoutExtension}_mono{extension}";

            string modifiedFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(originalFileName), modifiedFileName);

            return modifiedFilePath;
        }
    }
}
