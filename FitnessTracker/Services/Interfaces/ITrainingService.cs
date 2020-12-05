using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<List<Training>> GetAllUserTrainingsAsync();
        Task<List<Training>> GetAllAvailableUserPublicTrainingsAsync();
        Task<List<Training>> GetAllPublicTrainingsAsync();
        Task<Training> GetTrainingByIdAsync(int trainingId);
        Task<Training> CreateTrainingAsync(Training training);
        Task<bool> UpdateTrainingAsync(Training training);
        Task<bool> DeleteTrainingAsync(Training training);
        Task<bool> UpdateTrainingExercisesAsync(int[] exerciseIds);
        Task<bool> AddTrainingToHistory(int trainingId, List<ExerciseHistory> exerciseHistories);
    }
}