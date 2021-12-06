using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class ConsumeTransactionalApi
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ConsumeTransactionalApi));

    public static void Consume()
    {
        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var result = ProjectManagementHandler.Instance.ProjectManagementClient.GetWorkPaged(
                "LCH",
                new DateTime(2021, 1, 1),
                new DateTime(2021, 1, 31),
                1,
                350,
                ProjectManagementHandler.Instance.Token);

            if (result.ResponseState == ExecutionStatus.Success)
            {
                double sum = 0;
                foreach (var workUnitFlat in result.Return)
                {
                    sum += workUnitFlat.Hours;

                    if (Logger.IsDebugEnabled)
                    {
                        Logger.DebugFormat("{0} hours registrered on {1}", workUnitFlat.Hours, workUnitFlat.Date);
                    }
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Number of registrations: {0}", result.Return.Count());
                    Logger.DebugFormat("Sum of hours: {0}", sum);
                }
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