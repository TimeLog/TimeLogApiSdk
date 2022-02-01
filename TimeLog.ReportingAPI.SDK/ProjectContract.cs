using System;
using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

public class ProjectContract
{
    public ProjectContract()
    {
        Id = 0;
        ProjectContractGuid = Guid.Empty;
        Name = string.Empty;
        ContractModelType = 0;
        ProjectID = 0;
        ProjectContractStatus = -1;
        WorkBudgetHours = 0;
        WorkBudgetAmount = 0;
    }

    public ProjectContract(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Id = int.Parse(node.Attributes["ID"].InnerText);
        ProjectContractGuid = Guid.Parse(node.Attributes["GUID"].InnerText);
        Name = node.GetStringSafe("tlp:Name", namespaceManager);
        ContractModelType = node.GetIntSafe("tlp:ContractModelType", namespaceManager);
        ProjectID = node.GetIntSafe("tlp:ProjectID", namespaceManager);
        ProjectContractStatus = node.GetIntSafe("tlp:ProjectContractStatus", namespaceManager);
        WorkBudgetHours = node.GetDoubleSafe("tlp:WorkBudgetHours", namespaceManager);
        WorkBudgetAmount = node.GetDoubleSafe("tlp:WorkBudgetAmount", namespaceManager);
    }

    public int Id { get; set; }
    public Guid ProjectContractGuid { get; set; }
    public string Name { get; set; }
    public int ContractModelType { get; set; }
    public int ProjectID { get; set; }
    public int ProjectContractStatus { get; set; }
    public double WorkBudgetHours { get; set; }
    public double WorkBudgetAmount { get; set; }
}