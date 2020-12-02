using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}