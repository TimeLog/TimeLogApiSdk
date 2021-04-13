using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class TransactionalManager
    {
        private readonly DocumentationHelper _helper;

        public TransactionalManager(IWebHostEnvironment environment)
        {
            var _filePath = Path.Combine(environment.ContentRootPath, "Source/TimeLog.TLP.API.XML");
            _helper = new DocumentationHelper(_filePath);
        }

        public IEnumerable<TypeDoc> GetServices()
        {
            return _helper.GetTypes("^TimeLog\\.TLP\\.API\\..+Service$").OrderBy(t => t.FullName);
        }

        public TypeDoc GetService(string typeFullName)
        {
            return _helper.Types.FirstOrDefault(t => t.FullName.UrlEncode() == typeFullName);
        }

        public MethodDoc GetMethod(string methodFullName)
        {
            return _helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        }
    }
}