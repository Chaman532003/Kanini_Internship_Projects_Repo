using Microsoft.AspNetCore.Authorization;
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
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeaguesController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        // All roles can view leagues
        [Authorize(Roles = "Player,Manager,User,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagues()
        {
            var leagues = await _leagueService.GetLeaguesAsync();
            var dtos = leagues.Select(l => new LeagueDto
            {
                LeagueId = l.LeagueId,
                LeagueName = l.LeagueName,
                Country = l.Country,
                FoundedYear = l.FoundedYear,
                NumberOfTeams = l.NumberOfTeams
            });
            return Ok(dtos);
        }

        [Authorize(Roles = "Player,Manager,User,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueDto>> GetLeague(int id)
        {
            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null) return NotFound();

            var dto = new LeagueDto
            {
                LeagueId = league.LeagueId,
                LeagueName = league.LeagueName,
                Country = league.Country,
                FoundedYear = league.FoundedYear,
                NumberOfTeams = league.NumberOfTeams
            };

            return Ok(dto);
        }

        // Only Admin and Manager can create, update, delete
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult> CreateLeague([FromBody] LeagueDto leagueDto)
        {
            var league = new SportsLeague
            {
                LeagueName = leagueDto.LeagueName,
                Country = leagueDto.Country,
                FoundedYear = leagueDto.FoundedYear,
                NumberOfTeams = leagueDto.NumberOfTeams
            };
            await _leagueService.AddLeagueAsync(league);
            return CreatedAtAction(nameof(GetLeague), new { id = league.LeagueId }, leagueDto);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLeague(int id, [FromBody] LeagueDto leagueDto)
        {
            if (id != leagueDto.LeagueId) return BadRequest();

            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null) return NotFound();

            league.LeagueName = leagueDto.LeagueName;
            league.Country = leagueDto.Country;
            league.FoundedYear = leagueDto.FoundedYear;
            league.NumberOfTeams = leagueDto.NumberOfTeams;

            await _leagueService.UpdateLeagueAsync(league);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeague(int id)
        {
            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null) return NotFound();

            await _leagueService.DeleteLeagueAsync(id);
            return NoContent();
        }

        // All roles can query
        [Authorize(Roles = "Player,Manager,User,Admin")]
        [HttpGet("ByCountry/{country}")]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeaguesByCountry(string country)
        {
            var leagues = await _leagueService.GetLeaguesByCountryAsync(country);
            var dtos = leagues.Select(l => new LeagueDto
            {
                LeagueId = l.LeagueId,
                LeagueName = l.LeagueName,
                Country = l.Country,
                FoundedYear = l.FoundedYear,
                NumberOfTeams = l.NumberOfTeams
            });
            return Ok(dtos);
        }

        [Authorize(Roles = "Player,Manager,User,Admin")]
        [HttpGet("TotalTeamsCount")]
        public async Task<ActionResult<int>> GetTotalTeamsCount()
        {
            int count = await _leagueService.GetTotalTeamsCountAsync();
            return Ok(count);
        }
    }
}
