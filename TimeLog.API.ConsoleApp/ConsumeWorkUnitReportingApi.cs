using System.Globalization;
using System.Xml;
using log4net;
using TimeLog.ReportingAPI.SDK;

namespace TimeLog.API.ConsoleApp;

/// <summary>
///     Template class for consuming the reporting API
/// </summary>
public class ConsumeWorkUnitReportingApi
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ConsumeWorkUnitReportingApi));

    public static void Consume()
    {
        if (ServiceHandler.Instance.TryAuthenticate())
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Successfully authenticated on reporting API");
            }

            var _startDate = DateTime.Now.AddDays(-3);
            while (_startDate < DateTime.Now)
            {
                var _fileName = _startDate.ToString("yyyyMMdd") + "WorkUnitsRaw.xml";
                try
                {
                    Logger.Debug("Fetching from " + _startDate.ToString("yyyyMMdd"));
                    var raw = ServiceHandler.Instance.Client.GetWorkUnitsRaw(
                        ServiceHandler.Instance.SiteCode,
                        ServiceHandler.Instance.ApiId,
                        ServiceHandler.Instance.ApiPassword,
                        0,
                        0, 0, 0, 0, 0,
                        _startDate.ToString("yyyy-MM-dd"),
                        _startDate.AddDays(1).ToString("yyyy-MM-dd"));

                    Logger.Debug("Saving " + _fileName);
                    File.WriteAllText(_fileName, raw.InnerXml);
                }
                catch (Exception _exception)
                {
                    File.WriteAllText(_fileName, _exception.Message + " " + _exception.StackTrace);
                }

                _startDate = _startDate.AddDays(1);
            }
        }
        else
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Failed to authenticate to reporting API");
            }
        }
    }
}