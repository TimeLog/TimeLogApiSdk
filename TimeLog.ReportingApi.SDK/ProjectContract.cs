using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeLog.ReportingApi.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectContract
    {
        public ProjectContract()
        {
            this.Id = 0;
            this.ProjectContractGuid = Guid.Empty;
            this.Name=String.Empty;
            this.ContractModelType = 0;
            this.ProjectID = 0;
            this.ProjectContractStatus = -1;
            this.WorkBudgetHours = 0;
            this.WorkBudgetAmount = 0;
        }

        public ProjectContract(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.ProjectContractGuid = Guid.Parse(node.Attributes["GUID"].InnerText);
            this.Name = node.GetStringSafe("tlp:Name",namespaceManager);
            this.ContractModelType = node.GetIntSafe("tlp:ContractModelType", namespaceManager);
            this.ProjectID = node.GetIntSafe("tlp:ProjectID", namespaceManager);
            this.ProjectContractStatus = node.GetIntSafe("tlp:ProjectContractStatus", namespaceManager);
            this.WorkBudgetHours = node.GetDoubleSafe("tlp:WorkBudgetHours", namespaceManager);
            this.WorkBudgetAmount = node.GetDoubleSafe("tlp:WorkBudgetAmount", namespaceManager);
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
}
