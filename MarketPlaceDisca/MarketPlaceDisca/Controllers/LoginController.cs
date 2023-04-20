using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MarketPlaceSoftware.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly DbMpdiscaContext _context;
        private readonly IConfiguration _confi;

        public LoginController(DbMpdiscaContext context, IConfiguration confi)
        {
            _context = context;
            _confi = confi;
        }




        public static string ToSHA256(string s)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(Registro person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Credential crede = _context.Credentials.Where(p => p.Username == person.Email).FirstOrDefault();

                if (crede == null)
                {
                    User personaux = _context.Users.Where(a => a.IdUser == person.IdUser).FirstOrDefault();
                    if (personaux == null)
                    {
                        User user1 = new User()
                        {
                            IdUser = person.IdUser,
                            NameUser = person.NameUser,
                            LastNameUser = person.LastNameUser,
                            Address = person.Address,
                            Telephone = person.Telephone,
                            Email = person.Email,
                            TypeDocument = person.TypeDocument,
                            Gender = person.Gender
                        };
                        _context.Users.Add(user1);
                        string passaux = ToSHA256(person.Password);
                        Credential cred = new Credential()
                        {
                            Username = person.Email,
                            Password = passaux,
                            UserIdUser = person.IdUser
                        };
                        _context.Credentials.Add(cred);
                        await _context.SaveChangesAsync();

                        return Created($"/User/{person.IdUser}", person);
                    }
                    else
                    {
                        return BadRequest(new { message = "Numero de documento repetido" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Username ya esta en uso" });
                }


            }
        }


    }
}
