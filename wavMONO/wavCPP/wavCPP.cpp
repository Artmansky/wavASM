/*
Project title: wavASM
Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
The result is then saved back to the left channel.
The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.

Time: 1 term, 3 years
Author: Tomasz Artmanski
Version: 1.0
*/

#include "pch.h"

void wavMONO(int* leftChannel, int* rightChannel, int startIndex, int endIndex) {

    for (int i = startIndex; i < endIndex; ++i) {
        leftChannel[i] = (leftChannel[i] + rightChannel[i]) / 2;
    }

}

extern "C" __declspec(dllexport) void stereoToMono(int* leftChannel, int* rightChannel, int startIndex, int endIndex) {
	wavMONO(leftChannel,rightChannel, startIndex, endIndex);
}