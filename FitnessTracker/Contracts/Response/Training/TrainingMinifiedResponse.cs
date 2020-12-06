namespace FitnessTracker.Contracts.Response.Training
{
    public class TrainingMinifiedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool Favourite { get; set; }
    }
}