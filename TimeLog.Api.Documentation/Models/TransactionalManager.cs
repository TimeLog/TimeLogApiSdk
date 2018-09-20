namespace TimeLog.Api.Documentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;

    public class TransactionalManager
    {
        private readonly DocumentationHelper _helper;

        private TransactionalManager(string filePath)
        {
            this._helper = new DocumentationHelper(filePath);
        }

        public static TransactionalManager Instance
        {
            get
            {
                TransactionalManager _instance = (TransactionalManager)HttpContext.Current.Cache["TransactionalManager"];
                if (_instance == null)
                {
                    string _filePath = HttpContext.Current.Server.MapPath("~/Source/") + "TimeLog.TLP.API.XML";
                    _instance = new TransactionalManager(_filePath);
                    HttpContext.Current.Cache.Add("TransactionalManager", _instance, new CacheDependency(_filePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.Normal, null);
                }

                return _instance;
            }
        }

        public IEnumerable<TypeDoc> GetServices()
        {
            return this._helper.GetTypes("^TimeLog\\.TLP\\.API\\..+Service$").OrderBy(t => t.FullName);
        }

        public TypeDoc GetService(string typeFullName)
        {
            return this._helper.Types.FirstOrDefault(t => t.FullName.UrlEncode() == typeFullName);
        }

        public MethodDoc GetMethod(string methodFullName)
        {
            return this._helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        }
    }
}