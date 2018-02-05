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

                var _command = args[0].Trim('/');

                switch (_command)
                {
                    case "m":
                    case "methods":
                        {
                            Console.WriteLine("List of available methods to query:");
                            var _methods = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IMethod).IsAssignableFrom(t) && !t.IsInterface);
                            foreach (var _method in _methods)
                            {
                                Console.WriteLine("\t" + _method.Name);
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

                            var _methodName = args[1];
                            FileInfo _outputFilePath = new FileInfo(_methodName + ".config");

                            ExportFormat _format;
                            switch (args[2].ToLowerInvariant())
                            {
                                case "csv":
                                    _format = ExportFormat.Csv;
                                    break;
                                case "xml":
                                    _format = ExportFormat.Xml;
                                    break;
                                default:
                                    Console.WriteLine("Format not recognized.");
                                    return;
                            }

                            var _methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == _methodName.ToLower());

                            if (_methodType != null)
                            {
                                IMethod _method = (IMethod)Activator.CreateInstance(_methodType);
                                using (TextWriter _writer = new StreamWriter(_outputFilePath.FullName, false, Encoding.UTF8))
                                {
                                    var _serializer = new XmlSerializer(typeof(OutputConfiguration));
                                    _serializer.Serialize(_writer, _method.GetConfiguration(_format));
                                    Console.WriteLine("Default configuration for \"" + _methodName + "\" saved to " + _outputFilePath.Name);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Method \"" + _methodName + "\" not found");
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

                            FileInfo _configImport = new FileInfo(args[1]);

                            if (!_configImport.FullName.Contains(".config"))
                            {
                                _configImport = new FileInfo(args[1] + ".config");
                            }

                            FileInfo _outputFilePath;

                            if (!_configImport.Exists)
                            {
                                Console.WriteLine("Invalid config file path");
                                return;
                            }

                            try
                            {
                                _outputFilePath = new FileInfo(args[2]);
                                if (_outputFilePath.Directory == null || !_outputFilePath.Directory.Exists)
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
                                OutputConfiguration _configuration = GetMethodClass<OutputConfiguration>(_configImport);
                                Console.WriteLine("Trying to run " + _configuration.Name + " with parameters:");
                                foreach (var _parameter in _configuration.Parameter)
                                {
                                    Console.WriteLine(_parameter.Name + ": " + _parameter.Value);
                                }

                                var _methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == _configuration.Name.ToLower());
                                if (_methodType != null)
                                {
                                    IMethod _method = (IMethod)Activator.CreateInstance(_methodType);
                                    var _outputNode = _method.GetData(_configuration);

                                    // Allow other namespaces
                                    var _listElementTypeName = _configuration.ListElementType.Contains(",")
                                        ? _configuration.ListElementType
                                        : _configuration.ListElementType + ",TimeLog.ReportingApi.SDK";
                                    var _listElementType = Type.GetType(_listElementTypeName);
                                    if (_listElementType == null)
                                    {
                                        throw new ArgumentException(string.Format("ListElementType is not recognized. Searching for \"{0}\"", _listElementTypeName));
                                    }

                                    SaveOutput(_listElementType, _configuration.ExportFormat, _outputNode, _outputFilePath);
                                }
                                else
                                {
                                    Console.WriteLine("Method \"" + _configuration.Name + "\" not found");
                                }
                            }
                            catch (Exception _ex)
                            {
                                Console.WriteLine(_ex.Message + _ex.StackTrace);
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
            catch (Exception _ex)
            {
                Console.WriteLine("The application failed with the following error:");
                Console.WriteLine(_ex.Message);
                Console.WriteLine(_ex.StackTrace);
            }

            //// Console.ReadKey();
        }

        private static T GetMethodClass<T>(FileInfo configFilePath)
        {
            var _methodName = XDocument.Load(configFilePath.FullName).Root.Name.ToString();
            var _methodType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.ToLower() == _methodName.ToLower());
            object _methodClass;
            using (TextReader _reader = new StreamReader(configFilePath.FullName, Encoding.UTF8))
            {
                var _serializer = new XmlSerializer(_methodType);
                _methodClass = _serializer.Deserialize(_reader);
            }

            return (T)_methodClass;
        }

        private static void SaveOutput(Type listElementType, ExportFormat format, XmlNode rawData, FileInfo outputFile)
        {
            switch (format)
            {
                case ExportFormat.Xml:
                    XDocument.Parse(rawData.OuterXml).Save(outputFile.FullName);
                    break;
                case ExportFormat.Csv:
                    var _document = XDocument.Parse(rawData.OuterXml);

                    var _mainElement = _document.Elements().FirstOrDefault();
                    bool _firstLine = true;

                    using (TextWriter _writer = new StreamWriter(outputFile.FullName, false, Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage)))
                    {
                        if (_mainElement != null)
                        {
                            // Find the properties that aren't ignored
                            var _headers = listElementType.GetProperties().Where(p => !p.CustomAttributes.Any(a => a.AttributeType.Name.Contains("Ignore"))).ToList();

                            // Print them to the file as CSV headers
                            _writer.WriteLine(string.Join(
                                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, 
                                _headers.Select(a => string.Format("\"{0}\"", a.Name))));

                            // Loop each XML element
                            foreach (XElement _childElement in _mainElement.Elements())
                            {
                                // Loop through each property in the header reference list
                                var _cells = new List<string>();
                                foreach (var _propertyInfo in _headers)
                                {
                                    var _elementvalue = _childElement.Elements().FirstOrDefault(e => e.Name.LocalName.ToLower() == _propertyInfo.Name.ToLower());
                                    var _attributevalue = _childElement.Attributes().FirstOrDefault(e => e.Name.LocalName.ToLower() == _propertyInfo.Name.ToLower());

                                    // Did any element or attribute fit?
                                    if (_elementvalue != null)
                                    {
                                        _cells.Add(_elementvalue.Value);
                                    }
                                    else if (_attributevalue != null)
                                    {
                                        _cells.Add(_attributevalue.Value);
                                    }
                                    else
                                    {
                                        // Empty is also fine
                                        _cells.Add(string.Empty);
                                    }
                                }

                                _writer.WriteLine(string.Join(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, _cells.Select(a => string.Format("\"{0}\"", a))));
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