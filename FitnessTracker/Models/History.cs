using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class History
    {

        [Required]
        [Key, Column(Order = 0)]
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int IdExercise { get; set; }

        [ForeignKey(nameof(IdExercise))]
        public Exercise Exercise { get; set; }

        [Key, Column(Order = 2, TypeName = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
    }

}
