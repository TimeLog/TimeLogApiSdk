using System;

namespace TimeLog.Api.Documentation.Models;

public class MethodParam
{
    public MethodParam(string name, string description, string type)
    {
        Name = name;
        Description = description;

        if (type.StartsWith("System"))
        {
            Type = type.Replace("System.", string.Empty);
        }
        else
        {
            var _lastDot = type.LastIndexOf(".", StringComparison.Ordinal);
            Type = type.Substring(_lastDot + 1, type.Length - _lastDot - 1);
        }

        if (Type.ToLowerInvariant() == "datetime")
        {
            Type = "DateTime (yyyy-MM-dd)";
        }
        else if (Type.ToLowerInvariant() == "int32")
        {
            Type = "Integer";
        }
    }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }
}