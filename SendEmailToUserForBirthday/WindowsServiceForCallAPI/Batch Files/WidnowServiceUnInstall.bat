@ECHO OFF
 
REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319

 
echo Uninstalling Tonquin NotificationService...
echo ---------------------------------------------------
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u "D:\tatvasoft\training\WebAPI\SendEmailToUserForBirthday\WindowsServiceForCallAPI\bin\Debug\WindowsServiceForCallAPI.exe"
echo ---------------------------------------------------
echo Done

pause
pause