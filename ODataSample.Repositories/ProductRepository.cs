using ODataSample.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace ODataSample.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private CustomContext db = new CustomContext();

        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.ProductId == key);
        }

        public IQueryable<Product> GetAll()
        {

            return db.Products;
        }

        public Product GetById(int id)
        {
            var result = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            return result;
        }

        public async Task<Product> Create(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Edit(int id, Delta<Product> deltaProduct)
        {
            var entity = await db.Products.FindAsync(id);

            deltaProduct.Patch(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        public async Task<Product> FullUpdate(int id, Product updateProduct)
        {
            db.Entry(updateProduct).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return updateProduct;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return product;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
