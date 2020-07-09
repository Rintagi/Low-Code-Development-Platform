
#visual studio code for quick code editing(cannot used to manage ASP.NET site, needs visual studio for that)
Write-Output "choco install vscode -y"
choco install vscode -y

# .NET 4.6.2 devpack
# required for installer compilation(can either be here or prerequisite)
# https://www.microsoft.com/en-us/download/details.aspx?id=53321
Write-Output "choco install netfx-4.6.2-devpack -y"
choco install netfx-4.6.2-devpack -y

#Write-Output "choco install adobereader -y"
#choco install adobereader -y

#Write-Output "choco install googlechrome -y"
#choco install googlechrome -y

#stick to nodejs 10.x
#this is much more light weight than 12.x which requires visual studio build tools
#12.x is needed for other react development, say react-native or expo but not Rintagi react
#Write-Output "choco install nodejs --version=10.16.3 -y"
#choco install nodejs --version=10.16.3 -y

#this is nodejs 12.x as of 7/6/2020
Write-Output "choco install nodejs-lts -y"
choco install nodejs-lts -y

#force reloading of path after nodejs installation
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User") 

#below is a must for nodejs 12+ as node-sass require build from source instead of avaiable binary packages
#no longer needed as there are now binary package available for node-sass via npm install don't need to build locally
#Write-Output "npm install --global windows-build-tools"
#npm install --global windows-build-tools

#git for windows
#for version control, not required by highly recommended
Write-Output "choco install git -y"
choco install git -y

#helpful and recommended even for non-developer BUT not really needed so here and not prerequesite 
#SQL Server Management Sutdio
Write-Output "choco install sql-server-management-studio --version=15.0.18330.0 -y"
choco install sql-server-management-studio --version=15.0.18330.0

Write-Output "You have to reboot for nodejs installation to be initialized properly"