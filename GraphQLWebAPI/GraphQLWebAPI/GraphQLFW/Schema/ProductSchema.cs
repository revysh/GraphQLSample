using GraphQLWebAPI.GraphQLFW.Mutation;
using GraphQLWebAPI.GraphQLFW.Query;

namespace GraphQLWebAPI.GraphQLFW.Schema
{
    public class ProductSchema : GraphQL.Types.Schema
    {
        public ProductSchema(ProductQuery productQuery, ProductMutation productMutation)
        {
            Query = productQuery;
            Mutation = productMutation;
        }
    }
}
