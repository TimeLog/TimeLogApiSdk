namespace TimeLog.ApiConsoleApp
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;

    using log4net;

    using TransactionalApi.SDK;
    using TransactionalApi.SDK.ProjectManagementService;

    public class CreateProjectsForCustomersInSql
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectsForCustomersInSql));

        public static void Consume()
        {
            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Successfully authenticated on transactional API");
                }

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

                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["CreateProjectForCustomersInSql.ConnectionString"]))
                {
                    using (var command = connection.CreateCommand())
                    {
                        //// Column setup:
                        //// ProjectTemplateName
                        //// ProjectName
                        //// ProjectNo
                        //// CustomerName
                        //// LegalEntityGuid
                        //// CurrencyListGuid
                        //// ProjectManagerInitials
                        //// AccountManagerInitials
                        //// HandledByDepartmentNo
                        //// OrderedByDepartmentNo
                        //// useProjectNumberSeries
                        //// useTasksAndMileStonesFromTemplate
                        //// useResourceGroupFromTemplate
                        //// useAllocationsFromTemplate	
                        //// usePaymentPlanFromTemplate
                        command.CommandText = @"SELECT * FROM OldTable";

                        connection.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var projectName = reader.GetString(1);
                            var projectNo = reader.GetString(2);

                            Logger.Info("Creating project " + projectNo + " - " + projectName);

                            if (existingProjects.Return.Any(p => p.Name == projectName))
                            {
                                Logger.Info("Skipping. Already exists");
                            }
                            else
                            {
                                var result = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProjectFromTemplate(
                                    reader.GetString(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetString(3),
                                    reader.GetGuid(4),
                                    reader.GetGuid(5),
                                    reader.GetString(6),
                                    reader.GetString(6),
                                    reader.GetString(8),
                                    reader.GetString(8),
                                    reader.GetInt32(10) == 1,
                                    reader.GetInt32(11) == 1,
                                    reader.GetInt32(12) == 1,
                                    reader.GetInt32(13) == 1,
                                    reader.GetInt32(14) == 1,
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
                        }
                    }
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
