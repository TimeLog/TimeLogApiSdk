namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Configuration;
    using System.ServiceModel;

    using ReportingService;

    /// <summary>
    /// Handling security and connectivity for the reporting API
    /// </summary>
    public class ServiceHandler : IDisposable
    {
        private static ServiceHandler instance;
        private ServiceSoapClient client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceHandler"/> class from being created.
        /// </summary>
        private ServiceHandler()
        {
            this.SiteCode = ConfigurationManager.AppSettings["TimeLogProjectReportingSiteCode"];
            this.ApiId = ConfigurationManager.AppSettings["TimeLogProjectReportingApiId"];
            this.ApiPassword = ConfigurationManager.AppSettings["TimeLogProjectReportingApiPassword"];
            this.ServiceUrl = SettingsHandler.Instance.Url.Trim('/') + "/service.asmx";
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ServiceHandler"/>.
        /// </summary>
        public static ServiceHandler Instance
        {
            get
            {
                return instance ?? (instance = new ServiceHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the reporting service.
        /// </summary>
        public string ServiceUrl { get; private set; }

        /// <summary>
        /// Gets the site code associated with the reporting service
        /// </summary>
        public string SiteCode { get; private set; }

        /// <summary>
        /// Gets the user associated with the reporting service
        /// </summary>
        public string ApiId { get; private set; }

        /// <summary>
        /// Gets the password associated with the reporting service
        /// </summary>
        public string ApiPassword { get; private set; }

        /// <summary>
        /// Gets the client associated with the reporting service
        /// </summary>
        public ServiceSoapClient Client
        {
            get
            {
                if (this.client == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(this.ServiceUrl);

                    if (this.ServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.client = new ServiceSoapClient(binding, endpoint);
                }

                return this.client;
            }
        }

        /// <summary>
        /// Overwrites the service URL from the app.config
        /// </summary>
        /// <param name="url">TimeLog Project URL (e.g. https://app3.timelog.com/soxdemo )</param>
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
            if (this.Client.ValidateCredentials(siteCode, user, password))
            {
                this.SiteCode = siteCode;
                this.ApiId = user;
                this.ApiPassword = password;
                return true;
            }

            this.SiteCode = string.Empty;
            this.ApiId = string.Empty;
            this.ApiPassword = string.Empty;
            return false;
        }

        /// <summary>
        /// Disposes the handler.
        /// </summary>
        public void Dispose()
        {
            this.client = null;
            instance = null;
        }
    }
}