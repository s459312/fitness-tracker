using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class UserTraining
    {

        [Required]
        [Key, Column(Order = 0)]
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int IdTraining { get; set; }

        [ForeignKey(nameof(IdTraining))]
        public Training Training { get; set; }

        [DefaultValue(false)]
        public bool Favourite { get; set; }

    }

}
