using GraphQL;
using GraphQL.Types;
using GraphQLWebAPI.GraphQLFW.Type;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Services;

namespace GraphQLWebAPI.GraphQLFW.Query
{
    public class ProductQuery : ObjectGraphType
    {
        IProduct _productService;
        public ProductQuery(IProduct productService)
        {
            _productService = productService;
            Field<ListGraphType<ProductType>>("products", 
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "pageNumber" }, new QueryArgument<IntGraphType> { Name = "pageSize" }),
                resolve: context => { return productService.GetAllProducts(context.GetArgument<int>("pageNumber", defaultValue:1), context.GetArgument<int>("pageSize", defaultValue: 1)); });
            Field<ProductType>("product", arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => { return productService.GetProductById(context.GetArgument<int>("id")); });
        }
    }
}
