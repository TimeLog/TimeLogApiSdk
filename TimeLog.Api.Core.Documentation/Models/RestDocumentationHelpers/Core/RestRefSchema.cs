using System.Collections.Generic;
using System.Linq;

namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core
{
    /// <summary>
    /// Rest ref schema structure for swagger 2.0 format.
    /// </summary>
    public class RestRefSchema
    {
        private readonly IEnumerable<RestDefinition>? _definitions;
        public string? Value { get; }

        public RestDefinition? Definition
        {
            get
            {
                return _definitions?.FirstOrDefault(x => x.Name == Value);
            }
        }

        #region Constructor

        public RestRefSchema(string url, IEnumerable<RestDefinition>? definitions)
        {
            _definitions = definitions;
            if (!string.IsNullOrEmpty(url))
            {
                Value = url.Replace("#/definitions/", "");
            }
        }

        #endregion
    }
}