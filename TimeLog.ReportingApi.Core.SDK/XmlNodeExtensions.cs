using System;
using System.Globalization;
using System.Xml;

namespace TimeLog.ReportingApi.Core.SDK
{
    /// <summary>
    /// XmlNode extensions for reporting API
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Gets a strongly types string value from a given XPath. Returns empty string if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <returns>A string value (empty string if parsing fails)</returns>
        public static string GetStringSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                return _element.InnerText;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets a strongly typed int value from a given XPath. Returns 0 if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <returns>An int value (0 if parsing fails)</returns>
        public static int GetIntSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                if (int.TryParse(_element.InnerText, out var _result))
                {
                    return _result;
                }

                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Gets a strongly typed double value from a given XPath. Returns 0 if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <param name="culture">Optional culture for conversion</param>
        /// <returns>An double value (0 if parsing fails)</returns>
        public static double GetDoubleSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager, CultureInfo culture = null)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                if (culture == null)
                {
                    culture = ServiceHandler.DataCulture;
                }

                if (double.TryParse(_element.InnerText, NumberStyles.Any, culture, out var _result))
                {
                    return _result;
                }

                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Gets a strongly typed float value from a given XPath. Returns 0 if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <param name="culture">Optional culture for conversion</param>
        /// <returns>An double value (0 if parsing fails)</returns>
        public static float GetFloatSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager, CultureInfo culture = null)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                if (culture == null)
                {
                    culture = ServiceHandler.DataCulture;
                }

                if (float.TryParse(_element.InnerText, NumberStyles.Any, culture, out var _result))
                {
                    return _result;
                }

                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Gets a strongly typed DateTime value from a given XPath. Returns DateTime.MinValue if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <returns>An DateTime value (DateTime.MinValue if parsing fails)</returns>
        public static DateTime GetDateTimeSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                if (DateTime.TryParse(_element.InnerText, out var _result))
                {
                    return _result;
                }

                return DateTime.MinValue;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets a strongly typed boolean value from a given XPath. Returns false if parsing fails.
        /// </summary>
        /// <param name="node">The XML node</param>
        /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
        /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
        /// <returns>An boolean value (false if parsing fails)</returns>
        public static bool GetBoolTimeSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            var _element = node.SelectSingleNode(xpath, namespaceManager);
            if (_element != null)
            {
                if (bool.TryParse(_element.InnerText, out var _result))
                {
                    return _result;
                }

                return false;
            }

            return false;
        }
    }
}
