namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

/// <summary>
///     Rest property structure for swagger 2.0 format.
/// </summary>
public class RestProperty
{
    #region Constructor

    public RestProperty(string name, string format, string type, RestRefSchema refSchema, string description)
    {
        Name = name;
        Format = format;
        Type = new RestType(type);
        RefSchema = refSchema;
        Description = description;
    }

    #endregion

    #region Variables

    public string Name { get; }
    public string Format { get; }
    public RestType Type { get; }
    public RestRefSchema RefSchema { get; }
    public string Description { get; }

    #endregion
}