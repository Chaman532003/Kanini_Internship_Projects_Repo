using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsLeagueDB.API.Controllers;
using SportsLeagueDB.Core.DTOs;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportsLeagueDB.Tests.Controllers
{
    public class TeamsControllerTests
    {
        private readonly Mock<ITeamService> _mockService;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _mockService = new Mock<ITeamService>();
            _controller = new TeamsController(_mockService.Object);
        }

        [Fact]
        public async Task GetTeams_ReturnsOk_WithTeams()
        {
            var teams = new List<Team>
            {
                new Team { TeamId = 1, TeamName = "Team A", City = "London", LeagueId = 1, TotalWins = 15 }
            };
            _mockService.Setup(s => s.GetTeamsAsync()).ReturnsAsync(teams);

            var result = await _controller.GetTeams();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TeamDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetTeam_ReturnsOk_WhenTeamExists()
        {
            var team = new Team { TeamId = 1, TeamName = "Team B", City = "Paris", LeagueId = 1, TotalWins = 10 };
            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync(team);

            var result = await _controller.GetTeam(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<TeamDto>(okResult.Value);
            Assert.Equal("Team B", dto.TeamName);
        }

        [Fact]
        public async Task GetTeam_ReturnsNotFound_WhenTeamDoesNotExist()
        {
            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync((Team)null);

            var result = await _controller.GetTeam(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateTeam_ReturnsCreatedAtAction()
        {
            var dto = new TeamDto { TeamId = 1, TeamName = "Team C", City = "Berlin", LeagueId = 2, TotalWins = 12 };

            _mockService.Setup(s => s.AddTeamAsync(It.IsAny<Team>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.CreateTeam(dto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTeam", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateTeam_ReturnsBadRequest_WhenIdMismatch()
        {
            var dto = new TeamDto { TeamId = 2, TeamName = "Mismatch" };

            var result = await _controller.UpdateTeam(1, dto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateTeam_ReturnsNotFound_WhenTeamDoesNotExist()
        {
            var dto = new TeamDto { TeamId = 1, TeamName = "Missing" };
            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync((Team)null);

            var result = await _controller.UpdateTeam(1, dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateTeam_ReturnsNoContent_WhenSuccessful()
        {
            var team = new Team { TeamId = 1, TeamName = "Old Name", City = "Rome", LeagueId = 1, TotalWins = 5 };
            var dto = new TeamDto { TeamId = 1, TeamName = "Updated Name", City = "Rome", LeagueId = 1, TotalWins = 8 };

            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync(team);
            _mockService.Setup(s => s.UpdateTeamAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);

            var result = await _controller.UpdateTeam(1, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTeam_ReturnsNotFound_WhenTeamDoesNotExist()
        {
            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync((Team)null);

            var result = await _controller.DeleteTeam(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteTeam_ReturnsNoContent_WhenSuccessful()
        {
            var team = new Team { TeamId = 1, TeamName = "Team D", City = "Madrid", LeagueId = 2, TotalWins = 7 };
            _mockService.Setup(s => s.GetTeamByIdAsync(1)).ReturnsAsync(team);
            _mockService.Setup(s => s.DeleteTeamAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteTeam(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTeamsByCity_ReturnsOk_WithResults()
        {
            var teams = new List<Team>
            {
                new Team { TeamId = 1, TeamName = "Team E", City = "London", LeagueId = 3, TotalWins = 20 }
            };
            _mockService.Setup(s => s.GetTeamsByCityAsync("London")).ReturnsAsync(teams);

            var result = await _controller.GetTeamsByCity("London");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TeamDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetTopWinningTeams_ReturnsOk_WithResults()
        {
            var teams = new List<Team>
            {
                new Team { TeamId = 1, TeamName = "Team F", City = "Lisbon", LeagueId = 4, TotalWins = 25 }
            };
            _mockService.Setup(s => s.GetTopWinningTeamsAsync(1)).ReturnsAsync(teams);

            var result = await _controller.GetTopWinningTeams(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TeamDto>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}
