using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers;

public class RestDocumentationHelper
{
    #region Constructor

    public RestDocumentationHelper(string path)
    {
        string fileJsonText;

        using (var reader = new StreamReader(path))
        {
            fileJsonText = reader.ReadToEnd();
        }

        var fileDoc = JsonConvert.DeserializeObject<JObject>(fileJsonText);
        Definitions = GetDefinitions(fileDoc!).ToList();

        var restDoc = GetRestDoc(fileDoc!);
        Types = MapToTypeDoc(restDoc);
        Methods = GenerateMethodDocs();
    }

    #endregion

    #region Variables

    public IEnumerable<RestTypeDoc> Types { get; }

    public IEnumerable<RestMethodDoc> Methods { get; }

    public IEnumerable<RestDefinition> Definitions { get; }

    #endregion

    #region Internal and Private Implementations

    public IEnumerable<RestTypeDoc> GetTypes()
    {
        return Types.OrderBy(x => x.Name);
    }

    private IEnumerable<RestMethodDoc> GenerateMethodDocs()
    {
        return Types.SelectMany(x => x.Methods);
    }

    private IEnumerable<RestDefinition> GetDefinitions(JObject fileDoc)
    {
        return fileDoc
            .SelectToken("definitions")!
            .OfType<JProperty>()
            .Select(definition =>
                new RestDefinition(
                    definition.Name,
                    definition.Value["properties"]!
                        .Children()
                        .OfType<JProperty>()
                        .Select(property => new RestProperty(
                            property.Name,
                            (string) property.Value["format"]!,
                            (string) property.Value["type"]!,
                            new RestRefSchema((string) property.Value["$ref"]!, Definitions),
                            (string) property.Value["description"]!
                        ))
                        .OrderBy(x => x.Name)
                        .ToList()
                ))
            .ToList();
    }

    private RestDoc GetRestDoc(JObject fileDoc)
    {
        var infoProperty = fileDoc.GetValue("info");

        return new RestDoc(
            new RestInfo(
                (string) infoProperty["version"]!,
                (string) infoProperty["title"]!,
                (string) infoProperty["description"]!
            ),
            fileDoc
                .SelectToken("tags")!
                .OfType<JObject>()
                .Select(x => new RestTag(
                    x.Value<string>("name")!,
                    x.Value<string>("description")!
                ))
                .ToList(),
            fileDoc
                .SelectToken("paths")!
                .Select(path =>
                {
                    return new RestPath(
                        ((JProperty) path).Name,
                        path.First!.Children()
                            .OfType<JProperty>()
                            .Select(action =>
                            {
                                return new RestAction(
                                    ((JProperty) path).Name,
                                    action.Name,
                                    ((JArray) action.Value["tags"]!)
                                    .Select(tag => tag.Value<string>())
                                    .ToArray()!,
                                    (string) action.Value["summary"]!,
                                    (string) action.Value["operationId"]!,
                                    ((JArray) action.Value["parameters"]!)
                                    .OfType<JObject>()
                                    .Select(parameter => new RestParameter(
                                        (string) parameter.GetValue("name")!,
                                        (string) parameter.GetValue("description")!,
                                        (string) parameter.GetValue("type")!,
                                        (string) parameter.GetValue("format")!,
                                        (string) parameter.GetValue("default")!,
                                        new RestRefSchema((string) parameter.SelectToken("schema.$ref")!, Definitions)
                                    ))
                                    .ToList(),
                                    ((JObject) action.Value["responses"]!)
                                    .Properties()
                                    .Select(responseToken =>
                                    {
                                        var tokenValue = (JObject) responseToken.Value;

                                        return new RestResponse(
                                            responseToken.Name,
                                            (string) tokenValue["description"]!,
                                            new RestRefSchema((string) tokenValue.SelectToken("schema.$ref")!,
                                                Definitions)
                                        );
                                    })
                                    .ToList()
                                );
                            }).ToList()
                    );
                }).ToList()
        );
    }

    private IEnumerable<RestTypeDoc> MapToTypeDoc(RestDoc restDoc)
    {
        return restDoc
            .RestPaths
            .SelectMany(restPath => restPath.Actions)
            .GroupBy(x => x.Tags[0])
            .Select(grouped => new RestTypeDoc(
                grouped.Key,
                restDoc.RestInfo,
                restDoc.Tags.FirstOrDefault(x => x.ServiceName == grouped.Key),
                grouped.ToList()
            ));
    }

    #endregion
}