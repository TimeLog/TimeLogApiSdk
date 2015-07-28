namespace TimeLog.Api.Documentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
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

            var attribute = element.Attribute("name");
            this.FullName = attribute.Value.Replace("M:", string.Empty);

            var firstParentesis = attribute.Value.IndexOf('(');
            if (firstParentesis < 0)
            {
                firstParentesis = attribute.Value.Length - 1;
            }

            var lastDot = attribute.Value.LastIndexOf('.', firstParentesis);
            this.Name = attribute.Value.Substring(lastDot + 1, firstParentesis - lastDot - 1);
            this.FullyQuantifiedName = attribute.Value.Substring(0, firstParentesis).Replace("M:", string.Empty);

            this.IsConstructor = this.Name.StartsWith("#");

            var xmlSummary = element.Descendants("summary").FirstOrDefault();
            if (xmlSummary != null)
            {
                this.Summary = xmlSummary.Value;
            }

            var xmlRemarks = element.Descendants("remarks").FirstOrDefault();
            if (xmlRemarks != null)
            {
                this.Remarks = xmlRemarks.Value;
            }

            var xmlExample = element.Descendants("example").FirstOrDefault();
            if (xmlExample != null)
            {
                this.Example = xmlExample.Value;
            }

            var xmlPermission = element.Descendants("permission").FirstOrDefault();
            if (xmlPermission != null)
            {
                this.Permission = xmlPermission.Value;
            }

            var xmlReturns = element.Descendants("returns").FirstOrDefault();
            if (xmlReturns != null)
            {
                this.Returns = xmlReturns.Value;
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

            firstParentesis = this.FullName.IndexOf('(');
            var parameterTypeList = firstParentesis > 0 ? this.FullName.Substring(
                firstParentesis + 1, 
                this.FullName.LastIndexOf(")", StringComparison.Ordinal) - firstParentesis - 1).Split(',')
                : new string[] {};
            var parameterIndex = 0;

            foreach (var param in element.Descendants("param"))
            {
                var nameAttribute = param.Attribute("name");
                if (nameAttribute != null)
                {
                    this.Params.Add(new MethodParam(nameAttribute.Value, param.Value, parameterTypeList[parameterIndex]));
                }

                parameterIndex++;
            }

            foreach (var exc in element.Descendants("exception"))
            {
                var crefAttribute = exc.Attribute("cref");
                if (crefAttribute != null)
                {
                    this.Exceptions.Add(new MethodException(crefAttribute.Value, exc.Value));
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