namespace TimeLog.ReportingApi.Exporter
{
    using System.Xml;

    public interface IMethod
    {
        OutputConfiguration GetConfiguration(ExportFormat format);

        XmlNode GetData(OutputConfiguration configuration);
    }
}
