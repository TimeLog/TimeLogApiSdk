using System;
using System.Collections.Generic;
using System.Text;

namespace TimeLog.DataImporter.TimeLogApi
{
    public class ApiResponse
    {
        public ApiResponse(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public int Code { get; set; }
        public string[] Details { get; set; }
        public string Message { get; set; }
    }
}
