namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Configuration;
    using System.ServiceModel;

    using TimeLog.ReportingApi.SDK.ReportingService;

    public class ServiceHandler : IDisposable
    {
        private static ServiceHandler instance;
        private ServiceSoapClient client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceHandler"/> class from being created.
        /// </summary>
        private ServiceHandler()
        {
            SiteCode = ConfigurationManager.AppSettings["TimeLogProjectReportingSiteCode"];
            ApiId = ConfigurationManager.AppSettings["TimeLogProjectReportingApiId"];
            ApiPassword = ConfigurationManager.AppSettings["TimeLogProjectReportingApiPassword"];
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
        public string ServiceUrl
        {
            get
            {
                return SettingsHandler.Instance.Url + "service.asmx";
            }
        }

        /// <summary>
        /// Gets the sitecode associated with the reporting service
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
        /// Executes an authentication request to the API using the application settings
        /// TimeLogProjectReportingSiteCode, TimeLogProjectReportingUser and TimeLogProjectReportingPassword.
        /// </summary>
        /// <remarks>
        /// Make sure to set the application setting TimeLogProjectUri.
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
        /// Executes an authentication request to the API using the parameters.
        /// </summary>
        /// <param name="siteCode">Site code part of the credentials to authenticate with</param>
        /// <param name="user">Username part of the credentials to authenticate with</param>
        /// <param name="password">ApiPassword part of the credentials to authenticate with</param>
        /// <returns>A value indicating whether the authentication is successful</returns>
        public bool TryAuthenticate(string siteCode, string user, string password)
        {
            if (this.Client.ValidateCredentials(siteCode, user, password))
            {
                SiteCode = siteCode;
                ApiId = user;
                ApiPassword = password;
                return true;
            }

            SiteCode = string.Empty;
            ApiId = string.Empty;
            ApiPassword = string.Empty;
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