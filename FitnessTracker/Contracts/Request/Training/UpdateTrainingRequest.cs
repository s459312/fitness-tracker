namespace FitnessTracker.Contracts.Request.Training
{
    public class UpdateTrainingRequest
    {
        /// <summary>
        /// Nowa nazwa treningu
        /// </summary>
        /// <example>Example Training Name</example>
        public string Name { get; set; }
        
        /// <summary>
        /// Nowy opis treningu
        /// </summary>
        /// <example>Example Training Description</example>
        public string Description { get; set; }
    }
}