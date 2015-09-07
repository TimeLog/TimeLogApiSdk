namespace TimeLog.ReportingApi.SDK
{
    using System.Xml;

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
            var element = node.SelectSingleNode(xpath, namespaceManager);
            if (element != null)
            {
                return element.InnerText;
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
            var element = node.SelectSingleNode(xpath, namespaceManager);
            if (element != null)
            {
                int result;
                if (int.TryParse(element.InnerText, out result))
                {
                    return result;
                }

                return 0;
            }

            return 0;
        }
    }
}
