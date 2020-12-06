using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class AddTrainingToHistoryRequest
    {
        public int TrainingId { get; set; }
        public List<ExerciseHistoryRequest> Exercises { get; set; }
    }
}