using GraphQLWebAPI.GraphQLFW.Query;

namespace GraphQLWebAPI.GraphQLFW.Schema
{
    public class ProductSchema : GraphQL.Types.Schema
    {
        public ProductSchema(ProductQuery productQuery)
        {
            Query = productQuery;
        }
    }
}
