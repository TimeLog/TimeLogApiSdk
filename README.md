# TimeLogApiSdk

Use this SDK for interacting with the TimeLog APIs. 

This source can also work as templates for your own projects. 

See http://api.timelog.com for more documentation about the APIs.

Please be aware that TimeLog has multiple APIs available and the authentication differs.

- Reporting API
    - Use for pulling data for reporting purposes
    - Only read-access is available
    - Server-to-server service user only
- Transactional API
    - Use for transactional purposes
    - Reading and writing data is available for selected data types
    - Server-to-server employee user only
    - Coordinator role available (add data for other users)
- REST API
    - Use for employee-centered registration purposes
    - Reading and writing data is available for selected data types
    - Employee-specific-token only
    - Coordinator role not available

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

Use the reporting API to fetch big data sets from TimeLog for use in third party reporting systems.

For the reporting API you will need the SiteCode for your TimeLog site as well as ApiId and ApiPassword. Find the information in "System administration" > "Integrations and API" > "Reporting API settings".

Use the Deploy\TimeLog.ReportingApi.SDK.dll for scaffolding classes for initialization and authentication.

**Recommended usage**

In order to replicate TimeLog data to your datawarehouse or similar we recommend the following procedure to ensure consistent data without heavy API usage:

- Every quarter fetch and sync the full data set in batches by month
- Every month fetch and sync the last 3 months of data
- Every week fetch and sync the last 4 weeks of data
- Every day fetch and sync the last 7 days of data

Note that you might need to adjust the intervals and periods to fit the data diciplin in your TimeLog instance.

### TimeLog Transactional API

Use the transactional API for transferring data to and from TimeLog to keep other systems in sync or automatically push updates to TimeLog.

For the transactional API you will need to use the credentials of a normal TimeLog user.

Use the Deploy\TimeLog.TransactionalApi.SDK.dll for scaffolding classes for initialization and authentication.
