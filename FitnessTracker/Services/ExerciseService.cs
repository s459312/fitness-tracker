using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;

        public ExerciseService(DatabaseContext context, IAuthHelper authHelper)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public async Task<List<Exercise>> GetAllExercisesAsync(PaginationFilter paginationFilter)
        {
            var queryable = _context.Exercise.AsQueryable();
            queryable = FilterExercise(queryable);
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize)
                .ToListAsync();
        }

        private IQueryable<Exercise> FilterExercise(IQueryable<Exercise> queryable)
        {
            return queryable;
        }

        public async Task<Exercise> GetExerciseByIdAsync(int id)
        {
            return await _context.Exercise.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Exercise> CreateExerciseAsync(Exercise exercise)
        {
            await _context.Exercise.AddAsync(exercise);
            await _context.SaveChangesAsync();
            
            return exercise;
        }

        public async Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            _context.Exercise.Update(exercise);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteExerciseAsync(Exercise exercise)
        {
            _context.Exercise.Remove(exercise);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<int> ExercisesCountAsync()
        {
            var queryable = _context.Exercise.AsQueryable();
            queryable = FilterExercise(queryable);
            return await queryable.CountAsync();
        }
    }
}