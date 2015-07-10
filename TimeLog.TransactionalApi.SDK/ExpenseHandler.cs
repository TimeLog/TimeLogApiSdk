namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;

    using TimeLog.TransactionalApi.SDK.ExpenseService;

    public class ExpenseHandler : IDisposable
    {
        private static ExpenseHandler instance;
        private ExpenseServiceClient expenseClient;

        /// <summary>
        /// Prevents a default instance of the <see cref="ExpenseHandler"/> class from being created.
        /// </summary>
        private ExpenseHandler()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ExpenseHandler"/>.
        /// </summary>
        public static ExpenseHandler Instance
        {
            get
            {
                return instance ?? (instance = new ExpenseHandler());
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

        public ExpenseServiceClient ExpenseClient
        {
            get
            {
                if (this.expenseClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(ExpenseServiceUrl);

                    if (ExpenseServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.expenseClient = new ExpenseServiceClient(binding, endpoint);
                }

                return this.expenseClient;
            }
        }

        public void Dispose()
        {
            this.expenseClient = null;
            instance = null;
        }
    }
}