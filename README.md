# wavASM

Written in C#, C++ and MASM64. Program converts stereo WAV file to MONO using chosen technique.




## Algorithm explanation

Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
The result is then saved back to the left channel.
The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.
## Installation

Download project, install NAudio using Package Manager and build app.
## Authors

- [@Artmansky](https://github.com/Artmansky)

