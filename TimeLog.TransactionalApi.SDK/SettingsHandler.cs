namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    /// <summary>
    /// Handler for settings in the API connection
    /// </summary>
    public class SettingsHandler : IDisposable
    {
        private static SettingsHandler instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsHandler"/> class from being created.
        /// </summary>
        private SettingsHandler()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the SettingsHandler.
        /// </summary>
        public static SettingsHandler Instance
        {
            get
            {
                return instance ?? (instance = new SettingsHandler());
            }
        }

        /// <summary>
        /// Gets the base uri for the TimeLog Project site read from the
        /// application setting TimeLogProjectUri.
        /// </summary>
        public string Url
        {
            get
            {
                var url = ConfigurationManager.AppSettings["TimeLogProjectUri"];
                if (!url.EndsWith("/"))
                {
                    url += "/";
                }

                Uri rootUri;
                if (Uri.TryCreate(url, UriKind.Absolute, out rootUri))
                {
                    if (rootUri.ToString().Contains("http://") && !rootUri.ToString().Contains("localhost"))
                    {
                        return rootUri.ToString().Replace("http://", "https://");
                    }

                    return rootUri.ToString();
                }

                throw new ArgumentException("The AppSetting \"TimeLogProjectUri\" is missing or invalid Uri");
            }
        }

        /// <summary>
        /// Gets the default max received message size for all calls to the TimeLog Project API.
        /// Default is 1024000, but can be overwritten from application setting TimeLogProjectMaxReceivedMessageSize.
        /// </summary>
        public long MaxReceivedMessageSize
        {
            get
            {
                long result;
                if (long.TryParse(ConfigurationManager.AppSettings["TimeLogProjectMaxReceivedMessageSize"], out result))
                {
                    return result;
                }

                return 1024000;
            }
        }

        /// <summary>
        /// Gets the default operation timeout for all calls to the TimeLog Project API.
        /// Default is 60 seconds, but can be overwritten from application setting TimeLogProjectOperationTimeoutSeconds.
        /// </summary>
        public TimeSpan OperationTimeout
        {
            get
            {
                int result;
                if (int.TryParse(ConfigurationManager.AppSettings["TimeLogProjectOperationTimeoutSeconds"], out result))
                {
                    return TimeSpan.FromSeconds(result);
                }

                return TimeSpan.FromSeconds(60);
            }
        }

        /// <summary>
        /// Gets a standard http binding
        /// </summary>
        public HttpTransportBindingElement StandardHttpTransportBindingElement
        {
            get
            {
                return new HttpTransportBindingElement
                {
                    AllowCookies = false,
                    HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                    BypassProxyOnLocal = false,
                    MaxBufferSize = (int)this.MaxReceivedMessageSize,
                    MaxBufferPoolSize = this.MaxReceivedMessageSize,
                    MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                    TransferMode = TransferMode.Buffered,
                    UseDefaultWebProxy = true,
                    AuthenticationScheme = AuthenticationSchemes.Anonymous
                };
            }
        }

        /// <summary>
        /// Gets a standard https binding
        /// </summary>
        public HttpsTransportBindingElement StandardHttpsTransportBindingElement
        {
            get
            {
                return new HttpsTransportBindingElement
                {
                    AllowCookies = false,
                    HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                    BypassProxyOnLocal = false,
                    MaxBufferSize = (int)this.MaxReceivedMessageSize,
                    MaxBufferPoolSize = this.MaxReceivedMessageSize,
                    MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                    TransferMode = TransferMode.Buffered,
                    UseDefaultWebProxy = true,
                    AuthenticationScheme = AuthenticationSchemes.Anonymous
                };
            }
        }

        public void Dispose()
        {
            instance = null;
        }
    }
}
