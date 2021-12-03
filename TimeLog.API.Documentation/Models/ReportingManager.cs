using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TimeLog.Api.Documentation.Models;

public class ReportingManager : IReportingManager
{
    private readonly DocumentationHelper helper;
    private readonly IWebHostEnvironment webHostEnvironment;

    public ReportingManager(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;

        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.WebAppCode.xml");
        helper = new DocumentationHelper(filePath);
    }

    public IEnumerable<MethodDoc> GetMethods()
    {
        var doc = helper.Types.FirstOrDefault(t => t.FullName == "TimeLog.TLP.WebAppCode.Service");
        if (doc != null)
        {
            return doc.Methods.OrderBy(m => m.FullName);
        }

        return new List<MethodDoc>();
    }

    public MethodDoc GetMethod(string methodFullName)
    {
        var result = helper.Methods.First(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        result.InitializeReportingExampleAndSchema(webHostEnvironment.WebRootPath);
        return result;
    }
}