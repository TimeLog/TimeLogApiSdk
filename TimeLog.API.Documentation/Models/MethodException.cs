namespace TimeLog.Api.Documentation.Models;

public class MethodException
{
    public MethodException(string exception, string reason)
    {
        Cref = exception;
        Description = reason;
    }

    public string Cref { get; set; }

    public string Description { get; set; }
}