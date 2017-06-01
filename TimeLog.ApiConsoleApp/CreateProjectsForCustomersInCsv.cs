using TimeLog.Library.Data;

namespace TimeLog.ApiConsoleApp
{
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using TransactionalApi.SDK;
    using TransactionalApi.SDK.ProjectManagementService;

    public class CreateProjectsForCustomersInCsv
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectsForCustomersInCsv));

        public static void Consume()
        {
            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Successfully authenticated on transactional API");
                }

                //// CSV Column setup:
                //// ProjectTemplateName
                //// ProjectName
                //// ProjectNo
                //// CustomerName
                //// PriceListID
                //// PriceGroupID
                //// ProjectManagerInitials
                //// AccountManagerInitials
                //// HandledByDepartmentNo
                //// OrderedByDepartmentNo
                //// useProjectNumberSeries
                //// useTasksAndMileStonesFromTemplate
                //// useResourceGroupFromTemplate
                //// useAllocationsFromTemplate	
                //// usePaymentPlanFromTemplate
                //// 
                //// E.g.
                //// Fixed Fee
                //// TLP - Initial project
                //// 2016P239
                //// TimeLog A/S
                //// 4A6BE668-6BC6-E511-80DC-005056B220E2
                //// 4B6BE668-6BC6-E511-80DC-005056B220E2
                //// JKM
                //// 
                //// 2
                //// 
                //// 0
                //// 1
                //// 0
                //// 0
                //// 1
                //// 0
                
                // Get list of existing projects to avoid duplicates
                var existingProjects = ProjectManagementHandler.Instance.ProjectManagementClient.GetProjects(-1, -1, false, -1, ProjectManagementHandler.Instance.Token);
                if (existingProjects.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var apiMessage in existingProjects.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(apiMessage.Message);
                        }
                    }

                    return;
                }

                var csv = new CsvReader("c:\\temp\\fischer.csv", ',', true);
                if (Logger.IsInfoEnabled)
                {
                    Logger.InfoFormat("Fetched {0} customers", csv.Lines.Length);
                }

                while (csv.Read())
                {
                    var projectName = csv.GetString(1);
                    var projectNo = csv.GetString(2);

                    Logger.Info("Creating project " + projectNo + " - " + projectName);

                    if (existingProjects.Return.Any(p => p.No == projectNo))
                    {
                        Logger.Info("Skipping. Already exists");
                    }
                    else
                    {
                        var result = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProjectFromTemplate(
                            csv.GetString(0),
                            csv.GetString(1),
                            csv.GetString(2),
                            csv.GetString(3),
                            csv.GetGuid(4),
                            csv.GetGuid(5),
                            csv.GetString(6),
                            csv.GetString(7),
                            csv.GetString(8),
                            csv.GetString(8),
                            csv.GetBoolean(10),
                            csv.GetBoolean(11),
                            csv.GetBoolean(12),
                            csv.GetBoolean(13),
                            csv.GetBoolean(14),
                            99,
                            ProjectManagementHandler.Instance.Token);
                        if (result.ResponseState == ExecutionStatus.Success)
                        {
                            foreach (var project in result.Return)
                            {
                                if (Logger.IsDebugEnabled)
                                {
                                    Logger.DebugFormat("Project created with ID {0}", project.Item.ProjectID);
                                }
                            }
                        }
                        else
                        {
                            foreach (var apiMessage in result.Messages)
                            {
                                if (Logger.IsErrorEnabled)
                                {
                                    Logger.Error(apiMessage.Message);
                                }
                            }
                        }
                    }

                    //break;
                }
            }
            else
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("Failed to authenticate to reporting API");
                }
            }
        }
    }
}
