using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.InvoicingService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the invoicing service
/// </summary>
public class InvoicingHandler : IDisposable
{
    private static InvoicingHandler? _instance;

    private bool _collectRawRequestResponse;
    private InvoicingServiceClient? _invoicingClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="InvoicingHandler" /> class from being created.
    /// </summary>
    private InvoicingHandler()
    {
        _collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="InvoicingHandler" />.
    /// </summary>
    public static InvoicingHandler Instance => _instance ??= new InvoicingHandler();

    /// <summary>
    ///     Gets the uri associated with the invoicing service.
    /// </summary>
    public string InvoicingServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/Invoicing/V1_1/InvoicingServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/Invoicing/V1_1/InvoicingService.svc";
        }
    }

    /// <summary>
    ///     Gets the invoicing token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
        get => _collectRawRequestResponse;

        set
        {
            _collectRawRequestResponse = value;
            _invoicingClient = null;
        }
    }

    /// <summary>
    ///     Gets the active invoicing client
    /// </summary>
    public InvoicingServiceClient InvoicingClient
    {
        get
        {
            if (_invoicingClient == null)
            {
                var endpoint = new EndpointAddress(InvoicingServiceUrl);

                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(InvoicingServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    _invoicingClient = new InvoicingServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (InvoicingServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    _invoicingClient = new InvoicingServiceClient(binding, endpoint);
                }
            }

            _invoicingClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            return _invoicingClient;
        }
    }

    /// <summary>
    ///     Dispose
    /// </summary>
    public void Dispose()
    {
        _invoicingClient = null;
        _instance = null;
    }
}