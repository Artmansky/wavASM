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
