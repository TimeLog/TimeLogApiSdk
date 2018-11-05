namespace TimeLog.ApiConsoleApp
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class InsertWork
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InsertWork));

        public static void Consume()
        {
            // For getting the raw XML
            SecurityHandler.Instance.CollectRawRequestResponse = true;

            if (SecurityHandler.Instance.TryAuthenticate(out var _messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _workUnits = new List<WorkUnit>();

                _workUnits.Add(
                    new WorkUnit
                    {
                        // AllocationGUID = Guid.Parse("243A4DE6-B869-4E05-8EA8-AEB42AF38063"), // Not required if TaskID is defined
                        Description = "Test registration", // Required
                        EmployeeInitials = ProjectManagementHandler.Instance.Token.Initials, // Required. Either initials or username is accepted
                        // EndDateTime = DateTime.Now.AddHours(-5), // Not required for most scenarios
                        StartDateTime = DateTime.Now.AddHours(-4), // Required
                        TaskID = 1000, // Always required
                        GUID = Guid.NewGuid(), // Not necessary, but good practice
                        Duration = TimeSpan.FromHours(1), // Required
                        // IsEditable = true, // Not required. Internal use
                        // IsMeeting = false // Not required. Special field for interacting with calendar items
                        // TimeStamp = new byte[] { }, // Not required. Internal use
                        // AdditionalText = "", // Only required if the site has this feature turned ON
                        // BillableDuration = TimeSpan.FromHours(1), // Add if you want to deviate from registrered time
                    });

                var _insertWorkResult = ProjectManagementHandler.Instance.ProjectManagementClient.InsertWork(_workUnits.ToArray(), 99, ProjectManagementHandler.Instance.Token);

                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("General request status: " + _insertWorkResult.ResponseState.ToString("G"));
                }

                foreach (var _containerOfWorkUnit in _insertWorkResult.Return)
                {
                    if (Logger.IsInfoEnabled)
                    {
                        Logger.Info((_containerOfWorkUnit.Item?.GUID.ToString() ?? "Unknown") + " - " + _containerOfWorkUnit.Status.ToString("G") + " > " + _containerOfWorkUnit.Message);
                    }
                }
                foreach (var _apiMessage in _insertWorkResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(_apiMessage.Message);
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