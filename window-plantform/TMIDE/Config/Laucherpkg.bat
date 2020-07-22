set varpath=%~dp0..
echo %varpath%
md "%varpath%\laucherLibary"
copy %varpath%\libzmq.dll %varpath%\laucherLibary
copy %varpath%\log4cplus*.dll %varpath%\laucherLibary
copy %varpath%\msvcp120*.dll %varpath%\laucherLibary
copy %varpath%\msvcr120*.dll %varpath%\laucherLibary
copy %varpath%\vccorlib120*.dll %varpath%\laucherLibary
copy %varpath%\Poco*.dll %varpath%\laucherLibary
copy %varpath%\TMLauncher.exe %varpath%\laucherLibary
