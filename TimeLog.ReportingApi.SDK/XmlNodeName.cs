using System;

namespace TimeLog.ReportingApi.SDK
{
    /// <summary>
    ///     This attribute indicates the real name in the XML, so the TimeLog.Reporting.Api.Exporter can find the data.
    ///     TimeLog.Reporting.Api.Exporter uses the XML node name and the property name to match values for export.
    ///     When the property name in the object is not the same as the XML node name, the values are not exported.
    /// </summary>
    public class XmlNodeName : Attribute
    {
        internal XmlNodeName(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Name defined in the XML.
        /// </summary>
        public string Name { get; }
    }
}