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
    public class ProjectHandler : BaseHandler
    {
        private static ProjectHandler _instance;

        private ProjectHandler()
        {
        }
        public static ProjectHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new ProjectHandler());
            }
        }


        public ApiResponse ValidateProject(ProjectModel project, string token)
        {
            var data = JsonConvert.SerializeObject(project, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.ProjectValidateEndpoint;

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
