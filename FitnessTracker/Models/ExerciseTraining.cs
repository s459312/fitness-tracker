using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class ExerciseTraining
    {

        [Required]
        [Key, Column(Order = 0)]
        public int IdExercise { get; set; }

        [ForeignKey(nameof(IdExercise))]
        public Exercise Exercise { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int IdTraining { get; set; }

        [ForeignKey(nameof(IdTraining))]
        public Training Training { get; set; }

    }

}
