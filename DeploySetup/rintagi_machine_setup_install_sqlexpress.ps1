
Write-Output "choco install sql-server-express -y"
choco install sql-server-express

#configure mixed authentication mode

$env:PSModulePath = [System.Environment]::GetEnvironmentVariable("PSModulePath","Machine")

Import-Module SQLPS

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
