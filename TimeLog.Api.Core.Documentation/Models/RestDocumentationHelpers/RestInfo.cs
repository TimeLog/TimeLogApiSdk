namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers
{
    public class RestInfo
    {
        #region Variables

        public string Version { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        #endregion

        #region Constructor

        public RestInfo(string version, string title, string description)
        {
            Version = version;
            Title = title;
            Description = description;
        }

        #endregion
    }
}