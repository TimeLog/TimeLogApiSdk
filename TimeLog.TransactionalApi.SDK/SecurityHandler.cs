using System;

namespace TimeLog.TransactionalApi.SDK
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.ServiceModel;

    public class SecurityHandler : IDisposable
    {
        private static SecurityHandler instance;
        private SecurityService.SecurityServiceClient securityClient;

        private SecurityService.SecurityToken token;

        /// <summary>
        /// Prevents a default instance of the <see cref="SecurityHandler"/> class from being created.
        /// </summary>
        private SecurityHandler()
        {
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
                if (securityClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(SecurityServiceUrl);

                    if (SecurityServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    securityClient = new SecurityService.SecurityServiceClient(binding, endpoint);
                }

                return securityClient;
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
        /// Executes an authentication request to the API using the application settings
        /// TimeLogProjectUsername and TimeLogProjectPassword. If successful the token is
        /// set to the Token property.
        /// </summary>
        /// <remarks>
        /// Make sure to set the application setting TimeLogProjectUri.
        /// </remarks>
        /// <param name="messages">Outputs the messages returned from the API</param>
        /// <returns>A value indicating whether the authentication is succesful</returns>
        public bool TryAuthenticate(out IEnumerable<string> messages)
        {
            string username = ConfigurationManager.AppSettings["TimeLogProjectUsername"];
            string password = ConfigurationManager.AppSettings["TimeLogProjectPassword"];

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectUsername\" is missing");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The AppSetting \"TimeLogProjectPassword\" is missing");
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
            var tokenResponse = this.SecurityClient.GetToken(username, password);
            if (tokenResponse.ResponseState == SecurityService.ExecutionStatus.Success &&
                tokenResponse.Return.Any())
            {
                this.token = tokenResponse.Return[0];
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