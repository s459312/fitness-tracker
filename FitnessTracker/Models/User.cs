using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class User
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Surname { get; set; }
        
        [Required]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public int RoleId { get; set; }
        
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [Required]
        public int GoalId { get; set; }

        [ForeignKey(nameof(GoalId))]
        public Goal Goal { get; set; }

        public virtual ICollection<Training> IdTraining { get; set; }

        public virtual ICollection<History> IdHistory { get; set; }

    }

}
