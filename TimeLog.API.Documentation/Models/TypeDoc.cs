using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TimeLog.Api.Documentation.Models;

public class TypeDoc
{
    public TypeDoc(XElement element)
    {
        if (element == null)
        {
            throw new NullReferenceException("Parameter \"element\" cannot be null");
        }

        var fields = new List<FieldDoc>();
        var properties = new List<FieldDoc>();
        var methods = new List<MethodDoc>();

        var attribute = element.Attribute("name")?.Value ?? string.Empty;
        FullName = attribute.Replace("T:", string.Empty);
        Name = attribute.Substring(attribute.LastIndexOf('.') + 1,
            attribute.Length - attribute.LastIndexOf('.') - 1);

        Summary = element.Element("summary")?.Value ?? string.Empty;
        Remarks = element.Element("remarks")?.Value ?? string.Empty;

        if (element.Document != null)
        {
            foreach (var member in element.Document.Descendants("member"))
            {
                var name = member.Attribute("name")?.Value ?? string.Empty;
                if (name.StartsWith("M:" + FullName + "."))
                {
                    methods.Add(new MethodDoc(this, member));
                }
                else if (name.StartsWith("F:" + FullName + "."))
                {
                    fields.Add(new FieldDoc(member));
                }
            }
        }

        Fields = fields;
        Methods = methods;
        Properties = properties;
    }

    public string Name { get; }

    public string FullName { get; }

    public string Summary { get; }

    public string Remarks { get; }

    public IEnumerable<FieldDoc> Fields { get; }

    public IEnumerable<FieldDoc> Properties { get; }

    public IEnumerable<MethodDoc> Methods { get; }
}