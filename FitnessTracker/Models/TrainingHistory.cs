using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class TrainingHistory
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TrainingId { get; set; }

        [ForeignKey(nameof(TrainingId))]
        public virtual Training Training { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}