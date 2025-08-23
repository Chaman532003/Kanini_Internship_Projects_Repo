using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsLeagueDB.Infrastructure.Repositories
{
    public class SeasonStandingRepository : ISeasonStandingRepository
    {
        private readonly SportsLeagueDbContext _context;

        public SeasonStandingRepository(SportsLeagueDbContext context)
        {
            _context = context;
        }

        public IQueryable<SeasonStanding> GetAll()
        {
            return _context.SeasonStandings.AsQueryable();
        }

        public async Task<SeasonStanding> GetByIdAsync(int id)
        {
            return await _context.SeasonStandings.FindAsync(id);
        }

        public async Task AddAsync(SeasonStanding standing)
        {
            _context.SeasonStandings.Add(standing);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SeasonStanding standing)
        {
            _context.SeasonStandings.Update(standing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var standing = await _context.SeasonStandings.FindAsync(id);
            if (standing != null)
            {
                _context.SeasonStandings.Remove(standing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
