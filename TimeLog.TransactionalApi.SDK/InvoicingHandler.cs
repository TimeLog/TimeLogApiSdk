namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using InvoicingService;
    using RawHelper;

    /// <summary>
    /// Handler of functionality related to the invoicing service
    /// </summary>
    public class InvoicingHandler : IDisposable
    {
        private static InvoicingHandler _instance;
        private InvoicingServiceClient _invoicingClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="InvoicingHandler"/> class from being created.
        /// </summary>
        private InvoicingHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="InvoicingHandler"/>.
        /// </summary>
        public static InvoicingHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new InvoicingHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the invoicing service.
        /// </summary>
        public string InvoicingServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Invoicing/V1_0/InvoicingServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Invoicing/V1_0/InvoicingService.svc";
            }
        }

        /// <summary>
        /// Gets the invoicing token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
                this._invoicingClient = null;
            }
        }

        /// <summary>
        /// Gets the active invoicing client
        /// </summary>
        public InvoicingServiceClient InvoicingClient
        {
            get
            {
                if (this._invoicingClient == null)
                {
                    var endpoint = new EndpointAddress(this.InvoicingServiceUrl);

                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.InvoicingServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._invoicingClient = new InvoicingServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.InvoicingServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._invoicingClient = new InvoicingServiceClient(binding, endpoint);
                    }
                }

                this._invoicingClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                return this._invoicingClient;
            }
        }

        public void Dispose()
        {
            this._invoicingClient = null;
            _instance = null;
        }
    }
}