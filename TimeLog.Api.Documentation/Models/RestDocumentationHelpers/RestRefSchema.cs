using System.Linq;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core
{
    public class RestRefSchema
    {
        #region Variables

        public string Value { get; }

        public RestDefinition Definition
        {
            get
            {
                return RestManager.Instance.GetDefinitions()
                    .FirstOrDefault(x => x.Name == Value);
            }
        }

        #endregion

        #region Constructor

        public RestRefSchema(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Value = url.Replace("#/definitions/", "");
            }
        }

        #endregion
    }
}