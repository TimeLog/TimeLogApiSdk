using System.Text.RegularExpressions;
using System.Web;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation;

public static class DocExtensions
{
    public static Regex VersionRegEx = new(@"^TimeLog\.TLP\.API\.[^0-9]+(\d)_(\d).+Service$");

    public static string Version(this TypeDoc doc)
    {
        var matches = VersionRegEx.Matches(doc.FullName);
        if (matches[0].Groups.Count == 3)
        {
            return matches[0].Groups[1].Value + "." + matches[0].Groups[2].Value;
        }

        return string.Empty;
    }

    public static string UrlEncode(this string str)
    {
        var urlEncode = HttpUtility.UrlEncode(str);

        if (urlEncode != null)
        {
            return urlEncode.Replace(".", string.Empty);
        }

        return string.Empty;
    }

    public static string SplitOnUppercase(this string str)
    {
        var result = string.Empty;
        var characters = string.Concat(str, " ").ToCharArray();
        var isPreparedForNewWord = false;
        var buffer = '\0';

        foreach (var t in characters)
        {
            if (buffer != '\0')
            {
                if (isPreparedForNewWord && t.ToString() == t.ToString().ToLower())
                {
                    result += " ";
                    isPreparedForNewWord = false;
                }
                else if (t.ToString() == t.ToString().ToUpper())
                {
                    isPreparedForNewWord = true;
                }

                result += buffer;
            }

            buffer = t;
        }

        return result;
    }
}