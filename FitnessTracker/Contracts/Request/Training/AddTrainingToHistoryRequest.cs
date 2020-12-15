using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class AddTrainingToHistoryRequest
    {
        /// <summary>
        /// Id treningu ktore zostanie dodane do historii treningów
        /// </summary>
        public int TrainingId { get; set; }
        public List<ExerciseHistoryRequest> Exercises { get; set; }
    }
}