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
    /// <summary>
    /// The Authentication controller, used for generating JWT access tokens for API access.
    /// </summary>
    [Route("auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        IConfiguration _configuration;

        /// <summary>
        /// The initializer for authentication, used to receive configuration file.
        /// </summary>
        /// <param name="configuration">The configuration singleton</param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST api/Auth
        /// <summary>
        /// The authentication endpoint for Boneless Pharmacy's API.
        /// 
        /// Requires a Staff member's login credentials to receive a 24 hour JWT.
        /// </summary>
        /// <param name="value">A JSON object containing a staff member's `name` and `password`</param>
        /// <returns>A single JWT</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> value) => await Task.Run<IActionResult>(() =>
        {
            using (var db = new Db())
            {
                if (!db.Staff.Any(s => s.Name == value["name"] && s.Password == value["password"]))
                    return Unauthorized();
            }
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
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        });
    }
}
