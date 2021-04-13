using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TimeLog.Api.Core.Documentation.Models
{
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
            var _documentation = XDocument.Load(path);
            var _typeDocs = new List<TypeDoc>();
            var _methodDocs = new List<MethodDoc>();
            var _fieldDocs = new List<FieldDoc>();

            foreach (XElement _element in _documentation.Descendants("member"))
            {
                var _name = _element.Attribute("name")?.Value ?? string.Empty;

                if (_name.StartsWith("T:"))
                {
                    var _newType = new TypeDoc(_element);
                    _typeDocs.Add(_newType);

                    _methodDocs.AddRange(_newType.Methods);
                    _fieldDocs.AddRange(_newType.Fields);
                }
            }

            this.Types = _typeDocs;
            this.Methods = _methodDocs;
            this.Fields = _fieldDocs;
        }

        public IEnumerable<TypeDoc> Types { get; }

        public IEnumerable<MethodDoc> Methods { get; }

        public IEnumerable<FieldDoc> Fields { get; }

        public IEnumerable<TypeDoc> GetTypes(string nameRegexSearchPattern)
        {
            var _regExp = new Regex(nameRegexSearchPattern);
            return this.Types.Where(t => _regExp.IsMatch(t.FullName));
        }
    }
}