using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using Task = TimeLog.TransactionalAPI.SDK.ProjectManagementService.Task;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class CreateProjectTransactionalApi2
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectTransactionalApi2));

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

            var newProject = new Project
            {
                ID = Guid.NewGuid(),
                AccountManagerID = 523,
                ProjectManagerID = 523,
                TypeID = 249,
                StartDate = DateTime.Today,
                StageID = 0,
                Name = "TimeLog test implementering",
                CustomerID = 667, //Original value 709 does not exist
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
                DepartmentHandledByID = 0,
                DepartmentOrderedByID = 0,
                //Description = string.Empty,
                EndDate = DateTime.Today.AddDays(30),
                No = "MIROEBS",
                LegalEntityID = 0,
                PriceGroupID = 0,
                PurchaseOrderNumber = "42",
                IsInternalProject = true
            };

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

            project.Item.Name = "TimeLog test implementering updated";
            newProject.BudgetAmountExpenses = 100;

            var updateProjectResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.UpdateProject(newProject,
                    ProjectManagementHandler.Instance.Token);
            if (updateProjectResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in updateProjectResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            project = updateProjectResult.Return.FirstOrDefault();
            if (project == null || project.Status != ExecutionStatus.Success)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No project updated");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Project updated (ID: {0})", project.Item.ProjectID);
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

            task1.ExternalKeys = new[] {new ExternalSystemContext {SystemName = "OEBS", ExternalID = "9988"}};
            task1.IsExternalKeysLoaded = true;

            var createTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateTask(task1,
                project.Item.ProjectID, ProjectManagementHandler.Instance.Token);
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

            var task2 = new Task
            {
                Action = DataAction.Created,
                AdditionalTextIsRequired = false,
                BudgetAmount = 100000,
                BudgetHours = 100,
                Description = "Test subtask description",
                EndDate = DateTime.Now.AddMonths(6),
                ID = Guid.NewGuid(),
                Link = "https://login.timelog.com",
                Name = "Test task",
                No = "TASK002",
                StartDate = DateTime.Now,
                ProjectSubContractID = 0, // 0 = Inherit from parent task
                State = new TaskState {IsActive = true},
                TaskID = -1,
                TaskTypeID = 328
            };

            var createTask2Result = ProjectManagementHandler.Instance.ProjectManagementClient.CreateSubTask(task2,
                task.Item.TaskID, task.Item.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);
            if (createTask2Result.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in createTask2Result.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var task2Return = createTask2Result.Return.FirstOrDefault();
            if (task2Return == null || task2Return.Status != ExecutionStatus.Success)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task created");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task created (ID: {0})", task2Return.Item.TaskID);
            }

            var getTaskByIdResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskById(task.Item.ID,
                    ProjectManagementHandler.Instance.Token);
            if (getTaskByIdResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in createTask2Result.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            foreach (var taskItem in getTaskByIdResult.Return)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("{0} > {1}", taskItem.Details.WBS, taskItem.Name);
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