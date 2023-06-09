using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Query.Builder;
using GraphQL.Types;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;

namespace GraphQLWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private ISchema _schema;
        private IDocumentExecuter _documentExecuter;

        public ProductController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpGet]
        public void Get()
        {
            IQuery<Product> queryBuilder = new Query<Product>("products")
                .AddField(f => f.Id)
                .AddField(f => f.Name);

            string query = $"query{{{queryBuilder.Build().ToLower()}}}";


            ExecutionOptions executionOptions = new ExecutionOptions()
            {
                Schema = _schema,
                Query = query
            };

            ExecutionResult executionResult = _documentExecuter.ExecuteAsync(executionOptions).Result;

            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = executionResult.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
            var responseJson = new GraphQL.NewtonsoftJson.GraphQLSerializer().Serialize(executionResult);
        }
    }
}
