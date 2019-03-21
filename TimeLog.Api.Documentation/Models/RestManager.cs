using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models
{
    public class RestManager
    {
        #region Variables

        private readonly RestDocumentationHelper _restDocHelper;

        public static RestManager Instance
        {
            get
            {
                var _instance = (RestManager) HttpContext.Current.Cache["RestManager"];
                if (_instance == null)
                {
                    var _filePath = HttpContext.Current.Server.MapPath("~/Source/") + "TimeLog.REST.API.json";
                    _instance = new RestManager(_filePath);
                    HttpContext.Current.Cache.Add("RestManager", _instance, new CacheDependency(_filePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
                }

                return _instance;
            }
        }

        #endregion

        #region Constructor

        private RestManager(string jsonFilePath)
        {
            _restDocHelper = new RestDocumentationHelper(jsonFilePath);
        }

        #endregion

        #region Internal and Private Implementations

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

        #endregion
    }
}