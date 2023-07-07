@echo off

cd /d %~dp0

echo chekdll
set dllfile=MechTE_ContextMenu.dll
if not exist %dllfile% (
    echo %dllfile% is not exist!
    pause>nul 
    exit
)
".\RegAsm.exe"  /codebase %dllfile% /u

echo Refresh the Resource Manager
taskkill /f /im explorer.exe & start explorer.exe

pause

exit