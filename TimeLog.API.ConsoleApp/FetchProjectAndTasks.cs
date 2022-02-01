using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class FetchProjectAndTasks
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(FetchProjectAndTasks));

    public static void Consume()
    {
        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var result = ProjectManagementHandler.Instance.ProjectManagementClient.GetProjectTasksPaged(
                Guid.Parse("02624193-784C-4569-9CD1-50B764EEE1A6"), 1, 100, ProjectManagementHandler.Instance.Token);
            if (result.ResponseState == ExecutionStatus.Success)
            {
                foreach (var task in result.Return)
                {
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.DebugFormat("{0} > {1}", task.TaskWBS, task.TaskName);
                    }
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