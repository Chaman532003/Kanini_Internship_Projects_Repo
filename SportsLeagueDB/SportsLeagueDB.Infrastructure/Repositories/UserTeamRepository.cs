using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsLeagueDB.Infrastructure.Repositories
{
    public class UserTeamRepository : IUserTeamRepository
    {
        private readonly SportsLeagueDbContext _context;

        public UserTeamRepository(SportsLeagueDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserTeam> GetAll()
        {
            return _context.UserTeams.AsQueryable();
        }

        public async Task<UserTeam> GetByIdAsync(int id)
        {
            return await _context.UserTeams.FindAsync(id);
        }

        public async Task AddAsync(UserTeam userTeam)
        {
            _context.UserTeams.Add(userTeam);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserTeam userTeam)
        {
            _context.UserTeams.Update(userTeam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userTeam = await _context.UserTeams.FindAsync(id);
            if (userTeam != null)
            {
                _context.UserTeams.Remove(userTeam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
