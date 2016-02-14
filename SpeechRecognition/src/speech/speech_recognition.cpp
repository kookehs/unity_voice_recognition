#ifdef _WIN32
#include <windows.h>
#else
#include <unistd.h>
#endif

#include <iostream>
#include <string>

#include "voce/voce.h"

int
main(int agrc, char **argv) {
        voce::init("SpeechRecognition/lib", false, true,
                        "SpeechRecognition/grammar", "game");

        for (;;) {
#ifdef _WIN32
                ::Sleep(250);
#else
                usleep(250);
#endif

                while (voce::getRecognizerQueueSize() > 0) {
                        std::string command = voce::popRecognizedString();
                        std::cout << command << std::endl;
                }
        }

        voce::destroy();
        return 0;
}
