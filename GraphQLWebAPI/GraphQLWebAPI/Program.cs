using GraphiQl;
using GraphQL.Server;
using GraphQL.Types;
using GraphQLWebAPI.GraphQLFW.Query;
using GraphQLWebAPI.GraphQLFW.Schema;
using GraphQLWebAPI.GraphQLFW.Type;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IProduct, ProductService>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<ProductQuery>();
builder.Services.AddSingleton<GraphQL.Types.ISchema, ProductSchema>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL(options => {
    options.EnableMetrics = true;
}).AddSystemTextJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

//app.MapControllers();
app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();
app.Run();
