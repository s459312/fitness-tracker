using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class Goal
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

    }

}
