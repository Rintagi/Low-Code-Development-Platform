This power shell script in this folders are for setting up machines in order to run Rintagi based applications


Below should be run from an elevated(administrator) power shell prompt as it makes machine wide changes including install new softwares(via the utility choco)
It only install known source packages, if you are not comfortable with the scripts, just manually download from various vendors and do it manually


1. run rintagi_machine_setup_prerequisite.ps1 in the power shell prompt(assuming current directory is this directory, this is the base minimum for rintagi installation, production or development)

./rintagi_machine_setup_install_prerequisite.ps1

2. run rintagi_machine_setup_install_sqlexpress.ps1(if you want to use SQL Express as the database and hasn't installed it yet, so this is optional if you have your own SQL Server setup, just make sure it has mixed mode turned on)

./rintagi_machine_setup_install_prerequisite.ps1

3. run rintagi_install_NewRintagi.bat(need to find the path of the desired rintagi installer EXE path) 

./rintagi_install_NewRintagi.bat <the appropriate installer exe path>

4. run rintagi_machine_setup_install_devtools.ps1(if you are doing development on this machine, it would install the minimal required dev tools)

./rintagi_machine_setup_install_devtools.ps1