using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TimeLog.DataImporter.TimeLogApi
{
    public class ApiHelper
    {
        public string LocalhostUrl = "http://localhost/tlp";
        public string CustomerValidateEndpoint = "/api/v1/customer/validate-new-customer";
        public string CustomerCreateEndpoint = "/api/v1/customer/create";
        //public string ProjectValidateEndpoint = "/api/v1/project/validate-new-project";
        public string ProjectValidateEndpoint = "/api/v1/project/create";
        public string ProjectCreateEndpoint = "/api/v1/project/create";

        private static ApiHelper apiHelper;
        private static readonly object ApiHelperLock = new object();

        private ApiHelper()
        {
        }

        public static ApiHelper Instance
        {
            get
            {
                lock (ApiHelperLock)
                {
                    return apiHelper ?? (apiHelper = new ApiHelper());
                }
            }
        }




        public WebClient WebClient(string token)
        {
          
            var _client = new WebClient();
            _client.Encoding = Encoding.UTF8;
            _client.Headers.Add("Authorization", "Bearer " + token);
            _client.Headers.Add("Accept", "application/json");
            _client.Headers.Add("Content-Type", "application/json");
            return _client;
        }



    }
}
