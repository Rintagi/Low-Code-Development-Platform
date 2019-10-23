The power shell scripts listed here are for setting up machines to run Rintagi based applications.

The scripts should be run from an elevated (administrator) power shell prompt as it makes machine-wide changes including installation of new software(via the utility choco).

It only installs known source packages, so if you are not comfortable with the scripts, just manually download from various vendors and install:

* Run rintagi_machine_setup_prerequisite.ps1 in the power shell prompt (assuming current directory is this directory, this is the base minimum for rintagi installation, production or development)

./rintagi_machine_setup_install_prerequisite.ps1

* Run rintagi_machine_setup_install_sqlexpress.ps1 (If you want SQL Express as the database and haven't installed it yet. If you have your own SQL Server setup, just make sure it has mixed mode turned on)

./rintagi_machine_setup_install_prerequisite.ps1

* Run rintagi_install_NewRintagi.bat (need to find the path of the desired rintagi installer EXE path)

./rintagi_install_NewRintagi.bat <the appropriate installer exe path>

* Run rintagi_machine_setup_install_devtools.ps1 (if development  is on this machine, it would install minimal required dev tools)

./rintagi_machine_setup_install_devtools.ps1

For detailed set-up instructions, please refer to our [user manual](https://www.rintagi.com/Docs/site/Initial-Setup/index.html).
