using ODataSample.Models;
using ODataSample.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public class CountryRepository : IBaseRepository<Country>
    {
        private CustomContext db = new CustomContext();

        private bool CountryExists(int key)
        {
            return db.Countries.Any(p => p.CountryId == key);
        }

        public IQueryable<Country> GetAll()
        {
              return db.Countries;
        }

        public Country GetById(int id)
        {
            var result = db.Countries.SingleOrDefault(p => p.CountryId == id);
            return result;
        }

        public async Task<Country> Create(Country entity)
        {
            db.Countries.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<Country> Edit(int id, Delta<Country> delta)
        {
            var entity = await db.Countries.FindAsync(id);

            delta.Patch(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return entity;
        }

        public async Task<Country> FullUpdate(int id, Country update)
        {
            db.Entry(update).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return update;
        }

        public async Task<Country> Delete(int id)
        {
            var entity = await db.Countries.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            db.Countries.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
