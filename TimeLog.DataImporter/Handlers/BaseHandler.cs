using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using TimeLog.DataImporter.TimeLogApi;
using TimeLog.DataImporter.TimeLogApi.Model;

namespace TimeLog.DataImporter.Handlers
{
    public class BaseHandler
    {
        public List<string> FileColumnHeaders = new List<string>();
        private readonly List<string> _delimiterList = new List<string>{",",";","|"};

        // The state of expanding or collapsing panel
        public enum ExpandState
        {
            Expanded,
            Expanding,
            Collapsing,
            Collapsed,
        }

        public DataTable GetFileContent(string selectedDelimiter)
        {
            try
            {
                OpenFileDialog _dialog = new OpenFileDialog();
                _dialog.ShowDialog();
                
                if (_dialog.FileName != "")
                {
                    if (_dialog.FileName.EndsWith(".csv"))
                    {
                        DataTable _fileData = new DataTable();
                        _fileData = GetDataTableFromCSVFile(_dialog.FileName, selectedDelimiter);
                        
                        if (_fileData != null)
                        {
                            if (_fileData.Rows.Count > 0)
                            {
                                return _fileData;
                            }

                            //return this message when the row count is zero
                            MessageBox.Show("There is no data in this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected file is invalid, please select a valid CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex);
            }

            return null;
        }

        private DataTable GetDataTableFromCSVFile(string csvFilePath, string selectedDelimiter)
        {
            DataTable _csvData = new DataTable();

            try
            {
                if (csvFilePath.EndsWith(".csv"))
                {
                    using Microsoft.VisualBasic.FileIO.TextFieldParser _csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvFilePath);
                    _csvReader.SetDelimiters(new string[]
                    {
                            selectedDelimiter
                    });

                    string[] _colFields = _csvReader.ReadFields();

                    if (_colFields != null)
                    {
                        foreach (string _column in _colFields)
                        {
                            DataColumn _dataColumn = new DataColumn(_column)
                            {
                                AllowDBNull = true
                            };

                            _csvData.Columns.Add(_dataColumn);
                            FileColumnHeaders.Add(_dataColumn.ColumnName);
                        }

                        while (!_csvReader.EndOfData)
                        {
                            string[] _fieldData = _csvReader.ReadFields();

                            for (int i = 0; i < _fieldData.Length; i++)
                            {
                                if (_fieldData[i] == "")
                                {
                                    _fieldData[i] = null;
                                }
                            }

                            _csvData.Rows.Add(_fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex);
            }

            return _csvData;
        }

        public List<string> GetDelimiterList()
        {
            return _delimiterList;
        }

        public List<CurrencyReadModel> GetAllCurrency(string token)
        {
            var _address = ApiHelper.Instance.LocalhostUrl + ApiHelper.Instance.GetAllCurrencyEndpoint;

            try
            {
                string _jsonResult = ApiHelper.Instance.WebClient(token).DownloadString(_address);
                dynamic _jsonDeserializedObject = JsonConvert.DeserializeObject<dynamic>(_jsonResult);

                if (_jsonDeserializedObject != null && _jsonDeserializedObject.Entities.Count > 0)
                {
                    List<CurrencyReadModel> _apiResponse = new List<CurrencyReadModel>();

                    foreach (var _entity in _jsonDeserializedObject.Entities)
                    {
                        foreach (var _property in _entity.Properties())
                        {
                            _apiResponse.Add(JsonConvert.DeserializeObject<CurrencyReadModel>(_property.Value.ToString()));
                        }
                    }

                    return _apiResponse;
                }
            }
            catch (WebException _webEx)
            {
                MessageBox.Show("Failed to obtain default currency ID list. " + _webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;
        }

        public DefaultApiResponse ProcessApiResponseContent(WebException webEx, string responseContent, out BusinessRulesApiResponse businessRulesApiResponse)
        {
            DefaultApiResponse _apiResponse = null;
            businessRulesApiResponse = null;

            if (webEx.Message == "The remote server returned an error: (401) Unauthorized.")
            {
                _apiResponse = JsonConvert.DeserializeObject<DefaultApiResponse>(responseContent);
                _apiResponse.Code = 401;
            }
            else
            {
                dynamic _apiResponseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

                if (_apiResponseObject.Code.ToString() == "200")
                {
                    _apiResponse = JsonConvert.DeserializeObject<DefaultApiResponse>(responseContent);
                    _apiResponse.Code = 201;
                }
                else if (_apiResponseObject.Code.ToString() == "102")
                {
                    businessRulesApiResponse = JsonConvert.DeserializeObject<BusinessRulesApiResponse>(responseContent);
                    businessRulesApiResponse.Code = 102;
                }
                else
                {
                    MessageBox.Show(webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return _apiResponse;
        }

        public string CheckAndGetString(object columnValue)
        {
            return columnValue.ToString();
        }

        public bool CheckAndGetBoolean(string columnName, object columnValue)
        {
            var _value = columnValue.ToString();

            if (_value != "")
            {
                if (bool.TryParse(_value, out var _result))
                {
                    return _result;
                }

                if (_value == "1")
                {
                    return true;
                }

                if (_value == "0")
                {
                    return false;
                }

                throw new FormatException("String format cannot be converted to boolean for column [" + columnName + "]. Please recheck input.");
            }

            return false;
        }

        public int CheckAndGetInteger(string columnName, object columnValue)
        {
            try
            {
                if (columnValue != DBNull.Value && columnValue.ToString() != "")
                {
                    return Convert.ToInt32(columnValue);
                }

                return 0;
            }
            catch (Exception)
            {
                throw new FormatException("String format cannot be converted to integer for column [" + columnName + "]. Please recheck input.");
            }
        }

        public int? CheckAndGetNullableInteger(string columnName, object columnValue)
        {
            try
            {
                if (columnValue == DBNull.Value | columnValue.ToString() == "")
                {
                    return null;
                }

                return Convert.ToInt32(columnValue);
            }
            catch (Exception)
            {
                throw new FormatException("String format cannot be converted to integer for column [" + columnName + "]. Please recheck input.");
            }
        }

        public double CheckAndGetDouble(string columnName, object columnValue)
        {
            try
            {
                if (columnValue != DBNull.Value)
                {
                    return Convert.ToDouble(columnValue);
                }

                return 0;
            }
            catch (Exception)
            {
                throw new FormatException("String format cannot be converted to double for column [" + columnName + "]. Please recheck input.");
            }
        }

        public DateTime CheckAndGetDate(string columnName, object columnValue)
        {
            try
            {
                if (columnValue != DBNull.Value)
                {
                    return Convert.ToDateTime(columnValue);
                }

                return DateTime.Now;
            }
            catch (Exception)
            {
                throw new FormatException("String format cannot be converted to datetime for column [" + columnName + "]. Please recheck input.");
            }
        }
    }
}