namespace TimeLog.Api.Documentation.Models
{
    using System;

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
                var lastDot = type.LastIndexOf(".", StringComparison.Ordinal);
                this.Type = type.Substring(lastDot + 1, type.Length - lastDot - 1);
            }
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}