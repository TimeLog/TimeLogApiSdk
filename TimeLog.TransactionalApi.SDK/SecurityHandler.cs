namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using TimeLog.TransactionalApi.SDK.RawHelper;
    using TimeLog.TransactionalApi.SDK.SecurityService;

    /// <summary>
    /// Security handler class for transactional API calls
    /// </summary>
    public class SecurityHandler : IDisposable
    {
        private static SecurityHandler instance;
        private readonly Dictionary<string, SecurityService.SecurityToken> cachedTokens;
        private SecurityService.SecurityServiceClient securityClient;
        private SecurityService.SecurityToken token;

        private bool collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="SecurityHandler"/> class from being created.
        /// </summary>
        private SecurityHandler()
        {
            this.cachedTokens = new Dictionary<string, SecurityService.SecurityToken>();
            this.CollectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="SecurityHandler"/>.
        /// </summary>
        public static SecurityHandler Instance
        {
            get
            {
                return instance ?? (instance = new SecurityHandler());
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
        public SecurityService.SecurityServiceClient SecurityClient
        {
            get
            {
                if (this.securityClient == null)
                {
                    var endpoint = new EndpointAddress(this.SecurityServiceUrl);

                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.SecurityServiceUrl.Contains("https") ? SettingsHandler.Instance.StandardHttpsTransportBindingElement : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this.securityClient = new SecurityService.SecurityServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                        if (this.SecurityServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this.securityClient = new SecurityService.SecurityServiceClient(binding, endpoint);                        
                    }
                }

                return this.securityClient;
            }
        }

        /// <summary>
        /// Gets the security token for use in other methods. Make sure to call TryAuthenticate 
        /// before trying to access this property.
        /// </summary>
        public SecurityService.SecurityToken Token
        {
            get
            {
                if (this.token == null)
                {
                    throw new Exception("Please authenticate using the \"SecurityHandler.Instance.TryAuthenticate\" method before use");
                }

                return this.token;
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
                this.securityClient = null;
                this.token = null;
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
                this.token = new SecurityToken
                                 {
                                     Expires = DateTime.ParseExact(ConfigurationManager.AppSettings["TimeLogProjectRawTokenExpires"], "yyyyMMddHHmmssK", new CultureInfo("da-DK")),
                                     Hash = ConfigurationManager.AppSettings["TimeLogProjectRawTokenHash"],
                                     Initials = ConfigurationManager.AppSettings["TimeLogProjectRawTokenInitials"]
                                 };

                messages = new List<string>();
                return true;
            }

            // Reuse the token if we already have it in the cache
            if (this.cachedTokens.ContainsKey(username))
            {
                this.token = this.cachedTokens[username];

                // Check if the token has expired - leave a minute to other code to run
                if (this.token.Expires > DateTime.Now.AddMinutes(1))
                {
                    messages = new List<string>();
                    return true;
                }
                
                this.cachedTokens.Remove(username);
            }

            var tokenResponse = this.SecurityClient.GetToken(username, password);
            if (tokenResponse.ResponseState == SecurityService.ExecutionStatus.Success &&
                tokenResponse.Return.Any())
            {
                this.token = tokenResponse.Return[0];

                this.cachedTokens.Add(username, this.token);

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
            this.securityClient = null;
            this.token = null;
            instance = null;
        }
    }
}