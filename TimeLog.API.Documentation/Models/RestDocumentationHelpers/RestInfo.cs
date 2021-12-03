namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers;

public class RestInfo
{
    #region Constructor

    public RestInfo(string version, string title, string description)
    {
        Version = version;
        Title = title;
        Description = description;
    }

    #endregion

    #region Variables

    public string Version { get; }

    public string Title { get; }

    public string Description { get; }

    #endregion
}