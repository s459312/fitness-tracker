using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Contracts.Response.Training;
using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<List<TrainingMinifiedResponse>> GetAllUserTrainingsAsync();
        Task<List<PublicTrainingResponse>> GetAllAvailableUserPublicTrainingsAsync();
        Task<List<Training>> GetAllPublicTrainingsAsync();
        
        /// <summary>
        /// Metoda używana tylko przy zwracaniu treningu
        /// Nie używać przy edytowani i usuwaniu
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        Task<TrainingFullResponse> GetFullTrainingByIdAsync(int trainingId);
        
        /// <summary>
        /// Metoda używana tylko przy edytowani i dodawaniu
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        Task<Training> GetTrainingByIdAsync(int trainingId);
        Task<Training> GetPublicTrainingByIdAsync(int trainingId);
        Task<Training> CreateTrainingAsync(Training training);
        Task<Training> CreatePublicTrainingAsync(Training training);
        Task<bool> AddPublicTrainingToUser(Training training);
        Task<bool> ToggleTrainingFavouriteAsync(Training training, bool favourite);
        Task<bool> UpdateTrainingAsync(Training training);
        Task<bool> DeleteTrainingAsync(Training training);
        Task<bool> DeletePublicTrainingAsync(Training training);
        Task<bool> UpdateTrainingExercisesAsync(Training training, int[] exerciseIds);
        Task<bool> AddTrainingToHistory(Training training, List<ExerciseHistory> exerciseHistories);
    }
}