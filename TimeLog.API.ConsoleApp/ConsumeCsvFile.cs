using System.Reflection;
using log4net;
using TimeLog.Library.Data;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming a csv file
/// </summary>
public class ConsumeCsvFile
{
    private static readonly DirectoryInfo? AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ConsumeCsvFile));

    public static void Consume()
    {
        try
        {
            var csvReader = new CsvReader(AppPath + "\\test.csv") {Splitter = ','};
            
            while (csvReader.Read())
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.DebugFormat(
                        "Found project \"{0}\" for customer \"{1}\" with period {2:r} to {3:r} with a budget of {4} hours ({5})",
                        csvReader.GetString("ProjectName"),
                        csvReader.GetString("CustomerName"), csvReader.GetDateTime("StartDate"),
                        csvReader.GetDateTime("EndDate"),
                        csvReader.GetDouble("BudgetHours"),
                        csvReader.GetString("Comment"));
                }
            }
        }
        catch (Exception ex)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error("Failed to read file", ex);
            }
        }
    }
}