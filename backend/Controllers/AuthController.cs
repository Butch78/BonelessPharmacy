using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> staffDetails) => await Task.Run<IActionResult>(() =>
        {
            bool isAuthenticated = false;
            //TODO: Properly use Identity
            using (var db = new Db())
            {
                isAuthenticated = db.Staff.Any(s => s.Name == staffDetails["Name"] && s.Password == staffDetails["Password"]);
            }
            if (!isAuthenticated)
                return Unauthorized();
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims =  new[]{
                // new Claim(JwtRegisteredClaimNames.Sub, "alex_billson@outlook.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Issuer"],
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: creds
                );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        });
    }
}
