namespace TimeLog.Api.Documentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class MethodDoc
    {
        public MethodDoc()
        {
            this.Name = string.Empty;
            this.Summary = string.Empty;
            this.Params = new List<MethodParam>();
            this.Example = string.Empty;
            this.Exceptions = new List<MethodException>();
            this.Permission = string.Empty;
            this.Returns = string.Empty;
            this.SeeAlso = new List<TypeDoc>();
        }

        public MethodDoc(TypeDoc parent, XElement element)
            : this()
        {
            this.Parent = parent;

            var _attribute = element.Attribute("name")?.Value ?? string.Empty;
            this.FullName = _attribute.Replace("M:", string.Empty);

            var _firstParentesis = _attribute.IndexOf('(');
            if (_firstParentesis < 0)
            {
                _firstParentesis = _attribute.Length - 1;
            }

            var _lastDot = _attribute.LastIndexOf('.', _firstParentesis);
            this.Name = _attribute.Substring(_lastDot + 1, _firstParentesis - _lastDot - 1);
            this.FullyQuantifiedName = _attribute.Substring(0, _firstParentesis).Replace("M:", string.Empty);

            this.IsConstructor = this.Name.StartsWith("#");

            var _xmlSummary = element.Descendants("summary").FirstOrDefault();
            if (_xmlSummary != null)
            {
                this.Summary = _xmlSummary.Value;
            }

            var _xmlRemarks = element.Descendants("remarks").FirstOrDefault();
            if (_xmlRemarks != null)
            {
                this.Remarks = _xmlRemarks.Value;
            }

            var _xmlExample = element.Descendants("example").FirstOrDefault();
            if (_xmlExample != null)
            {
                this.Example = _xmlExample.Value;
            }

            var _xmlPermission = element.Descendants("permission").FirstOrDefault();
            if (_xmlPermission != null)
            {
                this.Permission = _xmlPermission.Value;
            }

            var _xmlReturns = element.Descendants("returns").FirstOrDefault();
            if (_xmlReturns != null)
            {
                this.Returns = _xmlReturns.Value;
            }

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

            _firstParentesis = this.FullName.IndexOf('(');
            var _parameterTypeList = _firstParentesis > 0 ? this.FullName.Substring(
                _firstParentesis + 1,
                this.FullName.LastIndexOf(")", StringComparison.Ordinal) - _firstParentesis - 1).Split(',')
                : new string[] { };
            var _parameterIndex = 0;

            foreach (var _param in element.Descendants("param"))
            {
                var _nameAttribute = _param.Attribute("name");
                if (_nameAttribute != null)
                {
                    this.Params.Add(new MethodParam(_nameAttribute.Value, _param.Value, _parameterTypeList[_parameterIndex]));
                }

                _parameterIndex++;
            }

            foreach (var _exc in element.Descendants("exception"))
            {
                var _crefAttribute = _exc.Attribute("cref");
                if (_crefAttribute != null)
                {
                    this.Exceptions.Add(new MethodException(_crefAttribute.Value, _exc.Value));
                }
            }
        }

        public string Name { get; set; }

        public string FullyQuantifiedName { get; set; }

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
    }
}