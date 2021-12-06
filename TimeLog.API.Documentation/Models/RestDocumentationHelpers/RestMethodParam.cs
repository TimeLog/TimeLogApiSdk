using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers;

public class RestMethodParam
{
    #region Constructor

    public RestMethodParam(string name, string description, RestType type, RestRefSchema refSchema)
    {
        Name = name;
        Description = description;
        Type = type;
        RefSchema = refSchema;
    }

    #endregion

    #region Variables

    public string Name { get; }
    public string Description { get; }
    public RestType Type { get; }
    public RestRefSchema RefSchema { get; }

    #endregion
}