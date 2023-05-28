using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceDisca.Controllers
{
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly DbMpdiscaContext _context;

        public ServiceController(DbMpdiscaContext context)
        {
            _context = context;
        }

        // GET api/services

        /* public async Task<ActionResult<IEnumerable<Service>>> listServices()
         {
             return await _context.Services.ToListAsync();
         }*/
        [HttpGet]
        [Route("listServices")]
        public ActionResult<Service> listServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        [HttpGet]
        [Route("listServicesByIdUser")]
        public ActionResult<List<Service>> listServicesByIdUser(string idUser)
        {
            var services = _context.Services.Where(s => s.IdUser == idUser).ToList();
            return Ok(services);
        }

        // GET api/services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return BadRequest();
            }

            return service;
        }

        // POST api/services
        [HttpPost]
        [Route("createService")]

        public async Task<ActionResult<Service>> createService( [FromBody] Service service)
        {
         
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.IdService }, service);
        }


        // PUT api/services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.IdService)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.IdService == id);
        }

        // PATCH api/services/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchService(int id, JsonPatchDocument<Service> patchDoc)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(service, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!TryValidateModel(service))
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        


    }
}
