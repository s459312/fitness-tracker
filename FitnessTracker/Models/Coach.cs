using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class Coach
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public int? GoalId { get; set; }

        [ForeignKey(nameof(GoalId))]
        public virtual Goal Goal { get; set; }

    }

}
