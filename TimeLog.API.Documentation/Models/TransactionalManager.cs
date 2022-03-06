namespace TimeLog.Api.Documentation.Models;

public class TransactionalManager : ITransactionalManager
{
    private readonly DocumentationHelper _helper;

    public TransactionalManager(IWebHostEnvironment webHostEnvironment)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.API.XML");
        _helper = new DocumentationHelper(filePath);
    }

    public IEnumerable<TypeDoc> GetServices()
    {
        return _helper.GetTypes("^TimeLog\\.TLP\\.API\\..+Service$").OrderBy(t => t.FullName);
    }

    public TypeDoc? GetService(string typeFullName)
    {
        return _helper.Types.FirstOrDefault(t => t.FullName.UrlEncode().ToLowerInvariant() == typeFullName.ToLowerInvariant());
    }

    public MethodDoc? GetMethod(string methodFullName)
    {
        return _helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode().ToLowerInvariant() == methodFullName.ToLowerInvariant());
    }
}