using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using Task = TimeLog.TransactionalAPI.SDK.ProjectManagementService.Task;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class ExternalKeysOnProjectAndTasks
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ExternalKeysOnProjectAndTasks));

    public static void Consume()
    {
        ProjectManagementHandler.Instance.CollectRawRequestResponse = true;
        SecurityHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");

            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var newProject = new Project
            {
                ID = Guid.NewGuid(),
                AccountManagerID = 523,
                ProjectManagerID = 523,
                TypeID = 249,
                StartDate = DateTime.Today,
                StageID = 0,
                Name = "TimeLog test implementering",
                CustomerID = 667,
                Action = DataAction.Created,
                BudgetAmountExpenses = 0,
                BudgetAmountTravelExpenses = 0,
                BudgetWorkAmountFixedPriceProject = 0,
                BudgetWorkAmountFixedPriceTasks = 0,
                BudgetWorkAmountTimeAndMaterial = 0,
                BudgetWorkHoursFixedPriceProject = 0,
                BudgetWorkHoursFixedPriceTasks = 0,
                BudgetWorkHoursTimeAndMaterial = 0,
                CategoryID = 0,
                CurrencyID = 0,
                MainContractID =
                    1, // ONLY FOR CREATION! TimeMaterial = 1, FixedPrice = 2, TimeMaterialAccountEndBalancing = 3, TimeMaterialAccountPeriodicBalancing = 4, PrepaidServices = 5, RevenueReqPerTask = 6, ContinuousService = 7, ContinuousItemInvoicing = 8
                DepartmentHandledByID = 8,
                DepartmentOrderedByID = 0,
                //Description = string.Empty,
                EndDate = DateTime.Today.AddDays(30),
                ExchangeRate = 0,
                No = "MIROEBS",
                LegalEntityID = 0,
                PriceGroupID = 0,
                PurchaseOrderNumber = "42",
                IsInternalProject = true
            };

            newProject.ExternalKeys = new[] {new ExternalSystemContext {SystemName = "Jira", ExternalID = "PROJ1"}};
            newProject.IsExternalKeysLoaded = true;

            var createProjectResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.CreateProject(newProject,
                    ProjectManagementHandler.Instance.Token);

            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateProject.txt");
            if (createProjectResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in createProjectResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var project = createProjectResult.Return.FirstOrDefault();
            if (project == null || project.Status != ExecutionStatus.Success)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No project created");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Project created (ID: {0})", project.Item.ProjectID);
            }

            var getProjectByExternalKeyResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.GetProjectByExternalKey("PROJ1", "Jira",
                    ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetProjectByExternalKey.txt");
            if (getProjectByExternalKeyResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in getProjectByExternalKeyResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var projectByExternalKey = getProjectByExternalKeyResult.Return.FirstOrDefault();
            if (projectByExternalKey == null)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No project fetched");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Project by external key (ID: {0})", projectByExternalKey.ProjectID);
            }

            var task1 = new Task
            {
                Action = DataAction.Created,
                AdditionalTextIsRequired = false,
                BudgetAmount = 100000,
                BudgetHours = 100,
                Description = "Test task description",
                EndDate = DateTime.Now.AddMonths(6),
                ID = Guid.NewGuid(),
                Link = "https://login.timelog.com",
                Name = "Test task",
                No = "TASK001",
                StartDate = DateTime.Now,
                ProjectSubContractID = 0, // 0 = Inherit from project default contract
                State = new TaskState {IsActive = true},
                TaskID = -1,
                TaskTypeID = 328
            };

            task1.ExternalKeys = new[]
            {
                new ExternalSystemContext {SystemName = "Jira", ExternalID = "ENDK-1"},
                new ExternalSystemContext {SystemName = "OEBS", ExternalID = "200542"}
            };
            task1.IsExternalKeysLoaded = true;

            var createTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateTask(task1,
                project.Item.ProjectID, ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateTask.txt");
            if (createTaskResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in createTaskResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var task = createTaskResult.Return.FirstOrDefault();
            if (task == null || task.Status != ExecutionStatus.Success)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task created");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task created (ID: {0})", task.Item.TaskID);
            }

            var getByID = ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByID(task.Item.TaskID,
                true, ProjectManagementHandler.Instance.Token);

            var updateTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(task.Item,
                project.Item.ProjectID, ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\UpdateTask.txt");

            var getTaskByExternalKeyResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByExternalKey("ENDK-1", "Jira",
                    ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByExternalKey.txt");
            if (getTaskByExternalKeyResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in getTaskByExternalKeyResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var taskByExternalKey = getTaskByExternalKeyResult.Return.FirstOrDefault();
            if (taskByExternalKey == null)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task fetched");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task by external key (ID: {0})", taskByExternalKey.TaskID);
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