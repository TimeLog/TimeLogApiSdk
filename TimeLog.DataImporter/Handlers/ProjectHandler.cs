using System.IO;
using System.Net;
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
                return _instance ??= new ProjectHandler();
            }
        }

        public DefaultApiResponse ValidateProject(ProjectCreateModel project, string token, out BusinessRulesApiResponse businessRulesApiResponse)
        {
            var _data = JsonConvert.SerializeObject(project, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.ProjectValidateEndpoint;
            businessRulesApiResponse = null;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", _data);

                return new DefaultApiResponse(200, "OK", new string[] { });
            }
            catch (WebException _webEx)
            {
                using StreamReader _r = new StreamReader(_webEx.Response.GetResponseStream());
                string _responseContent = _r.ReadToEnd();

                return ProcessApiResponseContent(_webEx, _responseContent, out businessRulesApiResponse);
            }
        }

        public DefaultApiResponse ImportProject(ProjectCreateModel project, string token, out BusinessRulesApiResponse businessRulesApiResponse)
        {
            var _data = JsonConvert.SerializeObject(project, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.ProjectCreateEndpoint;
            businessRulesApiResponse = null;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", _data);

                return new DefaultApiResponse(200, "OK", new string[] { });
            }
            catch (WebException _webEx)
            {
                using StreamReader _r = new StreamReader(_webEx.Response.GetResponseStream());
                string _responseContent = _r.ReadToEnd();

                return ProcessApiResponseContent(_webEx, _responseContent, out businessRulesApiResponse);
            }
        }
    }
}