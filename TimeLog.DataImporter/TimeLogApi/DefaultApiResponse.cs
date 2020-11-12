using System.Collections.Generic;

namespace TimeLog.DataImporter.TimeLogApi
{
    public class DefaultApiResponse
    {
        //to capture default response and domain validation
        public DefaultApiResponse(int code, string message, string[] details)
        {
            this.Code = code;
            this.Details = details;
            this.Message = message;
        }

        public int Code { get; set; }
        public string[] Details{ get; set; }
        public string Message { get; set; }
    }
}