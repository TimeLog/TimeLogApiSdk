namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.CrmService;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    using ExecutionStatus = TimeLog.TransactionalApi.SDK.ProjectManagementService.ExecutionStatus;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class CreateProjectTransactionalApi
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectTransactionalApi));
        
        public static void Consume()
        {
            // For getting the raw XML
            SecurityHandler.Instance.CollectRawRequestResponse = true;
            CrmHandler.Instance.CollectRawRequestResponse = true;
            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                // Get a customer
                var customersResult = CrmHandler.Instance.CrmClient.GetCustomersByNamePaged("Timelog Kunde", false, false, 1, 10, CrmHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetCustomersByNamePaged.txt");
                if (customersResult.ResponseState == TransactionalApi.SDK.CrmService.ExecutionStatus.Success)
                {
                    var customer = customersResult.Return.FirstOrDefault();
                    if (customer != null)
                    {
                        var customerId = customer.ID;
                        var projectGuid = Guid.NewGuid();
                        var taskGuid = Guid.NewGuid();

                        var project = new Project { ID = projectGuid, Name = "My API Test Project", CustomerID = customerId };
                        var projectResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProject(project, ProjectManagementHandler.Instance.Token);
                        RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateProject.txt");
                        if (projectResult.ResponseState == ExecutionStatus.Success)
                        {
                            if (Logger.IsInfoEnabled)
                            {
                                Logger.Info("Project created");
                            }

                            var projectId = projectResult.Return.FirstOrDefault().Item.ProjectID;
                            var task = new Task() { ID = taskGuid, Name = "First task" };
                            var taskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateTask(task, projectId, ProjectManagementHandler.Instance.Token);
                            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateTask.txt");
                            if (taskResult.ResponseState == ExecutionStatus.Success)
                            {
                                if (Logger.IsInfoEnabled)
                                {
                                    Logger.Info("Task created");
                                }
                            }
                            else
                            {
                                foreach (var apiMessage in taskResult.Messages)
                                {
                                    if (Logger.IsErrorEnabled)
                                    {
                                        Logger.Error(apiMessage.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var apiMessage in projectResult.Messages)
                            {
                                if (Logger.IsErrorEnabled)
                                {
                                    Logger.Error(apiMessage.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Logger.IsWarnEnabled)
                        {
                            Logger.Warn("No customer found");
                        }
                    }
                }
                else
                {
                    foreach (var apiMessage in customersResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(apiMessage.Message);
                        }
                    }
                }
            }
            else
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("Failed to authenticate to transactional API");
                    Logger.Warn(string.Join(",", messages));
                }
            }
        }
    }
}