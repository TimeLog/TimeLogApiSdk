using System;

namespace TimeLog.TransactionalApi.SDK
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.ServiceModel;

    /// <summary>
    /// Security handler class for transactional API calls
    /// </summary>
    public class SecurityHandler : IDisposable
    {
        private static SecurityHandler _instance;
        
        private SecurityService.SecurityServiceClient _securityClient;

        private SecurityService.SecurityToken _token;

        private readonly Dictionary<string, SecurityService.SecurityToken> _cachedTokens;

        /// <summary>
        /// Prevents a default instance of the <see cref="SecurityHandler"/> class from being created.
        /// </summary>
        private SecurityHandler()
        {
            _cachedTokens = new Dictionary<string, SecurityService.SecurityToken>();
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
        public SecurityService.SecurityServiceClient SecurityClient
        {
            get
            {
                if (_securityClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(SecurityServiceUrl);

                    if (SecurityServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    _securityClient = new SecurityService.SecurityServiceClient(binding, endpoint);
                }

                return _securityClient;
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
                if (_token == null)
                {
                    throw new Exception("Please authenticate using the \"SecurityHandler.Instance.TryAuthenticate\" method before use");
                }

                return _token;
            }
        }

        /// <summary>
        /// Executes an authentication request to the API using the application settings
        /// TimeLogProjectTransactionalUsername and TimeLogProjectTransactionalPassword. If successful the token is
        /// set to the Token property.
        /// </summary>
        /// <remarks>
        /// Make sure to set the application setting TimeLogProjectUri.
        /// </remarks>
        /// <param name="messages">Outputs the messages returned from the API</param>
        /// <returns>A value indicating whether the authentication is succesful</returns>
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

            return TryAuthenticate(username, password, out messages);
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
            // Reuse the token if we already have it in the cache
            if (_cachedTokens.ContainsKey(username))
            {
                _token = _cachedTokens[username];

                // Check if the token has expired - leave a minute to other code to run
                if (_token.Expires > DateTime.Now.AddMinutes(1))
                {
                    messages = new List<string>();
                    return true;
                }
                
                _cachedTokens.Remove(username);
            }

            var tokenResponse = SecurityClient.GetToken(username, password);
            if (tokenResponse.ResponseState == SecurityService.ExecutionStatus.Success &&
                tokenResponse.Return.Any())
            {
                _token = tokenResponse.Return[0];

                _cachedTokens.Add(username, _token);

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
            _securityClient = null;
            _token = null;
            _instance = null;
        }
    }
}