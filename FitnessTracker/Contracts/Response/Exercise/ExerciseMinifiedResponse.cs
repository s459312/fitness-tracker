namespace FitnessTracker.Contracts.Response.Exercise
{
    public class ExerciseMinifiedResponse
    {
        public int Id { get; set; }
        
        public string Goal { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }
    }
}