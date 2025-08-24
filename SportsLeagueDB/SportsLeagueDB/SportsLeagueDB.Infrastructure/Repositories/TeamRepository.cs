using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsLeagueDB.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SportsLeagueDbContext _context;

        public TeamRepository(SportsLeagueDbContext context)
        {
            _context = context;
        }

        public IQueryable<Team> GetAll()
        {
            return _context.Teams.AsQueryable();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task AddAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
