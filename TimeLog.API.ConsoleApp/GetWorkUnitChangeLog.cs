using System.Collections.Generic;

namespace TimeLog.ApiConsoleApp
{
    using System;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;

    public class GetWorkUnitChangeLog
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GetWorkUnitChangeLog));

        public static void Consume()
        {
            IEnumerable<string> _messages;
            if (SecurityHandler.Instance.TryAuthenticate(out _messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                int _resultCount = 9999;
                int _pageIndex = 1;

                while (_resultCount > 0)
                {
                    var _result = ProjectManagementHandler.Instance.ProjectManagementClient.GetWorkChangeLogPaged(new DateTime(2018, 12, 20, 0, 0, 0), new DateTime(2018, 12, 21, 23, 59, 59), true, true, true, _pageIndex, 100, ProjectManagementHandler.Instance.Token);
                    if (_result.ResponseState == ExecutionStatus.Success)
                    {
                        Logger.Info("Page " + _pageIndex + " with " + _result.Return.Length + " results");
                        _resultCount = _result.Return.Length;

                        foreach (var _workUnitFlat in _result.Return)
                        {
                            Logger.InfoFormat("On {0} did the employee ({1}) add {2} hours tracked on {3}", _workUnitFlat.ActionDate, _workUnitFlat.EmployeeInitials, _workUnitFlat.Hours, _workUnitFlat.Date);
                        }

                        _pageIndex = _pageIndex + 1;
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

                        break;
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
}
