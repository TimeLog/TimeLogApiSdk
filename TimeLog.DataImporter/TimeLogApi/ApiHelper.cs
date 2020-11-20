using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TimeLog.DataImporter.TimeLogApi
{
    public class ApiHelper
    {
        public string LocalhostUrl = "http://localhost/tlptest";
        public string CustomerValidateEndpoint = "/api/v1/customer/validate-new-customer";
        public string CustomerCreateEndpoint = "/api/v1/customer/create";
        public string ProjectValidateEndpoint = "/api/v1/project/validate-create-from-template";
        public string ProjectCreateEndpoint = "/api/v1/project/create-from-template";
        public string GetAllCountryEndpoint = "/api/v1/country/get-all?$page=1&$pagesize=300";
        public string GetAllIndustryEndpoint = "/api/v1/industry/get-all?$page=1&$pagesize=50";
        public string GetAllCurrencyEndpoint = "/api/v1/currency/active?$page=1&$pagesize=300";
        public string GetAllCustomerStatusEndpoint = "/api/v1/customerstatus?$page=1&$pagesize=30";
        public string GetAllEmployeeEndpoint = "/api/v1/user?$page=1&$pagesize=500";
        public string GetAllPaymentTermEndpoint = "/api/v1/payment-method?$page=1&$pagesize=30";  //endpoint coming soon, no payment term yet
        public string GetAllProjectTemplateEndpoint = "/api/v1/project-template/get-all?$page=1&$pagesize=500";
        public string GetAllLegalEntityEndpoint = "/api/v1/LegalEntity?$page=1&$pagesize=500";
        public string GetAllProjectTypeEndpoint = "/api/v1/ProjectType?$page=1&$pagesize=500";
        public string GetAllProjectCategoryEndpoint = "/api/v1/ProjectCategory?$page=1&$pagesize=500";

        private static ApiHelper _apiHelper;
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
                    return _apiHelper ??= new ApiHelper();
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