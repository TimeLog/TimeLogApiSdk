namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;

    using TimeLog.TransactionalApi.SDK.OrganisationService;

    public class OrganisationHandler : IDisposable
    {
        private static OrganisationHandler instance;
        private OrganisationServiceClient organisationClient;

        /// <summary>
        /// Prevents a default instance of the <see cref="OrganisationHandler"/> class from being created.
        /// </summary>
        private OrganisationHandler()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="OrganisationHandler"/>.
        /// </summary>
        public static OrganisationHandler Instance
        {
            get
            {
                return instance ?? (instance = new OrganisationHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the organisation service.
        /// </summary>
        public string OrganisationServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Organisation/V1_5/OrganisationServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Organisation/V1_5/OrganisationService.svc";
            }
        }

        /// <summary>
        /// Gets the organisation token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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

        public OrganisationServiceClient OrganisationClient
        {
            get
            {
                if (this.organisationClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(OrganisationServiceUrl);

                    if (OrganisationServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.organisationClient = new OrganisationServiceClient(binding, endpoint);
                }

                return this.organisationClient;
            }
        }

        public void Dispose()
        {
            this.organisationClient = null;
            instance = null;
        }
    }
}