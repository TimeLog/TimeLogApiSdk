using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class RestManager : IRestManager
    {
        private readonly RestDocumentationHelper _restDocHelper;

        public RestManager(IWebHostEnvironment webHostEnvironment)
        {
            var _filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.REST.API.json");
            _restDocHelper = new RestDocumentationHelper(_filePath);
        }

        public IEnumerable<RestDefinition> GetDefinitions()
        {
            return _restDocHelper.Definitions;
        }

        public RestMethodDoc GetMethod(string id)
        {
            return _restDocHelper.Methods.First(x => x.OperationId.UrlEncode() == id);
        }

        public RestTypeDoc GetService(string id)
        {
            return _restDocHelper.Types.First(x => x.Name.UrlEncode() == id);
        }

        public IEnumerable<RestTypeDoc> GetServices()
        {
            return _restDocHelper.GetTypes().OrderBy(x => x.Name);
        }
    }
}