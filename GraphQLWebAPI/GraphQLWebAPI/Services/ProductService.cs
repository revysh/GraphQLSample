﻿using GraphQLWebAPI.Interfaces;
using GraphQLWebAPI.Models;

namespace GraphQLWebAPI.Services
{
    public class ProductService : IProduct
    {
        private List<Product> products = new List<Product>(){
                new Product() {Id = 0, Name = "Coffee", Price = 10},
                new Product() {Id = 0, Name = "Tea", Price = 15},
            };

        public Product AddProduct(Product product)
        {
            products.Add(product);
            return product;
        }

        public void DeleteProduct(int id)
        {
            products.RemoveAt(id);
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            return products.First(x=>x.Id== id);
        }

        public Product UpdateProduct(int id, Product product)
        {
            products[id] = product;
            return product;
        }
    }
}