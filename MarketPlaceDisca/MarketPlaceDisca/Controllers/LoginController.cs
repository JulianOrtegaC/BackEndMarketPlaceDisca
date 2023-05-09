using MarketPlaceDisca.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MarketPlaceSoftware.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly DbMpdiscaContext _context;
        private readonly IConfiguration _confi;

        public LoginController(DbMpdiscaContext context, IConfiguration confi)
        {
            _context = context;
            _confi = confi;
        }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        [HttpPost]
        public IActionResult Login([FromQuery] string userName, [FromQuery] string password)
        {
            string auxPassword = ToSHA256(password);
            Credential creden = _context.Credentials.Where(c => c.Username == userName && c.Password == auxPassword).FirstOrDefault();
            if (creden != null)
            {
                User user = _context.Users.Where(p => p.IdUser == creden.UserIdUser).FirstOrDefault();
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(30);
                string token = generateToken(userName, expirationDate);
                return Ok(new { token = token, usuario = JsonSerializer.Serialize(user, jsonOptions) });


            }
            else
            {
                return BadRequest(new { message = "Credenciales incorrectas" });
            }
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


        [Route("cambiarcontrasena")]
        [HttpPut]
        public async Task<ActionResult> CambiarContrasena([FromBody] ChangePassword change)
        {
            string auxPassword = ToSHA256(change.ContrasenaActual);
            Credential creden = _context.Credentials.Where(c => c.Username == change.Email && c.Password == auxPassword).FirstOrDefault();
            if (creden != null)
            {
                string auxNewPassword = ToSHA256(change.NuevaContrasena);
                creden.Password = auxNewPassword;
                _context.SaveChanges();
                return Ok();
            }
            else {
                return BadRequest("La contraseña actual ingresada no es la correcta.");
            }
    
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
                            BirthDate = person.BirthDate,
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
                        return BadRequest(new { message = "Documento de Identidad  ya Registrado" });
                    }
                }
                else
                {
                    return BadRequest(new { message = " El Correo Electronico ya esta Registrado" });
                }


            }
        }


        [HttpGet]
        [Route("Tokenn")]
        public string generateToken(string username, DateTime expirationDate)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var keyByte = Encoding.ASCII.GetBytes(_confi.GetSection("settings").GetSection("secretKey").ToString());
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }


    }
}
