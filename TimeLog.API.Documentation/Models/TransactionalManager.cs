using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TimeLog.Api.Documentation.Models;

public class TransactionalManager : ITransactionalManager
{
    private readonly DocumentationHelper helper;

    public TransactionalManager(IWebHostEnvironment webHostEnvironment)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Source/TimeLog.TLP.API.XML");
        helper = new DocumentationHelper(filePath);
    }

    public IEnumerable<TypeDoc> GetServices()
    {
        return helper.GetTypes("^TimeLog\\.TLP\\.API\\..+Service$").OrderBy(t => t.FullName);
    }

    public TypeDoc? GetService(string typeFullName)
    {
        return helper.Types.FirstOrDefault(t => t.FullName.UrlEncode() == typeFullName);
    }

    public MethodDoc? GetMethod(string methodFullName)
    {
        return helper.Methods.FirstOrDefault(m => m.FullyQuantifiedName.UrlEncode() == methodFullName);
    }
}