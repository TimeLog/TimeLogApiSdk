namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using OrganisationService;
    using RawHelper;

    /// <summary>
    /// Handler of functionality related to the organisation service
    /// </summary>
    public class OrganisationHandler : IDisposable
    {
        private static OrganisationHandler _instance;
        private OrganisationServiceClient _organisationClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="OrganisationHandler"/> class from being created.
        /// </summary>
        private OrganisationHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="OrganisationHandler"/>.
        /// </summary>
        public static OrganisationHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new OrganisationHandler());
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

        /// <summary>
        /// Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
        /// </summary>
        public bool CollectRawRequestResponse
        {
            get
            {
                return this._collectRawRequestResponse;
            }

            set
            {
                this._collectRawRequestResponse = value;
                this._organisationClient = null;
            }
        }

        /// <summary>
        /// Gets the active organisation client
        /// </summary>
        public OrganisationServiceClient OrganisationClient
        {
            get
            {
                if (this._organisationClient == null)
                {
                    var endpoint = new EndpointAddress(OrganisationServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.OrganisationServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._organisationClient = new OrganisationServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.OrganisationServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._organisationClient = new OrganisationServiceClient(binding, endpoint);
                    }

                    this._organisationClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._organisationClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._organisationClient = null;
            _instance = null;
        }
    }
}