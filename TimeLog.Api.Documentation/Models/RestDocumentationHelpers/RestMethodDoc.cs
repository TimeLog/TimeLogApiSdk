using System.Collections.Generic;
using System.Linq;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers
{
    public class RestMethodDoc
    {
        #region Variables

        public IReadOnlyList<RestResponse> Responses { get; }

        public IReadOnlyList<RestMethodParam> Params { get; }

        public string MethodType { get; }

        public RestTypeDoc Parent { get; private set; }

        public string OperationId { get; private set; }

        public string FullName { get; private set; }

        public string Summary { get; private set; }

        public string Name { get; private set; }

        #endregion

        #region Constructor

        public RestMethodDoc(RestTypeDoc restTypeDoc, RestAction action)
        {
            OperationId = action.OperationId;
            FullName = $"http://app[x].timelog.com/[account name]{action.Name}";
            Name = action.OperationId.Replace($"{action.Tags[0]}_", "");
            Summary = action.Summary;
            MethodType = action.MethodType;
            Parent = restTypeDoc;
            Params = MapToMethodParam(action.Parameters);
            Responses = action.Responses;
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