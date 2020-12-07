namespace FitnessTracker.Contracts.Request.Training
{
    public class CreateTrainingRequest
    {
        /// <summary>
        /// Nazwa nowego treningu
        /// </summary>
        /// <example>Example Training Name</example>
        public string Name { get; set; }
        
        /// <summary>
        /// Opis nowego treningu
        /// </summary>
        /// <example>Example Training Description</example>
        public string Description { get; set; }
    }
}