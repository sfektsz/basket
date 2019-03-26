using Basket.API.Models;
using Basket.API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Models.DataManager
{
    public class ProductManager : IProductRepository<Product>
    {
        private ProductModel context = new ProductModel();

        public void Add(Product product)
        {
            context._products.Add(product);
        }

        public void Delete(Product product)
        {
            context._products.RemoveAll(p => p.Id == product.Id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.FindAll();
        }

        public Product GetById(long id)
        {
            return context._products.FirstOrDefault<Product>(p => p.Id == id);
        }

        public void Update(Product product)
        {
            context._products.Select(p => p.Id == product.Id ? product : p);
        }
    }
}