namespace TimeLog.ApiConsoleApp
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;
    using ExecutionStatus = TimeLog.TransactionalApi.SDK.ProjectManagementService.ExecutionStatus;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class CreateProjectTransactionalApi2
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectTransactionalApi2));

        public static void Consume()
        {
            // For getting the raw XML
            SecurityHandler.Instance.CollectRawRequestResponse = true;
            CrmHandler.Instance.CollectRawRequestResponse = true;
            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;

            IEnumerable<string> _messages;
            if (SecurityHandler.Instance.TryAuthenticate(out _messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _newProject = new Project
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
                                              1, // TimeMaterial = 1, FixedPrice = 2, TimeMaterialAccountEndBalancing = 3, TimeMaterialAccountPeriodicBalancing = 4, PrepaidServices = 5, RevenueReqPerTask = 6, ContinuousService = 7, ContinuousItemInvoicing = 8
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

                var _createProjectResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProject(_newProject, ProjectManagementHandler.Instance.Token);

                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateProject.txt");
                if (_createProjectResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _createProjectResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _project = _createProjectResult.Return.FirstOrDefault();
                if (_project == null || _project.Status != ExecutionStatus.Success)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No project created");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Project created (ID: {0})", _project.Item.ProjectID);
                }

                var _task1 = new Task
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
                    State = new TaskState { IsActive = true },
                    TaskID = -1,
                    TaskTypeID = 328
                };

                var _createTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateTask(_task1, _project.Item.ProjectID, ProjectManagementHandler.Instance.Token);
                if (_createTaskResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _createTaskResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _task = _createTaskResult.Return.FirstOrDefault();
                if (_task == null || _task.Status != ExecutionStatus.Success)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task created");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task created (ID: {0})", _task.Item.TaskID);
                }

                var _task2 = new Task
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
                    State = new TaskState { IsActive = true },
                    TaskID = -1,
                    TaskTypeID = 328
                };

                var _createTask2Result = ProjectManagementHandler.Instance.ProjectManagementClient.CreateSubTask(_task2, _task.Item.TaskID, _task.Item.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);
                if (_createTask2Result.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _createTask2Result.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _task2Return = _createTask2Result.Return.FirstOrDefault();
                if (_task2Return == null || _task.Status != ExecutionStatus.Success)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task created");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task created (ID: {0})", _task2Return.Item.TaskID);
                }


            }
            else
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("Failed to authenticate to transactional API");
                    Logger.Warn(string.Join(",", _messages));
                }
            }
        }
    }
}