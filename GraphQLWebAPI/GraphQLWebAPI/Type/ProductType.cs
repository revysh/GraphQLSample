using GraphQL.Types;
using GraphQLWebAPI.Models;

namespace GraphQLWebAPI.Type
{
    public class ProductType:ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(f => f.Id);
            Field(f => f.Name);
            Field(f => f.Price);
        }
    }
}
