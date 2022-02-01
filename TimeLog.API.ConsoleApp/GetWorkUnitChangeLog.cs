using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;

namespace TimeLog.API.ConsoleApp;

public class GetWorkUnitChangeLog
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(GetWorkUnitChangeLog));

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
                var result = ProjectManagementHandler.Instance.ProjectManagementClient.GetWorkChangeLogPaged(
                    new DateTime(2018, 12, 20, 0, 0, 0), new DateTime(2018, 12, 21, 23, 59, 59), true, true, true,
                    pageIndex, 100, ProjectManagementHandler.Instance.Token);
                if (result.ResponseState == ExecutionStatus.Success)
                {
                    Logger.Info("Page " + pageIndex + " with " + result.Return.Length + " results");
                    resultCount = result.Return.Length;

                    foreach (var workUnitFlat in result.Return)
                    {
                        Logger.InfoFormat("On {0} did the employee ({1}) add {2} hours tracked on {3}",
                            workUnitFlat.ActionDate, workUnitFlat.EmployeeInitials, workUnitFlat.Hours,
                            workUnitFlat.Date);
                    }

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