using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{

    public class HistoryStats
    {

        [Key]
        public int Id { get; set; }

        public int? Serie { get; set; }

        public int? Powtorzenia { get; set; }

        public int? Czas { get; set; }

        public int? Obciazenie { get; set; }

        public int? Dystans { get; set; }

        public int HistoryId { get; set; }

    }

}
