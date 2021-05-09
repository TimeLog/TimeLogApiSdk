using System;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class MethodParam
    {
        public MethodParam(string name, string description, string type)
        {
            this.Name = name;
            this.Description = description;

            if (type.StartsWith("System"))
            {
                this.Type = type.Replace("System.", string.Empty);
            }
            else
            {
                var _lastDot = type.LastIndexOf(".", StringComparison.Ordinal);
                this.Type = type.Substring(_lastDot + 1, type.Length - _lastDot - 1);
            }

            if (this.Type.ToLowerInvariant() == "datetime")
            {
                this.Type = "DateTime (yyyy-MM-dd)";
            }
            else if (this.Type.ToLowerInvariant() == "int32")
            {
                this.Type = "Integer";
            }
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}