using System.Collections.Generic;
using FitnessTracker.Contracts.Response.Exercise;

namespace FitnessTracker.Contracts.Response.Training
{
    public class TrainingFullResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool Favourite { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<ExerciseMinifiedResponse> TrainingExercises { get; set; }
    }
}