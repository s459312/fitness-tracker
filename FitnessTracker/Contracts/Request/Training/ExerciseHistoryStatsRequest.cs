namespace FitnessTracker.Contracts.Request.Training
{
    public class ExerciseHistoryStatsRequest
    {
        
        public int Serie { get; set; }

        public int Powtorzenia { get; set; }

        public int Czas { get; set; }

        public int Obciazenie { get; set; }

        public int Dystans { get; set; }
    }
}