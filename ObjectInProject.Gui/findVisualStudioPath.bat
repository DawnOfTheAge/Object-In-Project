@echo off
setlocal 
call:vs%1 2>nul
if "%n%" == "" (
    echo Visual Studio Is Not Supported.
    exit /b
)
for /f "tokens=1,2*" %%a in ('reg query "HKLM\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\SxS\VS7" /v "%n%.0" 2^>nul') do set "VSPATH=%%c"
if "%VSPATH%" == "" (
    echo Visual studio %1 is not installed on this machine
    exit /b
)

echo %VSPATH%
endlocal & exit /b

:vs2017
    set /a "n=%n%+1"
:vs2015
    set /a "n=%n%+2"
:vs2013
    set /a "n=%n%+1"
:vs2012
    set /a "n=%n%+1"
:vs2010
    set /a "n=%n%+10"
    exit /b