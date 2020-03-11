using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using evolutionPrueba.Controllers;
using evolutionPrueba.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using testEvolution.Models.Entities;
using testEvolution.Services;

namespace testEvolution.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly Response<User> _response;
        public UserController(IConfiguration configuration)
        {
            _userService = new UserService();
            _roleService = new RoleService();
            _configuration = configuration;
            _response = new Response<User>();
        }
        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ActionResult<User> Authenticate([FromBody]User user)
        {
            if (user.Username == null || user.Username.Length < 4) return BadRequest("Username is empty ó no cumple");
            User model = _userService.Find(user);
            if(model==null) return BadRequest(model);
            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            Console.WriteLine($"{model.roleId} - {model.Username}");
            Role role = _roleService.Find(model.roleId);
            // Creamos los claims (pertenencias, características) del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, role.Name),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            model.token = tokenHandler.WriteToken(createdToken);
            // return tokenHandler.WriteToken(createdToken);
            return Ok(new Response<User>(model,true,"su registro fue exitoso"));
        }

        
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return _userService.Find(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public ActionResult<User> Register(User user)
        {
            if (user.Username == null || user.Username.Length < 4) return BadRequest("Username is empty ó no cumple");
            User model = _userService.Add(user);
            if (model == null) return BadRequest(model);
            return Ok(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, [FromBody]User user)
        {
            Console.WriteLine(user.Username);
            if (_userService.Find(id) == null) return BadRequest("user is empty ó no cumple");
            User model = _userService.Edit(id, user);
            if (model == null) return BadRequest(model);
            return Ok(model);
        }

        //[AllowAnonymous]
        [Route("Autorization")]
        [HttpGet]
        public ActionResult<Boolean> Autorization()
        {
            return HttpContext.User.Identity.IsAuthenticated?true:false;
        }
    }
}