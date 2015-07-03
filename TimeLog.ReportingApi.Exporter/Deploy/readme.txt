Runs queries agains the TimeLog Project API and outputs the result in a file

TimeLog.ReportingApi.Exporter.exe /e methodName outputFile

/t      Executes a credentials validation request
/g      Generates a default configuration for a given methodName
/e      Exports data from the methodName to the outputFile
/m      Lists all the available methods

ExportFormats supported: Csv, Xml

Usage: TimeLog.ReportingApi.Exporter.exe /e GetCustomersRaw.config CustomersRaw.csv