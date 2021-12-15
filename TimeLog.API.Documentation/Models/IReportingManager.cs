namespace TimeLog.Api.Documentation.Models;

public interface IReportingManager
{
    IEnumerable<MethodDoc> GetMethods();

    MethodDoc GetMethod(string methodFullName);
}