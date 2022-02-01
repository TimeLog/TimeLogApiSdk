Runs queries agains the TimeLog API and outputs the result in a file

TimeLog.ReportingApi.Exporter.exe /e methodName outputFile

/t      Executes a credentials validation request
/g      Generates a default configuration for a given methodName
/e      Exports data from the methodName to the outputFile
/m      Lists all the available methods
/h		Shows this message

ExportFormats supported: Csv, Xml

Usage:
Step 1: Setup connection details in the associated .config file
Step 2: Generate a method configuration file using the /g parameter
Step 3: Execute an export command using the /e parameter