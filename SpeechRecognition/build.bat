@echo off

set RootDirectory=%~dp0
set RootDirectory=%RootDirectory:\=/%

set ExecutableName=speech_recognition
set ExecutableSourceFiles=speech_recognition.cpp

set CommonCompilerFlags=/EHsc /nologo /WX /Zi /I%RootDirectory%include/
set CommonLinkerFlags=/LIBPATH:%RootDirectory%lib/ jvm.lib

if not exist "%RootDirectory%bin/" mkdir "%RootDirectory%bin/"

pushd "%RootDirectory%src/speech/"
copy * "%RootDirectory%bin/"
popd

pushd "%RootDirectory%bin/"

cl %CommonCompilerFlags% -Fe%ExecutableName%.exe %ExecutableSourceFiles% /link %CommonLinkerFlags%

if exist *.cpp del *.cpp
popd
