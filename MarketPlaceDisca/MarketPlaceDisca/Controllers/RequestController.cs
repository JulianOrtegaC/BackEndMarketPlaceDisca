using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarketPlaceDisca.Controllers
{
    [Route("api/request")]
    public class RequestController: ControllerBase
    {
        private readonly DbMpdiscaContext _context;

        public RequestController(DbMpdiscaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("createRequest")]
        public async Task<ActionResult<Request>> CreateRequest([FromBody] Request request)
        {
            try
            {
                // Aquí puedes realizar cualquier validación o lógica de negocio antes de guardar la entidad Request

                _context.Requests.Add(request);
                await _context.SaveChangesAsync();

                return new OkObjectResult(request);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error o excepción aquí
                return new ObjectResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpGet]
        [Route("listServicesByIdUser")]
        public ActionResult<List<RequestAux>> listServicesByIdUser(string idUser)
        {
            List<RequestAux> filteredRequests = _context.Requests
             .Where(r => r.UserIdUser == idUser)
             .Join(
                 _context.Services,
                 request => request.ServiceIdService,
                 service => service.IdService,
                 (request, service) => new RequestAux
                 {
                     ServiceIdService = request.ServiceIdService,
                     UserIdUser = request.UserIdUser,
                     NameService = service.NameService,
                     Status = request.Status
                 }
             )
             .ToList();

            return new OkObjectResult(filteredRequests);
        }

        [HttpDelete]
        [Route("deleteRequest")]
        public IActionResult DeleteRequest(int serviceId, string userId)
        {
            var request = _context.Requests.FirstOrDefault(r => r.ServiceIdService == serviceId && r.UserIdUser == userId);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }



    }
}
