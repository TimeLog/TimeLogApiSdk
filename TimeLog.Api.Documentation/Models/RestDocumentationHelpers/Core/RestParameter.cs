namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    /// <summary>
    /// Rest parameter structure for swagger 2.0 format.
    /// </summary>
    public class RestParameter
    {
        #region Variables

        public string Name { get; }
        public string Description { get; }
        public RestType Type { get; }
        public string Format { get; }
        public string Default { get; }
        public RestRefSchema SchemaRestRef { get; }

        #endregion

        #region Constructor

        public RestParameter(string name, string description, string type, string format, string @default, RestRefSchema schemaRestRef)
        {
            Name = name;
            Description = description;
            Type = new RestType(type);
            Format = format;
            Default = @default;
            SchemaRestRef = schemaRestRef;
        }

        #endregion
    }
}