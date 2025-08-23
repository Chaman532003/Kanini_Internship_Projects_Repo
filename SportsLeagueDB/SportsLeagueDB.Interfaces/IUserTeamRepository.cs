using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface IUserTeamRepository
    {
        IQueryable<UserTeam> GetAll();
        Task<UserTeam> GetByIdAsync(int id);
        Task AddAsync(UserTeam userTeam);
        Task UpdateAsync(UserTeam userTeam);
        Task DeleteAsync(int id);
    }
}
