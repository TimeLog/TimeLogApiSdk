namespace TimeLog.MicrosoftBiSync.Models
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Web;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    using TimeLog.Library.Configuration;

    /// <summary>
    /// The azure authentication helper.
    /// </summary>
    public class AzureAuthenticationHelper
    {
        /// <summary>
        /// Gets the authentication URL.
        /// </summary>
        private static string AuthenticationUrl
        {
            get
            {
                var @params = new NameValueCollection
                                  {
                                      // Azure AD will return an authorization code. 
                                      // See the Redirect class to see how "code" is used to AcquireTokenByAuthorizationCode
                                      { "response_type", "code" },

                                      // Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
                                      // You get the client id when you register your Azure app.
                                      {
                                          "client_id",
                                          PersonalConfigurationManager.AppSettings["AzureClientId"]
                                      },

                                      // Resource uri to the Power BI resource to be authorized
                                      { "resource", "https://analysis.windows.net/powerbi/api" },

                                      // After user authenticates, Azure AD will redirect back to the web app
                                      { "redirect_uri", PersonalConfigurationManager.AppSettings["AzureRedirectUrl"] }
                                  };

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString.Add(@params);

                return string.Format("{0}?{1}", PersonalConfigurationManager.AppSettings["AzureAuthorityUri"], queryString);
            }
        }

        public static void SetReportingCredentials(ReportingCredentials credentials)
        {
            HttpContext.Current.Session["reportingCredentials"] = credentials;
        }

        public static ReportingCredentials GetReportingCredentials()
        {
            var session = HttpContext.Current.Session["reportingCredentials"];
            if (session == null)
            {
                return null;
            }

            return (ReportingCredentials)session;
        }

        public static void SetSession(AuthenticationResult value)
        {
            HttpContext.Current.Session["authResult"] = value;
        }

        public static bool IsAuthenticated()
        {
            return GetSession() != null;
        }

        public static AuthenticationResult GetSession()
        {
            var session = HttpContext.Current.Session["authResult"];
            if (session == null)
            {
                return null;
            }

            return (AuthenticationResult)session;
        }

        /// <summary>
        /// Gets the authentication URL for O365.
        /// </summary>
        /// <returns>A authentication URL for O365</returns>
        public static string GetAuthString()
        {
            return AuthenticationUrl;
        }
    }
}