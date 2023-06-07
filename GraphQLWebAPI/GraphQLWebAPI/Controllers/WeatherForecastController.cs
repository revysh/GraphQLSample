using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQLWebAPI.GraphQLFW.Query;
using GraphQLWebAPI.GraphQLFW.Schema;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using DocumentWriter = GraphQL.SystemTextJson.DocumentWriter;

namespace GraphQLWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private ISchema _schema;
        private IDocumentWriter _documentWriter;
        private IDocumentExecuter _documentExecuter;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISchema schema, IDocumentWriter documentWriter, IDocumentExecuter documentExecuter)
        {
            _logger = logger;
            _schema = schema;
            _documentWriter = documentWriter;
            _documentExecuter = documentExecuter;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //ExecutionOptions executionOptions = new ExecutionOptions()
            //{
            //    Schema = _schema,
            //    Query = graphQlRequest.Query,
            //    Inputs = graphQlRequest.Variables?.ToInputs()
            //};

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "WeatherForecastPost")]
        public void Post()
        {
            GraphQLRequest graphQlRequest = new GraphQLRequest()
            {
                OperationName = "123",
                Query = @"query{products{id name}}"
            };

            ExecutionOptions executionOptions = new ExecutionOptions()
            {
                Schema = _schema,
                Query = graphQlRequest.Query,
                //Inputs = graphQlRequest.Inputs
            };

            ExecutionResult executionResult = _documentExecuter.ExecuteAsync(executionOptions).Result;
            WriteResponseAsync(HttpContext, executionResult);

        }

        private void WriteResponseAsync(HttpContext context, ExecutionResult result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            _documentWriter.WriteAsync(context.Response.Body, result);
        }
    }
}