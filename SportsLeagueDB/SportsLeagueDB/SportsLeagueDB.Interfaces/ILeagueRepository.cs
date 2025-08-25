using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ILeagueRepository
    {
        IQueryable<SportsLeague> GetAll();  // IQueryable for LINQ queries
        Task<SportsLeague> GetByIdAsync(int id);
        Task AddAsync(SportsLeague league);
        Task UpdateAsync(SportsLeague league);
        Task DeleteAsync(int id);
        
    }
}
