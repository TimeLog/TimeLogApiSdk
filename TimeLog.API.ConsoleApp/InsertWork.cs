using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class InsertWork
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(InsertWork));

    public static void Consume()
    {
        // For getting the raw XML
        SecurityHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out var messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var workUnits = new List<WorkUnit>
            {
                new()
                {
                    // AllocationGUID = Guid.Parse("243A4DE6-B869-4E05-8EA8-AEB42AF38063"), // Not required if TaskID is defined
                    Description = "Test registration", // Required
                    EmployeeInitials =
                        ProjectManagementHandler.Instance.Token
                            .Initials, // Required. Either initials or username is accepted
                    // EndDateTime = DateTime.Now.AddHours(-5), // Not required for most scenarios
                    StartDateTime = DateTime.Now.AddHours(-4), // Required
                    TaskID = 1000, // Always required
                    GUID = Guid.NewGuid(), // Not necessary, but good practice
                    Duration = TimeSpan.FromHours(1) // Required
                    // IsEditable = true, // Not required. Internal use
                    // IsMeeting = false // Not required. Special field for interacting with calendar items
                    // TimeStamp = new byte[] { }, // Not required. Internal use
                    // AdditionalText = "", // Only required if the site has this feature turned ON
                    // BillableDuration = TimeSpan.FromHours(1), // Add if you want to deviate from registrered time
                }
            };

            var insertWorkResult =
                ProjectManagementHandler.Instance.ProjectManagementClient.InsertWork(workUnits.ToArray(), 99,
                    ProjectManagementHandler.Instance.Token);

            if (Logger.IsInfoEnabled)
            {
                Logger.Info("General request status: " + insertWorkResult.ResponseState.ToString("G"));
            }

            foreach (var containerOfWorkUnit in insertWorkResult.Return)
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info((containerOfWorkUnit.Item?.GUID.ToString() ?? "Unknown") + " - " +
                                containerOfWorkUnit.Status.ToString("G") + " > " + containerOfWorkUnit.Message);
                }
            }

            foreach (var apiMessage in insertWorkResult.Messages)
            {
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error(apiMessage.Message);
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