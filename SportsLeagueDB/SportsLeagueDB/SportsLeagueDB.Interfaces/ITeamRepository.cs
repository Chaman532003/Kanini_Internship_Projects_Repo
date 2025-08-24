using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ITeamRepository
    {
        IQueryable<Team> GetAll();
        Task<Team> GetByIdAsync(int id);
        Task AddAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(int id);
    }
}
