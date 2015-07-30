using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeLog.Api.Documentation.Models
{
    using System.Web.Caching;

    public class ReportingManager
    {
        private readonly DocumentationHelper helper;

        private ReportingManager(string filePath)
        {
            this.helper = new DocumentationHelper(filePath);
        }

        public static ReportingManager Instance
        {
            get
            {
                ReportingManager instance = (ReportingManager)HttpContext.Current.Cache["ReportingManager"];
                if (instance == null)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/Source/") + "TimeLog.TLP.WebAppCode.XML";
                    instance = new ReportingManager(filePath);
                    HttpContext.Current.Cache.Add("ReportingManager", instance, new CacheDependency(filePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
                }

                return instance;
            }
        }

        public IEnumerable<MethodDoc> GetMethods()
        {
            var doc = this.helper.Types.FirstOrDefault(t => t.FullName == "TimeLog.TLP.WebAppCode.Service");
            if (doc != null)
            {
                return doc.Methods.OrderBy(m => m.FullName);
            }

            return new List<MethodDoc>();
        }

        public MethodDoc GetMethod(string methodFullName)
        {
            return this.helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        }
    }
}