namespace FitnessTracker.Contracts.Request.Training
{
    public class AddTrainingToFavouritesRequest
    {
        public int TrainingId { get; set; }
        public bool Favourite { get; set; }
    }
}