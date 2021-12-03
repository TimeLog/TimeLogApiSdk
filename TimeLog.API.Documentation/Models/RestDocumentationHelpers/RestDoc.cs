using System.Collections.Generic;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers;

/// <summary>
///     Rest document root structure for swagger 2.0 format.
/// </summary>
public class RestDoc
{
    #region Constructor

    public RestDoc(RestInfo restInfo, IReadOnlyList<RestTag> tags, IReadOnlyList<RestPath> restPaths)
    {
        RestInfo = restInfo;
        Tags = tags;
        RestPaths = restPaths;
    }

    #endregion

    #region Variables

    public IReadOnlyList<RestTag> Tags { get; }

    public IReadOnlyList<RestPath> RestPaths { get; }

    public RestInfo RestInfo { get; }

    #endregion
}