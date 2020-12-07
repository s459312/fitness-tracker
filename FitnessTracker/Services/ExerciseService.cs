using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly DatabaseContext _context;

        public ExerciseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Exercise>> GetAllExercisesAsync(PaginationFilter paginationFilter, ExerciseFilter exerciseFilter)
        {
            var queryable = _context.Exercise.AsQueryable();
            queryable = FilterExercise(queryable, exerciseFilter);
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize)
                .ToListAsync();
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

        public async Task<int> ExercisesCountAsync(ExerciseFilter exerciseFilter)
        {
            var queryable = _context.Exercise.AsQueryable();
            queryable = FilterExercise(queryable, exerciseFilter);
            return await queryable.CountAsync();
        }

        public bool AllExercisesExists(int[] ids)
        {
            int dbCount = _context.Exercise.Count(x => ids.Contains(x.Id));
            return dbCount == ids.Length;
        }
        
        public bool AllExercisesBelongsToTraining(int trainingId, int[] ids)
        {
            int dbCount = _context.TrainingExercise
                .Count(x => ids.Contains(x.ExerciseId) && x.TrainingId == trainingId);
            return dbCount == ids.Length;
        }

        private IQueryable<Exercise> FilterExercise(IQueryable<Exercise> queryable, ExerciseFilter exerciseFilter)
        {
            if (exerciseFilter.GoalId != null && exerciseFilter.GoalId.Length > 0)
                return queryable.Where(x => exerciseFilter.GoalId.Contains(x.GoalId));
            return queryable;
        }
    }
}