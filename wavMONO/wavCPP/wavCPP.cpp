#include "pch.h"

void wavMONO(short* leftChannel, short* rightChannel, int size) {

    for (int i = 0; i < size; ++i) {
        leftChannel[i] = (leftChannel[i] + rightChannel[i]) / 2;
    }

}

extern "C" __declspec(dllexport) void stereoToMono(short* leftChannel, short* rightChannel, int size) {
	wavMONO(leftChannel,rightChannel,size);
}