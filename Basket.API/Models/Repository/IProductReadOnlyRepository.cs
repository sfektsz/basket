using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Models.Repository
{
    public interface IProductReadOnlyRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(long id);
    }
}