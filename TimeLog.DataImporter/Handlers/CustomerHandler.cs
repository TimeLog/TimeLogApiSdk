using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using IdentityModel.Client;
using Newtonsoft.Json;
using TimeLog.DataImporter.TimeLogApi;

namespace TimeLog.DataImporter.Handlers
{
    public class CustomerHandler : BaseHandler
    {
        private static CustomerHandler _instance;

        private CustomerHandler()
        {
        }
        public static CustomerHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new CustomerHandler());
            }
        }


        public ApiResponse ValidateCustomer(CustomerModel customer, string token)
        {
            var data = JsonConvert.SerializeObject(customer, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.CustomerValidateEndpoint;

            try
            {
                string jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", data);

                return new ApiResponse(200,"OK");
            }
            catch (WebException webex)
            {
                using (StreamReader r = new StreamReader(
                    webex.Response.GetResponseStream()))
                {
                    string _responseContent = r.ReadToEnd();

                    var _apiResponse = JsonConvert.DeserializeObject<ApiResponse>(_responseContent);
                    _apiResponse.Code = 400;
                    return _apiResponse;
                    
                }
            }
        }
    }
}
