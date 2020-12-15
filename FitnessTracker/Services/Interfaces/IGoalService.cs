using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllGoalsAsync();
        bool GoalExists(int id);
    }
}