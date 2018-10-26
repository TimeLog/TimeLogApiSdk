namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class FetchProjectAndTasks
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FetchProjectAndTasks));
        
        public static void Consume()
        {
            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _result = ProjectManagementHandler.Instance.ProjectManagementClient.GetProjectTasksPaged(Guid.Parse("02624193-784C-4569-9CD1-50B764EEE1A6"), 1, 100, ProjectManagementHandler.Instance.Token);
                if (_result.ResponseState == ExecutionStatus.Success)
                {
                    foreach (var _task in _result.Return)
                    {
                        if (Logger.IsDebugEnabled)
                        {
                            Logger.DebugFormat("{0} > {1}", _task.TaskWBS, _task.TaskName);
                        }
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
                    Logger.Warn(string.Join(",", messages));
                }
            }
        }
    }
}