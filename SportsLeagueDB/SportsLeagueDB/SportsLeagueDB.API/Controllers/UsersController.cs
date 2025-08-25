using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueDB.Core.DTOs;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsLeagueDB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var dtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRegistrationDto userDto)
        {
            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Role = userDto.Role
            };

            // Hash the password
            user.PasswordHash = hasher.HashPassword(user, userDto.Password);

            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId) return BadRequest();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.Role = userDto.Role;

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // Additional querying endpoints

        [HttpGet("ByRole/{role}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(string role)
        {
            var users = await _userService.GetUsersByRoleAsync(role);
            var dtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            });
            return Ok(dtos);
        }

        [HttpGet("SearchByName")]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchUsersByName([FromQuery] string namePart)
        {
            var users = await _userService.SearchUsersByNameAsync(namePart);
            var dtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            });
            return Ok(dtos);
        }
    }
}
