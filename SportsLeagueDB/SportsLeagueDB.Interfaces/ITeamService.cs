using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);

        // Additional querying methods
        Task<IEnumerable<Team>> GetTeamsByCityAsync(string city);
        Task<IEnumerable<Team>> GetTopWinningTeamsAsync(int topCount);
    }
}
