using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class MethodDoc
    {
        public string Name { get; }

        public string FullyQuantifiedName { get; }

        public string FullName { get; }

        public string Summary { get; }

        public string Example { get; }

        public string Permission { get; }

        public string Returns { get; }

        public string Remarks { get; }

        public bool IsConstructor { get; }

        public TypeDoc? Parent { get; }

        public IList<TypeDoc> SeeAlso { get; }

        public IList<MethodParam> Params { get; }

        public IList<MethodException> Exceptions { get; }

        public string OutputExample { get; private set; }

        public string OutputSchema { get; private set; }

        public MethodDoc()
        {
            Name = string.Empty;
            Summary = string.Empty;
            Params = new List<MethodParam>();
            Example = string.Empty;
            Exceptions = new List<MethodException>();
            Permission = string.Empty;
            Returns = string.Empty;
            SeeAlso = new List<TypeDoc>();

            FullyQuantifiedName = string.Empty;
            FullName = string.Empty;
            Remarks = string.Empty;

            OutputExample = string.Empty;
            OutputSchema = string.Empty;
        }

        public void InitializeReportingExampleAndSchema(string webRootPath)
        {
            FileInfo _outputExampleInfo = new(Path.Combine(webRootPath, "Reporting/" + Name + ".xml"));
            if (_outputExampleInfo.Exists)
            {
                OutputExample = File.ReadAllText(_outputExampleInfo.FullName);
            }
            
            FileInfo _fileInfo = new(Path.Combine(webRootPath, "Reporting/" + Name + ".xsd"));
            if (_fileInfo.Exists)
            {
                OutputSchema = File.ReadAllText(_fileInfo.FullName);
            }
        }

        public MethodDoc(TypeDoc parent, XElement element)
            : this()
        {
            Parent = parent;

            var _attribute = element.Attribute("name")?.Value ?? string.Empty;
            FullName = _attribute.Replace("M:", string.Empty);

            var _firstParenthesis = _attribute.IndexOf('(');
            if (_firstParenthesis < 0) _firstParenthesis = _attribute.Length - 1;

            var _lastDot = _attribute.LastIndexOf('.', _firstParenthesis);
            Name = _attribute.Substring(_lastDot + 1, _firstParenthesis - _lastDot - 1);
            FullyQuantifiedName = _attribute.Substring(0, _firstParenthesis).Replace("M:", string.Empty);

            IsConstructor = Name.StartsWith("#");

            var _xmlSummary = element.Descendants("summary").FirstOrDefault();
            if (_xmlSummary != null) Summary = _xmlSummary.Value;

            var _xmlRemarks = element.Descendants("remarks").FirstOrDefault();
            if (_xmlRemarks != null) Remarks = _xmlRemarks.Value;

            var _xmlExample = element.Descendants("example").FirstOrDefault();
            if (_xmlExample != null) Example = _xmlExample.Value;

            var _xmlPermission = element.Descendants("permission").FirstOrDefault();
            if (_xmlPermission != null) Permission = _xmlPermission.Value;

            var _xmlReturns = element.Descendants("returns").FirstOrDefault();
            if (_xmlReturns != null) Returns = _xmlReturns.Value;

            //foreach (var seealso in element.Descendants("seealso"))
            //{
            //    var crefAttribute = seealso.Attribute("cref");
            //    if (crefAttribute != null)
            //    {
            //        this.SeeAlso.Add(crefAttribute.Value.Contains("Proxy") ? DocumentationHelper.ApiProxyInstance.GetTypeDocumentation(crefAttribute.Value) : DocumentationHelper.InterfaceInstance.GetTypeDocumentation(crefAttribute.Value));
            //    }
            //}

            //foreach (var seealso in element.Descendants("see"))
            //{
            //    var crefAttribute = seealso.Attribute("cref");
            //    if (crefAttribute != null)
            //    {
            //        this.SeeAlso.Add(crefAttribute.Value.Contains("Proxy") ? DocumentationHelper.ApiProxyInstance.GetTypeDocumentation(crefAttribute.Value) : DocumentationHelper.InterfaceInstance.GetTypeDocumentation(crefAttribute.Value));
            //    }
            //}

            _firstParenthesis = FullName.IndexOf('(');
            var _parameterTypeList = _firstParenthesis > 0
                ? FullName.Substring(
                    _firstParenthesis + 1,
                    FullName.LastIndexOf(")", StringComparison.Ordinal) - _firstParenthesis - 1).Split(',')
                : new string[] { };
            var _parameterIndex = 0;

            foreach (var _param in element.Descendants("param"))
            {
                var _nameAttribute = _param.Attribute("name");
                if (_nameAttribute != null) Params.Add(new MethodParam(_nameAttribute.Value, _param.Value, _parameterTypeList[_parameterIndex]));

                _parameterIndex++;
            }

            foreach (var _exc in element.Descendants("exception"))
            {
                var _crefAttribute = _exc.Attribute("cref");
                if (_crefAttribute != null) Exceptions.Add(new MethodException(_crefAttribute.Value, _exc.Value));
            }
        }
    }
}