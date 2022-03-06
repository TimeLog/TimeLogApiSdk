using TimeLog.Api.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models;

public class RestManager : IRestManager
{
    private readonly RestDocumentationHelper _restDocHelper;

    public RestManager(IWebHostEnvironment webHostEnvironment)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.REST.API.json");
        _restDocHelper = new RestDocumentationHelper(filePath);
    }

    public IEnumerable<RestDefinition> GetDefinitions()
    {
        return _restDocHelper.Definitions;
    }

    public RestMethodDoc GetMethod(string id)
    {
        return _restDocHelper.Methods.First(x => x.OperationId.UrlEncode().ToLowerInvariant() == id.ToLowerInvariant());
    }

    public RestTypeDoc GetService(string id)
    {
        return _restDocHelper.Types.First(x => x.Name.UrlEncode().ToLowerInvariant() == id.ToLowerInvariant());
    }

    public IEnumerable<RestTypeDoc> GetServices()
    {
        return _restDocHelper.GetTypes().OrderBy(x => x.Name);
    }
}