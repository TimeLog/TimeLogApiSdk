namespace TimeLog.Api.Documentation.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class DocumentationHelper
    {
        //// N: = Namespace
        //// T: = Type i.e. class, interface, struct, enum or delegate
        //// F: = Field
        //// P: Property
        //// E: Event
        //// I: error unresolved construct

        public DocumentationHelper(string path)
        {
            var xDocumentation = XDocument.Load(path);
            var typeDocs = new List<TypeDoc>();
            var methodDocs = new List<MethodDoc>();
            var fieldDocs = new List<FieldDoc>();

            foreach (XElement element in xDocumentation.Descendants("member"))
            {
                var name = element.Attribute("name")?.Value;

                if (name != null && name.StartsWith("T:"))
                {
                    var newType = new TypeDoc(element);
                    typeDocs.Add(newType);

                    methodDocs.AddRange(newType.Methods);
                    fieldDocs.AddRange(newType.Fields);
                }
            }

            this.Types = typeDocs;
            this.Methods = methodDocs;
            this.Fields = fieldDocs;
        }

        public IEnumerable<TypeDoc> Types { get; }
        public IEnumerable<MethodDoc> Methods { get; private set; }
        public IEnumerable<FieldDoc> Fields { get; private set; }

        public IEnumerable<TypeDoc> GetTypes(string nameRegexSearchPattern)
        {
            var regExp = new Regex(nameRegexSearchPattern);
            return this.Types.Where(t => regExp.IsMatch(t.FullName));
        } 
    }
}