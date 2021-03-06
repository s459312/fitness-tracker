﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{

    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        public int? GoalId { get; set; }

        [ForeignKey(nameof(GoalId))]
        public virtual Goal Goal { get; set; }

        //public virtual ICollection<History> Histories { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }

    }

}
