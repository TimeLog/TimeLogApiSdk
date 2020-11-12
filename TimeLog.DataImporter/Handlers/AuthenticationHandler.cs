using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace TimeLog.DataImporter.Handlers
{
    public class AuthenticationHandler
    {
        private OidcClient _oidcClient;
        public string _token = string.Empty;


        private static AuthenticationHandler _instance;

        private AuthenticationHandler()
        {
            var options = new OidcClientOptions
            {
                Authority = "http://10.50.6.61/tlplogin",

                ClientId = "postman",
                ClientSecret = "postman",
                Scope = "openid profile tlp",
                RedirectUri = "com.timelog:/oauthredirect",

                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,


                Policy = new Policy { Discovery = new DiscoveryPolicy { RequireHttps = false } },
                Browser = new WinFormsWebView()
            };

            //var options = new OidcClientOptions
            //{
            //    Authority = "https://newlogin.timelog.com",
            //    ClientId = "mobile",
            //    ClientSecret = "timelog4life",
            //    Scope = "openid profile tlp",
            //    RedirectUri = "com.timelog:/oauthredirect",

            //    ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
            //    Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,


            //    Policy = new Policy {Discovery = new DiscoveryPolicy {RequireHttps = false}},
            //    Browser = new WinFormsWebView()
            //};


            _oidcClient = new OidcClient(options);
        }
        public static AuthenticationHandler Instance
        {
            get
            {
                return _instance ??= new AuthenticationHandler();
            }
        }

        public async void Authenticate()
        {
            var result = await _oidcClient.LoginAsync();
            _token = result.AccessToken;
        }
    }
}