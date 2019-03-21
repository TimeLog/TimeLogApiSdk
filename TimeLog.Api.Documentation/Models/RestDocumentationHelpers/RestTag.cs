namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    public class RestTag
    {
        #region Variables

        public string Name { get; }
        public string Description { get; }

        #endregion

        #region Constructor

        public RestTag(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion
    }
}