namespace TimeLog.Api.Documentation.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class MethodDoc
    {
        private static readonly Regex NameRegex = new Regex("M:TimeLog\\.TLP\\.API\\.([^\\.]+)\\.([^\\.]+)\\.([^\\.]+)Service\\.([\\w]+)");

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

        public MethodDoc(XElement element)
            : this()
        {

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

            this.Summary = element.Descendants("summary").FirstOrDefault()?.Value;
            this.Remarks = element.Descendants("remarks").FirstOrDefault()?.Value;
            this.Example = element.Descendants("example").FirstOrDefault()?.Value;
            this.Permission = element.Descendants("permission").FirstOrDefault()?.Value;
            this.Returns = element.Descendants("returns").FirstOrDefault()?.Value;

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

            foreach (var param in element.Descendants("param"))
            {
                var nameAttribute = param.Attribute("name");
                if (nameAttribute != null)
                {
                    this.Params.Add(new MethodParam(nameAttribute.Value, param.Value));
                }
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

        public IList<TypeDoc> SeeAlso { get; set; }

        public IList<MethodParam> Params { get; set; }

        public IList<MethodException> Exceptions { get; set; }
    }
}