using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsLeagueDB.Services
{
    public class SeasonStandingService : ISeasonStandingService
    {
        private readonly ISeasonStandingRepository _seasonStandingRepository;

        public SeasonStandingService(ISeasonStandingRepository seasonStandingRepository)
        {
            _seasonStandingRepository = seasonStandingRepository;
        }

        public async Task<IEnumerable<SeasonStanding>> GetSeasonStandingsAsync()
        {
            return await _seasonStandingRepository.GetAll().ToListAsync();
        }

        public async Task<SeasonStanding> GetSeasonStandingByIdAsync(int id)
        {
            return await _seasonStandingRepository.GetByIdAsync(id);
        }

        public async Task AddSeasonStandingAsync(SeasonStanding standing)
        {
            await _seasonStandingRepository.AddAsync(standing);
        }

        public async Task UpdateSeasonStandingAsync(SeasonStanding standing)
        {
            await _seasonStandingRepository.UpdateAsync(standing);
        }

        public async Task DeleteSeasonStandingAsync(int id)
        {
            await _seasonStandingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SeasonStanding>> GetStandingsBySeasonAsync(int seasonYear)
        {
            return await _seasonStandingRepository.GetAll()
                .Where(ss => ss.SeasonYear == seasonYear)
                .ToListAsync();
        }

        public async Task<IEnumerable<SeasonStanding>> GetTopPerformingTeamsAsync(int topCount)
        {
            return await _seasonStandingRepository.GetAll()
                .OrderByDescending(ss => ss.Points)
                .Take(topCount)
                .ToListAsync();
        }
    }
}
