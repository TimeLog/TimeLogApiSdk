using System.Dynamic;
using Newtonsoft.Json;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

/// <summary>
///     Rest definition structure for swagger 2.0 format.
/// </summary>
public class RestDefinition
{
    #region Constructor

    public RestDefinition(string name, IReadOnlyList<RestProperty> restProperties)
    {
        Name = name;
        RestProperties = restProperties;
    }

    #endregion

    #region Variables

    public string Name { get; }

    public IReadOnlyList<RestProperty> RestProperties { get; }

    #endregion

    #region Internal and Private Implementations

    public string ToHtmlString()
    {
        var jsonData = BuildObjectFromProperties(RestProperties);

        return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
    }

    private IDictionary<string, object> BuildObjectFromProperties(IReadOnlyList<RestProperty> properties)
    {
        var result = new ExpandoObject() as IDictionary<string, object>;

        foreach (var restProperty in properties)
        {
            switch (restProperty.Type.Value)
            {
                case "integer":
                    result.Add(restProperty.Name, 0);
                    break;
                case "number":
                    result.Add(restProperty.Name, 0d);
                    break;
                case "boolean":
                    result.Add(restProperty.Name, false);
                    break;
                case "string" when restProperty.Format == "uuid":
                    result.Add(restProperty.Name, Guid.Empty);
                    break;
                case "string" when restProperty.Format == "date-time":
                    result.Add(restProperty.Name, DateTime.Now.Date);
                    break;
                case "object":
                    if (restProperty.RefSchema.Definition != null)
                    {
                        result.Add(restProperty.Name,
                            BuildObjectFromProperties(restProperty.RefSchema.Definition.RestProperties));
                    }

                    break;
                default:
                    result.Add(restProperty.Name, string.Empty);
                    break;
            }
        }

        return result;
    }

    #endregion
}