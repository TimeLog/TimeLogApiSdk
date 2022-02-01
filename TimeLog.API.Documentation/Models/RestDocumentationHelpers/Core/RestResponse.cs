namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

/// <summary>
///     Rest response structure for swagger 2.0 format.
/// </summary>
public class RestResponse
{
    #region Constructor

    public RestResponse(string code, string description, RestRefSchema schemaRestRef)
    {
        Code = code;
        Description = description;
        SchemaRestRef = schemaRestRef;
    }

    #endregion

    #region Variables

    public string Code { get; }

    public string Description { get; }

    public RestRefSchema SchemaRestRef { get; }

    #endregion
}