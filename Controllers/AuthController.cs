using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using noticiasmvc.AppContext.Entities;
using noticiasmvc.Services;

namespace noticiasmvc.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {        
        private readonly IBaseService<Usuario> userService;
        private readonly IConfiguration configuration;

        public AuthController(IBaseService<Usuario> userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var users = await userService.GetAllAsync();
            var user = (await userService.FindAsync(u => u.Email == email && u.Senha == password)).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("Index");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}