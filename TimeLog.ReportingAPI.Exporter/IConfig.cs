using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLog.ReportingApi.Exporter
{
    using System.Xml.Serialization;

    public interface IConfig
    {
        string Name { get; set; }

        ExportFormat ExportFormat { get; set; } 

        Parameter[] Parameter { get; set; }
    }

    public class Parameter
    {
        public Parameter()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        public Parameter(string name, object value)
        {
            this.Name = name;
            this.Value = value.ToString();
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }

    public enum ExportFormat
    {
        Xml,
        Csv
    }

}
