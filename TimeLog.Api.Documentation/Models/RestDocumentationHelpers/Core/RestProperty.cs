namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    /// <summary>
    /// Rest property structure for swagger 2.0 format.
    /// </summary>
    public class RestProperty
    {
        #region Variables

        public string Name { get; }
        public string Format { get; }
        public string Type { get; }
        public RestRefSchema RefSchema { get; }

        #endregion

        #region Constructor

        public RestProperty(string name, string format, string type, RestRefSchema refSchema)
        {
            Name = name;
            Format = format;
            Type = type;
            RefSchema = refSchema;
        }

        #endregion
    }
}