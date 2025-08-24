using SportsLeagueDB.Core.Models;

namespace SportsLeagueDB.SportsLeagueDB.Interfaces
{
    public interface ISeasonStandingRepository
    {
        IQueryable<SeasonStanding> GetAll();
        Task<SeasonStanding> GetByIdAsync(int id);
        Task AddAsync(SeasonStanding standing);
        Task UpdateAsync(SeasonStanding standing);
        Task DeleteAsync(int id);
    }
}
