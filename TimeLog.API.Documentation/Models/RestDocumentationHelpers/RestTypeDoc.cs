using System.Collections.Generic;
using System.Linq;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers;

public class RestTypeDoc
{
    #region Constructor

    public RestTypeDoc(string name, RestInfo restInfo, RestTag? tag, IReadOnlyList<RestAction> restActions)
    {
        Version = restInfo.Version.Replace("v", "");
        Name = name;
        Methods = restActions
            .Select(x => new RestMethodDoc(this, x))
            .OrderBy(x => x.MethodType)
            .ThenBy(x => x.Name)
            .ToList();

        Description = tag != null ? tag.Description : string.Empty;
    }

    #endregion

    #region Variables

    public IEnumerable<RestMethodDoc> Methods { get; }

    public string Name { get; }

    public string Description { get; }

    public string Version { get; }

    #endregion
}