
namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using HelpDeskService;
    using RawHelper;

    /// <summary>
    /// Handler of functionality related to the HelpDesk service
    /// </summary>
    public class HelpDeskHandler : IDisposable
    {
        private static HelpDeskHandler _instance;
        private HelpDeskServiceClient _helpDeskClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="HelpDeskHandler"/> class from being created.
        /// </summary>
        private HelpDeskHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="HelpDeskHandler"/>.
        /// </summary>
        public static HelpDeskHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new HelpDeskHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the HelpDesk service.
        /// </summary>
        public string HelpDeskServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/HelpDesk/V1_6/HelpDeskServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/HelpDesk/V1_6/HelpDeskService.svc";
            }
        }

        /// <summary>
        /// Gets the HelpDesk token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
                this._helpDeskClient = null;
            }
        }

        /// <summary>
        /// Gets the active HelpDesk client
        /// </summary>
        public HelpDeskServiceClient HelpDeskClient
        {
            get
            {
                if (this._helpDeskClient == null)
                {
                    var endpoint = new EndpointAddress(HelpDeskServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.HelpDeskServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._helpDeskClient = new HelpDeskServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.HelpDeskServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._helpDeskClient = new HelpDeskServiceClient(binding, endpoint);
                    }

                    this._helpDeskClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._helpDeskClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._helpDeskClient = null;
            _instance = null;
        }
    }
}