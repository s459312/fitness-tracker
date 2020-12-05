using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services
{
    public class TrainingService : ITrainingService
    {
        public Task<List<Training>> GetAllUserTrainingsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Training>> GetAllAvailableUserPublicTrainingsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Training>> GetAllPublicTrainingsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Training> GetTrainingByIdAsync(int trainingId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Training> CreateTrainingAsync(Training training)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateTrainingAsync(Training training)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTrainingAsync(Training training)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateTrainingExercisesAsync(int[] exerciseIds)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddTrainingToHistory(int trainingId, List<ExerciseHistory> exerciseHistories)
        {
            throw new System.NotImplementedException();
        }
    }
}