namespace TimeLog.ReportingApi.Exporter
{
    using System.Xml;

    public interface IMethod
    {
        OutputConfiguration GetConfiguration();

        XmlNode GetData(OutputConfiguration configuration);
    }
}
