namespace FitnessTracker.Contracts.Response.Goal
{
    public class GoalResponse
    {
        /// <summary>
        /// Id celu
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Nazwa celu
        /// </summary>
        /// <example>Rekompozycja sylwetki</example>
        public string Name { get; set; }
    }
}