using FitnessTracker.Contracts.Response.Goal;

namespace FitnessTracker.Contracts.Response.Exercise
{
    public class ExerciseResponse
    {
        
        public int Id { get; set; }
        
        public virtual GoalResponse Goal { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }
    }
}