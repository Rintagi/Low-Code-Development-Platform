Write-Output "Setup TLS 1.0/1.1/1.2"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12 -bor [Net.SecurityProtocolType]::Tls11 -bor [Net.SecurityProtocolType]::Tls

Write-Output "Install chocolatey"

Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

choco feature enable -n allowGlobalConfirmation

Write-Output "Install windows features"

#Install-WindowsFeature -Name NET-Framework-Features,NET-Framework-45-ASPNET,NET-WCF-HTTP-Activation45,Windows-Internal-Database,Web-Dir-Browsing,Web-Http-Errors,Web-Http-Redirect,Web-DAV-Publishing,Web-Http-Logging,Web-Custom-Logging,Web-Log-Libraries,Web-ODBC-Logging,Web-Request-Monitor,Web-Http-Tracing,Web-Stat-Compression,Web-Dyn-Compression,Web-Basic-Auth,Web-CertProvider,Web-IP-Security,Web-Client-Auth,Web-Url-Auth,Web-Windows-Auth,Web-Digest-Auth,Web-Cert-Auth,Web-Net-Ext45,Web-Asp-Net45,Web-ISAPI-Ext,Web-ISAPI-Filter,Web-Scripting-Tools,Web-Mgmt-Service,Web-Mgmt-Console,Web-Metabase,Web-Lgcy-Mgmt-Console,Web-Lgcy-Scripting,Web-WMI -computerName $env:computername

$iisfeature = Get-WindowsOptionalFeature -Online | Where-Object {$_.FeatureName.StartsWith('IIS') -and !$_.FeatureName.Contains('Legacy') -and !$_.FeatureName.Contains('FTP') -and !$_.FeatureName.Contains('ODBC') -and !($_.FeatureName -eq "IIS-ASPNET") -and !($_.FeatureName -eq 'IIS-NetFxExtensibility') -and !($_.FeatureName -like 'IIS-NetFxExtensibility') -and !($_.FeatureName -eq 'IIS-WMICompatibility')} | Select FeatureName | Sort FeatureName | Foreach {"$($_.FeatureName)"}

Write-Output $iisfeature

Enable-WindowsOptionalFeature -Online -FeatureName @('NetFx4Extended-ASPNET45','IIS-WebServerRole','IIS-WebServer')

Enable-WindowsOptionalFeature -Online -FeatureName $iisfeature


Set-Service -Name aspnet_state -StartupType Automatic
Start-Service -Name aspnet_state

Write-Output "Create folders and setup permissions"

$user = "NETWORK SERVICE"
$Rights = "FullControl"
$InheritSettings = "Containerinherit, ObjectInherit"
$PropogationSettings = "None"
$RuleType = "Allow"

#$path = "C:\inetpub" 
#$acl = Get-Acl $path
#$perm = $user, $Rights, $InheritSettings, $PropogationSettings, $RuleType
#$rule = New-Object -TypeName System.Security.AccessControl.FileSystemAccessRule -ArgumentList $perm
#$acl.SetAccessRule($rule)
#$acl | Set-Acl -Path $path

#$path = "C:\Import"
#If(!(test-path $path))
#{
#    New-Item -ItemType Directory -Force -Path $path
#    $acl = Get-Acl $path
#    $perm = $user, $Rights, $InheritSettings, $PropogationSettings, $RuleType
#    $rule = New-Object -TypeName System.Security.AccessControl.FileSystemAccessRule -ArgumentList $perm
#    $acl.SetAccessRule($rule)
#    $acl | Set-Acl -Path $path
#}

$path = "C:\Rintagi"
If(!(test-path $path))
{
      New-Item -ItemType Directory -Force -Path $path
      $acl = Get-Acl $path
      $perm = $user, $Rights, $InheritSettings, $PropogationSettings, $RuleType
      $rule = New-Object -TypeName System.Security.AccessControl.FileSystemAccessRule -ArgumentList $perm
      $acl.SetAccessRule($rule)
      $acl | Set-Acl -Path $path
}

#$path = "C:\Deploy"
#If(!(test-path $path))
#{
#    New-Item -ItemType Directory -Force -Path $path
#    $acl = Get-Acl $path
#    $perm = $user, $Rights, $InheritSettings, $PropogationSettings, $RuleType
#    $rule = New-Object -TypeName System.Security.AccessControl.FileSystemAccessRule -ArgumentList $perm
#    $acl.SetAccessRule($rule)
#    $acl | Set-Acl -Path $path
#}

# need to test with a server that has "D:" drive

#cmd /c mklink /j C:\SQLData D:\SQLData
#cmd /c mklink /j C:\Backup D:\Backup

Write-Output "Installing prerequisite software"

#SQL server client(bcp/sqmcmd), would pull in ODBC driver as well
#must have for the installer to run and/or development(installer creation)
#https://www.microsoft.com/en-us/download/details.aspx?id=53591
Write-Output "choco install sqlserver-cmdlineutils -y"
choco install sqlserver-cmdlineutils

#MS oledb sql, required for tls 1.2 connectivity
# source url
# https://www.microsoft.com/en-us/download/details.aspx?id=56730

#Write-Output "choco install msoledbsql -y"
#choco install msoledbsql -y

