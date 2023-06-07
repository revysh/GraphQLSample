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
        private readonly IProduct _productService;
        private readonly ILogger<WeatherForecastController> _logger;
        private ISchema _schema;
        private IDocumentWriter _documentWriter;
        private IDocumentExecuter _documentExecuter;

        public ProductController(IProduct productService) => this._productService = productService;

        [HttpGet]
        public void Get()
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

            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = executionResult.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            _documentWriter.WriteAsync(HttpContext.Response.Body, executionResult);
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public Product Post(Product product)
        {
            return _productService.AddProduct(product);
        }

        [HttpPut("{id}")]
        public Product Put(int id, Product product)
        {
            return _productService.UpdateProduct(id, product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
