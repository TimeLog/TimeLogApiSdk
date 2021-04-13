using System.Collections.Generic;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Core.Documentation.Models
{
    public interface IRestManager
    {
        IEnumerable<RestDefinition> GetDefinitions();
        RestMethodDoc GetMethod(string id);
        RestTypeDoc GetService(string id);
        IEnumerable<RestTypeDoc> GetServices();
    }
}