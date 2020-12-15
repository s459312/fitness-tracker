namespace FitnessTracker.Contracts.Request.Training
{
    public class AddPublicTrainingToUserRequest
    {
        /// <summary>
        /// Id publicznego treningu który ma zostać przypisany do listy treningów użytwkonika
        /// </summary>
        public int TrainingId { get; set; }
    }
}