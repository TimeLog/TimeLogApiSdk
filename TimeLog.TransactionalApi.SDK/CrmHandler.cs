namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using TimeLog.TransactionalApi.SDK.CrmService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Handler of functionality related to the CRM service
    /// </summary>
    public class CrmHandler : IDisposable
    {
        private static CrmHandler instance;
        private CRMServiceClient crmClient;

        private bool collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="CrmHandler"/> class from being created.
        /// </summary>
        private CrmHandler()
        {
            this.collectRawRequestResponse = false;
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

        /// <summary>
        /// Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
        /// </summary>
        public bool CollectRawRequestResponse
        {
            get
            {
                return this.collectRawRequestResponse;
            }

            set
            {
                this.collectRawRequestResponse = value;
                this.crmClient = null;
            }
        }

        /// <summary>
        /// Gets the active CRM client
        /// </summary>
        public CRMServiceClient CrmClient
        {
            get
            {
                if (this.crmClient == null)
                {
                    var endpoint = new EndpointAddress(this.CrmServiceUrl);

                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.CrmServiceUrl.Contains("https") ? SettingsHandler.Instance.StandardHttpsTransportBindingElement : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this.crmClient = new CRMServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                        if (this.CrmServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this.crmClient = new CRMServiceClient(binding, endpoint);
                    }
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