using System.Collections.Generic;
using System.Linq;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers
{
    public class RestMethodDoc
    {
        #region Variables

        public IReadOnlyList<RestResponse> Responses { get; }

        public IReadOnlyList<RestMethodParam> Params { get; }

        public string MethodType { get; }

        public RestTypeDoc Parent { get; }

        public string OperationId { get; }

        public string FullName { get; }

        public string Summary { get; }

        public string Name { get; }

        #endregion

        #region Constructor

        public RestMethodDoc(RestTypeDoc restTypeDoc, RestAction action)
        {
            OperationId = action.OperationId;
            FullName = $"https://app[x].timelog.com/[account name]/api{action.Name}";
            Name = action.OperationId.Replace($"{action.Tags[0]}_", "");
            Summary = action.Summary;
            MethodType = action.MethodType.ToUpper();
            Parent = restTypeDoc;
            Params = MapToMethodParam(action.Parameters);
            Responses = action.Responses.OrderBy(x => x.Code).ToList();
        }

        #endregion

        #region Internal and Private Implementations

        private IReadOnlyList<RestMethodParam> MapToMethodParam(IReadOnlyList<RestParameter> parameters)
        {
            return parameters
                .Select(x => new RestMethodParam(x.Name, x.Description, x.Type, x.SchemaRestRef))
                .ToList();
        }

        #endregion
    }
}