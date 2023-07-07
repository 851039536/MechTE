@echo off

cd /d %~dp0

echo chekdll
set dllfile=MechTE_ContextMenu.dll
if not exist %dllfile% (
    echo %dllfile% is not exist!
    pause>nul 
    exit
)
".\RegAsm.exe"  /codebase %dllfile%

pause

exit