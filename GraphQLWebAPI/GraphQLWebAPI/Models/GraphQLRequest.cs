using GraphQL.SystemTextJson;
using System.Text.Json.Serialization;

namespace GraphQLWebAPI.Models
{
    public sealed class GraphQLRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }

        //[JsonConverter(typeof(ObjectDictionaryConverter))]
        //public Dictionary<string, object> Variables { get; set; }
    }
}
