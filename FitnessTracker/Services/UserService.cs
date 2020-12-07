using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;

        public UserService(DatabaseContext context, IAuthHelper authHelper)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public async Task<List<User>> GetAllUsersAsync(PaginationFilter paginationFilter)
        {
            var queryable = _context.Users
                .Include(x => x.Role)
                .AsQueryable();

            int skip = PaginationHelper.CountSkip(paginationFilter);

            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var queryable = _context.Users
                .Include(x => x.Role)
                .AsQueryable();

            if (_authHelper.IsNormalUser())
                queryable = queryable.Where(x => x.Id == _authHelper.GetAuthenticatedUserId());

            return await queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user.RoleId == 0)
                user.Role = null;
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}