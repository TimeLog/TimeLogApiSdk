# TimeLogApiSdk

Use this SDK for interacting with the TimeLog APIs. 

This source can also work as templates for your own projects. 

See http://api.timelog.com for more documentation about the APIs.

Please be aware that TimeLog has multiple APIs available and the authentication differs.

## What's available

The list below is a quick rundown of the directories in this repository.

- Deploy - Download precompiled DLLs of the SDKs
- TimeLog.Api.Documentation - The documentation website
- TimeLog.ApiConsoleApp - A sample application for consuming the SDKs
- TimeLog.JavaScript.SDK - A time registration only SDK for JavaScript and a Chrome Extension sample
- TimeLog.ReportingApi.Exporter - A console application to export XML or CSV from the reporting API
- TimeLog.ReportingApi.SDK - The reporting API SDK source code
- TimeLog.TransactionalApi.SDK - The transactional API SDK source code

### TimeLog Reporting API

Use the reporting API to fetch big data sets from TimeLog for use in third party reporting systems.

For the reporting API you will need the SiteCode for your TimeLog site as well as ApiId and ApiPassword. Find the information in "System administration" > "Integrations and API" > "API settings (SOAP)".

Also ensure that you enable all the methods you need in the system administration.

It's encouraged to require SSL connections. This SDK will enforce it.

Use the Deploy\TimeLog.ReportingApi.SDK.dll for scaffolding classes for initialization and authentication.

### TimeLog Transactional API

Use the transactional API for transferring data to and from TimeLog to keep other systems in sync or automatically push updates to TimeLog.

For the transactional API you will need to use the credentials of a normal TimeLog user.

To activate and setup the transactional API, please go to "System administration" > "Integrations and API" > "API settings (WCF)".

It's encouraged to require SSL connections. This SDK will enforce it.

Use the Deploy\TimeLog.TransactionalApi.SDK.dll for scaffolding classes for initialization and authentication.
