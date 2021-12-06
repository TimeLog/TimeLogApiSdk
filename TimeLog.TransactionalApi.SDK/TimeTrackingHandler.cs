using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using TimeLog.TransactionalAPI.SDK.TimeTrackingService;

namespace TimeLog.TransactionalAPI.SDK
{
    /// <summary>
    ///     Handler of functionality related to the time tracking service
    /// </summary>
    public class TimeTrackingHandler : IDisposable
    {
        private static TimeTrackingHandler _instance;

        private bool collectRawRequestResponse;
        private TimeTrackingServiceClient timeTrackingClient;

        /// <summary>
        ///     Prevents a default instance of the <see cref="TimeTrackingHandler" /> class from being created.
        /// </summary>
        private TimeTrackingHandler()
        {
            collectRawRequestResponse = false;
        }

        /// <summary>
        ///     Gets the singleton instance of the <see cref="TimeTrackingHandler" />.
        /// </summary>
        public static TimeTrackingHandler Instance => _instance ??= new TimeTrackingHandler();

        /// <summary>
        ///     Gets the uri associated with the time tracking service.
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
        ///     Gets the time tracking token for use in other methods. Makes use of SecurityHandler.Instance.Token.
        /// </summary>
        public SecurityToken Token =>
            new SecurityToken
            {
                Expires = SecurityHandler.Instance.Token.Expires,
                Hash = SecurityHandler.Instance.Token.Hash,
                Initials = SecurityHandler.Instance.Token.Initials
            };

        /// <summary>
        ///     Gets or sets a value indicating whether all raw XML requests should be stored in memory to allow saving them
        /// </summary>
        public bool CollectRawRequestResponse
        {
            get => collectRawRequestResponse;

            set
            {
                collectRawRequestResponse = value;
                timeTrackingClient = null;
            }
        }

        /// <summary>
        ///     Gets the active time tracking client
        /// </summary>
        public TimeTrackingServiceClient TimeTrackingClient
        {
            get
            {
                if (timeTrackingClient == null)
                {
                    var endpoint = new EndpointAddress(TimeTrackingServiceUrl);
                    if (CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement
                        {
                            MessageVersion = MessageVersion.Soap11
                        };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(TimeTrackingServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        timeTrackingClient = new TimeTrackingServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (TimeTrackingServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        timeTrackingClient = new TimeTrackingServiceClient(binding, endpoint);
                    }

                    timeTrackingClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return timeTrackingClient;
            }
        }

        void IDisposable.Dispose()
        {
            timeTrackingClient = null;
            _instance = null;
        }
    }
}