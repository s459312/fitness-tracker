using System.Collections.Generic;

namespace FitnessTracker.Contracts.Request.Training
{
    public class ExerciseHistoryRequest
    {
        /// <summary>
        /// Id ćwiczenia ktore zostanie dodane do historii ćwiczeń
        /// </summary>
        public int ExerciseId { get; set; }
        
        public ExerciseHistoryStatsRequest ExerciseHistoryStat { get; set; }
    }
}