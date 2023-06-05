using GraphQLWebAPI.Query;

namespace GraphQLWebAPI.Schema
{
    public class ProductSchema:GraphQL.Types.Schema
    {
        public ProductSchema(ProductQuery productQuery)
        {
            Query= productQuery;
        }
    }
}
