using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;

namespace FitnessTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync(PaginationFilter paginationFilter);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<int> CountUsersAsync();
    }
}