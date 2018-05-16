using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public interface IBaseRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task<T> Create(T entity);
        Task<T> Edit(int id, Delta<T> delta);
        Task<T> FullUpdate(int id, T update);
        Task<T> Delete(int id);
    }
}
