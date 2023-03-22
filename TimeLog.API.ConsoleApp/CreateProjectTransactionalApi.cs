using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using ExecutionStatus = TimeLog.TransactionalAPI.SDK.CRMService.ExecutionStatus;
using Task = TimeLog.TransactionalAPI.SDK.ProjectManagementService.Task;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class CreateProjectTransactionalApi
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectTransactionalApi));

    public static void Consume()
    {
        // For getting the raw XML
        SecurityHandler.Instance.CollectRawRequestResponse = true;
        CRMHandler.Instance.CollectRawRequestResponse = true;
        ProjectManagementHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            // Get a customer
            var customersResult =
                CRMHandler.Instance.CRMClient.GetCustomersByNamePaged(
                    "Timelog",
                    false,
                    false,
                    1,
                    10,
                    CRMHandler.Instance.Token);

            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetCustomersByNamePaged.txt");

            if (customersResult.ResponseState == ExecutionStatus.Success)
            {
                var customer = customersResult.Return.FirstOrDefault();
                if (customer != null)
                {
                    var customerId = customer.ID;
                    var projectGuid = Guid.NewGuid();
                    var taskGuid = Guid.NewGuid();

                    var project = new Project
                    {
                        ID = projectGuid, Name = "My API Test Project", CustomerID = customerId, TypeID = 1,
                        MainContractID = 1
                    };
                    var projectResult =
                        ProjectManagementHandler.Instance.ProjectManagementClient.CreateProject(project,
                            ProjectManagementHandler.Instance.Token);
                    RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateProject.txt");
                    if (projectResult.ResponseState ==
                        TransactionalAPI.SDK.ProjectManagementService.ExecutionStatus.Success)
                    {
                        if (Logger.IsInfoEnabled)
                        {
                            Logger.Info("Project created");
                        }
                        
                        var task = new Task {ID = taskGuid, Name = "First task"};
                        var taskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateTask(
                            task,
                            project!.ProjectID,
                            ProjectManagementHandler.Instance.Token);

                        RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateTask.txt");
                        if (taskResult.ResponseState ==
                            TransactionalAPI.SDK.ProjectManagementService.ExecutionStatus.Success)
                        {
                            if (Logger.IsInfoEnabled)
                            {
                                Logger.Info("Task created");
                            }

                            task = taskResult.Return.FirstOrDefault().Item;
                            var subtask = new Task
                            {
                                ID = Guid.NewGuid(),
                                Name = "First sub-task",
                                ProjectSubContractID = project.MainContractID
                            };
                            taskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateSubTask(
                                subtask, task.TaskID, project.ProjectID, ProjectManagementHandler.Instance.Token);
                            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateSubTask.txt");
                            if (taskResult.ResponseState ==
                                TransactionalAPI.SDK.ProjectManagementService.ExecutionStatus.Success)
                            {
                                if (Logger.IsInfoEnabled)
                                {
                                    Logger.Info("Sub-Task created");
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