using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TimeLog.TransactionalAPI.SDK.OrganisationService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.TransactionalAPI.SDK
{
    /// <summary>
    ///     Handler of functionality related to the organisation service
    /// </summary>
    public class OrganisationHandler : IDisposable
    {
        private static OrganisationHandler _instance;

        private bool collectRawRequestResponse;
        private OrganisationServiceClient organisationClient;

        /// <summary>
        ///     Prevents a default instance of the <see cref="OrganisationHandler" /> class from being created.
        /// </summary>
        private OrganisationHandler()
        {
            collectRawRequestResponse = false;
        }

        /// <summary>
        ///     Gets the singleton instance of the <see cref="OrganisationHandler" />.
        /// </summary>
        public static OrganisationHandler Instance => _instance ?? (_instance = new OrganisationHandler());

        /// <summary>
        ///     Gets the uri associated with the organisation service.
        /// </summary>
        public string OrganisationServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/Organisation/V1_7/OrganisationServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/Organisation/V1_7/OrganisationService.svc";
            }
        }

        /// <summary>
        ///     Gets the organisation token for use in other methods. Makes use of SecurityHandler.Instance.Token.
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
                organisationClient = null;
            }
        }

        /// <summary>
        ///     Gets the active organisation client
        /// </summary>
        public OrganisationServiceClient OrganisationClient
        {
            get
            {
                if (organisationClient == null)
                {
                    var endpoint = new EndpointAddress(OrganisationServiceUrl);
                    if (CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement {MessageVersion = MessageVersion.Soap11};
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(OrganisationServiceUrl.Contains("https")
                            ? SettingsHandler.Instance.StandardHttpsTransportBindingElement
                            : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        organisationClient = new OrganisationServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize
                        };

                        if (OrganisationServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        organisationClient = new OrganisationServiceClient(binding, endpoint);
                    }

                    organisationClient.InnerChannel.OperationTimeout = SettingsHandler.Instance.OperationTimeout;
                }

                return organisationClient;
            }
        }

        void IDisposable.Dispose()
        {
            organisationClient = null;
            _instance = null;
        }
    }
}