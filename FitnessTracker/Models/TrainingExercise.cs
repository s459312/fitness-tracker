using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class TrainingExercise
    {

        public int ExerciseId { get; set; }
        
        [ForeignKey(nameof(ExerciseId))]
        public virtual Exercise Exercise { get; set; }
        
        public int TrainingId { get; set; }
        
        [ForeignKey(nameof(TrainingId))]
        public virtual Training Training { get; set; }

    }

}
