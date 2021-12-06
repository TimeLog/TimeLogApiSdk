using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TimeLog.Api.Documentation.Models;

public class MethodDoc
{
    public MethodDoc()
    {
        Name = string.Empty;
        Summary = string.Empty;
        Params = new List<MethodParam>();
        Example = string.Empty;
        Exceptions = new List<MethodException>();
        Permission = string.Empty;
        Returns = string.Empty;
        SeeAlso = new List<TypeDoc>();

        FullyQuantifiedName = string.Empty;
        FullName = string.Empty;
        Remarks = string.Empty;

        OutputExample = string.Empty;
        OutputSchema = string.Empty;
    }

    public MethodDoc(TypeDoc parent, XElement element)
        : this()
    {
        Parent = parent;

        var attribute = element.Attribute("name")?.Value ?? string.Empty;
        FullName = attribute.Replace("M:", string.Empty);

        var firstParenthesis = attribute.IndexOf('(');
        if (firstParenthesis < 0)
        {
            firstParenthesis = attribute.Length - 1;
        }

        var lastDot = attribute.LastIndexOf('.', firstParenthesis);
        Name = attribute.Substring(lastDot + 1, firstParenthesis - lastDot - 1);
        FullyQuantifiedName = attribute.Substring(0, firstParenthesis).Replace("M:", string.Empty);

        IsConstructor = Name.StartsWith("#");

        var xmlSummary = element.Descendants("summary").FirstOrDefault();
        if (xmlSummary != null)
        {
            Summary = xmlSummary.Value;
        }

        var xmlRemarks = element.Descendants("remarks").FirstOrDefault();
        if (xmlRemarks != null)
        {
            Remarks = xmlRemarks.Value;
        }

        var xmlExample = element.Descendants("example").FirstOrDefault();
        if (xmlExample != null)
        {
            Example = xmlExample.Value;
        }

        var xmlPermission = element.Descendants("permission").FirstOrDefault();
        if (xmlPermission != null)
        {
            Permission = xmlPermission.Value;
        }

        var xmlReturns = element.Descendants("returns").FirstOrDefault();
        if (xmlReturns != null)
        {
            Returns = xmlReturns.Value;
        }

        //foreach (var seealso in element.Descendants("seealso"))
        //{
        //    var crefAttribute = seealso.Attribute("cref");
        //    if (crefAttribute != null)
        //    {
        //        this.SeeAlso.Add(crefAttribute.Value.Contains("Proxy") ? DocumentationHelper.ApiProxyInstance.GetTypeDocumentation(crefAttribute.Value) : DocumentationHelper.InterfaceInstance.GetTypeDocumentation(crefAttribute.Value));
        //    }
        //}

        //foreach (var seealso in element.Descendants("see"))
        //{
        //    var crefAttribute = seealso.Attribute("cref");
        //    if (crefAttribute != null)
        //    {
        //        this.SeeAlso.Add(crefAttribute.Value.Contains("Proxy") ? DocumentationHelper.ApiProxyInstance.GetTypeDocumentation(crefAttribute.Value) : DocumentationHelper.InterfaceInstance.GetTypeDocumentation(crefAttribute.Value));
        //    }
        //}

        firstParenthesis = FullName.IndexOf('(');
        var parameterTypeList = firstParenthesis > 0
            ? FullName.Substring(
                firstParenthesis + 1,
                FullName.LastIndexOf(")", StringComparison.Ordinal) - firstParenthesis - 1).Split(',')
            : new string[] { };
        var parameterIndex = 0;

        foreach (var param in element.Descendants("param"))
        {
            var nameAttribute = param.Attribute("name");
            if (nameAttribute != null)
            {
                Params.Add(new MethodParam(nameAttribute.Value, param.Value, parameterTypeList[parameterIndex]));
            }

            parameterIndex++;
        }

        foreach (var exc in element.Descendants("exception"))
        {
            var crefAttribute = exc.Attribute("cref");
            if (crefAttribute != null)
            {
                Exceptions.Add(new MethodException(crefAttribute.Value, exc.Value));
            }
        }
    }

    public string Name { get; }

    public string FullyQuantifiedName { get; }

    public string FullName { get; }

    public string Summary { get; }

    public string Example { get; }

    public string Permission { get; }

    public string Returns { get; }

    public string Remarks { get; }

    public bool IsConstructor { get; }

    public TypeDoc? Parent { get; }

    public IList<TypeDoc> SeeAlso { get; }

    public IList<MethodParam> Params { get; }

    public IList<MethodException> Exceptions { get; }

    public string OutputExample { get; private set; }

    public string OutputSchema { get; private set; }

    public void InitializeReportingExampleAndSchema(string webRootPath)
    {
        FileInfo outputExampleInfo = new(Path.Combine(webRootPath, "Reporting/" + Name + ".xml"));
        if (outputExampleInfo.Exists)
        {
            OutputExample = File.ReadAllText(outputExampleInfo.FullName);
        }

        FileInfo fileInfo = new(Path.Combine(webRootPath, "Reporting/" + Name + ".xsd"));
        if (fileInfo.Exists)
        {
            OutputSchema = File.ReadAllText(fileInfo.FullName);
        }
    }
}