Write-Output "Install chocolatey"

Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

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

Write-Output "Installing software"

# missing
Write-Output "choco install access2016runtime -y"
choco install access2016runtime -y

Write-Output "choco install msoledbsql -y"
choco install msoledbsql -y

# microsoft access database engine 2016 with custom package
#choco install made2016runtime -s="C:\ChocoPackages\made2016runtime" -y
Write-Output "choco install made2010 -y"
choco install made2010 -y
Write-Output "choco install SQL2012.ClrTypes --ignore-checksums -y"
choco install SQL2012.ClrTypes --ignore-checksums -y
Write-Output "choco install reportviewer2012 -y"
choco install reportviewer2012 -y
Write-Output "choco install crystalreports2010runtime -y"
choco install crystalreports2010runtime -y --install-arguments="'UPGRADE=1'"

Write-Output "choco install urlrewrite -y"
choco install urlrewrite -y
Write-Output "choco install iis-arr -y"
choco install iis-arr -y

Write-Output "choco install sql-server-management-studio --version=14.0.17285.0 -y"
choco install sql-server-management-studio --version=14.0.17285.0


