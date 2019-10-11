@echo off
if "%1"=="" goto usage

%1 /n:RO /v:2016 /dat:".\SQLEXPRESS" /au:roadmin /ap:password /s:"Default Web Site" /r /url:localhost /cln:c:\Rintagi\RO\Web /wsv:c:\Rintagi\RO\ROWs /xls:"c:\Rintagi\RO\Wsxls" /rul:c:\Rintagi\RO /srv:"Network Service" /sspi /c:c:\backup /d:c:\backup /u:"abc" /p:"abc" /m:n /overwrite
goto :eof

:usage
@echo "%0 <InstallExeName>"

:eof
EXIT 0