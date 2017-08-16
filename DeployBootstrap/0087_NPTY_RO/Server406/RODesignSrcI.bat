echo ... >> Install.log
echo ... Executing RODesignSrcI.bat: >> Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AppInfo"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AppInfo" in "DataDesign\AppInfo.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AppItem"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AppItem" in "DataDesign\AppItem.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AppLog"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AppLog" in "DataDesign\AppLog.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AppZipId"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AppZipId" in "DataDesign\AppZipId.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AtRowAuth"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AtRowAuth" in "DataDesign\AtRowAuth.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.AtRowAuthPrm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.AtRowAuthPrm" in "DataDesign\AtRowAuthPrm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ButtonHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ButtonHlp" in "DataDesign\ButtonHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ClientRule"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ClientRule" in "DataDesign\ClientRule.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ClientTier"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ClientTier" in "DataDesign\ClientTier.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ColOvrd"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ColOvrd" in "DataDesign\ColOvrd.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CronJob"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CronJob" in "DataDesign\CronJob.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtAccess"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtAccess" in "DataDesign\CtAccess.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtAggregate"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtAggregate" in "DataDesign\CtAggregate.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtAlignment"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtAlignment" in "DataDesign\CtAlignment.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtAndOr"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtAndOr" in "DataDesign\CtAndOr.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtBgGradType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtBgGradType" in "DataDesign\CtBgGradType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtBorderStyle"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtBorderStyle" in "DataDesign\CtBorderStyle.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtButtonHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtButtonHlp" in "DataDesign\CtButtonHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtButtonType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtButtonType" in "DataDesign\CtButtonType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCheckBox"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCheckBox" in "DataDesign\CtCheckBox.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtClientScript"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtClientScript" in "DataDesign\CtClientScript.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCountry"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCountry" in "DataDesign\CtCountry.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCrawler"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCrawler" in "DataDesign\CtCrawler.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCrudType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCrudType" in "DataDesign\CtCrudType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCudAction"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCudAction" in "DataDesign\CtCudAction.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCulture"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCulture" in "DataDesign\CtCulture.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtCultureLbl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtCultureLbl" in "DataDesign\CtCultureLbl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtDataType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtDataType" in "DataDesign\CtDataType.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtDbProvider"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtDbProvider" in "DataDesign\CtDbProvider.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtDirection"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtDirection" in "DataDesign\CtDirection.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtDisplayType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtDisplayType" in "DataDesign\CtDisplayType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtEvent"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtEvent" in "DataDesign\CtEvent.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtEveryone"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtEveryone" in "DataDesign\CtEveryone.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtFontStyle"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtFontStyle" in "DataDesign\CtFontStyle.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtFontWeight"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtFontWeight" in "DataDesign\CtFontWeight.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtFormat"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtFormat" in "DataDesign\CtFormat.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtFramework"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtFramework" in "DataDesign\CtFramework.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtGridGrp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtGridGrp" in "DataDesign\CtGridGrp.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtHintQuestion"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtHintQuestion" in "DataDesign\CtHintQuestion.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtJustify"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtJustify" in "DataDesign\CtJustify.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtLanguage"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtLanguage" in "DataDesign\CtLanguage.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtLineType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtLineType" in "DataDesign\CtLineType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtLinkType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtLinkType" in "DataDesign\CtLinkType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtMatch"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtMatch" in "DataDesign\CtMatch.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtMsgType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtMsgType" in "DataDesign\CtMsgType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtObjectType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtObjectType" in "DataDesign\CtObjectType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtOperator"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtOperator" in "DataDesign\CtOperator.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtOrientation"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtOrientation" in "DataDesign\CtOrientation.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtOsType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtOsType" in "DataDesign\CtOsType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtPermKey"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtPermKey" in "DataDesign\CtPermKey.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtReleaseType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtReleaseType" in "DataDesign\CtReleaseType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtReportSct"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtReportSct" in "DataDesign\CtReportSct.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtReportType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtReportType" in "DataDesign\CtReportType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRowAuth"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRowAuth" in "DataDesign\CtRowAuth.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRowAuthPrm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRowAuthPrm" in "DataDesign\CtRowAuthPrm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptChart"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptChart" in "DataDesign\CtRptChart.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptChaType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptChaType" in "DataDesign\CtRptChaType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptCtrType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptCtrType" in "DataDesign\CtRptCtrType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptElmType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptElmType" in "DataDesign\CtRptElmType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptGroup"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptGroup" in "DataDesign\CtRptGroup.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptObjType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptObjType" in "DataDesign\CtRptObjType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptStyDef"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptStyDef" in "DataDesign\CtRptStyDef.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRptTblType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRptTblType" in "DataDesign\CtRptTblType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRuleCntType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRuleCntType" in "DataDesign\CtRuleCntType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRuleLayer"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRuleLayer" in "DataDesign\CtRuleLayer.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRuleMethod"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRuleMethod" in "DataDesign\CtRuleMethod.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtRuleType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtRuleType" in "DataDesign\CtRuleType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtScreenType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtScreenType" in "DataDesign\CtScreenType.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtSection"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtSection" in "DataDesign\CtSection.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtSelectType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtSelectType" in "DataDesign\CtSelectType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtSProcOnly"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtSProcOnly" in "DataDesign\CtSProcOnly.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtSysListVisible"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtSysListVisible" in "DataDesign\CtSysListVisible.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtTextAlign"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtTextAlign" in "DataDesign\CtTextAlign.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtTextDecor"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtTextDecor" in "DataDesign\CtTextDecor.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtTierType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtTierType" in "DataDesign\CtTierType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtUnit"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtUnit" in "DataDesign\CtUnit.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtVerticalAlign"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtVerticalAlign" in "DataDesign\CtVerticalAlign.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtViewType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtViewType" in "DataDesign\CtViewType.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtWizardType"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtWizardType" in "DataDesign\CtWizardType.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.CtWritingMode"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.CtWritingMode" in "DataDesign\CtWritingMode.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.DataTier"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.DataTier" in "DataDesign\DataTier.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.DbColumn"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.DbColumn" in "DataDesign\DbColumn.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.DbKey"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.DbKey" in "DataDesign\DbKey.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.DbTable"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.DbTable" in "DataDesign\DbTable.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Document"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Document" in "DataDesign\Document.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Entity"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Entity" in "DataDesign\Entity.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.GlobalFilter"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.GlobalFilter" in "DataDesign\GlobalFilter.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.GlobalFilterKey"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.GlobalFilterKey" in "DataDesign\GlobalFilterKey.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.GroupCol"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.GroupCol" in "DataDesign\GroupCol.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.GroupRow"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.GroupRow" in "DataDesign\GroupRow.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Label"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Label" in "DataDesign\Label.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.LastEmail"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.LastEmail" in "DataDesign\LastEmail.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MaintMsg"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MaintMsg" in "DataDesign\MaintMsg.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MemTrans"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MemTrans" in "DataDesign\MemTrans.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Menu"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Menu" in "DataDesign\Menu.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MenuHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MenuHlp" in "DataDesign\MenuHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MenuPrm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MenuPrm" in "DataDesign\MenuPrm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Msg"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Msg" in "DataDesign\Msg.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MsgCenter"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MsgCenter" in "DataDesign\MsgCenter.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.MyMenuOpt"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.MyMenuOpt" in "DataDesign\MyMenuOpt.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Num2Word"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Num2Word" in "DataDesign\Num2Word.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Ovride"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Ovride" in "DataDesign\Ovride.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.OvrideGrp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.OvrideGrp" in "DataDesign\OvrideGrp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.PageLnk"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.PageLnk" in "DataDesign\PageLnk.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.PageObj"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.PageObj" in "DataDesign\PageObj.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Release"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Release" in "DataDesign\Release.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReleaseDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReleaseDtl" in "DataDesign\ReleaseDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Report"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Report" in "DataDesign\Report.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportCri" in "DataDesign\ReportCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportCriHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportCriHlp" in "DataDesign\ReportCriHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportDel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportDel" in "DataDesign\ReportDel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportGrp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportGrp" in "DataDesign\ReportGrp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportHlp" in "DataDesign\ReportHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportObj"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportObj" in "DataDesign\ReportObj.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ReportObjHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ReportObjHlp" in "DataDesign\ReportObjHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RowOvrd"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RowOvrd" in "DataDesign\RowOvrd.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RowOvrdPrm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RowOvrdPrm" in "DataDesign\RowOvrdPrm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptCel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptCel" in "DataDesign\RptCel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptCha"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptCha" in "DataDesign\RptCha.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptCtr"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptCtr" in "DataDesign\RptCtr.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptElm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptElm" in "DataDesign\RptElm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptMemCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptMemCri" in "DataDesign\RptMemCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptMemCriDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptMemCriDtl" in "DataDesign\RptMemCriDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptMemFld"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptMemFld" in "DataDesign\RptMemFld.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptStyle"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptStyle" in "DataDesign\RptStyle.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptTbl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptTbl" in "DataDesign\RptTbl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptTemplate"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptTemplate" in "DataDesign\RptTemplate.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Rptwiz"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Rptwiz" in "DataDesign\Rptwiz.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptwizCat"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptwizCat" in "DataDesign\RptwizCat.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptwizCatDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptwizCatDtl" in "DataDesign\RptwizCatDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptwizDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptwizDtl" in "DataDesign\RptwizDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RptwizTyp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RptwizTyp" in "DataDesign\RptwizTyp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.RuleTier"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.RuleTier" in "DataDesign\RuleTier.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Screen"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Screen" in "DataDesign\Screen.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenCri" in "DataDesign\ScreenCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenCriHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenCriHlp" in "DataDesign\ScreenCriHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenDel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenDel" in "DataDesign\ScreenDel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenFilter"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenFilter" in "DataDesign\ScreenFilter.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenFilterHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenFilterHlp" in "DataDesign\ScreenFilterHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenHlp" in "DataDesign\ScreenHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenObj"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenObj" in "DataDesign\ScreenObj.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenObjHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenObjHlp" in "DataDesign\ScreenObjHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenTab"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenTab" in "DataDesign\ScreenTab.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScreenTabHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScreenTabHlp" in "DataDesign\ScreenTabHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScrMemCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScrMemCri" in "DataDesign\ScrMemCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ScrMemCriDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ScrMemCriDtl" in "DataDesign\ScrMemCriDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.SctGrpCol"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.SctGrpCol" in "DataDesign\SctGrpCol.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.SctGrpRow"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.SctGrpRow" in "DataDesign\SctGrpRow.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.ServerRule"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.ServerRule" in "DataDesign\ServerRule.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.SqlToSybMap"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.SqlToSybMap" in "DataDesign\SqlToSybMap.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.SredMebr"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.SredMebr" in "DataDesign\SredMebr.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.SredTime"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.SredTime" in "DataDesign\SredTime.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.StaticCs"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.StaticCs" in "DataDesign\StaticCs.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.StaticFi"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.StaticFi" in "DataDesign\StaticFi.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.StaticJs"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.StaticJs" in "DataDesign\StaticJs.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.StaticPg"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.StaticPg" in "DataDesign\StaticPg.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Systems"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Systems" in "DataDesign\Systems.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.TbdRule"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.TbdRule" in "DataDesign\TbdRule.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Template"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Template" in "DataDesign\Template.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UsrGroup"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UsrGroup" in "DataDesign\UsrGroup.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UsrGroupAuth"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UsrGroupAuth" in "DataDesign\UsrGroupAuth.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UsrImpr"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UsrImpr" in "DataDesign\UsrImpr.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UsrPref"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UsrPref" in "DataDesign\UsrPref.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UsrProvider"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UsrProvider" in "DataDesign\UsrProvider.txt"  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReport"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReport" in "DataDesign\UtReport.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportCri" in "DataDesign\UtReportCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportCriHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportCriHlp" in "DataDesign\UtReportCriHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportDel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportDel" in "DataDesign\UtReportDel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportHlp" in "DataDesign\UtReportHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportLstCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportLstCri" in "DataDesign\UtReportLstCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportObj"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportObj" in "DataDesign\UtReportObj.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtReportObjHlp"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtReportObjHlp" in "DataDesign\UtReportObjHlp.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptCel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptCel" in "DataDesign\UtRptCel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptCha"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptCha" in "DataDesign\UtRptCha.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptCtr"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptCtr" in "DataDesign\UtRptCtr.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptElm"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptElm" in "DataDesign\UtRptElm.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptMemCri"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptMemCri" in "DataDesign\UtRptMemCri.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptMemCriDtl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptMemCriDtl" in "DataDesign\UtRptMemCriDtl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptMemFld"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptMemFld" in "DataDesign\UtRptMemFld.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.UtRptTbl"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.UtRptTbl" in "DataDesign\UtRptTbl.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.WebRule"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.WebRule" in "DataDesign\WebRule.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.Wizard"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.Wizard" in "DataDesign\Wizard.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.WizardDel"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.WizardDel" in "DataDesign\WizardDel.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.WizardObj"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.WizardObj" in "DataDesign\WizardObj.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqlcmd" -Q "TRUNCATE TABLE RODesign.dbo.WizardRule"  -S %1 -U %2 -P %3  >> DataDesign\..\Install.log
"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\bcp" "RODesign.dbo.WizardRule" in "DataDesign\WizardRule.txt"  -E  -e "DataDesign\..\Error.txt" -S %1 -U %2 -P %3 -q -w -CRAW -t"~@~" -r"~#~" >> DataDesign\..\Install.log
IF ERRORLEVEL 1 GOTO ThereIsError
exit /b 0
:ThereIsError
exit /b 99