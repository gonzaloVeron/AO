line=$(sed -n 2p testLogs.xml)

failedTest=$(echo $line | grep -oP 'failed=.[^\s]*' | grep -o '[[:digit:]]*')

if [ $failedTest -ne 0 ];
	then
		echo "Failed tests: " $failedTest
		exit 1
	else
		echo "All test passed"
		exit 0
fi
