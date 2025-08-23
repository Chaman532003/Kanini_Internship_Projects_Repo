using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsLeagueDB.Services
{
    public class UserTeamService : IUserTeamService
    {
        private readonly IUserTeamRepository _userTeamRepository;

        public UserTeamService(IUserTeamRepository userTeamRepository)
        {
            _userTeamRepository = userTeamRepository;
        }

        public async Task<IEnumerable<UserTeam>> GetUserTeamsAsync()
        {
            return await _userTeamRepository.GetAll().ToListAsync();
        }

        public async Task<UserTeam> GetUserTeamByIdAsync(int id)
        {
            return await _userTeamRepository.GetByIdAsync(id);
        }

        public async Task AddUserTeamAsync(UserTeam userTeam)
        {
            await _userTeamRepository.AddAsync(userTeam);
        }

        public async Task UpdateUserTeamAsync(UserTeam userTeam)
        {
            await _userTeamRepository.UpdateAsync(userTeam);
        }

        public async Task DeleteUserTeamAsync(int id)
        {
            await _userTeamRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserTeam>> GetUserTeamsByUserIdAsync(int userId)
        {
            return await _userTeamRepository.GetAll()
                .Where(ut => ut.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTeam>> GetUserTeamsByRoleAsync(string roleInTeam)
        {
            return await _userTeamRepository.GetAll()
                .Where(ut => ut.RoleInTeam == roleInTeam)
                .ToListAsync();
        }
    }
}
