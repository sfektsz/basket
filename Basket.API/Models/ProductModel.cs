using Basket.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Models
{
    public class ProductModel
    {
        public readonly List<Product> _products;

        public ProductModel()
        {
            _products = new List<Product>()
            {
                new Product { Id = 1, Name = "Apple", Description = "Pink Lady", Price = 11 },
                new Product { Id = 2, Name = "Banana", Description = "Make nutritious snack!", Price = 22 },
                new Product { Id = 3, Name = "Cherries", Description = "Stone fruits", Price = 33 },
                new Product { Id = 4, Name = "Dates", Description = "The fruit of the palm tree", Price = 44 }
            };
        }

        public List<Product> FindAll()
        {
            return _products;
        }
    }
}
