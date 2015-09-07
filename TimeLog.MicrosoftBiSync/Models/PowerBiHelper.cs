namespace TimeLog.MicrosoftBiSync.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Script.Serialization;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    using Newtonsoft.Json;

    using PowerBi;

    using TimeLog.ReportingApi.SDK;

    public class PowerBiHelper
    {
        public const string DatasetName = "TimeLogProject";

        private readonly AuthenticationResult authentication;
        private readonly string datasetsUri = "https://api.powerbi.com/v1.0/myorg/datasets";
        private readonly string tablesUri = "https://api.powerbi.com/v1.0/myorg/datasets/{0}/tables";
        private readonly string tableSchemaUri = "https://api.powerbi.com/v1.0/myorg/datasets/{0}/tables/{1}";
        private readonly string rowsUri = "https://api.powerbi.com/v1.0/myorg/datasets/{0}/tables/{1}/rows";


        public PowerBiHelper(AuthenticationResult authentication)
        {
            this.authentication = authentication;
        }

        public void AddRows(string datasetId, string tableId, List<string> jsonRows, int batchSize)
        {
            int index = 0;

            while (jsonRows.Count > index)
            {
                var requestList = jsonRows.Skip(index).Take(batchSize);
                index = index + batchSize;

                var rowTable = "{ \"rows\" : [ {0} ] }";
                var requestJson = rowTable.Replace("{0}", string.Join(",", requestList));

                System.Net.WebRequest request = System.Net.WebRequest.Create(string.Format(this.rowsUri, datasetId, tableId)) as System.Net.HttpWebRequest;
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentLength = 0;
                    request.ContentType = "application/json";
                    request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(requestJson);
                    request.ContentLength = byteArray.Length;

                    // Write JSON byte[] into a Stream
                    using (Stream writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                    {
                        // Get reader from response stream
                        if (response != null)
                        {
                        }
                    }
                }
            }
        }

        public bool IsDefaultDatasetAvailable()
        {
            return !string.IsNullOrWhiteSpace(this.GetDefaultDatasetId());
        }

        public string GetDefaultDatasetId()
        {
            var dataset = this.GetDataSets().Value.ToList().FirstOrDefault(e => e.Name == DatasetName);
            if (dataset != null)
            {
                return dataset.Id;
            }

            return string.Empty;
        }

        public void CreateDefaultDataset()
        {
            if (this.IsDefaultDatasetAvailable())
            {
                return;
            }

            // Configure datasets request
            System.Net.WebRequest request = System.Net.WebRequest.Create(this.datasetsUri) as System.Net.HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                dynamic jsonRequest =
                    new
                    {
                        name = DatasetName,
                        tables =
                                new[]
                                    {
                                        new
                                            {
                                                name = "Customers",
                                                columns =
                                            new[]
                                                {
                                                    new { name = "CustomerId", dataType = "Int64" },
                                                    new { name = "Name", dataType = "string" }
                                                }
                                            },
                                    new
                                            {
                                                name = "Contacts",
                                                columns =
                                            new[]
                                                {
                                                    new { name = "ContactId", dataType = "Int64" },
                                                    new { name = "Name", dataType = "string" }
                                                }
                                            }
                                    }
                    };

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonRequest));
                request.ContentLength = byteArray.Length;

                // Write JSON byte[] into a Stream
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Get reader from response stream
                    if (response != null)
                    {
                    }
                }
            }
        }

        public Tables GetTables(string datasetId)
        {
            // Configure datasets request
            System.Net.WebRequest request = System.Net.WebRequest.Create(string.Format(this.tablesUri, datasetId)) as System.Net.HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.ContentLength = 0;
                request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Get reader from response stream
                    if (response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            var responseContent = reader.ReadToEnd();

                            // Deserialize JSON string
                            // JavaScriptSerializer class is in System.Web.Script.Serialization
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            return (Tables)json.Deserialize(responseContent, typeof(Tables));
                        }
                    }
                }
            }

            return new Tables();
        }

        public Datasets GetDataSets()
        {
            // Configure datasets request
            System.Net.WebRequest request = System.Net.WebRequest.Create(this.datasetsUri) as System.Net.HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.ContentLength = 0;
                request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Get reader from response stream
                    if (response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            var responseContent = reader.ReadToEnd();

                            // Deserialize JSON string
                            // JavaScriptSerializer class is in System.Web.Script.Serialization
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            return (Datasets)json.Deserialize(responseContent, typeof(Datasets));
                        }
                    }
                }
            }

            return new Datasets();
        }

        public void UpdateTableSchema(string datasetId, string tableName, string schemaJson)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(string.Format(this.tableSchemaUri, datasetId, tableName)) as System.Net.HttpWebRequest;
            if (request != null)
            {
                request.Method = "PUT";
                request.ContentLength = 0;
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(schemaJson);
                request.ContentLength = byteArray.Length;

                // Write JSON byte[] into a Stream
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Get reader from response stream
                    if (response != null)
                    {
                    }
                }
            }
        }

        public void DeleteRows(string datasetId, string tableName)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(string.Format(this.rowsUri, datasetId, tableName)) as System.Net.HttpWebRequest;
            if (request != null)
            {
                request.Method = "DELETE";
                request.ContentLength = 0;
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", string.Format("Bearer {0}", this.authentication.AccessToken));

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Get reader from response stream
                    if (response != null)
                    {
                    }
                }
            }
        }

        public string BuildTableSchemaJson(string tableName, Type structure)
        {
            var tableStructure = "{ \"name\" : \"{0}\", \"columns\" : [{1}] }";
            var tableColumnTemplate = "\"name\" : \"{0}\", \"dataType\" : \"{1}\"";

            IList<Column> columns = new List<Column>();
            foreach (var prop in structure.GetProperties())
            {
                ColumnDataType type = ColumnDataType.@string;
                if (prop.PropertyType == typeof(bool))
                {
                    type = ColumnDataType.@bool;
                }
                else if (prop.PropertyType == typeof(int))
                {
                    type = ColumnDataType.Int64;
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    type = ColumnDataType.DateTime;
                }

                columns.Add(new Column(prop.Name, type));
            }

            var result = tableStructure.Replace("{0}", tableName);
            var jsonColumns = columns.Select(column => "{" + string.Format(tableColumnTemplate, column.Name, column.DataType) + "}").ToList();

            return result.Replace("{1}", string.Join(",", jsonColumns));
        }

        public string BuildTableRowJson<T>(T data)
        {
            var rowColumnTemplate = "\"{0}\" : {1}";

            var jsonColumns = new List<string>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetGetMethod().IsStatic)
                {
                    continue;
                }

                if (prop.PropertyType == typeof(int))
                {
                    jsonColumns.Add(string.Format(rowColumnTemplate, prop.Name, prop.GetValue(data)));
                }
                else
                {
                    jsonColumns.Add(string.Format(rowColumnTemplate, prop.Name, "\"" + prop.GetValue(data) + "\""));
                }
            }

            return "{" + string.Join(",", jsonColumns) + "}";
        }
    }
}