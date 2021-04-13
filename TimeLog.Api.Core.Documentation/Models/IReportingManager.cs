using System.Collections.Generic;

namespace TimeLog.Api.Core.Documentation.Models
{
    public interface IReportingManager
    {
        string GetReportingSchema(MethodDoc doc);

        string GetReportingExample(MethodDoc doc);
        
        IEnumerable<MethodDoc> GetMethods();
        
        MethodDoc GetMethod(string methodFullName);
    }
}