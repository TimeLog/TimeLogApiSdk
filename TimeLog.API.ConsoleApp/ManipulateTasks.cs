using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class ManipulateTasks
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ManipulateTasks));

    public static void Consume()
    {
        // For getting the raw XML
        SecurityHandler.Instance.CollectRawRequestResponse = false;
        ProjectManagementHandler.Instance.CollectRawRequestResponse = false;

        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var tasks = ProjectManagementHandler.Instance.ProjectManagementClient.GetTasksByProjectId(
                Guid.Parse("F88D516A-215F-4E5C-A7CB-FD79E64996F5"), ProjectManagementHandler.Instance.Token);

            foreach (var task in tasks.Return)
            {
                Logger.Debug("Task.Details.WBS: " + task.Details.WBS);
                Logger.Debug("Task.FullName: " + task.FullName);
                Logger.Debug("Task.ProjectSubContractID: " + task.ProjectSubContractID);

                // OBSOLETE
                //_task.ProjectSubContractID = 22;
                //var _updateTaskRaw = ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(_task, _task.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);

                //foreach (var _apiMessage in _updateTaskRaw.Messages)
                //{
                //    Logger.Debug("UpdateTask message: " + _apiMessage.Message);
                //}

                var changeTaskContractRaw =
                    ProjectManagementHandler.Instance.ProjectManagementClient.ChangeTaskContract(task.ID, 22,
                        ProjectManagementHandler.Instance.Token);

                foreach (var apiMessage in changeTaskContractRaw.Messages)
                {
                    Logger.Debug("ChangeTaskContract message: " + apiMessage.Message);
                }

                Logger.Debug("---");
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