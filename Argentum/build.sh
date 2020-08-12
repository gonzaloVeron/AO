#! /bin/sh

# NOTE the command args below make the assumption that your Unity project folder is
#  a subdirectory of the repo root directory, e.g. for this repo "unity-ci-test" 
#  the project folder is "UnityProject". If this is not true then adjust the 
#  -projectPath argument to point to the right location.

echo "Create the license file"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-createManualActivationFile \
	-logfile

echo "-----------------------------------------------------------------------------------"
cat Unity_v2020.1.0f1.alf
echo "-----------------------------------------------------------------------------------"
## Run the editor unit tests
echo "Running editor unit tests for ${UNITYCI_PROJECT_NAME}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-silent-crashes \
	-username "gonveron96@gmail.com" \
	-password "Tiranosaurio0" \
	-manualLicenseFile Unity_v2020.1.0f1 \
	-projectPath "$(pwd)/${UNITYCI_PROJECT_NAME}" \
	-runTests -testPlatform editmode \
	-logFile \
	-testResults $(pwd)/test.xml \
	-quit

rc0=$?
echo "Unit test logs"
cat $(pwd)/test.xml
echo rc0
# exit if tests failed
if [ $rc0 -ne 0 ]; then { echo "Failed unit tests"; exit $rc0; } fi

exit 0