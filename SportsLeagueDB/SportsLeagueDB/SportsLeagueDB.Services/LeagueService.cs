using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsLeagueDB.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeagueService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<IEnumerable<SportsLeague>> GetLeaguesAsync()
        {
            return await _leagueRepository.GetAll().ToListAsync();
        }

        public async Task<SportsLeague> GetLeagueByIdAsync(int id)
        {
            return await _leagueRepository.GetByIdAsync(id);
        }

        public async Task AddLeagueAsync(SportsLeague league)
        {
            await _leagueRepository.AddAsync(league);
        }

        public async Task UpdateLeagueAsync(SportsLeague league)
        {
            await _leagueRepository.UpdateAsync(league);
        }

        public async Task DeleteLeagueAsync(int id)
        {
            await _leagueRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SportsLeague>> GetLeaguesByCountryAsync(string country)
        {
            return await _leagueRepository.GetAll()
                .Where(l => l.Country == country)
                .ToListAsync();
        }

        public async Task<int> GetTotalTeamsCountAsync()
        {
            return await _leagueRepository.GetAll()
                .SumAsync(l => l.NumberOfTeams);
        }
    }
}
