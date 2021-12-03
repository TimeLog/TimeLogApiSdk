using System.Collections.Generic;
using System.Linq;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

/// <summary>
///     Rest ref schema structure for swagger 2.0 format.
/// </summary>
public class RestRefSchema
{
    private readonly IEnumerable<RestDefinition>? definitions;

    #region Constructor

    public RestRefSchema(string url, IEnumerable<RestDefinition>? definitions)
    {
        this.definitions = definitions;

        if (!string.IsNullOrEmpty(url))
        {
            Value = url.Replace("#/definitions/", "");
        }
    }

    #endregion

    public string? Value { get; }

    public RestDefinition? Definition
    {
        get { return definitions?.FirstOrDefault(x => x.Name == Value); }
    }
}