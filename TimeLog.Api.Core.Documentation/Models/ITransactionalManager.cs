using System.Collections.Generic;

namespace TimeLog.Api.Core.Documentation.Models
{
    public interface ITransactionalManager
    {
        IEnumerable<TypeDoc> GetServices();
        
        TypeDoc? GetService(string typeFullName);
        
        MethodDoc? GetMethod(string methodFullName);
    }
}