using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class ExerciseHistoryRequest
    {
        public int ExerciseId { get; set; }
        
        public ExerciseHistoryStatsRequest ExerciseHistoryStat { get; set; }
    }
}