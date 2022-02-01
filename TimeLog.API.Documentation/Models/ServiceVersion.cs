namespace TimeLog.Api.Documentation.Models;

public class ServiceVersion
{
    public ServiceVersion(string v)
    {
        var versionSplit = v.Split('.', '_');
        Major = Convert.ToInt32(versionSplit[0]);
        Minor = Convert.ToInt32(versionSplit[1]);
    }

    public ServiceVersion(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }

    public int Major { get; set; }

    public int Minor { get; set; }
}