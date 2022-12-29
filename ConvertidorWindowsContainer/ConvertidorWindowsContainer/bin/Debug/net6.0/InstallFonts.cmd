REM Connect to the WinSxS share on the container host
for /f "tokens=3 delims=: " %%g in ('netsh interface ip show address ^| findstr /c:"Default Gateway"') do set GATEWAY=%%g
net use o: \\%GATEWAY%\WinSxS /user:ShareUser %SHARE_PW%
if errorlevel 1 goto :eof
 
dism /online /enable-feature /featurename:ServerCoreFonts-NonCritical-Fonts-MinConsoleFonts /Source:O:\ /LimitAccess
dism /online /enable-feature /featurename:ServerCoreFonts-NonCritical-Fonts-Support /Source:O:\ /LimitAccess
dism /online /enable-feature /featurename:ServerCoreFonts-NonCritical-Fonts-BitmapFonts /Source:O:\ /LimitAccess
dism /online /enable-feature /featurename:ServerCoreFonts-NonCritical-Fonts-TrueType /Source:O:\ /LimitAccess
dism /online /enable-feature /featurename:ServerCoreFonts-NonCritical-Fonts-UAPFonts /Source:O:\ /LimitAccess