ECHO npm run build STARTED ...
CALL npm run build
ECHO npm run build COMPLETED ...
PAUSE
ECHO do robocopy STARTED
for %%I in (.) do robocopy build ../../web/react/%%~nxI /MIR
ECHO do robocopy COMPLETED
REM exit 0