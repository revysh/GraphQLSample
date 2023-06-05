using GraphiQl;
using GraphQL.Server;
using GraphQL.Types;
using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Query;
using GraphQLWebAPI.Schema;
using GraphQLWebAPI.Services;
using GraphQLWebAPI.Type;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IProduct, ProductService>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<ProductQuery>();
builder.Services.AddSingleton<ISchema, ProductSchema>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL(options => {
    options.EnableMetrics = false;
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
