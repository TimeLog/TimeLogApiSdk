using System;
using System.Reflection;
using TimeLog.ReportingApi.SDK;

namespace TimeLog.ReportingApi.Exporter
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using TimeLog.ReportingApi.Exporter.MethodTemplates;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                if (args.Length == 0)
                {
                    args = new[] { "/?" };
                }

                var command = args[0].Trim('/');

                switch (command)
                {
                    case "m":
                    case "methods":
                        {
                            Console.WriteLine("List of available methods to query:");
                            var methods = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IMethod).IsAssignableFrom(t) && !t.IsInterface);
                            foreach (var method in methods)
                            {
                                Console.WriteLine("\t" + method.Name);
                            }

                            break;
                        }
                    case "t":
                    case "test":
                        {
                            Console.WriteLine("Trying to authenticate using AppSettings:");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("TimeLogProjectUri: " + ServiceHandler.Instance.ServiceUrl);
                            Console.WriteLine("TimeLogProjectReportingSiteCode: " + ServiceHandler.Instance.SiteCode);
                            Console.WriteLine("TimeLogProjectReportingApiId: " + ServiceHandler.Instance.ApiId);
                            Console.WriteLine("TimeLogProjectReportingApiPassword: " + ServiceHandler.Instance.ApiPassword);
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("Result: " + ServiceHandler.Instance.TryAuthenticate());
                            break;
                        }
                    case "g":
                    case "generate":
                        {
                            if (args.Length != 3)
                            {
                                Console.WriteLine("Parameters invalid");
                                Console.WriteLine(string.Empty);
                                Console.WriteLine("Example usage: TimeLog.ReportingApi.Exporter /g [methodName] [format]");
                                Console.WriteLine("Example usage: TimeLog.ReportingApi.Exporter /g GetCustomersRaw csv");
                                Console.WriteLine(string.Empty);
                                Console.WriteLine("Formats available: Csv, xml");
                                return;
                            }

                            var methodName = args[1];
                            FileInfo outputFilePath = new FileInfo(methodName + ".config");

                            ExportFormat format;
                            switch (args[2].ToLowerInvariant())
                            {
                                case "csv":
                                    format = ExportFormat.Csv;
                                    break;
                                case "xml":
                                    format = ExportFormat.Xml;
                                    break;
                                default:
                                    Console.WriteLine("Format not recognized.");
                                    return;
                            }

                            var methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == methodName.ToLower());

                            if (methodType != null)
                            {
                                IMethod method = (IMethod)Activator.CreateInstance(methodType);
                                using (TextWriter writer = new StreamWriter(outputFilePath.FullName, false, Encoding.UTF8))
                                {
                                    var serializer = new XmlSerializer(typeof(OutputConfiguration));
                                    serializer.Serialize(writer, method.GetConfiguration(format));
                                    Console.WriteLine("Default configuration for \"" + methodName + "\" saved to " + outputFilePath.Name);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Method \"" + methodName + "\" not found");
                            }

                            break;
                        }
                    case "e":
                    case "export":
                        {
                            if (args.Length != 3)
                            {
                                Console.WriteLine("Parameters invalid");
                                Console.WriteLine(string.Empty);
                                Console.WriteLine("Example usage: TimeLog.ReportingApi.Exporter.exe /e parameterConfigPath outputFilePath");
                                Console.WriteLine(string.Empty);
                                Console.WriteLine("Use \"TimeLog.ReportingApi.Exporter.exe /g methodName\" for generate a sample parameter config file");
                                return;
                            }

                            FileInfo configImport = new FileInfo(args[1]);

                            if (!configImport.FullName.Contains(".config"))
                            {
                                configImport = new FileInfo(args[1] + ".config");
                            }

                            FileInfo outputFilePath;

                            if (!configImport.Exists)
                            {
                                Console.WriteLine("Invalid config file path");
                                return;
                            }

                            try
                            {
                                outputFilePath = new FileInfo(args[2]);
                                if (outputFilePath.Directory == null || !outputFilePath.Directory.Exists)
                                {
                                    Console.WriteLine("Invalid output file path");
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid output file path");
                                return;
                            }


                            try
                            {
                                OutputConfiguration configuration = GetMethodClass<OutputConfiguration>(configImport);
                                Console.WriteLine("Trying to run " + configuration.Name + " with parameters:");
                                foreach (var parameter in configuration.Parameter)
                                {
                                    Console.WriteLine(parameter.Name + ": " + parameter.Value);
                                }

                                var methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == configuration.Name.ToLower());
                                if (methodType != null)
                                {
                                    IMethod method = (IMethod)Activator.CreateInstance(methodType);
                                    var outputNode = method.GetData(configuration);

                                    // Allow other namespaces
                                    var listElementTypeName = configuration.ListElementType.Contains(",")
                                        ? configuration.ListElementType
                                        : configuration.ListElementType + ",TimeLog.ReportingApi.SDK";
                                    var listElementType = Type.GetType(listElementTypeName);
                                    if (listElementType == null)
                                    {
                                        throw new ArgumentException(string.Format("ListElementType is not recognized. Searching for \"{0}\"", listElementTypeName));
                                    }

                                    SaveOutput(listElementType, configuration.ExportFormat, outputNode, outputFilePath);
                                }
                                else
                                {
                                    Console.WriteLine("Method \"" + configuration.Name + "\" not found");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message + ex.StackTrace);
                            }

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Runs queries agains the TimeLog Project API and outputs the result in a file");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("TimeLog.ReportingApi.Exporter /e methodName outputFile");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("/t\tExecutes a credentials validation request");
                            Console.WriteLine("/g\tGenerates a default configuration for a given methodName");
                            Console.WriteLine("/e\tExports data from the methodName to the outputFile");
                            Console.WriteLine("/m\tLists all the available methods");
                            Console.WriteLine("/h\tShows this message");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("ExportFormats supported: Csv, Xml");
                            Console.WriteLine(string.Empty);
                            Console.WriteLine("Step 1: Setup connection details in the associated .config file");
                            Console.WriteLine("Step 2: Generate a method configuration file using the /g parameter");
                            Console.WriteLine("Step 3: Execute an export command using the /e parameter");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The application failed with the following error:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            //// Console.ReadKey();
        }

        private static T GetMethodClass<T>(FileInfo configFilePath)
        {
            var methodName = XDocument.Load(configFilePath.FullName).Root.Name.ToString();
            var methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == methodName.ToLower());
            object methodClass;
            using (TextReader reader = new StreamReader(configFilePath.FullName, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(methodType);
                methodClass = serializer.Deserialize(reader);
            }

            return (T)methodClass;
        }

        private static void SaveOutput(Type listElementType, ExportFormat format, XmlNode rawData, FileInfo outputFile)
        {
            switch (format)
            {
                case ExportFormat.Xml:
                    XDocument.Parse(rawData.OuterXml).Save(outputFile.FullName);
                    break;
                case ExportFormat.Csv:
                    var document = XDocument.Parse(rawData.OuterXml);

                    var mainElement = document.Elements().FirstOrDefault();
                    bool firstLine = true;

                    using (TextWriter writer = new StreamWriter(outputFile.FullName, false, Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage)))
                    {
                        if (mainElement != null)
                        {
                            // Find the properties that aren't ignored
                            var headers = listElementType.GetProperties().Where(p => !p.CustomAttributes.Any(a => a.AttributeType.Name.Contains("Ignore"))).ToList();

                            // Print them to the file as CSV headers
                            writer.WriteLine(string.Join(
                                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, 
                                headers.Select(a => string.Format("\"{0}\"", a.Name))));

                            // Loop each XML element
                            foreach (XElement childElement in mainElement.Elements())
                            {
                                // Loop through each property in the header reference list
                                var cells = new List<string>();
                                foreach (var propertyInfo in headers)
                                {
                                    var elementvalue = childElement.Elements().FirstOrDefault(e => e.Name.LocalName.ToLower() == propertyInfo.Name.ToLower());
                                    var attributevalue = childElement.Attributes().FirstOrDefault(e => e.Name.LocalName.ToLower() == propertyInfo.Name.ToLower());

                                    // Did any element or attribute fit?
                                    if (elementvalue != null)
                                    {
                                        cells.Add(elementvalue.Value);
                                    }
                                    else if (attributevalue != null)
                                    {
                                        cells.Add(attributevalue.Value);
                                    }
                                    else
                                    {
                                        // Empty is also fine
                                        cells.Add(string.Empty);
                                    }
                                }

                                writer.WriteLine(string.Join(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, cells.Select(a => string.Format("\"{0}\"", a))));
                            }
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(format.ToString(), format, null);
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("Output saved to " + outputFile.FullName);
        }
    }
}