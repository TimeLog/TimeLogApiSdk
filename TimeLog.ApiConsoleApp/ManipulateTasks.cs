namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class ManipulateTasks
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ManipulateTasks));

        public static void Consume()
        {
            // For getting the raw XML
            SecurityHandler.Instance.CollectRawRequestResponse = false;
            ProjectManagementHandler.Instance.CollectRawRequestResponse = false;

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _tasks = ProjectManagementHandler.Instance.ProjectManagementClient.GetTasksByProjectId(Guid.Parse("F88D516A-215F-4E5C-A7CB-FD79E64996F5"), ProjectManagementHandler.Instance.Token);

                foreach (var _task in _tasks.Return)
                {
                    Logger.Debug("Task.Details.WBS: " + _task.Details.WBS);
                    Logger.Debug("Task.FullName: " + _task.FullName);
                    Logger.Debug("Task.ProjectSubContractID: " + _task.ProjectSubContractID);

                    // OBSOLETE
                    //_task.ProjectSubContractID = 22;
                    //var _updateTaskRaw = ProjectManagementHandler.Instance.ProjectManagementClient.UpdateTask(_task, _task.Details.ProjectHeader.ID, ProjectManagementHandler.Instance.Token);

                    //foreach (var _apiMessage in _updateTaskRaw.Messages)
                    //{
                    //    Logger.Debug("UpdateTask message: " + _apiMessage.Message);
                    //}

                    var _changeTaskContractRaw = ProjectManagementHandler.Instance.ProjectManagementClient.ChangeTaskContract(_task.ID, 22, ProjectManagementHandler.Instance.Token);

                    foreach (var _apiMessage in _changeTaskContractRaw.Messages)
                    {
                        Logger.Debug("ChangeTaskContract message: " + _apiMessage.Message);
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
}