using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class Exercise
    {

        [Key]
        public int Id { get; set; }

        //[ForeignKey(nameof(Goal))]
        public Goal IdGoal { get; set; }

        public virtual ICollection<ExerciseTraining> ExerciseTraining { get; set; }

        [MinLength(1)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }

    }

}
