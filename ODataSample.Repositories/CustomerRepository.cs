using ODataSample.Models;
using ODataSample.Repositories.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public class CustomerRepository : IBaseRepository<Customer>
    {
        private CustomContext db = new CustomContext();

        private bool CustomerExists(int key)
        {
            using (var db = new CustomContext())
            {
                return db.Customers.Any(p => p.CountryId == key);
            }
        }

        public IQueryable<Customer> GetAll()
        {
            using (var db = new CustomContext())
            {
                return db.Customers;
            }
        }

        public Customer GetById(int id)
        {
            using (var db = new CustomContext())
            {
                var result = db.Customers.Where(p => p.CountryId == id).FirstOrDefault();
                return result;
            }
        }

        public async Task<Customer> Create(Customer entity)
        {
            using (var db = new CustomContext())
            {
                db.Customers.Add(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<Customer> Edit(int id, Delta<Customer> delta)
        {
            using (var db = new CustomContext())
            {
                var entity = await db.Customers.FindAsync(id);

                delta.Patch(entity);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
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
        }

        public async Task<Customer> FullUpdate(int id, Customer update)
        {
            using (var db = new CustomContext())
            {
                db.Entry(update).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
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
        }

        public async Task<Customer> Delete(int id)
        {
            using (var db = new CustomContext())
            {
                var entity = await db.Customers.FindAsync(id);

                if (entity == null)
                {
                    return null;
                }

                db.Customers.Remove(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
