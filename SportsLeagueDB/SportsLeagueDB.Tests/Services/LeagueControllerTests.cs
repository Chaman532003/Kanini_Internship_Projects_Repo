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
    public class LeaguesControllerTests
    {
        private readonly Mock<ILeagueService> _mockService;
        private readonly LeaguesController _controller;

        public LeaguesControllerTests()
        {
            _mockService = new Mock<ILeagueService>();
            _controller = new LeaguesController(_mockService.Object);
        }

        [Fact]
        public async Task GetLeagues_ReturnsOk_WithLeagues()
        {
            // Arrange
            var leagues = new List<SportsLeague>
            {
                new SportsLeague { LeagueId = 1, LeagueName = "Premier League", Country = "England", FoundedYear = 1992, NumberOfTeams = 20 }
            };
            _mockService.Setup(s => s.GetLeaguesAsync()).ReturnsAsync(leagues);

            // Act
            var result = await _controller.GetLeagues();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<LeagueDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetLeague_ReturnsOk_WhenLeagueExists()
        {
            var league = new SportsLeague { LeagueId = 1, LeagueName = "La Liga", Country = "Spain" };
            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync(league);

            var result = await _controller.GetLeague(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<LeagueDto>(okResult.Value);
            Assert.Equal("La Liga", dto.LeagueName);
        }

        [Fact]
        public async Task GetLeague_ReturnsNotFound_WhenLeagueDoesNotExist()
        {
            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync((SportsLeague)null);

            var result = await _controller.GetLeague(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateLeague_ReturnsCreatedAtAction()
        {
            var dto = new LeagueDto { LeagueId = 1, LeagueName = "Serie A", Country = "Italy" };

            _mockService.Setup(s => s.AddLeagueAsync(It.IsAny<SportsLeague>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.CreateLeague(dto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetLeague", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateLeague_ReturnsBadRequest_WhenIdMismatch()
        {
            var dto = new LeagueDto { LeagueId = 2, LeagueName = "Bundesliga" };

            var result = await _controller.UpdateLeague(1, dto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateLeague_ReturnsNotFound_WhenLeagueDoesNotExist()
        {
            var dto = new LeagueDto { LeagueId = 1, LeagueName = "Bundesliga" };
            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync((SportsLeague)null);

            var result = await _controller.UpdateLeague(1, dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateLeague_ReturnsNoContent_WhenSuccessful()
        {
            var league = new SportsLeague { LeagueId = 1, LeagueName = "Bundesliga" };
            var dto = new LeagueDto { LeagueId = 1, LeagueName = "Bundesliga Updated" };

            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync(league);
            _mockService.Setup(s => s.UpdateLeagueAsync(It.IsAny<SportsLeague>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.UpdateLeague(1, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLeague_ReturnsNotFound_WhenLeagueDoesNotExist()
        {
            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync((SportsLeague)null);

            var result = await _controller.DeleteLeague(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteLeague_ReturnsNoContent_WhenSuccessful()
        {
            var league = new SportsLeague { LeagueId = 1, LeagueName = "Eredivisie" };
            _mockService.Setup(s => s.GetLeagueByIdAsync(1)).ReturnsAsync(league);
            _mockService.Setup(s => s.DeleteLeagueAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteLeague(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetLeaguesByCountry_ReturnsOk_WithResults()
        {
            var leagues = new List<SportsLeague>
            {
                new SportsLeague { LeagueId = 1, LeagueName = "Ligue 1", Country = "France" }
            };
            _mockService.Setup(s => s.GetLeaguesByCountryAsync("France")).ReturnsAsync(leagues);

            var result = await _controller.GetLeaguesByCountry("France");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<LeagueDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetTotalTeamsCount_ReturnsOk()
        {
            _mockService.Setup(s => s.GetTotalTeamsCountAsync()).ReturnsAsync(50);

            var result = await _controller.GetTotalTeamsCount();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(50, okResult.Value);
        }
    }
}
