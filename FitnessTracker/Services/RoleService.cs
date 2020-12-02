using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
    public class RoleService : IRoleService
    {
        private readonly DatabaseContext _context;

        public RoleService(DatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return _context.Roles.ToListAsync();
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(x => x.Id == id);
        }
    }
}