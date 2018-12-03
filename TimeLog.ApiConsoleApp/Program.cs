namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.IO;
    using System.Reflection;

    using log4net;
    using log4net.Config;

    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        private static readonly DirectoryInfo AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

        public static void Main(string[] args)
        {
            // Initialize console and file logging.
            XmlConfigurator.Configure(new FileInfo(AppPath + "\\log4net.config"));
            BasicConfigurator.Configure();

            try
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Running application");
                }

                // Run example classes
                // ConsumeReportingApi.Consume();
                // ConsumeTransactionalApi.Consume();
                // ConsumeCsvFile.Consume();
                // GetEmployeesTransactionalApi.Consume();
                // CreateProjectTransactionalApi.Consume();
                // FetchProjectAndTasks.Consume();
                // InsertCustomerTest.Consume();
                // InsertEmployeeTest.Consume();
                // CreateProjectsForAllCustomers.Consume();
                // CreateProjectsForCustomersInSql.Consume();
                // InvoiceManipulation.Consume();
                // ManipulateTasks.Consume();
                // InsertWork.Consume();
                // CreateProjectTransactionalApi2.Consume();
                ExternalKeysOnProjectAndTasks.Consume();
            }
            catch (Exception ex)
            {
                // Catch any exception and report
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error("Application loop exception", ex);
                }
            }

            if (Logger.IsInfoEnabled)
            {
                Logger.Info("---");
                Logger.Info("Application loop ended. Click to exit");
            }

            // Wait for the user to end the application.
            Console.ReadKey();
        }
    }
}