namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;

    using TimeLog.TransactionalApi.SDK.CrmService;

    public class CrmHandler : IDisposable
    {
        private static CrmHandler instance;
        private CRMServiceClient crmClient;

        /// <summary>
        /// Prevents a default instance of the <see cref="CrmHandler"/> class from being created.
        /// </summary>
        private CrmHandler()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="CrmHandler"/>.
        /// </summary>
        public static CrmHandler Instance
        {
            get
            {
                return instance ?? (instance = new CrmHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the CRM service.
        /// </summary>
        public string CrmServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/CRM/V1_2/CRMServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/CRM/V1_2/CRMService.svc";
            }
        }

        /// <summary>
        /// Gets the CRM token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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

        public CRMServiceClient CrmClient
        {
            get
            {
                if (this.crmClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(CrmServiceUrl);

                    if (CrmServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.crmClient = new CRMServiceClient(binding, endpoint);
                }

                return this.crmClient;
            }
        }

        public void Dispose()
        {
            this.crmClient = null;
            instance = null;
        }
    }
}