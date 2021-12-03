using System;
using System.Globalization;
using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     XmlNode extensions for reporting API
/// </summary>
public static class XmlNodeExtensions
{
    /// <summary>
    ///     Gets a strongly types string value from a given XPath. Returns empty string if parsing fails.
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
    ///     Gets a strongly typed int value from a given XPath. Returns 0 if parsing fails.
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
            if (int.TryParse(element.InnerText, out var result))
            {
                return result;
            }

            return 0;
        }

        return 0;
    }

    /// <summary>
    ///     Gets a strongly typed double value from a given XPath. Returns 0 if parsing fails.
    /// </summary>
    /// <param name="node">The XML node</param>
    /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
    /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
    /// <param name="culture">Optional culture for conversion</param>
    /// <returns>An double value (0 if parsing fails)</returns>
    public static double GetDoubleSafe(
        this XmlNode node,
        string xpath,
        XmlNamespaceManager namespaceManager,
        CultureInfo culture = null)
    {
        var element = node.SelectSingleNode(xpath, namespaceManager);

        if (element != null)
        {
            if (culture == null)
            {
                culture = ServiceHandler.DataCulture;
            }

            if (double.TryParse(element.InnerText, NumberStyles.Any, culture, out var result))
            {
                return result;
            }

            return 0;
        }

        return 0;
    }

    /// <summary>
    ///     Gets a strongly typed float value from a given XPath. Returns 0 if parsing fails.
    /// </summary>
    /// <param name="node">The XML node</param>
    /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
    /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
    /// <param name="culture">Optional culture for conversion</param>
    /// <returns>An double value (0 if parsing fails)</returns>
    public static float GetFloatSafe(
        this XmlNode node, 
        string xpath, 
        XmlNamespaceManager namespaceManager,
        CultureInfo culture = null)
    {
        var element = node.SelectSingleNode(xpath, namespaceManager);
        
        if (element != null)
        {
            if (culture == null)
            {
                culture = ServiceHandler.DataCulture;
            }

            if (float.TryParse(element.InnerText, NumberStyles.Any, culture, out var result))
            {
                return result;
            }

            return 0;
        }

        return 0;
    }

    /// <summary>
    ///     Gets a strongly typed DateTime value from a given XPath. Returns DateTime.MinValue if parsing fails.
    /// </summary>
    /// <param name="node">The XML node</param>
    /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
    /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
    /// <returns>An DateTime value (DateTime.MinValue if parsing fails)</returns>
    public static DateTime GetDateTimeSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
    {
        var element = node.SelectSingleNode(xpath, namespaceManager);
        
        if (element != null)
        {
            if (DateTime.TryParse(element.InnerText, out var result))
            {
                return result;
            }

            return DateTime.MinValue;
        }

        return DateTime.MinValue;
    }

    /// <summary>
    ///     Gets a strongly typed boolean value from a given XPath. Returns false if parsing fails.
    /// </summary>
    /// <param name="node">The XML node</param>
    /// <param name="xpath">Selects the first XmlNode that matches the XPath expression</param>
    /// <param name="namespaceManager">An XmlNamespaceManager to use for resolving namespaces</param>
    /// <returns>An boolean value (false if parsing fails)</returns>
    public static bool GetBoolTimeSafe(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
    {
        var element = node.SelectSingleNode(xpath, namespaceManager);
        
        if (element != null)
        {
            if (bool.TryParse(element.InnerText, out var result))
            {
                return result;
            }

            return false;
        }

        return false;
    }
}