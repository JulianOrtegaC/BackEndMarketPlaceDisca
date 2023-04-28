using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Mvc;
namespace MarketPlaceDisca.Controllers
{

  

    
        [ApiController]
        [Route("[controller]")]
        public class DepartmentController : ControllerBase
        {

           
        private readonly DbMpdiscaContext _context;
    
        public DepartmentController(DbMpdiscaContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IEnumerable<Departament> Get()
            {
                return _context.Departaments.ToList();
            }
            [HttpGet]
            [Route("GetCities")]
            public IEnumerable<Municipio> GetCities(int codigoDepartamento)
            {
                return _context.Municipios.Where(x => x.Codigodepartamento == codigoDepartamento).ToList();



            }


        }
    }
