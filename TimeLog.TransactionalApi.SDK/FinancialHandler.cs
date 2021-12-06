using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.FinancialService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the fiancial service
/// </summary>
public class FinancialHandler : IDisposable
{
    private static FinancialHandler? _instance;

    private bool collectRawRequestResponse;
    private FinancialServiceClient? financialClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="FinancialHandler" /> class from being created.
    /// </summary>
    private FinancialHandler()
    {
        collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="FinancialHandler" />.
    /// </summary>
    public static FinancialHandler Instance => _instance ??= new FinancialHandler();

    /// <summary>
    ///     Gets the uri associated with the Financial service.
    /// </summary>
    public string FinancialServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/Financial/V1_2/FinancialServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/Financial/V1_2/FinancialService.svc";
        }
    }

    /// <summary>
    ///     Gets the financial token for use in other methods. Makes use of SecurityHandler.Instance.Token.
    /// </summary>
    public SecurityToken Token => new()
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
            financialClient = null;
        }
    }

    /// <summary>
    ///     Gets the active Financial client
    /// </summary>
    public FinancialServiceClient FinancialClient
    {
        get
        {
            if (financialClient == null)
            {
                var endpoint = new EndpointAddress(FinancialServiceUrl);
                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(FinancialServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    financialClient = new FinancialServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (FinancialServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    financialClient = new FinancialServiceClient(binding, endpoint);
                }

                financialClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return financialClient;
        }
    }

    void IDisposable.Dispose()
    {
        financialClient = null;
        _instance = null;
    }
}