using GraphQLWebAPI.Models;

namespace GraphQLWebAPI.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAllProducts();

        Product AddProduct(Product product);

        Product UpdateProduct(int id, Product product);

        void DeleteProduct(int id);

        Product GetProductById(int id);
    }
}
