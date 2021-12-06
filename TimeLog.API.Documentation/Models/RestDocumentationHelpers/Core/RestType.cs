namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

public class RestType
{
    #region Constructor

    public RestType(string value)
    {
        Value = value ?? "object";
    }

    #endregion

    #region Variables

    public string Value { get; }

    #endregion
}