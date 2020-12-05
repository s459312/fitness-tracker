using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class AddTrainingToHistoryRequest
    {
        public List<ExerciseHistoryRequest> Exercises { get; set; }
    }
}