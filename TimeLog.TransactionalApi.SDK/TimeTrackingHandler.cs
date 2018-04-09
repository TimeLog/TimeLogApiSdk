namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using RawHelper;

    using TimeLog.TransactionalApi.SDK.TimeTrackingService;

    using SecurityToken = TimeTrackingService.SecurityToken;

    /// <summary>
    /// Handler of functionality related to the time tracking service
    /// </summary>
    public class TimeTrackingHandler : IDisposable
    {
        private static TimeTrackingHandler _instance;
        private TimeTrackingServiceClient _timeTrackingClient;

        private bool _collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="TimeTrackingHandler"/> class from being created.
        /// </summary>
        private TimeTrackingHandler()
        {
            this._collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="TimeTrackingHandler"/>.
        /// </summary>
        public static TimeTrackingHandler Instance => _instance ?? (_instance = new TimeTrackingHandler());

        /// <summary>
        /// Gets the uri associated with the time tracking service.
        /// </summary>
        public string TimeTrackingServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Registration/V1_0/TimeTrackingServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Registration/V1_0/TimeTrackingService.svc";
            }
        }

        /// <summary>
        /// Gets the time tracking token for use in other methods. Makes use of SecurityHandler.Instance.Token.
        /// </summary>
        public SecurityToken Token =>
            new SecurityToken
                {
                    Expires = SecurityHandler.Instance.Token.Expires,
                    Hash = SecurityHandler.Instance.Token.Hash,
                    Initials = SecurityHandler.Instance.Token.Initials
                };

        /// <summary>
        /// Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
        /// </summary>
        public bool CollectRawRequestResponse
        {
            get => this._collectRawRequestResponse;

            set
            {
                this._collectRawRequestResponse = value;
                this._timeTrackingClient = null;
            }
        }

        /// <summary>
        /// Gets the active time tracking client
        /// </summary>
        public TimeTrackingServiceClient TimeTrackingClient
        {
            get
            {
                if (this._timeTrackingClient == null)
                {
                    var _endpoint = new EndpointAddress(this.TimeTrackingServiceUrl);
                    if (this.CollectRawRequestResponse)
                    {
                        var _binding = new CustomBinding();
                        var _encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        _binding.Elements.Add(_encoding);
                        _binding.Elements.Add(this.TimeTrackingServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this._timeTrackingClient = new TimeTrackingServiceClient(_binding, _endpoint);
                    }
                    else
                    {
                        var _binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (this.TimeTrackingServiceUrl.Contains("https"))
                        {
                            _binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this._timeTrackingClient = new TimeTrackingServiceClient(_binding, _endpoint);
                    }

                    this._timeTrackingClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return this._timeTrackingClient;
            }
        }

        void IDisposable.Dispose()
        {
            this._timeTrackingClient = null;
            _instance = null;
        }
    }
}