namespace TimeLog.Api.Documentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class TypeDoc
    {
        public TypeDoc(XElement element)
        {
            if (element == null)
            {
                throw new NullReferenceException("Parameter \"element\" cannot be null");
            }

            var fields = new List<FieldDoc>();
            var properties = new List<FieldDoc>();
            var methods = new List<MethodDoc>();

            var attribute = element.Attribute("name");
            this.FullName = attribute.Value.Replace("T:", string.Empty);
            this.Name = attribute.Value.Substring(attribute.Value.LastIndexOf('.') + 1, attribute.Value.Length - attribute.Value.LastIndexOf('.') - 1);

            this.Summary = element.Element("summary") != null ? element.Element("summary").Value : string.Empty;
            this.Remarks = element.Element("remarks") != null ? element.Element("remarks").Value : string.Empty;

            if (element.Document != null)
            {
                foreach (XElement member in element.Document.Descendants("member"))
                {
                    var name = member.Attribute("name") != null ? member.Attribute("name").Value : string.Empty;
                    if (name.StartsWith("M:" + this.FullName + "."))
                    {
                        methods.Add(new MethodDoc(this, member));
                    }
                    else if (name.StartsWith("F:" + this.FullName + "."))
                    {
                        fields.Add(new FieldDoc(member));
                    }
                }
            }

            this.Fields = fields;
            this.Methods = methods;
            this.Properties = properties;
        }

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public string Summary { get; private set; }

        public string Remarks { get; private set; }

        public IEnumerable<FieldDoc> Fields { get; private set; }

        public IEnumerable<FieldDoc> Properties { get; private set; }

        public IEnumerable<MethodDoc> Methods { get; private set; }
    }
}