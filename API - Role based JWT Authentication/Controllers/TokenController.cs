using GeneratingTokens.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeneratingTokens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly productDBContext _context;
        private readonly IConfiguration _config;

        public TokenController(productDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Login User and Generate Token
        [HttpPost("user-login")]
        public async Task<IActionResult> UserLogin([FromBody] User userData)
        {
            if (userData == null || string.IsNullOrEmpty(userData.user_email) || string.IsNullOrEmpty(userData.user_password))
                return BadRequest("Invalid request data");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.user_email == userData.user_email && u.user_password == userData.user_password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user.user_name, user.user_email, user.user_role);

            return Ok(new { token });
        }

        // Authorize Product Access with Token
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Check Authorization
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("You must provide a valid token");

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        // JWT Token Generation Method (Inside Controller itself)
        private string GenerateJwtToken(string username, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
