using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class Coach
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public Goal IdGoal { get; set; }

    }

}
