namespace TimeLog.Api.Documentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;

    public class TransactionalManager
    {
        private readonly DocumentationHelper helper;
        
        private TransactionalManager(string filePath)
        {
            this.helper = new DocumentationHelper(filePath);
        }

        public static TransactionalManager Instance
        {
            get
            {
                TransactionalManager instance = (TransactionalManager)HttpContext.Current.Cache["TransactionalManager"];
                if (instance == null)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/Source/") + "TimeLog.TLP.API.XML";
                    instance = new TransactionalManager(filePath);
                    HttpContext.Current.Cache.Add("TransactionalManager", instance, new CacheDependency(filePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
                }

                return instance;
            }
        }

        public IEnumerable<TypeDoc> GetServices()
        {
            return this.helper.GetTypes("^TimeLog\\.TLP\\.API\\..+Service$").OrderBy(t => t.FullName);
        }

        public TypeDoc GetService(string typeFullName)
        {
            return this.helper.Types.FirstOrDefault(t => t.FullName.UrlEncode() == typeFullName);
        }
    }
}