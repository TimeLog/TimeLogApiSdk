using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.ProjectManagementService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the project management service
/// </summary>
public class ProjectManagementHandler : IDisposable
{
    private static ProjectManagementHandler? _instance;

    private bool collectRawRequestResponse;
    private ProjectManagementServiceClient? projectManagementClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="ProjectManagementHandler" /> class from being created.
    /// </summary>
    private ProjectManagementHandler()
    {
        collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="ProjectManagementHandler" />.
    /// </summary>
    public static ProjectManagementHandler Instance => _instance ??= new ProjectManagementHandler();

    /// <summary>
    ///     Gets the uri associated with the project management service.
    /// </summary>
    public string ProjectManagementServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url +
                       "WebServices/ProjectManagement/V1_7/ProjectManagementServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/ProjectManagement/V1_7/ProjectManagementService.svc";
        }
    }

    /// <summary>
    ///     Gets the project management token for use in other methods. Makes use of SecurityHandler.Instance.Token.
    /// </summary>
    public SecurityToken Token =>
        new()
        {
            Expires = SecurityHandler.Instance.Token.Expires,
            Hash = SecurityHandler.Instance.Token.Hash,
            Initials = SecurityHandler.Instance.Token.Initials
        };

    /// <summary>
    ///     Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
    /// </summary>
    public bool CollectRawRequestResponse
    {
        get => collectRawRequestResponse;

        set
        {
            collectRawRequestResponse = value;
            projectManagementClient = null;
        }
    }

    /// <summary>
    ///     Gets the active project management client
    /// </summary>
    public ProjectManagementServiceClient ProjectManagementClient
    {
        get
        {
            if (projectManagementClient == null)
            {
                var endpoint = new EndpointAddress(ProjectManagementServiceUrl);

                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(ProjectManagementServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    projectManagementClient = new ProjectManagementServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                        {MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize};
                    if (ProjectManagementServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    projectManagementClient = new ProjectManagementServiceClient(binding, endpoint);
                }

                projectManagementClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return projectManagementClient;
        }
    }

    void IDisposable.Dispose()
    {
        projectManagementClient = null;
        _instance = null;
    }
}