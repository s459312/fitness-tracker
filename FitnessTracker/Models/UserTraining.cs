using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class UserTraining
    {

        public int UserId { get; set; }

        public int TrainingId { get; set; }

        [ForeignKey(nameof(TrainingId))]
        public virtual Training Training { get; set; }

        [DefaultValue(false)]
        public bool Favourite { get; set; }

    }

}
