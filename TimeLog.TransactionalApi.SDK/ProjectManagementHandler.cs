namespace TimeLog.TransactionalApi.SDK
{
    using System;
    using System.ServiceModel;

    using TimeLog.TransactionalApi.SDK.ProjectManagementService;

    public class ProjectManagementHandler : IDisposable
    {
        private static ProjectManagementHandler instance;
        private ProjectManagementServiceClient projectManagementClient;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectManagementHandler"/> class from being created.
        /// </summary>
        private ProjectManagementHandler()
        {
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

        public ProjectManagementServiceClient ProjectManagementClient
        {
            get
            {
                if (this.projectManagementClient == null)
                {
                    var binding = new BasicHttpBinding { MaxReceivedMessageSize = SettingsHandler.Instance.MaxReceivedMessageSize };
                    var endpoint = new EndpointAddress(ProjectManagementServiceUrl);

                    if (ProjectManagementServiceUrl.Contains("https"))
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    this.projectManagementClient = new ProjectManagementServiceClient(binding, endpoint);
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