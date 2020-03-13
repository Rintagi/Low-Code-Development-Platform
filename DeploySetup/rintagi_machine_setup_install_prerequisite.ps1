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

#MS oledb sql
# source url
# https://www.microsoft.com/en-us/download/details.aspx?id=56730

Write-Output "choco install msoledbsql -y"
choco install msoledbsql -y

# microsoft access database engine runtime(needs 2010+)
# source url (2016)
# https://www.microsoft.com/en-us/download/details.aspx?id=54920
# or (2010)
# https://www.microsoft.com/en-ca/download/details.aspx?id=13255

Write-Output "choco install access2016runtime -y"
choco install access2016runtime -y

Write-Output "choco install made2010 --ignore-checksums -y "
choco install made2010 --ignore-checksums -y

# SQL 2012 CLR types
# source url 
# https://www.microsoft.com/en-us/download/details.aspx?id=35580

Write-Output "choco install SQL2012.ClrTypes --ignore-checksums -y"
choco install SQL2012.ClrTypes --ignore-checksums -y

#MS SQL Report Viewer
# source url(2015)
# https://www.microsoft.com/en-us/download/details.aspx?id=45496
# or
# 2012
# https://www.microsoft.com/en-ca/download/details.aspx?id=35747

Write-Output "choco install reportviewer2012 -y"
choco install reportviewer2012 -y

# crystal report viewer runtime
# source url
# https://wiki.scn.sap.com/wiki/display/BOBJ/Crystal+Reports%2C+Developer+for+Visual+Studio+Downloads
Write-Output "choco install crystalreports2010runtime -y"
choco install crystalreports2010runtime -y --install-arguments="'UPGRADE=1'"

# IIS urlrewrite module
# source url
# https://www.microsoft.com/en-ca/download/details.aspx?id=47337

Write-Output "choco install urlrewrite -y"
choco install urlrewrite -y

# IIS Application Request Routing(proxying) 3.0
# source url
# https://www.microsoft.com/en-us/download/details.aspx?id=47333
Write-Output "choco install iis-arr -y"
choco install iis-arr -y


