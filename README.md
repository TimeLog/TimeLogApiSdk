# TimeLogApiSdk

Use this SDK for interacting with the TimeLog APIs. 

This source can also work as templates for your own projects. 

See [https://api.timelog.com](https://api.timelog.com) for more documentation about the APIs.

Please be aware that TimeLog has multiple APIs available and the authentication differs. 
Refer to the definition of the [3 flavors of access](https://api.timelog.com#3-flavor-of-access)

## Support

The SDK is provided "as is" and should be seen as examples and guidance.

For questions and bug reports associated with the API, you can write to support@timelog.com under the normal SLA for TimeLog customers.

For questions and bug reports associated with the SDK and code in this repository, you can write to support@timelog.dk but will not follow normal SLA for TimeLog customers.

## What's available

The list below is a quick rundown of the directories in this repository.

- Deploy - Download precompiled DLLs of the SDKs
- TimeLog.Api.Documentation - The documentation website
- TimeLog.ApiConsoleApp - A sample application for consuming the SDKs
- TimeLog.ReportingApi.Exporter - A console application to export XML or CSV from the reporting API
- TimeLog.ReportingApi.SDK - The reporting API SDK source code
- TimeLog.TransactionalApi.SDK - The transactional API SDK source code

### TimeLog Reporting API

- [Getting Started](https://api.timelog.com/reporting/gettingstarted)
- [List of methods](https://api.timelog.com/reporting/methods)
- [Using PowerBI with TimeLog](https://api.timelog.com/reporting/powerbi)
- [Synchronizing data](https://api.timelog.com/reporting/synchronizingdata)

### TimeLog Transactional API

Use the transactional API for transferring data to and from TimeLog to keep other systems in sync or automatically push updates to TimeLog.

For the transactional API you will need to use the credentials of a normal TimeLog user.

Use the Deploy\TimeLog.TransactionalApi.SDK.dll for scaffolding classes for initialization and authentication.
