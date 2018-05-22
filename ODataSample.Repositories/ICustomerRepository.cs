using ODataSample.Models;
using ODataSample.Repositories.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IQueryable<Customer> GetAll();
        Customer GetById(int id);
        Task<Customer> Create(Customer entity);
        Task<Customer> Edit(int id, Delta<Customer> delta);
        Task<Customer> FullUpdate(int id, Customer update);
        Task<Customer> Delete(int id);
        Task<Customer> AddProduct(int customerId, int productId);
        Task<Customer> RemoveProduct(int customerId, int productId);
        IQueryable<Product> GetProducts(int id);
    }
}
