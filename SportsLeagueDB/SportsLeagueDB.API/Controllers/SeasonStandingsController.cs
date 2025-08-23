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
    public class SeasonStandingsController : ControllerBase
    {
        private readonly ISeasonStandingService _seasonStandingService;

        public SeasonStandingsController(ISeasonStandingService seasonStandingService)
        {
            _seasonStandingService = seasonStandingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonStandingDto>>> GetSeasonStandings()
        {
            var standings = await _seasonStandingService.GetSeasonStandingsAsync();
            var dtos = standings.Select(ss => new SeasonStandingDto
            {
                StandingId = ss.StandingId,
                SeasonYear = ss.SeasonYear,
                Wins = ss.Wins,
                Losses = ss.Losses,
                Points = ss.Points,
                LeagueId = ss.LeagueId,
                TeamId = ss.TeamId
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonStandingDto>> GetSeasonStanding(int id)
        {
            var standing = await _seasonStandingService.GetSeasonStandingByIdAsync(id);
            if (standing == null) return NotFound();

            var dto = new SeasonStandingDto
            {
                StandingId = standing.StandingId,
                SeasonYear = standing.SeasonYear,
                Wins = standing.Wins,
                Losses = standing.Losses,
                Points = standing.Points,
                LeagueId = standing.LeagueId,
                TeamId = standing.TeamId
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSeasonStanding([FromBody] SeasonStandingDto standingDto)
        {
            var standing = new SeasonStanding
            {
                SeasonYear = standingDto.SeasonYear,
                Wins = standingDto.Wins,
                Losses = standingDto.Losses,
                Points = standingDto.Points,
                LeagueId = standingDto.LeagueId,
                TeamId = standingDto.TeamId
            };
            await _seasonStandingService.AddSeasonStandingAsync(standing);
            return CreatedAtAction(nameof(GetSeasonStanding), new { id = standing.StandingId }, standingDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSeasonStanding(int id, [FromBody] SeasonStandingDto standingDto)
        {
            if (id != standingDto.StandingId) return BadRequest();

            var standing = await _seasonStandingService.GetSeasonStandingByIdAsync(id);
            if (standing == null) return NotFound();

            standing.SeasonYear = standingDto.SeasonYear;
            standing.Wins = standingDto.Wins;
            standing.Losses = standingDto.Losses;
            standing.Points = standingDto.Points;
            standing.LeagueId = standingDto.LeagueId;
            standing.TeamId = standingDto.TeamId;

            await _seasonStandingService.UpdateSeasonStandingAsync(standing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeasonStanding(int id)
        {
            var standing = await _seasonStandingService.GetSeasonStandingByIdAsync(id);
            if (standing == null) return NotFound();

            await _seasonStandingService.DeleteSeasonStandingAsync(id);
            return NoContent();
        }

        // Additional endpoints

        [HttpGet("BySeason/{seasonYear}")]
        public async Task<ActionResult<IEnumerable<SeasonStandingDto>>> GetStandingsBySeason(int seasonYear)
        {
            var standings = await _seasonStandingService.GetStandingsBySeasonAsync(seasonYear);
            var dtos = standings.Select(ss => new SeasonStandingDto
            {
                StandingId = ss.StandingId,
                SeasonYear = ss.SeasonYear,
                Wins = ss.Wins,
                Losses = ss.Losses,
                Points = ss.Points,
                LeagueId = ss.LeagueId,
                TeamId = ss.TeamId
            });
            return Ok(dtos);
        }

        [HttpGet("TopPerforming/{count}")]
        public async Task<ActionResult<IEnumerable<SeasonStandingDto>>> GetTopPerformingTeams(int count)
        {
            var standings = await _seasonStandingService.GetTopPerformingTeamsAsync(count);
            var dtos = standings.Select(ss => new SeasonStandingDto
            {
                StandingId = ss.StandingId,
                SeasonYear = ss.SeasonYear,
                Wins = ss.Wins,
                Losses = ss.Losses,
                Points = ss.Points,
                LeagueId = ss.LeagueId,
                TeamId = ss.TeamId
            });
            return Ok(dtos);
        }
    }
}
