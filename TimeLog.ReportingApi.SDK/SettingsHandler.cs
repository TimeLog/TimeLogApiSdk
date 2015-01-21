namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Configuration;

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

        public void Dispose()
        {
            instance = null;
        }
    }
}
