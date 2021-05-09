using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class ReportingManager : IReportingManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DocumentationHelper _helper;

        public ReportingManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            
            var _filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.WebAppCode.xml");
            _helper = new DocumentationHelper(_filePath);
        }

        public IEnumerable<MethodDoc> GetMethods()
        {
            var _doc = _helper.Types.FirstOrDefault(t => t.FullName == "TimeLog.TLP.WebAppCode.Service");
            if (_doc != null)
            {
                return _doc.Methods.OrderBy(m => m.FullName);
            }

            return new List<MethodDoc>();
        }

        public MethodDoc GetMethod(string methodFullName)
        {
            var _result = _helper.Methods.First(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
            _result.InitializeReportingExampleAndSchema(_webHostEnvironment.WebRootPath);
            return _result;
        }
    }
}