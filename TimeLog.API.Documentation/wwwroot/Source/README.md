# Documentation Extraction

## Rest API Documentation Process
`TimeLog.REST.API.json` will always be replace and it can be retrieve from TimeLog project path.  
Access to the following path for the actual `JSON` file, copy and replace to update documentation to the latest format.  
http://[localPath]/api/v1/docs

## Transactional API Documentation Process
`TimeLog.TLP.API.XML` needs to be replaced whenever we upgrade the API version so that it updates the public API documentation page we have here (https://api.timelog.com/transactional/services).
This file can be retrieved from TimeLog project path (TLP) `.\ASP\bin` whenever we build the TLP project.
Remember to also update the 'TimeLog.TransactionalAPI.SDK/Connected Services/[service you are updating]/ConnectedService.json' 
and its 'Reference.cs' by rightclicking the project and selecting "Add" And then 'Connected services'.
The same goes for the "OrganisationHandler.cs" or similar for the service you are updating.