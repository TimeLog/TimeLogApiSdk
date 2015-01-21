namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using log4net;
    using log4net.Config;

    using TimeLog.TransactionalApi.SDK;

    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        private static readonly DirectoryInfo AppPath = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory;

        public static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            XmlConfigurator.Configure(new FileInfo(AppPath + "\\log4net.config"));

            try
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Running application");
                }

                IEnumerable<string> messages;
                SecurityHandler.Instance.TryAuthenticate(out messages);

                const string ProjectTemplate = "standard skabelon";
                string projectName = string.Empty;
                string projectNo = string.Empty;
                string customerName = string.Empty;
                Guid priceListId = Guid.Empty;
                Guid priceGroupId = Guid.Empty;
                string projectManagerInitials = string.Empty;
                string accountManagerInitials = string.Empty;
                string handledByDepartmentNo = string.Empty;
                string orderedByDepartmentNo = string.Empty;

                ProjectManagementHandler.Instance.ProjectManagementClient.CreateProjectFromTemplate(
                    ProjectTemplate,
                    projectName,
                    projectNo,
                    customerName,
                    priceListId,
                    priceGroupId,
                    projectManagerInitials,
                    accountManagerInitials,
                    handledByDepartmentNo,
                    orderedByDepartmentNo,
                    true,
                    true,
                    true,
                    false,
                    false,
                    99,
                    ProjectManagementHandler.Instance.Token);
            }
            catch (Exception ex)
            {
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

            Console.ReadKey();
        }
    }
}
