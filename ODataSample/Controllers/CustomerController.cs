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
        private readonly IBaseRepository<Customer> customerRepository;

        public CustomersController(IBaseRepository<Customer> customerRepository)
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
    }
}