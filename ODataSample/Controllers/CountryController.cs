using ODataSample.Repositories;
using ODataSample.Repositories.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace ODataSample.Controllers
{
    
    public class CountriesController : ODataController
    {
        private readonly IBaseRepository<Country> countryRepository;

        public CountriesController(IBaseRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [ODataRoute("Countries")]
        [EnableQuery]
        public IQueryable<Country> Get()
        {
            return countryRepository.GetAll();
        }

        [EnableQuery]
        public Country Get([FromODataUri] int key)
        {
            var entity = countryRepository.GetById(key);
            return entity;
        }

        public async Task<IHttpActionResult> Post(Country entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await countryRepository.Create(entity);
            return Created(entity);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int id, Delta<Country> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await countryRepository.Edit(id, delta);

            if (entity == null)
            {
                return NotFound();
            }

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int id, Country update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await countryRepository.FullUpdate(id, update);

            if (updatedProduct == null)
            {
                return BadRequest();
            }

            return Updated(updatedProduct);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int id)
        {
            var entry = await countryRepository.Delete(id);

            if (entry == null)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            countryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}