using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class ExerciseHistoryRequest
    {
        public int ExerciseId { get; set; }
        
        public List<ExerciseHistoryStatsRequest> ExerciseStats { get; set; }
    }
}