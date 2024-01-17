
#visual studio code for quick code editing(cannot used to manage ASP.NET site, needs visual studio for that)
Write-Output "choco install vscode -y"
choco install vscode -y

# .NET 4.6.2 devpack
# required for installer compilation(can either be here or prerequisite)
# https://www.microsoft.com/en-us/download/details.aspx?id=53321
# Write-Output "choco install netfx-4.6.2-devpack -y"
# choco install netfx-4.6.2-devpack -y

# .NET 4.8 devpack
# required (can either be here or prerequisite)
# https://dotnet.microsoft.com/download/dotnet-framework/net48
Write-Output "choco install netfx-4.8-devpack -y"
choco install netfx-4.8-devpack -y

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

#helpful and recommended even for non-developer and most likely needed when using SQL express(non-enterprise use)
#SQL Server Management Sutdio 18.10(to support newer SQL server)
#Write-Output "choco install sql-server-management-studio --version=15.0.18390.0 -y"
#choco install sql-server-management-studio --version=15.0.18390.0
#can't specify version as choco remove package from time to time
Write-Output "choco install sql-server-management-studio"
choco install sql-server-management-studio

# crystal report viewer runtime(this take a long time as it is a full VS package for both 32/64 bit)
# below are the 'main site' 
# https://origin.softwaredownloads.sap.com/public/site/index.html
# 64 bit runtime (SP 33)
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649962022
# 32 bit runtime (SP 33)
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649922022
# 32 bit  for VS(VS2019 or lower) must have VS installed
# https://origin-az.softwaredownloads.sap.com/public/file/0020000001649932022
# 64 bit for VS(VS2022 only) must have VS installed(at the time of writing only SP32 works, not SP33)
# https://origin-az.softwaredownloads.sap.com/public/file/0020000000661582022
# all version should have 13.0.4000.0 as the assembly version if required in web.config

Write-Output "use browser and go to https://origin-az.softwaredownloads.sap.com/public/file/0020000000661582022 to download crystal report for VS(64 bit, SP32 VS2022 only)"
Write-Output "then run the exe"
Write-Output "the assembly version number is 13.0.4000.0"
Write-Output "or visit https://origin.softwaredownloads.sap.com/public/site/index.html if above fails(and find correct version, must be under crystal report for visual studio)"
Write-Output "must also install the 32 bit runtime(part of the package) if you want to use the designer"
Write-Output "must reboot to have the installation take effect"

Write-Output "install msvc++ 2013 re-dist"
Write-Output "this is needed if you need to use crystal report designer 64 bit for vs 2022"
choco install MSVisualCplusplus2013-redist 1.1

Write-Output "You have to reboot for nodejs installation to be initialized properly"