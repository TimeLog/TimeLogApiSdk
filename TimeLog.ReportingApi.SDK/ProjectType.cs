using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

public class ProjectType
{
    public ProjectType()
    {
        Id = -1;
        Name = string.Empty;
    }

    public ProjectType(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Id = int.Parse(node.Attributes["ID"].InnerText);
        Name = node.GetStringSafe("tlp:Name", namespaceManager);
    }

    public static int All => 0;

    public int Id { get; set; }

    public string Name { get; set; }
}