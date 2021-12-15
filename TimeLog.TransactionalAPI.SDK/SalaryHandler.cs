using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using TimeLog.TransactionalAPI.SDK.SalaryService;

namespace TimeLog.TransactionalAPI.SDK;

/// <summary>
///     Handler of functionality related to the salary service
/// </summary>
public class SalaryHandler : IDisposable
{
    private static SalaryHandler? _instance;

    private bool _collectRawRequestResponse;
    private SalaryServiceClient? _salaryClient;

    /// <summary>
    ///     Prevents a default instance of the <see cref="SalaryHandler" /> class from being created.
    /// </summary>
    private SalaryHandler()
    {
        _collectRawRequestResponse = false;
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="SalaryHandler" />.
    /// </summary>
    public static SalaryHandler Instance => _instance ??= new SalaryHandler();

    /// <summary>
    ///     Gets the uri associated with the salary service.
    /// </summary>
    public string SalaryServiceUrl
    {
        get
        {
            if (SettingsHandler.Instance.Url.Contains("https"))
            {
                return SettingsHandler.Instance.Url + "WebServices/Salary/V1_1/SalaryServiceSecure.svc";
            }

            return SettingsHandler.Instance.Url + "WebServices/Salary/V1_1/SalaryService.svc";
        }
    }

    /// <summary>
    ///     Gets the salary token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
            _salaryClient = null;
        }
    }

    /// <summary>
    ///     Gets the active salary client
    /// </summary>
    public SalaryServiceClient SalaryClient
    {
        get
        {
            if (_salaryClient == null)
            {
                var endpoint = new EndpointAddress(SalaryServiceUrl);
                if (CollectRawRequestResponse)
                {
                    var binding = new CustomBinding();
                    var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                    binding.Elements.Add(encoding);
                    binding.Elements.Add(SalaryServiceUrl.Contains("https")
                        ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                        : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                    _salaryClient = new SalaryServiceClient(binding, endpoint);
                }
                else
                {
                    var binding = new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                    };

                    if (SalaryServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    _salaryClient = new SalaryServiceClient(binding, endpoint);
                }

                _salaryClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
            }

            return _salaryClient;
        }
    }

    void IDisposable.Dispose()
    {
        _salaryClient = null;
        _instance = null;
    }
}