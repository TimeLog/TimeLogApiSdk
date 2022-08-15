using System.Reflection;
using log4net;
using log4net.Config;
using TimeLog.API.ConsoleApp;

var logger = LogManager.GetLogger(typeof(Program));
var appPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

// Initialize console and file logging.
XmlConfigurator.Configure(new FileInfo(appPath + "\\log4net.config"));
BasicConfigurator.Configure();

try
{
    if (logger.IsInfoEnabled)
    {
        logger.Info("Running application");
    }

    // Run example classes
    //ConsumeReportingApi.Consume();
    ConsumeWorkUnitReportingApi.Consume();
    // ConsumeTransactionalApi.Consume();
    // ConsumeCsvFile.Consume();
    // GetEmployeesTransactionalApi.Consume();
    // CreateProjectTransactionalApi.Consume();
    // FetchProjectAndTasks.Consume();
    // InsertCustomerTest.Consume();
    // InsertEmployeeTest.Consume();
    // CreateProjectsForAllCustomers.Consume();
    // CreateProjectsForCustomersInSql.Consume();
    // CreateProjectsForCustomersInCsv.Consume();
    // InvoiceManipulation.Consume();
    // ManipulateTasks.Consume();
    // InsertWork.Consume();
    // CreateProjectTransactionalApi2.Consume();
    // ManipulateExternalKeys.Consume();
    // GetWorkUnitChangeLog.Consume();
}
catch (Exception ex)
{
    // Catch any exception and report
    if (logger.IsErrorEnabled)
    {
        logger.Error("Application loop exception", ex);
    }
}

if (logger.IsInfoEnabled)
{
    logger.Info("---");
    logger.Info("Application loop ended. Click to exit");
}

// Wait for the user to end the application.
Console.ReadKey();