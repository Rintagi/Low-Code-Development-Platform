
#Microsoft SQL Server Express edition
Write-Output "choco install sql-server-express -y"
choco install sql-server-express

#configure mixed authentication mode

$env:PSModulePath = [System.Environment]::GetEnvironmentVariable("PSModulePath","Machine")
#Import-Module -Name SQLPS
Install-PackageProvider -Name NuGet -MinimumVersion 2.8.5.201 -Force
Install-Module -Name SqlServer -AllowClobber -Force
Import-Module -Name SqlServer -Force

# Connect to the instance using SMO
$s = new-object ('Microsoft.SqlServer.Management.Smo.Server') '.\SQLEXPRESS'
[string]$nm = $s.Name
[string]$mode = $s.Settings.LoginMode
write-output "Instance Name: $nm"
write-output "Login Mode: $mode"
#Change to Mixed Mode
$s.Settings.LoginMode = [Microsoft.SqlServer.Management.SMO.ServerLoginMode]::Mixed
# Make the changes
$s.Alter()
Restart-Service -Force 'MSSQL$SQLEXPRESS'

#helpful and recommended even for non-developer and most likely needed when using SQL express(non-enterprise use)
#SQL Server Management Sutdio 18.10(to support newer SQL server)
#Write-Output "choco install sql-server-management-studio --version=15.0.18390.0 -y"
#choco install sql-server-management-studio --version=15.0.18390.0
#can't specify version as choco remove package from time to time
Write-Output "choco install sql-server-management-studio"
choco install sql-server-management-studio
