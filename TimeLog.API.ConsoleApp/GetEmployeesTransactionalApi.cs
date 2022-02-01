using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.OrganisationService;

namespace TimeLog.API.ConsoleApp;

public class GetEmployeesTransactionalApi
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(GetEmployeesTransactionalApi));

    public static void Consume()
    {
        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var resultCount = 9999;
            var pageIndex = 1;

            while (resultCount > 0)
            {
                var result =
                    OrganisationHandler.Instance.OrganisationClient.GetEmployeesPaged(pageIndex, 100,
                        OrganisationHandler.Instance.Token);
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