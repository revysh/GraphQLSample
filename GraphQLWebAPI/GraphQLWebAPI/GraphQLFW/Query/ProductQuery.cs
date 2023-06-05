using GraphQL;
using GraphQL.Types;
using GraphQLWebAPI.GraphQLFW.Type;
using GraphQLWebAPI.Interfaces;

namespace GraphQLWebAPI.GraphQLFW.Query
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProduct productService)
        {
            Field<ListGraphType<ProductType>>("products", resolve: context => { return productService.GetAllProducts(); });
            Field<ProductType>("product", arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => { return productService.GetProductById(context.GetArgument<int>("id")); });
        }
    }
}
