using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class TransactionalManager : ITransactionalManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DocumentationHelper _helper;

        public TransactionalManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            var _filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.API.XML");
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