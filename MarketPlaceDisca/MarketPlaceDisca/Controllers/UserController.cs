using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MarketPlaceDisca.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly DbMpdiscaContext _context;
        public UserController(DbMpdiscaContext context)
        {
            _context = context;
        }



        [Route("editUser/{id}")]
        [HttpPatch]
        public async Task<ActionResult<User>> PatchUser(string id, [FromBody] User updateModel)
        {
            // Busca el usuario por su identificador en la base de datos
            var user = await _context.Users.FindAsync(id);
            var emailaux = user.Email;

            if (user == null)
            {
                return NotFound("No se encontró el usuario");
            }

            // Actualiza las propiedades que se enviaron en el objeto UserUpdateModel
            if (!string.IsNullOrEmpty(updateModel.NameUser))
            {
                user.NameUser = updateModel.NameUser;
            }
            if (!string.IsNullOrEmpty(updateModel.LastNameUser))
            {
                user.LastNameUser = updateModel.LastNameUser;
            }
            if (!string.IsNullOrEmpty(updateModel.Address))
            {
                user.Address = updateModel.Address;
            }
            if (!string.IsNullOrEmpty(updateModel.Telephone))
            {
                user.Telephone = updateModel.Telephone;
            }
            if (!string.IsNullOrEmpty(updateModel.Email))
            {
                var userAux = await _context.Users.SingleOrDefaultAsync(u => u.Email == updateModel.Email);

                if (userAux == null)
                {
                    var credenAux = await _context.Credentials.SingleOrDefaultAsync(u => u.Username == emailaux);

                    credenAux.Username = updateModel.Email;
                    user.Email = updateModel.Email;
                }
                else
                {
                    return BadRequest("El correo ingresado ya esta en uso");
                }

            }
            if (!string.IsNullOrEmpty(updateModel.TypeDocument))
            {
                user.TypeDocument = updateModel.TypeDocument;
            }
            if (!string.IsNullOrEmpty(updateModel.Gender))
            {
                user.Gender = updateModel.Gender;
            }
            if (!string.IsNullOrEmpty(updateModel.Photo))
            {
                user.Photo = updateModel.Photo;
            }
            if (!string.IsNullOrEmpty(updateModel.CoverPhoto))
            {
                user.CoverPhoto = updateModel.CoverPhoto;
            }

            if (updateModel.BirthDate != default(DateOnly))
            {
                user.BirthDate = updateModel.BirthDate;
            }

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Retorna el usuario actualizado
            return user;
        }


    }
}
