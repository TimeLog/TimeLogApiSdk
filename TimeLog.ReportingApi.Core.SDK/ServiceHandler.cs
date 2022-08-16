using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using ServiceReference;

namespace TimeLog.ReportingApi.Core.SDK
{
    /// <summary>
    /// Handling security and connectivity for the reporting API
    /// </summary>
    public class ServiceHandler : IDisposable
    {
        private static ServiceHandler _instance;
        private ServiceSoapClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceHandler"/> class from being created.
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

            this.SiteCode = siteCode;
            this.ApiId = apiId;
            this.ApiPassword = apiPassword;
            this.ServiceUrl = serviceUrl.Trim('/') + "/service.asmx";
            this.MaxReceivedMessageSize = maxReceivedMessageSize;
            this.Timeout = timeOut ?? TimeSpan.FromSeconds(60);
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ServiceHandler"/>.
        /// </summary>
        public static ServiceHandler Instance
        {
            get
            {
                if (!long.TryParse(ConfigurationManager.AppSettings["TimeLogProjectMaxReceivedMessageSize"], out var _maxReceivedMessageSize))
                {
                    _maxReceivedMessageSize = 4096000;
                }

                if (!int.TryParse(ConfigurationManager.AppSettings["TimeLogProjectTimeoutInSeconds"],
                    out var _timeOutSeconds))
                {
                    _timeOutSeconds = 60;
                }

                var _serviceUrl = "";
                var _url = ConfigurationManager.AppSettings["TimeLogProjectUri"];
                if (!_url.EndsWith("/"))
                {
                    _url += "/";
                }

                if (Uri.TryCreate(_url, UriKind.Absolute, out var _rootUri))
                {
                    if (_rootUri.ToString().Contains("http://") && !_rootUri.ToString().Contains("localhost"))
                    {
                        _serviceUrl = _rootUri.ToString().Replace("http://", "https://");
                    }

                    _serviceUrl = _rootUri.ToString();
                }
                else
                {
                    throw new ArgumentException("The AppSetting \"TimeLogProjectUri\" is missing or invalid Uri");
                }

                return _instance ?? (_instance = new ServiceHandler(
                    ConfigurationManager.AppSettings["TimeLogProjectReportingSiteCode"],
                    ConfigurationManager.AppSettings["TimeLogProjectReportingApiId"],
                    ConfigurationManager.AppSettings["TimeLogProjectReportingApiPassword"],
                    _serviceUrl,
                    _maxReceivedMessageSize,
                    TimeSpan.FromSeconds(_timeOutSeconds)));
            }
        }

        /// <summary>
        /// Gets the correct culture for converting data from the reporting API
        /// </summary>
        public static CultureInfo DataCulture => new CultureInfo("en-US");

        /// <summary>
        /// Gets the uri associated with the reporting service.
        /// </summary>
        public string ServiceUrl { get; private set; }

        /// <summary>
        /// Gets the site code associated with the reporting service
        /// </summary>
        public string SiteCode { get; }

        /// <summary>
        /// Gets the user associated with the reporting service
        /// </summary>
        public string ApiId { get;}

        /// <summary>
        /// Gets the password associated with the reporting service
        /// </summary>
        public string ApiPassword { get; }

        /// <summary>
        /// Gets the default max received message size for all calls to the TimeLog API.
        /// Default is 1024000, but can be overwritten from application setting TimeLogProjectMaxReceivedMessageSize.
        /// </summary>
        public long MaxReceivedMessageSize { get; }

        /// <summary>
        /// Gets the default timeout value for all calls to the TimeLog API.
        /// Default is 60 seconds, but can be overwritten from application setting TimeLogProjectTimeoutInSeconds.
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Gets the client associated with the reporting service
        /// </summary>
        public ServiceSoapClient Client
        {
            get
            {
                if (this._client == null)
                {
                    var _binding = new BasicHttpBinding
                                      {
                                          MaxReceivedMessageSize = MaxReceivedMessageSize,
                                          CloseTimeout = Timeout,
                                          OpenTimeout = Timeout,
                                          ReceiveTimeout = Timeout,
                                          SendTimeout = Timeout
                                      };

                    var _endpoint = new EndpointAddress(this.ServiceUrl);

                    if (this.ServiceUrl.Contains("https"))
                    {
                        _binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this._client = new ServiceSoapClient(_binding, _endpoint);
                }

                return this._client;
            }
        }

        /// <summary>
        /// Overwrites the service URL from the app.config
        /// </summary>
        /// <param name="url">TimeLog URL (e.g. https://app4.timelog.com/soxdemo4 )</param>
        public void OverwriteServiceUrl(string url)
        {
            this.ServiceUrl = url.Trim('/') + "/service.asmx";
        }

        /// <summary>
        /// Executes an authentication request to the API using the application settings
        /// TimeLogProjectReportingSiteCode, TimeLogProjectReportingUser and TimeLogProjectReportingPassword.
        /// </summary>
        /// <remarks>
        /// Make sure to set the application setting TimeLogProjectUri.
        /// </remarks>
        /// <returns>A value indicating whether the authentication is successful</returns>
        public bool TryAuthenticate()
        {
            if (string.IsNullOrWhiteSpace(this.SiteCode))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectReportingSiteCode\" is missing");
            }

            if (string.IsNullOrWhiteSpace(this.ApiId))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectReportingApiId\" is missing");
            }

            if (string.IsNullOrWhiteSpace(this.ApiPassword))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectReportingApiPassword\" is missing");
            }

            return this.TryAuthenticate(this.SiteCode, this.ApiId, this.ApiPassword);
        }

        /// <summary>
        /// Executes an authentication request to the API using the parameters.
        /// </summary>
        /// <param name="siteCode">Site code part of the credentials to authenticate with</param>
        /// <param name="user">User name part of the credentials to authenticate with</param>
        /// <param name="password">ApiPassword part of the credentials to authenticate with</param>
        /// <returns>A value indicating whether the authentication is successful</returns>
        public bool TryAuthenticate(string siteCode, string user, string password)
        {
            return this.Client.ValidateCredentials(siteCode, user, password);
        }

        /// <summary>
        /// Disposes the handler.
        /// </summary>
        public void Dispose()
        {
            this._client = null;
            _instance = null;
        }
    }
}