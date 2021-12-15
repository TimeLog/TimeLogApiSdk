namespace TimeLog.Api.Documentation.Models;

public class ReportingManager : IReportingManager
{
    private readonly DocumentationHelper _helper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ReportingManager(IWebHostEnvironment webHostEnvironment)
    {
        this._webHostEnvironment = webHostEnvironment;

        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.WebAppCode.xml");
        _helper = new DocumentationHelper(filePath);
    }

    public IEnumerable<MethodDoc> GetMethods()
    {
        var doc = _helper.Types.FirstOrDefault(t => t.FullName == "TimeLog.TLP.WebAppCode.Service");
        if (doc != null)
        {
            return doc.Methods.OrderBy(m => m.FullName);
        }

        return new List<MethodDoc>();
    }

    public MethodDoc GetMethod(string methodFullName)
    {
        var result = _helper.Methods.First(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
        result.InitializeReportingExampleAndSchema(_webHostEnvironment.WebRootPath);
        return result;
    }
}