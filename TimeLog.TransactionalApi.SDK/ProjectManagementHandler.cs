namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    /// <summary>
    /// Handler of functionality related to the project management service
    /// </summary>
    public class ProjectManagementHandler : IDisposable
    {
        private static ProjectManagementHandler instance;
        private ProjectManagementServiceClient projectManagementClient;

        private bool collectRawRequestResponse;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectManagementHandler"/> class from being created.
        /// </summary>
        private ProjectManagementHandler()
        {
            this.collectRawRequestResponse = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ProjectManagementHandler"/>.
        /// </summary>
        public static ProjectManagementHandler Instance
        {
            get
            {
                return instance ?? (instance = new ProjectManagementHandler());
            }
        }

        /// <summary>
        /// Gets the uri associated with the project management service.
        /// </summary>
        public string ProjectManagementServiceUrl
        {
            get
            {
                if (SettingsHandler.Instance.Url.Contains("https"))
                {
                    return SettingsHandler.Instance.Url + "WebServices/ProjectManagement/V1_6/ProjectManagementServiceSecure.svc";
                }

                return SettingsHandler.Instance.Url + "WebServices/ProjectManagement/V1_6/ProjectManagementService.svc";
            }
        }

        /// <summary>
        /// Gets the project management token for use in other methods. Makes use of SecurityHandler.Instance.Token.
        /// </summary>
        public SecurityToken Token
        {
            get
            {
                return new SecurityToken
                       {
                           Expires = SecurityHandler.Instance.Token.Expires,
                           Hash = SecurityHandler.Instance.Token.Hash,
                           Initials = SecurityHandler.Instance.Token.Initials
                       };
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
                this.projectManagementClient = null;
            }
        }

        /// <summary>
        /// Gets the active project management client
        /// </summary>
        public ProjectManagementServiceClient ProjectManagementClient
        {
            get
            {
                if (this.projectManagementClient == null)
                {
                    var endpoint = new EndpointAddress(this.ProjectManagementServiceUrl);

                    if (this.CollectRawRequestResponse)
                    {
                        var binding = new CustomBinding();
                        var encoding = new RawMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11 };
                        binding.Elements.Add(encoding);
                        binding.Elements.Add(this.ProjectManagementServiceUrl.Contains("https") ? SettingsHandler.Instance.StandardHttpsTransportBindingElement : SettingsHandler.Instance.StandardHttpTransportBindingElement);
                        this.projectManagementClient = new ProjectManagementServiceClient(binding, endpoint);
                    }
                    else
                    {
                        var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                        if (this.ProjectManagementServiceUrl.Contains("https"))
                        {
                            binding.Security.Mode = BasicHttpSecurityMode.Transport;
                        }

                        this.projectManagementClient = new ProjectManagementServiceClient(binding, endpoint);
                    }
                }

                return this.projectManagementClient;
            }
        }

        public void Dispose()
        {
            this.projectManagementClient = null;
            instance = null;
        }
    }
}