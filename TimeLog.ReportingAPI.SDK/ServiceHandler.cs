using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Handling security and connectivity for the reporting API
/// </summary>
public class ServiceHandler : IDisposable
{
    private static ServiceHandler _instance;
    private ServiceSoapClient _client;

    /// <summary>
    ///     Prevents a default instance of the <see cref="ServiceHandler" /> class from being created.
    /// </summary>
    public ServiceHandler(string siteCode,
        string apiId,
        string apiPassword,
        string serviceUrl,
        long maxReceivedMessageSize = 4096000,
        TimeSpan? timeOut = null)
    {
        if (string.IsNullOrWhiteSpace(siteCode))
        {
            throw new ArgumentException("SiteCode empty!");
        }

        SiteCode = siteCode;
        ApiId = apiId;
        ApiPassword = apiPassword;
        ServiceUrl = serviceUrl.Trim('/') + "/service.asmx";
        MaxReceivedMessageSize = maxReceivedMessageSize;
        Timeout = timeOut ?? TimeSpan.FromSeconds(60);
    }

    /// <summary>
    ///     Gets the singleton instance of the <see cref="ServiceHandler" />.
    /// </summary>
    public static ServiceHandler Instance
    {
        get
        {
            if (!long.TryParse(
                    ConfigurationManager.AppSettings["TimeLogProjectMaxReceivedMessageSize"],
                    out var maxReceivedMessageSize))
            {
                maxReceivedMessageSize = 4096000;
            }

            if (!int.TryParse(
                    ConfigurationManager.AppSettings["TimeLogProjectTimeoutInSeconds"],
                    out var timeOutSeconds))
            {
                timeOutSeconds = 60;
            }

            var serviceUrl = "";

            var url = ConfigurationManager.AppSettings["TimeLogProjectUri"];

            if (url != null && !url.EndsWith("/"))
            {
                url += "/";
            }

            if (Uri.TryCreate(url, UriKind.Absolute, out var rootUri))
            {
                if (rootUri.ToString().Contains("http://") && !rootUri.ToString().Contains("localhost"))
                {
                    serviceUrl = rootUri.ToString().Replace("http://", "https://");
                }

                serviceUrl = rootUri.ToString();
            }
            else
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectUri\" is missing or invalid Uri");
            }

            return _instance ??= new ServiceHandler(
                ConfigurationManager.AppSettings["TimeLogProjectReportingSiteCode"],
                ConfigurationManager.AppSettings["TimeLogProjectReportingApiId"],
                ConfigurationManager.AppSettings["TimeLogProjectReportingApiPassword"],
                serviceUrl,
                maxReceivedMessageSize,
                TimeSpan.FromSeconds(timeOutSeconds));
        }
    }

    /// <summary>
    ///     Gets the correct culture for converting data from the reporting API
    /// </summary>
    public static CultureInfo DataCulture => new("en-US");

    /// <summary>
    ///     Gets the uri associated with the reporting service.
    /// </summary>
    public string ServiceUrl { get; private set; }

    /// <summary>
    ///     Gets the site code associated with the reporting service
    /// </summary>
    public string SiteCode { get; }

    /// <summary>
    ///     Gets the user associated with the reporting service
    /// </summary>
    public string ApiId { get; }

    /// <summary>
    ///     Gets the password associated with the reporting service
    /// </summary>
    public string ApiPassword { get; }

    /// <summary>
    ///     Gets the default max received message size for all calls to the TimeLog API.
    ///     Default is 1024000, but can be overwritten from application setting TimeLogProjectMaxReceivedMessageSize.
    /// </summary>
    public long MaxReceivedMessageSize { get; }

    /// <summary>
    ///     Gets the default timeout value for all calls to the TimeLog API.
    ///     Default is 60 seconds, but can be overwritten from application setting TimeLogProjectTimeoutInSeconds.
    /// </summary>
    public TimeSpan Timeout { get; }

    /// <summary>
    ///     Gets the client associated with the reporting service
    /// </summary>
    public ServiceSoapClient Client
    {
        get
        {
            if (_client == null)
            {
                var binding = new BasicHttpBinding
                {
                    MaxReceivedMessageSize = MaxReceivedMessageSize,
                    CloseTimeout = Timeout,
                    OpenTimeout = Timeout,
                    ReceiveTimeout = Timeout,
                    SendTimeout = Timeout
                };

                var endpoint = new EndpointAddress(ServiceUrl);

                if (ServiceUrl.Contains("https"))
                {
                    binding.Security.Mode = BasicHttpSecurityMode.Transport;
                }

                _client = new ServiceSoapClient(binding, endpoint);
            }

            return _client;
        }
    }

    /// <summary>
    ///     Disposes the handler.
    /// </summary>
    public void Dispose()
    {
        _client = null;
        _instance = null;
    }

    /// <summary>
    ///     Overwrites the service URL from the app.config
    /// </summary>
    /// <param name="url">TimeLog URL (e.g. https://app4.timelog.com/soxdemo4 )</param>
    public void OverwriteServiceUrl(string url)
    {
        ServiceUrl = url.Trim('/') + "/service.asmx";
    }

    /// <summary>
    ///     Executes an authentication request to the API using the application settings
    ///     TimeLogProjectReportingSiteCode, TimeLogProjectReportingUser and TimeLogProjectReportingPassword.
    /// </summary>
    /// <remarks>
    ///     Make sure to set the application setting TimeLogProjectUri.
    /// </remarks>
    /// <returns>A value indicating whether the authentication is successful</returns>
    public bool TryAuthenticate()
    {
        if (string.IsNullOrWhiteSpace(SiteCode))
        {
            throw new ArgumentException("The AppSetting \"TimeLogProjectReportingSiteCode\" is missing");
        }

        if (string.IsNullOrWhiteSpace(ApiId))
        {
            throw new ArgumentException("The AppSetting \"TimeLogProjectReportingApiId\" is missing");
        }

        if (string.IsNullOrWhiteSpace(ApiPassword))
        {
            throw new ArgumentException("The AppSetting \"TimeLogProjectReportingApiPassword\" is missing");
        }

        return TryAuthenticate(SiteCode, ApiId, ApiPassword);
    }

    /// <summary>
    ///     Executes an authentication request to the API using the parameters.
    /// </summary>
    /// <param name="siteCode">Site code part of the credentials to authenticate with</param>
    /// <param name="user">User name part of the credentials to authenticate with</param>
    /// <param name="password">ApiPassword part of the credentials to authenticate with</param>
    /// <returns>A value indicating whether the authentication is successful</returns>
    public bool TryAuthenticate(string siteCode, string user, string password)
    {
        return Client.ValidateCredentials(siteCode, user, password);
    }
}