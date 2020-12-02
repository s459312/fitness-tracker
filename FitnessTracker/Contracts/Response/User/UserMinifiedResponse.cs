namespace FitnessTracker.Contracts.Response.User
{
    public class UserMinifiedResponse
    {
        /// <summary>
        /// Id użytkownika
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Login użytkownika
        /// </summary>
        /// <example>user@example.com</example>
        public string Login { get; set; }
    }
}