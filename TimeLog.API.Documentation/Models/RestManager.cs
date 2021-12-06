using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models;

public class RestManager : IRestManager
{
    private readonly RestDocumentationHelper restDocHelper;

    public RestManager(IWebHostEnvironment webHostEnvironment)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.REST.API.json");
        restDocHelper = new RestDocumentationHelper(filePath);
    }

    public IEnumerable<RestDefinition> GetDefinitions()
    {
        return restDocHelper.Definitions;
    }

    public RestMethodDoc GetMethod(string id)
    {
        return restDocHelper.Methods.First(x => x.OperationId.UrlEncode() == id);
    }

    public RestTypeDoc GetService(string id)
    {
        return restDocHelper.Types.First(x => x.Name.UrlEncode() == id);
    }

    public IEnumerable<RestTypeDoc> GetServices()
    {
        return restDocHelper.GetTypes().OrderBy(x => x.Name);
    }
}