using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace TimeLog.TransactionalAPI.SDK
{
    /// <summary>
    ///     Handler for settings in the API connection
    /// </summary>
    public class SettingsHandler : IDisposable
    {
        private static SettingsHandler _instance;

        /// <summary>
        ///     Prevents a default instance of the <see cref="SettingsHandler" /> class from being created.
        /// </summary>
        private SettingsHandler()
        {
        }

        /// <summary>
        ///     Gets the singleton instance of the SettingsHandler.
        /// </summary>
        public static SettingsHandler Instance => _instance ?? (_instance = new SettingsHandler());

        /// <summary>
        ///     Gets the base uri for the TimeLog site read from the
        ///     application setting TimeLogProjectUri.
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

                if (Uri.TryCreate(url, UriKind.Absolute, out var rootUri))
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
        ///     Gets the default max received message size for all calls to the TimeLog API.
        ///     Default is 1024000, but can be overwritten from application setting TimeLogProjectMaxReceivedMessageSize.
        /// </summary>
        public long MaxReceivedMessageSize
        {
            get
            {
                if (long.TryParse(ConfigurationManager.AppSettings["TimeLogProjectMaxReceivedMessageSize"],
                        out var result))
                {
                    return result;
                }

                return 1024000;
            }
        }

        /// <summary>
        ///     Gets the default operation timeout for all calls to the TimeLog API.
        ///     Default is 60 seconds, but can be overwritten from application setting TimeLogProjectOperationTimeoutSeconds.
        /// </summary>
        public TimeSpan OperationTimeout
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["TimeLogProjectOperationTimeoutSeconds"],
                        out var result))
                {
                    return TimeSpan.FromSeconds(result);
                }

                return TimeSpan.FromSeconds(60);
            }
        }

        /// <summary>
        ///     Gets a standard http binding
        /// </summary>
        public HttpTransportBindingElement StandardHttpTransportBindingElement =>
            new HttpTransportBindingElement
            {
                AllowCookies = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                BypassProxyOnLocal = false,
                MaxBufferSize = (int) MaxReceivedMessageSize,
                MaxBufferPoolSize = MaxReceivedMessageSize,
                MaxReceivedMessageSize = MaxReceivedMessageSize,
                TransferMode = TransferMode.Buffered,
                UseDefaultWebProxy = true,
                AuthenticationScheme = AuthenticationSchemes.Anonymous
            };

        /// <summary>
        ///     Gets a standard https binding
        /// </summary>
        public HttpsTransportBindingElement StandardHttpsTransportBindingElement =>
            new HttpsTransportBindingElement
            {
                AllowCookies = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                BypassProxyOnLocal = false,
                MaxBufferSize = (int) MaxReceivedMessageSize,
                MaxBufferPoolSize = MaxReceivedMessageSize,
                MaxReceivedMessageSize = MaxReceivedMessageSize,
                TransferMode = TransferMode.Buffered,
                UseDefaultWebProxy = true,
                AuthenticationScheme = AuthenticationSchemes.Anonymous
            };

        public void Dispose()
        {
            _instance = null;
        }
    }
}