using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class HistoryStats
    {

        [Key]
        public int Id { get; set; }

        public int HistoryIdUser { get; set; }

        public int HistoryIdExercise { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HistoryDate { get; set; }
        
        public History History { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }

    }

}
