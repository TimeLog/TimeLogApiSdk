namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using SalaryService;
    using RawHelper;

    /// <summary>
    /// Handler of functionality related to the salary service
    /// </summary>
    public class SalaryHandler : IDisposable
    {
        private static SalaryHandler _instance;
        private SalaryServiceClient _salaryClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="SalaryHandler"/> class from being created.
        /// </summary>
        private SalaryHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="SalaryHandler"/>.
        /// </summary>
        public static SalaryHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new SalaryHandler());
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
                this._salaryClient = null;
            }
        }

        /// <summary>
        /// Gets the active salary client
        /// </summary>
        public SalaryServiceClient SalaryClient
        {
            get
            {
                if (this._salaryClient == null)
                {
                    var endpoint = new EndpointAddress(SalaryServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.SalaryServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._salaryClient = new SalaryServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.SalaryServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._salaryClient = new SalaryServiceClient(binding, endpoint);
                    }

                    this._salaryClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._salaryClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._salaryClient = null;
            _instance = null;
        }
    }
}