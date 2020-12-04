namespace FitnessTracker.Contracts.Request.Exercise
{
    public class CreateExerciseRequest
    {
        /// <summary>
        /// Nazwa ćwiczenia
        /// </summary>
        /// <example>Example Exercise</example>
        public string Name { get; set; }
        
        /// <summary>
        /// Opis ćwiczenia
        /// </summary>
        /// <example>Exercise Description</example>
        public string Description { get; set; }
        
        /// <summary>
        /// Ilość serii w ćwiczeniu
        /// </summary>
        /// <example>Exercise Description</example>
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
        
        /// <summary>
        /// Cel ćwiczenia
        /// </summary>
        /// <example>1</example>
        public int GoalId { get; set; }
    }
}