using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Models.Repository
{
    public interface IProductRepository<T> : IProductReadOnlyRepository<T> where T : class
    {
        void Add(T product);

        void Update(T product);

        void Delete(T product);
    }
}