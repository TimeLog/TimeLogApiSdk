using System.Xml.Linq;

namespace TimeLog.Api.Documentation.Models;

public class FieldDoc
{
    public FieldDoc(XElement element)
    {
        Name = string.Empty;
        FullName = string.Empty;
        Summary = string.Empty;
        Example = string.Empty;

        var attribute = element.Attribute("name");
        if (attribute != null)
        {
            FullName = attribute.Value;
            Name = attribute
                .Value; //.Replace("F:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + "Header.", string.Empty);
        }

        var summary = element.Element("summary");
        if (summary != null)
        {
            Summary = summary.Value;
        }
    }

    public string Name { get; set; }

    public string FullName { get; set; }

    public string Summary { get; set; }

    public string Example { get; set; }
}