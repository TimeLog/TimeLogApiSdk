namespace TimeLog.Api.Documentation.Models
{
    using System.Xml.Linq;

    public class FieldDoc
    {
        public FieldDoc(XElement element)
        {
            var attribute = element.Attribute("name");
            if (attribute != null)
            {
                this.FullName = attribute.Value;
                this.Name = attribute.Value; //.Replace("F:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + ".", string.Empty).Replace("P:" + typeNamespace + "Header.", string.Empty);
            }

            var summary = element.Element("summary");
            if (summary != null)
            {
                this.Summary = summary.Value;
            }
        }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Summary { get; set; }

        public string Example { get; set; }
    }
}