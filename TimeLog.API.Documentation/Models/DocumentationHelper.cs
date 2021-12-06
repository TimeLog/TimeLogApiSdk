using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TimeLog.Api.Documentation.Models;

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
        var documentation = XDocument.Load(path);
        var typeDocs = new List<TypeDoc>();
        var methodDocs = new List<MethodDoc>();
        var fieldDocs = new List<FieldDoc>();

        foreach (var element in documentation.Descendants("member"))
        {
            var name = element.Attribute("name")?.Value ?? string.Empty;

            if (name.StartsWith("T:"))
            {
                var newType = new TypeDoc(element);
                typeDocs.Add(newType);

                methodDocs.AddRange(newType.Methods);
                fieldDocs.AddRange(newType.Fields);
            }
        }

        Types = typeDocs;
        Methods = methodDocs;
        Fields = fieldDocs;
    }

    public IEnumerable<TypeDoc> Types { get; }

    public IEnumerable<MethodDoc> Methods { get; }

    public IEnumerable<FieldDoc> Fields { get; }

    public IEnumerable<TypeDoc> GetTypes(string nameRegexSearchPattern)
    {
        var regExp = new Regex(nameRegexSearchPattern);
        return Types.Where(t => regExp.IsMatch(t.FullName));
    }
}