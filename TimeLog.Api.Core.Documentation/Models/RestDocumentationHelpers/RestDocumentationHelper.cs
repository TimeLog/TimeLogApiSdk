using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers.Core;

namespace TimeLog.Api.Core.Documentation.Models.RestDocumentationHelpers
{
    public class RestDocumentationHelper
    {
        #region Variables

        public IEnumerable<RestTypeDoc> Types { get; }

        public IEnumerable<RestMethodDoc> Methods { get; }

        public IEnumerable<RestDefinition> Definitions { get; }

        #endregion

        #region Constructor

        public RestDocumentationHelper(string path)
        {
            string _fileJsonText;

            using (var _reader = new StreamReader(path))
            {
                _fileJsonText = _reader.ReadToEnd();
            }

            var _fileDoc = JsonConvert.DeserializeObject<JObject>(_fileJsonText);
            var _restDoc = GetRestDoc(_fileDoc);

            Definitions = GetDefinitions(_fileDoc).ToList();
            Types = MapToTypeDoc(_restDoc);
            Methods = GenerateMethodDocs();
        }

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
                .SelectToken("definitions")
                .OfType<JProperty>()
                .Select(definition =>
                    new RestDefinition(
                        definition.Name,
                        definition.Value["properties"]
                            .Children()
                            .OfType<JProperty>()
                            .Select(property => new RestProperty(
                                property.Name,
                                (string) property.Value["format"],
                                (string) property.Value["type"],
                                new RestRefSchema((string) property.Value["$ref"], null),
                                (string) property.Value["description"]
                            ))
                            .OrderBy(x => x.Name)
                            .ToList()
                    ))
                .ToList();
        }

        private RestDoc GetRestDoc(JObject fileDoc)
        {
            var _infoProperty = fileDoc.GetValue("info");

            return new RestDoc(
                new RestInfo(
                    (string)_infoProperty["version"],
                    (string) _infoProperty["title"],
                    (string) _infoProperty["description"]
                ),
                fileDoc
                    .SelectToken("tags")
                    .OfType<JObject>()
                    .Select(x => new RestTag(
                        x.Value<string>("name"),
                        x.Value<string>("description")
                    ))
                    .ToList(),
                fileDoc
                    .SelectToken("paths")
                    .Select(path =>
                    {
                        return new RestPath(
                            ((JProperty) path).Name,
                            path.First.Children()
                                .OfType<JProperty>()
                                .Select(action =>
                                {
                                    return new RestAction(
                                        ((JProperty) path).Name,
                                        action.Name,
                                        ((JArray) action.Value["tags"])
                                        .Select(tag => tag.Value<string>())
                                        .ToArray(),
                                        (string) action.Value["summary"],
                                        (string) action.Value["operationId"],
                                        ((JArray) action.Value["parameters"])
                                        .OfType<JObject>()
                                        .Select(parameter => new RestParameter(
                                            (string) parameter.GetValue("name"),
                                            (string) parameter.GetValue("description"),
                                            (string) parameter.GetValue("type"),
                                            (string) parameter.GetValue("format"),
                                            (string) parameter.GetValue("default"),
                                            new RestRefSchema((string) parameter.SelectToken("schema.$ref"), null)
                                        ))
                                        .ToList(),
                                        ((JObject) action.Value["responses"])
                                        .Properties()
                                        .Select(responseToken =>
                                        {
                                            var _tokenValue = (JObject) responseToken.Value;

                                            return new RestResponse(
                                                responseToken.Name,
                                                (string) _tokenValue["description"],
                                                new RestRefSchema((string) _tokenValue.SelectToken("schema.$ref"), null)
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
}