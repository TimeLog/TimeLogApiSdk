﻿using TimeLog.ReportingApi.Core.SDK;

namespace TimeLog.ReportingApi.Exporter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Serialization;

    public class OutputConfiguration : IConfig
    {
        public OutputConfiguration()
        {
            this.Name = "OutputConfiguration";
            this.ExportFormat = ExportFormat.Xml;
            this.InternalParameters = new Dictionary<string, object>();
            this.ListElementType = null;
        }

        public OutputConfiguration(string name)
        {
            this.Name = name;
            this.ExportFormat = ExportFormat.Xml;
            this.InternalParameters = new Dictionary<string, object>();
            this.ListElementType = null;
        }

        public string Name { get; set; }

        public ExportFormat ExportFormat { get; set; }

        public Parameter[] Parameter
        {
            get
            {
                return this.InternalParameters.Select(e => new Parameter(e.Key, e.Value)).ToArray();
            }
            set
            {
                this.InternalParameters = new Dictionary<string, object>();
                foreach (var param in value)
                {
                    this.InternalParameters.Add(param.Name, param.Value);
                }
            }
        }

        [XmlIgnore]
        public Dictionary<string, object> InternalParameters { get; set; }

        public string ListElementType { get; set; }

        public int GetIntegerSafe(string key)
        {
            try
            {
                return int.Parse(this.InternalParameters[key].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DateTime GetDateTimeSafe(string key)
        {
            try
            {
                return DateTime.Parse(this.InternalParameters[key].ToString());
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public string GetStringSafe(string key)
        {
            try
            {
                return this.InternalParameters[key].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
