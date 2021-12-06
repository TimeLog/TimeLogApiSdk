using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.CRMService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the CRM service
/// </summary>
public class CRMHandler : IDisposable
{
    private static CRMHandler? _instance;

    private bool collectRawRequestResponse;
    private CRMServiceClient? crmClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="CRMHandler" /> class from being created.
    /// </summary>
    private CRMHandler()
    {
        collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="CRMHandler" />.
    /// </summary>
    public static CRMHandler Instance => _instance ??= new CRMHandler();

    /// <summary>
    ///     Gets the uri associated with the CRM service.
    /// </summary>
    public string CrmServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/CRM/V1_4/CRMServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/CRM/V1_4/CRMService.svc";
        }
    }

    /// <summary>
    ///     Gets the CRM token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
            crmClient = null;
        }
    }

    /// <summary>
    ///     Gets the active CRM client
    /// </summary>
    public CRMServiceClient CrmClient
    {
        get
        {
            if (crmClient == null)
            {
                var endpoint = new EndpointAddress(CrmServiceUrl);
                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(CrmServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    crmClient = new CRMServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (CrmServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    crmClient = new CRMServiceClient(binding, endpoint);
                }

                crmClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return crmClient;
        }
    }

    /// <summary>
    ///     Dispose
    /// </summary>
    public void Dispose()
    {
        crmClient = null;
        _instance = null;
    }
}