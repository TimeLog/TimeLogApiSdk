using System.Collections.Generic;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models;

public interface IRestManager
{
    IEnumerable<RestDefinition> GetDefinitions();

    RestMethodDoc GetMethod(string id);

    RestTypeDoc GetService(string id);

    IEnumerable<RestTypeDoc> GetServices();
}