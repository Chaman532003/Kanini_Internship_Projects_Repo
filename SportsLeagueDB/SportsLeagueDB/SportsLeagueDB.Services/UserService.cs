using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using SportsLeagueDB.SportsLeagueDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsLeagueDB.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAll().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _userRepository.GetAll()
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersByNameAsync(string namePart)
        {
            return await _userRepository.GetAll()
                .Where(u => u.UserName.Contains(namePart))
                .ToListAsync();
        }
    }
}
