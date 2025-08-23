using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface IUserTeamService
    {
        Task<IEnumerable<UserTeam>> GetUserTeamsAsync();
        Task<UserTeam> GetUserTeamByIdAsync(int id);
        Task AddUserTeamAsync(UserTeam userTeam);
        Task UpdateUserTeamAsync(UserTeam userTeam);
        Task DeleteUserTeamAsync(int id);

        // Additional querying methods
        Task<IEnumerable<UserTeam>> GetUserTeamsByUserIdAsync(int userId);
        Task<IEnumerable<UserTeam>> GetUserTeamsByRoleAsync(string roleInTeam);
    }
}
