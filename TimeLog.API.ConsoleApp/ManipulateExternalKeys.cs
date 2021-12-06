using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class ManipulateExternalKeys
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ManipulateExternalKeys));

    public static void Consume()
    {
        ProjectManagementHandler.Instance.CollectRawRequestResponse = true;
        SecurityHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out IEnumerable<string> messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");

            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var getTaskByID1 =
                ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByID(22627, true,
                    ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByID1.txt");
            if (getTaskByID1.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in getTaskByID1.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var task = getTaskByID1.Return.FirstOrDefault();
            if (task == null)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task fetched");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task fetched (ID: {0})", task.TaskID);
            }

            task.ExternalKeys = new[]
            {
                new ExternalSystemContext {SystemName = "Jira", ExternalID = "ENDK-1"},
                new ExternalSystemContext {SystemName = "OEBS", ExternalID = "200542"}
            };
            task.IsExternalKeysLoaded = true;

            var updateTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(task,
                task.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\UpdateTask.txt");

            if (updateTaskResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in updateTaskResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var taskUpdated = updateTaskResult.Return.FirstOrDefault();
            if (taskUpdated == null)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task updated");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task updated (ID: {0})", taskUpdated.Item.TaskID);
            }

            var getTaskByExternalKeyResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByExternalKey("ENDK-1", "Jira",
                    ProjectManagementHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByExternalKey.txt");
            if (getTaskByExternalKeyResult.ResponseState != ExecutionStatus.Success)
            {
                foreach (var apiMessage in getTaskByExternalKeyResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }

                return;
            }

            var taskByExternalKey = getTaskByExternalKeyResult.Return.FirstOrDefault();
            if (taskByExternalKey == null)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("No task fetched");
                }

                return;
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("Task by external key (ID: {0})", taskByExternalKey.TaskID);
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