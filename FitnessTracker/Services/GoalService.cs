using System.Linq;
using FitnessTracker.Data;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services
{
    public class GoalService : IGoalService
    {
        private readonly DatabaseContext _context;

        public GoalService(DatabaseContext context)
        {
            _context = context;
        }

        public bool GoalExists(int id)
        {
            return _context.Goal.Any(x => x.Id == id);
        }
    }
}