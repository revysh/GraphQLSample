using GraphQL;
using GraphQL.Types;
using GraphQLWebAPI.GraphQLFW.Type;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Models;

namespace GraphQLWebAPI.GraphQLFW.Mutation
{
    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation(IProduct productService) {
            Field<ProductType>("createProduct", arguments: new QueryArguments(new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: context => { return productService.AddProduct(context.GetArgument<Product>("product")); });

            Field<ProductType>("updateProduct", arguments: 
                new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" },
                new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: context => { 
                    return productService.UpdateProduct(context.GetArgument<int>("id"), context.GetArgument<Product>("product")); 
                });

            Field<StringGraphType>("deleteProduct", arguments:
                new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => {
                    productService.DeleteProduct(context.GetArgument<int>("id"));
                    return "";
                });
        }
    }
}
