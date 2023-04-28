using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MarketPlaceDisca.Controllers
{
    public class UserController: ControllerBase
    {

        private readonly DbMpdiscaContext _context;
        public UserController(DbMpdiscaContext context) { 
            _context = context;
        }



        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> PatchUser(string id, [FromBody] User updateModel)
        {
            // Busca el usuario por su identificador en la base de datos
            var user = await _context.Users.FindAsync(id);

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
                user.Email = updateModel.Email;
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
