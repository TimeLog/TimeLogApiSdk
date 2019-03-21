namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    public class RestParameter
    {
        #region Variables

        public string Name { get; }
        public string Description { get; }
        public string Type { get; }
        public string Format { get; }
        public string Default { get; }
        public RestRefSchema SchemaRestRef { get; }

        #endregion

        #region Constructor

        public RestParameter(string name, string description, string type, string format, string @default, RestRefSchema schemaRestRef)
        {
            Name = name;
            Description = description;
            Type = type ?? "object";
            Format = format;
            Default = @default;
            SchemaRestRef = schemaRestRef;
        }

        #endregion
    }
}