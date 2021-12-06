using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

public class TaskType
{
    public TaskType()
    {
        Id = -1;
        Name = string.Empty;
    }

    public TaskType(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Id = int.Parse(node.Attributes["ID"].InnerText);
        Name = node.GetStringSafe("tlp:Name", namespaceManager);
    }

    public static int All => 0;

    public int Id { get; set; }

    public string Name { get; set; }
}