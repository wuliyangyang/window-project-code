echo "Run Prox with Auto Mode"
if "%1" == "h" goto begin
mshta vbscript:createobject("wscript.shell").run("""%~nx0"" h",0)(window.close)&&exit
:begin

cd %~d0
cd %~dp0

copy /y .\Prox_ASemi.prj ..\Prox.prj
copy /y .\ASemi_config.json ..\TM\TesterConfig\config.json

cd..
ping 127.0.0.1 -n 2 >nul

start ..\TMLauncher.exe 0
