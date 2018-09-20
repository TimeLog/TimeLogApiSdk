namespace TimeLog.Api.Documentation.Models
{
    using System.Xml.Linq;

    public class FieldDoc
    {
        public FieldDoc(XElement element)
        {
            var _attribute = element.Attribute("name");
            if (_attribute != null)
            {
                this.FullName = _attribute.Value;
                this.Name = _attribute.Value; //.Replace("F:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + "Header.", string.Empty);
            }

            var _summary = element.Element("summary");
            if (_summary != null)
            {
                this.Summary = _summary.Value;
            }
        }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Summary { get; set; }

        public string Example { get; set; }
    }
}