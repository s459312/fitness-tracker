using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class Goal
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
        
        public IEnumerable<Coach> Coaches { get; set; }
        
        public IEnumerable<Exercise> Exercises { get; set; }

    }

}
