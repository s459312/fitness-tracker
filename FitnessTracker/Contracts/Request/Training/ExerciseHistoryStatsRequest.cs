namespace FitnessTracker.Contracts.Request.Training
{
    public class ExerciseHistoryStatsRequest
    {
        /// <summary>
        /// Ilość serii w ćwiczeniu
        /// </summary>
        /// <example>0</example>
        public int Serie { get; set; }

        /// <summary>
        /// Ilość powtórzeń w ćwiczeniu
        /// </summary>
        /// <example>0</example>
        public int Powtorzenia { get; set; }

        /// <summary>
        /// Ilość czasu w ćwiczeniu
        /// </summary>
        /// <example>0</example>
        public int Czas { get; set; }

        /// <summary>
        /// Ilość obciążenia w ćwiczeniu
        /// </summary>
        /// <example>0</example>
        public int Obciazenie { get; set; }

        /// <summary>
        /// Ilość dystanus w ćwiczeniu
        /// </summary>
        /// <example>0</example>
        public int Dystans { get; set; }
    }
}