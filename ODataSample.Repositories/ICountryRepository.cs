using ODataSample.Repositories.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public interface ICountryRepository: IDisposable
    {
        IQueryable<Country> GetAll();
        Country GetById(int id);
        Task<Country> Create(Country entity);
        Task<Country> Edit(int id, Delta<Country> delta);
        Task<Country> FullUpdate(int id, Country update);
        Task<Country> Delete(int id);
    }
}
