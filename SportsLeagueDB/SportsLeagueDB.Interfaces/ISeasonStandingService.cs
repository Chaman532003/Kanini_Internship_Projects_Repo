using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ISeasonStandingService
    {
        Task<IEnumerable<SeasonStanding>> GetSeasonStandingsAsync();
        Task<SeasonStanding> GetSeasonStandingByIdAsync(int id);
        Task AddSeasonStandingAsync(SeasonStanding standing);
        Task UpdateSeasonStandingAsync(SeasonStanding standing);
        Task DeleteSeasonStandingAsync(int id);

        // Additional querying methods
        Task<IEnumerable<SeasonStanding>> GetStandingsBySeasonAsync(int seasonYear);
        Task<IEnumerable<SeasonStanding>> GetTopPerformingTeamsAsync(int topCount);
    }
}
