namespace FitnessTracker.Contracts.Request.Training
{
    public class UpdateTrainingExercisesRequest
    {
        /// <summary>
        /// Lista ćwiczeń do przypisania
        /// </summary>
        public int[] Exercises { get; set; }
    }
}