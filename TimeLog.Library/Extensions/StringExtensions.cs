using System;

namespace TimeLog.Library.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string s, int length)
        {
            if (s == null)
            {
                return string.Empty;
            }

            return s.Substring(0, Math.Min(s.Length, length));
        }
    }
}
