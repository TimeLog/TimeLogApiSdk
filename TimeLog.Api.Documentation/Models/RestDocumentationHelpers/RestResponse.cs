namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    public class RestResponse
    {
        #region Variables

        public string Code { get; }
        public string Description { get; }
        public RestRefSchema SchemaRestRef { get; }

        #endregion

        #region Constructor

        public RestResponse(string code, string description, RestRefSchema schemaRestRef)
        {
            Code = code;
            Description = description;
            SchemaRestRef = schemaRestRef;
        }

        #endregion
    }
}