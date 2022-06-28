using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class UpdateTask
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(UpdateTask));

    public static void Consume()
    {
        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> _messages))
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Successfully authenticated on transactional API");
            }

            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;
            var _result = ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskById(
                Guid.Parse("CA0333D6-F1C1-4C3F-A02F-9006DF463E39"), ProjectManagementHandler.Instance.Token);
            if (_result.ResponseState == ExecutionStatus.Success)
            {
                foreach (var _task in _result.Return)
                {
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.DebugFormat("{0} > {1}", _task.Name, _task.ID);
                    }

                    ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(_task,
                        _task.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);
                    RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\UpdateTask.txt");
                }
            }
            else
            {
                foreach (var _apiMessage in _result.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(_apiMessage.Message);
                    }
                }
            }
        }
        else
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Failed to authenticate to transactional API");
                Logger.Warn(string.Join(",", _messages));
            }
        }
    }
}