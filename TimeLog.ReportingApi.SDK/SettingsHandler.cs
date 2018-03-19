namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// Allows access to application settings including default values.
    /// </summary>
    public class SettingsHandler : IDisposable
    {
        private static SettingsHandler _instance;

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
                return _instance ?? (_instance = new SettingsHandler());
            }
        }

        /// <summary>
        /// Gets the base uri for the TimeLog site read from the
        /// application setting TimeLogProjectUri.
        /// </summary>
        public string Url
        {
            get
            {
                var _url = ConfigurationManager.AppSettings["TimeLogProjectUri"];
                if (!_url.EndsWith("/"))
                {
                    _url += "/";
                }

                if (Uri.TryCreate(_url, UriKind.Absolute, out var _rootUri))
                {
                    if (_rootUri.ToString().Contains("http://") && !_rootUri.ToString().Contains("localhost"))
                    {
                        return _rootUri.ToString().Replace("http://", "https://");
                    }

                    return _rootUri.ToString();
                }

                throw new ArgumentException("The AppSetting \"TimeLogProjectUri\" is missing or invalid Uri");
            }
        }

        /// <summary>
        /// Gets the default max received message size for all calls to the TimeLog API.
        /// Default is 1024000, but can be overwritten from application setting TimeLogProjectMaxReceivedMessageSize.
        /// </summary>
        public long MaxReceivedMessageSize
        {
            get
            {
                if (long.TryParse(ConfigurationManager.AppSettings["TimeLogProjectMaxReceivedMessageSize"], out var _result))
                {
                    return _result;
                }

                return 4096000;
            }
        }

        /// <summary>
        /// Gets the default timeout value for all calls to the TimeLog API.
        /// Default is 60 seconds, but can be overwritten from applicatino setting TimeLogProjectTimeoutInSeconds.
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["TimeLogProjectTimeoutInSeconds"], out var _result))
                {
                    return TimeSpan.FromSeconds(_result);
                }

                return TimeSpan.FromSeconds(60);
            }
        }

        /// <summary>
        /// Gets the correct culture for converting data from the reporting API
        /// </summary>
        public CultureInfo DataCulture => new CultureInfo("en-US");

        /// <summary>
        /// Disposes the class and releases memory
        /// </summary>
        public void Dispose()
        {
            _instance = null;
        }
    }
}