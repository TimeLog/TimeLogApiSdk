using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace TimeLog.Api.Core.Documentation.Models
{
    public class MethodDoc
    {
        public string Name { get; }

        public string FullyQuantifiedName { get; }

        public string FullName { get; set; }

        public string Summary { get; set; }

        public string Example { get; set; }

        public string Permission { get; set; }

        public string Returns { get; set; }

        public string Remarks { get; set; }

        public bool IsConstructor { get; set; }

        public TypeDoc Parent { get; set; }

        public IList<TypeDoc> SeeAlso { get; set; }

        public IList<MethodParam> Params { get; set; }

        public IList<MethodException> Exceptions { get; set; }

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
        }
        
        public string GetReportingExample()
        {
            FileInfo fi = new FileInfo(AppDomain.CurrentDomain..Server.MapPath("~/Source/Reporting/" + doc.Name + ".xml"));
            return fi.Exists ? File.ReadAllText(HttpContext.Current.Server.MapPath("~/Source/Reporting/" + doc.Name + ".xml")) : string.Empty;
        }
        
        public string GetReportingSchema()
        {
            FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath("~/Source/Reporting/" + doc.Name + ".xsd"));
            return fi.Exists ? File.ReadAllText(HttpContext.Current.Server.MapPath("~/Source/Reporting/" + doc.Name + ".xsd")) : string.Empty;
        }

        public MethodDoc(TypeDoc parent, XElement element)
            : this()
        {
            Parent = parent;

            var _attribute = element.Attribute("name")?.Value ?? string.Empty;
            FullName = _attribute.Replace("M:", string.Empty);

            var _firstParentesis = _attribute.IndexOf('(');
            if (_firstParentesis < 0) _firstParentesis = _attribute.Length - 1;

            var _lastDot = _attribute.LastIndexOf('.', _firstParentesis);
            Name = _attribute.Substring(_lastDot + 1, _firstParentesis - _lastDot - 1);
            FullyQuantifiedName = _attribute.Substring(0, _firstParentesis).Replace("M:", string.Empty);

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

            _firstParentesis = FullName.IndexOf('(');
            var _parameterTypeList = _firstParentesis > 0
                ? FullName.Substring(
                    _firstParentesis + 1,
                    FullName.LastIndexOf(")", StringComparison.Ordinal) - _firstParentesis - 1).Split(',')
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