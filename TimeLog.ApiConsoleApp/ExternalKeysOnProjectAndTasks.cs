namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class ExternalKeysOnProjectAndTasks
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ExternalKeysOnProjectAndTasks));
        
        public static void Consume()
        {
            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;
            SecurityHandler.Instance.CollectRawRequestResponse = true;

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
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

                _newProject.ExternalKeys = new[] { new ExternalSystemContext { SystemName = "Jira", ExternalID = "PROJ1" } };
                _newProject.IsExternalKeysLoaded = true;
                
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

                var _getProjectByExternalKeyResult = ProjectManagementHandler.Instance.ProjectManagementClient.GetProjectByExternalKey("PROJ1", "Jira", ProjectManagementHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetProjectByExternalKey.txt");
                if (_getProjectByExternalKeyResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _getProjectByExternalKeyResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _projectByExternalKey = _getProjectByExternalKeyResult.Return.FirstOrDefault();
                if (_projectByExternalKey == null)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No project fetched");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Project by external key (ID: {0})", _projectByExternalKey.ProjectID);
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

                _task1.ExternalKeys = new[] { new ExternalSystemContext { SystemName = "Jira", ExternalID = "ENDK-1" } };
                _task1.IsExternalKeysLoaded = true;

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

                var _getTaskByExternalKeyResult = ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByExternalKey("ENDK-1", "Jira", ProjectManagementHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByExternalKey.txt");
                if (_getTaskByExternalKeyResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _getTaskByExternalKeyResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _taskByExternalKey = _getTaskByExternalKeyResult.Return.FirstOrDefault();
                if (_taskByExternalKey == null)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task fetched");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task by external key (ID: {0})", _taskByExternalKey.TaskID);
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