using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeagueDB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public TokenController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] User loginUser)
        {
            if (await IsValidUserCredentials(loginUser.Email, loginUser.PasswordHash))
            {
                var tokenString = GenerateJwtToken(loginUser.Email);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }

        private async Task<bool> IsValidUserCredentials(string email, string password)
        {
            // Validate user against the stored user data
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return false;

            // Here you should verify the password hash properly instead of direct comparison
            return user.PasswordHash == password;
        }

        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, email)
        // Add roles or other claims if needed
    };

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
