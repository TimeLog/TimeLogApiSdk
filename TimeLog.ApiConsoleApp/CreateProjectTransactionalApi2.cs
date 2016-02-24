namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

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

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var newProject = new Project()
                {
                    ID = Guid.NewGuid(),
                    AccountManagerID = 384,
                    ProjectManagerID = 384,
                    TypeID = 247,
                    StartDate = DateTime.Today,
                    StageID = 2,
                    Name = "Projekt PivotPoint",
                    CustomerID = 707, //Original value 709 does not exist
                    Action = DataAction.Created,
                    BudgetAmountExpenses = 0,
                    BudgetAmountTravelExpenses = 0,
                    BudgetWorkAmountFixedPriceProject = 0,
                    BudgetWorkAmountFixedPriceTasks = 0,
                    BudgetWorkAmountTimeAndMaterial = 0,
                    BudgetWorkHoursFixedPriceProject = 0,
                    BudgetWorkHoursFixedPriceTasks = 0,
                    BudgetWorkHoursTimeAndMaterial = 0,
                    CategoryID = 3,
                    CurrencyID = 35,
                    DepartmentHandledByID = 0,
                    DepartmentOrderedByID = 0,
                    Description = string.Empty,
                    EndDate = DateTime.Today.AddDays(30),
                    ExchangeRate = 1,
                    No = "42",
                    PriceListID = 1,
                    PriceGroupID = 1,
                    PurchaseOrderNumber = "42"
                };

                var createProjectResult = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProject(newProject,
                    ProjectManagementHandler.Instance.Token);


                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\CreateProject.txt");
                if (createProjectResult.ResponseState == ExecutionStatus.Success)
                {
                    var project = createProjectResult.Return.FirstOrDefault();
                    if (project != null)
                    {

                    }
                    else
                    {
                        if (Logger.IsWarnEnabled)
                        {
                            Logger.Warn("No project found");
                        }
                    }
                }
                else
                {
                    foreach (var apiMessage in createProjectResult.Messages)
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