namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    public class RestType
    {
        #region Variables

        public string Value { get; private set; }

        #endregion

        #region Constructor

        public RestType(string value)
        {
            Value = value ?? "object";
        }

        #endregion
    }
}