using System.Collections.Generic;

namespace TimeLog.Api.Core.Documentation.Models
{
    public interface IReportingManager
    {
        IEnumerable<MethodDoc> GetMethods();
        
        MethodDoc GetMethod(string methodFullName);
    }
}