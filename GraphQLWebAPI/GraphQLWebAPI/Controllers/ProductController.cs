using GraphQL;
using GraphQL.Types;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GraphQLWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private ISchema _schema;
        private IDocumentWriter _documentWriter;
        private IDocumentExecuter _documentExecuter;

        public ProductController(ISchema schema, IDocumentWriter documentWriter, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentWriter = documentWriter;
            _documentExecuter = documentExecuter;
        }

        [HttpGet]
        public void Get()
        {
            GraphQLRequest graphQlRequest = new GraphQLRequest()
            {
                Query = @"query{products{id name}}"
            };

            ExecutionOptions executionOptions = new ExecutionOptions()
            {
                Schema = _schema,
                Query = graphQlRequest.Query,
                //Inputs = graphQlRequest.Inputs
            };

            ExecutionResult executionResult = _documentExecuter.ExecuteAsync(executionOptions).Result;

            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = executionResult.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            _documentWriter.WriteAsync(HttpContext.Response.Body, executionResult);
        }
    }
}
