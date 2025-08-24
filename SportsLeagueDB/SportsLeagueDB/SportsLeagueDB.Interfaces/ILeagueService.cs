using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ILeagueService
    {
        Task<IEnumerable<SportsLeague>> GetLeaguesAsync();
        Task<SportsLeague> GetLeagueByIdAsync(int id);
        Task AddLeagueAsync(SportsLeague league);
        Task UpdateLeagueAsync(SportsLeague league);
        Task DeleteLeagueAsync(int id);

        // Additional querying methods
        Task<IEnumerable<SportsLeague>> GetLeaguesByCountryAsync(string country);
        Task<int> GetTotalTeamsCountAsync();
    }
}
