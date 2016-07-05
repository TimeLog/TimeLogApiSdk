using System.Collections.Generic;

namespace TimeLog.ApiConsoleApp
{
    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.OrganisationService;

    public class GetEmployeesTransactionalApi
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GetEmployeesTransactionalApi));

        public static void Consume()
        {
            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                int resultCount = 9999;
                int pageIndex = 1;

                while (resultCount > 0)
                {
                    var result = OrganisationHandler.Instance.OrganisationClient.GetEmployeesPaged(pageIndex, 100, OrganisationHandler.Instance.Token);
                    if (result.ResponseState == ExecutionStatus.Success)
                    {
                        Logger.Info("Page " + pageIndex + " with " + result.Return.Length + " results");
                        resultCount = result.Return.Length;
                        pageIndex = pageIndex + 1;
                    }
                    else
                    {
                        foreach (var apiMessage in result.Messages)
                        {
                            if (Logger.IsErrorEnabled)
                            {
                                Logger.Error(apiMessage.Message);
                            }
                        }

                        break;
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
