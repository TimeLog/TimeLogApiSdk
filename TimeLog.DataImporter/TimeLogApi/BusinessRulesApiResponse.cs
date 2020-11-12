using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TimeLog.DataImporter.TimeLogApi
{
    public class BusinessRulesApiResponse
    {
        //to capture business rules validation in API response
        public BusinessRulesApiResponse(int code, string message, List<Detail> details)
        {
            this.Code = code;
            this.Details = details;
            this.Message = message;
        }

        public int Code { get; set; }
        public List<Detail> Details{ get; set; }
        public string Message { get; set; }
    }

    public class Detail
    {
        public Detail(string message, int errorCode)
        {
            this.Message = message;
            this.ErrorCode = errorCode;
        }

        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}