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
        static extern void ASMtoMONO(short[] rightChannel, short[] leftChannel, int startIndex, int endIndex);

        [DllImport(@"C:\Users\Tomek\Documents\Asembler\wavMONO\x64\Debug\wavCPP.dll")]
        static extern void stereoToMono(short[] leftChannel, short[] rightChannel, int startIndex, int endIndex);

        public void runConversion(short[] leftChannel, short[] rightChannel, int size, bool isASM)
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