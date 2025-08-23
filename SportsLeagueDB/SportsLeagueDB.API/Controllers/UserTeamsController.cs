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
    public class UserTeamsController : ControllerBase
    {
        private readonly IUserTeamService _userTeamService;

        public UserTeamsController(IUserTeamService userTeamService)
        {
            _userTeamService = userTeamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeamDto>>> GetUserTeams()
        {
            var userTeams = await _userTeamService.GetUserTeamsAsync();
            var dtos = userTeams.Select(ut => new UserTeamDto
            {
                UserTeamId = ut.UserTeamId,
                UserId = ut.UserId,
                TeamId = ut.TeamId,
                RoleInTeam = ut.RoleInTeam,
                JoinDate = ut.JoinDate
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeamDto>> GetUserTeam(int id)
        {
            var userTeam = await _userTeamService.GetUserTeamByIdAsync(id);
            if (userTeam == null) return NotFound();

            var dto = new UserTeamDto
            {
                UserTeamId = userTeam.UserTeamId,
                UserId = userTeam.UserId,
                TeamId = userTeam.TeamId,
                RoleInTeam = userTeam.RoleInTeam,
                JoinDate = userTeam.JoinDate
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserTeam([FromBody] UserTeamDto userTeamDto)
        {
            var userTeam = new UserTeam
            {
                UserId = userTeamDto.UserId,
                TeamId = userTeamDto.TeamId,
                RoleInTeam = userTeamDto.RoleInTeam,
                JoinDate = userTeamDto.JoinDate
            };
            await _userTeamService.AddUserTeamAsync(userTeam);
            return CreatedAtAction(nameof(GetUserTeam), new { id = userTeam.UserTeamId }, userTeamDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserTeam(int id, [FromBody] UserTeamDto userTeamDto)
        {
            if (id != userTeamDto.UserTeamId) return BadRequest();

            var userTeam = await _userTeamService.GetUserTeamByIdAsync(id);
            if (userTeam == null) return NotFound();

            userTeam.UserId = userTeamDto.UserId;
            userTeam.TeamId = userTeamDto.TeamId;
            userTeam.RoleInTeam = userTeamDto.RoleInTeam;
            userTeam.JoinDate = userTeamDto.JoinDate;

            await _userTeamService.UpdateUserTeamAsync(userTeam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserTeam(int id)
        {
            var userTeam = await _userTeamService.GetUserTeamByIdAsync(id);
            if (userTeam == null) return NotFound();

            await _userTeamService.DeleteUserTeamAsync(id);
            return NoContent();
        }

        // Additional endpoints

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTeamDto>>> GetUserTeamsByUserId(int userId)
        {
            var userTeams = await _userTeamService.GetUserTeamsByUserIdAsync(userId);
            var dtos = userTeams.Select(ut => new UserTeamDto
            {
                UserTeamId = ut.UserTeamId,
                UserId = ut.UserId,
                TeamId = ut.TeamId,
                RoleInTeam = ut.RoleInTeam,
                JoinDate = ut.JoinDate
            });
            return Ok(dtos);
        }

        [HttpGet("ByRoleInTeam/{role}")]
        public async Task<ActionResult<IEnumerable<UserTeamDto>>> GetUserTeamsByRole(string role)
        {
            var userTeams = await _userTeamService.GetUserTeamsByRoleAsync(role);
            var dtos = userTeams.Select(ut => new UserTeamDto
            {
                UserTeamId = ut.UserTeamId,
                UserId = ut.UserId,
                TeamId = ut.TeamId,
                RoleInTeam = ut.RoleInTeam,
                JoinDate = ut.JoinDate
            });
            return Ok(dtos);
        }
    }
}
