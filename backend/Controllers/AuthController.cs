using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST api/Auth
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post() => await Task.Run<IActionResult>(() =>
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Issuer"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                null,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: creds
                );
            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        });
    }
}
