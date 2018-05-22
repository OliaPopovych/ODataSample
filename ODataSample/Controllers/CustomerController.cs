using ODataSample.Models;
using ODataSample.Repositories;
using ODataSample.Repositories.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace ODataSample.Controllers
{
    public class CustomersController : ODataController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [EnableQuery]
        public IQueryable<Customer> Get()
        {
            return customerRepository.GetAll();
        }

        [EnableQuery]
        public Customer Get([FromODataUri] int key)
        {
            var entity = customerRepository.GetById(key);
            return entity;
        }

        // GET /Customer(1)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return customerRepository.GetProducts(key);
        }

        public async Task<IHttpActionResult> Post(Customer entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await customerRepository.Create(entity);
            return Created(entity);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int id, Delta<Customer> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await customerRepository.Edit(id, delta);

            if (entity == null)
            {
                return NotFound();
            }

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int id, Customer update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await customerRepository.FullUpdate(id, update);

            if (updatedProduct == null)
            {
                return BadRequest();
            }

            return Updated(updatedProduct);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int id)
        {
            var entry = await customerRepository.Delete(id);

            if (entry == null)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            customerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}