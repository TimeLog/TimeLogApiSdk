using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using IdentityModel.Client;
using Newtonsoft.Json;
using TimeLog.DataImporter.TimeLogApi;
using TimeLog.DataImporter.TimeLogApi.Model;

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
                //return _instance ?? (_instance = new ProjectHandler());
                return _instance ??= new ProjectHandler();
            }
        }

        public BusinessRulesApiResponse ValidateProject(ProjectCreateModel project, string token)
        {
            var _data = JsonConvert.SerializeObject(project, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.ProjectValidateEndpoint;

            try
            {
                string jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", _data);

                return new BusinessRulesApiResponse(200,"OK", new List<Detail>());
            }
            catch (WebException webex)
            {
                using (StreamReader r = new StreamReader(webex.Response.GetResponseStream()))
                {
                    string _responseContent = r.ReadToEnd();
                    var _apiResponse = JsonConvert.DeserializeObject<BusinessRulesApiResponse>(_responseContent);
                    _apiResponse.Code = 400;

                    return _apiResponse;
                }
            }
        }
    }
}