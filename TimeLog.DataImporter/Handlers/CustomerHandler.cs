using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using TimeLog.DataImporter.TimeLogApi;
using TimeLog.DataImporter.TimeLogApi.Model;

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
            get { return _instance ??= new CustomerHandler(); }
        }

        public DefaultApiResponse ValidateCustomer(CustomerCreateModel customer, string token, out BusinessRulesApiResponse businessRulesApiResponse)
        {
            var _data = JsonConvert.SerializeObject(customer, Newtonsoft.Json.Formatting.None, 
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.CustomerValidateEndpoint;
            businessRulesApiResponse = null;

            try
            {
                var _jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", _data);

                if (_jsonResult == "null")
                {
                    return new DefaultApiResponse(200, "OK", new string[] { });
                }

                return new DefaultApiResponse(500, "Internal Application Error: Fail to Validate Customer", new string[] { });
            }
            catch (WebException _webEx)
            {
                using StreamReader _r = new StreamReader(_webEx.Response.GetResponseStream());
                string _responseContent = _r.ReadToEnd();

                return ProcessApiResponseContent(_webEx, _responseContent, out businessRulesApiResponse);
            }
        }

        public DefaultApiResponse ImportCustomer(CustomerCreateModel customer, string token, out BusinessRulesApiResponse businessRulesApiResponse)
        {
            var _data = JsonConvert.SerializeObject(customer, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.CustomerCreateEndpoint;
            businessRulesApiResponse = null;

            try
            {
                var _jsonResult = ApiHelper.Instance.WebClient(token).UploadString(_address, "POST", _data);

                if (_jsonResult == "null")
                {
                    return new DefaultApiResponse(200, "OK", new string[] { });
                }

                return new DefaultApiResponse(500, "Internal Application Error: Fail to Import Customer", new string[] { });
            }
            catch (WebException _webEx)
            {
                using StreamReader _r = new StreamReader(_webEx.Response.GetResponseStream());
                string _responseContent = _r.ReadToEnd();

                return ProcessApiResponseContent(_webEx, _responseContent, out businessRulesApiResponse);
            }
        }

        public List<CountryReadModel> GetAllCountry(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllCountryEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<CountryReadModel> _apiResponse = new List<CountryReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            _apiResponse.Add(JsonConvert.DeserializeObject<CountryReadModel>(_property.Value.ToString()));
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default country ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;
        }

        public List<CustomerStatusReadModel> GetAllCustomerStatus(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllCustomerStatusEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<CustomerStatusReadModel> _apiResponse = new List<CustomerStatusReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            _apiResponse.Add(JsonConvert.DeserializeObject<CustomerStatusReadModel>(_property.Value.ToString()));
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default customer status ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return null;
        }

        public List<EmployeeReadModel> GetAllEmployee(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllEmployeeEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<EmployeeReadModel> _apiResponse = new List<EmployeeReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            if (_property.Name == "Properties")
                            {
                                _apiResponse.Add(JsonConvert.DeserializeObject<EmployeeReadModel>(_property.Value.ToString()));
                            }
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default primary and secondary KAM ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            return null;
        }

        public List<IndustryReadModel> GetAllIndustry(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllIndustryEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<IndustryReadModel> _apiResponse = new List<IndustryReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            _apiResponse.Add(JsonConvert.DeserializeObject<IndustryReadModel>(_property.Value.ToString()));
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default industry ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return null;
        }

        public List<PaymentTermReadModel> GetAllPaymentTerm(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllPaymentTermEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<PaymentTermReadModel> _apiResponse = new List<PaymentTermReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            _apiResponse.Add(JsonConvert.DeserializeObject<PaymentTermReadModel>(_property.Value.ToString()));
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default payment term ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;
        }
    }
}