namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using RawHelper;

    using TimeLog.TransactionalApi.SDK.FinancialService;

    /// <summary>
    /// Handler of functionality related to the fiancial service
    /// </summary>
    public class FinancialHandler : IDisposable
    {
        private static FinancialHandler _instance;
        private FinancialServiceClient _financialClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="FinancialHandler"/> class from being created.
        /// </summary>
        private FinancialHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="FinancialHandler"/>.
        /// </summary>
        public static FinancialHandler Instance => _instance ?? (_instance = new FinancialHandler());

        /// <summary>
        /// Gets the uri associated with the Financial service.
        /// </summary>
        public string FinancialServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Financial/V1_2/FinancialServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Financial/V1_2/FinancialService.svc";
            }
        }

        /// <summary>
        /// Gets the financial token for use in other methods. Makes use of SecurityHandler.Instance.Token.
        /// </summary>
        public SecurityToken Token => new SecurityToken
                                          {
                                              Expires = SecurityHandler.Instance.Token.Expires,
                                              Hash = SecurityHandler.Instance.Token.Hash,
                                              Initials = SecurityHandler.Instance.Token.Initials
                                          };

        /// <summary>
        /// Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
        /// </summary>
        public bool CollectRawRequestResponse
        {
            get => this._collectRawRequestResponse;

            set
            {
                this._collectRawRequestResponse = value;
                this._financialClient = null;
            }
        }

        /// <summary>
        /// Gets the active Financial client
        /// </summary>
        public FinancialServiceClient FinancialClient
        {
            get
            {
                if (this._financialClient == null)
                {
                    var _endpoint = new EndpointAddress(this.FinancialServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var _binding = new CustomBinding();
                        var _encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        _binding.Elements.Add(_encoding);
                        _binding.Elements.Add(this.FinancialServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._financialClient = new FinancialServiceClient(_binding, _endpoint);
                    }
                    else
                    {
                        var _binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.FinancialServiceUrl.Contains("https"))
                        {
                            _binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._financialClient = new FinancialServiceClient(_binding, _endpoint);
                    }

                    this._financialClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._financialClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._financialClient = null;
            _instance = null;
        }
    }
}