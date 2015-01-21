# TimeLogApiSdk

Use this SDK for interacting with the TimeLog Project APIs. 

This source can also work as templates for your own projects. 

See http://api.timelog.dk for more documentation about the APIs.

## Available APIs

Please be aware that TimeLog Project has multiple APIs available and the authentication differs.

### TimeLog Project Reporting API

Use the reporting API to fetch big data sets from TimeLog Project for use in third party reporting systems.

For the reporting API you will need the SiteCode for your TimeLog Project site as well as ApiId and ApiPassword. Find the information in "System administration" > "Integrations and API" > "API settings (SOAP)".

Also ensure that you enable all the methods you need in the system administration.

It's encouraged to require SSL connections. This SDK will enforce it.

Use the Deploy\TimeLog.ReportingApi.SDK.dll for scaffolding classes for initialization and authentication.

### TimeLog Project Transactional API

Use the transactional API for transferring data to and from TimeLog Project to keep other systems in sync or automatically push updates to TimeLog Project.

For the transactional API you will need to use the credentials of a normal TimeLog Project user.

To activate and setup the transactional API, please go to "System administration" > "Integrations and API" > "API settings (WCF)".

It's encouraged to require SSL connections. This SDK will enforce it.

Use the Deploy\TimeLog.TransactionalApi.SDK.dll for scaffolding classes for initialization and authentication.