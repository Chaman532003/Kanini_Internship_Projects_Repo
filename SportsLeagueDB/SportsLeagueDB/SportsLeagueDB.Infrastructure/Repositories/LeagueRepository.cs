using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsLeagueDB.Infrastructure.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly SportsLeagueDbContext _context;

        public LeagueRepository(SportsLeagueDbContext context)
        {
            _context = context;
        }

        public IQueryable<SportsLeague> GetAll()
        {
            return _context.SportsLeagues.AsQueryable();
        }

        public async Task<SportsLeague> GetByIdAsync(int id)
        {
            return await _context.SportsLeagues.FindAsync(id);
        }

        public async Task AddAsync(SportsLeague league)
        {
            _context.SportsLeagues.Add(league);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SportsLeague league)
        {
            _context.SportsLeagues.Update(league);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var league = await _context.SportsLeagues.FindAsync(id);
            if (league != null)
            {
                _context.SportsLeagues.Remove(league);
                await _context.SaveChangesAsync();
            }
        }
    }
}
