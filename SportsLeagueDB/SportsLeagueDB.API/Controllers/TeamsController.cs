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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var teams = await _teamService.GetTeamsAsync();
            var dtos = teams.Select(t => new TeamDto
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                City = t.City,
                LeagueId = t.LeagueId,
                TotalWins = t.TotalWins
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            var dto = new TeamDto
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                City = team.City,
                LeagueId = team.LeagueId,
                TotalWins = team.TotalWins
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TeamDto teamDto)
        {
            var team = new Team
            {
                TeamName = teamDto.TeamName,
                City = teamDto.City,
                LeagueId = teamDto.LeagueId,
                TotalWins = teamDto.TotalWins
            };
            await _teamService.AddTeamAsync(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, teamDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam(int id, [FromBody] TeamDto teamDto)
        {
            if (id != teamDto.TeamId) return BadRequest();

            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            team.TeamName = teamDto.TeamName;
            team.City = teamDto.City;
            team.LeagueId = teamDto.LeagueId;
            team.TotalWins = teamDto.TotalWins;

            await _teamService.UpdateTeamAsync(team);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }

        // Additional methods

        [HttpGet("ByCity/{city}")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsByCity(string city)
        {
            var teams = await _teamService.GetTeamsByCityAsync(city);
            var dtos = teams.Select(t => new TeamDto
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                City = t.City,
                LeagueId = t.LeagueId,
                TotalWins = t.TotalWins
            });
            return Ok(dtos);
        }

        [HttpGet("TopWinning/{count}")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTopWinningTeams(int count)
        {
            var teams = await _teamService.GetTopWinningTeamsAsync(count);
            var dtos = teams.Select(t => new TeamDto
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                City = t.City,
                LeagueId = t.LeagueId,
                TotalWins = t.TotalWins
            });
            return Ok(dtos);
        }
    }
}
