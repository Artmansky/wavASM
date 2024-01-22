/*
Project title: wavASM
Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
The result is then saved back to the left channel.
The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.

Time: 1 term, 3 years
Author: Tomasz Artmanski
Version: 1.0
*/

using System.Runtime.InteropServices;
using System.Threading;

namespace wavMONO
{
    internal class ThreadHandler
    {
        private int threads;
        public ThreadHandler(int threadCount)
        {
            threads = threadCount;
        }
        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavASM.dll")]
        static extern void ASMtoMONO(int[] rightChannel, int[] leftChannel, int startIndex, int endIndex);

        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavCPP.dll")]
        static extern void stereoToMono(int[] leftChannel, int[] rightChannel, int startIndex, int endIndex);

        public void runConversion(int[] leftChannel, int[] rightChannel, int size, bool isASM)
        {
            int chunkSize = size / threads;

            Thread[] conversionThreads = new Thread[threads];

            for (int i = 0; i < threads; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = (i + 1) * chunkSize;
                conversionThreads[i] = new Thread(() =>
                {
                    if (isASM)
                    {
                        ASMtoMONO(leftChannel, rightChannel, startIndex, endIndex);
                    }
                    else
                    {
                        stereoToMono(leftChannel, rightChannel, startIndex, endIndex);
                    }
                });

                conversionThreads[i].Start();
            }

            for (int i = 0; i < threads; i++)
            {
                conversionThreads[i].Join();
            }

            int remainingSize = size % threads;
            int remainingStartIndex = threads * chunkSize;

            if (remainingSize > 0)
            {
                if (isASM)
                {
                    ASMtoMONO(rightChannel, leftChannel, remainingStartIndex , size);
                }
                else
                {
                    stereoToMono(leftChannel, rightChannel, remainingStartIndex, size);
                }
            }

        }
    }
}