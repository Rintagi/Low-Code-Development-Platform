echo ... >> Install.log
echo ... Executing ROCmonDSrcI.bat: >> Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.AdvRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.AdvRule" in "DataCmonD\AdvRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.AppInfo"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.AppInfo" in "DataCmonD\AppInfo.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.AppItem"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.AppItem" in "DataCmonD\AppItem.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.AppZip"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.AppZip" in "DataCmonD\AppZip.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ButtonHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ButtonHlp" in "DataCmonD\ButtonHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ClientRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ClientRule" in "DataCmonD\ClientRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ColOvrd"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ColOvrd" in "DataCmonD\ColOvrd.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.CronJob"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.CronJob" in "DataCmonD\CronJob.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.DbColumn"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.DbColumn" in "DataCmonD\DbColumn.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.DbKey"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.DbKey" in "DataCmonD\DbKey.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.DbTable"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.DbTable" in "DataCmonD\DbTable.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Document"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Document" in "DataCmonD\Document.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.GlobalFilter"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.GlobalFilter" in "DataCmonD\GlobalFilter.txt"  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.GlobalFilterKey"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.GlobalFilterKey" in "DataCmonD\GlobalFilterKey.txt"  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Label"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Label" in "DataCmonD\Label.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.MemTrans"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.MemTrans" in "DataCmonD\MemTrans.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Menu"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Menu" in "DataCmonD\Menu.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.MenuHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.MenuHlp" in "DataCmonD\MenuHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.MenuPrm"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.MenuPrm" in "DataCmonD\MenuPrm.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Msg"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Msg" in "DataCmonD\Msg.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.MsgCenter"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.MsgCenter" in "DataCmonD\MsgCenter.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Report"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Report" in "DataCmonD\Report.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportCri" in "DataCmonD\ReportCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportCriHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportCriHlp" in "DataCmonD\ReportCriHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportDel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportDel" in "DataCmonD\ReportDel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportGrp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportGrp" in "DataCmonD\ReportGrp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportHlp" in "DataCmonD\ReportHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportLstCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportLstCri" in "DataCmonD\ReportLstCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportObj"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportObj" in "DataCmonD\ReportObj.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ReportObjHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ReportObjHlp" in "DataCmonD\ReportObjHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RowOvrd"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RowOvrd" in "DataCmonD\RowOvrd.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RowOvrdPrm"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RowOvrdPrm" in "DataCmonD\RowOvrdPrm.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptCel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptCel" in "DataCmonD\RptCel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptCha"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptCha" in "DataCmonD\RptCha.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptCtr"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptCtr" in "DataCmonD\RptCtr.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptElm"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptElm" in "DataCmonD\RptElm.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptMemCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptMemCri" in "DataCmonD\RptMemCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptMemCriDtl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptMemCriDtl" in "DataCmonD\RptMemCriDtl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptMemFld"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptMemFld" in "DataCmonD\RptMemFld.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptStyle"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptStyle" in "DataCmonD\RptStyle.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptTbl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptTbl" in "DataCmonD\RptTbl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptTemplate"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptTemplate" in "DataCmonD\RptTemplate.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Rptwiz"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Rptwiz" in "DataCmonD\Rptwiz.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptwizCat"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptwizCat" in "DataCmonD\RptwizCat.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptwizCatDtl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptwizCatDtl" in "DataCmonD\RptwizCatDtl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptwizDtl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptwizDtl" in "DataCmonD\RptwizDtl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RptwizTyp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RptwizTyp" in "DataCmonD\RptwizTyp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RuleAsmx"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RuleAsmx" in "DataCmonD\RuleAsmx.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.RuleReact"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.RuleReact" in "DataCmonD\RuleReact.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Screen"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Screen" in "DataCmonD\Screen.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenCri" in "DataCmonD\ScreenCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenCriHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenCriHlp" in "DataCmonD\ScreenCriHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenDel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenDel" in "DataCmonD\ScreenDel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenFilter"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenFilter" in "DataCmonD\ScreenFilter.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenFilterHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenFilterHlp" in "DataCmonD\ScreenFilterHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenHlp" in "DataCmonD\ScreenHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenLstCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenLstCri" in "DataCmonD\ScreenLstCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenLstInf"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenLstInf" in "DataCmonD\ScreenLstInf.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenObj"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenObj" in "DataCmonD\ScreenObj.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenObjHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenObjHlp" in "DataCmonD\ScreenObjHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenTab"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenTab" in "DataCmonD\ScreenTab.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScreenTabHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScreenTabHlp" in "DataCmonD\ScreenTabHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScrMemCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScrMemCri" in "DataCmonD\ScrMemCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ScrMemCriDtl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ScrMemCriDtl" in "DataCmonD\ScrMemCriDtl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.ServerRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.ServerRule" in "DataCmonD\ServerRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.StaticCs"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.StaticCs" in "DataCmonD\StaticCs.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.StaticFi"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.StaticFi" in "DataCmonD\StaticFi.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.StaticJs"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.StaticJs" in "DataCmonD\StaticJs.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.StaticPg"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.StaticPg" in "DataCmonD\StaticPg.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.TbdRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.TbdRule" in "DataCmonD\TbdRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Template"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Template" in "DataCmonD\Template.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReport"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReport" in "DataCmonD\UtReport.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportCri" in "DataCmonD\UtReportCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportCriHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportCriHlp" in "DataCmonD\UtReportCriHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportDel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportDel" in "DataCmonD\UtReportDel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportHlp" in "DataCmonD\UtReportHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportLstCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportLstCri" in "DataCmonD\UtReportLstCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportObj"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportObj" in "DataCmonD\UtReportObj.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtReportObjHlp"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtReportObjHlp" in "DataCmonD\UtReportObjHlp.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptCel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptCel" in "DataCmonD\UtRptCel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptCha"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptCha" in "DataCmonD\UtRptCha.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptCtr"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptCtr" in "DataCmonD\UtRptCtr.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptElm"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptElm" in "DataCmonD\UtRptElm.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptMemCri"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptMemCri" in "DataCmonD\UtRptMemCri.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptMemCriDtl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptMemCriDtl" in "DataCmonD\UtRptMemCriDtl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptMemFld"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptMemFld" in "DataCmonD\UtRptMemFld.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.UtRptTbl"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.UtRptTbl" in "DataCmonD\UtRptTbl.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.WebRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.WebRule" in "DataCmonD\WebRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.Wizard"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.Wizard" in "DataCmonD\Wizard.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.WizardDel"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.WizardDel" in "DataCmonD\WizardDel.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.WizardObj"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.WizardObj" in "DataCmonD\WizardObj.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE ROCmonD.dbo.WizardRule"  -S %1 -U %2 -P %3  >> DataCmonD\..\Install.log
"C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\bcp" "ROCmonD.dbo.WizardRule" in "DataCmonD\WizardRule.txt"  -E  -e "DataCmonD\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataCmonD\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
exit /b 0
:ThereIsError
exit /b 99