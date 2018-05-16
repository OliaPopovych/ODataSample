using ODataSample.Models;
using ODataSample.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace ODataSample.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly IBaseRepository<Product> productRepository;

        public ProductsController(IBaseRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        [EnableQuery]
        public IQueryable<Product> Get()
        {
             return productRepository.GetAll();
        }

        [EnableQuery]
        public Product Get([FromODataUri] int key)
        {
            var product = productRepository.GetById(key);
            return product;
        }

        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await productRepository.Create(product);
            return Created(product);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int id, Delta<Product> deltaProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await productRepository.Edit(id, deltaProduct);

            if (entity == null)
            {
                return NotFound();
            }

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put ([FromODataUri] int id, Product updateProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await productRepository.FullUpdate(id, updateProduct);

            if (updatedProduct == null)
            {
                return BadRequest();
            }

            return Updated(updatedProduct);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int id)
        {
            var product = await productRepository.Delete(id);

            if (product == null)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}