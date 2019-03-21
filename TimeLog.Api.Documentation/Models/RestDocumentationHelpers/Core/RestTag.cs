namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    /// <summary>
    /// Rest tag structure for swagger 2.0 format.
    /// </summary>
    public class RestTag
    {
        #region Variables

        public string Name { get; }

        public string Description { get; }

        public string ControllerName
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return string.Empty;

                return Name
                    .Substring(Name.IndexOf("[") + 1)
                    .Replace("]", "");
            }
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName => ControllerName.Replace("Controller", "");

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