@echo off
set pspath=%windir%\Sysnative\WindowsPowerShell\v1.0
if not exist %pspath%\powershell.exe set pspath=%windir%\System32\WindowsPowerShell\v1.0
%pspath%\powershell.exe -ExecutionPolicy RemoteSigned %*