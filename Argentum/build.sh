#!/bin/bash

#
#   Builds Unity home window in 'dist' folder

echo === Prepare deployment folder ===
#UNITY_DIR=/tmp/unity
#mkdir $UNITY_DIR

echo "Download unity package list"

#curl -X GET 'https://core.cloud.unity3d.com/api/login' -v

echo

ifconfig

echo

echo 'Downloading Unity 2020.1.0 pkg:'
curl -o Unity.pkg https://beta.unity3d.com/download/2ab9c4179772/MacEditorInstaller/Unity.pkg?_ga=2.82049238.1779359896.1596674838-1256837220.1596674838
#curl -o Unity.pkg "http://beta.unity3d.com/download/90500643c620/MacEditorInstaller/Unity-5.4.0b13.pkg"
if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi

# Run installer(s)
echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /


echo "Verify firewall"
/usr/libexec/ApplicationFirewall/socketfilterfw --getappblocked /Applications/Unity/Unity.app/Contents/MacOS/Unity

echo "Create Certificate Folder"
mkdir ~/Library/Unity
mkdir ~/Library/Unity/Certificates

cp CACerts.pem ~/Library/Unity/Certificates/

echo "activate license"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -serial I3-GKE5-PKF4-XXXX-XXXX-XXXX -username "gonveron96@gmail.com" -password "Tiranosaurio0" -logfile


echo "Running editor unit tests for ${UNITYCI_PROJECT_NAME}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-projectPath "$(pwd)" \
	-runTests \
	-testResults "$(pwd)/testLogs.xml"\
	-logFile "$(pwd)/logs.txt"\

echo "┌───────────────┐"
echo "│Unity test logs│"
echo "└───────────────┘"

echo "------------------------- MUESTRO DIRECTORIO -----------------------------"
ls -las
echo "------------------------- MUESTRO DIRECTORIO -----------------------------"

sudo -E sh ./verifyTest.sh

# exit if tests failed
#if [ $rc0 -ne 0 ]; then { echo "Failed unit tests"; exit $rc0; } fi

echo "return license"

/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -returnlicense -logfile

echo === Done ===

