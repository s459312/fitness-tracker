using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
    public class GoalService : IGoalService
    {
        private readonly DatabaseContext _context;

        public GoalService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Goal>> GetAllGoalsAsync()
        {
            return await _context.Goal.ToListAsync();
        }

        public bool GoalExists(int id)
        {
            return _context.Goal.Any(x => x.Id == id);
        }
    }
}