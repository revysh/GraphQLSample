using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProduct _productService;
        public ProductController(IProduct productService) => this._productService = productService;

        [HttpGet]
        public List<Product> Get()
        {
            return _productService.GetAllProducts();
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
