using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsLeagueDB.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await _teamRepository.GetAll().ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }

        public async Task AddTeamAsync(Team team)
        {
            await _teamRepository.AddAsync(team);
        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Team>> GetTeamsByCityAsync(string city)
        {
            return await _teamRepository.GetAll()
                .Where(t => t.City == city)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTopWinningTeamsAsync(int topCount)
        {
            return await _teamRepository.GetAll()
                .OrderByDescending(t => t.TotalWins)
                .Take(topCount)
                .ToListAsync();
        }
    }
}
