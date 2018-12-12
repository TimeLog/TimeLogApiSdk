namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class ManipulateExternalKeys
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ManipulateExternalKeys));
        
        public static void Consume()
        {
            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;
            SecurityHandler.Instance.CollectRawRequestResponse = true;

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");

                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _getTaskByID1 = ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByID1(22627, true, ProjectManagementHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByID1.txt");
                if (_getTaskByID1.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _getTaskByID1.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _task = _getTaskByID1.Return.FirstOrDefault();
                if (_task == null)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task fetched");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task fetched (ID: {0})", _task.TaskID);
                }

                _task.ExternalKeys = new[]
                                          {
                                              new ExternalSystemContext { SystemName = "Jira", ExternalID = "ENDK-1" },
                                              new ExternalSystemContext { SystemName = "OEBS", ExternalID = "200542" }
                                          };
                _task.IsExternalKeysLoaded = true;

                var _updateTaskResult = ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(_task, _task.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\UpdateTask.txt");

                if (_updateTaskResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _updateTaskResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _taskUpdated = _updateTaskResult.Return.FirstOrDefault();
                if (_taskUpdated == null)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task updated");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task updated (ID: {0})", _taskUpdated.Item.TaskID);
                }

                var _getTaskByExternalKeyResult = ProjectManagementHandler.Instance.ProjectManagementClient.GetTaskByExternalKey("ENDK-1", "Jira", ProjectManagementHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\GetTaskByExternalKey.txt");
                if (_getTaskByExternalKeyResult.ResponseState != ExecutionStatus.Success)
                {
                    foreach (var _apiMessage in _getTaskByExternalKeyResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }

                    return;
                }

                var _taskByExternalKey = _getTaskByExternalKeyResult.Return.FirstOrDefault();
                if (_taskByExternalKey == null)
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No task fetched");
                    }

                    return;
                }

                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat("Task by external key (ID: {0})", _taskByExternalKey.TaskID);
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