#MS odbc driver for SQL server(17)
# source url 
# https://docs.microsoft.com/en-us/sql/connect/odbc/download-odbc-driver-for-sql-server?view=sql-server-ver15
# pull in by the client so not needed unless there is checksum issue
#Write-Output "choco install sqlserver-odbcdriver -y"
#choco install sqlserver-odbcdriver -y

#MS odbc driver for SQL server(13)
# source url 
# https://www.microsoft.com/en-us/download/details.aspx?id=53339
# default driver used, not needed if default changed to 17
# Write-Output "choco install sqlserver-odbcdriver --version=13.1.4413.46 -y"
# choco install sqlserver-odbcdriver --version=13.1.4413.46 -y

# microsoft access database engine runtime(needs 2010+) for excel file import
# source url (2016)
# https://www.microsoft.com/en-us/download/details.aspx?id=54920
# or (2010)
# https://www.microsoft.com/en-ca/download/details.aspx?id=13255

Write-Output "choco install access2016runtime -y"
choco install access2016runtime -y


#not needed anymore, included in access2016runtime
#Write-Output "choco install made2010 --ignore-checksums -y "
#choco install made2010 --ignore-checksums -y

# SQL 2012 CLR types
# source url 
# must come before sql report viewer 2012 to avoid checksum error
# https://www.microsoft.com/en-us/download/details.aspx?id=35580

Write-Output "choco install SQL2012.ClrTypes --ignore-checksums -y"
choco install SQL2012.ClrTypes --ignore-checksums -y

Write-Output "choco install SQL2014.ClrTypes --ignore-checksums -y"
choco install SQL2014.ClrTypes --ignore-checksums -y

#MS SQL Report Viewer
# 2012
# https://www.microsoft.com/en-ca/download/details.aspx?id=35747

Write-Output "choco install reportviewer2012 -y"
choco install reportviewer2012 --ignore-checksums -y

#MS SQL Report Viewer
# source url(2015)
# https://www.microsoft.com/en-us/download/details.aspx?id=45496
Write-Output "choco install ms-reportviewer2015 -y"
choco install ms-reportviewer2015 --ignore-checksums -y

#Write-Output "choco install crystalreports-for-visualstudio -y --install-arguments="'UPGRADE=1'""
#choco install crystalreports-for-visualstudio -y --install-arguments="'UPGRADE=1'"
#Write-Output "go to https://www.crystalreports.com/download/ to download manually"

# IIS urlrewrite module
# source url
# https://www.microsoft.com/en-ca/download/details.aspx?id=47337

Write-Output "choco install urlrewrite -y"
choco install urlrewrite -y

# IIS Application Request Routing(proxying) 3.0
# source url
# https://www.microsoft.com/en-us/download/details.aspx?id=47333
Write-Output "choco install iis-arr  --ignore-checksums -y"
choco install iis-arr  --ignore-checksums -y

# .NET 4.8 devpack
# required (can either be here or prerequisite)
# https://dotnet.microsoft.com/download/dotnet-framework/net48
Write-Output "choco install netfx-4.8-devpack -y"
choco install netfx-4.8-devpack -y

# crystal report viewer runtime(this take a long time as it is a full VS package for both 32/64 bit)
# below are the 'main site' 
# https://origin.softwaredownloads.sap.com/public/site/index.html
# 64 bit runtime (SP 33)
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649962022
# 32 bit runtime (SP 33)
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649922022
# 32 bit  for VS(VS2019 or lower) must have VS installed
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649932022
# 64 bit for VS(VS2022 only) must have VS installed
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649972022
# all version should have 13.0.4000.0 as the assembly version if required in web.config
#
Write-Output "installing Crystal Report 64 bit runtime for .NET(SP33), if fails follow instruction to do it manually"
Write-Output "use browser and go to https://origin-az.softwaredownloads.sap.com/public/file/0020000001649962022 to download crystal report runtime(64 bit, SP33)"
Write-Output "then run the msi"
Write-Output "the assembly version number is 13.0.4000.0"
Write-Output "or visit https://origin.softwaredownloads.sap.com/public/site/index.html if above fails(and find correct version, must be under crystal report for visual studio)"
Write-Output "must reboot to have the installation take effect"
# below may fail
# trial install and run
Invoke-WebRequest "https://origin-az.softwaredownloads.sap.com/public/file/0020000001649962022" -OutFile ./cr64SP33.msi
# this may fail
./cr64SP33.msi 

# this may be required for building production package(which is using 32 bit environment)
Write-Output "installing Crystal Report 32 bit runtime for .NET(SP33), if fails follow instruction to do it manually"
Write-Output "may need to do this if running 32 bit VS studio or need to use designer in VS studio(even 64 bit, VS 2022)"
Write-Output "use browser and go to https://origin-az.softwaredownloads.sap.com/public/file/0020000001649922022 to download crystal report runtime(32 bit, SP33)"
Write-Output "then run the msi"
Write-Output "the assembly version number is 13.0.4000.0"
Write-Output "or visit https://origin.softwaredownloads.sap.com/public/site/index.html if above fails(and find correct version, must be under crystal report for visual studio)"
Write-Output "must reboot to have the installation take effect"
# below may fail
# trial install and run
# Invoke-WebRequest "https://origin-az.softwaredownloads.sap.com/public/file/0020000001649922022" -OutFile ./cr32SP33.msi
# this may fail
#./cr32SP33.msi 