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

            var _fields = new List<FieldDoc>();
            var _properties = new List<FieldDoc>();
            var _methods = new List<MethodDoc>();

            var _attribute = element.Attribute("name")?.Value ?? string.Empty;
            this.FullName = _attribute.Replace("T:", string.Empty);
            this.Name = _attribute.Substring(_attribute.LastIndexOf('.') + 1, _attribute.Length - _attribute.LastIndexOf('.') - 1);

            this.Summary = element.Element("summary")?.Value ?? string.Empty;
            this.Remarks = element.Element("remarks")?.Value ?? string.Empty;

            if (element.Document != null)
            {
                foreach (XElement _member in element.Document.Descendants("member"))
                {
                    var _name = _member.Attribute("name")?.Value ?? string.Empty;
                    if (_name.StartsWith("M:" + this.FullName + "."))
                    {
                        _methods.Add(new MethodDoc(this, _member));
                    }
                    else if (_name.StartsWith("F:" + this.FullName + "."))
                    {
                        _fields.Add(new FieldDoc(_member));
                    }
                }
            }

            this.Fields = _fields;
            this.Methods = _methods;
            this.Properties = _properties;
        }

        public string Name { get; }

        public string FullName { get; }

        public string Summary { get; }

        public string Remarks { get; }

        public IEnumerable<FieldDoc> Fields { get; }

        public IEnumerable<FieldDoc> Properties { get; }

        public IEnumerable<MethodDoc> Methods { get; }
    }
}