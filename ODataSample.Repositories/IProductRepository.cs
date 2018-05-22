using ODataSample.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Product> GetAll();
        Product GetById(int id);
        Task<Product> Create(Product entity);
        Task<Product> Edit(int id, Delta<Product> delta);
        Task<Product> FullUpdate(int id, Product update);
        Task<Product> Delete(int id);
    }
}
