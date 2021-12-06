using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.HelpDeskService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the HelpDesk service
/// </summary>
public class HelpDeskHandler : IDisposable
{
    private static HelpDeskHandler? _instance;

    private bool collectRawRequestResponse;
    private HelpDeskServiceClient? helpDeskClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="HelpDeskHandler" /> class from being created.
    /// </summary>
    private HelpDeskHandler()
    {
        collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="HelpDeskHandler" />.
    /// </summary>
    public static HelpDeskHandler Instance => _instance ??= new HelpDeskHandler();

    /// <summary>
    ///     Gets the uri associated with the HelpDesk service.
    /// </summary>
    public string HelpDeskServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/HelpDesk/V1_6/HelpDeskServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/HelpDesk/V1_6/HelpDeskService.svc";
        }
    }

    /// <summary>
    ///     Gets the HelpDesk token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
            helpDeskClient = null;
        }
    }

    /// <summary>
    ///     Gets the active HelpDesk client
    /// </summary>
    public HelpDeskServiceClient HelpDeskClient
    {
        get
        {
            if (helpDeskClient == null)
            {
                var endpoint = new EndpointAddress(HelpDeskServiceUrl);
                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(HelpDeskServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    helpDeskClient = new HelpDeskServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (HelpDeskServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    helpDeskClient = new HelpDeskServiceClient(binding, endpoint);
                }

                helpDeskClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return helpDeskClient;
        }
    }

    void IDisposable.Dispose()
    {
        helpDeskClient = null;
        _instance = null;
    }
}