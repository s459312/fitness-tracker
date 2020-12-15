using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;

namespace FitnessTracker.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetAllExercisesAsync(PaginationFilter paginationFilter, ExerciseFilter exerciseFilter);
        Task<Exercise> GetExerciseByIdAsync(int id);
        Task<Exercise> CreateExerciseAsync(Exercise exercise);
        Task<bool> UpdateExerciseAsync(Exercise exercise);
        Task<bool> DeleteExerciseAsync(Exercise exercise);
        Task<int> ExercisesCountAsync(ExerciseFilter exerciseFilter);
        bool AllExercisesExists(int[] ids);
        bool AllExercisesBelongsToTraining(int trainingId, int[] ids);
    }
}