#include "pch.h"

void wavMONO(short* leftChannel, short* rightChannel, int startIndex, int endIndex) {

    for (int i = startIndex; i < endIndex; ++i) {
        leftChannel[i] = (leftChannel[i] + rightChannel[i]) / 2;
    }

}

extern "C" __declspec(dllexport) void stereoToMono(short* leftChannel, short* rightChannel, int startIndex, int endIndex) {
	wavMONO(leftChannel,rightChannel, startIndex, endIndex);
}