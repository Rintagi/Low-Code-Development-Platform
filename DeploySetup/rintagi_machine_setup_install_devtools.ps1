
#visual studio code for quick code editing(cannot used to manage ASP.NET site, needs visual studio for that)
Write-Output "choco install vscode -y"
choco install vscode -y

#Write-Output "choco install adobereader -y"
#choco install adobereader -y

#Write-Output "choco install googlechrome -y"
#choco install googlechrome -y

#Write-Output "choco install nodejs-lts -y"
#choco install nodejs-lts -y

#stick to nodejs 10.x
Write-Output "choco install nodejs --version=10.16.3 -y"
choco install nodejs --version=10.16.3 -y



#force reloading of path after nodejs installation
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User") 

#below is a must for nodejs 12+ as node-sass require build from source instead of avaiable binary packages
#Write-Output "npm install --global windows-build-tools"
#npm install --global windows-build-tools

#git for windows
Write-Output "choco install git -y"
choco install git -y

#helpful and recommended even for non-developer BUT not really needed so here and not prerequesite 
#SQL Server Management Sutdio
Write-Output "choco install sql-server-management-studio --version=14.0.17285.0 -y"
choco install sql-server-management-studio --version=14.0.17285.0

Write-Output "You have to reboot for nodejs installation to be initialized properly"