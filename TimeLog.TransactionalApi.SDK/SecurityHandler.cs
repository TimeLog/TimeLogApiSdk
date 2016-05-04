namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using RawHelper;
    using SecurityService;

    /// <summary>
    /// Security handler class for transactional API calls
    /// </summary>
    public class SecurityHandler : IDisposable
    {
        private static SecurityHandler _instance;
        private readonly Dictionary<string, SecurityToken> _cachedTokens;
        private SecurityServiceClient _securityClient;
        private SecurityToken _token;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="SecurityHandler"/> class from being created.
        /// </summary>
        private SecurityHandler()
        {
            this._cachedTokens = new Dictionary<string, SecurityToken>();
            this.CollectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="SecurityHandler"/>.
        /// </summary>
        public static SecurityHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new SecurityHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the security service.
        /// </summary>
        public string SecurityServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Security/V1_2/SecurityServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Security/V1_2/SecurityService.svc";
            }
        }

        /// <summary>
        /// Gets the client associated with the security service
        /// </summary>
        public SecurityServiceClient SecurityClient
        {
            get
            {
                if (this._securityClient == null)
                {
                    var endpoint = new EndpointAddress(this.SecurityServiceUrl);

                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.SecurityServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._securityClient = new SecurityServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.SecurityServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._securityClient = new SecurityServiceClient(binding, endpoint);
                    }

                    this._securityClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._securityClient;
            }
        }

        /// <summary>
        /// Gets the security token for use in other methods. Make sure to call TryAuthenticate 
        /// before trying to access this property.
        /// </summary>
        public SecurityToken Token
        {
            get
            {
                if (this._token == null)
                {
                    throw new Exception("Please authenticate using the \"SecurityHandler.Instance.TryAuthenticate\" method before use");
                }

                return this._token;
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
                this._securityClient = null;
                this._token = null;
            }
        }

        /// <summary>
        /// Executes an authentication request to the API using the application settings
        /// TimeLogProjectTransactionalUsername and TimeLogProjectTransactionalPassword. If successful the token is
        /// set to the Token property.
        /// </summary>
        /// <remarks>Make sure to set the application setting TimeLogProjectUri</remarks>
        /// <param name="messages">Outputs the messages returned from the API</param>
        /// <returns>A value indicating whether the authentication is successful</returns>
        public bool TryAuthenticate(out IEnumerable<string> messages)
        {
            string username = ConfigurationManager.AppSettings["TimeLogProjectTransactionalUsername"];
            string password = ConfigurationManager.AppSettings["TimeLogProjectTransactionalPassword"];

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectTransactionalUsername\" is missing");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectTransactionalPassword\" is missing");
            }

            return this.TryAuthenticate(username, password, out messages);
        }

        /// <summary>
        /// Executes an authentication request to the API using the parameters. If successful
        /// the token is set to the Token property.
        /// </summary>
        /// <param name="username">Username part of the credentials to authenticate with</param>
        /// <param name="password">Password part of the credentials to authenticate with</param>
        /// <param name="messages">Outputs the messages returned from the API</param>
        /// <returns>A value indicating whether the authentication is successful</returns>
        public bool TryAuthenticate(string username, string password, out IEnumerable<string> messages)
        {
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["TimeLogProjectRawTokenHash"]))
            {
                this._token = new SecurityToken
                                 {
                                     Expires = DateTime.ParseExact(ConfigurationManager.AppSettings["TimeLogProjectRawTokenExpires"], "yyyyMMddHHmmssK", new CultureInfo("da-DK")),
                                     Hash = ConfigurationManager.AppSettings["TimeLogProjectRawTokenHash"],
                                     Initials = ConfigurationManager.AppSettings["TimeLogProjectRawTokenInitials"]
                                 };

                messages = new List<string>();
                return true;
            }

            // Reuse the token if we already have it in the cache
            if (this._cachedTokens.ContainsKey(username))
            {
                this._token = this._cachedTokens[username];

                // Check if the token has expired - leave a minute to other code to run
                if (this._token.Expires > DateTime.Now.AddMinutes(1))
                {
                    messages = new List<string>();
                    return true;
                }
                
                this._cachedTokens.Remove(username);
            }

            var tokenResponse = this.SecurityClient.GetToken(username, password);
            if (tokenResponse.ResponseState == ExecutionStatus.Success &&
                tokenResponse.Return.Any())
            {
                this._token = tokenResponse.Return[0];

                this._cachedTokens.Add(username, this._token);

                messages = new List<string>();
                return true;
            }

            messages = tokenResponse.Messages.Select(m => string.Concat(m.ErrorCode, " ", m.Message));
            return false;
        }

        /// <summary>
        /// Disposes the handler.
        /// </summary>
        public void Dispose()
        {
            this._securityClient = null;
            this._token = null;
            _instance = null;
        }
    }
}