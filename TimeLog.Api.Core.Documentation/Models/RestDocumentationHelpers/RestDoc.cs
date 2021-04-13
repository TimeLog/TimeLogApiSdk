using System.Collections.Generic;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers
{
    /// <summary>
    /// Rest document root structure for swagger 2.0 format.
    /// </summary>
    public class RestDoc
    {
        #region Variables

        public IReadOnlyList<RestTag> Tags { get; }

        public IReadOnlyList<RestPath> RestPaths { get; }

        public RestInfo RestInfo { get; }

        #endregion

        #region Constructor

        public RestDoc(RestInfo restInfo, IReadOnlyList<RestTag> tags, IReadOnlyList<RestPath> restPaths)
        {
            RestInfo = restInfo;
            Tags = tags;
            RestPaths = restPaths;
        }

        #endregion
    }
}