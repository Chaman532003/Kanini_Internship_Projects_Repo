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
    public class SeasonStandingsControllerTests
    {
        private readonly Mock<ISeasonStandingService> _mockService;
        private readonly SeasonStandingsController _controller;

        public SeasonStandingsControllerTests()
        {
            _mockService = new Mock<ISeasonStandingService>();
            _controller = new SeasonStandingsController(_mockService.Object);
        }

        [Fact]
        public async Task GetSeasonStandings_ReturnsOk_WithStandings()
        {
            var standings = new List<SeasonStanding>
            {
                new SeasonStanding { StandingId = 1, SeasonYear = 2024, Wins = 10, Losses = 5, Points = 30, LeagueId = 1, TeamId = 1 }
            };
            _mockService.Setup(s => s.GetSeasonStandingsAsync()).ReturnsAsync(standings);

            var result = await _controller.GetSeasonStandings();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SeasonStandingDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetSeasonStanding_ReturnsOk_WhenStandingExists()
        {
            var standing = new SeasonStanding { StandingId = 1, SeasonYear = 2024, Wins = 12, Losses = 3, Points = 36, LeagueId = 1, TeamId = 2 };
            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync(standing);

            var result = await _controller.GetSeasonStanding(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<SeasonStandingDto>(okResult.Value);
            Assert.Equal(36, dto.Points);
        }

        [Fact]
        public async Task GetSeasonStanding_ReturnsNotFound_WhenStandingDoesNotExist()
        {
            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync((SeasonStanding)null);

            var result = await _controller.GetSeasonStanding(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateSeasonStanding_ReturnsCreatedAtAction()
        {
            var dto = new SeasonStandingDto { StandingId = 1, SeasonYear = 2024, Wins = 15, Losses = 2, Points = 45, LeagueId = 1, TeamId = 3 };

            _mockService.Setup(s => s.AddSeasonStandingAsync(It.IsAny<SeasonStanding>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.CreateSeasonStanding(dto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetSeasonStanding", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateSeasonStanding_ReturnsBadRequest_WhenIdMismatch()
        {
            var dto = new SeasonStandingDto { StandingId = 2, SeasonYear = 2024 };

            var result = await _controller.UpdateSeasonStanding(1, dto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateSeasonStanding_ReturnsNotFound_WhenStandingDoesNotExist()
        {
            var dto = new SeasonStandingDto { StandingId = 1, SeasonYear = 2024 };
            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync((SeasonStanding)null);

            var result = await _controller.UpdateSeasonStanding(1, dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateSeasonStanding_ReturnsNoContent_WhenSuccessful()
        {
            var standing = new SeasonStanding { StandingId = 1, SeasonYear = 2023, Wins = 8, Losses = 7, Points = 24, LeagueId = 1, TeamId = 4 };
            var dto = new SeasonStandingDto { StandingId = 1, SeasonYear = 2024, Wins = 12, Losses = 3, Points = 36, LeagueId = 1, TeamId = 4 };

            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync(standing);
            _mockService.Setup(s => s.UpdateSeasonStandingAsync(It.IsAny<SeasonStanding>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.UpdateSeasonStanding(1, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSeasonStanding_ReturnsNotFound_WhenStandingDoesNotExist()
        {
            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync((SeasonStanding)null);

            var result = await _controller.DeleteSeasonStanding(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteSeasonStanding_ReturnsNoContent_WhenSuccessful()
        {
            var standing = new SeasonStanding { StandingId = 1, SeasonYear = 2022, Wins = 14, Losses = 4, Points = 42, LeagueId = 1, TeamId = 5 };
            _mockService.Setup(s => s.GetSeasonStandingByIdAsync(1)).ReturnsAsync(standing);
            _mockService.Setup(s => s.DeleteSeasonStandingAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteSeasonStanding(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetStandingsBySeason_ReturnsOk_WithResults()
        {
            var standings = new List<SeasonStanding>
            {
                new SeasonStanding { StandingId = 1, SeasonYear = 2024, Wins = 20, Losses = 2, Points = 60, LeagueId = 1, TeamId = 6 }
            };
            _mockService.Setup(s => s.GetStandingsBySeasonAsync(2024)).ReturnsAsync(standings);

            var result = await _controller.GetStandingsBySeason(2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SeasonStandingDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetTopPerformingTeams_ReturnsOk_WithResults()
        {
            var standings = new List<SeasonStanding>
            {
                new SeasonStanding { StandingId = 1, SeasonYear = 2024, Wins = 18, Losses = 1, Points = 54, LeagueId = 1, TeamId = 7 }
            };
            _mockService.Setup(s => s.GetTopPerformingTeamsAsync(1)).ReturnsAsync(standings);

            var result = await _controller.GetTopPerformingTeams(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SeasonStandingDto>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}
