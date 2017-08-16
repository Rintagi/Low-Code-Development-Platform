echo ... >> Install.log
echo ... Executing ROCmonSrcI.bat: >> Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.City"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.City" in "DataCmon\City.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.Company"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.Company" in "DataCmon\Company.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.Country"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.Country" in "DataCmon\Country.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.Firm"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.Firm" in "DataCmon\Firm.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.FxRate"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.FxRate" in "DataCmon\FxRate.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmon.dbo.State"  -S %1 -U %2 -P %3  >> DataCmon\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "ROCmon.dbo.State" in "DataCmon\State.txt"  -E  -e "DataCmon\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmon\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
exit /b 0
:ThereIsError
exit /b 99