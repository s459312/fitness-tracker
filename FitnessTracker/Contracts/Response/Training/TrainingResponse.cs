using System.Collections.Generic;

namespace FitnessTracker.Contracts.Response.Training
{
    public class TrainingResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ExerciseResponse> TrainingExercises { get; set; }
    }
}