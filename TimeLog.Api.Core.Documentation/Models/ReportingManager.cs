using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class ReportingManager : IReportingManager
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DocumentationHelper _helper;

        public ReportingManager(IWebHostEnvironment environment)
        {
            _environment = environment;
            var _filePath = Path.Combine(environment.ContentRootPath, "Source/TimeLog.TLP.WebAppCode.XML");
            _helper = new DocumentationHelper(_filePath);
        }

        
        
        public string GetReportingExample(MethodDoc doc)
        {
            var _filePath = Path.Combine(_environment.ContentRootPath, "Source/Reporting/" + doc.Name + ".xml");
            FileInfo _fileInfo = new FileInfo(_filePath);
            return _fileInfo.Exists ? File.ReadAllText(_filePath) : string.Empty;
        }
        
        public string GetReportingSchema(MethodDoc doc)
        {
            var _filePath = Path.Combine(_environment.ContentRootPath, "Source/Reporting/" + doc.Name + ".xsd");
            FileInfo _fileInfo = new FileInfo(_filePath);
            return _fileInfo.Exists ? File.ReadAllText(_filePath) : string.Empty;
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
            return _helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        }
    }
}