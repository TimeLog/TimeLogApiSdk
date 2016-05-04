namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using ExpenseService;
    using RawHelper;

    /// <summary>
    /// Handler of functionality related to the expense service
    /// </summary>
    public class ExpenseHandler : IDisposable
    {
        private static ExpenseHandler _instance;
        private ExpenseServiceClient _expenseClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="ExpenseHandler"/> class from being created.
        /// </summary>
        private ExpenseHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ExpenseHandler"/>.
        /// </summary>
        public static ExpenseHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new ExpenseHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the expense service.
        /// </summary>
        public string ExpenseServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Expenses/V1_0/ExpensesServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Expenses/V1_0/ExpensesService.svc";
            }
        }

        /// <summary>
        /// Gets the expense token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
                this._expenseClient = null;
            }
        }

        /// <summary>
        /// Gets the active expense client
        /// </summary>
        public ExpenseServiceClient ExpenseClient
        {
            get
            {
                if (this._expenseClient == null)
                {
                    var endpoint = new EndpointAddress(ExpenseServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.ExpenseServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._expenseClient = new ExpenseServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.ExpenseServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._expenseClient = new ExpenseServiceClient(binding, endpoint);
                    }

                    this._expenseClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._expenseClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._expenseClient = null;
            _instance = null;
        }
    }
}