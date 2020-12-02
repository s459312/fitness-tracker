using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();

        bool RoleExists(int id);
    }
}