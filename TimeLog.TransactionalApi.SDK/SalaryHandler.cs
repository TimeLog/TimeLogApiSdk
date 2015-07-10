namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;

    using TimeLog.TransactionalApi.SDK.SalaryService;

    public class SalaryHandler : IDisposable
    {
        private static SalaryHandler instance;
        private SalaryServiceClient salaryClient;

        /// <summary>
        /// Prevents a default instance of the <see cref="SalaryHandler"/> class from being created.
        /// </summary>
        private SalaryHandler()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="SalaryHandler"/>.
        /// </summary>
        public static SalaryHandler Instance
        {
            get
            {
                return instance ?? (instance = new SalaryHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the salary service.
        /// </summary>
        public string SalaryServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Salary/V1_0/SalaryServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Salary/V1_0/SalaryService.svc";
            }
        }

        /// <summary>
        /// Gets the salary token for use in other methods. Makes use of SecurityHandler.Instance.Token.
        /// </summary>
        public SecurityToken Token
        {
            get
            {
                return new SecurityToken
                       {
                           Expires = SecurityHandler.Instance.Token.Expires,
                           Hash = SecurityHandler.Instance.Token.Hash,
                           Initials = SecurityHandler.Instance.Token.Initials
                       };
            }
        }

        public SalaryServiceClient SalaryClient
        {
            get
            {
                if (this.salaryClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(SalaryServiceUrl);

                    if (SalaryServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.salaryClient = new SalaryServiceClient(binding, endpoint);
                }

                return this.salaryClient;
            }
        }

        public void Dispose()
        {
            this.salaryClient = null;
            instance = null;
        }
    }
}