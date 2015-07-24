using System.Web;

namespace TimeLog.Api.Documentation
{
    using System.Text.RegularExpressions;

    using TimeLog.Api.Documentation.Models;

    public static class DocExtensions
    {
        public static Regex VersionRegEx = new Regex(@"^TimeLog\.TLP\.API\.[^0-9]+(\d)_(\d).+Service$");

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
            return HttpUtility.UrlEncode(str)?.Replace(".", "");
        }
    }
}