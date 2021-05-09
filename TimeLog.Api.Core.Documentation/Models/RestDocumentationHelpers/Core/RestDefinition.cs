using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core
{
    /// <summary>
    /// Rest definition structure for swagger 2.0 format.
    /// </summary>
    public class RestDefinition
    {
        #region Variables

        public string Name { get; }
        public IReadOnlyList<RestProperty> RestProperties { get; }

        #endregion

        #region Constructor

        public RestDefinition(string name, IReadOnlyList<RestProperty> restProperties)
        {
            Name = name;
            RestProperties = restProperties;
        }

        #endregion

        #region Internal and Private Implementations

        public string ToHtmlString()
        {
            var _jsonData = BuildObjectFromProperties(RestProperties);
            return JsonConvert.SerializeObject(_jsonData, Formatting.Indented);
        }

        private IDictionary<string, object> BuildObjectFromProperties(IReadOnlyList<RestProperty> properties)
        {
            var _result = new ExpandoObject() as IDictionary<string, object>;

            foreach (var _restProperty in properties)
                switch (_restProperty.Type.Value)
                {
                    case "integer":
                        _result.Add(_restProperty.Name, 0);
                        break;
                    case "number":
                        _result.Add(_restProperty.Name, 0d);
                        break;
                    case "boolean":
                        _result.Add(_restProperty.Name, false);
                        break;
                    case "string" when _restProperty.Format == "uuid":
                        _result.Add(_restProperty.Name, Guid.Empty);
                        break;
                    case "string" when _restProperty.Format == "date-time":
                        _result.Add(_restProperty.Name, DateTime.Now.Date);
                        break;
                    case "object":
                        if (_restProperty.RefSchema.Definition != null)
                            _result.Add(_restProperty.Name, BuildObjectFromProperties(_restProperty.RefSchema.Definition.RestProperties));
                        break;
                    default:
                        _result.Add(_restProperty.Name, string.Empty);
                        break;
                }

            return _result;
        }

        #endregion
    }
}