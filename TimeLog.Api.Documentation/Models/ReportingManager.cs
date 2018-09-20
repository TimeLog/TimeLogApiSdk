using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeLog.Api.Documentation.Models
{
    using System.Web.Caching;

    public class ReportingManager
    {
        private readonly DocumentationHelper _helper;

        private ReportingManager(string filePath)
        {
            this._helper = new DocumentationHelper(filePath);
        }

        public static ReportingManager Instance
        {
            get
            {
                ReportingManager _instance = (ReportingManager)HttpContext.Current.Cache["ReportingManager"];
                if (_instance == null)
                {
                    string _filePath = HttpContext.Current.Server.MapPath("~/Source/") + "TimeLog.TLP.WebAppCode.XML";
                    _instance = new ReportingManager(_filePath);
                    HttpContext.Current.Cache.Add("ReportingManager", _instance, new CacheDependency(_filePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
                }

                return _instance;
            }
        }

        public IEnumerable<MethodDoc> GetMethods()
        {
            var _doc = this._helper.Types.FirstOrDefault(t => t.FullName == "TimeLog.TLP.WebAppCode.Service");
            if (_doc != null)
            {
                return _doc.Methods.OrderBy(m => m.FullName);
            }

            return new List<MethodDoc>();
        }

        public MethodDoc GetMethod(string methodFullName)
        {
            return this._helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        }
    }
}