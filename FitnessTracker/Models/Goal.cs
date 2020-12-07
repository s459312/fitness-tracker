using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class Goal
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        //public virtual IEnumerable<User> Users { get; set; }

        //public virtual IEnumerable<Coach> Coaches { get; set; }

        //public virtual IEnumerable<Exercise> Exercises { get; set; }

    }

}
